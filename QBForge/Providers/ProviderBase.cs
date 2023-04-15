using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using QBForge.Providers.Configuration;
using System;
using System.Globalization;

namespace QBForge.Providers
{
	internal abstract partial class ProviderBase : IQBProvider
	{
		public virtual string Name => "Default";
		public virtual string ParameterPlaceholder => "@p";
		public DocumentMapping Mapping { get; }

		public ProviderBase(DocumentMapping mapping)
		{
			Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));

			_mappingCache = new();

			_parameterPlaceholdersCache = new(100);
			for (int placeholder = 1; placeholder <= 100; placeholder++)
			{
				_parameterPlaceholdersCache.Add(placeholder, ParameterPlaceholder + placeholder.ToString(CultureInfo.InvariantCulture));
			}
		}

		public virtual ISelectQB<T> CreateSelectQB<T>()
		{
			return new SelectQB<T>(new QBContext(this, new SelectSectionClause(typeof(T))));
		}

		public virtual IWithCteQB CreateWithCteQB()
		{
			return new WithCteQB(new QBContext(this, new WithCteSectionClause()));
		}
	}
}
