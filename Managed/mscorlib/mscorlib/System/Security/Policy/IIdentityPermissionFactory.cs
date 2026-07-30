using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines the method that creates a new identity permission.</summary>
	// Token: 0x0200056E RID: 1390
	[ComVisible(true)]
	public interface IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission for the specified evidence.</summary>
		/// <returns>The new identity permission.</returns>
		/// <param name="evidence">The evidence from which to create the new identity permission. </param>
		// Token: 0x06003E58 RID: 15960
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
