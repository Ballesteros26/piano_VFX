using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000105 RID: 261
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x04000CFA RID: 3322
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x04000CFB RID: 3323
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x04000CFC RID: 3324
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x04000CFD RID: 3325
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x04000CFE RID: 3326
		IPv6
	}
}
