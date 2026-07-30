using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x02000618 RID: 1560
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x04002815 RID: 10261
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x04002816 RID: 10262
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x04002817 RID: 10263
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x04002818 RID: 10264
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x04002819 RID: 10265
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x0400281A RID: 10266
		Random
	}
}
