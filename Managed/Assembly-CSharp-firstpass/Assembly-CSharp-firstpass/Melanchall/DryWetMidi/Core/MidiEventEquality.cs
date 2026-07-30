using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000123 RID: 291
	internal static class MidiEventEquality
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, MidiEventEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiEvent1 == midiEvent2)
			{
				return true;
			}
			if (midiEvent1 == null || midiEvent2 == null)
			{
				message = "One of events is null.";
				return false;
			}
			if (settings.CompareDeltaTimes)
			{
				long deltaTime = midiEvent1.DeltaTime;
				long deltaTime2 = midiEvent2.DeltaTime;
				if (deltaTime != deltaTime2)
				{
					message = string.Format("Delta-times are different ({0} vs {1}).", deltaTime, deltaTime2);
					return false;
				}
			}
			Type type = midiEvent1.GetType();
			Type type2 = midiEvent2.GetType();
			if (type != type2)
			{
				message = string.Format("Types of events are different ({0} vs {1}).", type, type2);
				return false;
			}
			if (midiEvent1 is SystemRealTimeEvent)
			{
				return true;
			}
			ChannelEvent channelEvent = midiEvent1 as ChannelEvent;
			if (channelEvent != null)
			{
				ChannelEvent channelEvent2 = (ChannelEvent)midiEvent2;
				if (!ArrayUtilities.Equals<byte>(channelEvent._parameters, channelEvent2._parameters))
				{
					message = "Channel events parameters are different.";
					return false;
				}
				FourBitNumber channel = channelEvent.Channel;
				FourBitNumber channel2 = channelEvent2.Channel;
				if (channel != channel2)
				{
					message = string.Format("Channel events parameters are different ({0} vs {1}).", channel, channel2);
					return false;
				}
				return true;
			}
			else
			{
				SysExEvent sysExEvent = midiEvent1 as SysExEvent;
				if (sysExEvent != null)
				{
					SysExEvent sysExEvent2 = (SysExEvent)midiEvent2;
					bool completed = sysExEvent.Completed;
					bool completed2 = sysExEvent2.Completed;
					if (completed != completed2)
					{
						message = string.Format("'Completed' state of system exclusive events are different ({0} vs {1}).", completed, completed2);
						return false;
					}
					if (!ArrayUtilities.Equals<byte>(sysExEvent.Data, sysExEvent2.Data))
					{
						message = "System exclusive events data are different.";
						return false;
					}
					return true;
				}
				else
				{
					SequencerSpecificEvent sequencerSpecificEvent = midiEvent1 as SequencerSpecificEvent;
					if (sequencerSpecificEvent != null)
					{
						SequencerSpecificEvent sequencerSpecificEvent2 = (SequencerSpecificEvent)midiEvent2;
						if (!ArrayUtilities.Equals<byte>(sequencerSpecificEvent.Data, sequencerSpecificEvent2.Data))
						{
							message = "Sequencer specific events data are different.";
							return false;
						}
						return true;
					}
					else
					{
						UnknownMetaEvent unknownMetaEvent = midiEvent1 as UnknownMetaEvent;
						if (unknownMetaEvent != null)
						{
							UnknownMetaEvent unknownMetaEvent2 = (UnknownMetaEvent)midiEvent2;
							byte statusByte = unknownMetaEvent.StatusByte;
							byte statusByte2 = unknownMetaEvent2.StatusByte;
							if (statusByte != statusByte2)
							{
								message = string.Format("Unknown meta events status bytes are different ({0} vs {1}).", statusByte, statusByte2);
								return false;
							}
							if (!ArrayUtilities.Equals<byte>(unknownMetaEvent.Data, unknownMetaEvent2.Data))
							{
								message = "Unknown meta events data are different.";
								return false;
							}
							return true;
						}
						else
						{
							BaseTextEvent baseTextEvent = midiEvent1 as BaseTextEvent;
							if (baseTextEvent != null)
							{
								BaseTextEvent baseTextEvent2 = (BaseTextEvent)midiEvent2;
								string text = baseTextEvent.Text;
								string text2 = baseTextEvent2.Text;
								if (!string.Equals(text, text2, settings.TextComparison))
								{
									message = string.Concat(new string[] { "Meta events texts are different (", text, " vs ", text2, ")." });
									return false;
								}
								return true;
							}
							else
							{
								Func<MidiEvent, MidiEvent, bool> func;
								if (MidiEventEquality.Comparers.TryGetValue(midiEvent1.EventType, out func))
								{
									return func(midiEvent1, midiEvent2);
								}
								return midiEvent1.Equals(midiEvent2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001DEEC File Offset: 0x0001C0EC
		// Note: this type is marked as 'beforefieldinit'.
		static MidiEventEquality()
		{
			Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>> dictionary = new Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>>();
			dictionary[MidiEventType.ChannelPrefix] = (MidiEvent e1, MidiEvent e2) => ((ChannelPrefixEvent)e1).Channel == ((ChannelPrefixEvent)e2).Channel;
			dictionary[MidiEventType.KeySignature] = delegate(MidiEvent e1, MidiEvent e2)
			{
				KeySignatureEvent keySignatureEvent = (KeySignatureEvent)e1;
				KeySignatureEvent keySignatureEvent2 = (KeySignatureEvent)e2;
				return keySignatureEvent.Key == keySignatureEvent2.Key && keySignatureEvent.Scale == keySignatureEvent2.Scale;
			};
			dictionary[MidiEventType.PortPrefix] = (MidiEvent e1, MidiEvent e2) => ((PortPrefixEvent)e1).Port == ((PortPrefixEvent)e2).Port;
			dictionary[MidiEventType.SequenceNumber] = (MidiEvent e1, MidiEvent e2) => ((SequenceNumberEvent)e1).Number == ((SequenceNumberEvent)e2).Number;
			dictionary[MidiEventType.SetTempo] = (MidiEvent e1, MidiEvent e2) => ((SetTempoEvent)e1).MicrosecondsPerQuarterNote == ((SetTempoEvent)e2).MicrosecondsPerQuarterNote;
			dictionary[MidiEventType.SmpteOffset] = delegate(MidiEvent e1, MidiEvent e2)
			{
				SmpteOffsetEvent smpteOffsetEvent = (SmpteOffsetEvent)e1;
				SmpteOffsetEvent smpteOffsetEvent2 = (SmpteOffsetEvent)e2;
				return smpteOffsetEvent.Hours == smpteOffsetEvent2.Hours && smpteOffsetEvent.Minutes == smpteOffsetEvent2.Minutes && smpteOffsetEvent.Seconds == smpteOffsetEvent2.Seconds && smpteOffsetEvent.Frames == smpteOffsetEvent2.Frames && smpteOffsetEvent.SubFrames == smpteOffsetEvent2.SubFrames;
			};
			dictionary[MidiEventType.TimeSignature] = delegate(MidiEvent e1, MidiEvent e2)
			{
				TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)e1;
				TimeSignatureEvent timeSignatureEvent2 = (TimeSignatureEvent)e2;
				return timeSignatureEvent.Numerator == timeSignatureEvent2.Numerator && timeSignatureEvent.Denominator == timeSignatureEvent2.Denominator && timeSignatureEvent.ClocksPerClick == timeSignatureEvent2.ClocksPerClick && timeSignatureEvent.ThirtySecondNotesPerBeat == timeSignatureEvent2.ThirtySecondNotesPerBeat;
			};
			dictionary[MidiEventType.EndOfTrack] = (MidiEvent e1, MidiEvent e2) => true;
			dictionary[MidiEventType.MidiTimeCode] = delegate(MidiEvent e1, MidiEvent e2)
			{
				MidiTimeCodeEvent midiTimeCodeEvent = (MidiTimeCodeEvent)e1;
				MidiTimeCodeEvent midiTimeCodeEvent2 = (MidiTimeCodeEvent)e2;
				return midiTimeCodeEvent.Component == midiTimeCodeEvent2.Component && midiTimeCodeEvent.ComponentValue == midiTimeCodeEvent2.ComponentValue;
			};
			dictionary[MidiEventType.SongPositionPointer] = (MidiEvent e1, MidiEvent e2) => ((SongPositionPointerEvent)e1).PointerValue == ((SongPositionPointerEvent)e2).PointerValue;
			dictionary[MidiEventType.SongSelect] = (MidiEvent e1, MidiEvent e2) => ((SongSelectEvent)e1).Number == ((SongSelectEvent)e2).Number;
			dictionary[MidiEventType.TuneRequest] = (MidiEvent e1, MidiEvent e2) => true;
			MidiEventEquality.Comparers = dictionary;
		}

		// Token: 0x04000848 RID: 2120
		private static readonly Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>> Comparers;
	}
}
