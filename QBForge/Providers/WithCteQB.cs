using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Linq;

#pragma warning disable CA1716

namespace QBForge.Providers
{
	internal sealed class WithCteQB : IWithCteQB
	{
		private readonly IQBContext _context;

		public IQBContext Context => _context;

		public WithCteQB(IQBContext context)
		{
			_context = context;
		}

		public override string ToString() => ToString(out var _, ReadabilityLevels.Default);
		public string ToString(bool pretty) => ToString(out var _, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(out object? parameters, bool pretty) => ToString(out parameters, pretty ? ReadabilityLevels.Middle : ReadabilityLevels.Default);
		public string ToString(ReadabilityLevels level) => ToString(out var _, level);
		public string ToString(out object? parameters, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			var buildContent = _context.Provider.Build(this, _context.Provider.CreateBuildQueryContext(level));
			var query = buildContent.ToString();
			parameters = buildContent.Parameters;
			return query!;
		}

		public IWithCteQB With<TCte>(string labelCte, ISelectQB<TCte> subQuery)
		{
			if (string.IsNullOrEmpty(labelCte)) throw new ArgumentNullException(nameof(labelCte));
			if (subQuery == null) throw new ArgumentNullException(nameof(subQuery));
			if (Context.Clause.Any(x => x.Key == labelCte)) throw new ArgumentException($"Lablel \"{labelCte}\" has already been added.", nameof(labelCte));

			Context.Clause.Add(new WithCteClause(labelCte, new SubQueryClause(subQuery)));

			return this;
		}

		public ISelectQB<T> Select<T>(string tableName, string? labelAs = null, dynamic? parameters = null)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

			var selectQB = Context.Provider.CreateSelectQB<T>();

			selectQB.Context.Clause.Add(Context.Clause);//!!! deep copy is needed here

			return selectQB.From(tableName, labelAs, parameters);
		}
	}
}

#pragma warning restore CA1716