using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B16 RID: 2838
	internal struct ManifestEnvelope
	{
		// Token: 0x040032CF RID: 13007
		public const int MaxChunkSize = 65280;

		// Token: 0x040032D0 RID: 13008
		public ManifestEnvelope.ManifestFormats Format;

		// Token: 0x040032D1 RID: 13009
		public byte MajorVersion;

		// Token: 0x040032D2 RID: 13010
		public byte MinorVersion;

		// Token: 0x040032D3 RID: 13011
		public byte Magic;

		// Token: 0x040032D4 RID: 13012
		public ushort TotalChunks;

		// Token: 0x040032D5 RID: 13013
		public ushort ChunkNumber;

		// Token: 0x02000B17 RID: 2839
		public enum ManifestFormats : byte
		{
			// Token: 0x040032D7 RID: 13015
			SimpleXmlFormat = 1
		}
	}
}
