﻿using QBForge.Extensions;
using QBForge.Provider;
using QBForge.Provider.Clauses;

#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace QBForge.PostgreSql
{
	public static class Ob
	{
		public static IOrderByRender ASC(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" ASC"); return render;
		}

		public static IOrderByRender ASC_NULLS_FIRST(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" ASC NULLS FIRST"); return render;
		}

		public static IOrderByRender DESC(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" DESC"); return render;
		}

		public static IOrderByRender DESC_NULLS_LAST(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" DESC NULLS LAST"); return render;
		}

		public static IOrderByRender RANK(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" RANK"); return render;
		}
	}
}

#pragma warning restore CA1707 // Identifiers should not contain underscores