using System;

namespace System.Web.Hosting
{
	/// <summary>Specifies the type of security policy to apply to an assembly. </summary>
	// Token: 0x02000766 RID: 1894
	public enum HostSecurityPolicyResults
	{
		/// <summary>Indicates that the permission set that is associated with the <see cref="T:System.AppDomain" /> instance should be applied to the specified assembly. This means that the assembly should be treated as a user assembly that is loaded into the partial-trust ASP.NET <see cref="T:System.AppDomain" /> instance. In addition, the assembly should have the permission set that was assigned to the <see cref="T:System.AppDomain" /> instance at initialization.</summary>
		// Token: 0x040025E7 RID: 9703
		AppDomainTrust = 2,
		/// <summary>Indicates that ASP.NET should use default logic to determine the appropriate permissions set for the specified assembly. You should return the <see cref="F:System.Web.Hosting.HostSecurityPolicyResults.DefaultPolicy" /> value if you do not you want to decide the permission set for the assembly.</summary>
		// Token: 0x040025E8 RID: 9704
		DefaultPolicy = 0,
		/// <summary>Indicates that the specified assembly should be granted full trust. </summary>
		// Token: 0x040025E9 RID: 9705
		FullTrust,
		/// <summary>Indicates that the permission set for the specified assembly is set to empty. An empty permission set is a new instance of the <see cref="T:System.Security.PermissionSet" /> class, with a parameter value of <see cref="F:System.Security.Permissions.PermissionState.None" /> passed to the constructor. An assembly that is associated with an empty permission set will not load in an ASP.NET partial trust application domain. Therefore, you can use the <see cref="F:System.Web.Hosting.HostSecurityPolicyResults.Nothing" /> field to prevent an assembly from loading into a partial trust ASP.NET application domain.</summary>
		// Token: 0x040025EA RID: 9706
		Nothing = 3
	}
}
