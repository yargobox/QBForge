namespace QBForge.Interfaces
{
	public interface IOrderBy : IRenderContext { }

	public delegate IOrderBy OrderByClauseDe(IOrderBy render, DataEntry arg);
}