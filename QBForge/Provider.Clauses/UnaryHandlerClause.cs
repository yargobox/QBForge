using System;

namespace QBForge.Provider.Clauses
{
	public class UnaryHandlerClause : UnaryClause
	{
		public Delegate Handler { get; }

		public UnaryHandlerClause(UnaryOperator handler, Clause arg) : this((Delegate)handler, arg) { }
		public UnaryHandlerClause(UnaryOperator handler, DataEntry arg) : this((Delegate)handler, new DataEntryClause(arg)) { }
		public UnaryHandlerClause(UnaryOperator handler, IQueryBuilder arg) : this((Delegate)handler, new SubQueryClause(arg)) { }

		public UnaryHandlerClause(UnaryOrderByHandler handler, Clause arg) : this((Delegate)handler, arg) { }
		public UnaryHandlerClause(UnaryOrderByHandler handler, DataEntry arg) : this((Delegate)handler, new DataEntryClause(arg)) { }

		public UnaryHandlerClause(UnaryAggrHandler handler, Clause arg) : this((Delegate)handler, arg) { }
		public UnaryHandlerClause(UnaryAggrHandler handler, DataEntry arg) : this((Delegate)handler, new DataEntryClause(arg)) { }

		protected UnaryHandlerClause(Delegate handler, Clause clause) : base(clause) => Handler = handler;

		public override void Render(IBuildQueryContext context)
		{
			Handler.DynamicInvoke(context.RenderContext, Left);
		}

		public override Clause Clone()
		{
			var left = Left.Clone();

			if (ReferenceEquals(left, Left))
			{
				return this;
			}
			else
			{
				return new UnaryHandlerClause(Handler, left);
			}
		}
	}
}