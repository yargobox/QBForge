namespace QBForge.Interfaces.Clauses
{
	public class TableClause : ValueClause<ObjectEntry>
	{
		public TableClause(ObjectEntry objectEntry) : base(objectEntry) { }
		public TableClause(string? schemaName, string objectName) : base(new ObjectEntry(schemaName, objectName)) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(Value.SchemaName))
			{
				render.AppendObject(Value.SchemaName!);
				render.Append('.');
			}
			render.AppendObject(Value.ObjectName);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}