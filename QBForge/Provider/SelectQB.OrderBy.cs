﻿using QBForge.Extensions;
using QBForge.Extensions.Linq.Expressions;
using QBForge.Extensions.Text;
using QBForge.Provider.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Provider
{
    partial class SelectQB<T>
	{
		public virtual ISelectQB<T> OrderBy(Expression<Func<T, object?>> lhs, UnaryOrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new UnaryOrderByHandler(DefaultOrderBy), lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrderBy<T2>(Expression<Func<T2, object?>> lhs, UnaryOrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new UnaryOrderByHandler(DefaultOrderBy), lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrderByComputed(AggrHandler ag, UnaryOrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new UnaryOrderByHandler(DefaultOrderBy),
				new HandlerClause(ag)));

		public virtual ISelectQB<T> OrderByComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, UnaryOrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new UnaryOrderByHandler(DefaultOrderBy),
				new UnaryHandlerClause(ag, lhs.ToDataEntry())));

		public virtual ISelectQB<T> OrderByComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, UnaryOrderByHandler? ob)
			=> AddOrderByCond(new UnaryHandlerClause(ob ?? new UnaryOrderByHandler(DefaultOrderBy),
				new UnaryHandlerClause(ag, lhs.ToDataEntry())));

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