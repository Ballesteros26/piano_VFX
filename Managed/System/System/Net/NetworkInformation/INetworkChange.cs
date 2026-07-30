using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000653 RID: 1619
	internal interface INetworkChange : IDisposable
	{
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003386 RID: 13190
		// (remove) Token: 0x06003387 RID: 13191
		event NetworkAddressChangedEventHandler NetworkAddressChanged;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003388 RID: 13192
		// (remove) Token: 0x06003389 RID: 13193
		event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged;

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600338A RID: 13194
		bool HasRegisteredEvents { get; }
	}
}
