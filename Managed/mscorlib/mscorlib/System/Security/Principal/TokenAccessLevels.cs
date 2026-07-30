using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the privileges of the user account associated with the access token. </summary>
	// Token: 0x02000628 RID: 1576
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TokenAccessLevels
	{
		/// <summary>The user can attach a primary token to a process.</summary>
		// Token: 0x04002274 RID: 8820
		AssignPrimary = 1,
		/// <summary>The user can duplicate the token.</summary>
		// Token: 0x04002275 RID: 8821
		Duplicate = 2,
		/// <summary>The user can impersonate a client.</summary>
		// Token: 0x04002276 RID: 8822
		Impersonate = 4,
		/// <summary>The user can query the token.</summary>
		// Token: 0x04002277 RID: 8823
		Query = 8,
		/// <summary>The user can query the source of the token.</summary>
		// Token: 0x04002278 RID: 8824
		QuerySource = 16,
		/// <summary>The user can enable or disable privileges in the token.</summary>
		// Token: 0x04002279 RID: 8825
		AdjustPrivileges = 32,
		/// <summary>The user can change the attributes of the groups in the token.</summary>
		// Token: 0x0400227A RID: 8826
		AdjustGroups = 64,
		/// <summary>The user can change the default owner, primary group, or discretionary access control list (DACL) of the token.</summary>
		// Token: 0x0400227B RID: 8827
		AdjustDefault = 128,
		/// <summary>The user can adjust the session identifier of the token.</summary>
		// Token: 0x0400227C RID: 8828
		AdjustSessionId = 256,
		/// <summary>The user has standard read rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.Query" /> privilege for the token.</summary>
		// Token: 0x0400227D RID: 8829
		Read = 131080,
		/// <summary>The user has standard write rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustPrivileges,F:System.Security.Principal.TokenAccessLevels.AdjustGroups" />, and <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustDefault" /> privileges for the token.</summary>
		// Token: 0x0400227E RID: 8830
		Write = 131296,
		/// <summary>The user has all possible access to the token.</summary>
		// Token: 0x0400227F RID: 8831
		AllAccess = 983551,
		/// <summary>The maximum value that can be assigned for the <see cref="T:System.Security.Principal.TokenAccessLevels" /> enumeration.</summary>
		// Token: 0x04002280 RID: 8832
		MaximumAllowed = 33554432
	}
}
