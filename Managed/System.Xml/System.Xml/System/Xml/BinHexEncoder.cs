using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200007B RID: 123
	internal static class BinHexEncoder
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000E870 File Offset: 0x0000CA70
		internal static void Encode(byte[] buffer, int index, int count, XmlWriter writer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[(count * 2 < 128) ? (count * 2) : 128];
			int num = index + count;
			while (index < num)
			{
				int num2 = ((count < 64) ? count : 64);
				int num3 = BinHexEncoder.Encode(buffer, index, num2, array);
				writer.WriteRaw(array, 0, num3);
				index += num2;
				count -= num2;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E908 File Offset: 0x0000CB08
		internal static string Encode(byte[] inArray, int offsetIn, int count)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (0 > offsetIn)
			{
				throw new ArgumentOutOfRangeException("offsetIn");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > inArray.Length - offsetIn)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[2 * count];
			int num = BinHexEncoder.Encode(inArray, offsetIn, count, array);
			return new string(array, 0, num);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E970 File Offset: 0x0000CB70
		private static int Encode(byte[] inArray, int offsetIn, int count, char[] outArray)
		{
			int num = 0;
			int num2 = 0;
			int num3 = outArray.Length;
			for (int i = 0; i < count; i++)
			{
				byte b = inArray[offsetIn++];
				outArray[num++] = "0123456789ABCDEF"[b >> 4];
				if (num == num3)
				{
					break;
				}
				outArray[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				if (num == num3)
				{
					break;
				}
			}
			return num - num2;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		internal static async Task EncodeAsync(byte[] buffer, int index, int count, XmlWriter writer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] chars = new char[(count * 2 < 128) ? (count * 2) : 128];
			int endIndex = index + count;
			while (index < endIndex)
			{
				int cnt = ((count < 64) ? count : 64);
				int num = BinHexEncoder.Encode(buffer, index, cnt, chars);
				await writer.WriteRawAsync(chars, 0, num).ConfigureAwait(false);
				index += cnt;
				count -= cnt;
			}
		}

		// Token: 0x04000232 RID: 562
		private const string s_hexDigits = "0123456789ABCDEF";

		// Token: 0x04000233 RID: 563
		private const int CharsChunkSize = 128;
	}
}
