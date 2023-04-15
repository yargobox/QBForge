using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Schema;

namespace QBForge.Providers
{
	internal sealed partial class SelectQB<T> : ISelectQB<T>
	{
		private readonly IQBContext _context;
		private readonly bool _foreignContext;

		IQBContext IQueryBuilder.Context => _context;

		public SelectQB(IQBContext context, bool foreignContext)
		{
			_context = context;
			_foreignContext = foreignContext;
		}

		public override string ToString() => ToString(out var _, ReadabilityLevels.Default);
		public string ToString(bool pretty) => ToString(out var _, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(out object? parameters, bool pretty) => ToString(out parameters, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(ReadabilityLevels level) => ToString(out var _, level);
		public string ToString(out object? parameters, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			var buildContent = _context.Provider.Build(this, _context.Provider.CreateBuildQueryContext(level));
			var query = buildContent.ToString();
			parameters = buildContent.Parameters;
			return query!;
		}

		ISelectQB<T> ISelectQB<T>.From(string tableName, string? labelAs, dynamic? parameters)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

			var (schemaName, objectName) = _context.Provider.ParseSchemaObject(tableName);

			var fromSection = EnsureSectionClause<FromSectionClause>(ClauseSections.From);

			fromSection.Add(new FromClause(new TableClause(schemaName, objectName), labelAs));//!!! parameters

			return this;
		}

		ISelectQB<T> ISelectQB<T>.Distinct() => this;

		ISelectQB<T> ISelectQB<T>.Having(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.Having<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs) => this;

		ISelectQB<T> ISelectQB<T>.Skip(long skip, string? @label = null) => this;
		ISelectQB<T> ISelectQB<T>.Take(int take, string? @label = null) => this;


		private TSectionClause EnsureSectionClause<TSectionClause>(string section) where TSectionClause : Clause, new()
		{
			var sectionClause = _context.Clause.FirstOrDefault(x => x.Key == section);
			if (sectionClause == null)
			{
				_context.Clause.Add(sectionClause = new TSectionClause());
			}

			return (TSectionClause)sectionClause;
		}

		private Clause? GetSectionClause(string section)
		{
			return _context.Clause.FirstOrDefault(x => x.Key == section);
		}

		private TSectionClause? GetSectionClause<TSectionClause>(string section) where TSectionClause : Clause
		{
			return (TSectionClause?)_context.Clause.FirstOrDefault(x => x.Key == section);
		}

		private OnClause? GetJoinOnClause(string joinedTableLabel, Clause? joinSection = null)
		{
			joinSection ??= GetSectionClause(ClauseSections.Join);

			var joinClause = joinSection?.FirstOrDefault(x => x.Key == joinedTableLabel);

			return (OnClause?)(joinClause as BinaryClause)?.Right;
		}

		private bool TableLabelExists(string labelAs, Clause? fromSection = null, Clause? joinSection = null, Clause? withCteSection = null)
		{
			var concreteFromSection = (fromSection ?? GetSectionClause(ClauseSections.From)) as FromSectionClause;
			joinSection ??= GetSectionClause(ClauseSections.Join);
			withCteSection ??= GetSectionClause(ClauseSections.WithCte);

			return concreteFromSection?.Any(x => x.Key == labelAs) == true
				|| joinSection?.Any(x => x.Key == labelAs) == true
				|| withCteSection?.Any(x => x.Key == labelAs) == true;
		}
	}
}