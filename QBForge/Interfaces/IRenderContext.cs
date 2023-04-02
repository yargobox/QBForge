namespace QBForge.Interfaces
{
	public interface IRenderContext
	{
		IQBProvider Provider { get; }
		ReadabilityLevels ReadabilityLevel { get; }

		IRenderContext Append(string text);
		IRenderContext Append(char ch);
		IRenderContext AppendLine();
		IRenderContext Append(DataEntry de);
		IRenderContext AppendIdentifier(string identifier);
		IRenderContext AppendLabel(string label);
		IRenderContext AppendAsLabel(string label);
	}
}