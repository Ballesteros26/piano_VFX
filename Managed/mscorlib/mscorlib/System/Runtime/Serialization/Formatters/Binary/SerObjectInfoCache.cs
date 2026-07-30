using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000736 RID: 1846
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x06004C77 RID: 19575 RVA: 0x001115F4 File Offset: 0x0010F7F4
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x00111614 File Offset: 0x0010F814
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x040028D4 RID: 10452
		internal string fullTypeName;

		// Token: 0x040028D5 RID: 10453
		internal string assemblyString;

		// Token: 0x040028D6 RID: 10454
		internal bool hasTypeForwardedFrom;

		// Token: 0x040028D7 RID: 10455
		internal MemberInfo[] memberInfos;

		// Token: 0x040028D8 RID: 10456
		internal string[] memberNames;

		// Token: 0x040028D9 RID: 10457
		internal Type[] memberTypes;
	}
}
