using System;

namespace QBForge.Provider.Clauses
{
	public class HandlerClause : TextClause
	{
		public Delegate Handler { get; }

		public HandlerClause(AggrHandler handler) => Handler = handler;

		public override void Render(IBuildQueryContext context)
		{
			Handler.DynamicInvoke(context.RenderContext);
		}

		public override Clause Clone()
		{
			return this;
		}
	}
}