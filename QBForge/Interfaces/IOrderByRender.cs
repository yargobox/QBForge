using QBForge.Interfaces.Clauses;

namespace QBForge.Interfaces
{
	public interface IOrderByRender : IRenderContext { }

	public delegate IOrderByRender UnaryOrderByHandler(IOrderByRender render, Clause arg);
}