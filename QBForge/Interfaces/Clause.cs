using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QBForge.Interfaces
{
	public abstract class Clause : IReadOnlyList<Clause>
	{
		public virtual string? Section { get; }

		public virtual int Count => Clauses?.Count ?? 0;
		public virtual IList<Clause>? Clauses { get; }

		public virtual Clause this[int index] => (Clauses ?? throw new NotSupportedException())[index];

		public abstract void Render(IBuildQueryContext context);

		public virtual void Add(Clause clause) => throw new NotSupportedException();
		public virtual void AddRange(IEnumerable<Clause> clauses) => throw new NotSupportedException();

		public virtual IEnumerator<Clause> GetEnumerator() => (Clauses ?? Enumerable.Empty<Clause>()).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public abstract class LeafClause : Clause
	{
		public sealed override IList<Clause>? Clauses => null;

		public sealed override void Add(Clause clause) => throw new NotSupportedException();
		public sealed override void AddRange(IEnumerable<Clause> clauses) => throw new NotSupportedException();
	}

	public abstract class CompositeClause : Clause, IEnumerable<Clause>
	{
		private readonly List<Clause> _clauses;

		public sealed override IList<Clause>? Clauses => _clauses;

		protected CompositeClause() => _clauses = new List<Clause>();

		public sealed override void Add(Clause clause) => _clauses.Add(clause);
		public sealed override void AddRange(IEnumerable<Clause> clauses) => _clauses.AddRange(clauses);

		public override IEnumerator<Clause> GetEnumerator() => _clauses.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}


	public abstract class SimpleClause : Clause
	{
	}

	public abstract class UnaryClause : Clause
	{
		public sealed override int Count => 1;
		public Clause Clause { get; }

		public UnaryClause(Clause clause)
		{
			Clause = clause;
		}

		public sealed override Clause this[int index] => index == 0 ? Clause : throw new ArgumentOutOfRangeException(nameof(index));

		public sealed override IEnumerator<Clause> GetEnumerator()
		{
			yield return Clause;
		}
	}

	public abstract class BinaryClause : Clause
	{
		public sealed override int Count => 2;

		public Clause Left { get; }
		public Clause Right { get; }

		public BinaryClause(Clause left, Clause right)
		{
			Left = left;
			Right = right;
		}

		public sealed override Clause this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return Left;
					case 1: return Right;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public sealed override IEnumerator<Clause> GetEnumerator()
		{
			yield return Left;
			yield return Right;
		}
	}

	public abstract class TernaryClause : Clause
	{
		public sealed override int Count => 3;

		public Clause Left { get; }
		public Clause Right1 { get; }
		public Clause Right2 { get; }

		public TernaryClause(Clause left, Clause right1, Clause right2)
		{
			Left = left;
			Right1 = right1;
			Right2 = right2;
		}

		public sealed override Clause this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return Left;
					case 1: return Right1;
					case 2: return Right2;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public sealed override IEnumerator<Clause> GetEnumerator()
		{
			yield return Left;
			yield return Right1;
			yield return Right2;
		}
	}

	public abstract class BlockClause : Clause
	{
		private readonly List<Clause> _clauses;

		public sealed override int Count => _clauses.Count;
		public sealed override IList<Clause>? Clauses => _clauses;

		public BlockClause()
		{
			_clauses = new List<Clause>();
		}

		public sealed override Clause this[int index] => _clauses[index];

		public override void Add(Clause clause) => _clauses.Add(clause);
		public override void AddRange(IEnumerable<Clause> clauses) => _clauses.AddRange(clauses);

		public sealed override IEnumerator<Clause> GetEnumerator() => _clauses.GetEnumerator();
	}

	public class DataEntryClouse : SimpleClause
	{
		public DataEntry DataEntry { get; }
		public string? LabelAs { get; }

		public DataEntryClouse(DataEntry de, string? labelAs = null)
		{
			DataEntry = de;
			LabelAs = labelAs;
		}

		public override void Render(IBuildQueryContext context)
		{
			if (!string.IsNullOrEmpty(DataEntry.Label))
			{
				context.RenderContext.AppendLabel(DataEntry.Label!);
				context.RenderContext.Append('.');
			}
			context.RenderContext.AppendIdentifier(DataEntry.Name);
			if (!string.IsNullOrEmpty(LabelAs))
			{
				context.RenderContext.AppendAsLabel(LabelAs!);
			}
		}
	}

	public class TableClouse : SimpleClause
	{
		public string TableName { get; }
		public string? LabelAs { get; }

		public TableClouse(string tableName, string? labelAs = null)
		{
			TableName = tableName;
			LabelAs = labelAs;
		}

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.AppendIdentifier(TableName);
			if (!string.IsNullOrEmpty(LabelAs))
			{
				context.RenderContext.AppendAsLabel(LabelAs!);
			}
		}
	}

	public class ConstClouse : SimpleClause
	{
		public object? Value { get; }

		public ConstClouse(object? value)
		{
			Value = value;
		}

		public override void Render(IBuildQueryContext context)
		{
		}
	}

	public class SelectSectionClause : BlockClause
	{
		public override string? Section => ClauseSections.Select;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.Append("SELECT");

			foreach (var clause in this)
			{
				context.RenderContext.AppendLine();

				clause.Render(context);
			}
		}
	}

	public class FromSectionClause : UnaryClause
	{
		public override string? Section => ClauseSections.From;

		public FromSectionClause(TableClouse tableClouse) : base(tableClouse) { }

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.AppendLine().Append("FROM ");
			
			Clause.Render(context);
		}
	}

	public class IncludeSectionClause : BlockClause
	{
		public override string? Section => ClauseSections.Include;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.AppendLine();

			var next = false;
			foreach (var clause in this)
			{
				if (next) context.RenderContext.Append(',').AppendLine().Append('\t'); else next = true;

				clause.Render(context);
			}
		}
	}

	public class JoinSectionClause : BlockClause
	{
		public override string? Section => ClauseSections.Join;

		public override void Render(IBuildQueryContext context)
		{
			foreach (var clause in this)
			{
				context.RenderContext.AppendLine();

				clause.Render(context);
			}
		}
	}

	public class WhereSectionClause : BlockClause
	{
		public override string? Section => ClauseSections.Where;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.Append("WHERE").AppendLine().Append('\t');

			foreach (var clause in this)
			{
				clause.Render(context);
			}
		}
	}

	//public class SelectSectionClause : CompositeClause
	//{
	//	public SelectSectionClause()
	//	{
	//		_ = new SelectSectionClause()
	//		{
	//			new WithCteSectionClause()
	//			{
	//				new SelectSectionClause() { },
	//				new SelectSectionClause() { },
	//			},
	//			new IncludeSectionClause()
	//			{
	//				new FuncClause()
	//				{
	//					new DataEntryClause(),
	//					new ParamClause()
	//				},
	//				new DataEntryClause()
	//			},
	//			new FromSectionClause(),
	//			new JoinSectionClause()
	//			{
	//				new JoinClause()
	//				{
	//					new WhereClause() { new DataEntryClause(), new DataEntryClause() }
	//				}
	//			},
	//			new WhereSectionClause()
	//			{
	//				new WhereClause() { new DataEntryClause(), new ParamClause() },
	//				new WhereClause() { new DataEntryClause() }
	//			},
	//			new UnionSectionClause()
	//			{
	//				new SelectSectionClause() { },
	//				new SelectSectionClause() { },
	//			}
	//		};
	//	}

	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class WithCteSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class IncludeSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class FromSectionClause : LeafClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class WhereSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class JoinSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class UnionSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class OrderBySectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class GroupBySectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class HavingSectionClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class JoinClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class DataEntryClause : LeafClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class ParamClause : LeafClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class WhereClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class FuncClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}

	//public class AggrClause : CompositeClause
	//{
	//	public override void Render(IBuildQueryContext context) => throw new NotImplementedException();
	//}


}
