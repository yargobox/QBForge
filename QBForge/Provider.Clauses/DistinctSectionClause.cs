namespace QBForge.Provider.Clauses
{
	internal class DistinctSectionClause : TextClause
	{
		public static DistinctSectionClause Default => Static.Default;

		public override string? Key => ClauseSections.Distinct;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.Append("DISTINCT");
		}

		public override string ToString()
		{
			return "DISTINCT";
		}

		public override Clause Clone()
		{
			return this;
		}

		private static class Static
		{
			public static readonly DistinctSectionClause Default = new DistinctSectionClause();

			static Static() { }
		}
	}
}
