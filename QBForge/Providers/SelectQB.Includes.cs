using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using QBForge.Providers.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.IncludeAll(string? tableLabel)
		{
			var key = IncludeClause.MakeKey(tableLabel, "*");

			var dataEntry = new DataEntry(tableLabel, "*");

			Clause includeClause = new DataEntryClause(dataEntry);
			if (includeClause.Key != key)
			{
				includeClause = new IncludeClause(key, includeClause);
			}

			var includeSection = EnsureSectionClause<IncludeSectionClause>(ClauseSections.Include);

			if (string.IsNullOrEmpty(tableLabel))
			{
				includeSection.Clauses!.Clear();
			}
			else
			{
				for (int i = 0; i < includeSection.Count; i++)
				{
					if (includeSection[i].Key == key)
					{
						includeSection.Clauses![i] = includeClause;
						return this;
					}
				}
			}

			includeSection.Add(includeClause);
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include(Expression<Func<T, object?>> de, string? asLabel)
		{
			var (paramName, memberName) = de.GetMemberName(true);

			if (memberName.Length == 0)
			{
				var mappingInfo = _context.Provider.GetMappingInfo<T>();
				foreach (var mi in mappingInfo.Values.Where(x => x.MapAs == MapMemberAs.Element))
				{
					AddIncludeClause(paramName, mi.Name, mi.MappedName);
				}
			}
			else
			{
				var mappedName = _context.Provider.GetMappedName<T>(memberName);
				AddIncludeClause(paramName, memberName, mappedName, asLabel);
			}

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(Expression<Func<TJoined, object?>> de, string? asLabel)
		{
			var (paramName, memberName) = de.GetMemberName(true);

			if (memberName.Length == 0)
			{
				var mappingInfo = _context.Provider.GetMappingInfo<TJoined>();
				foreach (var mi in mappingInfo.Values.Where(x => x.MapAs == MapMemberAs.Element))
				{
					AddIncludeClause(paramName, mi.Name, mi.MappedName);
				}
			}
			else
			{
				var mappedName = _context.Provider.GetMappedName<TJoined>(memberName);
				AddIncludeClause(paramName, memberName, mappedName, asLabel);
			}

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include(UnaryAggrHandler aggregate, Expression<Func<T, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(UnaryAggrHandler aggregate, Expression<Func<TJoined, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDe func, Expression<Func<T, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDe func, Expression<Func<TJoined, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDeV func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDeV func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? asLabel) => this;


		private void AddIncludeClause(string? tableLabel, string memberName, string mappedName, string? asLabel = null)
		{
			if (string.IsNullOrEmpty(tableLabel) || tableLabel == "_")
			{
				tableLabel = null;
			}
			if (string.IsNullOrEmpty(asLabel))
			{
				asLabel = memberName;
			}

			var key = IncludeClause.MakeKey(_context.MapNextTo, asLabel!);

			var dataEntry = new DataEntry(tableLabel, mappedName);

			Clause includeClause = new DataEntryClause(dataEntry);
			if (includeClause.Key != key || dataEntry.Name != asLabel)
			{
				includeClause = new IncludeClause(key, includeClause, asLabel);
			}

			var includeSection = EnsureSectionClause<IncludeSectionClause>(ClauseSections.Include);

			for (int i = 0; i < includeSection.Count; i++)
			{
				if (includeSection[i].Key == key)
				{
					includeSection.Clauses![i] = includeClause;
					return;
				}
			}

			includeSection.Add(includeClause);
		}
	}
}
