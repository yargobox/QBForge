namespace QBForge.Provider
{
	public interface IRenderContext
	{
		IQBProvider Provider { get; }
		ReadabilityLevels Readability { get; }
		int TabSize { get; }
		int CurrentIndent { get; set; }

		string MakeParamPlaceholder(object? parameter);

		IRenderContext Append(string text);
		IRenderContext Append(char ch);
		IRenderContext Append(char ch, int repeatCount);
		IRenderContext AppendLine();
		IRenderContext Append(DataEntry de);
		IRenderContext AppendObject(string objectName);
		IRenderContext AppendLabel(string label);
		IRenderContext AppendAsLabel(string label);
	}
}