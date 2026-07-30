using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies the session-state mode.</summary>
	// Token: 0x020004A3 RID: 1187
	public enum SessionStateMode
	{
		/// <summary>Session state is disabled.</summary>
		// Token: 0x04001D7A RID: 7546
		Off,
		/// <summary>Session state is in process with an ASP.NET worker process.</summary>
		// Token: 0x04001D7B RID: 7547
		InProc,
		/// <summary>Session state is using the out-of-process ASP.NET State Service to store state information.</summary>
		// Token: 0x04001D7C RID: 7548
		StateServer,
		/// <summary>Session state is using an out-of-process SQL Server database to store state information.</summary>
		// Token: 0x04001D7D RID: 7549
		SQLServer,
		/// <summary>Session state is using a custom data store to store session-state information.</summary>
		// Token: 0x04001D7E RID: 7550
		Custom
	}
}
