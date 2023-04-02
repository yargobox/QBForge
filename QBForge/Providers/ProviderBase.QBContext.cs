using QBForge.Interfaces;
using QBForge.Providers.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace QBForge.Providers
{
	internal partial class ProviderBase
	{
		private class QBContext : IQBContext
		{
			public IQBProvider Provider { get; }

			public Clause Clause { get; }

			public List<ClauseEntry> ClauseEntries { get; }

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
				ClauseEntries = new List<ClauseEntry>();
				LastBuild = -1;
				LastQuery = string.Empty;
				MapNextTo = string.Empty;
			}

			public IQBContext Clone()
			{
				var copy = new QBContext(Provider, Clause)//!!! deep copy needed
				{
					Tag = Tag,
					Map = Map
				};

				// !!!

				return copy;
			}

			public void SetClause(ClauseEntry clause)
			{
				var key = clause.Key;
				if (key != null)
				{
					var index = ClauseEntries.FindLastIndex(x => x.Section == clause.Section && x.Key == key);
					if (index >= 0)
					{
						ClauseEntries[index] = clause;
						return;
					}
				}

				ClauseEntries.Add(clause);
			}
		}
	}
}
