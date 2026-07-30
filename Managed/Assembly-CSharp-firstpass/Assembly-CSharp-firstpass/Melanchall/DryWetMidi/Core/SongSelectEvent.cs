using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200015F RID: 351
	public sealed class SongSelectEvent : SystemCommonEvent
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00020093 File Offset: 0x0001E293
		public SongSelectEvent()
			: base(MidiEventType.SongSelect)
		{
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002009D File Offset: 0x0001E29D
		public SongSelectEvent(SevenBitNumber number)
			: this()
		{
			this.Number = number;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000200AC File Offset: 0x0001E2AC
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x000200B4 File Offset: 0x0001E2B4
		public SevenBitNumber Number { get; set; }

		// Token: 0x060008EF RID: 2287 RVA: 0x000200C0 File Offset: 0x0001E2C0
		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			byte b = reader.ReadByte();
			if (b > SevenBitNumber.MaxValue)
			{
				InvalidSystemCommonEventParameterValuePolicy invalidSystemCommonEventParameterValuePolicy = settings.InvalidSystemCommonEventParameterValuePolicy;
				if (invalidSystemCommonEventParameterValuePolicy == InvalidSystemCommonEventParameterValuePolicy.Abort)
				{
					throw new InvalidSystemCommonEventParameterValueException(base.GetType(), "Number", (int)b);
				}
				if (invalidSystemCommonEventParameterValuePolicy == InvalidSystemCommonEventParameterValuePolicy.SnapToLimits)
				{
					b = SevenBitNumber.MaxValue;
				}
			}
			this.Number = (SevenBitNumber)b;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002011A File Offset: 0x0001E31A
		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(this.Number);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00003941 File Offset: 0x00001B41
		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002012D File Offset: 0x0001E32D
		protected override MidiEvent CloneEvent()
		{
			return new SongSelectEvent(this.Number);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002013A File Offset: 0x0001E33A
		public override string ToString()
		{
			return string.Format("Song Number ({0})", this.Number);
		}
	}
}
