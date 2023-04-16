using QBForge.Provider.Clauses;
using System;
using System.Data.Common;

namespace QBForge.Provider
{
	public class QBContext : IQBContext
	{
		public IQBProvider Provider { get; }

		public Clause Clause { get; }

		public int LastBuild { get; set; }
		public string LastQuery { get; set; }
		public virtual DbParameterCollection LastParameters => throw new NotImplementedException();

		public string MapNextTo { get; set; }

		public object? Tag { get; set; }
		public Delegate? Map { get; set; }

		public QBContext(IQBProvider provider, Clause clause)
		{
			Provider = provider;
			Clause = clause;
			LastBuild = -1;
			LastQuery = string.Empty;
			MapNextTo = string.Empty;
		}

		public virtual object Clone()
		{
			return new QBContext(Provider, Clause.Clone())
			{
				LastBuild = LastBuild,
				LastQuery = LastQuery,
				//LastParameters = LastParameters,//!!!
				MapNextTo = MapNextTo,
				Tag = Tag,
				Map = Map
			};
		}
	}
}
