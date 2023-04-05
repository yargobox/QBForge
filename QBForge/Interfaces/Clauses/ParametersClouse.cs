namespace QBForge.Interfaces.Clauses
{
	public class ParametersClouse : Clause
	{
		public object? Value { get; }

		public ParametersClouse(object? value)
		{
			Value = value;
		}

		public override void Render(IBuildQueryContext context)
		{
		}
	}
}
