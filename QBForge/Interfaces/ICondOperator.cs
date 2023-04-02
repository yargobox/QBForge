namespace QBForge.Interfaces
{
	public interface ICondOperator : IRenderContext { }

	public delegate ICondOperator CondOperatorDe(ICondOperator query, DataEntry lhs);

	public delegate ICondOperator CondOperatorDeV(ICondOperator query, DataEntry lhs, string rhs);

	public delegate ICondOperator CondOperatorDeDe(ICondOperator query, DataEntry lhs, DataEntry rhs);

	public delegate ICondOperator CondOperatorDeVV(ICondOperator query, DataEntry lhs, dynamic rhs1, dynamic rhs2);

	public delegate ICondOperator CondOperatorDeDeV(ICondOperator query, DataEntry lhs, DataEntry rhs1, dynamic rhs2);

	public delegate ICondOperator CondOperatorDeVDe(ICondOperator query, DataEntry lhs, dynamic rhs1, DataEntry rhs2);

	public delegate ICondOperator CondOperatorDeDeDe(ICondOperator query, DataEntry lhs, DataEntry rhs1, DataEntry rhs2);

	public delegate ICondOperator CondOperatorVDeDe(ICondOperator query, dynamic lhs, DataEntry rhs1, DataEntry rhs2);
}