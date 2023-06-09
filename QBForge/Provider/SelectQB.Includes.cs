﻿using QBForge.Extensions.Linq.Expressions;
using QBForge.Provider.Clauses;
using QBForge.Provider.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> IncludeAll(string? tableLabel)
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

		public virtual ISelectQB<T> Include(Expression<Func<T, object?>> de, string? labelAs)
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
				AddIncludeClause(paramName, memberName, mappedName, labelAs);
			}

			return this;
		}

		public virtual ISelectQB<T> Include<TJoined>(Expression<Func<TJoined, object?>> de, string? labelAs)
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
				AddIncludeClause(paramName, memberName, mappedName, labelAs);
			}

			return this;
		}

		public virtual ISelectQB<T> IncludeComputed(UnaryAggrHandler aggregate, Expression<Func<T, object?>> de, string? labelAs) => this;

		public virtual ISelectQB<T> IncludeComputed<TJoined>(UnaryAggrHandler aggregate, Expression<Func<TJoined, object?>> de, string? labelAs) => this;

		public virtual ISelectQB<T> IncludeComputed(UnaryFuncHandler func, Expression<Func<T, object?>> de, string? labelAs) => this;

		public virtual ISelectQB<T> IncludeComputed<TJoined>(UnaryFuncHandler func, Expression<Func<TJoined, object?>> de, string? labelAs) => this;

		public virtual ISelectQB<T> IncludeComputed(BinaryFuncHandler func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? labelAs) => this;

		public virtual ISelectQB<T> IncludeComputed<TJoined>(BinaryFuncHandler func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? labelAs) => this;


		private void AddIncludeClause(string? tableLabel, string memberName, string mappedName, string? labelAs = null)
		{
			if (string.IsNullOrEmpty(tableLabel) || tableLabel == "_")
			{
				tableLabel = null;
			}
			if (string.IsNullOrEmpty(labelAs))
			{
				labelAs = memberName;
			}

			var key = IncludeClause.MakeKey(_context.MapNextTo, labelAs!);

			var dataEntry = new DataEntry(tableLabel, mappedName);

			Clause includeClause = new DataEntryClause(dataEntry);
			if (includeClause.Key != key || dataEntry.Name != labelAs)
			{
				includeClause = new IncludeClause(key, includeClause, labelAs);
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
