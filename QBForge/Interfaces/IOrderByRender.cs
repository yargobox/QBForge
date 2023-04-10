using QBForge.Interfaces.Clauses;

namespace QBForge.Interfaces
{
	public interface IOrderByRender : IRenderContext { }

	public delegate IOrderByRender OrderByHandler(IOrderByRender render, Clause arg);
}