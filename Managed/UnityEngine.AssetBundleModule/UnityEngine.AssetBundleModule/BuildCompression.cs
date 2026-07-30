using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[UsedByNativeCode]
	[Serializable]
	public struct BuildCompression
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000027B8 File Offset: 0x000009B8
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000027D0 File Offset: 0x000009D0
		public CompressionType compression
		{
			get
			{
				return this._compression;
			}
			private set
			{
				this._compression = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000027DC File Offset: 0x000009DC
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000027F4 File Offset: 0x000009F4
		public CompressionLevel level
		{
			get
			{
				return this._level;
			}
			private set
			{
				this._level = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002818 File Offset: 0x00000A18
		public uint blockSize
		{
			get
			{
				return this._blockSize;
			}
			private set
			{
				this._blockSize = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002822 File Offset: 0x00000A22
		private BuildCompression(CompressionType in_compression, CompressionLevel in_level, uint in_blockSize)
		{
			this = default(BuildCompression);
			this.compression = in_compression;
			this.level = in_level;
			this.blockSize = in_blockSize;
		}

		// Token: 0x0400001C RID: 28
		public static readonly BuildCompression Uncompressed = new BuildCompression(CompressionType.None, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400001D RID: 29
		public static readonly BuildCompression LZ4 = new BuildCompression(CompressionType.Lz4HC, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400001E RID: 30
		public static readonly BuildCompression LZMA = new BuildCompression(CompressionType.Lzma, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400001F RID: 31
		public static readonly BuildCompression UncompressedRuntime = BuildCompression.Uncompressed;

		// Token: 0x04000020 RID: 32
		public static readonly BuildCompression LZ4Runtime = new BuildCompression(CompressionType.Lz4, CompressionLevel.Maximum, 131072U);

		// Token: 0x04000021 RID: 33
		[NativeName("compression")]
		private CompressionType _compression;

		// Token: 0x04000022 RID: 34
		[NativeName("level")]
		private CompressionLevel _level;

		// Token: 0x04000023 RID: 35
		[NativeName("blockSize")]
		private uint _blockSize;
	}
}
