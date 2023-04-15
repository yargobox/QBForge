﻿using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class WithCteSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.WithCte;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			var next = false;
			foreach (var clause in this)
			{
				if (next)
				{
					render
						.Append(',').TryAppendLineOrAppendSpace()
						.TryAppendCurrentIndent();
				}
				else
				{
					next = true;

					render.TryAppendCurrentIndent().Append("WITH ");
				}

				clause.Render(context);
			}
		}
	}
}