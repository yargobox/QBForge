using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
	public interface IOrderByRender : IRenderContext { }

	public delegate IOrderByRender UnaryOrderByHandler(IOrderByRender render, Clause arg);
}