using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
	// Token: 0x02000113 RID: 275
	internal static class IPv6AddressHelper
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00025B90 File Offset: 0x00023D90
		internal unsafe static string ParseCanonicalName(string str, int start, ref bool isLoopback, ref string scopeId)
		{
			ushort* ptr = stackalloc ushort[(UIntPtr)16];
			*(long*)ptr = 0L;
			*(long*)(ptr + 4) = 0L;
			isLoopback = IPv6AddressHelper.Parse(str, ptr, start, ref scopeId);
			return "[" + IPv6AddressHelper.CreateCanonicalName(ptr) + "]";
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00025BD0 File Offset: 0x00023DD0
		internal unsafe static string CreateCanonicalName(ushort* numbers)
		{
			if (UriParser.ShouldUseLegacyV2Quirks)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0:X4}:{1:X4}:{2:X4}:{3:X4}:{4:X4}:{5:X4}:{6:X4}:{7:X4}", new object[]
				{
					*numbers,
					numbers[1],
					numbers[2],
					numbers[3],
					numbers[4],
					numbers[5],
					numbers[6],
					numbers[7]
				});
			}
			KeyValuePair<int, int> keyValuePair = IPv6AddressHelper.FindCompressionRange(numbers);
			bool flag = IPv6AddressHelper.ShouldHaveIpv4Embedded(numbers);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 8; i++)
			{
				if (flag && i == 6)
				{
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, ":{0:d}.{1:d}.{2:d}.{3:d}", new object[]
					{
						numbers[i] >> 8,
						(int)(numbers[i] & 255),
						numbers[i + 1] >> 8,
						(int)(numbers[i + 1] & 255)
					}));
					break;
				}
				if (keyValuePair.Key == i)
				{
					stringBuilder.Append(":");
				}
				if (keyValuePair.Key <= i && keyValuePair.Value == 7)
				{
					stringBuilder.Append(":");
					break;
				}
				if (keyValuePair.Key > i || i > keyValuePair.Value)
				{
					if (i != 0)
					{
						stringBuilder.Append(":");
					}
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0:x}", numbers[i]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00025D90 File Offset: 0x00023F90
		private unsafe static KeyValuePair<int, int> FindCompressionRange(ushort* numbers)
		{
			int num = 0;
			int num2 = -1;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (numbers[i] == 0)
				{
					num3++;
					if (num3 > num)
					{
						num = num3;
						num2 = i - num3 + 1;
					}
				}
				else
				{
					num3 = 0;
				}
			}
			if (num >= 2)
			{
				return new KeyValuePair<int, int>(num2, num2 + num - 1);
			}
			return new KeyValuePair<int, int>(-1, -1);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00025DE4 File Offset: 0x00023FE4
		private unsafe static bool ShouldHaveIpv4Embedded(ushort* numbers)
		{
			if (*numbers == 0 && numbers[1] == 0 && numbers[2] == 0 && numbers[3] == 0 && numbers[6] != 0)
			{
				if (numbers[4] == 0 && (numbers[5] == 0 || numbers[5] == 65535))
				{
					return true;
				}
				if (numbers[4] == 65535 && numbers[5] == 0)
				{
					return true;
				}
			}
			return numbers[4] == 0 && numbers[5] == 24318;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00025E6C File Offset: 0x0002406C
		private unsafe static bool InternalIsValid(char* name, int start, ref int end, bool validateStrictAddress)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			int num3 = 1;
			int i;
			for (i = start; i < end; i++)
			{
				if (flag3 ? (name[i] >= '0' && name[i] <= '9') : Uri.IsHexDigit(name[i]))
				{
					num2++;
					flag4 = false;
				}
				else
				{
					if (num2 > 4)
					{
						return false;
					}
					if (num2 != 0)
					{
						num++;
						num3 = i - num2;
					}
					char c = name[i];
					if (c <= '.')
					{
						if (c == '%')
						{
							while (++i != end)
							{
								if (name[i] == ']')
								{
									goto IL_00CD;
								}
								if (name[i] == '/')
								{
									goto IL_00FB;
								}
							}
							return false;
						}
						if (c != '.')
						{
							return false;
						}
						if (flag2)
						{
							return false;
						}
						i = end;
						if (!IPv4AddressHelper.IsValid(name, num3, ref i, true, false, false))
						{
							return false;
						}
						num++;
						flag2 = true;
						i--;
						goto IL_013D;
					}
					else
					{
						if (c == '/')
						{
							goto IL_00FB;
						}
						if (c != ':')
						{
							if (c != ']')
							{
								return false;
							}
						}
						else
						{
							if (i <= 0 || name[i - 1] != ':')
							{
								flag4 = true;
								goto IL_013D;
							}
							if (flag)
							{
								return false;
							}
							flag = true;
							flag4 = false;
							goto IL_013D;
						}
					}
					IL_00CD:
					start = i;
					i = end;
					goto IL_013F;
					IL_013D:
					num2 = 0;
					goto IL_013F;
					IL_00FB:
					if (validateStrictAddress)
					{
						return false;
					}
					if (num == 0 || flag3)
					{
						return false;
					}
					flag3 = true;
					flag4 = true;
					goto IL_013D;
				}
				IL_013F:;
			}
			if (flag3 && (num2 < 1 || num2 > 2))
			{
				return false;
			}
			int num4 = 8 + (flag3 ? 1 : 0);
			if (flag4 || num2 > 4 || !(flag ? (num < num4) : (num == num4)))
			{
				return false;
			}
			if (i == end + 1)
			{
				end = start + 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002600C File Offset: 0x0002420C
		internal unsafe static bool IsValid(char* name, int start, ref int end)
		{
			return IPv6AddressHelper.InternalIsValid(name, start, ref end, false);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00026017 File Offset: 0x00024217
		internal unsafe static bool IsValidStrict(char* name, int start, ref int end)
		{
			return IPv6AddressHelper.InternalIsValid(name, start, ref end, true);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00026024 File Offset: 0x00024224
		internal unsafe static bool Parse(string address, ushort* numbers, int start, ref string scopeId)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = true;
			int num4 = 0;
			if (address[start] == '[')
			{
				start++;
			}
			int num5 = start;
			while (num5 < address.Length && address[num5] != ']')
			{
				char c = address[num5];
				if (c != '%')
				{
					if (c != '/')
					{
						if (c != ':')
						{
							num = num * 16 + Uri.FromHex(address[num5++]);
						}
						else
						{
							numbers[num2++] = (ushort)num;
							num = 0;
							num5++;
							if (address[num5] == ':')
							{
								num3 = num2;
								num5++;
							}
							else if (num3 < 0 && num2 < 6)
							{
								continue;
							}
							int num6 = num5;
							while (address[num6] != ']' && address[num6] != ':' && address[num6] != '%' && address[num6] != '/')
							{
								if (num6 >= num5 + 4)
								{
									break;
								}
								if (address[num6] == '.')
								{
									while (address[num6] != ']' && address[num6] != '/' && address[num6] != '%')
									{
										num6++;
									}
									num = IPv4AddressHelper.ParseHostNumber(address, num5, num6);
									numbers[num2++] = (ushort)(num >> 16);
									numbers[num2++] = (ushort)num;
									num5 = num6;
									num = 0;
									flag = false;
									break;
								}
								num6++;
							}
						}
					}
					else
					{
						if (flag)
						{
							numbers[num2++] = (ushort)num;
							flag = false;
						}
						num5++;
						while (address[num5] != ']')
						{
							num4 = num4 * 10 + (int)(address[num5] - '0');
							num5++;
						}
					}
				}
				else
				{
					if (flag)
					{
						numbers[num2++] = (ushort)num;
						flag = false;
					}
					start = num5;
					num5++;
					while (address[num5] != ']' && address[num5] != '/')
					{
						num5++;
					}
					scopeId = address.Substring(start, num5 - start);
					while (address[num5] != ']')
					{
						num5++;
					}
				}
			}
			if (flag)
			{
				numbers[num2++] = (ushort)num;
			}
			if (num3 > 0)
			{
				int num7 = 7;
				int num8 = num2 - 1;
				for (int i = num2 - num3; i > 0; i--)
				{
					numbers[num7--] = numbers[num8];
					numbers[num8--] = 0;
				}
			}
			return *numbers == 0 && numbers[1] == 0 && numbers[2] == 0 && numbers[3] == 0 && numbers[4] == 0 && ((numbers[5] == 0 && numbers[6] == 0 && numbers[7] == 1) || (numbers[6] == 32512 && numbers[7] == 1 && (numbers[5] == 0 || numbers[5] == ushort.MaxValue)));
		}

		// Token: 0x04000D35 RID: 3381
		private const int NumberOfLabels = 8;

		// Token: 0x04000D36 RID: 3382
		private const string LegacyFormat = "{0:X4}:{1:X4}:{2:X4}:{3:X4}:{4:X4}:{5:X4}:{6:X4}:{7:X4}";

		// Token: 0x04000D37 RID: 3383
		private const string CanonicalNumberFormat = "{0:x}";

		// Token: 0x04000D38 RID: 3384
		private const string EmbeddedIPv4Format = ":{0:d}.{1:d}.{2:d}.{3:d}";

		// Token: 0x04000D39 RID: 3385
		private const string Separator = ":";
	}
}
