using QBForge.Interfaces;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal sealed partial class SelectQB<T> : ISelectQB<T>
	{
		private readonly IQBContext _context;
		private readonly bool _foreignContext;

		IQBContext IQueryBuilder.Context => _context;

		public SelectQB(IQBContext context, bool foreignContext)
		{
			_context = context;
			_foreignContext = foreignContext;
		}

		public override string ToString() => ToString(out var _, ReadabilityLevels.Default);
		public string ToString(bool pretty) => ToString(out var _, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(out object? parameters, bool pretty) => ToString(out parameters, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(ReadabilityLevels level) => ToString(out var _, level);
		public string ToString(out object? parameters, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			var buildContent = _context.Provider.Build(this, _context.Provider.CreateBuildQueryContext(level));
			var query = buildContent.ToString();
			parameters = buildContent.Parameters;
			return query!;
		}

		ISelectQB<T> ISelectQB<T>.Distinct() => this;

		ISelectQB<T> ISelectQB<T>.Union(ISelectQB<T> query) => this;
		ISelectQB<T> ISelectQB<T>.UnionAll(ISelectQB<T> query) => this;
		ISelectQB<T> ISelectQB<T>.Intersect(ISelectQB<T> query) => this;
		ISelectQB<T> ISelectQB<T>.IntersectAll(ISelectQB<T> query) => this;
		ISelectQB<T> ISelectQB<T>.Except(ISelectQB<T> query) => this;
		ISelectQB<T> ISelectQB<T>.ExceptAll(ISelectQB<T> query) => this;

		ISelectQB<T> ISelectQB<T>.On(Action<ISelectQB<T>> parenthesized) => this;
		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op) => this;
		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.On<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.On<T2, TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrOn(Action<ISelectQB<T>> parenthesized) => this;
		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, UnaryOperator op) => this;
		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrOn<TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T, object?>> rhs) => this;
		ISelectQB<T> ISelectQB<T>.OrOn<T2, TJoined>(Expression<Func<TJoined, object?>> lhs, BinaryOperator op, Expression<Func<T2, object?>> rhs) => this;

		ISelectQB<T> ISelectQB<T>.OrderBy(Expression<Func<T, object?>> lhs, OrderByClauseDe? ob = null) => this;
		ISelectQB<T> ISelectQB<T>.OrderBy<T2>(Expression<Func<T2, object?>> lhs, OrderByClauseDe? ob = null) => this;

		ISelectQB<T> ISelectQB<T>.GroupBy(Expression<Func<T, object?>> lhs) => this;
		ISelectQB<T> ISelectQB<T>.GroupBy<T2>(Expression<Func<T2, object?>> lhs) => this;

		ISelectQB<T> ISelectQB<T>.Having(AggrCallClauseDe ag, Expression<Func<T, object?>> lhs, BinaryOperator op, dynamic rhs) => this;
		ISelectQB<T> ISelectQB<T>.Having<T2>(AggrCallClauseDe ag, Expression<Func<T2, object?>> lhs, BinaryOperator op, dynamic rhs) => this;

		ISelectQB<T> ISelectQB<T>.Skip(long offset, string? @label = null) => this;
		ISelectQB<T> ISelectQB<T>.Take(int limit, string? @label = null) => this;
	}
}
