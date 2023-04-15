using QBForge.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QBForge.Interfaces.Clauses
{
	[DebuggerDisplay("Clause = {ToString()}, Key = {Key}")]
	public abstract class Clause : IReadOnlyList<Clause>
	{
		public static Clause Empty => StaticEmptyClause.Empty;

		public virtual string? Key { get; }

		public virtual int Count => Clauses?.Count ?? 0;
		public virtual IList<Clause>? Clauses { get; }

		public virtual Clause this[int index] => (Clauses ?? throw new NotSupportedException())[index];

		public abstract void Render(IBuildQueryContext context);

		public virtual void Add(Clause clause) => throw new NotSupportedException();
		public virtual void AddRange(IEnumerable<Clause> clauses) => throw new NotSupportedException();

		public virtual IEnumerator<Clause> GetEnumerator() => (Clauses ?? Enumerable.Empty<Clause>()).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public virtual Clause Clone() => throw new NotSupportedException();

		public override string? ToString()
		{
			var context = ProviderBase.Default.CreateBuildQueryContext(ReadabilityLevels.High);
			Render(context);
			return context.ToString();
		}

		private static class StaticEmptyClause
		{
			static StaticEmptyClause() { }

			public static readonly Clause Empty = new EmptyClause();

			private sealed class EmptyClause : Clause
			{
				public override void Render(IBuildQueryContext context) { }
			}
		}
	}

	public abstract class TextClause : Clause
	{
	}

	public abstract class ValueClause<T> : Clause
	{
		public T Value { get; }

		public ValueClause(T value) => Value = value;
	}

	public abstract class UnaryClause : Clause
	{
		public sealed override int Count => 1;
		public Clause Left { get; }

		public UnaryClause(Clause clause)
		{
			Left = clause;
		}

		public sealed override Clause this[int index] => index == 0 ? Left : throw new ArgumentOutOfRangeException(nameof(index));

		public sealed override IEnumerator<Clause> GetEnumerator()
		{
			yield return Left;
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