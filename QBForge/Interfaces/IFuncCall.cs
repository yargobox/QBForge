namespace QBForge.Interfaces
{
	public interface IFuncCall : IRenderContext { }

	public delegate IFuncCall FuncCallClauseDe(IFuncCall query, DataEntry arg);
	public delegate IFuncCall FuncCallClauseDeV(IFuncCall query, DataEntry arg1, string arg2);
	public delegate IFuncCall FuncCallClauseDeDe(IFuncCall query, DataEntry arg1, DataEntry arg2);
}