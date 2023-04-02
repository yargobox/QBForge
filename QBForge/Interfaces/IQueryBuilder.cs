namespace QBForge.Interfaces
{
	public interface IQueryBuilder
	{
		IQBContext Context { get; }

		string ToString(bool pretty);
		string ToString(out object? parameters, bool pretty);
		string ToString(out object? parameters, ReadabilityLevels level = ReadabilityLevels.Default);
		string ToString(ReadabilityLevels level);
	}

	public interface IQueryBuilder<T> : IQueryBuilder
	{
	}
}