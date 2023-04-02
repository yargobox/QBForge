using QBForge.Extensions.Text;
using QBForge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QBForge.Providers
{
	internal partial class ProviderBase : IQBProvider
	{
		protected sealed class SectionInfo
		{
			public string? Prefix { get; }
			public string Section { get; }
			public string? Joiner { get; }
			public string? Suffix { get; }

			public SectionInfo(string? prefix, string section, string? joiner, string? suffix)
			{
				Prefix = prefix;
				Section = section;
				Joiner = joiner;
				Suffix = suffix;
			}
		}

		private readonly IReadOnlyList<SectionInfo> _selectTemplate = new List<SectionInfo>()
		{
			new SectionInfo(null, ClauseSections.WithCte, Environment.NewLine, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Select, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Distinct, null, null),
			new SectionInfo(null, ClauseSections.Limit, null, null),
			new SectionInfo(null, ClauseSections.WithTies, null, null),
			new SectionInfo(null, ClauseSections.Include, "," + Environment.NewLine, Environment.NewLine),
			new SectionInfo(null, ClauseSections.From, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Join, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Where, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.GroupBy, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Having, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.OrderBy, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Offset, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Options, null, Environment.NewLine),
			new SectionInfo(null, ClauseSections.Union, null, Environment.NewLine)
		};

		protected virtual IReadOnlyList<SectionInfo> GetSelectBuildTempalte()
		{
			return _selectTemplate;
		}

		public virtual IBuildQueryContext CreateBuildQueryContext(ReadabilityLevels level = ReadabilityLevels.Default)
		{
			return new BuildQueryContext(this, level);
		}

		public virtual IBuildQueryContext Build<T>(ISelectQB<T> queryBuilder, IBuildQueryContext? context = null)
		{
			context ??= CreateBuildQueryContext();

			var clauses = ((QBContext)queryBuilder.Context).ClauseEntries;
			var buildTemplate = GetSelectBuildTempalte();
			bool next, prefix;

			foreach (var se in buildTemplate)
			{
				prefix = false;
				next = false;
				foreach (var clause in clauses.Where(x => x.Section == se.Section))
				{
					if (!prefix)
					{
						prefix = true;
						if (!string.IsNullOrEmpty(se.Prefix) && context.Output.EndsWith(se.Prefix!, StringComparison.Ordinal))
						{
							context.Output.Append(se.Prefix);
						}
					}

					if (next)
					{
						if (!string.IsNullOrEmpty(se.Joiner)) context.Output.Append(se.Joiner);
					}
					else
					{
						next = true;
					}

					clause.Render(context);
				}

				if (next && !string.IsNullOrEmpty(se.Suffix))
				{
					context.Output.Append(se.Suffix);
				}
			}

			context.Output.TrimEnd();

			return context;
		}
	}
}
