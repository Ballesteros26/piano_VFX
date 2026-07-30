using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000112 RID: 274
	internal sealed class HeaderChunk : MidiChunk
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0001C8D6 File Offset: 0x0001AAD6
		internal HeaderChunk()
			: base("MThd")
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001C8E3 File Offset: 0x0001AAE3
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001C8EB File Offset: 0x0001AAEB
		public ushort FileFormat { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public TimeDivision TimeDivision { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001C905 File Offset: 0x0001AB05
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001C90D File Offset: 0x0001AB0D
		public ushort TracksNumber { get; set; }

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C916 File Offset: 0x0001AB16
		public override MidiChunk Clone()
		{
			throw new NotSupportedException("Cloning of a header chunk isnot supported.");
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001C924 File Offset: 0x0001AB24
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			ushort num = reader.ReadWord();
			if (settings.UnknownFileFormatPolicy == UnknownFileFormatPolicy.Abort && !Enum.IsDefined(typeof(MidiFileFormat), num))
			{
				throw new UnknownFileFormatException(num);
			}
			this.FileFormat = num;
			this.TracksNumber = reader.ReadWord();
			this.TimeDivision = TimeDivisionFactory.GetTimeDivision(reader.ReadInt16());
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C983 File Offset: 0x0001AB83
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteWord(this.FileFormat);
			writer.WriteWord(this.TracksNumber);
			writer.WriteInt16(this.TimeDivision.ToInt16());
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C9AE File Offset: 0x0001ABAE
		protected override uint GetContentSize(WritingSettings settings)
		{
			return 6U;
		}

		// Token: 0x04000835 RID: 2101
		public const string Id = "MThd";
	}
}
