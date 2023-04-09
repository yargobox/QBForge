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
			return GetMappedName(typeof(T), name);
		}

		public string GetMappedName(Type documentType, string name)
		{
			return GetMappingInfo(documentType)[name].MappedName;
		}

		public IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo<T>()
		{
			return GetMappingInfo(typeof(T));
		}

		public IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo(Type documentType)
		{
			return _mappingCache.GetOrAdd(documentType, x => GetInfo());

			IReadOnlyDictionary<string, MemberMappingInfo> GetInfo()
			{
				var mappingInfo = Mapping.GetDocumentMappingInfo(documentType);
				return mappingInfo.AsOrderedReadOnlyDictionary(mi => mi.Name, null, mappingInfo.Count >= 8);
			}
		}
	}
}
