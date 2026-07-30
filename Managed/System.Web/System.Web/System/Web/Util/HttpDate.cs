using System;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x0200012A RID: 298
	internal static class HttpDate
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x00026668 File Offset: 0x00024868
		private static int atoi2(string s, int startIndex)
		{
			int num3;
			try
			{
				int num = (int)(s[startIndex] - '0');
				int num2 = (int)(s[1 + startIndex] - '0');
				num3 = HttpDate.s_tensDigit[num] + num2;
			}
			catch
			{
				throw new FormatException(global::SR.GetString("Unable to convert two characters in the string '{0}' to a number starting at offset {1}.", new object[] { s, startIndex }));
			}
			return num3;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000266D0 File Offset: 0x000248D0
		private static int make_month(string s, int startIndex)
		{
			int num = (int)((s[2 + startIndex] - '@') & '?');
			sbyte b = HttpDate.s_monthIndexTable[num];
			if (b >= 13)
			{
				if (b == 78)
				{
					if (HttpDate.s_monthIndexTable[(int)((s[1 + startIndex] - '@') & '?')] == 65)
					{
						b = 1;
					}
					else
					{
						b = 6;
					}
				}
				else
				{
					if (b != 82)
					{
						throw new FormatException(global::SR.GetString("Unable to convert characters in the string '{0}' to a month starting at offset {1}.", new object[] { s, startIndex }));
					}
					if (HttpDate.s_monthIndexTable[(int)((s[1 + startIndex] - '@') & '?')] == 65)
					{
						b = 3;
					}
					else
					{
						b = 4;
					}
				}
			}
			string text = HttpDate.s_months[(int)(b - 1)];
			if (s[startIndex] == text[0] && s[1 + startIndex] == text[1] && s[2 + startIndex] == text[2])
			{
				return (int)b;
			}
			if (char.ToUpper(s[startIndex], CultureInfo.InvariantCulture) == text[0] && char.ToLower(s[1 + startIndex], CultureInfo.InvariantCulture) == text[1] && char.ToLower(s[2 + startIndex], CultureInfo.InvariantCulture) == text[2])
			{
				return (int)b;
			}
			throw new FormatException(global::SR.GetString("Unable to convert characters in the string '{0}' to a month starting at offset {1}.", new object[] { s, startIndex }));
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00026820 File Offset: 0x00024A20
		internal static DateTime UtcParse(string time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			int num;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			if ((num = time.IndexOf(',')) != -1)
			{
				int num2 = time.Length - num;
				while (--num2 > 0 && time[++num] == ' ')
				{
				}
				if (time[num + 2] == '-')
				{
					if (num2 < 18)
					{
						throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", new object[] { time }));
					}
					num3 = HttpDate.atoi2(time, num);
					num4 = HttpDate.make_month(time, num + 3);
					num5 = HttpDate.atoi2(time, num + 7);
					if (num5 < 50)
					{
						num5 += 2000;
					}
					else
					{
						num5 += 1900;
					}
					num6 = HttpDate.atoi2(time, num + 10);
					num7 = HttpDate.atoi2(time, num + 13);
					num8 = HttpDate.atoi2(time, num + 16);
				}
				else
				{
					if (num2 < 20)
					{
						throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", new object[] { time }));
					}
					num3 = HttpDate.atoi2(time, num);
					num4 = HttpDate.make_month(time, num + 3);
					num5 = HttpDate.atoi2(time, num + 7) * 100 + HttpDate.atoi2(time, num + 9);
					num6 = HttpDate.atoi2(time, num + 12);
					num7 = HttpDate.atoi2(time, num + 15);
					num8 = HttpDate.atoi2(time, num + 18);
				}
			}
			else
			{
				num = -1;
				int num9 = time.Length + 1;
				while (--num9 > 0 && time[++num] == ' ')
				{
				}
				if (num9 < 24)
				{
					throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", new object[] { time }));
				}
				num3 = HttpDate.atoi2(time, num + 8);
				num4 = HttpDate.make_month(time, num + 4);
				num5 = HttpDate.atoi2(time, num + 20) * 100 + HttpDate.atoi2(time, num + 22);
				num6 = HttpDate.atoi2(time, num + 11);
				num7 = HttpDate.atoi2(time, num + 14);
				num8 = HttpDate.atoi2(time, num + 17);
			}
			return new DateTime(num5, num4, num3, num6, num7, num8, DateTimeKind.Utc);
		}

		// Token: 0x040011BF RID: 4543
		private static readonly int[] s_tensDigit = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };

		// Token: 0x040011C0 RID: 4544
		private static readonly string[] s_days = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

		// Token: 0x040011C1 RID: 4545
		private static readonly string[] s_months = new string[]
		{
			"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
			"Nov", "Dec"
		};

		// Token: 0x040011C2 RID: 4546
		private static readonly sbyte[] s_monthIndexTable = new sbyte[]
		{
			-1, 65, 2, 12, -1, -1, -1, 8, -1, -1,
			-1, -1, 7, -1, 78, -1, 9, -1, 82, -1,
			10, -1, 11, -1, -1, 5, -1, -1, -1, -1,
			-1, -1, -1, 65, 2, 12, -1, -1, -1, 8,
			-1, -1, -1, -1, 7, -1, 78, -1, 9, -1,
			82, -1, 10, -1, 11, -1, -1, 5, -1, -1,
			-1, -1, -1, -1
		};
	}
}
