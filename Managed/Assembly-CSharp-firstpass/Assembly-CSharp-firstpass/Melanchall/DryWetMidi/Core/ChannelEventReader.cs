using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000152 RID: 338
	internal sealed class ChannelEventReader : IEventReader
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x0001F744 File Offset: 0x0001D944
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			FourBitNumber head = currentStatusByte.GetHead();
			FourBitNumber tail = currentStatusByte.GetTail();
			ChannelEvent channelEvent;
			switch (head)
			{
			case 8:
				channelEvent = new NoteOffEvent();
				break;
			case 9:
				channelEvent = new NoteOnEvent();
				break;
			case 10:
				channelEvent = new NoteAftertouchEvent();
				break;
			case 11:
				channelEvent = new ControlChangeEvent();
				break;
			case 12:
				channelEvent = new ProgramChangeEvent();
				break;
			case 13:
				channelEvent = new ChannelAftertouchEvent();
				break;
			case 14:
				channelEvent = new PitchBendEvent();
				break;
			default:
				this.ReactOnUnknownChannelEvent(head, tail, reader, settings);
				return null;
			}
			channelEvent.Read(reader, settings, -1);
			channelEvent.Channel = tail;
			if (channelEvent.EventType == MidiEventType.NoteOn)
			{
				NoteOnEvent noteOnEvent = (NoteOnEvent)channelEvent;
				if (settings.SilentNoteOnPolicy == SilentNoteOnPolicy.NoteOff && noteOnEvent.Velocity == 0)
				{
					channelEvent = new NoteOffEvent
					{
						DeltaTime = noteOnEvent.DeltaTime,
						Channel = noteOnEvent.Channel,
						NoteNumber = noteOnEvent.NoteNumber
					};
				}
			}
			return channelEvent;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001F838 File Offset: 0x0001DA38
		private void ReactOnUnknownChannelEvent(FourBitNumber statusByte, FourBitNumber channel, MidiReader reader, ReadingSettings settings)
		{
			switch (settings.UnknownChannelEventPolicy)
			{
			case UnknownChannelEventPolicy.Abort:
				throw new UnknownChannelEventException(statusByte, channel);
			case UnknownChannelEventPolicy.SkipStatusByte:
				return;
			case UnknownChannelEventPolicy.SkipStatusByteAndOneDataByte:
				reader.Position += 1L;
				return;
			case UnknownChannelEventPolicy.SkipStatusByteAndTwoDataBytes:
				reader.Position += 2L;
				return;
			case UnknownChannelEventPolicy.UseCallback:
			{
				UnknownChannelEventCallback unknownChannelEventCallback = settings.UnknownChannelEventCallback;
				if (unknownChannelEventCallback == null)
				{
					throw new InvalidOperationException("Unknown channel event callback is not set.");
				}
				UnknownChannelEventAction unknownChannelEventAction = unknownChannelEventCallback(statusByte, channel);
				UnknownChannelEventInstruction instruction = unknownChannelEventAction.Instruction;
				if (instruction == UnknownChannelEventInstruction.Abort)
				{
					throw new UnknownChannelEventException(statusByte, channel);
				}
				if (instruction != UnknownChannelEventInstruction.SkipData)
				{
					return;
				}
				reader.Position += (long)unknownChannelEventAction.DataBytesToSkipCount;
				return;
			}
			default:
				return;
			}
		}
	}
}
