using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020004A9 RID: 1193
	internal struct XsdDateTime
	{
		// Token: 0x06003077 RID: 12407 RVA: 0x00117D53 File Offset: 0x00115F53
		public XsdDateTime(string text)
		{
			this = new XsdDateTime(text, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x00117D64 File Offset: 0x00115F64
		public XsdDateTime(string text, XsdDateTimeFlags kinds)
		{
			this = default(XsdDateTime);
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { text, kinds }));
			}
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00117DB5 File Offset: 0x00115FB5
		private XsdDateTime(XsdDateTime.Parser parser)
		{
			this = default(XsdDateTime);
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x00117DC8 File Offset: 0x00115FC8
		private void InitiateXsdDateTime(XsdDateTime.Parser parser)
		{
			this.dt = new DateTime(parser.year, parser.month, parser.day, parser.hour, parser.minute, parser.second);
			if (parser.fraction != 0)
			{
				this.dt = this.dt.AddTicks((long)parser.fraction);
			}
			this.extra = (uint)(((int)parser.typeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)parser.kind << 16) | (XsdDateTime.DateTimeTypeCode)(parser.zoneHour << 8) | (XsdDateTime.DateTimeTypeCode)parser.zoneMinute);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x00117E50 File Offset: 0x00116050
		internal static bool TryParse(string text, XsdDateTimeFlags kinds, out XsdDateTime result)
		{
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				result = default(XsdDateTime);
				return false;
			}
			result = new XsdDateTime(parser);
			return true;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x00117E88 File Offset: 0x00116088
		public XsdDateTime(DateTime dateTime, XsdDateTimeFlags kinds)
		{
			this.dt = dateTime;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			int num = 0;
			int num2 = 0;
			DateTimeKind kind = dateTime.Kind;
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (kind != DateTimeKind.Unspecified)
			{
				if (kind != DateTimeKind.Utc)
				{
					TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					if (utcOffset.Ticks < 0L)
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
						num = -utcOffset.Hours;
						num2 = -utcOffset.Minutes;
					}
					else
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
						num = utcOffset.Hours;
						num2 = utcOffset.Minutes;
					}
				}
				else
				{
					xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
				}
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Unspecified;
			}
			this.extra = (uint)(((int)dateTimeTypeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(num << 8) | (XsdDateTime.DateTimeTypeCode)num2);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x00117F1A File Offset: 0x0011611A
		public XsdDateTime(DateTimeOffset dateTimeOffset)
		{
			this = new XsdDateTime(dateTimeOffset, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x00117F24 File Offset: 0x00116124
		public XsdDateTime(DateTimeOffset dateTimeOffset, XsdDateTimeFlags kinds)
		{
			this.dt = dateTimeOffset.DateTime;
			TimeSpan timeSpan = dateTimeOffset.Offset;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (timeSpan.TotalMinutes < 0.0)
			{
				timeSpan = timeSpan.Negate();
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
			}
			else if (timeSpan.TotalMinutes > 0.0)
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
			}
			this.extra = (uint)(((int)dateTimeTypeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(timeSpan.Hours << 8) | (XsdDateTime.DateTimeTypeCode)timeSpan.Minutes);
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x00117FA6 File Offset: 0x001161A6
		private XsdDateTime.DateTimeTypeCode InternalTypeCode
		{
			get
			{
				return (XsdDateTime.DateTimeTypeCode)((this.extra & 4278190080U) >> 24);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x00117FB7 File Offset: 0x001161B7
		private XsdDateTime.XsdDateTimeKind InternalKind
		{
			get
			{
				return (XsdDateTime.XsdDateTimeKind)((this.extra & 16711680U) >> 16);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x00117FC8 File Offset: 0x001161C8
		public XmlTypeCode TypeCode
		{
			get
			{
				return XsdDateTime.typeCodes[(int)this.InternalTypeCode];
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x00117FD8 File Offset: 0x001161D8
		public DateTimeKind Kind
		{
			get
			{
				XsdDateTime.XsdDateTimeKind internalKind = this.InternalKind;
				if (internalKind == XsdDateTime.XsdDateTimeKind.Unspecified)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != XsdDateTime.XsdDateTimeKind.Zulu)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x00117FFA File Offset: 0x001161FA
		public int Year
		{
			get
			{
				return this.dt.Year;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x00118007 File Offset: 0x00116207
		public int Month
		{
			get
			{
				return this.dt.Month;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x00118014 File Offset: 0x00116214
		public int Day
		{
			get
			{
				return this.dt.Day;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x00118021 File Offset: 0x00116221
		public int Hour
		{
			get
			{
				return this.dt.Hour;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x0011802E File Offset: 0x0011622E
		public int Minute
		{
			get
			{
				return this.dt.Minute;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x0011803B File Offset: 0x0011623B
		public int Second
		{
			get
			{
				return this.dt.Second;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x00118048 File Offset: 0x00116248
		public int Fraction
		{
			get
			{
				return (int)(this.dt.Ticks - new DateTime(this.dt.Year, this.dt.Month, this.dt.Day, this.dt.Hour, this.dt.Minute, this.dt.Second).Ticks);
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x001180B1 File Offset: 0x001162B1
		public int ZoneHour
		{
			get
			{
				return (int)((this.extra & 65280U) >> 8);
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x001180C1 File Offset: 0x001162C1
		public int ZoneMinute
		{
			get
			{
				return (int)(this.extra & 255U);
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x001180D0 File Offset: 0x001162D0
		public DateTime ToZulu()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return new DateTime(this.dt.Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return new DateTime(this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return new DateTime(this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0)).Ticks, DateTimeKind.Utc);
			default:
				return this.dt;
			}
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0011816C File Offset: 0x0011636C
		public static implicit operator DateTime(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(timeSpan);
			}
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
				break;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				long num = dateTime.Ticks + new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num > DateTime.MaxValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num > DateTime.MaxValue.Ticks)
					{
						num = DateTime.MaxValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				long num = dateTime.Ticks - new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num < DateTime.MinValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num < DateTime.MinValue.Ticks)
					{
						num = DateTime.MinValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			}
			return dateTime;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00118354 File Offset: 0x00116554
		public static implicit operator DateTimeOffset(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(timeSpan);
			}
			DateTimeOffset dateTimeOffset;
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(0L));
				return dateTimeOffset;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(-xdt.ZoneHour, -xdt.ZoneMinute, 0));
				return dateTimeOffset;
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				return dateTimeOffset;
			}
			dateTimeOffset = new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime));
			return dateTimeOffset;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x00118478 File Offset: 0x00116678
		public static int Compare(XsdDateTime left, XsdDateTime right)
		{
			if (left.extra == right.extra)
			{
				return DateTime.Compare(left.dt, right.dt);
			}
			if (left.InternalTypeCode != right.InternalTypeCode)
			{
				throw new ArgumentException(Res.GetString("Cannot compare '{0}' and '{1}'.", new object[] { left.TypeCode, right.TypeCode }));
			}
			return DateTime.Compare(left.GetZuluDateTime(), right.GetZuluDateTime());
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x001184FC File Offset: 0x001166FC
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			return XsdDateTime.Compare(this, (XsdDateTime)value);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00118514 File Offset: 0x00116714
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			switch (this.InternalTypeCode)
			{
			case XsdDateTime.DateTimeTypeCode.DateTime:
				this.PrintDate(stringBuilder);
				stringBuilder.Append('T');
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Time:
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Date:
				this.PrintDate(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.GYearMonth:
			{
				char[] array = new char[XsdDateTime.Lzyyyy_MM];
				this.IntToCharArray(array, 0, this.Year, 4);
				array[XsdDateTime.Lzyyyy] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GYear:
			{
				char[] array = new char[XsdDateTime.Lzyyyy];
				this.IntToCharArray(array, 0, this.Year, 4);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonthDay:
			{
				char[] array = new char[XsdDateTime.Lz__mm_dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__mm_, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GDay:
			{
				char[] array = new char[XsdDateTime.Lz___dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				array[XsdDateTime.Lz__] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz___, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonth:
			{
				char[] array = new char[XsdDateTime.Lz__mm__];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				array[XsdDateTime.Lz__mm_] = '-';
				stringBuilder.Append(array);
				break;
			}
			}
			this.PrintZone(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x001186DC File Offset: 0x001168DC
		private void PrintDate(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.Lzyyyy_MM_dd];
			this.IntToCharArray(array, 0, this.Year, 4);
			array[XsdDateTime.Lzyyyy] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
			array[XsdDateTime.Lzyyyy_MM] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_MM_, this.Day);
			sb.Append(array);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x00118744 File Offset: 0x00116944
		private void PrintTime(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.LzHH_mm_ss];
			this.ShortToCharArray(array, 0, this.Hour);
			array[XsdDateTime.LzHH] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_, this.Minute);
			array[XsdDateTime.LzHH_mm] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_mm_, this.Second);
			sb.Append(array);
			int num = this.Fraction;
			if (num != 0)
			{
				int num2 = 7;
				while (num % 10 == 0)
				{
					num2--;
					num /= 10;
				}
				array = new char[num2 + 1];
				array[0] = '.';
				this.IntToCharArray(array, 1, num, num2);
				sb.Append(array);
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x001187E8 File Offset: 0x001169E8
		private void PrintZone(StringBuilder sb)
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				sb.Append('Z');
				return;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '+';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x001188A6 File Offset: 0x00116AA6
		private void IntToCharArray(char[] text, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				text[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x001188C7 File Offset: 0x00116AC7
		private void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x001188E4 File Offset: 0x00116AE4
		private DateTime GetZuluDateTime()
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				return this.dt;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				return this.dt.Add(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				return this.dt.Subtract(new TimeSpan(this.ZoneHour, this.ZoneMinute, 0));
			default:
				return this.dt.ToUniversalTime();
			}
		}

		// Token: 0x04001FA9 RID: 8105
		private DateTime dt;

		// Token: 0x04001FAA RID: 8106
		private uint extra;

		// Token: 0x04001FAB RID: 8107
		private const uint TypeMask = 4278190080U;

		// Token: 0x04001FAC RID: 8108
		private const uint KindMask = 16711680U;

		// Token: 0x04001FAD RID: 8109
		private const uint ZoneHourMask = 65280U;

		// Token: 0x04001FAE RID: 8110
		private const uint ZoneMinuteMask = 255U;

		// Token: 0x04001FAF RID: 8111
		private const int TypeShift = 24;

		// Token: 0x04001FB0 RID: 8112
		private const int KindShift = 16;

		// Token: 0x04001FB1 RID: 8113
		private const int ZoneHourShift = 8;

		// Token: 0x04001FB2 RID: 8114
		private const short maxFractionDigits = 7;

		// Token: 0x04001FB3 RID: 8115
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x04001FB4 RID: 8116
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04001FB5 RID: 8117
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04001FB6 RID: 8118
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x04001FB7 RID: 8119
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x04001FB8 RID: 8120
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x04001FB9 RID: 8121
		private static readonly int LzHH = "HH".Length;

		// Token: 0x04001FBA RID: 8122
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x04001FBB RID: 8123
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x04001FBC RID: 8124
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x04001FBD RID: 8125
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x04001FBE RID: 8126
		private static readonly int Lz_ = "-".Length;

		// Token: 0x04001FBF RID: 8127
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x04001FC0 RID: 8128
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x04001FC1 RID: 8129
		private static readonly int Lz_zz_zz = "-zz:zz".Length;

		// Token: 0x04001FC2 RID: 8130
		private static readonly int Lz__ = "--".Length;

		// Token: 0x04001FC3 RID: 8131
		private static readonly int Lz__mm = "--MM".Length;

		// Token: 0x04001FC4 RID: 8132
		private static readonly int Lz__mm_ = "--MM-".Length;

		// Token: 0x04001FC5 RID: 8133
		private static readonly int Lz__mm__ = "--MM--".Length;

		// Token: 0x04001FC6 RID: 8134
		private static readonly int Lz__mm_dd = "--MM-dd".Length;

		// Token: 0x04001FC7 RID: 8135
		private static readonly int Lz___ = "---".Length;

		// Token: 0x04001FC8 RID: 8136
		private static readonly int Lz___dd = "---dd".Length;

		// Token: 0x04001FC9 RID: 8137
		private static readonly XmlTypeCode[] typeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.DateTime,
			XmlTypeCode.Time,
			XmlTypeCode.Date,
			XmlTypeCode.GYearMonth,
			XmlTypeCode.GYear,
			XmlTypeCode.GMonthDay,
			XmlTypeCode.GDay,
			XmlTypeCode.GMonth
		};

		// Token: 0x020004AA RID: 1194
		private enum DateTimeTypeCode
		{
			// Token: 0x04001FCB RID: 8139
			DateTime,
			// Token: 0x04001FCC RID: 8140
			Time,
			// Token: 0x04001FCD RID: 8141
			Date,
			// Token: 0x04001FCE RID: 8142
			GYearMonth,
			// Token: 0x04001FCF RID: 8143
			GYear,
			// Token: 0x04001FD0 RID: 8144
			GMonthDay,
			// Token: 0x04001FD1 RID: 8145
			GDay,
			// Token: 0x04001FD2 RID: 8146
			GMonth,
			// Token: 0x04001FD3 RID: 8147
			XdrDateTime
		}

		// Token: 0x020004AB RID: 1195
		private enum XsdDateTimeKind
		{
			// Token: 0x04001FD5 RID: 8149
			Unspecified,
			// Token: 0x04001FD6 RID: 8150
			Zulu,
			// Token: 0x04001FD7 RID: 8151
			LocalWestOfZulu,
			// Token: 0x04001FD8 RID: 8152
			LocalEastOfZulu
		}

		// Token: 0x020004AC RID: 1196
		private struct Parser
		{
			// Token: 0x06003099 RID: 12441 RVA: 0x00118ACC File Offset: 0x00116CCC
			public bool Parse(string text, XsdDateTimeFlags kinds)
			{
				this.text = text;
				this.length = text.Length;
				int num = 0;
				while (num < this.length && char.IsWhiteSpace(text[num]))
				{
					num++;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Date | XsdDateTimeFlags.XdrDateTimeNoTz | XsdDateTimeFlags.XdrDateTime) && this.ParseDate(num))
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime) && this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.DateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Date) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.Date;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTime) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd) || (this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTimeNoTz))
					{
						if (!this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T'))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
						if (this.ParseTimeAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Time) && this.ParseTimeAndZoneAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrTimeNoTz) && this.ParseTimeAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear) && this.Parse4Dig(num, ref this.year) && 1 <= this.year)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth) && this.ParseChar(num + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(num + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM))
					{
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYearMonth;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYear) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy))
					{
						this.month = 1;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYear;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GMonth) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.Parse2Dig(num + XsdDateTime.Lz__, ref this.month) && 1 <= this.month && this.month <= 12)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay) && this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.Parse2Dig(num + XsdDateTime.Lz__mm_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, this.month) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm_dd))
					{
						this.year = 1904;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonthDay;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonth) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm) || (this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.ParseChar(num + XsdDateTime.Lz__mm_, '-') && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm__))))
					{
						this.year = 1904;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonth;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GDay) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.ParseChar(num + XsdDateTime.Lz__, '-') && this.Parse2Dig(num + XsdDateTime.Lz___, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, 1) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz___dd))
				{
					this.year = 1904;
					this.month = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.GDay;
					return true;
				}
				return false;
			}

			// Token: 0x0600309A RID: 12442 RVA: 0x00118EFC File Offset: 0x001170FC
			private bool ParseDate(int start)
			{
				return this.Parse4Dig(start, ref this.year) && 1 <= this.year && this.ParseChar(start + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseChar(start + XsdDateTime.Lzyyyy_MM, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_MM_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(this.year, this.month);
			}

			// Token: 0x0600309B RID: 12443 RVA: 0x00118FAD File Offset: 0x001171AD
			private bool ParseTimeAndZoneAndWhitespace(int start)
			{
				return this.ParseTime(ref start) && this.ParseZoneAndWhitespace(start);
			}

			// Token: 0x0600309C RID: 12444 RVA: 0x00118FC5 File Offset: 0x001171C5
			private bool ParseTimeAndWhitespace(int start)
			{
				if (this.ParseTime(ref start))
				{
					while (start < this.length)
					{
						start++;
					}
					return start == this.length;
				}
				return false;
			}

			// Token: 0x0600309D RID: 12445 RVA: 0x00118FEC File Offset: 0x001171EC
			private bool ParseTime(ref int start)
			{
				if (this.Parse2Dig(start, ref this.hour) && this.hour < 24 && this.ParseChar(start + XsdDateTime.LzHH, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_, ref this.minute) && this.minute < 60 && this.ParseChar(start + XsdDateTime.LzHH_mm, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_mm_, ref this.second) && this.second < 60)
				{
					start += XsdDateTime.LzHH_mm_ss;
					if (this.ParseChar(start, '.'))
					{
						this.fraction = 0;
						int num = 0;
						int num2 = 0;
						for (;;)
						{
							int num3 = start + 1;
							start = num3;
							if (num3 >= this.length)
							{
								break;
							}
							int num4 = (int)(this.text[start] - '0');
							if (9 < num4)
							{
								break;
							}
							if (num < 7)
							{
								this.fraction = this.fraction * 10 + num4;
							}
							else if (num == 7)
							{
								if (5 < num4)
								{
									num2 = 1;
								}
								else if (num4 == 5)
								{
									num2 = -1;
								}
							}
							else if (num2 < 0 && num4 != 0)
							{
								num2 = 1;
							}
							num++;
						}
						if (num < 7)
						{
							if (num == 0)
							{
								return false;
							}
							this.fraction *= XsdDateTime.Parser.Power10[7 - num];
						}
						else
						{
							if (num2 < 0)
							{
								num2 = this.fraction & 1;
							}
							this.fraction += num2;
						}
					}
					return true;
				}
				this.hour = 0;
				return false;
			}

			// Token: 0x0600309E RID: 12446 RVA: 0x0011915C File Offset: 0x0011735C
			private bool ParseZoneAndWhitespace(int start)
			{
				if (start < this.length)
				{
					char c = this.text[start];
					if (c == 'Z' || c == 'z')
					{
						this.kind = XsdDateTime.XsdDateTimeKind.Zulu;
						start++;
					}
					else if (start + 5 < this.length && this.Parse2Dig(start + XsdDateTime.Lz_, ref this.zoneHour) && this.zoneHour <= 99 && this.ParseChar(start + XsdDateTime.Lz_zz, ':') && this.Parse2Dig(start + XsdDateTime.Lz_zz_, ref this.zoneMinute) && this.zoneMinute <= 99)
					{
						if (c == '-')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
						else if (c == '+')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
					}
				}
				while (start < this.length && char.IsWhiteSpace(this.text[start]))
				{
					start++;
				}
				return start == this.length;
			}

			// Token: 0x0600309F RID: 12447 RVA: 0x00119254 File Offset: 0x00117454
			private bool Parse4Dig(int start, ref int num)
			{
				if (start + 3 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					int num4 = (int)(this.text[start + 2] - '0');
					int num5 = (int)(this.text[start + 3] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
					{
						num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060030A0 RID: 12448 RVA: 0x001192EC File Offset: 0x001174EC
			private bool Parse2Dig(int start, ref int num)
			{
				if (start + 1 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
					{
						num = num2 * 10 + num3;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060030A1 RID: 12449 RVA: 0x00119343 File Offset: 0x00117543
			private bool ParseChar(int start, char ch)
			{
				return start < this.length && this.text[start] == ch;
			}

			// Token: 0x060030A2 RID: 12450 RVA: 0x0011935F File Offset: 0x0011755F
			private static bool Test(XsdDateTimeFlags left, XsdDateTimeFlags right)
			{
				return (left & right) > (XsdDateTimeFlags)0;
			}

			// Token: 0x04001FD9 RID: 8153
			private const int leapYear = 1904;

			// Token: 0x04001FDA RID: 8154
			private const int firstMonth = 1;

			// Token: 0x04001FDB RID: 8155
			private const int firstDay = 1;

			// Token: 0x04001FDC RID: 8156
			public XsdDateTime.DateTimeTypeCode typeCode;

			// Token: 0x04001FDD RID: 8157
			public int year;

			// Token: 0x04001FDE RID: 8158
			public int month;

			// Token: 0x04001FDF RID: 8159
			public int day;

			// Token: 0x04001FE0 RID: 8160
			public int hour;

			// Token: 0x04001FE1 RID: 8161
			public int minute;

			// Token: 0x04001FE2 RID: 8162
			public int second;

			// Token: 0x04001FE3 RID: 8163
			public int fraction;

			// Token: 0x04001FE4 RID: 8164
			public XsdDateTime.XsdDateTimeKind kind;

			// Token: 0x04001FE5 RID: 8165
			public int zoneHour;

			// Token: 0x04001FE6 RID: 8166
			public int zoneMinute;

			// Token: 0x04001FE7 RID: 8167
			private string text;

			// Token: 0x04001FE8 RID: 8168
			private int length;

			// Token: 0x04001FE9 RID: 8169
			private static int[] Power10 = new int[] { -1, 10, 100, 1000, 10000, 100000, 1000000 };
		}
	}
}
