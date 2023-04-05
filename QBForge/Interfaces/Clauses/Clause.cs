using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Interfaces.Clauses
{
	public abstract class Clause : IReadOnlyList<Clause>
	{
		public virtual string? Key { get; }

		public virtual int Count => Clauses?.Count ?? 0;
		public virtual IList<Clause>? Clauses { get; }

		public virtual Clause this[int index] => (Clauses ?? throw new NotSupportedException())[index];

		public abstract void Render(IBuildQueryContext context);

		public virtual void Add(Clause clause) => throw new NotSupportedException();
		public virtual void AddRange(IEnumerable<Clause> clauses) => throw new NotSupportedException();

		public virtual IEnumerator<Clause> GetEnumerator() => (Clauses ?? Enumerable.Empty<Clause>()).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public abstract class ConstClause : Clause
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
}