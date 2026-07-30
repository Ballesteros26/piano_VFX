using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000486 RID: 1158
	internal static class HttpDateParse
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x000850D5 File Offset: 0x000832D5
		private static char MAKE_UPPER(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000850E4 File Offset: 0x000832E4
		private static int MapDayMonthToDword(char[] lpszDay, int index)
		{
			switch (HttpDateParse.MAKE_UPPER(lpszDay[index]))
			{
			case 'A':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'P')
				{
					return 4;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 8;
			}
			case 'D':
				return 12;
			case 'F':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'E')
				{
					return 2;
				}
				if (c == 'R')
				{
					return 5;
				}
				return -999;
			}
			case 'G':
				return -1000;
			case 'J':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'U')
					{
						c = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
						if (c == 'L')
						{
							return 7;
						}
						if (c == 'N')
						{
							return 6;
						}
					}
					return -999;
				}
				return 1;
			}
			case 'M':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c != 'A')
				{
					if (c == 'O')
					{
						return 1;
					}
				}
				else
				{
					c = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
					if (c == 'R')
					{
						return 3;
					}
					if (c == 'Y')
					{
						return 5;
					}
				}
				return -999;
			}
			case 'N':
				return 11;
			case 'O':
				return 10;
			case 'S':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'A')
				{
					return 6;
				}
				if (c == 'E')
				{
					return 9;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 0;
			}
			case 'T':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'H')
				{
					return 4;
				}
				if (c == 'U')
				{
					return 2;
				}
				return -999;
			}
			case 'U':
				return -1000;
			case 'W':
				return 3;
			}
			return -999;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00085278 File Offset: 0x00083478
		public static bool ParseHttpDate(string DateString, out DateTime dtOut)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			int[] array = new int[8];
			bool flag2 = true;
			char[] array2 = DateString.ToCharArray();
			dtOut = default(DateTime);
			while (num < DateString.Length && num2 < 8)
			{
				if (array2[num] >= '0' && array2[num] <= '9')
				{
					array[num2] = 0;
					do
					{
						array[num2] *= 10;
						array[num2] += (int)(array2[num] - '0');
						num++;
					}
					while (num < DateString.Length && array2[num] >= '0' && array2[num] <= '9');
					num2++;
				}
				else if ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z'))
				{
					array[num2] = HttpDateParse.MapDayMonthToDword(array2, num);
					num3 = num2;
					if (array[num2] == -999 && (!flag || num2 != 6))
					{
						flag2 = false;
						return flag2;
					}
					if (num2 == 1)
					{
						flag = true;
					}
					do
					{
						num++;
					}
					while (num < DateString.Length && ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z')));
					num2++;
				}
				else
				{
					num++;
				}
			}
			int num4 = 0;
			int num5;
			int num6;
			int num7;
			int num8;
			int num9;
			int num10;
			if (flag)
			{
				num5 = array[2];
				num6 = array[1];
				num7 = array[3];
				num8 = array[4];
				num9 = array[5];
				if (num3 != 6)
				{
					num10 = array[6];
				}
				else
				{
					num10 = array[7];
				}
			}
			else
			{
				num5 = array[1];
				num6 = array[2];
				num10 = array[3];
				num7 = array[4];
				num8 = array[5];
				num9 = array[6];
			}
			if (num10 < 100)
			{
				num10 += ((num10 < 80) ? 2000 : 1900);
			}
			if (num2 < 4 || num5 > 31 || num7 > 23 || num8 > 59 || num9 > 59)
			{
				return false;
			}
			dtOut = new DateTime(num10, num6, num5, num7, num8, num9, num4);
			if (num3 == 6)
			{
				dtOut = dtOut.ToUniversalTime();
			}
			if (num2 > 7 && array[7] != -1000)
			{
				double num11 = (double)array[7];
				dtOut.AddHours(num11);
			}
			dtOut = dtOut.ToLocalTime();
			return flag2;
		}

		// Token: 0x04001EB8 RID: 7864
		private const int BASE_DEC = 10;

		// Token: 0x04001EB9 RID: 7865
		private const int DATE_INDEX_DAY_OF_WEEK = 0;

		// Token: 0x04001EBA RID: 7866
		private const int DATE_1123_INDEX_DAY = 1;

		// Token: 0x04001EBB RID: 7867
		private const int DATE_1123_INDEX_MONTH = 2;

		// Token: 0x04001EBC RID: 7868
		private const int DATE_1123_INDEX_YEAR = 3;

		// Token: 0x04001EBD RID: 7869
		private const int DATE_1123_INDEX_HRS = 4;

		// Token: 0x04001EBE RID: 7870
		private const int DATE_1123_INDEX_MINS = 5;

		// Token: 0x04001EBF RID: 7871
		private const int DATE_1123_INDEX_SECS = 6;

		// Token: 0x04001EC0 RID: 7872
		private const int DATE_ANSI_INDEX_MONTH = 1;

		// Token: 0x04001EC1 RID: 7873
		private const int DATE_ANSI_INDEX_DAY = 2;

		// Token: 0x04001EC2 RID: 7874
		private const int DATE_ANSI_INDEX_HRS = 3;

		// Token: 0x04001EC3 RID: 7875
		private const int DATE_ANSI_INDEX_MINS = 4;

		// Token: 0x04001EC4 RID: 7876
		private const int DATE_ANSI_INDEX_SECS = 5;

		// Token: 0x04001EC5 RID: 7877
		private const int DATE_ANSI_INDEX_YEAR = 6;

		// Token: 0x04001EC6 RID: 7878
		private const int DATE_INDEX_TZ = 7;

		// Token: 0x04001EC7 RID: 7879
		private const int DATE_INDEX_LAST = 7;

		// Token: 0x04001EC8 RID: 7880
		private const int MAX_FIELD_DATE_ENTRIES = 8;

		// Token: 0x04001EC9 RID: 7881
		private const int DATE_TOKEN_JANUARY = 1;

		// Token: 0x04001ECA RID: 7882
		private const int DATE_TOKEN_FEBRUARY = 2;

		// Token: 0x04001ECB RID: 7883
		private const int DATE_TOKEN_Microsoft = 3;

		// Token: 0x04001ECC RID: 7884
		private const int DATE_TOKEN_APRIL = 4;

		// Token: 0x04001ECD RID: 7885
		private const int DATE_TOKEN_MAY = 5;

		// Token: 0x04001ECE RID: 7886
		private const int DATE_TOKEN_JUNE = 6;

		// Token: 0x04001ECF RID: 7887
		private const int DATE_TOKEN_JULY = 7;

		// Token: 0x04001ED0 RID: 7888
		private const int DATE_TOKEN_AUGUST = 8;

		// Token: 0x04001ED1 RID: 7889
		private const int DATE_TOKEN_SEPTEMBER = 9;

		// Token: 0x04001ED2 RID: 7890
		private const int DATE_TOKEN_OCTOBER = 10;

		// Token: 0x04001ED3 RID: 7891
		private const int DATE_TOKEN_NOVEMBER = 11;

		// Token: 0x04001ED4 RID: 7892
		private const int DATE_TOKEN_DECEMBER = 12;

		// Token: 0x04001ED5 RID: 7893
		private const int DATE_TOKEN_LAST_MONTH = 13;

		// Token: 0x04001ED6 RID: 7894
		private const int DATE_TOKEN_SUNDAY = 0;

		// Token: 0x04001ED7 RID: 7895
		private const int DATE_TOKEN_MONDAY = 1;

		// Token: 0x04001ED8 RID: 7896
		private const int DATE_TOKEN_TUESDAY = 2;

		// Token: 0x04001ED9 RID: 7897
		private const int DATE_TOKEN_WEDNESDAY = 3;

		// Token: 0x04001EDA RID: 7898
		private const int DATE_TOKEN_THURSDAY = 4;

		// Token: 0x04001EDB RID: 7899
		private const int DATE_TOKEN_FRIDAY = 5;

		// Token: 0x04001EDC RID: 7900
		private const int DATE_TOKEN_SATURDAY = 6;

		// Token: 0x04001EDD RID: 7901
		private const int DATE_TOKEN_LAST_DAY = 7;

		// Token: 0x04001EDE RID: 7902
		private const int DATE_TOKEN_GMT = -1000;

		// Token: 0x04001EDF RID: 7903
		private const int DATE_TOKEN_LAST = -1000;

		// Token: 0x04001EE0 RID: 7904
		private const int DATE_TOKEN_ERROR = -999;
	}
}
