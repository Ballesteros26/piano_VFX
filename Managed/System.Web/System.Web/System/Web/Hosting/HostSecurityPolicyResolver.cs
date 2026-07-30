using System;
using System.Security.Permissions;
using System.Security.Policy;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides a way to customize ASP.NET behavior at run time that overrides the ASP.NET code access security policy. </summary>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <see cref="P:System.Web.Configuration.TrustSection.HostSecurityPolicyResolverType" /> attribute has an invalid value or cannot be found.</exception>
	// Token: 0x02000765 RID: 1893
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class HostSecurityPolicyResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.HostSecurityPolicyResolver" /> class.</summary>
		// Token: 0x06004D30 RID: 19760 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public HostSecurityPolicyResolver()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates the security policy that should be applied to an assembly. </summary>
		/// <returns>A value that indicates the type of security permissions should be applied to an assembly.</returns>
		/// <param name="evidence">A collection of evidence about an assembly and a host, which is used as an input to security policy. </param>
		// Token: 0x06004D31 RID: 19761 RVA: 0x000CB308 File Offset: 0x000C9508
		public virtual HostSecurityPolicyResults ResolvePolicy(Evidence evidence)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return HostSecurityPolicyResults.DefaultPolicy;
		}
	}
}
