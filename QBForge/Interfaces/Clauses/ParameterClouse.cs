namespace QBForge.Interfaces.Clauses
{
	public class ParameterClause : ValueClause<object?>
	{
		public ParameterClause(object? value) : base(value) { }

		public override void Render(IBuildQueryContext context)
		{
		}

		public override string ToString()
		{
			return Value?.ToString() ?? "?";
		}

		public override Clause Clone()
		{
			return this;
		}
	}
}