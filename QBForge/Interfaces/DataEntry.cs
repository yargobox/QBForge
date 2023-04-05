namespace QBForge.Interfaces
{
	public readonly record struct DataEntry
	{
		public readonly string? Label { get; }
		public readonly string Name { get; }

		public DataEntry(string? label, string name)
		{
			Label = string.IsNullOrEmpty(label) ? null : label;
			Name = name;
		}
	}
}