using System;
using System.Text;

namespace System
{
	// Token: 0x020001FA RID: 506
	internal static class ParseNumbers
	{
		// Token: 0x060017B5 RID: 6069 RVA: 0x0005C6C4 File Offset: 0x0005A8C4
		public static int StringToInt(string value, int fromBase, int flags)
		{
			return ParseNumbers.StringToInt(value, fromBase, flags, null);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
		public unsafe static int StringToInt(string value, int fromBase, int flags, int* parsePos)
		{
			if ((flags & 12288) == 0)
			{
				throw new NotImplementedException(flags.ToString());
			}
			if (value == null)
			{
				return 0;
			}
			int num = 0;
			uint num2 = 0U;
			int length = value.Length;
			bool flag = false;
			if (length == 0)
			{
				throw new ArgumentOutOfRangeException("Empty string");
			}
			int i = ((parsePos == null) ? 0 : (*parsePos));
			if (value[i] == '-')
			{
				if (fromBase != 10)
				{
					throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
				}
				if ((flags & 512) != 0)
				{
					throw new OverflowException("Negative number");
				}
				flag = true;
				i++;
			}
			else if (value[i] == '+')
			{
				i++;
			}
			if (fromBase == 16 && i + 1 < length && value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X'))
			{
				i += 2;
			}
			uint num3;
			if ((flags & 1024) != 0)
			{
				num3 = 255U;
			}
			else if ((flags & 2048) != 0)
			{
				num3 = 65535U;
			}
			else
			{
				num3 = uint.MaxValue;
			}
			while (i < length)
			{
				char c = value[i];
				int num4;
				if (char.IsNumber(c))
				{
					num4 = (int)(c - '0');
				}
				else if (char.IsLetter(c))
				{
					num4 = (int)(char.ToLowerInvariant(c) - 'a' + '\n');
				}
				else
				{
					if (i == 0)
					{
						throw new FormatException("Could not find any parsable digits.");
					}
					if ((flags & 4096) != 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					break;
				}
				if (num4 >= fromBase)
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				else
				{
					long num5 = (long)fromBase * (long)((ulong)num2) + (long)num4;
					if (num5 > (long)((ulong)num3))
					{
						throw new OverflowException();
					}
					num2 = (uint)num5;
					num++;
					i++;
				}
			}
			if (num == 0)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (parsePos != null)
			{
				*parsePos = i;
			}
			if (!flag)
			{
				return (int)num2;
			}
			return (int)(-(int)num2);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005C898 File Offset: 0x0005AA98
		public static string LongToString(long value, int toBase, int width, char paddingChar, int flags)
		{
			if (value == 0L)
			{
				return "0";
			}
			if (toBase == 10)
			{
				return value.ToString();
			}
			byte[] bytes = BitConverter.GetBytes(value);
			if (toBase == 2)
			{
				return ParseNumbers.ConvertToBase2(bytes).ToString();
			}
			if (toBase == 8)
			{
				return ParseNumbers.ConvertToBase8(bytes).ToString();
			}
			if (toBase != 16)
			{
				throw new NotImplementedException();
			}
			return ParseNumbers.ConvertToBase16(bytes).ToString();
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005C8FA File Offset: 0x0005AAFA
		public static long StringToLong(string value, int fromBase, int flags)
		{
			return ParseNumbers.StringToLong(value, fromBase, flags, null);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005C908 File Offset: 0x0005AB08
		public unsafe static long StringToLong(string value, int fromBase, int flags, int* parsePos)
		{
			if ((flags & 12288) == 0)
			{
				throw new NotImplementedException(flags.ToString());
			}
			if (value == null)
			{
				return 0L;
			}
			int num = 0;
			ulong num2 = (ulong)((long)fromBase);
			ulong num3 = 0UL;
			int length = value.Length;
			bool flag = false;
			bool flag2 = (flags & 512) != 0;
			if (length == 0)
			{
				throw new ArgumentOutOfRangeException("Empty string");
			}
			int i = ((parsePos == null) ? 0 : (*parsePos));
			if (value[i] == '-')
			{
				if (fromBase != 10)
				{
					throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
				}
				if (flag2)
				{
					throw new OverflowException("Negative number");
				}
				flag = true;
				i++;
			}
			else if (value[i] == '+')
			{
				i++;
			}
			if (fromBase == 16 && i + 1 < length && value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X'))
			{
				i += 2;
			}
			while (i < length)
			{
				char c = value[i];
				ulong num4;
				if (char.IsNumber(c))
				{
					num4 = (ulong)((long)(c - '0'));
				}
				else if (char.IsLetter(c))
				{
					num4 = (ulong)((long)(char.ToLowerInvariant(c) - 'a' + '\n'));
				}
				else
				{
					if (i == 0)
					{
						throw new FormatException("Could not find any parsable digits.");
					}
					if ((flags & 4096) != 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					break;
				}
				if (num4 >= num2)
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				else
				{
					if (num3 <= 72057594037927935UL)
					{
						num3 = num3 * num2 + num4;
					}
					else
					{
						ulong num5 = (num3 >> 32) * num2;
						ulong num6 = (num3 & (ulong)(-1)) * num2 + num4;
						if ((num6 >> 32) + num5 > (ulong)(-1))
						{
							throw new OverflowException();
						}
						num3 = (num5 << 32) + num6;
					}
					num++;
					i++;
				}
			}
			if (num == 0)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (parsePos != null)
			{
				*parsePos = i;
			}
			if (flag2)
			{
				return (long)num3;
			}
			if (!flag)
			{
				if (fromBase == 10 && num3 > 9223372036854775807UL)
				{
					throw new OverflowException();
				}
				return (long)num3;
			}
			else
			{
				if (num3 <= 9223372036854775807UL)
				{
					return (long)(-(long)num3);
				}
				if (num3 > 9223372036854775808UL)
				{
					throw new OverflowException();
				}
				return (long)(9223372036854775808UL + (9223372036854775808UL - num3));
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005CB40 File Offset: 0x0005AD40
		public static string IntToString(int value, int toBase, int width, char paddingChar, int flags)
		{
			StringBuilder stringBuilder;
			if (value == 0)
			{
				if (width <= 0)
				{
					return "0";
				}
				stringBuilder = new StringBuilder("0", width);
			}
			else if (toBase == 10)
			{
				stringBuilder = new StringBuilder(value.ToString());
			}
			else
			{
				byte[] array;
				if ((flags & 64) != 0)
				{
					array = BitConverter.GetBytes((short)((byte)value));
				}
				else if ((flags & 128) != 0)
				{
					array = BitConverter.GetBytes((short)value);
				}
				else
				{
					array = BitConverter.GetBytes(value);
				}
				if (toBase != 2)
				{
					if (toBase != 8)
					{
						if (toBase != 16)
						{
							throw new NotImplementedException();
						}
						stringBuilder = ParseNumbers.ConvertToBase16(array);
					}
					else
					{
						stringBuilder = ParseNumbers.ConvertToBase8(array);
					}
				}
				else
				{
					stringBuilder = ParseNumbers.ConvertToBase2(array);
				}
			}
			for (int i = width - stringBuilder.Length; i > 0; i--)
			{
				stringBuilder.Insert(0, paddingChar);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005CBFC File Offset: 0x0005ADFC
		private static void EndianSwap(ref byte[] value)
		{
			byte[] array = new byte[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = value[value.Length - 1 - i];
			}
			value = array;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005CC34 File Offset: 0x0005AE34
		private static StringBuilder ConvertToBase2(byte[] value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				ParseNumbers.EndianSwap(ref value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = value.Length - 1; i >= 0; i--)
			{
				byte b = value[i];
				for (int j = 0; j < 8; j++)
				{
					if ((b & 128) == 128)
					{
						stringBuilder.Append('1');
					}
					else if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('0');
					}
					b = (byte)(b << 1);
				}
			}
			return stringBuilder;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005CCA8 File Offset: 0x0005AEA8
		private static StringBuilder ConvertToBase8(byte[] value)
		{
			int num = value.Length;
			switch (num)
			{
			case 1:
			{
				ulong num2 = (ulong)value[0];
				goto IL_0057;
			}
			case 2:
			{
				ulong num2 = (ulong)BitConverter.ToUInt16(value, 0);
				goto IL_0057;
			}
			case 3:
				break;
			case 4:
			{
				ulong num2 = (ulong)BitConverter.ToUInt32(value, 0);
				goto IL_0057;
			}
			default:
				if (num == 8)
				{
					ulong num2 = BitConverter.ToUInt64(value, 0);
					goto IL_0057;
				}
				break;
			}
			throw new ArgumentException("value");
			IL_0057:
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 21; i >= 0; i--)
			{
				ulong num2;
				char c = (char)((num2 >> i * 3) & 7UL);
				if (c != '\0' || stringBuilder.Length > 0)
				{
					c += '0';
					stringBuilder.Append(c);
				}
			}
			return stringBuilder;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0005CD4C File Offset: 0x0005AF4C
		private static StringBuilder ConvertToBase16(byte[] value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				ParseNumbers.EndianSwap(ref value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = value.Length - 1; i >= 0; i--)
			{
				char c = (char)((value[i] >> 4) & 15);
				if (c != '\0' || stringBuilder.Length > 0)
				{
					if (c < '\n')
					{
						c += '0';
					}
					else
					{
						c -= '\n';
						c += 'a';
					}
					stringBuilder.Append(c);
				}
				char c2 = (char)(value[i] & 15);
				if (c2 != '\0' || stringBuilder.Length > 0)
				{
					if (c2 < '\n')
					{
						c2 += '0';
					}
					else
					{
						c2 -= '\n';
						c2 += 'a';
					}
					stringBuilder.Append(c2);
				}
			}
			return stringBuilder;
		}

		// Token: 0x04000C4D RID: 3149
		internal const int PrintAsI1 = 64;

		// Token: 0x04000C4E RID: 3150
		internal const int PrintAsI2 = 128;

		// Token: 0x04000C4F RID: 3151
		internal const int TreatAsUnsigned = 512;

		// Token: 0x04000C50 RID: 3152
		internal const int TreatAsI1 = 1024;

		// Token: 0x04000C51 RID: 3153
		internal const int TreatAsI2 = 2048;

		// Token: 0x04000C52 RID: 3154
		internal const int IsTight = 4096;

		// Token: 0x04000C53 RID: 3155
		internal const int NoSpace = 8192;

		// Token: 0x04000C54 RID: 3156
		private const ulong base16MaxOverflowFreeValue = 72057594037927935UL;

		// Token: 0x04000C55 RID: 3157
		private const ulong longMinValue = 9223372036854775808UL;
	}
}
