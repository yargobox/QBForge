using QBForge.Extensions;
using QBForge.Provider;
using QBForge.Provider.Clauses;

namespace QBForge.SqlServer
{
	public static class Fn
	{
		public static IFuncRender ISNULL(this IFuncRender render, Clause arg, Clause value)
		{
			render.Append("ISNULL(").AppendArgument(arg).Append(", ").AppendArgument(value).Append(')'); return render;
		}

		public static IFuncRender LOWER(this IFuncRender render, Clause arg)
		{
			render.Append("LOWER(").AppendArgument(arg).Append(')'); return render;
		}

		public static IFuncRender UPPER(this IFuncRender render, Clause arg)
		{
			render.Append("UPPER(").AppendArgument(arg).Append(')'); return render;
		}

		public static IFuncRender LEN(this IFuncRender render, Clause arg)
		{
			render.Append("LEN(").AppendArgument(arg).Append(')'); return render;
		}

		public static IFuncRender DATALENGTH(this IFuncRender render, Clause arg)
		{
			render.Append("DATALENGTH(").AppendArgument(arg).Append(')'); return render;
		}
	}
}