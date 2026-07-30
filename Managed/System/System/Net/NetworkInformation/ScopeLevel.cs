using System;

namespace System.Net.NetworkInformation
{
	/// <summary>The scope level for an IPv6 address.</summary>
	// Token: 0x02000617 RID: 1559
	public enum ScopeLevel
	{
		/// <summary>The scope level is not specified.</summary>
		// Token: 0x0400280C RID: 10252
		None,
		/// <summary>The scope is interface-level.</summary>
		// Token: 0x0400280D RID: 10253
		Interface,
		/// <summary>The scope is link-level.</summary>
		// Token: 0x0400280E RID: 10254
		Link,
		/// <summary>The scope is subnet-level.</summary>
		// Token: 0x0400280F RID: 10255
		Subnet,
		/// <summary>The scope is admin-level.</summary>
		// Token: 0x04002810 RID: 10256
		Admin,
		/// <summary>The scope is site-level.</summary>
		// Token: 0x04002811 RID: 10257
		Site,
		/// <summary>The scope is organization-level.</summary>
		// Token: 0x04002812 RID: 10258
		Organization = 8,
		/// <summary>The scope is global.</summary>
		// Token: 0x04002813 RID: 10259
		Global = 14
	}
}
