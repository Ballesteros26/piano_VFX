using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016F RID: 367
	public sealed class InvalidChunkSizeException : MidiException
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x0002061F File Offset: 0x0001E81F
		internal InvalidChunkSizeException(Type chunkType, long expectedSize, long actualSize)
			: base(string.Format("Actual size ({0}) of a chunk of {1} type differs from the one declared in the chunk's header ({2}).", actualSize, chunkType, expectedSize))
		{
			this.ChunkType = chunkType;
			this.ExpectedSize = expectedSize;
			this.ActualSize = actualSize;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00020653 File Offset: 0x0001E853
		public Type ChunkType { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002065B File Offset: 0x0001E85B
		public long ExpectedSize { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00020663 File Offset: 0x0001E863
		public long ActualSize { get; }
	}
}
