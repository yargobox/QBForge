using System;

namespace QBForge.Interfaces.Clauses
{
	public class UnaryHandlerClause : UnaryClause
	{
		public Delegate Handler { get; }

		public UnaryHandlerClause(UnaryOperator handler, Clause arg) : base(arg) => Handler = handler;
		public UnaryHandlerClause(UnaryOperator handler, DataEntry arg) : this(handler, new DataEntryClause(arg)) { }
		public UnaryHandlerClause(UnaryOperator handler, IQueryBuilder arg) : this(handler, new SubQueryClause(arg)) { }

		public UnaryHandlerClause(OrderByHandler handler, Clause arg) : base(arg) => Handler = handler;
		public UnaryHandlerClause(OrderByHandler handler, DataEntry arg) : this(handler, new DataEntryClause(arg)) { }

		public override void Render(IBuildQueryContext context)
		{
			Handler.DynamicInvoke(context.RenderContext, Left);
		}
	}
}