using System;
using System.Linq.Expressions;

namespace QBForge.Interfaces
{
	public interface ISelectQB<T> : IQueryBuilder<T>
	{
#pragma warning disable CA1716
		ISelectQB<T> IncludeAll(string? tableLabel = null);
		ISelectQB<T> Include(Expression<Func<T, object?>> de, string? labelAs = null);
		ISelectQB<T> Include<TJoined>(Expression<Func<TJoined, object?>> de, string? labelAs = null);
		ISelectQB<T> Include(AggrCallClauseDe aggregate, Expression<Func<T, object?>> de, string? labelAs = null);
		ISelectQB<T> Include<TJoined>(AggrCallClauseDe aggregate, Expression<Func<TJoined, object?>> de, string? labelAs = null);
		ISelectQB<T> Include(FuncCallClauseDe func, Expression<Func<T, object?>> deArg, string? labelAs = null);
		ISelectQB<T> Include<TJoined>(FuncCallClauseDe func, Expression<Func<TJoined, object?>> deArg, string? labelAs = null);
		ISelectQB<T> Include(FuncCallClauseDeV func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? labelAs = null);
		ISelectQB<T> Include<TJoined>(FuncCallClauseDeV func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? labelAs = null);

		ISelectQB<T> Where(Action<ISelectQB<T>> parenthesized);
		ISelectQB<T> OrWhere(Action<ISelectQB<T>> parenthesized);
		ISelectQB<T> Where(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrWhere(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op);
		ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> Where(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrWhere(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> Where<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> OrWhere<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> Where<T2, T3>(Expression<Func<T3, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs);
		ISelectQB<T> OrWhere<T2, T3>(Expression<Func<T3, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs);
		ISelectQB<T> Where<T2>(UnaryOperator op, ISelectQB<T2> subQuery);
		ISelectQB<T> OrWhere<T2>(UnaryOperator op, ISelectQB<T2> subQuery);

		ISelectQB<T> Join<TJoined>(Action<ISelectQB<TJoined>> subQuery, string labelAs);
		ISelectQB<T> Join<TJoined>(string tableName, string? labelAs = null);
		ISelectQB<T> LeftJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string labelAs);
		ISelectQB<T> LeftJoin<TJoined>(string tableName, string? labelAs = null);
		ISelectQB<T> RightJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string labelAs);
		ISelectQB<T> RightJoin<TJoined>(string tableName, string? labelAs = null);
		ISelectQB<T> FullJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string labelAs);
		ISelectQB<T> FullJoin<TJoined>(string tableName, string? labelAs = null);
		ISelectQB<T> CrossJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string labelAs);
		ISelectQB<T> CrossJoin<TJoined>(string tableName, string? labelAs = null);

		ISelectQB<T> Distinct();

		ISelectQB<T> Union(ISelectQB<T> query);
		ISelectQB<T> UnionAll(ISelectQB<T> query);
		ISelectQB<T> Intersect(ISelectQB<T> query);
		ISelectQB<T> IntersectAll(ISelectQB<T> query);
		ISelectQB<T> Except(ISelectQB<T> query);
		ISelectQB<T> ExceptAll(ISelectQB<T> query);

		ISelectQB<T> On(Action<ISelectQB<T>> parenthesized);
		ISelectQB<T> On<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op);
		ISelectQB<T> On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> On<T2, TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs);
		ISelectQB<T> OrOn(Action<ISelectQB<T>> parenthesized);
		ISelectQB<T> OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> OrOn<T2, TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs);

		ISelectQB<T> OrderBy(Expression<Func<T, object?>> lhs, OrderByClauseDe? ob = null);
		ISelectQB<T> OrderBy<T2>(Expression<Func<T2, object?>> lhs, OrderByClauseDe? ob = null);

		ISelectQB<T> GroupBy(Expression<Func<T, object?>> lhs);
		ISelectQB<T> GroupBy<T2>(Expression<Func<T2, object?>> lhs);

		ISelectQB<T> Having(AggrCallClauseDe ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> Having<T2>(AggrCallClauseDe ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);

		ISelectQB<T> Skip(long offset, string? @label = null);
		ISelectQB<T> Take(int limit, string? @label = null);

		ISelectQB<T> Map(Func<T, T> map);
		ISelectQB<T> Map<TSecond>(Func<T, TSecond, T> map);
		ISelectQB<T> Map<TSecond, TThird>(Func<T, TSecond, TThird, T> map);
		ISelectQB<T> Map<TSecond, TThird, TFourth>(Func<T, TSecond, TThird, TFourth, T> map);
		ISelectQB<T> Map<TSecond, TThird, TFourth, TFifth>(Func<T, TSecond, TThird, TFourth, TFifth, T> map);
		ISelectQB<T> Map<TSecond, TThird, TFourth, TFifth, TSixth>(Func<T, TSecond, TThird, TFourth, TFifth, TSixth, T> map);
		ISelectQB<T> Map<TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(Func<T, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T> map);
		ISelectQB<T> Map(Func<object[], T> map);

		ISelectQB<T> MapNextTo<TMappedObjectType>(Expression<Func<T, TMappedObjectType>> mappedObject);
		ISelectQB<T> MapTo<TMappedObjectType>(Expression<Func<T, TMappedObjectType>> mappedObject, Action<ISelectQB<T>> includes);

#pragma warning restore CA1716
	}
}
