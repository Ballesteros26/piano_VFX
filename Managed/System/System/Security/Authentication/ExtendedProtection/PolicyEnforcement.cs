using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> enumeration specifies when the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> should be enforced.</summary>
	// Token: 0x02000387 RID: 903
	public enum PolicyEnforcement
	{
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is never enforced and extended protection is disabled.</summary>
		// Token: 0x040018CD RID: 6349
		Never,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is enforced only if the client and server supports extended protection.</summary>
		// Token: 0x040018CE RID: 6350
		WhenSupported,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is always enforced. Clients that don’t support extended protection will fail to authenticate.</summary>
		// Token: 0x040018CF RID: 6351
		Always
	}
}
