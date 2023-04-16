using QBForge.Extensions;
using QBForge.Provider;
using QBForge.Provider.Clauses;

namespace QBForge.SqlServer
{
	public static class Op
	{
		public static ICondOperator IsNull(this ICondOperator render, Clause lhs)
		{
			render.AppendArgument(lhs).Append(" IS NULL"); return render;
		}

		public static ICondOperator IsNotNull(this ICondOperator render, Clause lhs)
		{
			render.AppendArgument(lhs).Append(" IS NOT NULL"); return render;
		}

		public static ICondOperator Exists(this ICondOperator render, Clause lhs)
		{
			render
				.Append("EXISTS").TryAppendLine()
				.TryAppendCurrentIndent(1).Append('(').TryAppendLine();

			render.CurrentIndent += 2;
			try
			{
				lhs.Render((IBuildQueryContext)render);
			}
			finally
			{
				render.CurrentIndent -= 2;
			}

			render
				.TryAppendLine()
				.TryAppendCurrentIndent(1).Append(')');

			return render;
		}

		public static ICondOperator NotExists(this ICondOperator render, Clause lhs)
		{
			render
				.Append("NOT EXISTS").TryAppendLine()
				.TryAppendCurrentIndent(1).Append('(').TryAppendLine();

			render.CurrentIndent += 2;
			try
			{
				lhs.Render((IBuildQueryContext)render);
			}
			finally
			{
				render.CurrentIndent -= 2;
			}

			render
				.TryAppendLine()
				.TryAppendCurrentIndent(1).Append(')');

			return render;
		}

		public static ICondOperator Equal(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append('=').TryAppendSpace().AppendArgument(rhs); return render;
		}

		public static ICondOperator NotEqual(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append("!=").TryAppendSpace().AppendArgument(rhs); return render;
		}

		public static ICondOperator Greater(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append('>').TryAppendSpace().AppendArgument(rhs); return render;
		}

		public static ICondOperator GreaterOrEqual(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append(">=").TryAppendSpace().AppendArgument(rhs); return render;
		}

		public static ICondOperator Less(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append('<').TryAppendSpace().AppendArgument(rhs); return render;
		}

		public static ICondOperator LessOrEqual(this ICondOperator render, Clause lhs, Clause rhs)
		{
			render.AppendArgument(lhs).TryAppendSpace().Append("<=").TryAppendSpace().AppendArgument(rhs); return render;
		}
	}
}