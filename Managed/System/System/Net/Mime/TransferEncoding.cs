using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the Content-Transfer-Encoding header information for an e-mail message attachment.</summary>
	// Token: 0x020005AF RID: 1455
	public enum TransferEncoding
	{
		/// <summary>Encodes data that consists of printable characters in the US-ASCII character set. See RFC 2406 Section 6.7.</summary>
		// Token: 0x04002575 RID: 9589
		QuotedPrintable,
		/// <summary>Encodes stream-based data. See RFC 2406 Section 6.8.</summary>
		// Token: 0x04002576 RID: 9590
		Base64,
		/// <summary>Used for data that is not encoded. The data is in 7-bit US-ASCII characters with a total line length of no longer than 1000 characters. See RFC2406 Section 2.7.</summary>
		// Token: 0x04002577 RID: 9591
		SevenBit,
		/// <summary>The data is in 8-bit characters that may represent international characters with a total line length of no longer than 1000 8-bit characters. For more information about this 8-bit MIME transport extension, see IETF RFC 6152.</summary>
		// Token: 0x04002578 RID: 9592
		EightBit,
		/// <summary>Indicates that the transfer encoding is unknown.</summary>
		// Token: 0x04002579 RID: 9593
		Unknown = -1
	}
}
