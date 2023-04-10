using QBForge.Extensions.Linq.Expressions;
using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.OrderBy(Expression<Func<T, object?>> lhs, OrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new OrderByHandler(DefaultOrderBy), lhs.ToDataEntry()));

		ISelectQB<T> ISelectQB<T>.OrderBy<T2>(Expression<Func<T2, object?>> lhs, OrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new OrderByHandler(DefaultOrderBy), lhs.ToDataEntry()));

		private ISelectQB<T> AddOrderByCond(Clause orderByClause)
		{
			var orderBySection = EnsureSectionClause<OrderBySectionClause>(ClauseSections.OrderBy);

			orderBySection.Add(orderByClause);

			return this;
		}

		private static IOrderByRender DefaultOrderBy(IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg); return render;
		}
	}
}