using System.Reflection.Emit;

namespace QBForge.Interfaces.Clauses
{
	public class DataEntryClause : ValueClause<DataEntry>
	{
		public override string? Key => Value.Name;

		public DataEntryClause(DataEntry de) : base(de) { }
		public DataEntryClause(string? refLabel, string name) : base(new DataEntry(refLabel, name)) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (!string.IsNullOrEmpty(Value.RefLabel))
			{
				render.AppendLabel(Value.RefLabel!);
				render.Append('.');
			}
			render.AppendObject(Value.Name);
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public override Clause Clone()
		{
			return this;
		}
	}
}
