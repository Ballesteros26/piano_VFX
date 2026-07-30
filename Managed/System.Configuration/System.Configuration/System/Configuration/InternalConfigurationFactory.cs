using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000049 RID: 73
	internal class InternalConfigurationFactory : IInternalConfigConfigurationFactory
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00007D0F File Offset: 0x00005F0F
		public Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			InternalConfigurationSystem internalConfigurationSystem = new InternalConfigurationSystem();
			internalConfigurationSystem.Init(typeConfigHost, hostInitConfigurationParams);
			return new Configuration(internalConfigurationSystem, null);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007D24 File Offset: 0x00005F24
		public string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			return subPath;
		}
	}
}
