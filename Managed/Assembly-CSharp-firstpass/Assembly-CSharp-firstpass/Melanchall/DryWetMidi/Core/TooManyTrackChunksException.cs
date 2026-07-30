using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000177 RID: 375
	public sealed class TooManyTrackChunksException : MidiException
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0002076B File Offset: 0x0001E96B
		internal TooManyTrackChunksException(int trackChunksCount)
			: base(string.Format("Count of track chunks to be written ({0}) is greater than the valid maximum ({1}).", trackChunksCount, ushort.MaxValue))
		{
			this.TrackChunksCount = trackChunksCount;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00020794 File Offset: 0x0001E994
		public int TrackChunksCount { get; }
	}
}
