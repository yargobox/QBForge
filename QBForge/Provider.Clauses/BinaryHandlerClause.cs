using System;

namespace QBForge.Provider.Clauses
{
	public class BinaryHandlerClause : BinaryClause
	{
		public Delegate Handler { get; }

		public BinaryHandlerClause(BinaryOperator handler, Clause lhs, Clause rhs) : base(lhs, rhs) => Handler = handler;
		public BinaryHandlerClause(BinaryOperator handler, DataEntry lhs, DataEntry rhs) : this(handler, new DataEntryClause(lhs), new DataEntryClause(rhs)) { }
		public BinaryHandlerClause(BinaryOperator handler, DataEntry lhs, object? rhs) : this(handler, new DataEntryClause(lhs), new ParameterClause(rhs)) { }
		public BinaryHandlerClause(BinaryOperator handler, object? lhs, DataEntry rhs) : this(handler, new ParameterClause(lhs), new DataEntryClause(rhs)) { }

		public override void Render(IBuildQueryContext context)
		{
			Handler.DynamicInvoke(context.RenderContext, Left, Right);
		}

		public override Clause Clone()
		{
			var left = Left.Clone();
			var right = Right.Clone();

			if (ReferenceEquals(left, Left) && ReferenceEquals(right, Right))
			{
				return this;
			}
			else
			{
				return new BinaryHandlerClause((BinaryOperator)Handler, left, right);
			}
		}
	}
}