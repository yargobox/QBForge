namespace QBForge.Interfaces
{
	public readonly record struct ObjectEntry
	{
		public readonly string? SchemaName { get; }
		public readonly string ObjectName { get; }

		public ObjectEntry(string? schemaName, string objectName)
		{
			SchemaName = string.IsNullOrEmpty(schemaName) ? null : schemaName;
			ObjectName = objectName;
		}

		public override string ToString()
		{
			return SchemaName == null ? ObjectName : string.Concat(SchemaName, ".", ObjectName);
		}

		public override int GetHashCode()
		{
			return SchemaName == null ? ObjectName.GetHashCode() : SchemaName.GetHashCode() ^ ObjectName.GetHashCode();
		}
	}
}