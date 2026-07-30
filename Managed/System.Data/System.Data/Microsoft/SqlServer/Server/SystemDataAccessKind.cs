using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to system data for a user-defined method or function.</summary>
	// Token: 0x020003B7 RID: 951
	[Serializable]
	public enum SystemDataAccessKind
	{
		/// <summary>The method or function does not access system data. </summary>
		// Token: 0x04001B3A RID: 6970
		None,
		/// <summary>The method or function reads system data.</summary>
		// Token: 0x04001B3B RID: 6971
		Read
	}
}
