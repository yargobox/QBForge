using QBForge.Provider.Clauses;
using System;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> With<TCte>(string labelCte, ISelectQB<TCte> subQuery)
			=> AddWithClause(subQuery, labelCte);

		private ISelectQB<T> AddWithClause<TCte>(ISelectQB<TCte> subQuery, string labelCte)
		{
			if (subQuery == null) throw new ArgumentNullException(nameof(subQuery));
			if (string.IsNullOrEmpty(labelCte)) throw new ArgumentNullException(nameof(labelCte));

			var withCteSection = EnsureSectionClause<WithCteSectionClause>(ClauseSections.WithCte);

			if (TableLabelExists(labelCte, withCteSection: withCteSection))
			{
				throw MakeExceptionLablelHasAlreadyBeenAdded(labelCte);
			}

			var withCteClause = new WithCteClause(labelCte, new SubQueryClause(subQuery));

			withCteSection.Add(withCteClause);

			return this;
		}
	}
}
