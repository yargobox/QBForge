namespace QBForge.Interfaces.Clauses
{
	public class OnClause : BlockClause
	{
		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext
				.Append("ON ");

			foreach (var clause in this)
			{
				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new OnClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}