using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace QBForge.Interfaces
{
	//public abstract class ClauseEntry
	//{
	//	public string Section { get; }
	//	public string? Key { get; }
	//	public object? Parameters { get; }
	//	public virtual int ParameterCount { get; }

	//	public ClauseEntry(string section, string? key)
	//	{
	//		Section = section;
	//		Key = key;
	//	}

	//	public abstract void Render(IBuildQueryContext context);
	//}

	//public class ClauseDe : ClauseEntry
	//{
	//	public DataEntry FirstArg { get; }
	//	public Delegate Handler { get; }

	//	public ClauseDe(string section, string? key, Delegate handler, DataEntry firstArg) : base(section, key)
	//	{
	//		Handler = handler;
	//		FirstArg = firstArg;
	//	}

	//	public override void Render(IBuildQueryContext context)
	//	{
	//		Handler.DynamicInvoke(context.RenderContext, FirstArg);
	//	}
	//}

	//public class ClauseDeV : ClauseEntry
	//{
	//	public DataEntry FirstArg { get; }
	//	public object? SecondArg { get; }
	//	public Delegate Handler { get; }
	//	public override int ParameterCount => 1;

	//	public ClauseDeV(string section, string? key, Delegate handler, DataEntry firstArg, object? secondArg) : base(section, key)
	//	{
	//		Handler = handler;
	//		FirstArg = firstArg;
	//		SecondArg = secondArg;
	//	}

	//	public override void Render(IBuildQueryContext context)
	//	{
	//		Handler.DynamicInvoke(context.RenderContext, FirstArg, context.MakeParamPlaceholder());
	//	}
	//}

	//public class ClauseDeDe : ClauseEntry
	//{
	//	public DataEntry FirstArg { get; }
	//	public DataEntry SecondArg { get; }
	//	public Delegate Handler { get; }

	//	public ClauseDeDe(string section, string? key, Delegate handler, DataEntry firstArg, DataEntry secondArg) : base(section, key)
	//	{
	//		Handler = handler;
	//		FirstArg = firstArg;
	//		SecondArg = secondArg;
	//	}

	//	public override void Render(IBuildQueryContext context)
	//	{
	//		Handler.DynamicInvoke(context.RenderContext, FirstArg, SecondArg);
	//	}
	//}

	//public class TextClause : ClauseEntry
	//{
	//	public string Text { get; }
	//	public DataEntry FirstArg { get; }

	//	public TextClause(string section, string? key, string text) : base(section, key)
	//	{
	//		Text = text;
	//	}

	//	public TextClause(string section, string? key, string text, DataEntry firstArg) : base(section, key)
	//	{
	//		Text = text;
	//		FirstArg = firstArg;
	//	}

	//	public override void Render(IBuildQueryContext context)
	//	{
	//		context.RenderContext.Append(Text);
	//	}
	//}
}