using System;

namespace System.Web.Compilation
{
	// Token: 0x02000651 RID: 1617
	internal sealed class DefaultResourceProviderFactory : ResourceProviderFactory
	{
		// Token: 0x06004575 RID: 17781 RVA: 0x000BE34E File Offset: 0x000BC54E
		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new DefaultResourceProvider(classKey, true);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000BE357 File Offset: 0x000BC557
		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			return new DefaultResourceProvider(virtualPath, false);
		}
	}
}
