using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows applications to receive notification when the Internet Protocol (IP) address of a network interface, also called a network card or adapter, changes.</summary>
	// Token: 0x02000654 RID: 1620
	public sealed class NetworkChange
	{
		/// <summary>Occurs when the IP address of a network interface changes.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600338B RID: 13195 RVA: 0x000C09C4 File Offset: 0x000BEBC4
		// (remove) Token: 0x0600338C RID: 13196 RVA: 0x000C0A1C File Offset: 0x000BEC1C
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		/// <summary>Occurs when the availability of the network changes.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600338D RID: 13197 RVA: 0x000C0A74 File Offset: 0x000BEC74
		// (remove) Token: 0x0600338E RID: 13198 RVA: 0x000C0ACC File Offset: 0x000BECCC
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000C0B24 File Offset: 0x000BED24
		private static void MaybeCreate()
		{
			if (NetworkChange.networkChange != null)
			{
				return;
			}
			try
			{
				NetworkChange.networkChange = new MacNetworkChange();
			}
			catch
			{
				NetworkChange.networkChange = new LinuxNetworkChange();
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000C0B64 File Offset: 0x000BED64
		private static void MaybeDispose()
		{
			if (NetworkChange.networkChange != null && NetworkChange.networkChange.HasRegisteredEvents)
			{
				NetworkChange.networkChange.Dispose();
				NetworkChange.networkChange = null;
			}
		}

		/// <summary>Registers a network change instance to receive network change events.</summary>
		/// <param name="nc">The instance to register. </param>
		// Token: 0x06003392 RID: 13202 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterNetworkChange(NetworkChange nc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400290E RID: 10510
		private static INetworkChange networkChange;
	}
}
