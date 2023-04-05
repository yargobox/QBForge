namespace QBForge.Interfaces.Clauses
{
	public class TableClouse : ConstClause
	{
		public string? SchemaName { get; }
		public string TableName { get; }
		public string? LabelAs { get; }

		public TableClouse(string? schemaName, string tableName, string? labelAs = null)
		{
			SchemaName = schemaName;
			TableName = tableName;
			LabelAs = labelAs;
		}

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(SchemaName))
			{
				render.AppendObject(SchemaName!);
				render.Append('.');
			}
			render.AppendObject(TableName);
			if (!string.IsNullOrEmpty(LabelAs))
			{
				render.Append(' ');
				render.AppendAsLabel(LabelAs!);
			}
		}
	}
}
