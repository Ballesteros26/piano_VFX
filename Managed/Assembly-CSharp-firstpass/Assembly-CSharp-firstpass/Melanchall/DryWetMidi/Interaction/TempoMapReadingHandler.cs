using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000AD RID: 173
	public sealed class TempoMapReadingHandler : ReadingHandler
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00013226 File Offset: 0x00011426
		public TempoMapReadingHandler()
			: base(ReadingHandler.TargetScope.File | ReadingHandler.TargetScope.Event)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001322F File Offset: 0x0001142F
		public TempoMap TempoMap
		{
			get
			{
				if (!this._tempoMapIsReadyForUsage)
				{
					throw new InvalidOperationException("Tempo map is not ready for usage.");
				}
				return this._tempoMap;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001324A File Offset: 0x0001144A
		public override void Initialize()
		{
			this._tempoMapIsReadyForUsage = false;
			this._tempoMap = null;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001325A File Offset: 0x0001145A
		public override void OnFinishHeaderChunkReading(TimeDivision timeDivision)
		{
			this._tempoMap = new TempoMap(timeDivision)
			{
				IsTempoMapReady = false
			};
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001326F File Offset: 0x0001146F
		public override void OnFinishFileReading(MidiFile midiFile)
		{
			this._tempoMap.IsTempoMapReady = true;
			this._tempoMapIsReadyForUsage = true;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013284 File Offset: 0x00011484
		public override void OnFinishEventReading(MidiEvent midiEvent, long absoluteTime)
		{
			MidiEventType eventType = midiEvent.EventType;
			if (eventType == MidiEventType.SetTempo)
			{
				SetTempoEvent setTempoEvent = (SetTempoEvent)midiEvent;
				this._tempoMap.Tempo.SetValue(absoluteTime, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
				return;
			}
			if (eventType != MidiEventType.TimeSignature)
			{
				return;
			}
			TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)midiEvent;
			this._tempoMap.TimeSignature.SetValue(absoluteTime, new TimeSignature((int)timeSignatureEvent.Numerator, (int)timeSignatureEvent.Denominator));
		}

		// Token: 0x0400069F RID: 1695
		private TempoMap _tempoMap;

		// Token: 0x040006A0 RID: 1696
		private bool _tempoMapIsReadyForUsage;
	}
}
