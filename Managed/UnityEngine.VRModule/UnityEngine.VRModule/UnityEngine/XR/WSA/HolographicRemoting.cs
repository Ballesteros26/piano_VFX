using System;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000020 RID: 32
	public class HolographicRemoting
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002750 File Offset: 0x00000950
		public static HolographicStreamerConnectionState ConnectionState
		{
			get
			{
				return HolographicStreamerConnectionState.Disconnected;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002763 File Offset: 0x00000963
		public static void Connect(string clientName, int maxBitRate = 9999)
		{
			HolographicRemoting.Connect(clientName, maxBitRate, RemoteDeviceVersion.V1);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002737 File Offset: 0x00000937
		public static void Connect(string clientName, int maxBitRate, RemoteDeviceVersion deviceVersion)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002737 File Offset: 0x00000937
		public static void Disconnect()
		{
		}
	}
}
