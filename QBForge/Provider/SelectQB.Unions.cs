using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Union(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.UNION, new SubQueryClause(query)));

		public virtual ISelectQB<T> UnionAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.UNION_ALL, new SubQueryClause(query)));

		public virtual ISelectQB<T> Intersect(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.INTERSECT, new SubQueryClause(query)));

		public virtual ISelectQB<T> IntersectAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.INTERSECT_ALL, new SubQueryClause(query)));

		public virtual ISelectQB<T> Except(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.EXCEPT, new SubQueryClause(query)));

		public virtual ISelectQB<T> ExceptAll(ISelectQB<T> query)
			=> AddUnionClause(new UnionClause(UnionClause.EXCEPT_ALL, new SubQueryClause(query)));

		private ISelectQB<T> AddUnionClause(UnionClause clause)
		{
			var unionSection = EnsureSectionClause<UnionSectionClause>(ClauseSections.Union);

			unionSection.Add(clause);

			return this;
		}
	}
}