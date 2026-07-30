using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Mime
{
	// Token: 0x020005AE RID: 1454
	internal class SmtpDateTime
	{
		// Token: 0x06002D5D RID: 11613 RVA: 0x000B3A40 File Offset: 0x000B1C40
		internal static IDictionary<string, TimeSpan> InitializeShortHandLookups()
		{
			return new Dictionary<string, TimeSpan>
			{
				{
					"UT",
					TimeSpan.Zero
				},
				{
					"GMT",
					TimeSpan.Zero
				},
				{
					"EDT",
					new TimeSpan(-4, 0, 0)
				},
				{
					"EST",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CDT",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CST",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MDT",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MST",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PDT",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PST",
					new TimeSpan(-8, 0, 0)
				}
			};
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000B3B14 File Offset: 0x000B1D14
		internal SmtpDateTime(DateTime value)
		{
			this.date = value;
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				this.unknownTimeZone = true;
				return;
			case DateTimeKind.Utc:
				this.timeZone = TimeSpan.Zero;
				return;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				this.timeZone = this.ValidateAndGetSanitizedTimeSpan(utcOffset);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000B3B78 File Offset: 0x000B1D78
		internal SmtpDateTime(string value)
		{
			string text;
			this.date = this.ParseValue(value, out text);
			if (!this.TryParseTimeZoneString(text, out this.timeZone))
			{
				this.unknownTimeZone = true;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000B3BB0 File Offset: 0x000B1DB0
		internal DateTime Date
		{
			get
			{
				if (this.unknownTimeZone)
				{
					return DateTime.SpecifyKind(this.date, DateTimeKind.Unspecified);
				}
				DateTimeOffset dateTimeOffset = new DateTimeOffset(this.date, this.timeZone);
				return dateTimeOffset.LocalDateTime;
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000B3BEC File Offset: 0x000B1DEC
		public override string ToString()
		{
			if (this.unknownTimeZone)
			{
				return string.Format("{0} {1}", this.FormatDate(this.date), "-0000");
			}
			return string.Format("{0} {1}", this.FormatDate(this.date), this.TimeSpanToOffset(this.timeZone));
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000B3C40 File Offset: 0x000B1E40
		internal void ValidateAndGetTimeZoneOffsetValues(string offset, out bool positive, out int hours, out int minutes)
		{
			if (offset.Length != 5)
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			positive = offset.StartsWith("+");
			if (!int.TryParse(offset.Substring(1, 2), NumberStyles.None, CultureInfo.InvariantCulture, out hours))
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			if (!int.TryParse(offset.Substring(3, 2), NumberStyles.None, CultureInfo.InvariantCulture, out minutes))
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			if (minutes > 59)
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000B3CD8 File Offset: 0x000B1ED8
		internal void ValidateTimeZoneShortHandValue(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsLetter(value, i))
				{
					throw new FormatException(global::SR.GetString("An invalid character was found in the mail header: '{0}'."));
				}
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000B3D0F File Offset: 0x000B1F0F
		internal string FormatDate(DateTime value)
		{
			return value.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000B3D24 File Offset: 0x000B1F24
		internal DateTime ParseValue(string data, out string timeZone)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			int num = data.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException(global::SR.GetString("An invalid character was found in the mail header: '{0}'."));
			}
			int num2 = data.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars, num);
			if (num2 == -1)
			{
				throw new FormatException(global::SR.GetString("An invalid character was found in the mail header: '{0}'."));
			}
			DateTime dateTime;
			if (!DateTime.TryParseExact(data.Substring(0, num2).Trim(), SmtpDateTime.validDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			string text = data.Substring(num2).Trim();
			int num3 = text.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars);
			if (num3 != -1)
			{
				text = text.Substring(0, num3);
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			timeZone = text;
			return dateTime;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000B3E00 File Offset: 0x000B2000
		internal bool TryParseTimeZoneString(string timeZoneString, out TimeSpan timeZone)
		{
			timeZone = TimeSpan.Zero;
			if (timeZoneString == "-0000")
			{
				return false;
			}
			if (timeZoneString[0] == '+' || timeZoneString[0] == '-')
			{
				bool flag;
				int num;
				int num2;
				this.ValidateAndGetTimeZoneOffsetValues(timeZoneString, out flag, out num, out num2);
				if (!flag)
				{
					if (num != 0)
					{
						num *= -1;
					}
					else if (num2 != 0)
					{
						num2 *= -1;
					}
				}
				timeZone = new TimeSpan(num, num2, 0);
				return true;
			}
			this.ValidateTimeZoneShortHandValue(timeZoneString);
			if (SmtpDateTime.timeZoneOffsetLookup.ContainsKey(timeZoneString))
			{
				timeZone = SmtpDateTime.timeZoneOffsetLookup[timeZoneString];
				return true;
			}
			return false;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000B3E98 File Offset: 0x000B2098
		internal TimeSpan ValidateAndGetSanitizedTimeSpan(TimeSpan span)
		{
			TimeSpan timeSpan = new TimeSpan(span.Days, span.Hours, span.Minutes, 0, 0);
			if (Math.Abs(timeSpan.Ticks) > SmtpDateTime.timeSpanMaxTicks)
			{
				throw new FormatException(global::SR.GetString("The date is in an invalid format."));
			}
			return timeSpan;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000B3EE8 File Offset: 0x000B20E8
		internal string TimeSpanToOffset(TimeSpan span)
		{
			if (span.Ticks == 0L)
			{
				return "+0000";
			}
			uint num = (uint)Math.Abs(Math.Floor(span.TotalHours));
			uint num2 = (uint)Math.Abs(span.Minutes);
			string text = ((span.Ticks > 0L) ? "+" : "-");
			if (num < 10U)
			{
				text += "0";
			}
			text += num.ToString();
			if (num2 < 10U)
			{
				text += "0";
			}
			return text + num2.ToString();
		}

		// Token: 0x04002564 RID: 9572
		internal const string unknownTimeZoneDefaultOffset = "-0000";

		// Token: 0x04002565 RID: 9573
		internal const string utcDefaultTimeZoneOffset = "+0000";

		// Token: 0x04002566 RID: 9574
		internal const int offsetLength = 5;

		// Token: 0x04002567 RID: 9575
		internal const int maxMinuteValue = 59;

		// Token: 0x04002568 RID: 9576
		internal const string dateFormatWithDayOfWeek = "ddd, dd MMM yyyy HH:mm:ss";

		// Token: 0x04002569 RID: 9577
		internal const string dateFormatWithoutDayOfWeek = "dd MMM yyyy HH:mm:ss";

		// Token: 0x0400256A RID: 9578
		internal const string dateFormatWithDayOfWeekAndNoSeconds = "ddd, dd MMM yyyy HH:mm";

		// Token: 0x0400256B RID: 9579
		internal const string dateFormatWithoutDayOfWeekAndNoSeconds = "dd MMM yyyy HH:mm";

		// Token: 0x0400256C RID: 9580
		internal static readonly string[] validDateTimeFormats = new string[] { "ddd, dd MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss", "ddd, dd MMM yyyy HH:mm", "dd MMM yyyy HH:mm" };

		// Token: 0x0400256D RID: 9581
		internal static readonly char[] allowedWhiteSpaceChars = new char[] { ' ', '\t' };

		// Token: 0x0400256E RID: 9582
		internal static readonly IDictionary<string, TimeSpan> timeZoneOffsetLookup = SmtpDateTime.InitializeShortHandLookups();

		// Token: 0x0400256F RID: 9583
		internal static readonly long timeSpanMaxTicks = 3599400000000L;

		// Token: 0x04002570 RID: 9584
		internal static readonly int offsetMaxValue = 9959;

		// Token: 0x04002571 RID: 9585
		private readonly DateTime date;

		// Token: 0x04002572 RID: 9586
		private readonly TimeSpan timeZone;

		// Token: 0x04002573 RID: 9587
		private readonly bool unknownTimeZone;
	}
}
