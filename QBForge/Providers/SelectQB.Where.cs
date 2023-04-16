using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	partial class SelectQB<T>
	{
		public virtual ISelectQB<T> Where(Action<ISelectQB<T>> parenthesize)
			=> AddWhereCond(false, parenthesize);

		public virtual ISelectQB<T> OrWhere(Action<ISelectQB<T>> parenthesize)
			=> AddWhereCond(true, parenthesize);

		public virtual ISelectQB<T> Where(Expression<Func<T, object?>> lhs, UnaryOperator op)
			=> AddWhereCond(false, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrWhere(Expression<Func<T, object?>> lhs, UnaryOperator op)
			=> AddWhereCond(true, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op)
			=> AddWhereCond(false, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op)
			=> AddWhereCond(true, new UnaryHandlerClause(op, lhs.ToDataEntry()));

		public virtual ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddWhereCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddWhereCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> Where(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddWhereCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> OrWhere(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
			=> AddWhereCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs));

		public virtual ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
			=> AddWhereCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
			=> AddWhereCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> Where<T2, T3>(Expression<Func<T3, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs)
			=> AddWhereCond(false, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> OrWhere<T2, T3>(Expression<Func<T3, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs)
			=> AddWhereCond(true, new BinaryHandlerClause(op, lhs.ToDataEntry(), rhs.ToDataEntry()));

		public virtual ISelectQB<T> Where<T2>(UnaryOperator op, ISelectQB<T2> subQuery)
			=> AddWhereCond(false, new UnaryHandlerClause(op, new SubQueryClause(subQuery)));

		public virtual ISelectQB<T> OrWhere<T2>(UnaryOperator op, ISelectQB<T2> subQuery)
			=> AddWhereCond(true, new UnaryHandlerClause(op, new SubQueryClause(subQuery)));

		private ISelectQB<T> AddWhereCond(bool or, Clause right, Clause? whereSection = null)
		{
			whereSection ??= EnsureSectionClause<WhereSectionClause>(ClauseSections.Where);

			if (whereSection.Count > 0)
			{
				var left = whereSection.Clauses![whereSection.Count - 1];

				if (object.ReferenceEquals(left, Clause.Empty))
				{
					whereSection.Clauses![whereSection.Count - 1] = right;
				}
				else
				{
					whereSection.Clauses![whereSection.Count - 1] = or
						? new OrAlsoClause(left, right)
						: new AndAlsoClause(left, right);
				}
			}
			else
			{
				whereSection.Add(right);
			}

			return this;
		}

		private ISelectQB<T> AddWhereCond(bool or, Action<ISelectQB<T>> parenthesize)
		{
			var whereSection = EnsureSectionClause<WhereSectionClause>(ClauseSections.Where);

			// add Empty to the end
			whereSection.Add(Clause.Empty);

			// Empty will be replaced with the parenthesize condition (the Right one)
			parenthesize(this);

			// The last one must be our Right instead of Empty
			var right = whereSection.Clauses![whereSection.Count - 1];
			// remove it
			whereSection.Clauses.RemoveAt(whereSection.Count - 1);

			if (!object.ReferenceEquals(right, Clause.Empty))
			{
				AddWhereCond(or, right, whereSection);
			}

			return this;
		}
	}
}