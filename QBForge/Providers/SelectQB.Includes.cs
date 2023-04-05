using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using QBForge.Providers.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;

namespace QBForge.Providers
{
    internal sealed partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.IncludeAll(string? tableLabel)
		{
			var key = IncludeClause.MakeKey(tableLabel, "*");

			var dataEntry = new DataEntry(tableLabel, "*");

			Clause includeClause = new DataEntryClouse(dataEntry);
			if (includeClause.Key != key)
			{
				includeClause = new IncludeClause(key, includeClause);
			}

			var includeSection = _context.Clause.FirstOrDefault(x => x.Key == ClauseSections.Include);
			if (includeSection == null)
			{
				_context.Clause.Add(new IncludeSectionClause() { includeClause });
			}
			else if (string.IsNullOrEmpty(tableLabel))
			{
				includeClause.Clauses!.Clear();
				includeSection.Add(includeClause);
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

				includeSection.Add(includeClause);
			}

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
					SetIncludeClause(paramName, mi.Name, mi.MappedName);
				}
			}
			else
			{
				var mappedName = _context.Provider.GetMappedName<T>(memberName);
				SetIncludeClause(paramName, memberName, mappedName, asLabel);
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
					SetIncludeClause(paramName, mi.Name, mi.MappedName);
				}
			}
			else
			{
				var mappedName = _context.Provider.GetMappedName<TJoined>(memberName);
				SetIncludeClause(paramName, memberName, mappedName, asLabel);
			}

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include(AggrCallClauseDe aggregate, Expression<Func<T, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(AggrCallClauseDe aggregate, Expression<Func<TJoined, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDe func, Expression<Func<T, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDe func, Expression<Func<TJoined, object?>> de, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDeV func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? asLabel) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDeV func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? asLabel) => this;
		

		private void SetIncludeClause(string? tableLabel, string memberName, string mappedName, string? asLabel = null)
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

			Clause includeClause = new DataEntryClouse(dataEntry);
			if (includeClause.Key != key || dataEntry.Name != asLabel)
			{
				includeClause = new IncludeClause(key, includeClause, asLabel);
			}

			var includeSection = _context.Clause.FirstOrDefault(x => x.Key == ClauseSections.Include);
			if (includeSection == null)
			{
				_context.Clause.Add(new IncludeSectionClause() { includeClause });
			}
			else
			{
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
}
