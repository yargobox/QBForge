namespace QBForge.Provider.Clauses
{
	public class ConstClause<T> : ValueClause<T>
	{
		public ConstClause(T value) : base(value) { }

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.Append(Value?.ToString() ?? string.Empty);
		}

		public override Clause Clone()
		{
			return this;
		}
	}

	public class ConstClause : ConstClause<object?>
	{
		public ConstClause(object? value) : base(value) { }
	}
}