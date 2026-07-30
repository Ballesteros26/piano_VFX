using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000080 RID: 128
	internal abstract class BinXmlDateTime
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000F689 File Offset: 0x0000D889
		private static void Write2Dig(StringBuilder sb, int val)
		{
			sb.Append((char)(48 + val / 10));
			sb.Append((char)(48 + val % 10));
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		private static void Write4DigNeg(StringBuilder sb, int val)
		{
			if (val < 0)
			{
				val = -val;
				sb.Append('-');
			}
			BinXmlDateTime.Write2Dig(sb, val / 100);
			BinXmlDateTime.Write2Dig(sb, val % 100);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		private static void Write3Dec(StringBuilder sb, int val)
		{
			int num = val % 10;
			val /= 10;
			int num2 = val % 10;
			val /= 10;
			int num3 = val;
			sb.Append('.');
			sb.Append((char)(48 + num3));
			sb.Append((char)(48 + num2));
			sb.Append((char)(48 + num));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000F722 File Offset: 0x0000D922
		private static void WriteDate(StringBuilder sb, int yr, int mnth, int day)
		{
			BinXmlDateTime.Write4DigNeg(sb, yr);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, mnth);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, day);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000F74B File Offset: 0x0000D94B
		private static void WriteTime(StringBuilder sb, int hr, int min, int sec, int ms)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (ms != 0)
			{
				BinXmlDateTime.Write3Dec(sb, ms);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F780 File Offset: 0x0000D980
		private static void WriteTimeFullPrecision(StringBuilder sb, int hr, int min, int sec, int fraction)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (fraction != 0)
			{
				int i = 7;
				while (fraction % 10 == 0)
				{
					i--;
					fraction /= 10;
				}
				char[] array = new char[i];
				while (i > 0)
				{
					i--;
					array[i] = (char)(fraction % 10 + 48);
					fraction /= 10;
				}
				sb.Append('.');
				sb.Append(array);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000F804 File Offset: 0x0000DA04
		private static void WriteTimeZone(StringBuilder sb, TimeSpan zone)
		{
			bool flag = true;
			if (zone.Ticks < 0L)
			{
				flag = false;
				zone = zone.Negate();
			}
			BinXmlDateTime.WriteTimeZone(sb, flag, zone.Hours, zone.Minutes);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000F83E File Offset: 0x0000DA3E
		private static void WriteTimeZone(StringBuilder sb, bool negTimeZone, int hr, int min)
		{
			if (hr == 0 && min == 0)
			{
				sb.Append('Z');
				return;
			}
			sb.Append(negTimeZone ? '+' : '-');
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000F878 File Offset: 0x0000DA78
		private static void BreakDownXsdDateTime(long val, out int yr, out int mnth, out int day, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				long num = val / 4L;
				ms = (int)(num % 1000L);
				num /= 1000L;
				sec = (int)(num % 60L);
				num /= 60L;
				min = (int)(num % 60L);
				num /= 60L;
				hr = (int)(num % 24L);
				num /= 24L;
				day = (int)(num % 31L) + 1;
				num /= 31L;
				mnth = (int)(num % 12L) + 1;
				num /= 12L;
				yr = (int)(num - 9999L);
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000F920 File Offset: 0x0000DB20
		private static void BreakDownXsdDate(long val, out int yr, out int mnth, out int day, out bool negTimeZone, out int hr, out int min)
		{
			if (val >= 0L)
			{
				val /= 4L;
				int num = (int)(val % 1740L) - 840;
				long num2 = val / 1740L;
				if (negTimeZone = num < 0)
				{
					num = -num;
				}
				min = num % 60;
				hr = num / 60;
				day = (int)(num2 % 31L) + 1;
				num2 /= 31L;
				mnth = (int)(num2 % 12L) + 1;
				yr = (int)(num2 / 12L) - 9999;
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		private static void BreakDownXsdTime(long val, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				val /= 4L;
				ms = (int)(val % 1000L);
				val /= 1000L;
				sec = (int)(val % 60L);
				val /= 60L;
				min = (int)(val % 60L);
				hr = (int)(val / 60L);
				if (0 <= hr && hr <= 23)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public static string XsdDateTimeToString(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			BinXmlDateTime.BreakDownXsdDateTime(val, out num, out num2, out num3, out num4, out num5, out num6, out num7);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, num, num2, num3);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTime(stringBuilder, num4, num5, num6, num7);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public static DateTime XsdDateTimeToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			BinXmlDateTime.BreakDownXsdDateTime(val, out num, out num2, out num3, out num4, out num5, out num6, out num7);
			return new DateTime(num, num2, num3, num4, num5, num6, num7, DateTimeKind.Utc);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public static string XsdDateToString(long val)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			int num4;
			int num5;
			BinXmlDateTime.BreakDownXsdDate(val, out num, out num2, out num3, out flag, out num4, out num5);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, num, num2, num3);
			BinXmlDateTime.WriteTimeZone(stringBuilder, flag, num4, num5);
			return stringBuilder.ToString();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public static DateTime XsdDateToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			int num4;
			int num5;
			BinXmlDateTime.BreakDownXsdDate(val, out num, out num2, out num3, out flag, out num4, out num5);
			DateTime dateTime = new DateTime(num, num2, num3, 0, 0, 0, DateTimeKind.Utc);
			int num6 = (flag ? (-1) : 1) * (num4 * 60 + num5);
			return TimeZone.CurrentTimeZone.ToLocalTime(dateTime.AddMinutes((double)num6));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public static string XsdTimeToString(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			BinXmlDateTime.BreakDownXsdTime(val, out num, out num2, out num3, out num4);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTime(stringBuilder, num, num2, num3, num4);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public static DateTime XsdTimeToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			BinXmlDateTime.BreakDownXsdTime(val, out num, out num2, out num3, out num4);
			return new DateTime(1, 1, 1, num, num2, num3, num4, DateTimeKind.Utc);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public static string SqlDateTimeToString(int dateticks, uint timeticks)
		{
			DateTime dateTime = BinXmlDateTime.SqlDateTimeToDateTime(dateticks, timeticks);
			string text = ((dateTime.Millisecond != 0) ? "yyyy/MM/dd\\THH:mm:ss.ffff" : "yyyy/MM/dd\\THH:mm:ss");
			return dateTime.ToString(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public static DateTime SqlDateTimeToDateTime(int dateticks, uint timeticks)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			long num = (long)(timeticks / BinXmlDateTime.SQLTicksPerMillisecond + 0.5);
			return dateTime.Add(new TimeSpan((long)dateticks * 864000000000L + num * 10000L));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public static string SqlSmallDateTimeToString(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(dateticks, timeticks).ToString("yyyy/MM/dd\\THH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000FC4E File Offset: 0x0000DE4E
		public static DateTime SqlSmallDateTimeToDateTime(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlDateTimeToDateTime((int)dateticks, (uint)((int)timeticks * BinXmlDateTime.SQLTicksPerMinute));
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public static DateTime XsdKatmaiDateToDateTime(byte[] data, int offset)
		{
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public static DateTime XsdKatmaiDateTimeToDateTime(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks + katmaiTimeTicks);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000FCA3 File Offset: 0x0000DEA3
		public static DateTime XsdKatmaiTimeToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public static DateTime XsdKatmaiDateOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		public static DateTime XsdKatmaiDateTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
		public static DateTime XsdKatmaiTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public static DateTimeOffset XsdKatmaiDateToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000FD0E File Offset: 0x0000DF0E
		public static DateTimeOffset XsdKatmaiDateTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public static DateTimeOffset XsdKatmaiTimeToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		public static DateTimeOffset XsdKatmaiDateOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000FD34 File Offset: 0x0000DF34
		public static DateTimeOffset XsdKatmaiDateTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			long katmaiTimeZoneTicks = BinXmlDateTime.GetKatmaiTimeZoneTicks(data, offset);
			return new DateTimeOffset(katmaiDateTicks + katmaiTimeTicks + katmaiTimeZoneTicks, new TimeSpan(katmaiTimeZoneTicks));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		public static DateTimeOffset XsdKatmaiTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public static string XsdKatmaiDateToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(10);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			return stringBuilder.ToString();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public static string XsdKatmaiDateTimeToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(33);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTime.Hour, dateTime.Minute, dateTime.Second, BinXmlDateTime.GetFractions(dateTime));
			return stringBuilder.ToString();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000FE10 File Offset: 0x0000E010
		public static string XsdKatmaiTimeToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTime.Hour, dateTime.Minute, dateTime.Second, BinXmlDateTime.GetFractions(dateTime));
			return stringBuilder.ToString();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000FE54 File Offset: 0x0000E054
		public static string XsdKatmaiDateOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public static string XsdKatmaiDateTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(39);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, BinXmlDateTime.GetFractions(dateTimeOffset));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static string XsdKatmaiTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(22);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, BinXmlDateTime.GetFractions(dateTimeOffset));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000FF64 File Offset: 0x0000E164
		private static long GetKatmaiDateTicks(byte[] data, ref int pos)
		{
			int num = pos;
			pos = num + 3;
			return (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16)) * 864000000000L;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000FF98 File Offset: 0x0000E198
		private static long GetKatmaiTimeTicks(byte[] data, ref int pos)
		{
			int num = pos;
			byte b = data[num];
			num++;
			long num2;
			if (b <= 2)
			{
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				pos = num + 3;
			}
			else if (b <= 4)
			{
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				num2 |= (long)((long)((ulong)data[num + 3]) << 24);
				pos = num + 4;
			}
			else
			{
				if (b > 7)
				{
					throw new XmlException("Arithmetic Overflow.", null);
				}
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				num2 |= (long)(((ulong)data[num + 3] << 24) | ((ulong)data[num + 4] << 32));
				pos = num + 5;
			}
			return num2 * (long)BinXmlDateTime.KatmaiTimeScaleMultiplicator[(int)b];
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001004B File Offset: 0x0000E24B
		private static long GetKatmaiTimeZoneTicks(byte[] data, int pos)
		{
			return (long)((short)((int)data[pos] | ((int)data[pos + 1] << 8))) * 600000000L;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010064 File Offset: 0x0000E264
		private static int GetFractions(DateTime dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000100B4 File Offset: 0x0000E2B4
		private static int GetFractions(DateTimeOffset dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x04000297 RID: 663
		private const int MaxFractionDigits = 7;

		// Token: 0x04000298 RID: 664
		internal static int[] KatmaiTimeScaleMultiplicator = new int[] { 10000000, 1000000, 100000, 10000, 1000, 100, 10, 1 };

		// Token: 0x04000299 RID: 665
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x0400029A RID: 666
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x0400029B RID: 667
		public static readonly int SQLTicksPerMinute = BinXmlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x0400029C RID: 668
		public static readonly int SQLTicksPerHour = BinXmlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x0400029D RID: 669
		private static readonly int SQLTicksPerDay = BinXmlDateTime.SQLTicksPerHour * 24;
	}
}
