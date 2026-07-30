using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity;

namespace System.Security.Claims
{
	/// <summary>The single method, <see cref="M:System.Security.Claims.DynamicRoleClaimProvider.AddDynamicRoleClaims(System.Security.Claims.ClaimsIdentity,System.Collections.Generic.IEnumerable{System.Security.Claims.Claim})" />, exposed by this class is obsolete. You can use a <see cref="T:System.Security.Claims.ClaimsAuthenticationManager" /> object to add claims to a <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
	// Token: 0x020007D6 RID: 2006
	public static class DynamicRoleClaimProvider
	{
		/// <summary>You can use a <see cref="T:System.Security.Claims.ClaimsAuthenticationManager" /> object to add claims to a <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
		/// <param name="claimsIdentity">The claims identity to which to add the claims.</param>
		/// <param name="claims">The claims to add.</param>
		// Token: 0x06004027 RID: 16423 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		[Obsolete("Use ClaimsAuthenticationManager to add claims to a ClaimsIdentity", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void AddDynamicRoleClaims(ClaimsIdentity claimsIdentity, IEnumerable<Claim> claims)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
