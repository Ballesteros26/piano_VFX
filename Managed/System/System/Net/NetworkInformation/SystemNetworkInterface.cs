using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200065C RID: 1628
	internal static class SystemNetworkInterface
	{
		// Token: 0x060033C6 RID: 13254 RVA: 0x000C13C4 File Offset: 0x000BF5C4
		public static NetworkInterface[] GetNetworkInterfaces()
		{
			NetworkInterface[] array;
			try
			{
				array = SystemNetworkInterface.nif.GetAllNetworkInterfaces();
			}
			catch
			{
				array = new NetworkInterface[0];
			}
			return array;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000027E2 File Offset: 0x000009E2
		public static bool InternalGetIsNetworkAvailable()
		{
			return true;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000C13FC File Offset: 0x000BF5FC
		public static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.nif.GetLoopbackInterfaceIndex();
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x00004239 File Offset: 0x00002439
		public static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000C1408 File Offset: 0x000BF608
		public static IPAddress GetNetMask(IPAddress address)
		{
			return SystemNetworkInterface.nif.GetNetMask(address);
		}

		// Token: 0x04002937 RID: 10551
		private static readonly NetworkInterfaceFactory nif = NetworkInterfaceFactory.Create();
	}
}
