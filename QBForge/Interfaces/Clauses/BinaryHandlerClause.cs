using System;

namespace QBForge.Interfaces.Clauses
{
	public class BinaryHandlerClause : BinaryClause
	{
		public Delegate Handler { get; }

		public BinaryHandlerClause(BinaryOperator handler, Clause lhs, Clause rhs) : base(lhs, rhs) => Handler = handler;
		public BinaryHandlerClause(BinaryOperator handler, DataEntry lhs, DataEntry rhs) : this(handler, new DataEntryClouse(lhs), new DataEntryClouse(rhs)) { }
		public BinaryHandlerClause(BinaryOperator handler, DataEntry lhs, object? rhs) : this(handler, new DataEntryClouse(lhs), new ParameterClouse(rhs)) { }
		public BinaryHandlerClause(BinaryOperator handler, object? lhs, DataEntry rhs) : this(handler, new ParameterClouse(lhs), new DataEntryClouse(rhs)) { }

		public override void Render(IBuildQueryContext context)
		{
			Handler.DynamicInvoke(context.RenderContext, Left, Right);
		}
	}
}