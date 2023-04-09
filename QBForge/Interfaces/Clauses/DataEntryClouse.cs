namespace QBForge.Interfaces.Clauses
{
	public class DataEntryClouse : ValueClause<DataEntry>
	{
		public override string? Key => Value.Name;

		public DataEntryClouse(DataEntry de) : base(de) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(Value.RefLabel))
			{
				render.AppendLabel(Value.RefLabel!);
				render.Append('.');
			}
			render.AppendObject(Value.Name);
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(Value.RefLabel)
				? Value.Name
				: string.Concat(Value.RefLabel!, ".", Value.Name);
		}
	}
}
