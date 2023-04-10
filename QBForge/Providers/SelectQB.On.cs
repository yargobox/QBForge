using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.On(Action<ISelectQB<T>> parenthesize, string joinedTableLabel)
			=> AddOnCond(joinedTableLabel, false, parenthesize);

		ISelectQB<T> ISelectQB<T>.OrOn(Action<ISelectQB<T>> parenthesize, string joinedTableLabel)
			=> AddOnCond(joinedTableLabel, true, parenthesize);

		ISelectQB<T> ISelectQB<T>.On(Expression<Func<T, object?>> lhs, UnaryOperator op)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new UnaryHandlerClause(op, lde));
		}

		ISelectQB<T> ISelectQB<T>.OrOn(Expression<Func<T, object?>> lhs, UnaryOperator op)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new UnaryHandlerClause(op, lde));
		}

		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new UnaryHandlerClause(op, lde));
		}

		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new UnaryHandlerClause(op, lde));
		}

		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new BinaryHandlerClause(op, lde, rhs));
		}

		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new BinaryHandlerClause(op, lde, rhs));
		}

		ISelectQB<T> ISelectQB<T>.On(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new BinaryHandlerClause(op, lde, rhs));
		}

		ISelectQB<T> ISelectQB<T>.OrOn(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new BinaryHandlerClause(op, lde, rhs));
		}

		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new BinaryHandlerClause(op, lde, rhs.ToDataEntry()));
		}

		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new BinaryHandlerClause(op, lde, rhs.ToDataEntry()));
		}

		ISelectQB<T> ISelectQB<T>.On<TJoined1, TJoined2>(Expression<Func<TJoined2, object?>> lhs, BinaryOperator op, Expression<Func<TJoined1, object?>> rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, false, new BinaryHandlerClause(op, lde, rhs.ToDataEntry()));
		}

		ISelectQB<T> ISelectQB<T>.OrOn<TJoined1, TJoined2>(Expression<Func<TJoined2, object?>> lhs, BinaryOperator op, Expression<Func<TJoined1, object?>> rhs)
		{
			var lde = lhs.ToDataEntry();
			return AddOnCond(lde.RefLabel!, true, new BinaryHandlerClause(op, lde, rhs.ToDataEntry()));
		}

		private ISelectQB<T> AddOnCond(string joinedTableLabel, bool or, Clause right, OnClause? joinOnClause = null)
		{
			if (string.IsNullOrEmpty(joinedTableLabel)) throw new ArgumentNullException(nameof(joinedTableLabel));

			joinOnClause ??= GetJoinOnClause(joinedTableLabel) ?? throw MakeExceptionThereIsNoSuchJoinedTableLabeled(joinedTableLabel);

			if (joinOnClause.Count > 0)
			{
				var left = joinOnClause.Clauses![joinOnClause.Count - 1];

				if (object.ReferenceEquals(left, Clause.Empty))
				{
					joinOnClause.Clauses![joinOnClause.Count - 1] = right;
				}
				else
				{
					joinOnClause.Clauses![joinOnClause.Count - 1] = or
						? new OrAlsoClause(left, right)
						: new AndAlsoClause(left, right);
				}
			}
			else
			{
				joinOnClause.Add(right);
			}

			return this;
		}

		private ISelectQB<T> AddOnCond(string joinedTableLabel, bool or, Action<ISelectQB<T>> parenthesize)
		{
			if (string.IsNullOrEmpty(joinedTableLabel)) throw new ArgumentNullException(nameof(joinedTableLabel));

			var joinOnClause = GetJoinOnClause(joinedTableLabel) ?? throw MakeExceptionThereIsNoSuchJoinedTableLabeled(joinedTableLabel);

			// add Empty to the end
			joinOnClause.Add(Clause.Empty);

			// Empty will be replaced with the parenthesize condition (the Right one)
			parenthesize(this);

			// The last one must be our Right instead of Empty
			var right = joinOnClause.Clauses![joinOnClause.Count - 1];
			// remove it
			joinOnClause.Clauses.RemoveAt(joinOnClause.Count - 1);

			if (!object.ReferenceEquals(right, Clause.Empty))
			{
				AddOnCond(joinedTableLabel, or, right, joinOnClause);
			}

			return this;
		}

		private static InvalidOperationException MakeExceptionThereIsNoSuchJoinedTableLabeled(string joinedTableLabel)
		{
			return new InvalidOperationException($"There is no such joined table labeled \"{joinedTableLabel}\".");
		}
	}
}