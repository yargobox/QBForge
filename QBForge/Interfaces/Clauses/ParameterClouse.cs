namespace QBForge.Interfaces.Clauses
{
	public class ParameterClouse : ValueClause<object?>
	{
		public ParameterClouse(object? value) : base(value) { }

		public override void Render(IBuildQueryContext context)
		{
		}

		public override string ToString()
		{
			return Value?.ToString() ?? "?";
		}
	}
}