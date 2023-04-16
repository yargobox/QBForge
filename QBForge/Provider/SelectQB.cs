using QBForge.Provider.Clauses;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QBForge.Provider
{
    public partial class SelectQB<T> : ISelectQB<T>
	{
		private readonly IQBContext _context;

		IQBContext IQueryBuilder.Context => _context;

		public SelectQB(IQBContext context) => _context = context;

		public object Clone() => new SelectQB<T>((IQBContext)_context.Clone());

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

		public virtual ISelectQB<T> From(string tableName, string? labelAs, dynamic? parameters = null)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

			var (schemaName, objectName) = _context.Provider.ParseSchemaObject(tableName);

			var fromSection = EnsureSectionClause<FromSectionClause>(ClauseSections.From);

			fromSection.Add(new FromClause(new TableClause(schemaName, objectName), labelAs));//!!! parameters

			return this;
		}

		public virtual ISelectQB<T> Distinct() => this;

		public virtual ISelectQB<T> Having(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs) => this;
		public virtual ISelectQB<T> Having<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs) => this;

		protected TSectionClause EnsureSectionClause<TSectionClause>(string section) where TSectionClause : Clause, new()
		{
			var sectionClause = _context.Clause.FirstOrDefault(x => x.Key == section);
			if (sectionClause == null)
			{
				_context.Clause.Add(sectionClause = new TSectionClause());
			}

			return (TSectionClause)sectionClause;
		}

		protected Clause? FindSectionClause(string section)
		{
			return _context.Clause.FirstOrDefault(x => x.Key == section);
		}

		private OnClause? GetJoinOnClause(string joinedTableLabel, Clause? joinSection = null)
		{
			joinSection ??= FindSectionClause(ClauseSections.Join);

			var joinClause = joinSection?.FirstOrDefault(x => x.Key == joinedTableLabel);

			return (OnClause?)(joinClause as BinaryClause)?.Right;
		}

		private bool TableLabelExists(string labelAs, Clause? fromSection = null, Clause? joinSection = null, Clause? withCteSection = null)
		{
			var concreteFromSection = (fromSection ?? FindSectionClause(ClauseSections.From)) as FromSectionClause;
			joinSection ??= FindSectionClause(ClauseSections.Join);
			withCteSection ??= FindSectionClause(ClauseSections.WithCte);

			return concreteFromSection?.Any(x => x.Key == labelAs) == true
				|| joinSection?.Any(x => x.Key == labelAs) == true
				|| withCteSection?.Any(x => x.Key == labelAs) == true;
		}
	}
}