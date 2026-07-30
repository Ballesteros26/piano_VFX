using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of a principal object.</summary>
	// Token: 0x02000621 RID: 1569
	[ComVisible(true)]
	public interface IPrincipal
	{
		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600443C RID: 17468
		IIdentity Identity { get; }

		/// <summary>Determines whether the current principal belongs to the specified role.</summary>
		/// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
		/// <param name="role">The name of the role for which to check membership. </param>
		// Token: 0x0600443D RID: 17469
		bool IsInRole(string role);
	}
}
