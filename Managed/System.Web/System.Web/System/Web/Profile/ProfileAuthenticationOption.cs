using System;

namespace System.Web.Profile
{
	/// <summary>Describes the authentication type of user profiles to be searched.</summary>
	// Token: 0x02000501 RID: 1281
	public enum ProfileAuthenticationOption
	{
		/// <summary>Search only anonymous profiles.</summary>
		// Token: 0x04001F11 RID: 7953
		Anonymous,
		/// <summary>Search only authenticated profiles.</summary>
		// Token: 0x04001F12 RID: 7954
		Authenticated,
		/// <summary>Search all profiles.</summary>
		// Token: 0x04001F13 RID: 7955
		All
	}
}
