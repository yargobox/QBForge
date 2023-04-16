using QBForge.Provider.Clauses;
using System;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Offset(long skip)
			=> AddOffsetClause(skip);

		public virtual ISelectQB<T> Limit(long take)
			=> AddLimitClause(take);

		private ISelectQB<T> AddOffsetClause(long skip)
		{
			if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip));

			var skipSection = FindSectionClause(ClauseSections.Offset);
			if (skipSection != null)
			{
				throw MakeExceptionSectionHasAlreadyBeenAdded(ClauseSections.Offset);
			}

			_context.Clause.Add(new OffsetSectionClause(skip));

			return this;
		}

		private ISelectQB<T> AddLimitClause(long take)
		{
			if (take < 0) throw new ArgumentOutOfRangeException(nameof(take));

			var takeSection = FindSectionClause(ClauseSections.Limit);
			if (takeSection != null)
			{
				throw MakeExceptionSectionHasAlreadyBeenAdded(ClauseSections.Limit);
			}

			_context.Clause.Add(new LimitSectionClause(take));

			return this;
		}

		private static InvalidOperationException MakeExceptionSectionHasAlreadyBeenAdded(string sectionName)
		{
			return new InvalidOperationException($"\"{sectionName}\" has already been added");
		}
	}
}
