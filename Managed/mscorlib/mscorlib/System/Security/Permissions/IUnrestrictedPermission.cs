using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows a permission to expose an unrestricted state.</summary>
	// Token: 0x02000596 RID: 1430
	[ComVisible(true)]
	public interface IUnrestrictedPermission
	{
		/// <summary>Returns a value indicating whether unrestricted access to the resource protected by the permission is allowed.</summary>
		/// <returns>true if unrestricted use of the resource protected by the permission is allowed; otherwise, false.</returns>
		// Token: 0x06003FE3 RID: 16355
		bool IsUnrestricted();
	}
}
