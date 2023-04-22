using System;
using System.Linq.Expressions;

namespace QBForge.Provider
{
	public enum FetchNext
	{
		RowsOnly = 0,
		RowsWithTies = 1,
		Percent = 2
	}

	public interface ISelectQB<T> : IQueryBuilder<T>
	{
#pragma warning disable CA1716

		ISelectQB<T> From(string tableName, string? labelAs = null, dynamic? parameters = null);

		ISelectQB<T> Distinct();

		ISelectQB<T> With<TCte>(string labelCte, ISelectQB<TCte> subQuery);

		ISelectQB<T> IncludeAll(string? tableLabel = null);
		ISelectQB<T> Include(Expression<Func<T, object?>> de, string? labelAs = null);
		ISelectQB<T> Include<TJoined>(Expression<Func<TJoined, object?>> de, string? labelAs = null);
		ISelectQB<T> IncludeComputed(UnaryAggrHandler aggregate, Expression<Func<T, object?>> de, string? labelAs = null);
		ISelectQB<T> IncludeComputed<TJoined>(UnaryAggrHandler aggregate, Expression<Func<TJoined, object?>> de, string? labelAs = null);
		ISelectQB<T> IncludeComputed(UnaryFuncHandler func, Expression<Func<T, object?>> deArg, string? labelAs = null);
		ISelectQB<T> IncludeComputed<TJoined>(UnaryFuncHandler func, Expression<Func<TJoined, object?>> deArg, string? labelAs = null);
		ISelectQB<T> IncludeComputed(BinaryFuncHandler func, Expression<Func<T, object?>> deArg1, dynamic? arg2, string? labelAs = null);
		ISelectQB<T> IncludeComputed<TJoined>(BinaryFuncHandler func, Expression<Func<TJoined, object?>> deArg1, dynamic? arg2, string? labelAs = null);

		ISelectQB<T> Where(Action<ISelectQB<T>> parenthesize);
		ISelectQB<T> OrWhere(Action<ISelectQB<T>> parenthesize);
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
		ISelectQB<T> WhereComputed<T2>(UnaryOperator op, ISelectQB<T2> subQuery);
		ISelectQB<T> OrWhereComputed<T2>(UnaryOperator op, ISelectQB<T2> subQuery);

		ISelectQB<T> Join<TJoined>(ISelectQB<TJoined> subQuery, string labelAs);
		ISelectQB<T> Join<TJoined>(string tableName, string labelAs);
		ISelectQB<T> LeftJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs);
		ISelectQB<T> LeftJoin<TJoined>(string tableName, string labelAs);
		ISelectQB<T> RightJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs);
		ISelectQB<T> RightJoin<TJoined>(string tableName, string labelAs);
		ISelectQB<T> FullJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs);
		ISelectQB<T> FullJoin<TJoined>(string tableName, string labelAs);
		ISelectQB<T> CrossJoin<TJoined>(ISelectQB<TJoined> subQuery, string labelAs);
		ISelectQB<T> CrossJoin<TJoined>(string tableName, string labelAs);

		ISelectQB<T> Union(ISelectQB<T> query);
		ISelectQB<T> UnionAll(ISelectQB<T> query);
		ISelectQB<T> Intersect(ISelectQB<T> query);
		ISelectQB<T> IntersectAll(ISelectQB<T> query);
		ISelectQB<T> Except(ISelectQB<T> query);
		ISelectQB<T> ExceptAll(ISelectQB<T> query);

		ISelectQB<T> On(Action<ISelectQB<T>> parenthesize, string joinedTableLabel);
		ISelectQB<T> OrOn(Action<ISelectQB<T>> parenthesize, string joinedTableLabel);
		ISelectQB<T> On(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrOn(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> On<TJoin>(Expression<Func<TJoin, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrOn<TJoin>(Expression<Func<TJoin, object?>> lhs, UnaryOperator op);
		ISelectQB<T> On<TJoin>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrOn<TJoin>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> On(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrOn(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> On<TJoin>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> OrOn<TJoin>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> On<TJoin, TJoined>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, Expression<Func<TJoined, object?>> rhs);
		ISelectQB<T> OrOn<TJoin, TJoined>(Expression<Func<TJoin, object?>> lhs, BinaryOperator op, Expression<Func<TJoined, object?>> rhs);

		ISelectQB<T> OrderBy(Expression<Func<T, object?>> lhs, UnaryOrderByHandler? ob = null);
		ISelectQB<T> OrderBy<T2>(Expression<Func<T2, object?>> lhs, UnaryOrderByHandler? ob = null);
		ISelectQB<T> OrderByComputed(AggrHandler ag, UnaryOrderByHandler? ob = null);
		ISelectQB<T> OrderByComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, UnaryOrderByHandler? ob = null);
		ISelectQB<T> OrderByComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, UnaryOrderByHandler? ob = null);

		ISelectQB<T> GroupBy(Expression<Func<T, object?>> lhs);
		ISelectQB<T> GroupBy<T2>(Expression<Func<T2, object?>> lhs);

		ISelectQB<T> Having(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrHaving(Expression<Func<T, object?>> lhs, UnaryOperator op);
		ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op);
		ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, UnaryOperator op);
		ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> Having(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrHaving(Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> Having<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> OrHaving<T2>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs);
		ISelectQB<T> Having<T2, T3>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T3, object?>> rhs);
		ISelectQB<T> OrHaving<T2, T3>(Expression<Func<T2, object?>> lhs, BinaryOperator op, Expression<Func<T3, object?>> rhs);
		ISelectQB<T> HavingComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrHavingComputed(UnaryAggrHandler ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> HavingComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);
		ISelectQB<T> OrHavingComputed<T2>(UnaryAggrHandler ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs);

		/// <summary>
		/// Adds clause "OFFSET <paramref name="offset"/> ROWS", where <paramref name="offset"/> is hardcoded to the query as a const literal value
		/// </summary>
		/// <remarks>Use <see cref="Offset"/> if you need a parameterized request</remarks>
		ISelectQB<T> Skip(long offset);

		/// <summary>
		/// Adds clause "OFFSET $param ROWS", where $param is a parameter with the value <paramref name="offset"/>
		/// </summary>
		/// <remarks>Use <see cref="Skip"/> if you need a non-parameterized request</remarks>
		ISelectQB<T> Offset(long offset);

		/// <summary>
		/// Adds clause "LIMIT <paramref name="limit"/>" or "FETCH NEXT <paramref name="limit"/>", where <paramref name="limit"/> is hardcoded to the query as a const literal value
		/// </summary>
		/// <remarks>Use <see cref="Limit"/> if you need a parameterized request</remarks>
		ISelectQB<T> Take(long limit, FetchNext fetchNext = FetchNext.RowsOnly);

		/// <summary>
		/// Adds clause "LIMIT $param" or "FETCH NEXT $param", where $param is a parameter with the value <paramref name="limit"/>
		/// </summary>
		/// <remarks>Use <see cref="Take"/> if you need a non-parameterized request</remarks>
		ISelectQB<T> Limit(long limit, FetchNext fetchNext = FetchNext.RowsOnly);

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
