using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

namespace QBForge.PostgreSql
{
	public static class Op
	{
		public static ICondOperator IsNull(this ICondOperator query, Clause lhs)
		{
			query.AppendArgument(lhs).Append(" IS NULL"); return query;
		}

		public static ICondOperator IsNotNull(this ICondOperator query, Clause lhs)
		{
			query.AppendArgument(lhs).Append(" IS NOT NULL"); return query;
		}

		public static ICondOperator Exists(this ICondOperator query, Clause lhs)
		{
			query
				.Append("EXISTS").TryAppendLine()
				.TryAppendCurrentIndent().Append('(').TryAppendLine();

			lhs.Render((IBuildQueryContext)query);

			query
				.TryAppendLine()
				.TryAppendCurrentIndent().Append(')');

			return query;
		}

		public static ICondOperator NotExists(this ICondOperator query, Clause lhs)
		{
			query
				.Append("NOT EXISTS").TryAppendLine()
				.TryAppendCurrentIndent().Append('(').TryAppendLine();

			lhs.Render((IBuildQueryContext)query);

			query
				.TryAppendLine()
				.TryAppendCurrentIndent().Append(')');

			return query;
		}

		public static ICondOperator Equal(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append('=').TryAppendSpace().AppendArgument(rhs); return query;
		}

		public static ICondOperator NotEqual(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append("!=").TryAppendSpace().AppendArgument(rhs); return query;
		}

		public static ICondOperator Greater(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append('>').TryAppendSpace().AppendArgument(rhs); return query;
		}

		public static ICondOperator GreaterOrEqual(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append(">=").TryAppendSpace().AppendArgument(rhs); return query;
		}

		public static ICondOperator Less(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append('<').TryAppendSpace().AppendArgument(rhs); return query;
		}

		public static ICondOperator LessOrEqual(this ICondOperator query, Clause lhs, Clause rhs)
		{
			query.AppendArgument(lhs).TryAppendSpace().Append("<=").TryAppendSpace().AppendArgument(rhs); return query;
		}
	}
}