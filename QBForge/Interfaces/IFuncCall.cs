namespace QBForge.Interfaces
{
	public interface IFuncCall : IRenderContext { }

	public delegate IFuncCall FuncCallClauseDe(IFuncCall render, DataEntry arg);
	public delegate IFuncCall FuncCallClauseDeV(IFuncCall render, DataEntry arg1, string arg2);
	public delegate IFuncCall FuncCallClauseDeDe(IFuncCall render, DataEntry arg1, DataEntry arg2);
}