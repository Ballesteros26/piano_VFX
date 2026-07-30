using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the security policy components to be used by the host security manager.</summary>
	// Token: 0x0200053E RID: 1342
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		/// <summary>Use none of the security policy components.</summary>
		// Token: 0x04001F43 RID: 8003
		None = 0,
		/// <summary>Use the application domain evidence.</summary>
		// Token: 0x04001F44 RID: 8004
		HostAppDomainEvidence = 1,
		/// <summary>Use the policy level specified in the <see cref="P:System.Security.HostSecurityManager.DomainPolicy" /> property.</summary>
		// Token: 0x04001F45 RID: 8005
		HostPolicyLevel = 2,
		/// <summary>Use the assembly evidence.</summary>
		// Token: 0x04001F46 RID: 8006
		HostAssemblyEvidence = 4,
		/// <summary>Route calls to the <see cref="M:System.Security.Policy.ApplicationSecurityManager.DetermineApplicationTrust(System.ActivationContext,System.Security.Policy.TrustManagerContext)" /> method to the <see cref="M:System.Security.HostSecurityManager.DetermineApplicationTrust(System.Security.Policy.Evidence,System.Security.Policy.Evidence,System.Security.Policy.TrustManagerContext)" /> method first.</summary>
		// Token: 0x04001F47 RID: 8007
		HostDetermineApplicationTrust = 8,
		/// <summary>Use the <see cref="M:System.Security.HostSecurityManager.ResolvePolicy(System.Security.Policy.Evidence)" /> method to resolve the application evidence.</summary>
		// Token: 0x04001F48 RID: 8008
		HostResolvePolicy = 16,
		/// <summary>Use all security policy components.</summary>
		// Token: 0x04001F49 RID: 8009
		AllFlags = 31
	}
}
