using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address network prefix was located.</summary>
	// Token: 0x02000616 RID: 1558
	public enum PrefixOrigin
	{
		/// <summary>The prefix was located using an unspecified source.</summary>
		// Token: 0x04002806 RID: 10246
		Other,
		/// <summary>The prefix was manually configured.</summary>
		// Token: 0x04002807 RID: 10247
		Manual,
		/// <summary>The prefix is a well-known prefix. Well-known prefixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such prefixes are reserved for special purposes.</summary>
		// Token: 0x04002808 RID: 10248
		WellKnown,
		/// <summary>The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04002809 RID: 10249
		Dhcp,
		/// <summary>The prefix was supplied by a router advertisement.</summary>
		// Token: 0x0400280A RID: 10250
		RouterAdvertisement
	}
}
