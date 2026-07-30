using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000117 RID: 279
	public sealed class TrackChunk : MidiChunk
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		public TrackChunk()
			: base("MTrk")
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		public TrackChunk(IEnumerable<MidiEvent> events)
			: this()
		{
			ThrowIfArgument.IsNull("events", events);
			this.Events.AddRange(events);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001CC0B File Offset: 0x0001AE0B
		public TrackChunk(params MidiEvent[] events)
			: this()
		{
			this.Events.AddRange(events);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001CC1F File Offset: 0x0001AE1F
		public EventsCollection Events { get; } = new EventsCollection();

		// Token: 0x0600075F RID: 1887 RVA: 0x0001CC27 File Offset: 0x0001AE27
		public override MidiChunk Clone()
		{
			return new TrackChunk(this.Events.Select((MidiEvent e) => e.Clone()));
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001CC58 File Offset: 0x0001AE58
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			bool useReadingHandlers = settings.UseReadingHandlers;
			if (useReadingHandlers)
			{
				foreach (ReadingHandler readingHandler in settings.TrackChunkReadingHandlers)
				{
					readingHandler.OnStartTrackChunkContentReading(this);
				}
			}
			long num = reader.Position + (long)((ulong)size);
			bool flag = false;
			byte? b = null;
			long num2 = 0L;
			while (reader.Position < num && !reader.EndReached)
			{
				long num3;
				MidiEvent midiEvent = this.ReadEvent(reader, settings, ref b, out num3);
				if (midiEvent is EndOfTrackEvent)
				{
					flag = true;
					break;
				}
				num2 += num3;
				if (midiEvent != null)
				{
					if (useReadingHandlers)
					{
						foreach (ReadingHandler readingHandler2 in settings.EventReadingHandlers)
						{
							readingHandler2.OnFinishEventReading(midiEvent, num2);
						}
					}
					this.Events.Add(midiEvent);
				}
			}
			if (settings.MissedEndOfTrackPolicy == MissedEndOfTrackPolicy.Abort && !flag)
			{
				throw new MissedEndOfTrackEventException();
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001CD68 File Offset: 0x0001AF68
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			this.ProcessEvents(settings, delegate(IEventWriter eventWriter, MidiEvent midiEvent, bool writeStatusByte)
			{
				writer.WriteVlqNumber(midiEvent.DeltaTime);
				eventWriter.Write(midiEvent, writer, settings, writeStatusByte);
			});
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		protected override uint GetContentSize(WritingSettings settings)
		{
			uint result = 0U;
			this.ProcessEvents(settings, delegate(IEventWriter eventWriter, MidiEvent midiEvent, bool writeStatusByte)
			{
				result += (uint)(midiEvent.DeltaTime.GetVlqLength() + eventWriter.CalculateSize(midiEvent, settings, writeStatusByte));
			});
			return result;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		private MidiEvent ReadEvent(MidiReader reader, ReadingSettings settings, ref byte? channelEventStatusByte, out long deltaTime)
		{
			deltaTime = reader.ReadVlqLongNumber();
			if (deltaTime < 0L)
			{
				deltaTime = 0L;
			}
			byte b = reader.ReadByte();
			if (b <= SevenBitNumber.MaxValue)
			{
				if (channelEventStatusByte == null)
				{
					throw new UnexpectedRunningStatusException();
				}
				b = channelEventStatusByte.Value;
				long position = reader.Position;
				reader.Position = position - 1L;
			}
			MidiEvent midiEvent = EventReaderFactory.GetReader(b, true).Read(reader, settings, b);
			if (midiEvent is ChannelEvent)
			{
				channelEventStatusByte = new byte?(b);
			}
			if (midiEvent != null)
			{
				midiEvent.DeltaTime = deltaTime;
			}
			return midiEvent;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001CE74 File Offset: 0x0001B074
		private void ProcessEvents(WritingSettings settings, Action<IEventWriter, MidiEvent, bool> eventHandler)
		{
			byte? b = null;
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			foreach (MidiEvent midiEvent in this.Events)
			{
				if (!(midiEvent is SystemCommonEvent) && !(midiEvent is SystemRealTimeEvent) && (midiEvent.EventType != MidiEventType.UnknownMeta || !settings.CompressionPolicy.HasFlag(CompressionPolicy.DeleteUnknownMetaEvents)))
				{
					if (settings.CompressionPolicy.HasFlag(CompressionPolicy.NoteOffAsSilentNoteOn))
					{
						NoteOffEvent noteOffEvent = midiEvent as NoteOffEvent;
						if (noteOffEvent != null)
						{
							midiEvent = new NoteOnEvent
							{
								DeltaTime = noteOffEvent.DeltaTime,
								Channel = noteOffEvent.Channel,
								NoteNumber = noteOffEvent.NoteNumber
							};
						}
					}
					if ((!settings.CompressionPolicy.HasFlag(CompressionPolicy.DeleteDefaultSetTempo) || !TrackChunk.TrySkipDefaultSetTempo(midiEvent, ref flag)) && (!settings.CompressionPolicy.HasFlag(CompressionPolicy.DeleteDefaultKeySignature) || !TrackChunk.TrySkipDefaultKeySignature(midiEvent, ref flag2)) && (!settings.CompressionPolicy.HasFlag(CompressionPolicy.DeleteDefaultTimeSignature) || !TrackChunk.TrySkipDefaultTimeSignature(midiEvent, ref flag3)))
					{
						IEventWriter writer = EventWriterFactory.GetWriter(midiEvent);
						bool flag4 = true;
						if (midiEvent is ChannelEvent)
						{
							byte statusByte = writer.GetStatusByte(midiEvent);
							byte? b2 = b;
							int? num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
							int num2 = (int)statusByte;
							flag4 = !((num.GetValueOrDefault() == num2) & (num != null)) || !settings.CompressionPolicy.HasFlag(CompressionPolicy.UseRunningStatus);
							b = new byte?(statusByte);
						}
						else
						{
							b = null;
						}
						eventHandler(writer, midiEvent, flag4);
					}
				}
			}
			EndOfTrackEvent endOfTrackEvent = new EndOfTrackEvent();
			IEventWriter writer2 = EventWriterFactory.GetWriter(endOfTrackEvent);
			eventHandler(writer2, endOfTrackEvent, true);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001D09C File Offset: 0x0001B29C
		private static bool TrySkipDefaultSetTempo(MidiEvent midiEvent, ref bool skip)
		{
			if (skip)
			{
				SetTempoEvent setTempoEvent = midiEvent as SetTempoEvent;
				if (setTempoEvent != null)
				{
					if (setTempoEvent.MicrosecondsPerQuarterNote == 500000L)
					{
						return true;
					}
					skip = false;
				}
			}
			return false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		private static bool TrySkipDefaultKeySignature(MidiEvent midiEvent, ref bool skip)
		{
			if (skip)
			{
				KeySignatureEvent keySignatureEvent = midiEvent as KeySignatureEvent;
				if (keySignatureEvent != null)
				{
					if (keySignatureEvent.Key == 0 && keySignatureEvent.Scale == 0)
					{
						return true;
					}
					skip = false;
				}
			}
			return false;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001D100 File Offset: 0x0001B300
		private static bool TrySkipDefaultTimeSignature(MidiEvent midiEvent, ref bool skip)
		{
			if (skip)
			{
				TimeSignatureEvent timeSignatureEvent = midiEvent as TimeSignatureEvent;
				if (timeSignatureEvent != null)
				{
					if (timeSignatureEvent.Numerator == 4 && timeSignatureEvent.Denominator == 4 && timeSignatureEvent.ClocksPerClick == 24 && timeSignatureEvent.ThirtySecondNotesPerBeat == 8)
					{
						return true;
					}
					skip = false;
				}
			}
			return false;
		}

		// Token: 0x04000840 RID: 2112
		public const string Id = "MTrk";
	}
}
