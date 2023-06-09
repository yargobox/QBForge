﻿using System;

namespace QBForge.Provider.Clauses
{
	public class SubQueryClause : ValueClause<IQueryBuilder>
	{
		public SubQueryClause(IQueryBuilder queryBuilder) : base(queryBuilder) { }

		public override void Render(IBuildQueryContext context)
		{
			if (!ReferenceEquals(Value.Context.Provider, context.RenderContext.Provider)) throw new InvalidOperationException();

			context.RenderContext.Provider.Build(Value, context);
		}

		public override Clause Clone()
		{
			var clone = (IQueryBuilder)Value.Clone();

			if (ReferenceEquals(clone, Value))
			{
				return this;
			}
			else
			{
				return new SubQueryClause(clone);
			}
		}
	}
}