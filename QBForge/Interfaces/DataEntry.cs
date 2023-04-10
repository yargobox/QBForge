using System.Security.AccessControl;

namespace QBForge.Interfaces
{
	public readonly record struct DataEntry
	{
		public readonly string? RefLabel { get; }
		public readonly string Name { get; }

		public DataEntry(string? refLabel, string name)
		{
			RefLabel = string.IsNullOrEmpty(refLabel) ? null : refLabel;
			Name = name;
		}

		public override string ToString()
		{
			return RefLabel == null ? Name : string.Concat(RefLabel, ".", Name);
		}

		public override int GetHashCode()
		{
			return RefLabel == null ? Name.GetHashCode() : RefLabel.GetHashCode() ^ Name.GetHashCode();
		}
	}
}