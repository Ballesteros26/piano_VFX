using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020004AD RID: 1197
	internal struct XsdDuration
	{
		// Token: 0x060030A4 RID: 12452 RVA: 0x00119380 File Offset: 0x00117580
		public XsdDuration(bool isNegative, int years, int months, int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (years < 0)
			{
				throw new ArgumentOutOfRangeException("years");
			}
			if (months < 0)
			{
				throw new ArgumentOutOfRangeException("months");
			}
			if (days < 0)
			{
				throw new ArgumentOutOfRangeException("days");
			}
			if (hours < 0)
			{
				throw new ArgumentOutOfRangeException("hours");
			}
			if (minutes < 0)
			{
				throw new ArgumentOutOfRangeException("minutes");
			}
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds");
			}
			if (nanoseconds < 0 || nanoseconds > 999999999)
			{
				throw new ArgumentOutOfRangeException("nanoseconds");
			}
			this.years = years;
			this.months = months;
			this.days = days;
			this.hours = hours;
			this.minutes = minutes;
			this.seconds = seconds;
			this.nanoseconds = (uint)nanoseconds;
			if (isNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0011944F File Offset: 0x0011764F
		public XsdDuration(TimeSpan timeSpan)
		{
			this = new XsdDuration(timeSpan, XsdDuration.DurationType.Duration);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0011945C File Offset: 0x0011765C
		public XsdDuration(TimeSpan timeSpan, XsdDuration.DurationType durationType)
		{
			long ticks = timeSpan.Ticks;
			bool flag;
			ulong num;
			if (ticks < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)ticks);
			}
			else
			{
				flag = false;
				num = (ulong)ticks;
			}
			if (durationType == XsdDuration.DurationType.YearMonthDuration)
			{
				int num2 = (int)(num / 315360000000000UL);
				int num3 = (int)(num % 315360000000000UL / 25920000000000UL);
				if (num3 == 12)
				{
					num2++;
					num3 = 0;
				}
				this = new XsdDuration(flag, num2, num3, 0, 0, 0, 0, 0);
				return;
			}
			this.nanoseconds = (uint)(num % 10000000UL) * 100U;
			if (flag)
			{
				this.nanoseconds |= 2147483648U;
			}
			this.years = 0;
			this.months = 0;
			this.days = (int)(num / 864000000000UL);
			this.hours = (int)(num / 36000000000UL % 24UL);
			this.minutes = (int)(num / 600000000UL % 60UL);
			this.seconds = (int)(num / 10000000UL % 60UL);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0011954F File Offset: 0x0011774F
		public XsdDuration(string s)
		{
			this = new XsdDuration(s, XsdDuration.DurationType.Duration);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0011955C File Offset: 0x0011775C
		public XsdDuration(string s, XsdDuration.DurationType durationType)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, durationType, out xsdDuration);
			if (ex != null)
			{
				throw ex;
			}
			this.years = xsdDuration.Years;
			this.months = xsdDuration.Months;
			this.days = xsdDuration.Days;
			this.hours = xsdDuration.Hours;
			this.minutes = xsdDuration.Minutes;
			this.seconds = xsdDuration.Seconds;
			this.nanoseconds = (uint)xsdDuration.Nanoseconds;
			if (xsdDuration.IsNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x001195EE File Offset: 0x001177EE
		public bool IsNegative
		{
			get
			{
				return (this.nanoseconds & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x001195FF File Offset: 0x001177FF
		public int Years
		{
			get
			{
				return this.years;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x00119607 File Offset: 0x00117807
		public int Months
		{
			get
			{
				return this.months;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x0011960F File Offset: 0x0011780F
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x00119617 File Offset: 0x00117817
		public int Hours
		{
			get
			{
				return this.hours;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0011961F File Offset: 0x0011781F
		public int Minutes
		{
			get
			{
				return this.minutes;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x00119627 File Offset: 0x00117827
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0011962F File Offset: 0x0011782F
		public int Nanoseconds
		{
			get
			{
				return (int)(this.nanoseconds & 2147483647U);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x0011963D File Offset: 0x0011783D
		public int Microseconds
		{
			get
			{
				return this.Nanoseconds / 1000;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x0011964B File Offset: 0x0011784B
		public int Milliseconds
		{
			get
			{
				return this.Nanoseconds / 1000000;
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0011965C File Offset: 0x0011785C
		public XsdDuration Normalize()
		{
			int num = this.Years;
			int num2 = this.Months;
			int num3 = this.Days;
			int num4 = this.Hours;
			int num5 = this.Minutes;
			int num6 = this.Seconds;
			checked
			{
				try
				{
					if (num2 >= 12)
					{
						num += num2 / 12;
						num2 %= 12;
					}
					if (num6 >= 60)
					{
						num5 += num6 / 60;
						num6 %= 60;
					}
					if (num5 >= 60)
					{
						num4 += num5 / 60;
						num5 %= 60;
					}
					if (num4 >= 24)
					{
						num3 += num4 / 24;
						num4 %= 24;
					}
				}
				catch (OverflowException)
				{
					throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[]
					{
						this.ToString(),
						"Duration"
					}));
				}
				return new XsdDuration(this.IsNegative, num, num2, num3, num4, num5, num6, this.Nanoseconds);
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0011973C File Offset: 0x0011793C
		public TimeSpan ToTimeSpan()
		{
			return this.ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x00119748 File Offset: 0x00117948
		public TimeSpan ToTimeSpan(XsdDuration.DurationType durationType)
		{
			TimeSpan timeSpan;
			Exception ex = this.TryToTimeSpan(durationType, out timeSpan);
			if (ex != null)
			{
				throw ex;
			}
			return timeSpan;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x00119765 File Offset: 0x00117965
		internal Exception TryToTimeSpan(out TimeSpan result)
		{
			return this.TryToTimeSpan(XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x00119770 File Offset: 0x00117970
		internal Exception TryToTimeSpan(XsdDuration.DurationType durationType, out TimeSpan result)
		{
			Exception ex = null;
			ulong num = 0UL;
			checked
			{
				try
				{
					if (durationType != XsdDuration.DurationType.DayTimeDuration)
					{
						num += ((ulong)this.years + (ulong)this.months / 12UL) * 365UL;
						num += (ulong)this.months % 12UL * 30UL;
					}
					if (durationType != XsdDuration.DurationType.YearMonthDuration)
					{
						num += (ulong)this.days;
						num *= 24UL;
						num += (ulong)this.hours;
						num *= 60UL;
						num += (ulong)this.minutes;
						num *= 60UL;
						num += (ulong)this.seconds;
						num *= 10000000UL;
						num += (ulong)this.Nanoseconds / 100UL;
					}
					else
					{
						num *= 864000000000UL;
					}
					if (this.IsNegative)
					{
						if (num == 9223372036854775808UL)
						{
							result = new TimeSpan(long.MinValue);
						}
						else
						{
							result = new TimeSpan(0L - (long)num);
						}
					}
					else
					{
						result = new TimeSpan((long)num);
					}
					return null;
				}
				catch (OverflowException)
				{
					result = TimeSpan.MinValue;
					ex = new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[] { durationType, "TimeSpan" }));
				}
				return ex;
			}
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x001198B0 File Offset: 0x00117AB0
		public override string ToString()
		{
			return this.ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x001198BC File Offset: 0x00117ABC
		internal string ToString(XsdDuration.DurationType durationType)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.IsNegative)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append('P');
			if (durationType != XsdDuration.DurationType.DayTimeDuration)
			{
				if (this.years != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.years));
					stringBuilder.Append('Y');
				}
				if (this.months != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.months));
					stringBuilder.Append('M');
				}
			}
			if (durationType != XsdDuration.DurationType.YearMonthDuration)
			{
				if (this.days != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.days));
					stringBuilder.Append('D');
				}
				if (this.hours != 0 || this.minutes != 0 || this.seconds != 0 || this.Nanoseconds != 0)
				{
					stringBuilder.Append('T');
					if (this.hours != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.hours));
						stringBuilder.Append('H');
					}
					if (this.minutes != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.minutes));
						stringBuilder.Append('M');
					}
					int num = this.Nanoseconds;
					if (this.seconds != 0 || num != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.seconds));
						if (num != 0)
						{
							stringBuilder.Append('.');
							int length = stringBuilder.Length;
							stringBuilder.Length += 9;
							int num2 = stringBuilder.Length - 1;
							for (int i = num2; i >= length; i--)
							{
								int num3 = num % 10;
								stringBuilder[i] = (char)(num3 + 48);
								if (num2 == i && num3 == 0)
								{
									num2--;
								}
								num /= 10;
							}
							stringBuilder.Length = num2 + 1;
						}
						stringBuilder.Append('S');
					}
				}
				if (stringBuilder[stringBuilder.Length - 1] == 'P')
				{
					stringBuilder.Append("T0S");
				}
			}
			else if (stringBuilder[stringBuilder.Length - 1] == 'P')
			{
				stringBuilder.Append("0M");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x00119AAE File Offset: 0x00117CAE
		internal static Exception TryParse(string s, out XsdDuration result)
		{
			return XsdDuration.TryParse(s, XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x00119AB8 File Offset: 0x00117CB8
		internal static Exception TryParse(string s, XsdDuration.DurationType durationType, out XsdDuration result)
		{
			XsdDuration.Parts parts = XsdDuration.Parts.HasNone;
			result = default(XsdDuration);
			s = s.Trim();
			int length = s.Length;
			int num = 0;
			int i = 0;
			if (num < length)
			{
				if (s[num] == '-')
				{
					num++;
					result.nanoseconds = 2147483648U;
				}
				else
				{
					result.nanoseconds = 0U;
				}
				if (num < length && s[num++] == 'P')
				{
					int num2;
					if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) == null)
					{
						if (num >= length)
						{
							goto IL_02B5;
						}
						if (s[num] == 'Y')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasYears;
							result.years = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'M')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasMonths;
							result.months = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'D')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasDays;
							result.days = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'T')
						{
							if (i != 0)
							{
								goto IL_02B5;
							}
							num++;
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
							if (s[num] == 'H')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasHours;
								result.hours = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (num >= length)
								{
									goto IL_02B5;
								}
							}
							if (s[num] == 'M')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasMinutes;
								result.minutes = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (num >= length)
								{
									goto IL_02B5;
								}
							}
							if (s[num] == '.')
							{
								num++;
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (XsdDuration.TryParseDigits(s, ref num, true, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (i == 0)
								{
									num2 = 0;
								}
								while (i > 9)
								{
									num2 /= 10;
									i--;
								}
								while (i < 9)
								{
									num2 *= 10;
									i++;
								}
								result.nanoseconds |= (uint)num2;
								if (num >= length || s[num] != 'S')
								{
									goto IL_02B5;
								}
								if (++num == length)
								{
									goto IL_0298;
								}
							}
							else if (s[num] == 'S')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
							}
						}
						if (i != 0 || num != length)
						{
							goto IL_02B5;
						}
						IL_0298:
						if (parts != XsdDuration.Parts.HasNone)
						{
							if (durationType == XsdDuration.DurationType.DayTimeDuration)
							{
								if ((parts & (XsdDuration.Parts)3) != XsdDuration.Parts.HasNone)
								{
									goto IL_02B5;
								}
							}
							else if (durationType == XsdDuration.DurationType.YearMonthDuration && (parts & (XsdDuration.Parts)(-4)) != XsdDuration.Parts.HasNone)
							{
								goto IL_02B5;
							}
							return null;
						}
						goto IL_02B5;
					}
					IL_02D8:
					return new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[] { s, durationType }));
				}
			}
			IL_02B5:
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, durationType }));
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x00119DC0 File Offset: 0x00117FC0
		private static string TryParseDigits(string s, ref int offset, bool eatDigits, out int result, out int numDigits)
		{
			int num = offset;
			int length = s.Length;
			result = 0;
			numDigits = 0;
			while (offset < length && s[offset] >= '0' && s[offset] <= '9')
			{
				int num2 = (int)(s[offset] - '0');
				if (result > (2147483647 - num2) / 10)
				{
					if (!eatDigits)
					{
						return "Value '{0}' was either too large or too small for {1}.";
					}
					numDigits = offset - num;
					while (offset < length && s[offset] >= '0' && s[offset] <= '9')
					{
						offset++;
					}
					return null;
				}
				else
				{
					result = result * 10 + num2;
					offset++;
				}
			}
			numDigits = offset - num;
			return null;
		}

		// Token: 0x04001FEA RID: 8170
		private int years;

		// Token: 0x04001FEB RID: 8171
		private int months;

		// Token: 0x04001FEC RID: 8172
		private int days;

		// Token: 0x04001FED RID: 8173
		private int hours;

		// Token: 0x04001FEE RID: 8174
		private int minutes;

		// Token: 0x04001FEF RID: 8175
		private int seconds;

		// Token: 0x04001FF0 RID: 8176
		private uint nanoseconds;

		// Token: 0x04001FF1 RID: 8177
		private const uint NegativeBit = 2147483648U;

		// Token: 0x020004AE RID: 1198
		private enum Parts
		{
			// Token: 0x04001FF3 RID: 8179
			HasNone,
			// Token: 0x04001FF4 RID: 8180
			HasYears,
			// Token: 0x04001FF5 RID: 8181
			HasMonths,
			// Token: 0x04001FF6 RID: 8182
			HasDays = 4,
			// Token: 0x04001FF7 RID: 8183
			HasHours = 8,
			// Token: 0x04001FF8 RID: 8184
			HasMinutes = 16,
			// Token: 0x04001FF9 RID: 8185
			HasSeconds = 32
		}

		// Token: 0x020004AF RID: 1199
		public enum DurationType
		{
			// Token: 0x04001FFB RID: 8187
			Duration,
			// Token: 0x04001FFC RID: 8188
			YearMonthDuration,
			// Token: 0x04001FFD RID: 8189
			DayTimeDuration
		}
	}
}
