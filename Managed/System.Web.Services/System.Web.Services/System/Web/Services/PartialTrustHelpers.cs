using System;
using System.Security;
using System.Web.Hosting;

namespace System.Web.Services
{
	// Token: 0x02000005 RID: 5
	internal static class PartialTrustHelpers
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		[SecuritySafeCritical]
		internal static void FailIfInPartialTrustOutsideAspNet()
		{
			if (!PartialTrustHelpers.isInPartialTrustOutsideAspNetInitialized)
			{
				PartialTrustHelpers.isInPartialTrustOutsideAspNet = !AppDomain.CurrentDomain.IsFullyTrusted && !HostingEnvironment.IsHosted;
				PartialTrustHelpers.isInPartialTrustOutsideAspNetInitialized = true;
			}
			if (PartialTrustHelpers.isInPartialTrustOutsideAspNet)
			{
				throw new SecurityException(Res.GetString("CannotRunInPartialTrustOutsideAspNet"));
			}
		}

		// Token: 0x0400002C RID: 44
		[SecurityCritical]
		private static bool isInPartialTrustOutsideAspNet;

		// Token: 0x0400002D RID: 45
		[SecurityCritical]
		private static bool isInPartialTrustOutsideAspNetInitialized;
	}
}
