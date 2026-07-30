using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the type of a <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object.</summary>
	// Token: 0x0200008D RID: 141
	public enum TrustType
	{
		/// <summary>One of the domains in the trust relationship is a tree root.</summary>
		// Token: 0x04000176 RID: 374
		TreeRoot,
		/// <summary>The trust relationship is between a parent and a child domain.</summary>
		// Token: 0x04000177 RID: 375
		ParentChild,
		/// <summary>The trust relationship is a shortcut between two domains that exists to optimize the authentication processing between two domains that are in separate domain trees.</summary>
		// Token: 0x04000178 RID: 376
		CrossLink,
		/// <summary>The trust relationship is with a domain outside of the current forest.</summary>
		// Token: 0x04000179 RID: 377
		External,
		/// <summary>The trust relationship is between two forest root domains in separate Windows Server 2003 forests.</summary>
		// Token: 0x0400017A RID: 378
		Forest,
		/// <summary>The trusted domain is an MIT Kerberos realm.</summary>
		// Token: 0x0400017B RID: 379
		Kerberos,
		/// <summary>The trust is a non-specific type.</summary>
		// Token: 0x0400017C RID: 380
		Unknown
	}
}
