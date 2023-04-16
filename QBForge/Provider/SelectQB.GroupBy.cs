using QBForge.Extensions.Linq.Expressions;
using QBForge.Provider.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> GroupBy(Expression<Func<T, object?>> lhs)
			=> AddGroupByCond(new DataEntryClause(lhs.ToDataEntry()));

		public virtual ISelectQB<T> GroupBy<T2>(Expression<Func<T2, object?>> lhs)
			=> AddGroupByCond(new DataEntryClause(lhs.ToDataEntry()));

		private ISelectQB<T> AddGroupByCond(Clause groupByClause)
		{
			var groupBySection = EnsureSectionClause<GroupBySectionClause>(ClauseSections.GroupBy);

			groupBySection.Add(groupByClause);

			return this;
		}
	}
}