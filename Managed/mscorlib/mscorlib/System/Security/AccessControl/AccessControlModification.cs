using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the type of access control modification to perform. This enumeration is used by methods of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class and its descendents.</summary>
	// Token: 0x020005C3 RID: 1475
	public enum AccessControlModification
	{
		/// <summary>Add the specified authorization rule to the access control list (ACL).</summary>
		// Token: 0x04002112 RID: 8466
		Add,
		/// <summary>Remove all authorization rules from the ACL, then add the specified authorization rule to the ACL.</summary>
		// Token: 0x04002113 RID: 8467
		Set,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL, and then add the specified authorization rule to the ACL.</summary>
		// Token: 0x04002114 RID: 8468
		Reset,
		/// <summary>Remove authorization rules that contain the same security identifier (SID) and access mask as the specified authorization rule from the ACL.</summary>
		// Token: 0x04002115 RID: 8469
		Remove,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL.</summary>
		// Token: 0x04002116 RID: 8470
		RemoveAll,
		/// <summary>Remove authorization rules that exactly match the specified authorization rule from the ACL.</summary>
		// Token: 0x04002117 RID: 8471
		RemoveSpecific
	}
}
