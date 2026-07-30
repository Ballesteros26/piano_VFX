using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200015E RID: 350
	public sealed class SongPositionPointerEvent : SystemCommonEvent
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0001FF97 File Offset: 0x0001E197
		public SongPositionPointerEvent()
			: base(MidiEventType.SongPositionPointer)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001FFA1 File Offset: 0x0001E1A1
		public SongPositionPointerEvent(ushort pointerValue)
			: this()
		{
			this.PointerValue = pointerValue;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0001FFC3 File Offset: 0x0001E1C3
		public ushort PointerValue
		{
			get
			{
				return DataTypesUtilities.Combine(this._msb, this._lsb);
			}
			set
			{
				this._msb = value.GetHead();
				this._lsb = value.GetTail();
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001FFDD File Offset: 0x0001E1DD
		private SevenBitNumber ProcessValue(byte value, string property, InvalidSystemCommonEventParameterValuePolicy policy)
		{
			if (value > SevenBitNumber.MaxValue)
			{
				if (policy == InvalidSystemCommonEventParameterValuePolicy.Abort)
				{
					throw new InvalidSystemCommonEventParameterValueException(base.GetType(), property, (int)value);
				}
				if (policy == InvalidSystemCommonEventParameterValuePolicy.SnapToLimits)
				{
					return SevenBitNumber.MaxValue;
				}
			}
			return (SevenBitNumber)value;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002000F File Offset: 0x0001E20F
		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			this._lsb = this.ProcessValue(reader.ReadByte(), "LSB", settings.InvalidSystemCommonEventParameterValuePolicy);
			this._msb = this.ProcessValue(reader.ReadByte(), "MSB", settings.InvalidSystemCommonEventParameterValuePolicy);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002004B File Offset: 0x0001E24B
		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(this._lsb);
			writer.WriteByte(this._msb);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001EF45 File Offset: 0x0001D145
		internal override int GetSize(WritingSettings settings)
		{
			return 2;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002006F File Offset: 0x0001E26F
		protected override MidiEvent CloneEvent()
		{
			return new SongPositionPointerEvent(this.PointerValue);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002007C File Offset: 0x0001E27C
		public override string ToString()
		{
			return string.Format("Song Position Pointer ({0})", this.PointerValue);
		}

		// Token: 0x040008CB RID: 2251
		private SevenBitNumber _lsb;

		// Token: 0x040008CC RID: 2252
		private SevenBitNumber _msb;
	}
}
