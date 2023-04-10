using System;

namespace QBForge.Interfaces.Clauses
{
	public class SubQueryClause : ValueClause<IQueryBuilder>
	{
		public SubQueryClause(IQueryBuilder queryBuilder) : base(queryBuilder) { }

		public override void Render(IBuildQueryContext context)
		{
			if (!object.ReferenceEquals(Value.Context.Provider, context.RenderContext.Provider)) throw new InvalidOperationException();

			context.RenderContext.Provider.Build(Value, context);
		}
	}
}