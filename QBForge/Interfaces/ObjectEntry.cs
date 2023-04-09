namespace QBForge.Interfaces
{
	public readonly record struct ObjectEntry
	{
		public readonly string? SchemaName { get; }
		public readonly string ObjectName { get; }
		public readonly string? Label { get; }

		public ObjectEntry(string? schemaName, string objectName, string? label = null)
		{
			SchemaName = string.IsNullOrEmpty(schemaName) ? null : schemaName;
			ObjectName = objectName;
			Label = string.IsNullOrEmpty(label) ? null : label;
		}
	}
}