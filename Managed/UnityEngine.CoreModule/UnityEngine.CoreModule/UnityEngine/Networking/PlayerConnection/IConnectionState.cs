using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002FA RID: 762
	[MovedFrom("UnityEngine.Experimental.Networking.PlayerConnection")]
	public interface IConnectionState : IDisposable
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001A69 RID: 6761
		ConnectionTarget connectedToTarget { get; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001A6A RID: 6762
		string connectionName { get; }
	}
}
