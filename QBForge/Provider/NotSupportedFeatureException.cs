using System;

namespace QBForge.Provider
{
	/// <summary>
	/// Requested feature is not supported by provider
	/// </summary>
	public class NotSupportedFeatureException : NotSupportedException
	{
		public NotSupportedFeatureException() { }
		public NotSupportedFeatureException(string message) : base(message) { }
		public NotSupportedFeatureException(string message, Exception innerException) : base(message, innerException) { }
	}
}