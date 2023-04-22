# QBForge
QBForge is a simple query builder that supports PostgreSQL, SQL Server, etc.
It allows you to write queries that are closer to SQL syntax than LINQ, but retain their relationship to the Entity/Document model.

## When should it be used?
- When EF and LINQ are not sufficient or suitable, but you do not want to manually assemble raw queries.
- When you want business logic to stay at the domain level, and not settle down in repositories at the infrastructure (data access) level.
- When you need an easy way to provide filtering, ordering, and pagination.
- When you need more control over the query code to clearly apply certain SQL constructs and keep query structure stable avoiding inefficient execution plans.
- When you want to keep the ability to migrate to another DBMS in the future, but do not want to be bounded in functionality because of this.

## Structure, implementation, some ideology
QBForge provides interfaces for all the main types of operations: `SELECT`, `INSERT`, `UPDATE`, `DELETE`, `MERGE`, and the `WITH` CTE clause.
These interfaces are implemented by the underlying provider and elaborate by the provider specific to each DBMS.

QBForge is extendable. User is free to override the operation interface and provider method implementations, extend the operation interfaces with its own methods,
add support for specific operators, functions, and aggregates.

The query structure is stored as a composite tree.
The `Clause` abstract class is used for both components and leaf abstractions.
It may look like `Expression`, but it's not, and it's mutable.
For performance reasons, most `Clause` implementations have been made immutable, but section clauses that describe, for example, the entire `SELECT`, column include section,
`FROM` section, `JOIN`, `WHERE`, and others derive from `BlockClause` are mutable.
The operation interfaces and clauses are clonable. Immutable clauses return `this`, mutable ones perform a deep copy.

As with providers, the `DocumentMapping` class is instantiated for each DBMS.
It provides the query builder (via the provider) with information about the document and its structure, (columns to include, their mapped names and order).
You can also override its implementation partially or completely if the standard functionality is not enough.
By default, a document type will be mapped using its public writable properties (but not those of the base classes) and their names.
You can use `ColumnAttribute` (`Name` and `Order`) to include non-public properties or fields, specify mapped name aand column order.
`NotMappedAttribute` will exclude public writable properties from mapping.

QBForge has a separate namespace for each provider.
To change the provider, you must change the `using` namespace in the source files, or use `global using`.
The static classes in each provider's namespace have the same methods for this purpose, except for features supported only by that provider.
This means that you can only create and use a query builder with a specific provider.

## How does it look like?
### Entities
```csharp
	public class Brand
	{
		public int BrandId { get; set; }
		public string? Name { get; set; }
	}

	public class Category
	{
		public int CategoryId { get; set; }
		public string? Name { get; set; }
	}

	public class Group
	{
		public int GroupId { get; set; }
		public int CategoryId { get; set; }
		public string? Name { get; set; }

		public Category? Category { get; set; }
	}

	public class Product
	{
		public int ProductId { get; set; }
		public int BrandId { get; set; }
		public int GroupId { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }

		public Brand? Brand { get; set; }
		public Group? Group { get; set; }
	}
```

### Query examples
#### Simple select
```csharp
	sql = QB.Select<Product>("products").ToString();
	// the same as
	sql = QB.Select<Product>("products").Include(p => p).ToString();
	// and the same as
	sql = QB
		.Select<Product>("products")
			.Include(p => p.ProductId)
			.Include(p => p.BrandId)
			.Include(p => p.GroupId)
			.Include(p => p.Name)
			.Include(p => p.Price)
		.ToString();
```
```sql
SELECT "ProductId", "BrandId", "GroupId", "Name", "Price" FROM products
```

#### Select all
```csharp
	sql = QB.Select<Brand>("brands").IncludeAll().ToString();
```
```sql
SELECT * FROM brands
```

#### Select with joins, conditions, sort order, and pagination plus mapping hints for Dapper
```csharp
	var q = QB
		.Select<Product>("products", "p")
				.Include(p => p)
			.MapNextTo<Brand>(p => p.Brand!)
				.Include<Brand>(b => b)
			.MapNextTo<Group>(p => p.Group!)
				.Include<Group>(g => g)
			.MapNextTo<Category>(p => p.Group!.Category!)
				.Include<Category>(c => c)
		.LeftJoin<Brand>("brands", "b")
			.On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)
		.Join<Group>("groups", "g")
			.On<Group>(g => g.GroupId, Op.Equal, p => p.GroupId)
		.Join<Category>("categories", "c")
			.On<Category, Group>(c => c.CategoryId, Op.Equal, g => g.CategoryId)
		.Where(p => p.Price, Op.IsNull).OrWhere(p => p.Price, Op.Less, 12.50)
		.OrderBy(p => p.Price, Ob.DESC_NULLS_LAST)
		.Offset(20)
		.Limit(10)
	;

	// Dapper's query
	var products = connection.Query<Products, Brand, Group, Category, Products>(
		sql: q.Context.CommndText,
		map: q.Context.Map,
		param: q.Context.Parameters,
		spliOn: q.Context.MapSplitters
	);

	// or
	var products = connection.Query<Products>(
		sql: q.Context.CommndText,
		types: q.Context.MapTypes,
		map: q.Context.Map,
		param: q.Context.Parameters,
		spliOn: q.Context.MapSplitters
	);

	// or even (using an extension method aka static Enumerable<T> ToEnumerable<T>(this ISelectQB<T> qb, NpgsqlConnection connection))
	var products = q.ToEnumerable(connection);
```
```sql
SELECT
	p."ProductId",
	p."BrandId",
	p."GroupId",
	p."Name",
	p."Price",
	b."BrandId",
	b."Name",
	g."GroupId",
	g."Name",
	c."CategoryId",
	c."Name"
FROM products AS p
	LEFT JOIN brands AS b
		ON b."BrandId" = p."BrandId"
	INNER JOIN groups AS g
		ON b."GroupId" = p."GroupId"
	INNER JOIN categories AS c
		ON c."CategoryId" = g."CategoryId"
WHERE
	p."Price" IS NULL OR p."Price" < $1
ORDER BY
	p."Price" DESC NULLS LAST
OFFSET 20
LIMIT 10
```

