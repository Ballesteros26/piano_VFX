using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to user data for a user-defined method or function.</summary>
	// Token: 0x020003B6 RID: 950
	[Serializable]
	public enum DataAccessKind
	{
		/// <summary>The method or function does not access user data.</summary>
		// Token: 0x04001B37 RID: 6967
		None,
		/// <summary>The method or function reads user data.</summary>
		// Token: 0x04001B38 RID: 6968
		Read
	}
}
