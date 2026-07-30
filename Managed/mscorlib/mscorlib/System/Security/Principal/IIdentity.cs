using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of an identity object.</summary>
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	public interface IIdentity
	{
		/// <summary>Gets the type of authentication used.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004439 RID: 17465
		string AuthenticationType { get; }

		/// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
		/// <returns>true if the user was authenticated; otherwise, false.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600443A RID: 17466
		bool IsAuthenticated { get; }

		/// <summary>Gets the name of the current user.</summary>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600443B RID: 17467
		string Name { get; }
	}
}
