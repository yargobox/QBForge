using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
	public interface IAggrRender : IRenderContext { }

	public delegate IAggrRender AggrHandler(IAggrRender render);

	public delegate IAggrRender UnaryAggrHandler(IAggrRender render, Clause arg);
}