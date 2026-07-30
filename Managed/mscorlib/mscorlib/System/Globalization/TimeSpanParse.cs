using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200042E RID: 1070
	internal static class TimeSpanParse
	{
		// Token: 0x06003366 RID: 13158 RVA: 0x000B84A0 File Offset: 0x000B66A0
		internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined TimeSpanStyles value is being used."), parameterName);
			}
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000B84BC File Offset: 0x000B66BC
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days.IsInvalidNumber(10675199, -1) || hours.IsInvalidNumber(23, -1) || minutes.IsInvalidNumber(59, -1) || seconds.IsInvalidNumber(59, -1) || fraction.IsInvalidNumber(9999999, 7))
			{
				result = 0L;
				return false;
			}
			long num = ((long)days.num * 3600L * 24L + (long)hours.num * 3600L + (long)minutes.num * 60L + (long)seconds.num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction.num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction.zeroes > 0)
				{
					long num4 = (long)Math.Pow(10.0, (double)fraction.zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000B85D4 File Offset: 0x000B67D4
		internal static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000B860C File Offset: 0x000B680C
		internal static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000B864C File Offset: 0x000B684C
		internal static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000B8684 File Offset: 0x000B6884
		internal static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000B86C8 File Offset: 0x000B68C8
		internal static TimeSpan ParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000B8700 File Offset: 0x000B6900
		internal static bool TryParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000B8744 File Offset: 0x000B6944
		private static bool TryParseTimeSpan(string input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "input");
				return false;
			}
			input = input.Trim();
			if (input == string.Empty)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken timeSpanToken = timeSpanTokenizer.GetNextToken();
			while (timeSpanToken.ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref timeSpanToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
					return false;
				}
				timeSpanToken = timeSpanTokenizer.GetNextToken();
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			if (!TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			return true;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000B8818 File Offset: 0x000B6A18
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				timeSpanToken.sep = string.Empty;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
					return false;
				}
			}
			switch (raw.NumCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000B88C4 File Offset: 0x000B6AC4
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 6 || raw.NumCount != 5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], raw.numbers[4], out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000B89E4 File Offset: 0x000B6BE4
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 5 || raw.NumCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
			return false;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000B8EE8 File Offset: 0x000B70E8
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 4 || raw.NumCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
			return false;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000B9384 File Offset: 0x000B7584
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 3 || raw.NumCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000B9498 File Offset: 0x000B7698
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 2 || raw.NumCount != 1 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000B95A4 File Offset: 0x000B77A4
		private static bool TryParseExactTimeSpan(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "input");
				return false;
			}
			if (format == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "format");
				return false;
			}
			if (format.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.");
				return false;
			}
			if (format.Length != 1)
			{
				return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
			}
			if (format[0] == 'c' || format[0] == 't' || format[0] == 'T')
			{
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
			}
			TimeSpanParse.TimeSpanStandardStyles timeSpanStandardStyles;
			if (format[0] == 'g')
			{
				timeSpanStandardStyles = TimeSpanParse.TimeSpanStandardStyles.Localized;
			}
			else
			{
				if (format[0] != 'G')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.");
					return false;
				}
				timeSpanStandardStyles = TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull;
			}
			return TimeSpanParse.TryParseTimeSpan(input, timeSpanStandardStyles, formatProvider, ref result);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000B9670 File Offset: 0x000B7870
		private static bool TryParseByFormat(string input, string format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int i = 0;
			int num7 = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input, -1);
			while (i < format.Length)
			{
				char c = format[i];
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_02C5;
							}
							int num8 = DateTimeFormat.ParseNextChar(format, i);
							if (num8 >= 0 && num8 != 37)
							{
								num7 = 1;
								goto IL_02D3;
							}
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
							return false;
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_02C5;
						}
						num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num7 > 7 || flag5)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
							return false;
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6);
						flag5 = true;
						goto IL_02D3;
					}
					StringBuilder stringBuilder = new StringBuilder();
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num7))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Cannot find a matching quote character for the character '{0}'.", c);
						return false;
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
						return false;
					}
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num9 = 0;
							if (num7 > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num7 < 2) ? 1 : num7, (num7 < 2) ? 8 : num7, out num9, out num))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
								return false;
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_02C5;
						case 'f':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
								return false;
							}
							flag5 = true;
							break;
						case 'h':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num2))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
								return false;
							}
							flag2 = true;
							break;
						default:
							goto IL_02C5;
						}
					}
					else
					{
						int num8 = DateTimeFormat.ParseNextChar(format, i);
						if (num8 < 0 || timeSpanTokenizer.NextChar != (char)num8)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
							return false;
						}
						num7 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_02C5;
					}
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num4))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
						return false;
					}
					flag4 = true;
				}
				else
				{
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num3))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
						return false;
					}
					flag3 = true;
				}
				IL_02D3:
				i += num7;
				continue;
				IL_02C5:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.");
				return false;
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			long num10 = 0L;
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(num), new TimeSpanParse.TimeSpanToken(num2), new TimeSpanParse.TimeSpanToken(num3), new TimeSpanParse.TimeSpanToken(num4), new TimeSpanParse.TimeSpanToken(num5, num6), out num10))
			{
				if (!flag6)
				{
					num10 = -num10;
				}
				result.parsedTimeSpan._ticks = num10;
				return true;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
			return false;
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000B99DC File Offset: 0x000B7BDC
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			result = 0;
			int num = 0;
			int num2 = ((minDigitLength == 1) ? 2 : minDigitLength);
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, num2, out num, out result);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000B9A04 File Offset: 0x000B7C04
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			result = 0;
			zeroes = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				result = result * 10 + (int)(nextChar - '0');
				if (result == 0)
				{
					zeroes++;
				}
			}
			return i >= minDigitLength;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000B9A60 File Offset: 0x000B7C60
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000B9A90 File Offset: 0x000B7C90
		private static bool TryParseTimeSpanConstant(string input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000B9AB0 File Offset: 0x000B7CB0
		private static bool TryParseExactMultipleTimeSpan(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "input");
				return false;
			}
			if (formats == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "formats");
				return false;
			}
			if (input.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
				return false;
			}
			if (formats.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.");
				return false;
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.");
					return false;
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
				timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
				if (TimeSpanParse.TryParseExactTimeSpan(input, formats[i], formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
			return false;
		}

		// Token: 0x04001B2B RID: 6955
		internal const int unlimitedDigits = -1;

		// Token: 0x04001B2C RID: 6956
		internal const int maxFractionDigits = 7;

		// Token: 0x04001B2D RID: 6957
		internal const int maxDays = 10675199;

		// Token: 0x04001B2E RID: 6958
		internal const int maxHours = 23;

		// Token: 0x04001B2F RID: 6959
		internal const int maxMinutes = 59;

		// Token: 0x04001B30 RID: 6960
		internal const int maxSeconds = 59;

		// Token: 0x04001B31 RID: 6961
		internal const int maxFraction = 9999999;

		// Token: 0x04001B32 RID: 6962
		private static readonly TimeSpanParse.TimeSpanToken zero = new TimeSpanParse.TimeSpanToken(0);

		// Token: 0x0200042F RID: 1071
		private enum TimeSpanThrowStyle
		{
			// Token: 0x04001B34 RID: 6964
			None,
			// Token: 0x04001B35 RID: 6965
			All
		}

		// Token: 0x02000430 RID: 1072
		private enum ParseFailureKind
		{
			// Token: 0x04001B37 RID: 6967
			None,
			// Token: 0x04001B38 RID: 6968
			ArgumentNull,
			// Token: 0x04001B39 RID: 6969
			Format,
			// Token: 0x04001B3A RID: 6970
			FormatWithParameter,
			// Token: 0x04001B3B RID: 6971
			Overflow
		}

		// Token: 0x02000431 RID: 1073
		[Flags]
		private enum TimeSpanStandardStyles
		{
			// Token: 0x04001B3D RID: 6973
			None = 0,
			// Token: 0x04001B3E RID: 6974
			Invariant = 1,
			// Token: 0x04001B3F RID: 6975
			Localized = 2,
			// Token: 0x04001B40 RID: 6976
			RequireFull = 4,
			// Token: 0x04001B41 RID: 6977
			Any = 3
		}

		// Token: 0x02000432 RID: 1074
		private enum TTT
		{
			// Token: 0x04001B43 RID: 6979
			None,
			// Token: 0x04001B44 RID: 6980
			End,
			// Token: 0x04001B45 RID: 6981
			Num,
			// Token: 0x04001B46 RID: 6982
			Sep,
			// Token: 0x04001B47 RID: 6983
			NumOverflow
		}

		// Token: 0x02000433 RID: 1075
		private struct TimeSpanToken
		{
			// Token: 0x0600337D RID: 13181 RVA: 0x000B9B8C File Offset: 0x000B7D8C
			public TimeSpanToken(int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = 0;
				this.sep = null;
			}

			// Token: 0x0600337E RID: 13182 RVA: 0x000B9BAA File Offset: 0x000B7DAA
			public TimeSpanToken(int leadingZeroes, int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = leadingZeroes;
				this.sep = null;
			}

			// Token: 0x0600337F RID: 13183 RVA: 0x000B9BC8 File Offset: 0x000B7DC8
			public bool IsInvalidNumber(int maxValue, int maxPrecision)
			{
				return this.num > maxValue || (maxPrecision != -1 && (this.zeroes > maxPrecision || (this.num != 0 && this.zeroes != 0 && (long)this.num >= (long)maxValue / (long)Math.Pow(10.0, (double)(this.zeroes - 1)))));
			}

			// Token: 0x04001B48 RID: 6984
			internal TimeSpanParse.TTT ttt;

			// Token: 0x04001B49 RID: 6985
			internal int num;

			// Token: 0x04001B4A RID: 6986
			internal int zeroes;

			// Token: 0x04001B4B RID: 6987
			internal string sep;
		}

		// Token: 0x02000434 RID: 1076
		private struct TimeSpanTokenizer
		{
			// Token: 0x06003380 RID: 13184 RVA: 0x000B9C2A File Offset: 0x000B7E2A
			internal void Init(string input)
			{
				this.Init(input, 0);
			}

			// Token: 0x06003381 RID: 13185 RVA: 0x000B9C34 File Offset: 0x000B7E34
			internal void Init(string input, int startPosition)
			{
				this.m_pos = startPosition;
				this.m_value = input;
			}

			// Token: 0x06003382 RID: 13186 RVA: 0x000B9C44 File Offset: 0x000B7E44
			internal TimeSpanParse.TimeSpanToken GetNextToken()
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				char c = this.CurrentChar;
				if (c == '\0')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.End;
					return timeSpanToken;
				}
				if (c >= '0' && c <= '9')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.Num;
					timeSpanToken.num = 0;
					timeSpanToken.zeroes = 0;
					while (((long)timeSpanToken.num & (long)((ulong)(-268435456))) == 0L)
					{
						timeSpanToken.num = timeSpanToken.num * 10 + (int)c - 48;
						if (timeSpanToken.num == 0)
						{
							timeSpanToken.zeroes++;
						}
						if (timeSpanToken.num < 0)
						{
							timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
							return timeSpanToken;
						}
						c = this.NextChar;
						if (c < '0' || c > '9')
						{
							return timeSpanToken;
						}
					}
					timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
					return timeSpanToken;
				}
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				int pos = this.m_pos;
				int num = 0;
				while (c != '\0' && (c < '0' || '9' < c))
				{
					c = this.NextChar;
					num++;
				}
				timeSpanToken.sep = this.m_value.Substring(pos, num);
				return timeSpanToken;
			}

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x06003383 RID: 13187 RVA: 0x000B9D3E File Offset: 0x000B7F3E
			internal bool EOL
			{
				get
				{
					return this.m_pos >= this.m_value.Length - 1;
				}
			}

			// Token: 0x06003384 RID: 13188 RVA: 0x000B9D58 File Offset: 0x000B7F58
			internal void BackOne()
			{
				if (this.m_pos > 0)
				{
					this.m_pos--;
				}
			}

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x06003385 RID: 13189 RVA: 0x000B9D71 File Offset: 0x000B7F71
			internal char NextChar
			{
				get
				{
					this.m_pos++;
					return this.CurrentChar;
				}
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06003386 RID: 13190 RVA: 0x000B9D87 File Offset: 0x000B7F87
			internal char CurrentChar
			{
				get
				{
					if (this.m_pos > -1 && this.m_pos < this.m_value.Length)
					{
						return this.m_value[this.m_pos];
					}
					return '\0';
				}
			}

			// Token: 0x04001B4C RID: 6988
			private int m_pos;

			// Token: 0x04001B4D RID: 6989
			private string m_value;
		}

		// Token: 0x02000435 RID: 1077
		private struct TimeSpanRawInfo
		{
			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06003387 RID: 13191 RVA: 0x000B9DB8 File Offset: 0x000B7FB8
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x06003388 RID: 13192 RVA: 0x000B9DBF File Offset: 0x000B7FBF
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06003389 RID: 13193 RVA: 0x000B9DC6 File Offset: 0x000B7FC6
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this.m_posLocInit)
					{
						this.m_posLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_posLoc.Init(this.m_fullPosPattern, false);
						this.m_posLocInit = true;
					}
					return this.m_posLoc;
				}
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x0600338A RID: 13194 RVA: 0x000B9DFB File Offset: 0x000B7FFB
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this.m_negLocInit)
					{
						this.m_negLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_negLoc.Init(this.m_fullNegPattern, false);
						this.m_negLocInit = true;
					}
					return this.m_negLoc;
				}
			}

			// Token: 0x0600338B RID: 13195 RVA: 0x000B9E30 File Offset: 0x000B8030
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.AppCompatLiteral == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x0600338C RID: 13196 RVA: 0x000B9EBC File Offset: 0x000B80BC
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.AppCompatLiteral == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x0600338D RID: 13197 RVA: 0x000B9F34 File Offset: 0x000B8134
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 6 && this.NumCount == 5 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.SecondFractionSep == this.literals[4] && pattern.End == this.literals[5];
			}

			// Token: 0x0600338E RID: 13198 RVA: 0x000B9FDD File Offset: 0x000B81DD
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 2 && this.NumCount == 1 && pattern.Start == this.literals[0] && pattern.End == this.literals[1];
			}

			// Token: 0x0600338F RID: 13199 RVA: 0x000BA020 File Offset: 0x000B8220
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 3 && this.NumCount == 2 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.End == this.literals[2];
			}

			// Token: 0x06003390 RID: 13200 RVA: 0x000BA084 File Offset: 0x000B8284
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06003391 RID: 13201 RVA: 0x000BA0FC File Offset: 0x000B82FC
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06003392 RID: 13202 RVA: 0x000BA174 File Offset: 0x000B8374
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06003393 RID: 13203 RVA: 0x000BA204 File Offset: 0x000B8404
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.SecondFractionSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06003394 RID: 13204 RVA: 0x000BA294 File Offset: 0x000B8494
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this.lastSeenTTT = TimeSpanParse.TTT.None;
				this.tokenCount = 0;
				this.SepCount = 0;
				this.NumCount = 0;
				this.literals = new string[6];
				this.numbers = new TimeSpanParse.TimeSpanToken[5];
				this.m_fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this.m_fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this.m_posLocInit = false;
				this.m_negLocInit = false;
			}

			// Token: 0x06003395 RID: 13205 RVA: 0x000BA2FC File Offset: 0x000B84FC
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				if (tok.ttt == TimeSpanParse.TTT.NumOverflow)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null);
					return false;
				}
				if (tok.ttt != TimeSpanParse.TTT.Sep && tok.ttt != TimeSpanParse.TTT.Num)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null);
					return false;
				}
				TimeSpanParse.TTT ttt = tok.ttt;
				if (ttt != TimeSpanParse.TTT.Num)
				{
					if (ttt == TimeSpanParse.TTT.Sep && !this.AddSep(tok.sep, ref result))
					{
						return false;
					}
				}
				else
				{
					if (this.tokenCount == 0 && !this.AddSep(string.Empty, ref result))
					{
						return false;
					}
					if (!this.AddNum(tok, ref result))
					{
						return false;
					}
				}
				this.lastSeenTTT = tok.ttt;
				return true;
			}

			// Token: 0x06003396 RID: 13206 RVA: 0x000BA398 File Offset: 0x000B8598
			private bool AddSep(string sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.SepCount >= 6 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null);
					return false;
				}
				string[] array = this.literals;
				int sepCount = this.SepCount;
				this.SepCount = sepCount + 1;
				array[sepCount] = sep;
				this.tokenCount++;
				return true;
			}

			// Token: 0x06003397 RID: 13207 RVA: 0x000BA3F0 File Offset: 0x000B85F0
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.NumCount >= 5 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null);
					return false;
				}
				TimeSpanParse.TimeSpanToken[] array = this.numbers;
				int numCount = this.NumCount;
				this.NumCount = numCount + 1;
				array[numCount] = num;
				this.tokenCount++;
				return true;
			}

			// Token: 0x04001B4E RID: 6990
			internal TimeSpanParse.TTT lastSeenTTT;

			// Token: 0x04001B4F RID: 6991
			internal int tokenCount;

			// Token: 0x04001B50 RID: 6992
			internal int SepCount;

			// Token: 0x04001B51 RID: 6993
			internal int NumCount;

			// Token: 0x04001B52 RID: 6994
			internal string[] literals;

			// Token: 0x04001B53 RID: 6995
			internal TimeSpanParse.TimeSpanToken[] numbers;

			// Token: 0x04001B54 RID: 6996
			private TimeSpanFormat.FormatLiterals m_posLoc;

			// Token: 0x04001B55 RID: 6997
			private TimeSpanFormat.FormatLiterals m_negLoc;

			// Token: 0x04001B56 RID: 6998
			private bool m_posLocInit;

			// Token: 0x04001B57 RID: 6999
			private bool m_negLocInit;

			// Token: 0x04001B58 RID: 7000
			private string m_fullPosPattern;

			// Token: 0x04001B59 RID: 7001
			private string m_fullNegPattern;

			// Token: 0x04001B5A RID: 7002
			private const int MaxTokens = 11;

			// Token: 0x04001B5B RID: 7003
			private const int MaxLiteralTokens = 6;

			// Token: 0x04001B5C RID: 7004
			private const int MaxNumericTokens = 5;
		}

		// Token: 0x02000436 RID: 1078
		private struct TimeSpanResult
		{
			// Token: 0x06003398 RID: 13208 RVA: 0x000BA44B File Offset: 0x000B864B
			internal void Init(TimeSpanParse.TimeSpanThrowStyle canThrow)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this.throwStyle = canThrow;
			}

			// Token: 0x06003399 RID: 13209 RVA: 0x000BA460 File Offset: 0x000B8660
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null);
			}

			// Token: 0x0600339A RID: 13210 RVA: 0x000BA46C File Offset: 0x000B866C
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null);
			}

			// Token: 0x0600339B RID: 13211 RVA: 0x000BA478 File Offset: 0x000B8678
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				this.m_failureArgumentName = failureArgumentName;
				if (this.throwStyle != TimeSpanParse.TimeSpanThrowStyle.None)
				{
					throw this.GetTimeSpanParseException();
				}
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x000BA4A8 File Offset: 0x000B86A8
			internal Exception GetTimeSpanParseException()
			{
				switch (this.m_failure)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.Format:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case TimeSpanParse.ParseFailureKind.Overflow:
					return new OverflowException(Environment.GetResourceString(this.m_failureMessageID));
				default:
					return new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
				}
			}

			// Token: 0x04001B5D RID: 7005
			internal TimeSpan parsedTimeSpan;

			// Token: 0x04001B5E RID: 7006
			internal TimeSpanParse.TimeSpanThrowStyle throwStyle;

			// Token: 0x04001B5F RID: 7007
			internal TimeSpanParse.ParseFailureKind m_failure;

			// Token: 0x04001B60 RID: 7008
			internal string m_failureMessageID;

			// Token: 0x04001B61 RID: 7009
			internal object m_failureMessageFormatArgument;

			// Token: 0x04001B62 RID: 7010
			internal string m_failureArgumentName;
		}

		// Token: 0x02000437 RID: 1079
		private struct StringParser
		{
			// Token: 0x0600339D RID: 13213 RVA: 0x000BA540 File Offset: 0x000B8740
			internal void NextChar()
			{
				if (this.pos < this.len)
				{
					this.pos++;
				}
				this.ch = ((this.pos < this.len) ? this.str[this.pos] : '\0');
			}

			// Token: 0x0600339E RID: 13214 RVA: 0x000BA594 File Offset: 0x000B8794
			internal char NextNonDigit()
			{
				for (int i = this.pos; i < this.len; i++)
				{
					char c = this.str[i];
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x0600339F RID: 13215 RVA: 0x000BA5D4 File Offset: 0x000B87D4
			internal bool TryParse(string input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan._ticks = 0L;
				if (input == null)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "input");
					return false;
				}
				this.str = input;
				this.len = input.Length;
				this.pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this.ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this.ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
						return false;
					}
				}
				else if (num < 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
					return false;
				}
				this.SkipBlanks();
				if (this.pos < this.len)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
					return false;
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}

			// Token: 0x060033A0 RID: 13216 RVA: 0x000BA6F4 File Offset: 0x000B88F4
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int num = this.pos;
				while (this.ch >= '0' && this.ch <= '9')
				{
					if (((long)i & (long)((ulong)(-268435456))) != 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
						return false;
					}
					i = i * 10 + (int)this.ch - 48;
					if (i < 0)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
						return false;
					}
					this.NextChar();
				}
				if (num == this.pos)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
					return false;
				}
				if (i > max)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.");
					return false;
				}
				return true;
			}

			// Token: 0x060033A1 RID: 13217 RVA: 0x000BA790 File Offset: 0x000B8990
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this.ch != ':')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.");
					return false;
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this.ch == ':')
				{
					this.NextChar();
					if (this.ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this.ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this.ch >= '0' && this.ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this.ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x060033A2 RID: 13218 RVA: 0x000BA87D File Offset: 0x000B8A7D
			internal void SkipBlanks()
			{
				while (this.ch == ' ' || this.ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x04001B63 RID: 7011
			private string str;

			// Token: 0x04001B64 RID: 7012
			private char ch;

			// Token: 0x04001B65 RID: 7013
			private int pos;

			// Token: 0x04001B66 RID: 7014
			private int len;
		}
	}
}
