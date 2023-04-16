using QBForge.Provider.Clauses;
using System;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Join<TJoined>(ISelectQB<TJoined> subQuery, string labelAs)
			=> AddJoinClause(subQuery, labelAs, (subQueryClause, labelAs) => new InnerJoinClause(subQueryClause, labelAs));
		public virtual ISelectQB<T> Join<TJoined>(string tableName, string labelAs)
			=> AddJoinClause(tableName, labelAs, (tableClause, labelAs) => new InnerJoinClause(tableClause, labelAs));

		public virtual ISelectQB<T> LeftJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs)
			=> AddJoinClause(subQuery, labelAs, (subQueryClause, labelAs) => new LeftJoinClause(subQueryClause, labelAs));
		public virtual ISelectQB<T> LeftJoin<TJoined>(string tableName, string labelAs)
			=> AddJoinClause(tableName, labelAs, (tableClause, labelAs) => new LeftJoinClause(tableClause, labelAs));

		public virtual ISelectQB<T> RightJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs)
			=> AddJoinClause(subQuery, labelAs, (subQueryClause, labelAs) => new RightJoinClause(subQueryClause, labelAs));
		public virtual ISelectQB<T> RightJoin<TJoined>(string tableName, string labelAs)
			=> AddJoinClause(tableName, labelAs, (tableClause, labelAs) => new RightJoinClause(tableClause, labelAs));

		public virtual ISelectQB<T> FullJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs)
			=> AddJoinClause(subQuery, labelAs, (subQueryClause, labelAs) => new FullJoinClause(subQueryClause, labelAs));
		public virtual ISelectQB<T> FullJoin<TJoined>(string tableName, string labelAs)
			=> AddJoinClause(tableName, labelAs, (tableClause, labelAs) => new FullJoinClause(tableClause, labelAs));

		public virtual ISelectQB<T> CrossJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs)
			=> AddJoinClause(subQuery, labelAs, (subQueryClause, labelAs) => new CrossJoinClause(subQueryClause, labelAs));
		public virtual ISelectQB<T> CrossJoin<TJoined>(string tableName, string labelAs)
			=> AddJoinClause(tableName, labelAs, (tableClause, labelAs) => new CrossJoinClause(tableClause, labelAs));

		private ISelectQB<T> AddJoinClause(string tableName, string labelAs, Func<TableClause, string, Clause> joinClauseFactory)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));
			if (string.IsNullOrEmpty(labelAs)) throw new ArgumentNullException(nameof(labelAs));

			var joinSection = EnsureSectionClause<JoinSectionClause>(ClauseSections.Join);

			if (TableLabelExists(labelAs, joinSection: joinSection))
			{
				throw MakeExceptionLablelHasAlreadyBeenAdded(labelAs);
			}

			var (schemaName, objectName) = _context.Provider.ParseSchemaObject(tableName);

			var joinClause = joinClauseFactory(new TableClause(schemaName, objectName), labelAs);

			joinSection.Add(joinClause);

			return this;
		}

		private ISelectQB<T> AddJoinClause<TJoined>(ISelectQB<TJoined> subQuery, string labelAs, Func<SubQueryClause, string, Clause> joinClauseFactory)
		{
			if (subQuery == null) throw new ArgumentNullException(nameof(subQuery));
			if (string.IsNullOrEmpty(labelAs)) throw new ArgumentNullException(nameof(labelAs));

			var joinSection = EnsureSectionClause<JoinSectionClause>(ClauseSections.Join);

			if (TableLabelExists(labelAs, joinSection: joinSection))
			{
				throw MakeExceptionLablelHasAlreadyBeenAdded(labelAs);
			}

			var joinClause = joinClauseFactory(new SubQueryClause(subQuery), labelAs);

			joinSection.Add(joinClause);

			return this;
		}

		private static ArgumentException MakeExceptionLablelHasAlreadyBeenAdded(string labelAs)
		{
			return new ArgumentException($"Lablel \"{labelAs}\" has already been added.", nameof(labelAs));
		}
	}
}
