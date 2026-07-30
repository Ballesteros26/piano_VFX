using System;

namespace System.Web.SessionState
{
	/// <summary>Identifies whether a session item from a data store is for a session that requires initialization.</summary>
	// Token: 0x020004A0 RID: 1184
	public enum SessionStateActions
	{
		/// <summary>No initialization actions need to be performed by the calling code.</summary>
		// Token: 0x04001D76 RID: 7542
		None,
		/// <summary>The session item from the data store is for a session that requires initialization.</summary>
		// Token: 0x04001D77 RID: 7543
		InitializeItem
	}
}
