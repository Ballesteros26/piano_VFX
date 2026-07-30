using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace System
{
	// Token: 0x0200016B RID: 363
	internal static class DateTimeFormat
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x0003EF77 File Offset: 0x0003D177
		internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
		{
			DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003EF84 File Offset: 0x0003D184
		[SecuritySafeCritical]
		internal unsafe static void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
		{
			if (!overrideLengthLimit && len > 2)
			{
				len = 2;
			}
			char* ptr = stackalloc char[(UIntPtr)32];
			char* ptr2 = ptr + 16;
			int num = value;
			do
			{
				*(--ptr2) = (char)(num % 10 + 48);
				num /= 10;
			}
			while (num != 0 && ptr2 != ptr);
			int num2 = (int)((long)(ptr + 16 - ptr2));
			while (num2 < len && ptr2 != ptr)
			{
				*(--ptr2) = '0';
				num2++;
			}
			outputBuffer.Append(ptr2, num2);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003EFF2 File Offset: 0x0003D1F2
		private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
		{
			outputBuffer.Append(HebrewNumber.ToString(digits));
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003F004 File Offset: 0x0003D204
		internal static int ParseRepeatPattern(string format, int pos, char patternChar)
		{
			int length = format.Length;
			int num = pos + 1;
			while (num < length && format[num] == patternChar)
			{
				num++;
			}
			return num - pos;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003F033 File Offset: 0x0003D233
		private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
		{
			if (repeat == 3)
			{
				return dtfi.GetAbbreviatedDayName((DayOfWeek)dayOfWeek);
			}
			return dtfi.GetDayName((DayOfWeek)dayOfWeek);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003F048 File Offset: 0x0003D248
		private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003F060 File Offset: 0x0003D260
		private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
			{
				return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
			}
			if (month >= 7)
			{
				month++;
			}
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003F0B0 File Offset: 0x0003D2B0
		internal static int ParseQuoteString(string format, int pos, StringBuilder result)
		{
			int length = format.Length;
			int num = pos;
			char c = format[pos++];
			bool flag = false;
			while (pos < length)
			{
				char c2 = format[pos++];
				if (c2 == c)
				{
					flag = true;
					break;
				}
				if (c2 == '\\')
				{
					if (pos >= length)
					{
						throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
					}
					result.Append(format[pos++]);
				}
				else
				{
					result.Append(c2);
				}
			}
			if (!flag)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot find a matching quote character for the character '{0}'."), c));
			}
			return pos - num;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003F150 File Offset: 0x0003D350
		internal static int ParseNextChar(string format, int pos)
		{
			if (pos >= format.Length - 1)
			{
				return -1;
			}
			return (int)format[pos + 1];
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003F168 File Offset: 0x0003D368
		private static bool IsUseGenitiveForm(string format, int index, int tokenLen, char patternToMatch)
		{
			int num = 0;
			int num2 = index - 1;
			while (num2 >= 0 && format[num2] != patternToMatch)
			{
				num2--;
			}
			if (num2 >= 0)
			{
				while (--num2 >= 0 && format[num2] == patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			num2 = index + tokenLen;
			while (num2 < format.Length && format[num2] != patternToMatch)
			{
				num2++;
			}
			if (num2 < format.Length)
			{
				num = 0;
				while (++num2 < format.Length && format[num2] == patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003F200 File Offset: 0x0003D400
		private static string FormatCustomized(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			Calendar calendar = dtfi.Calendar;
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			bool flag = calendar.ID == 8;
			bool flag2 = true;
			int i = 0;
			while (i < format.Length)
			{
				char c = format[i];
				int num2;
				if (c <= 'K')
				{
					if (c <= '/')
					{
						if (c <= '%')
						{
							if (c != '"')
							{
								if (c != '%')
								{
									goto IL_05C0;
								}
								int num = DateTimeFormat.ParseNextChar(format, i);
								if (num >= 0 && num != 37)
								{
									stringBuilder.Append(DateTimeFormat.FormatCustomized(dateTime, ((char)num).ToString(), dtfi, offset));
									num2 = 2;
									goto IL_05CC;
								}
								throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
							}
						}
						else if (c != '\'')
						{
							if (c != '/')
							{
								goto IL_05C0;
							}
							stringBuilder.Append(dtfi.DateSeparator);
							num2 = 1;
							goto IL_05CC;
						}
						StringBuilder stringBuilder2 = new StringBuilder();
						num2 = DateTimeFormat.ParseQuoteString(format, i, stringBuilder2);
						stringBuilder.Append(stringBuilder2);
					}
					else if (c <= 'F')
					{
						if (c != ':')
						{
							if (c != 'F')
							{
								goto IL_05C0;
							}
							goto IL_01D8;
						}
						else
						{
							stringBuilder.Append(dtfi.TimeSeparator);
							num2 = 1;
						}
					}
					else if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_05C0;
						}
						num2 = 1;
						DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, stringBuilder);
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(stringBuilder, dateTime.Hour, num2);
					}
				}
				else if (c <= 'm')
				{
					if (c <= '\\')
					{
						if (c != 'M')
						{
							if (c != '\\')
							{
								goto IL_05C0;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num < 0)
							{
								throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
							}
							stringBuilder.Append((char)num);
							num2 = 2;
						}
						else
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int month = calendar.GetMonth(dateTime);
							if (num2 <= 2)
							{
								if (flag)
								{
									DateTimeFormat.HebrewFormatDigits(stringBuilder, month);
								}
								else
								{
									DateTimeFormat.FormatDigits(stringBuilder, month, num2);
								}
							}
							else if (flag)
							{
								stringBuilder.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num2, dtfi));
							}
							else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num2 >= 4)
							{
								stringBuilder.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, i, num2, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
							}
							else
							{
								stringBuilder.Append(DateTimeFormat.FormatMonth(month, num2, dtfi));
							}
							flag2 = false;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 <= 2)
							{
								int dayOfMonth = calendar.GetDayOfMonth(dateTime);
								if (flag)
								{
									DateTimeFormat.HebrewFormatDigits(stringBuilder, dayOfMonth);
								}
								else
								{
									DateTimeFormat.FormatDigits(stringBuilder, dayOfMonth, num2);
								}
							}
							else
							{
								int dayOfWeek = (int)calendar.GetDayOfWeek(dateTime);
								stringBuilder.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num2, dtfi));
							}
							flag2 = false;
							break;
						case 'e':
							goto IL_05C0;
						case 'f':
							goto IL_01D8;
						case 'g':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							stringBuilder.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
							break;
						case 'h':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = dateTime.Hour % 12;
							if (num3 == 0)
							{
								num3 = 12;
							}
							DateTimeFormat.FormatDigits(stringBuilder, num3, num2);
							break;
						}
						default:
							if (c != 'm')
							{
								goto IL_05C0;
							}
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							DateTimeFormat.FormatDigits(stringBuilder, dateTime.Minute, num2);
							break;
						}
					}
				}
				else if (c <= 't')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_05C0;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 == 1)
						{
							if (dateTime.Hour < 12)
							{
								if (dtfi.AMDesignator.Length >= 1)
								{
									stringBuilder.Append(dtfi.AMDesignator[0]);
								}
							}
							else if (dtfi.PMDesignator.Length >= 1)
							{
								stringBuilder.Append(dtfi.PMDesignator[0]);
							}
						}
						else
						{
							stringBuilder.Append((dateTime.Hour < 12) ? dtfi.AMDesignator : dtfi.PMDesignator);
						}
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(stringBuilder, dateTime.Second, num2);
					}
				}
				else if (c != 'y')
				{
					if (c != 'z')
					{
						goto IL_05C0;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num2, flag2, stringBuilder);
				}
				else
				{
					int year = calendar.GetYear(dateTime);
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (dtfi.HasForceTwoDigitYears)
					{
						DateTimeFormat.FormatDigits(stringBuilder, year, (num2 <= 2) ? num2 : 2);
					}
					else if (calendar.ID == 8)
					{
						DateTimeFormat.HebrewFormatDigits(stringBuilder, year);
					}
					else if (num2 <= 2)
					{
						DateTimeFormat.FormatDigits(stringBuilder, year % 100, num2);
					}
					else
					{
						string text = "D" + num2;
						stringBuilder.Append(year.ToString(text, CultureInfo.InvariantCulture));
					}
					flag2 = false;
				}
				IL_05CC:
				i += num2;
				continue;
				IL_01D8:
				num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
				if (num2 > 7)
				{
					throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
				}
				long num4 = dateTime.Ticks % 10000000L;
				num4 /= (long)Math.Pow(10.0, (double)(7 - num2));
				if (c == 'f')
				{
					stringBuilder.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num2 - 1], CultureInfo.InvariantCulture));
					goto IL_05CC;
				}
				int num5 = num2;
				while (num5 > 0 && num4 % 10L == 0L)
				{
					num4 /= 10L;
					num5--;
				}
				if (num5 > 0)
				{
					stringBuilder.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture));
					goto IL_05CC;
				}
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '.')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					goto IL_05CC;
				}
				goto IL_05CC;
				IL_05C0:
				stringBuilder.Append(c);
				num2 = 1;
				goto IL_05CC;
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, string format, int tokenLen, bool timeOnly, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				if (timeOnly && dateTime.Ticks < 864000000000L)
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					DateTimeFormat.InvalidFormatForUtc(format, dateTime);
					dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			if (tokenLen <= 1)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, "{0:0}", offset.Hours);
				return;
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}", offset.Hours);
			if (tokenLen >= 3)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, ":{0:00}", offset.Minutes);
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003F8F0 File Offset: 0x0003DAF0
		private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				DateTimeKind kind = dateTime.Kind;
				if (kind == DateTimeKind.Utc)
				{
					result.Append("Z");
					return;
				}
				if (kind != DateTimeKind.Local)
				{
					return;
				}
				offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}:{1:00}", offset.Hours, offset.Minutes);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003F988 File Offset: 0x0003DB88
		internal static string GetRealFormat(string format, DateTimeFormatInfo dtfi)
		{
			char c = format[0];
			if (c > 'U')
			{
				if (c != 'Y')
				{
					switch (c)
					{
					case 'd':
						return dtfi.ShortDatePattern;
					case 'e':
						goto IL_0159;
					case 'f':
						return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
					case 'g':
						return dtfi.GeneralShortTimePattern;
					default:
						switch (c)
						{
						case 'm':
							goto IL_0109;
						case 'n':
						case 'p':
						case 'q':
						case 'v':
						case 'w':
						case 'x':
							goto IL_0159;
						case 'o':
							goto IL_0112;
						case 'r':
							goto IL_011A;
						case 's':
							return dtfi.SortableDateTimePattern;
						case 't':
							return dtfi.ShortTimePattern;
						case 'u':
							return dtfi.UniversalSortableDateTimePattern;
						case 'y':
							break;
						default:
							goto IL_0159;
						}
						break;
					}
				}
				return dtfi.YearMonthPattern;
			}
			switch (c)
			{
			case 'D':
				return dtfi.LongDatePattern;
			case 'E':
				goto IL_0159;
			case 'F':
				return dtfi.FullDateTimePattern;
			case 'G':
				return dtfi.GeneralLongTimePattern;
			default:
				switch (c)
				{
				case 'M':
					break;
				case 'N':
				case 'P':
				case 'Q':
				case 'S':
					goto IL_0159;
				case 'O':
					goto IL_0112;
				case 'R':
					goto IL_011A;
				case 'T':
					return dtfi.LongTimePattern;
				case 'U':
					return dtfi.FullDateTimePattern;
				default:
					goto IL_0159;
				}
				break;
			}
			IL_0109:
			return dtfi.MonthDayPattern;
			IL_0112:
			return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			IL_011A:
			return dtfi.RFC1123Pattern;
			IL_0159:
			throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003FB00 File Offset: 0x0003DD00
		private static string ExpandPredefinedFormat(string format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
		{
			char c = format[0];
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_0160;
					}
					goto IL_005A;
				}
			}
			else if (c != 'U')
			{
				switch (c)
				{
				case 'o':
					break;
				case 'p':
				case 'q':
				case 't':
					goto IL_0160;
				case 'r':
					goto IL_005A;
				case 's':
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_0160;
				case 'u':
					if (offset != DateTimeFormat.NullOffset)
					{
						dateTime -= offset;
					}
					else if (dateTime.Kind == DateTimeKind.Local)
					{
						DateTimeFormat.InvalidFormatForLocal(format, dateTime);
					}
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_0160;
				default:
					goto IL_0160;
				}
			}
			else
			{
				if (offset != DateTimeFormat.NullOffset)
				{
					throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
				}
				dtfi = (DateTimeFormatInfo)dtfi.Clone();
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
				}
				dateTime = dateTime.ToUniversalTime();
				goto IL_0160;
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			goto IL_0160;
			IL_005A:
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			else if (dateTime.Kind == DateTimeKind.Local)
			{
				DateTimeFormat.InvalidFormatForLocal(format, dateTime);
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			IL_0160:
			format = DateTimeFormat.GetRealFormat(format, dtfi);
			return format;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003FC78 File Offset: 0x0003DE78
		internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi)
		{
			return DateTimeFormat.Format(dateTime, format, dtfi, DateTimeFormat.NullOffset);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003FC88 File Offset: 0x0003DE88
		internal static string Format(DateTime dateTime, string format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			if (format == null || format.Length == 0)
			{
				bool flag = false;
				if (dateTime.Ticks < 864000000000L)
				{
					int id = dtfi.Calendar.ID;
					switch (id)
					{
					case 3:
					case 4:
					case 6:
					case 8:
						break;
					case 5:
					case 7:
						goto IL_0063;
					default:
						if (id != 13 && id - 22 > 1)
						{
							goto IL_0063;
						}
						break;
					}
					flag = true;
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
				IL_0063:
				if (offset == DateTimeFormat.NullOffset)
				{
					if (flag)
					{
						format = "s";
					}
					else
					{
						format = "G";
					}
				}
				else if (flag)
				{
					format = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";
				}
				else
				{
					format = dtfi.DateTimeOffsetPattern;
				}
			}
			if (format.Length == 1)
			{
				format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
			}
			return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003FD50 File Offset: 0x0003DF50
		internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
		{
			string[] array;
			string[] array2;
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
				case 'F':
				case 'G':
					break;
				case 'E':
					goto IL_0140;
				default:
					switch (format)
					{
					case 'M':
					case 'T':
						break;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_0140;
					case 'O':
					case 'R':
						goto IL_011E;
					case 'U':
					{
						DateTime dateTime2 = dateTime.ToUniversalTime();
						array = dtfi.GetAllDateTimePatterns(format);
						array2 = new string[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = DateTimeFormat.Format(dateTime2, array[i], dtfi);
						}
						return array2;
					}
					default:
						goto IL_0140;
					}
					break;
				}
			}
			else if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
				case 'f':
				case 'g':
					break;
				case 'e':
					goto IL_0140;
				default:
					switch (format)
					{
					case 'm':
					case 't':
					case 'y':
						break;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_0140;
					case 'o':
					case 'r':
					case 's':
					case 'u':
						goto IL_011E;
					default:
						goto IL_0140;
					}
					break;
				}
			}
			array = dtfi.GetAllDateTimePatterns(format);
			array2 = new string[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = DateTimeFormat.Format(dateTime, array[j], dtfi);
			}
			return array2;
			IL_011E:
			return new string[] { DateTimeFormat.Format(dateTime, new string(new char[] { format }), dtfi) };
			IL_0140:
			throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003FEB0 File Offset: 0x0003E0B0
		internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimes = DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[i], dtfi);
				for (int j = 0; j < allDateTimes.Length; j++)
				{
					list.Add(allDateTimes[j]);
				}
			}
			string[] array = new string[list.Count];
			list.CopyTo(0, array, 0, list.Count);
			return array;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00002194 File Offset: 0x00000394
		internal static void InvalidFormatForLocal(string format, DateTime dateTime)
		{
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00002194 File Offset: 0x00000394
		[SecuritySafeCritical]
		internal static void InvalidFormatForUtc(string format, DateTime dateTime)
		{
		}

		// Token: 0x04000932 RID: 2354
		internal const int MaxSecondsFractionDigits = 7;

		// Token: 0x04000933 RID: 2355
		internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;

		// Token: 0x04000934 RID: 2356
		internal static char[] allStandardFormats = new char[]
		{
			'd', 'D', 'f', 'F', 'g', 'G', 'm', 'M', 'o', 'O',
			'r', 'R', 's', 't', 'T', 'u', 'U', 'y', 'Y'
		};

		// Token: 0x04000935 RID: 2357
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x04000936 RID: 2358
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x04000937 RID: 2359
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x04000938 RID: 2360
		internal static string[] fixedNumberFormats = new string[] { "0", "00", "000", "0000", "00000", "000000", "0000000" };
	}
}
