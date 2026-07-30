using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System
{
	// Token: 0x020001A7 RID: 423
	[FriendAccessAllowed]
	internal class Number
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x00002111 File Offset: 0x00000311
		private Number()
		{
		}

		// Token: 0x060011B6 RID: 4534
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern bool NumberBufferToDecimal(byte* number, ref decimal value);

		// Token: 0x060011B7 RID: 4535
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool NumberBufferToDouble(byte* number, ref double value);

		// Token: 0x060011B8 RID: 4536 RVA: 0x00048635 File Offset: 0x00046835
		public static string FormatDecimal(decimal value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004863F File Offset: 0x0004683F
		public static string FormatDouble(double value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00048649 File Offset: 0x00046849
		public static string FormatInt32(int value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00048653 File Offset: 0x00046853
		public static string FormatUInt32(uint value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004865D File Offset: 0x0004685D
		public static string FormatInt64(long value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00048667 File Offset: 0x00046867
		public static string FormatUInt64(ulong value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00048671 File Offset: 0x00046871
		public static string FormatSingle(float value, string format, NumberFormatInfo info)
		{
			return NumberFormatter.NumberToString(format, value, info);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0002126B File Offset: 0x0001F46B
		internal unsafe static string FormatNumberBuffer(byte* number, string format, NumberFormatInfo info, char* allDigits)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004867C File Offset: 0x0004687C
		private static bool HexNumberToInt32(ref Number.NumberBuffer number, ref int value)
		{
			uint num = 0U;
			bool flag = Number.HexNumberToUInt32(ref number, ref num);
			value = (int)num;
			return flag;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00048698 File Offset: 0x00046898
		private static bool HexNumberToInt64(ref Number.NumberBuffer number, ref long value)
		{
			ulong num = 0UL;
			bool flag = Number.HexNumberToUInt64(ref number, ref num);
			value = (long)num;
			return flag;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000486B4 File Offset: 0x000468B4
		[SecuritySafeCritical]
		private unsafe static bool HexNumberToUInt32(ref Number.NumberBuffer number, ref uint value)
		{
			int num = number.scale;
			if (num > 10 || num < number.precision)
			{
				return false;
			}
			char* ptr = number.digits;
			uint num2 = 0U;
			while (--num >= 0)
			{
				if (num2 > 268435455U)
				{
					return false;
				}
				num2 *= 16U;
				if (*ptr != '\0')
				{
					uint num3 = num2;
					if (*ptr != '\0')
					{
						if (*ptr >= '0' && *ptr <= '9')
						{
							num3 += (uint)(*ptr - '0');
						}
						else if (*ptr >= 'A' && *ptr <= 'F')
						{
							num3 += (uint)(*ptr - 'A' + '\n');
						}
						else
						{
							num3 += (uint)(*ptr - 'a' + '\n');
						}
						ptr++;
					}
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00048750 File Offset: 0x00046950
		[SecuritySafeCritical]
		private unsafe static bool HexNumberToUInt64(ref Number.NumberBuffer number, ref ulong value)
		{
			int num = number.scale;
			if (num > 20 || num < number.precision)
			{
				return false;
			}
			char* ptr = number.digits;
			ulong num2 = 0UL;
			while (--num >= 0)
			{
				if (num2 > 1152921504606846975UL)
				{
					return false;
				}
				num2 *= 16UL;
				if (*ptr != '\0')
				{
					ulong num3 = num2;
					if (*ptr != '\0')
					{
						if (*ptr >= '0' && *ptr <= '9')
						{
							num3 += (ulong)((long)(*ptr - '0'));
						}
						else if (*ptr >= 'A' && *ptr <= 'F')
						{
							num3 += (ulong)((long)(*ptr - 'A' + '\n'));
						}
						else
						{
							num3 += (ulong)((long)(*ptr - 'a' + '\n'));
						}
						ptr++;
					}
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000487F3 File Offset: 0x000469F3
		private static bool IsWhite(char ch)
		{
			return ch == ' ' || (ch >= '\t' && ch <= '\r');
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004880C File Offset: 0x00046A0C
		[SecuritySafeCritical]
		private unsafe static bool NumberToInt32(ref Number.NumberBuffer number, ref int value)
		{
			int num = number.scale;
			if (num > 10 || num < number.precision)
			{
				return false;
			}
			char* digits = number.digits;
			int num2 = 0;
			while (--num >= 0)
			{
				if (num2 > 214748364)
				{
					return false;
				}
				num2 *= 10;
				if (*digits != '\0')
				{
					num2 += (int)(*(digits++) - '0');
				}
			}
			if (number.sign)
			{
				num2 = -num2;
				if (num2 > 0)
				{
					return false;
				}
			}
			else if (num2 < 0)
			{
				return false;
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00048880 File Offset: 0x00046A80
		[SecuritySafeCritical]
		private unsafe static bool NumberToInt64(ref Number.NumberBuffer number, ref long value)
		{
			int num = number.scale;
			if (num > 19 || num < number.precision)
			{
				return false;
			}
			char* digits = number.digits;
			long num2 = 0L;
			while (--num >= 0)
			{
				if (num2 > 922337203685477580L)
				{
					return false;
				}
				num2 *= 10L;
				if (*digits != '\0')
				{
					num2 += (long)(*(digits++) - '0');
				}
			}
			if (number.sign)
			{
				num2 = -num2;
				if (num2 > 0L)
				{
					return false;
				}
			}
			else if (num2 < 0L)
			{
				return false;
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000488FC File Offset: 0x00046AFC
		[SecuritySafeCritical]
		private unsafe static bool NumberToUInt32(ref Number.NumberBuffer number, ref uint value)
		{
			int num = number.scale;
			if (num > 10 || num < number.precision || number.sign)
			{
				return false;
			}
			char* digits = number.digits;
			uint num2 = 0U;
			while (--num >= 0)
			{
				if (num2 > 429496729U)
				{
					return false;
				}
				num2 *= 10U;
				if (*digits != '\0')
				{
					uint num3 = num2 + (uint)(*(digits++) - '0');
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00048968 File Offset: 0x00046B68
		[SecuritySafeCritical]
		private unsafe static bool NumberToUInt64(ref Number.NumberBuffer number, ref ulong value)
		{
			int num = number.scale;
			if (num > 20 || num < number.precision || number.sign)
			{
				return false;
			}
			char* digits = number.digits;
			ulong num2 = 0UL;
			while (--num >= 0)
			{
				if (num2 > 1844674407370955161UL)
				{
					return false;
				}
				num2 *= 10UL;
				if (*digits != '\0')
				{
					ulong num3 = num2 + (ulong)((long)(*(digits++) - '0'));
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000489DC File Offset: 0x00046BDC
		[SecurityCritical]
		private unsafe static char* MatchChars(char* p, string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Number.MatchChars(p, ptr);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00048A00 File Offset: 0x00046C00
		[SecurityCritical]
		private unsafe static char* MatchChars(char* p, char* str)
		{
			if (*str == '\0')
			{
				return null;
			}
			while (*str != '\0')
			{
				if (*p != *str && (*str != '\u00a0' || *p != ' '))
				{
					return null;
				}
				p++;
				str++;
			}
			return p;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00048A30 File Offset: 0x00046C30
		[SecuritySafeCritical]
		internal unsafe static decimal ParseDecimal(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			decimal num = 0m;
			Number.StringToNumber(value, options, ref numberBuffer, numfmt, true);
			if (!Number.NumberBufferToDecimal(numberBuffer.PackForNative(), ref num))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a Decimal."));
			}
			return num;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00048A84 File Offset: 0x00046C84
		[SecuritySafeCritical]
		internal unsafe static double ParseDouble(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			double num = 0.0;
			if (!Number.TryStringToNumber(value, options, ref numberBuffer, numfmt, false))
			{
				string text = value.Trim();
				if (text.Equals(numfmt.PositiveInfinitySymbol))
				{
					return double.PositiveInfinity;
				}
				if (text.Equals(numfmt.NegativeInfinitySymbol))
				{
					return double.NegativeInfinity;
				}
				if (text.Equals(numfmt.NaNSymbol))
				{
					return double.NaN;
				}
				throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
			}
			else
			{
				if (!Number.NumberBufferToDouble(numberBuffer.PackForNative(), ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a Double."));
				}
				return num;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00048B48 File Offset: 0x00046D48
		[SecuritySafeCritical]
		internal unsafe static int ParseInt32(string s, NumberStyles style, NumberFormatInfo info)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			int num = 0;
			Number.StringToNumber(s, style, ref numberBuffer, info, false);
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToInt32(ref numberBuffer, ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
				}
			}
			else if (!Number.NumberToInt32(ref numberBuffer, ref num))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
			}
			return num;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00048BB4 File Offset: 0x00046DB4
		[SecuritySafeCritical]
		internal unsafe static long ParseInt64(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			long num = 0L;
			Number.StringToNumber(value, options, ref numberBuffer, numfmt, false);
			if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToInt64(ref numberBuffer, ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int64."));
				}
			}
			else if (!Number.NumberToInt64(ref numberBuffer, ref num))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int64."));
			}
			return num;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00048C20 File Offset: 0x00046E20
		[SecurityCritical]
		private unsafe static bool ParseNumber(ref char* str, NumberStyles options, ref Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
		{
			number.scale = 0;
			number.sign = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			bool flag = false;
			string text5;
			string text6;
			if ((options & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
			{
				text = numfmt.CurrencySymbol;
				if (numfmt.ansiCurrencySymbol != null)
				{
					text2 = numfmt.ansiCurrencySymbol;
				}
				text3 = numfmt.NumberDecimalSeparator;
				text4 = numfmt.NumberGroupSeparator;
				text5 = numfmt.CurrencyDecimalSeparator;
				text6 = numfmt.CurrencyGroupSeparator;
				flag = true;
			}
			else
			{
				text5 = numfmt.NumberDecimalSeparator;
				text6 = numfmt.NumberGroupSeparator;
			}
			int num = 0;
			bool flag2 = sb != null;
			bool flag3 = flag2 && (options & NumberStyles.AllowHexSpecifier) > NumberStyles.None;
			int num2 = (flag2 ? int.MaxValue : 50);
			char* ptr = str;
			char c = *ptr;
			for (;;)
			{
				if (!Number.IsWhite(c) || (options & NumberStyles.AllowLeadingWhite) == NumberStyles.None || ((num & 1) != 0 && ((num & 1) == 0 || ((num & 32) == 0 && numfmt.NumberNegativePattern != 2))))
				{
					bool flag4;
					char* ptr2;
					if ((flag4 = (options & NumberStyles.AllowLeadingSign) != NumberStyles.None && (num & 1) == 0) && (ptr2 = Number.MatchChars(ptr, numfmt.PositiveSign)) != null)
					{
						num |= 1;
						ptr = ptr2 - 1;
					}
					else if (flag4 && (ptr2 = Number.MatchChars(ptr, numfmt.NegativeSign)) != null)
					{
						num |= 1;
						number.sign = true;
						ptr = ptr2 - 1;
					}
					else if (c == '(' && (options & NumberStyles.AllowParentheses) != NumberStyles.None && (num & 1) == 0)
					{
						num |= 3;
						number.sign = true;
					}
					else
					{
						if ((text == null || (ptr2 = Number.MatchChars(ptr, text)) == null) && (text2 == null || (ptr2 = Number.MatchChars(ptr, text2)) == null))
						{
							break;
						}
						num |= 32;
						text = null;
						text2 = null;
						ptr = ptr2 - 1;
					}
				}
				c = *(++ptr);
			}
			int num3 = 0;
			int num4 = 0;
			for (;;)
			{
				char* ptr2;
				if ((c >= '0' && c <= '9') || ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None && ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))))
				{
					num |= 4;
					if (c != '0' || (num & 8) != 0 || flag3)
					{
						if (num3 < num2)
						{
							if (flag2)
							{
								sb.Append(c);
							}
							else
							{
								number.digits[(IntPtr)(num3++) * 2] = c;
							}
							if (c != '0' || parseDecimal)
							{
								num4 = num3;
							}
						}
						if ((num & 16) == 0)
						{
							number.scale++;
						}
						num |= 8;
					}
					else if ((num & 16) != 0)
					{
						number.scale--;
					}
				}
				else if ((options & NumberStyles.AllowDecimalPoint) != NumberStyles.None && (num & 16) == 0 && ((ptr2 = Number.MatchChars(ptr, text5)) != null || (flag && (num & 32) == 0 && (ptr2 = Number.MatchChars(ptr, text3)) != null)))
				{
					num |= 16;
					ptr = ptr2 - 1;
				}
				else
				{
					if ((options & NumberStyles.AllowThousands) == NumberStyles.None || (num & 4) == 0 || (num & 16) != 0 || ((ptr2 = Number.MatchChars(ptr, text6)) == null && (!flag || (num & 32) != 0 || (ptr2 = Number.MatchChars(ptr, text4)) == null)))
					{
						break;
					}
					ptr = ptr2 - 1;
				}
				c = *(++ptr);
			}
			bool flag5 = false;
			number.precision = num4;
			if (flag2)
			{
				sb.Append('\0');
			}
			else
			{
				number.digits[num4] = '\0';
			}
			if ((num & 4) != 0)
			{
				if ((c == 'E' || c == 'e') && (options & NumberStyles.AllowExponent) != NumberStyles.None)
				{
					char* ptr3 = ptr;
					c = *(++ptr);
					char* ptr2;
					if ((ptr2 = Number.MatchChars(ptr, numfmt.PositiveSign)) != null)
					{
						c = *(ptr = ptr2);
					}
					else if ((ptr2 = Number.MatchChars(ptr, numfmt.NegativeSign)) != null)
					{
						c = *(ptr = ptr2);
						flag5 = true;
					}
					if (c >= '0' && c <= '9')
					{
						int num5 = 0;
						do
						{
							num5 = num5 * 10 + (int)(c - '0');
							c = *(++ptr);
							if (num5 > 1000)
							{
								num5 = 9999;
								while (c >= '0' && c <= '9')
								{
									c = *(++ptr);
								}
							}
						}
						while (c >= '0' && c <= '9');
						if (flag5)
						{
							num5 = -num5;
						}
						number.scale += num5;
					}
					else
					{
						ptr = ptr3;
						c = *ptr;
					}
				}
				for (;;)
				{
					if (!Number.IsWhite(c) || (options & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
					{
						bool flag4;
						char* ptr2;
						if ((flag4 = (options & NumberStyles.AllowTrailingSign) != NumberStyles.None && (num & 1) == 0) && (ptr2 = Number.MatchChars(ptr, numfmt.PositiveSign)) != null)
						{
							num |= 1;
							ptr = ptr2 - 1;
						}
						else if (flag4 && (ptr2 = Number.MatchChars(ptr, numfmt.NegativeSign)) != null)
						{
							num |= 1;
							number.sign = true;
							ptr = ptr2 - 1;
						}
						else if (c == ')' && (num & 2) != 0)
						{
							num &= -3;
						}
						else
						{
							if ((text == null || (ptr2 = Number.MatchChars(ptr, text)) == null) && (text2 == null || (ptr2 = Number.MatchChars(ptr, text2)) == null))
							{
								break;
							}
							text = null;
							text2 = null;
							ptr = ptr2 - 1;
						}
					}
					c = *(++ptr);
				}
				if ((num & 2) == 0)
				{
					if ((num & 8) == 0)
					{
						if (!parseDecimal)
						{
							number.scale = 0;
						}
						if ((num & 16) == 0)
						{
							number.sign = false;
						}
					}
					str = ptr;
					return true;
				}
			}
			str = ptr;
			return false;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00049144 File Offset: 0x00047344
		[SecuritySafeCritical]
		internal unsafe static float ParseSingle(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			double num = 0.0;
			if (!Number.TryStringToNumber(value, options, ref numberBuffer, numfmt, false))
			{
				string text = value.Trim();
				if (text.Equals(numfmt.PositiveInfinitySymbol))
				{
					return float.PositiveInfinity;
				}
				if (text.Equals(numfmt.NegativeInfinitySymbol))
				{
					return float.NegativeInfinity;
				}
				if (text.Equals(numfmt.NaNSymbol))
				{
					return float.NaN;
				}
				throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
			}
			else
			{
				if (!Number.NumberBufferToDouble(numberBuffer.PackForNative(), ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a Single."));
				}
				float num2 = (float)num;
				if (float.IsInfinity(num2))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a Single."));
				}
				return num2;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00049214 File Offset: 0x00047414
		[SecuritySafeCritical]
		internal unsafe static uint ParseUInt32(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			uint num = 0U;
			Number.StringToNumber(value, options, ref numberBuffer, numfmt, false);
			if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToUInt32(ref numberBuffer, ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
				}
			}
			else if (!Number.NumberToUInt32(ref numberBuffer, ref num))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return num;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00049280 File Offset: 0x00047480
		[SecuritySafeCritical]
		internal unsafe static ulong ParseUInt64(string value, NumberStyles options, NumberFormatInfo numfmt)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			ulong num = 0UL;
			Number.StringToNumber(value, options, ref numberBuffer, numfmt, false);
			if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToUInt64(ref numberBuffer, ref num))
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
				}
			}
			else if (!Number.NumberToUInt64(ref numberBuffer, ref num))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
			}
			return num;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000492EC File Offset: 0x000474EC
		[SecuritySafeCritical]
		private unsafe static void StringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, NumberFormatInfo info, bool parseDecimal)
		{
			if (str == null)
			{
				throw new ArgumentNullException("String");
			}
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				if (!Number.ParseNumber(ref ptr2, options, ref number, null, info, parseDecimal) || ((long)(ptr2 - ptr) < (long)str.Length && !Number.TrailingZeros(str, (int)((long)(ptr2 - ptr)))))
				{
					throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
				}
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00049358 File Offset: 0x00047558
		private static bool TrailingZeros(string s, int index)
		{
			for (int i = index; i < s.Length; i++)
			{
				if (s[i] != '\0')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00049384 File Offset: 0x00047584
		[SecuritySafeCritical]
		internal unsafe static bool TryParseDecimal(string value, NumberStyles options, NumberFormatInfo numfmt, out decimal result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0m;
			return Number.TryStringToNumber(value, options, ref numberBuffer, numfmt, true) && Number.NumberBufferToDecimal(numberBuffer.PackForNative(), ref result);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000493CC File Offset: 0x000475CC
		[SecuritySafeCritical]
		internal unsafe static bool TryParseDouble(string value, NumberStyles options, NumberFormatInfo numfmt, out double result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0.0;
			return Number.TryStringToNumber(value, options, ref numberBuffer, numfmt, false) && Number.NumberBufferToDouble(numberBuffer.PackForNative(), ref result);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00049418 File Offset: 0x00047618
		[SecuritySafeCritical]
		internal unsafe static bool TryParseInt32(string s, NumberStyles style, NumberFormatInfo info, out int result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0;
			if (!Number.TryStringToNumber(s, style, ref numberBuffer, info, false))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToInt32(ref numberBuffer, ref result))
				{
					return false;
				}
			}
			else if (!Number.NumberToInt32(ref numberBuffer, ref result))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004946C File Offset: 0x0004766C
		[SecuritySafeCritical]
		internal unsafe static bool TryParseInt64(string s, NumberStyles style, NumberFormatInfo info, out long result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0L;
			if (!Number.TryStringToNumber(s, style, ref numberBuffer, info, false))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToInt64(ref numberBuffer, ref result))
				{
					return false;
				}
			}
			else if (!Number.NumberToInt64(ref numberBuffer, ref result))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000494C0 File Offset: 0x000476C0
		[SecuritySafeCritical]
		internal unsafe static bool TryParseSingle(string value, NumberStyles options, NumberFormatInfo numfmt, out float result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0f;
			double num = 0.0;
			if (!Number.TryStringToNumber(value, options, ref numberBuffer, numfmt, false))
			{
				return false;
			}
			if (!Number.NumberBufferToDouble(numberBuffer.PackForNative(), ref num))
			{
				return false;
			}
			float num2 = (float)num;
			if (float.IsInfinity(num2))
			{
				return false;
			}
			result = num2;
			return true;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00049524 File Offset: 0x00047724
		[SecuritySafeCritical]
		internal unsafe static bool TryParseUInt32(string s, NumberStyles style, NumberFormatInfo info, out uint result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0U;
			if (!Number.TryStringToNumber(s, style, ref numberBuffer, info, false))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToUInt32(ref numberBuffer, ref result))
				{
					return false;
				}
			}
			else if (!Number.NumberToUInt32(ref numberBuffer, ref result))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00049578 File Offset: 0x00047778
		[SecuritySafeCritical]
		internal unsafe static bool TryParseUInt64(string s, NumberStyles style, NumberFormatInfo info, out ulong result)
		{
			byte* ptr = stackalloc byte[(UIntPtr)Number.NumberBuffer.NumberBufferBytes];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(ptr);
			result = 0UL;
			if (!Number.TryStringToNumber(s, style, ref numberBuffer, info, false))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!Number.HexNumberToUInt64(ref numberBuffer, ref result))
				{
					return false;
				}
			}
			else if (!Number.NumberToUInt64(ref numberBuffer, ref result))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000495CB File Offset: 0x000477CB
		internal static bool TryStringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, NumberFormatInfo numfmt, bool parseDecimal)
		{
			return Number.TryStringToNumber(str, options, ref number, null, numfmt, parseDecimal);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000495DC File Offset: 0x000477DC
		[FriendAccessAllowed]
		[SecuritySafeCritical]
		internal unsafe static bool TryStringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
		{
			if (str == null)
			{
				return false;
			}
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				if (!Number.ParseNumber(ref ptr2, options, ref number, sb, numfmt, parseDecimal) || ((long)(ptr2 - ptr) < (long)str.Length && !Number.TrailingZeros(str, (int)((long)(ptr2 - ptr)))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000A3E RID: 2622
		private const int NumberMaxDigits = 50;

		// Token: 0x04000A3F RID: 2623
		private const int Int32Precision = 10;

		// Token: 0x04000A40 RID: 2624
		private const int UInt32Precision = 10;

		// Token: 0x04000A41 RID: 2625
		private const int Int64Precision = 19;

		// Token: 0x04000A42 RID: 2626
		private const int UInt64Precision = 20;

		// Token: 0x020001A8 RID: 424
		[FriendAccessAllowed]
		internal struct NumberBuffer
		{
			// Token: 0x060011DE RID: 4574 RVA: 0x00049633 File Offset: 0x00047833
			[SecurityCritical]
			public unsafe NumberBuffer(byte* stackBuffer)
			{
				this.baseAddress = stackBuffer;
				this.digits = (char*)(stackBuffer + (IntPtr)6 * 2);
				this.precision = 0;
				this.scale = 0;
				this.sign = false;
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x00049660 File Offset: 0x00047860
			[SecurityCritical]
			public unsafe byte* PackForNative()
			{
				int* ptr = (int*)this.baseAddress;
				*ptr = this.precision;
				ptr[1] = this.scale;
				ptr[2] = (this.sign ? 1 : 0);
				return this.baseAddress;
			}

			// Token: 0x04000A43 RID: 2627
			public static readonly int NumberBufferBytes = 114 + IntPtr.Size;

			// Token: 0x04000A44 RID: 2628
			[SecurityCritical]
			private unsafe byte* baseAddress;

			// Token: 0x04000A45 RID: 2629
			[SecurityCritical]
			public unsafe char* digits;

			// Token: 0x04000A46 RID: 2630
			public int precision;

			// Token: 0x04000A47 RID: 2631
			public int scale;

			// Token: 0x04000A48 RID: 2632
			public bool sign;
		}
	}
}
