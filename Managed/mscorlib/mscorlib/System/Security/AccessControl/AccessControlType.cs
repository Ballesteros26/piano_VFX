using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies whether an <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow or deny access. These values are not flags, and they cannot be combined.</summary>
	// Token: 0x020005C5 RID: 1477
	public enum AccessControlType
	{
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow access to a secured object.</summary>
		// Token: 0x04002120 RID: 8480
		Allow,
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to deny access to a secured object.</summary>
		// Token: 0x04002121 RID: 8481
		Deny
	}
}
