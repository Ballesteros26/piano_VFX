using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an application should be executed and which set of permissions should be granted to it.</summary>
	// Token: 0x0200056B RID: 1387
	[ComVisible(true)]
	public interface IApplicationTrustManager : ISecurityEncodable
	{
		/// <summary>Determines whether an application should be executed and which set of permissions should be granted to it.</summary>
		/// <returns>An object that contains security decisions about the application.</returns>
		/// <param name="activationContext">The activation context for the application.</param>
		/// <param name="context">The trust manager context for the application.</param>
		// Token: 0x06003E54 RID: 15956
		ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
	}
}
