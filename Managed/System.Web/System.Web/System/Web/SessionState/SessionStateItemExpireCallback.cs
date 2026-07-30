using System;

namespace System.Web.SessionState
{
	/// <summary>Represents the method that handles the <see cref="E:System.Web.SessionState.SessionStateModule.End" /> event of a session-state module.</summary>
	/// <param name="id">The <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" /> parameter of the session that is ending. </param>
	/// <param name="item">A <see cref="T:System.Web.SessionState.SessionStateStoreData" /> object containing the session-state data for the session that is ending. </param>
	// Token: 0x020004A2 RID: 1186
	// (Invoke) Token: 0x060035D5 RID: 13781
	public delegate void SessionStateItemExpireCallback(string id, SessionStateStoreData item);
}
