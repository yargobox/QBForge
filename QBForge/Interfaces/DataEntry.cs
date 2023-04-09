namespace QBForge.Interfaces
{
	public readonly record struct DataEntry
	{
		public readonly string? RefLabel { get; }
		public readonly string Name { get; }

		public DataEntry(string? label, string name)
		{
			RefLabel = string.IsNullOrEmpty(label) ? null : label;
			Name = name;
		}
	}
}