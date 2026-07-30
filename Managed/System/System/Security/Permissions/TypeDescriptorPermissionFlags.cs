using System;

namespace System.Security.Permissions
{
	/// <summary>Defines permission settings for type descriptors. </summary>
	// Token: 0x02000372 RID: 882
	[Flags]
	[Serializable]
	public enum TypeDescriptorPermissionFlags
	{
		/// <summary>No permission flags are set on the type descriptor.</summary>
		// Token: 0x0400188A RID: 6282
		NoFlags = 0,
		/// <summary>The type descriptor may be called from partially trusted code.</summary>
		// Token: 0x0400188B RID: 6283
		RestrictedRegistrationAccess = 1
	}
}
