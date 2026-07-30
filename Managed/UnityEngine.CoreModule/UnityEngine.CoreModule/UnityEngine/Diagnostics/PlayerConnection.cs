using System;
using System.ComponentModel;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.Diagnostics
{
	// Token: 0x020003B1 RID: 945
	public static class PlayerConnection
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0003801C File Offset: 0x0003621C
		[Obsolete("Use UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected instead.")]
		public static bool connected
		{
			get
			{
				return PlayerConnection.instance.isConnected;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00002EC3 File Offset: 0x000010C3
		[EditorBrowsable(1)]
		[Obsolete("PlayerConnection.SendFile is no longer supported.", true)]
		public static void SendFile(string remoteFilePath, byte[] data)
		{
		}
	}
}
