namespace QBForge.Provider
{
	public interface IBuildQueryContext
	{
		IRenderContext RenderContext { get; }

		int ParameterCount { get; }
		object? Parameters { get; }
	}
}