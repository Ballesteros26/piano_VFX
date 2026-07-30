using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200019F RID: 415
	internal sealed class SmpteData
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x0002200C File Offset: 0x0002020C
		public SmpteData()
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002201C File Offset: 0x0002021C
		public SmpteData(SmpteFormat format, byte hours, byte minutes, byte seconds, byte frames, byte subFrames)
		{
			this.Format = format;
			this.Hours = hours;
			this.Minutes = minutes;
			this.Seconds = seconds;
			this.Frames = frames;
			this.SubFrames = subFrames;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00022059 File Offset: 0x00020259
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00022061 File Offset: 0x00020261
		public SmpteFormat Format
		{
			get
			{
				return this._format;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<SmpteFormat>("value", value);
				this._format = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00022075 File Offset: 0x00020275
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002207D File Offset: 0x0002027D
		public byte Hours
		{
			get
			{
				return this._hours;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", (int)value, 23, string.Format("Hours number is out of valid range (0-{0}).", 23));
				this._hours = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000220A4 File Offset: 0x000202A4
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x000220AC File Offset: 0x000202AC
		public byte Minutes
		{
			get
			{
				return this._minutes;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", (int)value, 59, string.Format("Minutes number is out of valid range (0-{0}).", 59));
				this._minutes = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000220D3 File Offset: 0x000202D3
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x000220DB File Offset: 0x000202DB
		public byte Seconds
		{
			get
			{
				return this._seconds;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", (int)value, 59, string.Format("Seconds number is out of valid range (0-{0}).", 59));
				this._seconds = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00022102 File Offset: 0x00020302
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0002210C File Offset: 0x0002030C
		public byte Frames
		{
			get
			{
				return this._frames;
			}
			set
			{
				byte b = SmpteData.MaxFrames[this.Format];
				ThrowIfArgument.IsGreaterThan("value", (int)value, (int)b, string.Format("Frames number is out of valid range (0-{0}).", b));
				this._frames = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002214D File Offset: 0x0002034D
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00022155 File Offset: 0x00020355
		public byte SubFrames
		{
			get
			{
				return this._subFrames;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", (int)value, 99, string.Format("Sub-frames number is out of valid range (0-{0}).", 99));
				this._subFrames = value;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002217C File Offset: 0x0002037C
		public static SmpteData Read(Func<byte> byteReader, Func<byte, string, byte, byte> valueProcessor)
		{
			byte b = byteReader();
			SmpteFormat format = SmpteData.GetFormat(b);
			byte b2 = valueProcessor(SmpteData.GetHours(b), "Hours", 23);
			byte b3 = valueProcessor(byteReader(), "Minutes", 59);
			byte b4 = valueProcessor(byteReader(), "Seconds", 59);
			byte b5 = valueProcessor(byteReader(), "Frames", SmpteData.MaxFrames[format]);
			byte b6 = valueProcessor(byteReader(), "SubFrames", 99);
			return new SmpteData(format, b2, b3, b4, b5, b6);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00022215 File Offset: 0x00020415
		public void Write(Action<byte> byteWriter)
		{
			byteWriter(this.GetFormatAndHours());
			byteWriter(this.Minutes);
			byteWriter(this.Seconds);
			byteWriter(this.Frames);
			byteWriter(this.SubFrames);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00022253 File Offset: 0x00020453
		internal static SmpteFormat GetFormat(byte formatAndHours)
		{
			return SmpteData.Formats[(formatAndHours & 96) >> 5];
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00022261 File Offset: 0x00020461
		internal static byte GetHours(byte formatAndHours)
		{
			return formatAndHours & 31;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00022268 File Offset: 0x00020468
		internal byte GetFormatAndHours()
		{
			return SmpteData.GetFormatAndHours(this.Format, this.Hours);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002227C File Offset: 0x0002047C
		internal static byte GetFormatAndHours(SmpteFormat smpteFormat, byte hours)
		{
			byte b = 0;
			if (smpteFormat != SmpteFormat.TwentyFive)
			{
				if (smpteFormat != SmpteFormat.ThirtyDrop)
				{
					if (smpteFormat == SmpteFormat.Thirty)
					{
						b = 3;
					}
				}
				else
				{
					b = 2;
				}
			}
			else
			{
				b = 1;
			}
			return (byte)(((int)b << 5) & (int)hours);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000222AC File Offset: 0x000204AC
		// Note: this type is marked as 'beforefieldinit'.
		static SmpteData()
		{
			Dictionary<SmpteFormat, byte> dictionary = new Dictionary<SmpteFormat, byte>();
			dictionary[SmpteFormat.TwentyFour] = 23;
			dictionary[SmpteFormat.TwentyFive] = 24;
			dictionary[SmpteFormat.ThirtyDrop] = 28;
			dictionary[SmpteFormat.Thirty] = 29;
			SmpteData.MaxFrames = dictionary;
			SmpteData.Formats = new SmpteFormat[]
			{
				SmpteFormat.TwentyFour,
				SmpteFormat.TwentyFive,
				SmpteFormat.ThirtyDrop,
				SmpteFormat.Thirty
			};
		}

		// Token: 0x04000953 RID: 2387
		private const byte MaxHours = 23;

		// Token: 0x04000954 RID: 2388
		private const byte MaxMinutes = 59;

		// Token: 0x04000955 RID: 2389
		private const byte MaxSeconds = 59;

		// Token: 0x04000956 RID: 2390
		private const byte MaxSubFrames = 99;

		// Token: 0x04000957 RID: 2391
		private const int FormatMask = 96;

		// Token: 0x04000958 RID: 2392
		private const int FormatOffset = 5;

		// Token: 0x04000959 RID: 2393
		private const int HoursMask = 31;

		// Token: 0x0400095A RID: 2394
		private static readonly Dictionary<SmpteFormat, byte> MaxFrames;

		// Token: 0x0400095B RID: 2395
		private static readonly SmpteFormat[] Formats;

		// Token: 0x0400095C RID: 2396
		private SmpteFormat _format = SmpteFormat.TwentyFour;

		// Token: 0x0400095D RID: 2397
		private byte _hours;

		// Token: 0x0400095E RID: 2398
		private byte _minutes;

		// Token: 0x0400095F RID: 2399
		private byte _seconds;

		// Token: 0x04000960 RID: 2400
		private byte _frames;

		// Token: 0x04000961 RID: 2401
		private byte _subFrames;
	}
}
