using QBForge.Interfaces.Clauses;

namespace QBForge.Interfaces
{
	public interface IAggrRender : IRenderContext { }

	public delegate IAggrRender AggrHandler(IAggrRender render);

	public delegate IAggrRender UnaryAggrHandler(IAggrRender render, Clause arg);
}