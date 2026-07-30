using System;

namespace System.Net
{
	/// <summary>Represents the file compression and decompression encoding format to be used to compress the data received in response to an <see cref="T:System.Net.HttpWebRequest" />.</summary>
	// Token: 0x02000502 RID: 1282
	[Flags]
	public enum DecompressionMethods
	{
		/// <summary>Do not use compression.</summary>
		// Token: 0x04002103 RID: 8451
		None = 0,
		/// <summary>Use the gZip compression-decompression algorithm.</summary>
		// Token: 0x04002104 RID: 8452
		GZip = 1,
		/// <summary>Use the deflate compression-decompression algorithm.</summary>
		// Token: 0x04002105 RID: 8453
		Deflate = 2
	}
}
