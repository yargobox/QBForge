using QBForge.Extensions.Linq.Expressions;
using QBForge.Provider.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Provider
{
	partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Having(Expression<Func<T, object?>> lhs, UnaryOperator op)
			=> AddHavingCond(false, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrHaving(Expression<Func<T, object?>> lhs, UnaryOperator op)
			=> AddHavingCond(true, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op)
			=> AddHavingCond(false, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op)
			=> AddHavingCond(true, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> Having(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> OrHaving(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> Having<T2, T3>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T3, object?>> rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> OrHaving<T2, T3>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T3, object?>> rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> HavingComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, new UnaryHandlerClause(ag, lhs.ToDataEntry()), new ParameterClause(rhs)));

		public virtual ISelectQB<T> OrHavingComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, new UnaryHandlerClause(ag, lhs.ToDataEntry()), new ParameterClause(rhs)));

		public virtual ISelectQB<T> HavingComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(false, new BinaryHandlerClause(op, new UnaryHandlerClause(ag, lhs.ToDataEntry()), new ParameterClause(rhs)));

		public virtual ISelectQB<T> OrHavingComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddHavingCond(true, new BinaryHandlerClause(op, new UnaryHandlerClause(ag, lhs.ToDataEntry()), new ParameterClause(rhs)));

		private ISelectQB<T> AddHavingCond(bool or, Clause right)
		{
			var havingSection = EnsureSectionClause<HavingSectionClause>(ClauseSections.Having);

			if (havingSection.Count > 0)
			{
				var left = havingSection.Clauses![havingSection.Count - 1];

				if (object.ReferenceEquals(left, Clause.Empty))
				{
					havingSection.Clauses![havingSection.Count - 1] = right;
				}
				else
				{
					havingSection.Clauses![havingSection.Count - 1] = or
						? (Clause)new OrAlsoClause(left, right)
						: (Clause)new AndAlsoClause(left, right);
				}
			}
			else
			{
				havingSection.Add(right);
			}

			return this;
		}
	}
}