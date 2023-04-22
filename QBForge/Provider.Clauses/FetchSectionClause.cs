using System;
using System.Globalization;

namespace QBForge.Provider.Clauses
{
	public class FetchSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.Fetch;
		public FetchNext FetchNext { get; }

		public FetchSectionClause(long limit, FetchNext fetchNext, bool parametrized) : this(parametrized ? new ParameterClause(limit) : new ConstClause<long>(limit), fetchNext) { }
		protected FetchSectionClause(Clause clause, FetchNext fetchNext) : base(clause) => FetchNext = fetchNext;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (FetchNext == FetchNext.RowsOnly)
			{
				render.Append("LIMIT ");
			}
			else if (FetchNext == FetchNext.RowsWithTies)
			{
				render.Append("FETCH NEXT ");
			}
			else
			{
				throw new NotSupportedFeatureException($"Clause \"FETCH\" does not support a fetch method such as \"FETCH NEXT n ROWS {FetchNext}\"");
			}

			if (Left is ConstClause<long> longLeft)
			{
				render.Append(longLeft.Value.ToString(CultureInfo.InvariantCulture));
			}
			else if (Left is ParameterClause paramLeft)
			{
				render.Append(render.MakeParamPlaceholder(paramLeft.Value));
			}
			else
			{
				Left.Render(context);
			}

			if (FetchNext == FetchNext.RowsWithTies)
			{
				render.Append(" ROWS WITH TIES");
			}
		}

		public override string ToString()
		{
			if (Left is ConstClause<long> longLeft)
			{
				return $"LIMIT {longLeft.Value}";
			}
			else if (Left is ParameterClause paramLeft)
			{
				return $"LIMIT $?(={paramLeft.Value})";
			}
			else
			{
				return "LIMIT " + Left.ToString();
			}
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
				return new FetchSectionClause(left, FetchNext);
			}
		}
	}
}
