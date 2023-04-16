using QBForge.Provider.Clauses;
using System;
using System.Data.Common;

namespace QBForge.Provider
{
	public interface IQBContext : ICloneable
	{
		public Clause Clause { get; }

		IQBProvider Provider { get; }

		int LastBuild { get; set; }
		string LastQuery { get; set; }
		DbParameterCollection LastParameters { get; }

		string MapNextTo { get; set; }
		Delegate? Map { get; set; }

		object? Tag { get; set; }
	}
}