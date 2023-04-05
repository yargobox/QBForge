namespace QBForge.Interfaces.Clauses
{
	public class DataEntryClouse : ConstClause
	{
		public override string? Key => DataEntry.Name;
		public DataEntry DataEntry { get; }

		public DataEntryClouse(DataEntry de)
		{
			DataEntry = de;
		}

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(DataEntry.Label))
			{
				render.AppendLabel(DataEntry.Label!);
				render.Append('.');
			}
			render.AppendObject(DataEntry.Name);
		}
	}
}
