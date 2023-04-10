using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.Union(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.UNION, new SubQueryClause(query)));

		ISelectQB<T> ISelectQB<T>.UnionAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.UNION_ALL, new SubQueryClause(query)));

		ISelectQB<T> ISelectQB<T>.Intersect(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.INTERSECT, new SubQueryClause(query)));

		ISelectQB<T> ISelectQB<T>.IntersectAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.INTERSECT_ALL, new SubQueryClause(query)));

		ISelectQB<T> ISelectQB<T>.Except(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.EXCEPT, new SubQueryClause(query)));

		ISelectQB<T> ISelectQB<T>.ExceptAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.EXCEPT_ALL, new SubQueryClause(query)));

		private ISelectQB<T> AddUnionClause(UnionClause clause)
		{
			var unionSection = EnsureSectionClause<UnionSectionClause>(ClauseSections.Union);

			unionSection.Add(clause);

			return this;
		}
	}
}