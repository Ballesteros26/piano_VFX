using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200015C RID: 348
	public sealed class MidiTimeCodeEvent : SystemCommonEvent
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0001FD78 File Offset: 0x0001DF78
		public MidiTimeCodeEvent()
			: base(MidiEventType.MidiTimeCode)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public MidiTimeCodeEvent(MidiTimeCodeComponent component, FourBitNumber componentValue)
			: this()
		{
			ThrowIfArgument.IsInvalidEnumValue<MidiTimeCodeComponent>("component", component);
			byte b = MidiTimeCodeEvent.ComponentValueMasks[component];
			ThrowIfArgument.IsGreaterThan("componentValue", (int)componentValue, (int)b, string.Format("Component's value is greater than maximum valid one which is {0}.", b));
			this.Component = component;
			this.ComponentValue = componentValue;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001FDDD File Offset: 0x0001DFDD
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0001FDE5 File Offset: 0x0001DFE5
		public MidiTimeCodeComponent Component { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001FDEE File Offset: 0x0001DFEE
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0001FDF6 File Offset: 0x0001DFF6
		public FourBitNumber ComponentValue { get; set; }

		// Token: 0x060008DB RID: 2267 RVA: 0x0001FE00 File Offset: 0x0001E000
		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			byte b = reader.ReadByte();
			byte b2 = b.GetHead();
			if (!Enum.IsDefined(typeof(MidiTimeCodeComponent), b2))
			{
				throw new InvalidMidiTimeCodeComponentException(b2);
			}
			this.Component = (MidiTimeCodeComponent)b2;
			FourBitNumber fourBitNumber = b.GetTail();
			if (fourBitNumber > MidiTimeCodeEvent.ComponentValueMasks[this.Component])
			{
				InvalidSystemCommonEventParameterValuePolicy invalidSystemCommonEventParameterValuePolicy = settings.InvalidSystemCommonEventParameterValuePolicy;
				if (invalidSystemCommonEventParameterValuePolicy == InvalidSystemCommonEventParameterValuePolicy.Abort)
				{
					throw new InvalidSystemCommonEventParameterValueException(base.GetType(), string.Format("{0} (component is {1})", "ComponentValue", this.Component), (int)fourBitNumber);
				}
				if (invalidSystemCommonEventParameterValuePolicy == InvalidSystemCommonEventParameterValuePolicy.SnapToLimits)
				{
					fourBitNumber = (FourBitNumber)MidiTimeCodeEvent.ComponentValueMasks[this.Component];
				}
			}
			this.ComponentValue = fourBitNumber;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			MidiTimeCodeComponent component = this.Component;
			byte b = MidiTimeCodeEvent.ComponentValueMasks[component];
			int num = (int)(this.ComponentValue & b);
			byte b2 = DataTypesUtilities.Combine((FourBitNumber)((byte)component), (FourBitNumber)((byte)num));
			writer.WriteByte(b2);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00003941 File Offset: 0x00001B41
		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001FF04 File Offset: 0x0001E104
		protected override MidiEvent CloneEvent()
		{
			return new MidiTimeCodeEvent(this.Component, this.ComponentValue);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001FF17 File Offset: 0x0001E117
		public override string ToString()
		{
			return string.Format("MIDI Time Code ({0}, {1})", this.Component, this.ComponentValue);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001FF3C File Offset: 0x0001E13C
		// Note: this type is marked as 'beforefieldinit'.
		static MidiTimeCodeEvent()
		{
			Dictionary<MidiTimeCodeComponent, byte> dictionary = new Dictionary<MidiTimeCodeComponent, byte>();
			dictionary[MidiTimeCodeComponent.FramesLsb] = 15;
			dictionary[MidiTimeCodeComponent.FramesMsb] = 1;
			dictionary[MidiTimeCodeComponent.SecondsLsb] = 15;
			dictionary[MidiTimeCodeComponent.SecondsMsb] = 3;
			dictionary[MidiTimeCodeComponent.MinutesLsb] = 15;
			dictionary[MidiTimeCodeComponent.MinutesMsb] = 3;
			dictionary[MidiTimeCodeComponent.HoursLsb] = 15;
			dictionary[MidiTimeCodeComponent.HoursMsbAndTimeCodeType] = 7;
			MidiTimeCodeEvent.ComponentValueMasks = dictionary;
		}

		// Token: 0x040008C3 RID: 2243
		private static readonly Dictionary<MidiTimeCodeComponent, byte> ComponentValueMasks;
	}
}
