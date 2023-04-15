using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Data.Common;

namespace QBForge.Providers
{
	internal partial class ProviderBase
	{
		private class QBContext : IQBContext
		{
			public IQBProvider Provider { get; }

			public Clause Clause { get; }

			public int LastBuild { get; set; }
			public string LastQuery { get; set; }
			public DbParameterCollection LastParameters => throw new NotImplementedException();

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

			public object Clone()
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
}
