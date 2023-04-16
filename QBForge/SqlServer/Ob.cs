using QBForge.Extensions;
using QBForge.Provider;
using QBForge.Provider.Clauses;

#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace QBForge.SqlServer
{
	public static class Ob
	{
		public static IOrderByRender ASC(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" ASC"); return render;
		}

		public static IOrderByRender DESC(this IOrderByRender render, Clause arg)
		{
			render.AppendArgument(arg).Append(" DESC"); return render;
		}
	}
}

#pragma warning restore CA1707 // Identifiers should not contain underscores