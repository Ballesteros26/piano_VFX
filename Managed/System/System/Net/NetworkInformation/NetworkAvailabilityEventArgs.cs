using System;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x02000609 RID: 1545
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x000BD926 File Offset: 0x000BBB26
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>true if the network is available; otherwise, false.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000BD935 File Offset: 0x000BBB35
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal NetworkAvailabilityEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040027E5 RID: 10213
		private bool isAvailable;
	}
}