#### Select with subquery
```csharp
	var q = QB
		.Select<Product>("products", "p")
		.Join<Product>
		(
			QB.Select<Product>("products")
				.Include(_ => _.BrandId)
			.GroupBy(_ => _.BrandId)
			.Having(Ag.MIN, _ => _.Price, Op.Greater, 200.00),
			"expensiveBrands"
		)
			.On<Product>(expensiveBrands => expensiveBrands.BrandId, Op.Equal, p => p.BrandId)
		.OrderBy(p => p.Price, Ob.DESC_NULLS_LAST)
		.Limit(15)
	;
```
```sql
SELECT
	p."ProductId",
	p."BrandId",
	p."GroupId",
	p."Name",
	p."Price"
FROM products AS p
	INNER JOIN
	(
		SELECT "BrandId" FROM products GROUP BY "BrandId" HAVING MIN("Price") > $1
	)
	AS "expensiveBrands"
		ON "expensiveBrands"."BrandId" = p."BrandId"
ORDER BY
	p."Price" DESC NULLS LAST
LIMIT 15
```

#### With CTE select
```csharp
	var q = QB
		.With("expensiveBrands",
			QB.Select<Product>("products")
				.Include(_ => _.BrandId)
			.GroupBy(_ => _.BrandId)
			.Having(Ag.MIN, _ => _.Price, Op.Greater, 200.00)
		)
		.Select<Product>("products", "p")
		.Join<Product>("expensiveBrands", "expensiveBrands")
			.On<Product>(expensiveBrands => expensiveBrands.BrandId, Op.Equal, p => p.BrandId)
		.OrderBy(p => p.Price, Ob.DESC_NULLS_LAST)
		.Limit(15)
	;
```
```sql
WITH "expensiveBrands"
(
	SELECT "BrandId" FROM products GROUP BY "BrandId" HAVING MIN("Price") > $1
)
SELECT
	p."ProductId",
	p."BrandId",
	p."GroupId",
	p."Name",
	p."Price"
FROM products AS p
	INNER JOIN "expensiveBrands" AS "expensiveBrands"
		ON "expensiveBrands"."BrandId" = p."BrandId"
ORDER BY
	p."Price" DESC NULLS LAST
LIMIT 15
```

## Parameters
They will definitely be stored per clause by the query builder and assembled at build time in the correct order.
Named parameters can be used when pointing to a specific function argument.

## Future plans

### `Expression` and "raw" SQL support
At this point, the builder does not perform type checking.
In the future, it will support conditional expressions to describe conditions (eg `WHERE`, `ON`, `HAVING`)
and computed expressions in some other clauses (eg `SELECT`).
Using `Expression` in this case would also mean type checking.

```csharp
	// Current version (in development)
	QB.Select<Product>("products", "p").IncludeComputed(Ag.AVG, p => p.Price, "AveragePrice").Where(p => p.GroupId, Op.Equal, 8);
	// Next version
	QB.Select<Product>("products", "p").IncludeRaw("AVG(p.\"Price\")", "AveragePrice").WhereRaw("p.\"GroupId\" == 8");
	// Next version
	QB.Select<Product>("products", "p").Include(p => Ag.AVG(p.Price), "AveragePrice").Where(p => p.GroupId == 8);
```

I continue to play with the select interface as the most significant one, which will hint the best way to define others.
Thus, the final version and the query tree still can be refactored.

### SQLite, MySql, Oracle
Support for SQLite, MySql, and Oracle will be added after PostgreSQL and SQL Server.

### Repository implementations
To create a unified repository, you need to limit the filtering and ordering operations, as well as implement a single interface for such a repository for each provider you need.
Repository support from the box may be added in the future as a separate module.

### MongoDB support
This query builder approach can also be used to create document-oriented queries.
Joins, for example, can be translated into lookups and unwinds, columns includes into projects.
Specific commands can be added as extension methods.
Whatever, this requires a lot of research work.
Such a query builder can't be stupid to produce the valid result at the end, so the query tree has to be analized and reorganized beforehand.

## About
This project is an offshoot of another research project "QBCore".
QBCore is an attempt to reconsider the monolithic application model, based on the approach to describing functionality and behavior,
in combination with stateful datasources and complex datasources, into a microservices model based on stateless repositories and complex repositories.
QBForge is another look at the implementation of the query builder layer in QBCore.
It is more flexible, allows you to use more native database features, be more specific in the description of the query,
but unlike the approach used in QBCore, it does not provide a unified query interface.

# PS
The author has a full-time job, so he cannot work on the project every day.
Now the plans are to complete it within one to two years.
If you are interested in this project or have some good ideas, please let me know. I am open to communication.

Also, I have to say that a donation, grant, or offer will set aside my other stuff and I will finish the project much faster :)