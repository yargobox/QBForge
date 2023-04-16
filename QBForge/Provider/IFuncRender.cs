using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
	public interface IFuncRender : IRenderContext { }

	public delegate IFuncRender UnaryFuncHandler(IFuncRender render, Clause arg);

	public delegate IFuncRender BinaryFuncHandler(IFuncRender render, Clause arg1, Clause arg2);

	public delegate IFuncRender TernaryFuncHandler(IFuncRender render, Clause arg1, Clause arg2, Clause arg3);
}