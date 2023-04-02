using QBForge.Extensions.Linq;
using QBForge.Providers.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace QBForge.Providers
{
	internal partial class ProviderBase
	{
		private readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, MemberMappingInfo>> _mappingCache;
		private readonly Dictionary<int, string> _parameterPlaceholdersCache;

		public string GetMappedName<T>(string name)
		{
			return GetMappingInfo<T>()[name].MappedName;
		}

		public IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo<T>()
		{
			return _mappingCache.GetOrAdd(typeof(T), x => GetInfo());

			IReadOnlyDictionary<string, MemberMappingInfo> GetInfo()
			{
				var mappingInfo = Mapping.GetDocumentMappingInfo<T>();
				return mappingInfo.AsOrderedReadOnlyDictionary(mi => mi.Name, null, mappingInfo.Count >= 8);
			}
		}
	}
}
