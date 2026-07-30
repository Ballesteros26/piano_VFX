using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers used to represent the type of a session switch event.</summary>
	// Token: 0x020000D6 RID: 214
	public enum SessionSwitchReason
	{
		/// <summary>A session has been connected from the console.</summary>
		// Token: 0x04000B92 RID: 2962
		ConsoleConnect = 1,
		/// <summary>A session has been disconnected from the console.</summary>
		// Token: 0x04000B93 RID: 2963
		ConsoleDisconnect,
		/// <summary>A session has been connected from a remote connection.</summary>
		// Token: 0x04000B94 RID: 2964
		RemoteConnect,
		/// <summary>A session has been disconnected from a remote connection.</summary>
		// Token: 0x04000B95 RID: 2965
		RemoteDisconnect,
		/// <summary>A user has logged on to a session.</summary>
		// Token: 0x04000B96 RID: 2966
		SessionLogon,
		/// <summary>A user has logged off from a session.</summary>
		// Token: 0x04000B97 RID: 2967
		SessionLogoff,
		/// <summary>A session has been locked.</summary>
		// Token: 0x04000B98 RID: 2968
		SessionLock,
		/// <summary>A session has been unlocked.</summary>
		// Token: 0x04000B99 RID: 2969
		SessionUnlock,
		/// <summary>A session has changed its status to or from remote controlled mode.</summary>
		// Token: 0x04000B9A RID: 2970
		SessionRemoteControl
	}
}
