using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x020005F5 RID: 1525
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Get the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060030AE RID: 12462
		public abstract IPAddress Address { get; }
	}
}
