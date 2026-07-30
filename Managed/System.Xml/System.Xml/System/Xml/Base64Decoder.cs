using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000074 RID: 116
	internal class Base64Decoder : IncrementalReadDecoder
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000DAF2 File Offset: 0x0000BCF2
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000DB01 File Offset: 0x0000BD01
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DB14 File Offset: 0x0000BD14
		internal unsafe override int Decode(char[] chars, int startPos, int len)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (chars.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int num;
			int num2;
			fixed (char* ptr = &chars[startPos])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &this.buffer[this.curIndex])
				{
					byte* ptr4 = ptr3;
					this.Decode(ptr2, ptr2 + len, ptr4, ptr4 + (this.endIndex - this.curIndex), out num, out num2);
				}
			}
			this.curIndex += num2;
			return num;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		internal unsafe override int Decode(string str, int startPos, int len)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (str.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int num;
			int num2;
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (byte* ptr2 = &this.buffer[this.curIndex])
				{
					byte* ptr3 = ptr2;
					this.Decode(ptr + startPos, ptr + startPos + len, ptr3, ptr3 + (this.endIndex - this.curIndex), out num, out num2);
				}
			}
			this.curIndex += num2;
			return num;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000DC82 File Offset: 0x0000BE82
		internal override void Reset()
		{
			this.bitsFilled = 0;
			this.bits = 0;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DC92 File Offset: 0x0000BE92
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (byte[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		private static byte[] ConstructMapBase64()
		{
			byte[] array = new byte[123];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.MaxValue;
			}
			for (int j = 0; j < Base64Decoder.CharsBase64.Length; j++)
			{
				array[(int)Base64Decoder.CharsBase64[j]] = (byte)j;
			}
			return array;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000DD08 File Offset: 0x0000BF08
		private unsafe void Decode(char* pChars, char* pCharsEndPos, byte* pBytes, byte* pBytesEndPos, out int charsDecoded, out int bytesDecoded)
		{
			byte* ptr = pBytes;
			char* ptr2 = pChars;
			int num = this.bits;
			int num2 = this.bitsFilled;
			XmlCharType instance = XmlCharType.Instance;
			while (ptr2 < pCharsEndPos && ptr < pBytesEndPos)
			{
				char c = *ptr2;
				if (c == '=')
				{
					break;
				}
				ptr2++;
				if ((instance.charProperties[(int)c] & 1) == 0)
				{
					int num3;
					if (c > 'z' || (num3 = (int)Base64Decoder.MapBase64[(int)c]) == 255)
					{
						throw new XmlException("'{0}' is not a valid Base64 text sequence.", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
					}
					num = (num << 6) | num3;
					num2 += 6;
					if (num2 >= 8)
					{
						*(ptr++) = (byte)((num >> num2 - 8) & 255);
						num2 -= 8;
						if (ptr == pBytesEndPos)
						{
							IL_00EE:
							this.bits = num;
							this.bitsFilled = num2;
							bytesDecoded = (int)((long)(ptr - pBytes));
							charsDecoded = (int)((long)(ptr2 - pChars));
							return;
						}
					}
				}
			}
			if (ptr2 >= pCharsEndPos || *ptr2 != '=')
			{
				goto IL_00EE;
			}
			num2 = 0;
			do
			{
				ptr2++;
			}
			while (ptr2 < pCharsEndPos && *ptr2 == '=');
			if (ptr2 < pCharsEndPos)
			{
				while ((instance.charProperties[(int)(*(ptr2++))] & 1) != 0)
				{
					if (ptr2 >= pCharsEndPos)
					{
						goto IL_00EE;
					}
				}
				throw new XmlException("'{0}' is not a valid Base64 text sequence.", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
			}
			goto IL_00EE;
		}

		// Token: 0x0400020E RID: 526
		private byte[] buffer;

		// Token: 0x0400020F RID: 527
		private int startIndex;

		// Token: 0x04000210 RID: 528
		private int curIndex;

		// Token: 0x04000211 RID: 529
		private int endIndex;

		// Token: 0x04000212 RID: 530
		private int bits;

		// Token: 0x04000213 RID: 531
		private int bitsFilled;

		// Token: 0x04000214 RID: 532
		private static readonly string CharsBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x04000215 RID: 533
		private static readonly byte[] MapBase64 = Base64Decoder.ConstructMapBase64();

		// Token: 0x04000216 RID: 534
		private const int MaxValidChar = 122;

		// Token: 0x04000217 RID: 535
		private const byte Invalid = 255;
	}
}
