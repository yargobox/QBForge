using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Providers.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QBForge.Providers
{
	internal sealed partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.IncludeAll(string? label)
		{
			if (string.IsNullOrEmpty(label))
				_context.SetClause(new TextClause(ClauseSections.Include, "*", "*"));
			else
				_context.SetClause(new TextClause(ClauseSections.Include, _context.MapNextTo + ".*", _context.Provider.AppendLabel(label!) + ".*"));

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include(Expression<Func<T, object?>> de, string? label)
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
				SetIncludeClause(paramName, memberName, mappedName);
			}

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(Expression<Func<TJoined, object?>> de, string? label)
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
				SetIncludeClause(paramName, memberName, mappedName);
			}

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Include(AggrCallClauseDe aggregate, Expression<Func<T, object?>> de, string? label) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(AggrCallClauseDe aggregate, Expression<Func<TJoined, object?>> de, string? label) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDe func, Expression<Func<T, object?>> de, string? label) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDe func, Expression<Func<TJoined, object?>> de, string? label) => this;

		ISelectQB<T> ISelectQB<T>.Include(FuncCallClauseDeV func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? label) => this;

		ISelectQB<T> ISelectQB<T>.Include<TJoined>(FuncCallClauseDeV func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? label) => this;
		

		private void SetIncludeClause(string paramName, string memberName, string mappedName)
		{
			var sb = new StringBuilder();

			if (paramName != "_")
			{
				_context.Provider.AppendLabel(sb, paramName);
				sb.Append('.');
			}

			_context.Provider.AppendIdentifier(sb, mappedName);

			if (memberName != mappedName)
			{
				sb.Append(' ');
				_context.Provider.AppendAsLabel(sb, memberName);
			}

			var key = string.Concat(_context.MapNextTo, ".", memberName);
			var arg = new DataEntry(paramName != "_" ? paramName : null, memberName);

			_context.SetClause(new TextClause(ClauseSections.Include, key, sb.ToString(), arg));
		}
	}
}
