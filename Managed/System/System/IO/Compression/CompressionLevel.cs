using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values that indicate whether a compression operation emphasizes speed or compression size.</summary>
	// Token: 0x02000403 RID: 1027
	public enum CompressionLevel
	{
		/// <summary>The compression operation should be optimally compressed, even if the operation takes a longer time to complete.</summary>
		// Token: 0x04001B72 RID: 7026
		Optimal,
		/// <summary>The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed.</summary>
		// Token: 0x04001B73 RID: 7027
		Fastest,
		/// <summary>No compression should be performed on the file.</summary>
		// Token: 0x04001B74 RID: 7028
		NoCompression
	}
}
