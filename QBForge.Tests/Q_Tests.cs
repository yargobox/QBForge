using QBForge.PostgreSql;
using QBForge.Provider;

namespace QBForge.Tests
{
	[Collection(nameof(SharedTestContextFixture))]
	public class Q_Tests
	{
		private readonly SharedTestContextFixture _sharedContext;

		public Q_Tests(SharedTestContextFixture fixture)
        {
			_sharedContext = fixture;
		}

		[Fact]
		public void Select()
		{
			string? sql;
			/* SELECT "ProductId", "Name", "Price", "CategoryId", "BrandId" FROM "products" */
			
			sql = QB.Select<Product>("products").ToString();
			// the same as
			sql = QB.Select<Product>("products").Include(p => p).ToString();
			// and the same as
			sql = QB
				.Select<Product>("products")
					.Include(p => p.ProductId)
					.Include(p => p.Name)
					.Include(p => p.Price)
					.Include(p => p.GroupId)
					.Include(p => p.BrandId)
				.ToString();

			// and the same as

			

			sql = QB.Select<Brand>("brands").IncludeAll().ToString();

			var q = QB
				.Select<Product>("products", "p")
						.Include(p => p.ProductId)
						.Include(p => p.Name)
						.Include(p => p.Price)
						.Include(p => p.GroupId)
						.Include(p => p.BrandId)
					.MapNextTo<Brand>(p => p.Brand!)
						.Include<Brand>(b => b.BrandId)
						.Include<Brand>(b => b.Name)
					.MapNextTo<Group>(p => p.Group!)
						.Include<Group>(g => g.GroupId)
						.Include<Group>(g => g.Name)
					.MapNextTo<Category>(p => p.Group!.Category!)
						.Include<Category>(c => c.CategoryId)
						.Include<Category>(c => c.Name)
				.Join<Brand>("brands", "b")
					.On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)
				.LeftJoin<Group>("groups", "g")
					.On<Group>(g => g.GroupId, Op.Equal, p => p.GroupId)
				.LeftJoin<Category>("categories", "c")
					.On<Category, Group>(c => c.CategoryId, Op.Equal, g => g.CategoryId)
				.Where(p => p.Price, Op.Less, 12.50).OrWhere(p => p.Price, Op.IsNull)
				.OrderBy(p => p.Price, Ob.DESC_NULLS_LAST)
				.Offset(20)
				.Limit(10);

			q = QB
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

			q = QB
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

			QB.Select<Product>("products", "p")
				.Include(p => p.BrandId)
			.GroupBy(p => p.BrandId)
			.Having(Ag.MIN, p => p.Price, Op.Greater, 100.00);

			// Current version
			q = QB.Select<Product>("products", "p").IncludeComputed(Ag.AVG, p => p.Price, "AveragePrice").Where(p => p.GroupId, Op.Equal, 8);
			// Next version
			//q = QB.Select<Product>("products", "p").IncludeRaw("AVG(p.\"Price\")", "AveragePrice").WhereRaw("p.\"GroupId\" == 8");
			// Next version
			//q = QB.Select<Product>("products", "p").Include(p => Ag.AVG(p.Price), "AveragePrice").Where(p => p.GroupId == 8);

			//q = QB
			//	.Select<Product>("products", "p")
			//		.Include(p => p)
			//		.Include<Category>(c => c.CategoryId)
			//		.Include<Category>(c => c.Name)
			//		.Include<Brand>(b => b)
			//	.Join<Category>("categories", "c")
			//		.On<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId)
			//	.LeftJoin<Brand>("brands", "b")
			//		.On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)
			//	.Where(_ => _.Where(p => p.Price, Op.Greater, 12.00).Where(p => p.Price, Op.Less, 100.00)).OrWhere(p => p.Price, Op.IsNull)
			//	.OrderBy(p => p.Price)
			//	.OrderBy(p => p.Price, Ob.DESC)
			//	.OrderBy<Brand>(b => b.Name, Ob.ASC)
			//	.OrderBy(Ag.COUNT_1)
			//	.OrderBy(Ag.COUNT_ALL, Ob.DESC)
			//	.OrderBy(Ag.COUNT, p => p.CategoryId, Ob.DESC)
			//	.OrderBy(p => p.Name, Ob.DESC_NULLS_LAST)
			//	.OrderBy(p => p.Name, Ob.ASC_NULLS_FIRST)
			//	.GroupBy(p => p.Price)
			//	.GroupBy<Brand>(b => b.Name)
			//	.Where(Op.Exists, QB.Select<Brand>("brands", "b").Where(b => b.BrandId, Op.IsNotNull))

			//	.UnionAll(
			//		QB.Select<Product>("products")
			//			.Include(p => p.BrandId)
			//		.GroupBy(p => p.BrandId)
			//		.Having(Ag.MIN, p => p.Price, Op.Greater, 15.00)
			//	)

			//	.Offset(10)
			//	.Limit(20)

			//	.Map<Group, Brand>((p, b, g) => { p.Brand = b; p.Group = g; return p; })
			//;

			var query = q.ToString(ReadabilityLevels.High);
			var len = q.Length;
		}

		//[Fact]
		//public void Select2()
		//{
		//	var q2 = QB
		//		.Select<Product>("products", "p")
		//			.Include(p => p)
		//			.Include(p => p.ProductId)
		//			.Include(p => p.Name)
		//			.Include(p => p.Price)
		//			.Include(p => p.CategoryId)
		//			.Include(p => p.Category)

		//			.Include(Ag.MAX, p => p.Price, "max")
		//			.Include(Fn.ISNULL, p => p.Price, 0)
		//			.Include(Fn.LOWER, p => p.Name)

		//		.LeftJoin<Category>("categories", "c")
		//			.On<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId).OrOn<Category>(c => c.Name, Op.IsNull)

		//		.LeftJoin<Brand>("brands", "b").On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)

		//		.Where(_ => _.Where(p => p.Price, Op.IsNull).OrWhere(p => p.Price, Op.Greater, 15.80))
		//		.OrWhere(p => p.Name, Op.IsNull)
		//		.OrWhere<Brand>(b => b.Name, Op.IsNull)
		//		.OrWhere<Brand>(b => b.Name, Op.Greater, 20.00)
		//		.Where<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId)
		//		.Where<Category, Brand>(c => c.Name, Op.Equal, b => b.Name)

		//		.OrderBy(p => p.Price, Ob.ASC)

		//		.Offset(10)
		//		.Limit(20)

		//		.UnionAll(
		//			QB.Select<Product>("products")
		//				.Include(p => p.CategoryId)
		//			.GroupBy(p => p.CategoryId)
		//			.Having(Ag.MIN, p => p.Price, Op.Greater, 15.00)
		//		)
		//	;
		//}

		[Fact]
		public void WithCte()
		{
			var q =
				QB.With("b",
					QB.Select<Brand>("brands").IncludeAll()
				)
				.With("c",
					QB.Select<Category>("categories")
				)
				.Select<Product>("products", "p")
				.From("c", "c2")
			;

			var query = q.ToString(ReadabilityLevels.High);
			var len = query.Length;
		}

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
	}
}