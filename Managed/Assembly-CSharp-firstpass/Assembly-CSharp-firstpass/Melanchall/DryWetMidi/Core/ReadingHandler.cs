using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200018B RID: 395
	public abstract class ReadingHandler
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x000217F7 File Offset: 0x0001F9F7
		public ReadingHandler(ReadingHandler.TargetScope scope)
		{
			this.Scope = scope;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00021806 File Offset: 0x0001FA06
		public ReadingHandler.TargetScope Scope { get; }

		// Token: 0x06000990 RID: 2448 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void Initialize()
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnStartFileReading()
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnFinishFileReading(MidiFile midiFile)
		{
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnFinishHeaderChunkReading(TimeDivision timeDivision)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnStartTrackChunkReading()
		{
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnStartTrackChunkContentReading(TrackChunk trackChunk)
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnFinishTrackChunkReading(TrackChunk trackChunk)
		{
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00002994 File Offset: 0x00000B94
		public virtual void OnFinishEventReading(MidiEvent midiEvent, long absoluteTime)
		{
		}

		// Token: 0x0200029D RID: 669
		[Flags]
		public enum TargetScope
		{
			// Token: 0x04000DE7 RID: 3559
			File = 1,
			// Token: 0x04000DE8 RID: 3560
			TrackChunk = 2,
			// Token: 0x04000DE9 RID: 3561
			Event = 4
		}
	}
}
