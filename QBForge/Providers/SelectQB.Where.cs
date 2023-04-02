using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.Where(Action<ISelectQB<T>> parenthesized) => this;
		ISelectQB<T> ISelectQB<T>.OrWhere(Action<ISelectQB<T>> parenthesized) => this;

		ISelectQB<T> ISelectQB<T>.Where(Expression<Func<T, object?>> lhs, CondOperatorDe op)
		{
			_context.SetClause(new ClauseDe(ClauseSections.Where, null, op, lhs.ToDataEntry()));
			return this;
		}

		ISelectQB<T> ISelectQB<T>.OrWhere(Expression<Func<T, object?>> lhs, CondOperatorDe op) => this;
		ISelectQB<T> ISelectQB<T>.Where<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDe op)
		{
			_context.SetClause(new ClauseDe(ClauseSections.Where, null, op, lhs.ToDataEntry()));
			return this;
		}
		ISelectQB<T> ISelectQB<T>.OrWhere<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDe op) => this;
		ISelectQB<T> ISelectQB<T>.Where<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDeV op, dynamic rhs)
		{
			_context.SetClause(new ClauseDeV(ClauseSections.Where, null, op, lhs.ToDataEntry(), rhs));
			return this;
		}
		ISelectQB<T> ISelectQB<T>.OrWhere<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDeV op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.Where(Expression<Func<T, object?>> lhs, CondOperatorDeV op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrWhere(Expression<Func<T, object?>> lhs, CondOperatorDeV op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.Where<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDeDe op, Expression<Func<T, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrWhere<T2>(Expression<Func<T2, object?>> lhs, CondOperatorDeDe op, Expression<Func<T, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.Where<T2, T3>(Expression<Func<T3, object?>> lhs, CondOperatorDeDe op, Expression<Func<T2, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrWhere<T2, T3>(Expression<Func<T3, object?>> lhs, CondOperatorDeDe op, Expression<Func<T2, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.WhereExists<T2>(ISelectQB<T2> subQuery) => this;
		ISelectQB<T> ISelectQB<T>.WhereIn<T2>(ISelectQB<T2> subQuery) => this;
	}
}
