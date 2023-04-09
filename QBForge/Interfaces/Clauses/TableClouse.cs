namespace QBForge.Interfaces.Clauses
{
	public class TableClouse : ValueClause<ObjectEntry>
	{
		public TableClouse(string? schemaName, string objectName, string? label = null)
			: base(new ObjectEntry(schemaName, objectName, label)) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(Value.SchemaName))
			{
				render.AppendObject(Value.SchemaName!);
				render.Append('.');
			}
			render.AppendObject(Value.ObjectName);
			if (!string.IsNullOrEmpty(Value.Label))
			{
				render.Append(' ');
				render.AppendAsLabel(Value.Label!);
			}
		}

		public override string ToString()
		{
			var result = string.IsNullOrEmpty(Value.SchemaName)
				? Value.ObjectName
				: string.Concat(Value.SchemaName!, ".", Value.ObjectName);

			if (!string.IsNullOrEmpty(Value.Label))
			{
				result = string.Concat(result, " AS ", Value.Label);
			}

			return result;
		}
	}
}
