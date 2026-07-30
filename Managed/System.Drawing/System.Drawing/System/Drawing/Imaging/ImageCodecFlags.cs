using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides attributes of an image encoder/decoder (codec).</summary>
	// Token: 0x02000104 RID: 260
	[Flags]
	public enum ImageCodecFlags
	{
		/// <summary>The codec supports encoding (saving).</summary>
		// Token: 0x04000991 RID: 2449
		Encoder = 1,
		/// <summary>The codec supports decoding (reading).</summary>
		// Token: 0x04000992 RID: 2450
		Decoder = 2,
		/// <summary>The codec supports raster images (bitmaps).</summary>
		// Token: 0x04000993 RID: 2451
		SupportBitmap = 4,
		/// <summary>The codec supports vector images (metafiles).</summary>
		// Token: 0x04000994 RID: 2452
		SupportVector = 8,
		/// <summary>The encoder requires a seekable output stream.</summary>
		// Token: 0x04000995 RID: 2453
		SeekableEncode = 16,
		/// <summary>The decoder has blocking behavior during the decoding process.</summary>
		// Token: 0x04000996 RID: 2454
		BlockingDecode = 32,
		/// <summary>The codec is built into GDI+.</summary>
		// Token: 0x04000997 RID: 2455
		Builtin = 65536,
		/// <summary>Not used.</summary>
		// Token: 0x04000998 RID: 2456
		System = 131072,
		/// <summary>Not used.</summary>
		// Token: 0x04000999 RID: 2457
		User = 262144
	}
}
