using QBForge.PostgreSql;

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
			var q = QB
				.Select<Product>("products", "p")
					.Include(p => p)
					.Include<Category>(c => c.CategoryId)
					.Include<Category>(c => c.Name)
					.Include<Brand>(b => b)
				.Join<Category>("categories", "c")
					.On<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId)
				.LeftJoin<Brand>("brands", "b")
					.On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)
				.Where(_ => _.Where(p => p.Price, Op.Greater, 12.00).Where(p => p.Price, Op.Less, 100.00)).OrWhere(p => p.Price, Op.IsNull)
				.OrderBy(p => p.Price)
				.OrderBy(p => p.Price, Ob.DESC)
				.OrderBy<Brand>(b => b.Name, Ob.ASC)
				.GroupBy(p => p.Price)
				.GroupBy<Brand>(b => b.Name)
				.Where(Op.Exists, QB.Select<Brand>("brands", "b").Where(b => b.BrandId, Op.IsNotNull))

				.UnionAll(
					QB.Select<Product>("products")
						.Include(p => p.CategoryId)
					.GroupBy(p => p.CategoryId)
					.Having(Ag.MIN, p => p.Price, Op.Greater, 15.00)
				)

				.Map<Category, Brand>((p, c, b) => { p.Category = c; p.Brand = b; return p; })
			;

			var query = q.ToString(Interfaces.ReadabilityLevels.High);
			var len = query.Length;
		}

		[Fact]
		public void Select2()
		{
			var q2 = QB
				.Select<Product>("products", "p")
					.Include(p => p)
					.Include(p => p.ProductId)
					.Include(p => p.Name)
					.Include(p => p.Price)
					.Include(p => p.CategoryId)
					.Include(p => p.Category)

					.Include(Ag.MAX, p => p.Price, "max")
					.Include(Fn.ISNULL, p => p.Price, 0)
					.Include(Fn.LOWER, p => p.Name)

				.LeftJoin<Category>("categories", "c")
					.On<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId).OrOn<Category>(c => c.Name, Op.IsNull)

				.LeftJoin<Brand>("brands", "b").On<Brand>(b => b.BrandId, Op.Equal, p => p.BrandId)

				.Where(_ => _.Where(p => p.Price, Op.IsNull).OrWhere(p => p.Price, Op.Greater, 15.80))
				.OrWhere(p => p.Name, Op.IsNull)
				.OrWhere<Brand>(b => b.Name, Op.IsNull)
				.OrWhere<Brand>(b => b.Name, Op.Greater, 20.00)
				.Where<Category>(c => c.CategoryId, Op.Equal, p => p.CategoryId)
				.Where<Category, Brand>(c => c.Name, Op.Equal, b => b.Name)

				.OrderBy(p => p.Price, Ob.ASC)

				.Skip(10)
				.Take(20)

				.UnionAll(
					QB.Select<Product>("products")
						.Include(p => p.CategoryId)
					.GroupBy(p => p.CategoryId)
					.Having(Ag.MIN, p => p.Price, Op.Greater, 15.00)
				)
			;
		}

		public class Product
		{
			public int ProductId { get; set; }
			public int CategoryId { get; set; }
			public int BrandId { get; set; }
			public string? Name { get; set; }
			public decimal Price { get; set; }

			public virtual Category? Category { get; set; }
			public virtual Brand? Brand { get; set; }
		}

		public class Category
		{
			public int CategoryId { get; set; }
			public string? Name { get; set; }
		}

		public class Brand
		{
			public int BrandId { get; set; }
			public string? Name { get; set; }
		}
	}
}