#pragma warning disable CA1716

namespace QBForge.Interfaces
{
	public interface IWithCteQB : IQueryBuilder
	{
		IWithCteQB With<TCte>(string labelCte, ISelectQB<TCte> subQuery);

		ISelectQB<T> Select<T>(string tableName, string? labelAs = null, dynamic? parameters = null);


		//IUpdateQB<T> Update<T>(string tableName, string? labelAs = null);
	}
}

#pragma warning restore CA1716