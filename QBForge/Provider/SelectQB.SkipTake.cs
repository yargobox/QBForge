using QBForge.Provider.Clauses;
using System;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Skip(long offset)
			=> AddOffsetClause(offset, false);
		public virtual ISelectQB<T> Offset(long offset)
			=> AddOffsetClause(offset, true);

		public virtual ISelectQB<T> Take(long limit, FetchNext fetchNext = FetchNext.RowsOnly)
			=> AddLimitClause(limit, fetchNext, false);
		public virtual ISelectQB<T> Limit(long limit, FetchNext fetchNext = FetchNext.RowsOnly)
			=> AddLimitClause(limit, fetchNext, true);

		private ISelectQB<T> AddOffsetClause(long offset, bool parametrized)
		{
			if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

			var offsetSection = FindSectionClause(ClauseSections.Offset);
			if (offsetSection != null)
			{
				throw MakeExceptionSectionHasAlreadyBeenAdded(ClauseSections.Offset);
			}

			_context.Clause.Add(new OffsetSectionClause(offset, parametrized));

			return this;
		}

		private ISelectQB<T> AddLimitClause(long limit, FetchNext fetchNext, bool parametrized)
		{
			if (limit < 0) throw new ArgumentOutOfRangeException(nameof(limit));

			var fetchSection = FindSectionClause(ClauseSections.Fetch);
			if (fetchSection != null)
			{
				throw MakeExceptionSectionHasAlreadyBeenAdded(ClauseSections.Fetch);
			}

			_context.Clause.Add(new FetchSectionClause(limit, fetchNext, parametrized));

			return this;
		}

		private static InvalidOperationException MakeExceptionSectionHasAlreadyBeenAdded(string sectionName)
		{
			return new InvalidOperationException($"\"{sectionName}\" has already been added");
		}
	}
}
