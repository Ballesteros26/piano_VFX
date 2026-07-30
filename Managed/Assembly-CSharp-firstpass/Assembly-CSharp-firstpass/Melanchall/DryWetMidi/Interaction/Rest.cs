using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A3 RID: 163
	public sealed class Rest : ILengthedObject, ITimedObject
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00011FF8 File Offset: 0x000101F8
		internal Rest(long time, long length, FourBitNumber? channel, SevenBitNumber? noteNumber)
		{
			this.Time = time;
			this.Length = length;
			this.Channel = channel;
			this.NoteNumber = noteNumber;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001201D File Offset: 0x0001021D
		public long Time { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00012025 File Offset: 0x00010225
		public long Length { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001202D File Offset: 0x0001022D
		public FourBitNumber? Channel { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00012035 File Offset: 0x00010235
		public SevenBitNumber? NoteNumber { get; }

		// Token: 0x06000394 RID: 916 RVA: 0x00012040 File Offset: 0x00010240
		public static bool operator ==(Rest rest1, Rest rest2)
		{
			if (rest1 == rest2)
			{
				return true;
			}
			if (rest1 == null || rest2 == null)
			{
				return false;
			}
			if (rest1.Time == rest2.Time && rest1.Length == rest2.Length)
			{
				FourBitNumber? fourBitNumber = rest1.Channel;
				int? num = ((fourBitNumber != null) ? new int?((int)fourBitNumber.GetValueOrDefault()) : null);
				fourBitNumber = rest2.Channel;
				int? num2 = ((fourBitNumber != null) ? new int?((int)fourBitNumber.GetValueOrDefault()) : null);
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					SevenBitNumber? sevenBitNumber = rest1.NoteNumber;
					num2 = ((sevenBitNumber != null) ? new int?((int)sevenBitNumber.GetValueOrDefault()) : null);
					sevenBitNumber = rest2.NoteNumber;
					num = ((sevenBitNumber != null) ? new int?((int)sevenBitNumber.GetValueOrDefault()) : null);
					return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
				}
			}
			return false;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00012179 File Offset: 0x00010379
		public static bool operator !=(Rest rest1, Rest rest2)
		{
			return !(rest1 == rest2);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00012185 File Offset: 0x00010385
		public override string ToString()
		{
			return string.Format("Rest (channel = {0}, note number = {1})", this.Channel, this.NoteNumber);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000121A7 File Offset: 0x000103A7
		public override bool Equals(object obj)
		{
			return this == obj as Rest;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000121B8 File Offset: 0x000103B8
		public override int GetHashCode()
		{
			return (((17 * 23 + this.Time.GetHashCode()) * 23 + this.Length.GetHashCode()) * 23 + this.Channel.GetHashCode()) * 23 + this.NoteNumber.GetHashCode();
		}
	}
}
