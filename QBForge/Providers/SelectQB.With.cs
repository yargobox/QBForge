using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.With<TCte>(string labelCte, ISelectQB<TCte> subQuery)
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
