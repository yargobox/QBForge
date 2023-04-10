using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.GroupBy(Expression<Func<T, object?>> lhs)
			=> AddGroupByCond(new DataEntryClause(lhs.ToDataEntry()));

		ISelectQB<T> ISelectQB<T>.GroupBy<T2>(Expression<Func<T2, object?>> lhs)
			=> AddGroupByCond(new DataEntryClause(lhs.ToDataEntry()));

		private ISelectQB<T> AddGroupByCond(Clause groupByClause)
		{
			var groupBySection = EnsureSectionClause<GroupBySectionClause>(ClauseSections.GroupBy);

			groupBySection.Add(groupByClause);

			return this;
		}
	}
}