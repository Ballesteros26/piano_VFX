using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200017B RID: 379
	public sealed class UnknownChunkException : MidiException
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x00020821 File Offset: 0x0001EA21
		internal UnknownChunkException(string chunkId)
			: base("'" + chunkId + "' chunk ID is unknown.")
		{
			this.ChunkId = chunkId;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00020840 File Offset: 0x0001EA40
		public string ChunkId { get; }
	}
}
