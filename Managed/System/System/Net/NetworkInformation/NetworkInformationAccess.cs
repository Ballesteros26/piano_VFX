using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x0200060D RID: 1549
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x040027E7 RID: 10215
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x040027E8 RID: 10216
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x040027E9 RID: 10217
		Ping = 4
	}
}
