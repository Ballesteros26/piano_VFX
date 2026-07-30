using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x02000612 RID: 1554
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x040027F2 RID: 10226
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x040027F3 RID: 10227
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x040027F4 RID: 10228
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x040027F5 RID: 10229
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x040027F6 RID: 10230
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x040027F7 RID: 10231
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x040027F8 RID: 10232
		LowerLayerDown
	}
}
