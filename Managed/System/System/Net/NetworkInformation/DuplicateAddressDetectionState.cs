using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the current state of an IP address.</summary>
	// Token: 0x020005F4 RID: 1524
	public enum DuplicateAddressDetectionState
	{
		/// <summary>The address is not valid. A nonvalid address is expired and no longer assigned to an interface; applications should not send data packets to it.</summary>
		// Token: 0x040027A0 RID: 10144
		Invalid,
		/// <summary>The duplicate address detection procedure's evaluation of the address has not completed successfully. Applications should not use the address because it is not yet valid and packets sent to it are discarded.</summary>
		// Token: 0x040027A1 RID: 10145
		Tentative,
		/// <summary>The address is not unique. This address should not be assigned to the network interface.</summary>
		// Token: 0x040027A2 RID: 10146
		Duplicate,
		/// <summary>The address is valid, but it is nearing its lease lifetime and should not be used by applications.</summary>
		// Token: 0x040027A3 RID: 10147
		Deprecated,
		/// <summary>The address is valid and its use is unrestricted.</summary>
		// Token: 0x040027A4 RID: 10148
		Preferred
	}
}
