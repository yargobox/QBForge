﻿using System;
using System.Text;

namespace QBForge.Provider
{
    partial class ProviderBase
	{
		public ReadabilityLevels DefaultReadabilityLevel { get; set; }

		public abstract string AppendObject(string objectName, ReadabilityLevels level = ReadabilityLevels.Default);
		public abstract void AppendObject(StringBuilder sb, string objectName, ReadabilityLevels level = ReadabilityLevels.Default);
		public abstract string AppendLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		public abstract void AppendLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);
		public abstract string AppendAsLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		public abstract void AppendAsLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);

		public virtual (string? schemaName, string objectName) ParseSchemaObject(string schemaObject)
		{
			var period = schemaObject.IndexOf('.');

			if (period < 0)
			{
				return (null, schemaObject);
			}

			if (period == 0 || period + 1 == schemaObject.Length)
			{
				throw new ArgumentException("Invalid [schema.]{object} format.", nameof(schemaObject));
			}

			return (schemaObject.Substring(0, period), schemaObject.Substring(period + 1));
		}
	}
}
