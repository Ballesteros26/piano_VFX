using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	/// <summary>Provides methods for encoding and decoding URLs when processing Web requests. This class cannot be inherited. </summary>
	// Token: 0x020000BA RID: 186
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpUtility
	{
		/// <summary>Minimally converts a string into an HTML-encoded string and sends the encoded string to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The string to encode </param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream. </param>
		// Token: 0x06000A25 RID: 2597 RVA: 0x00018990 File Offset: 0x00016B90
		public static void HtmlAttributeEncode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			HttpEncoder.Current.HtmlAttributeEncode(s, output);
		}

		/// <summary>Minimally converts a string to an HTML-encoded string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="s">The string to encode. </param>
		// Token: 0x06000A26 RID: 2598 RVA: 0x000189AC File Offset: 0x00016BAC
		public static string HtmlAttributeEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlAttributeEncode(s, stringWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		/// <summary>Converts a string that has been encoded for transmission in a URL into a decoded string.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="str">The string to decode. </param>
		// Token: 0x06000A27 RID: 2599 RVA: 0x000189F4 File Offset: 0x00016BF4
		public static string UrlDecode(string str)
		{
			return HttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00018A01 File Offset: 0x00016C01
		private static char[] GetChars(MemoryStream b, Encoding e)
		{
			return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00018A18 File Offset: 0x00016C18
		private static void WriteCharBytes(IList buf, char ch, Encoding e)
		{
			if (ch > 'ÿ')
			{
				foreach (byte b in e.GetBytes(new char[] { ch }))
				{
					buf.Add(b);
				}
				return;
			}
			buf.Add((byte)ch);
		}

		/// <summary>Converts a URL-encoded string into a decoded string, using the specified encoding object.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="str">The string to decode. </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the decoding scheme. </param>
		// Token: 0x06000A2A RID: 2602 RVA: 0x00018A6C File Offset: 0x00016C6C
		public static string UrlDecode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOf('%') == -1 && str.IndexOf('+') == -1)
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			long num = (long)str.Length;
			List<byte> list = new List<byte>();
			int num2 = 0;
			while ((long)num2 < num)
			{
				char c = str[num2];
				if (c == '%' && (long)(num2 + 2) < num && str[num2 + 1] != '%')
				{
					int num3;
					if (str[num2 + 1] == 'u' && (long)(num2 + 5) < num)
					{
						num3 = HttpUtility.GetChar(str, num2 + 2, 4);
						if (num3 != -1)
						{
							HttpUtility.WriteCharBytes(list, (char)num3, e);
							num2 += 5;
						}
						else
						{
							HttpUtility.WriteCharBytes(list, '%', e);
						}
					}
					else if ((num3 = HttpUtility.GetChar(str, num2 + 1, 2)) != -1)
					{
						HttpUtility.WriteCharBytes(list, (char)num3, e);
						num2 += 2;
					}
					else
					{
						HttpUtility.WriteCharBytes(list, '%', e);
					}
				}
				else if (c == '+')
				{
					HttpUtility.WriteCharBytes(list, ' ', e);
				}
				else
				{
					HttpUtility.WriteCharBytes(list, c, e);
				}
				num2++;
			}
			byte[] array = list.ToArray();
			return e.GetString(array);
		}

		/// <summary>Converts a URL-encoded byte array into a decoded string using the specified decoding object.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="bytes">The array of bytes to decode. </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the decoding scheme. </param>
		// Token: 0x06000A2B RID: 2603 RVA: 0x00018B84 File Offset: 0x00016D84
		public static string UrlDecode(byte[] bytes, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecode(bytes, 0, bytes.Length, e);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00018B98 File Offset: 0x00016D98
		private static int GetInt(byte b)
		{
			if (b >= 48 && b <= 57)
			{
				return (int)(b - 48);
			}
			if (b >= 97 && b <= 102)
			{
				return (int)(b - 97 + 10);
			}
			if (b >= 65 && b <= 70)
			{
				return (int)(b - 65 + 10);
			}
			return -1;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00018BDC File Offset: 0x00016DDC
		private static int GetChar(byte[] bytes, int offset, int length)
		{
			int num = 0;
			int num2 = length + offset;
			for (int i = offset; i < num2; i++)
			{
				int @int = HttpUtility.GetInt(bytes[i]);
				if (@int == -1)
				{
					return -1;
				}
				num = (num << 4) + @int;
			}
			return num;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00018C14 File Offset: 0x00016E14
		private static int GetChar(string str, int offset, int length)
		{
			int num = 0;
			int num2 = length + offset;
			for (int i = offset; i < num2; i++)
			{
				char c = str[i];
				if (c > '\u007f')
				{
					return -1;
				}
				int @int = HttpUtility.GetInt((byte)c);
				if (@int == -1)
				{
					return -1;
				}
				num = (num << 4) + @int;
			}
			return num;
		}

		/// <summary>Converts a URL-encoded byte array into a decoded string using the specified encoding object, starting at the specified position in the array, and continuing for the specified number of bytes.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="bytes">The array of bytes to decode. </param>
		/// <param name="offset">The position in the byte to begin decoding. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the decoding scheme. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null, but <paramref name="count" /> does not equal 0.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0 or greater than the length of the <paramref name="bytes" /> array.- or -<paramref name="count" /> is less than 0, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00018C5C File Offset: 0x00016E5C
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			StringBuilder stringBuilder = new StringBuilder();
			MemoryStream memoryStream = new MemoryStream();
			int num = count + offset;
			int i = offset;
			while (i < num)
			{
				if (bytes[i] != 37 || i + 2 >= count || bytes[i + 1] == 37)
				{
					goto IL_00EE;
				}
				if (bytes[i + 1] == 117 && i + 5 < num)
				{
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
						memoryStream.SetLength(0L);
					}
					int num2 = HttpUtility.GetChar(bytes, i + 2, 4);
					if (num2 == -1)
					{
						goto IL_00EE;
					}
					stringBuilder.Append((char)num2);
					i += 5;
				}
				else
				{
					int num2;
					if ((num2 = HttpUtility.GetChar(bytes, i + 1, 2)) == -1)
					{
						goto IL_00EE;
					}
					memoryStream.WriteByte((byte)num2);
					i += 2;
				}
				IL_012C:
				i++;
				continue;
				IL_00EE:
				if (memoryStream.Length > 0L)
				{
					stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
					memoryStream.SetLength(0L);
				}
				if (bytes[i] == 43)
				{
					stringBuilder.Append(' ');
					goto IL_012C;
				}
				stringBuilder.Append((char)bytes[i]);
				goto IL_012C;
			}
			if (memoryStream.Length > 0L)
			{
				stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Converts a URL-encoded array of bytes into a decoded array of bytes.</summary>
		/// <returns>A decoded array of bytes.</returns>
		/// <param name="bytes">The array of bytes to decode. </param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x00018DC3 File Offset: 0x00016FC3
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeToBytes(bytes, 0, bytes.Length);
		}

		/// <summary>Converts a URL-encoded string into a decoded array of bytes.</summary>
		/// <returns>A decoded array of bytes.</returns>
		/// <param name="str">The string to decode. </param>
		// Token: 0x06000A31 RID: 2609 RVA: 0x00018DD4 File Offset: 0x00016FD4
		public static byte[] UrlDecodeToBytes(string str)
		{
			return HttpUtility.UrlDecodeToBytes(str, Encoding.UTF8);
		}

		/// <summary>Converts a URL-encoded string into a decoded array of bytes using the specified decoding object.</summary>
		/// <returns>A decoded array of bytes.</returns>
		/// <param name="str">The string to decode. </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the decoding scheme. </param>
		// Token: 0x06000A32 RID: 2610 RVA: 0x00018DE1 File Offset: 0x00016FE1
		public static byte[] UrlDecodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return HttpUtility.UrlDecodeToBytes(e.GetBytes(str));
		}

		/// <summary>Converts a URL-encoded array of bytes into a decoded array of bytes, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
		/// <returns>A decoded array of bytes.</returns>
		/// <param name="bytes">The array of bytes to decode. </param>
		/// <param name="offset">The position in the byte array at which to begin decoding. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null, but <paramref name="count" /> does not equal 0.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0 or greater than the length of the <paramref name="bytes" /> array.- or -<paramref name="count" /> is less than 0, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
		// Token: 0x06000A33 RID: 2611 RVA: 0x00018E04 File Offset: 0x00017004
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			if (count == 0)
			{
				return new byte[0];
			}
			int num = bytes.Length;
			if (offset < 0 || offset >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			MemoryStream memoryStream = new MemoryStream();
			int num2 = offset + count;
			for (int i = offset; i < num2; i++)
			{
				char c = (char)bytes[i];
				if (c == '+')
				{
					c = ' ';
				}
				else if (c == '%' && i < num2 - 2)
				{
					int @char = HttpUtility.GetChar(bytes, i + 1, 2);
					if (@char != -1)
					{
						c = (char)@char;
						i += 2;
					}
				}
				memoryStream.WriteByte((byte)c);
			}
			return memoryStream.ToArray();
		}

		/// <summary>Encodes a URL string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="str">The text to encode. </param>
		// Token: 0x06000A34 RID: 2612 RVA: 0x00018EA8 File Offset: 0x000170A8
		public static string UrlEncode(string str)
		{
			return HttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		/// <summary>Encodes a URL string using the specified encoding object.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="str">The text to encode. </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding scheme. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x00018EB8 File Offset: 0x000170B8
		public static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str == string.Empty)
			{
				return string.Empty;
			}
			bool flag = false;
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !HttpEncoder.NotEncoded(c))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return str;
			}
			byte[] array = new byte[e.GetMaxByteCount(str.Length)];
			int bytes = e.GetBytes(str, 0, str.Length, array, 0);
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(array, 0, bytes));
		}

		/// <summary>Converts a byte array into an encoded URL string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="bytes">The array of bytes to encode. </param>
		// Token: 0x06000A36 RID: 2614 RVA: 0x00018F6B File Offset: 0x0001716B
		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length));
		}

		/// <summary>Converts a byte array into a URL-encoded string, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="bytes">The array of bytes to encode. </param>
		/// <param name="offset">The position in the byte array at which to begin encoding. </param>
		/// <param name="count">The number of bytes to encode. </param>
		// Token: 0x06000A37 RID: 2615 RVA: 0x00018F90 File Offset: 0x00017190
		public static string UrlEncode(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, offset, count));
		}

		/// <summary>Converts a string into a URL-encoded array of bytes.</summary>
		/// <returns>An encoded array of bytes.</returns>
		/// <param name="str">The string to encode. </param>
		// Token: 0x06000A38 RID: 2616 RVA: 0x00018FB3 File Offset: 0x000171B3
		public static byte[] UrlEncodeToBytes(string str)
		{
			return HttpUtility.UrlEncodeToBytes(str, Encoding.UTF8);
		}

		/// <summary>Converts a string into a URL-encoded array of bytes using the specified encoding object.</summary>
		/// <returns>An encoded array of bytes.</returns>
		/// <param name="str">The string to encode </param>
		/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the encoding scheme. </param>
		// Token: 0x06000A39 RID: 2617 RVA: 0x00018FC0 File Offset: 0x000171C0
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str.Length == 0)
			{
				return new byte[0];
			}
			byte[] bytes = e.GetBytes(str);
			return HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		/// <summary>Converts an array of bytes into a URL-encoded array of bytes.</summary>
		/// <returns>An encoded array of bytes.</returns>
		/// <param name="bytes">The array of bytes to encode. </param>
		// Token: 0x06000A3A RID: 2618 RVA: 0x00018FF3 File Offset: 0x000171F3
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return new byte[0];
			}
			return HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		/// <summary>Converts an array of bytes into a URL-encoded array of bytes, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
		/// <returns>An encoded array of bytes.</returns>
		/// <param name="bytes">The array of bytes to encode. </param>
		/// <param name="offset">The position in the byte array at which to begin encoding. </param>
		/// <param name="count">The number of bytes to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null, but <paramref name="count" /> does not equal 0.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0 or greater than the length of the <paramref name="bytes" /> array.- or -<paramref name="count" /> is less than 0, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001900F File Offset: 0x0001720F
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpEncoder.Current.UrlEncode(bytes, offset, count);
		}

		/// <summary>Converts a string into a Unicode string.</summary>
		/// <returns>A Unicode string in %<paramref name="UnicodeValue" /> notation.</returns>
		/// <param name="str">The string to convert. </param>
		// Token: 0x06000A3C RID: 2620 RVA: 0x00019023 File Offset: 0x00017223
		public static string UrlEncodeUnicode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeUnicodeToBytes(str));
		}

		/// <summary>Converts a Unicode string into an array of bytes.</summary>
		/// <returns>A byte array.</returns>
		/// <param name="str">The string to convert. </param>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0001903C File Offset: 0x0001723C
		public static byte[] UrlEncodeUnicodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.Length == 0)
			{
				return new byte[0];
			}
			MemoryStream memoryStream = new MemoryStream(str.Length);
			for (int i = 0; i < str.Length; i++)
			{
				HttpEncoder.UrlEncodeChar(str[i], memoryStream, true);
			}
			return memoryStream.ToArray();
		}

		/// <summary>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="s">The string to decode. </param>
		// Token: 0x06000A3E RID: 2622 RVA: 0x00019090 File Offset: 0x00017290
		public static string HtmlDecode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlDecode(s, stringWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		/// <summary>Converts a string that has been HTML-encoded into a decoded string, and sends the decoded string to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The string to decode. </param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> stream of output. </param>
		// Token: 0x06000A3F RID: 2623 RVA: 0x000190D8 File Offset: 0x000172D8
		public static void HtmlDecode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!string.IsNullOrEmpty(s))
			{
				HttpEncoder.Current.HtmlDecode(s, output);
			}
		}

		/// <summary>Converts a string to an HTML-encoded string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="s">The string to encode. </param>
		// Token: 0x06000A40 RID: 2624 RVA: 0x000190FC File Offset: 0x000172FC
		public static string HtmlEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlEncode(s, stringWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		/// <summary>Converts a string into an HTML-encoded string, and returns the output as a <see cref="T:System.IO.TextWriter" /> stream of output.</summary>
		/// <param name="s">The string to encode </param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream. </param>
		// Token: 0x06000A41 RID: 2625 RVA: 0x00019144 File Offset: 0x00017344
		public static void HtmlEncode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!string.IsNullOrEmpty(s))
			{
				HttpEncoder.Current.HtmlEncode(s, output);
			}
		}

		/// <summary>Converts an object's string representation into an HTML-encoded string, and returns the encoded string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="value">An object.</param>
		// Token: 0x06000A42 RID: 2626 RVA: 0x00019168 File Offset: 0x00017368
		public static string HtmlEncode(object value)
		{
			if (value == null)
			{
				return null;
			}
			IHtmlString htmlString = value as IHtmlString;
			if (htmlString != null)
			{
				return htmlString.ToHtmlString();
			}
			return HttpUtility.HtmlEncode(value.ToString());
		}

		/// <summary>Encodes a string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="value">A string to encode.</param>
		// Token: 0x06000A43 RID: 2627 RVA: 0x00019196 File Offset: 0x00017396
		public static string JavaScriptStringEncode(string value)
		{
			return HttpUtility.JavaScriptStringEncode(value, false);
		}

		/// <summary>Encodes a string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="value">A string to encode.</param>
		/// <param name="addDoubleQuotes">A value that indicates whether double quotation marks will be included around the encoded string.</param>
		// Token: 0x06000A44 RID: 2628 RVA: 0x000191A0 File Offset: 0x000173A0
		public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!addDoubleQuotes)
				{
					return string.Empty;
				}
				return "\"\"";
			}
			else
			{
				int length = value.Length;
				bool flag = false;
				for (int i = 0; i < length; i++)
				{
					char c = value[i];
					if ((c >= '\0' && c <= '\u001f') || c == '"' || c == '\'' || c == '<' || c == '>' || c == '\\')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (addDoubleQuotes)
					{
						stringBuilder.Append('"');
					}
					for (int j = 0; j < length; j++)
					{
						char c = value[j];
						if ((c >= '\0' && c <= '\a') || (c == '\v' || (c >= '\u000e' && c <= '\u001f')) || c == '\'' || c == '<' || c == '>')
						{
							stringBuilder.AppendFormat("\\u{0:x4}", (int)c);
						}
						else
						{
							int num = (int)c;
							switch (num)
							{
							case 8:
								stringBuilder.Append("\\b");
								goto IL_0174;
							case 9:
								stringBuilder.Append("\\t");
								goto IL_0174;
							case 10:
								stringBuilder.Append("\\n");
								goto IL_0174;
							case 11:
								break;
							case 12:
								stringBuilder.Append("\\f");
								goto IL_0174;
							case 13:
								stringBuilder.Append("\\r");
								goto IL_0174;
							default:
								if (num == 34)
								{
									stringBuilder.Append("\\\"");
									goto IL_0174;
								}
								if (num == 92)
								{
									stringBuilder.Append("\\\\");
									goto IL_0174;
								}
								break;
							}
							stringBuilder.Append(c);
						}
						IL_0174:;
					}
					if (addDoubleQuotes)
					{
						stringBuilder.Append('"');
					}
					return stringBuilder.ToString();
				}
				if (!addDoubleQuotes)
				{
					return value;
				}
				return "\"" + value + "\"";
			}
		}

		/// <summary>Encodes the path portion of a URL string for reliable HTTP transmission from the Web server to a client.</summary>
		/// <returns>The encoded text.</returns>
		/// <param name="str">The text to encode. </param>
		// Token: 0x06000A45 RID: 2629 RVA: 0x00019341 File Offset: 0x00017541
		public static string UrlPathEncode(string str)
		{
			return HttpEncoder.Current.UrlPathEncode(str);
		}

		/// <summary>Parses a query string into a <see cref="T:System.Collections.Specialized.NameValueCollection" /> using <see cref="P:System.Text.Encoding.UTF8" /> encoding.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of query parameters and values.</returns>
		/// <param name="query">The query string to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="query" /> is null. </exception>
		// Token: 0x06000A46 RID: 2630 RVA: 0x0001934E File Offset: 0x0001754E
		public static NameValueCollection ParseQueryString(string query)
		{
			return HttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		/// <summary>Parses a query string into a <see cref="T:System.Collections.Specialized.NameValueCollection" /> using the specified <see cref="T:System.Text.Encoding" />. </summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of query parameters and values.</returns>
		/// <param name="query">The query string to parse.</param>
		/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="query" /> is null.- or -<paramref name="encoding" /> is null.</exception>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0001935C File Offset: 0x0001755C
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
			{
				return new HttpUtility.HttpQSCollection();
			}
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			NameValueCollection nameValueCollection = new HttpUtility.HttpQSCollection();
			HttpUtility.ParseQueryString(query, encoding, nameValueCollection);
			return nameValueCollection;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000193CC File Offset: 0x000175CC
		internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
		{
			if (query.Length == 0)
			{
				return;
			}
			string text = HttpUtility.HtmlDecode(query);
			int length = text.Length;
			int i = 0;
			bool flag = true;
			while (i <= length)
			{
				int num = -1;
				int num2 = -1;
				for (int j = i; j < length; j++)
				{
					if (num == -1 && text[j] == '=')
					{
						num = j + 1;
					}
					else if (text[j] == '&')
					{
						num2 = j;
						break;
					}
				}
				if (flag)
				{
					flag = false;
					if (text[i] == '?')
					{
						i++;
					}
				}
				string text2;
				if (num == -1)
				{
					text2 = null;
					num = i;
				}
				else
				{
					text2 = HttpUtility.UrlDecode(text.Substring(i, num - i - 1), encoding);
				}
				if (num2 < 0)
				{
					i = -1;
					num2 = text.Length;
				}
				else
				{
					i = num2 + 1;
				}
				string text3 = HttpUtility.UrlDecode(text.Substring(num, num2 - num), encoding);
				result.Add(text2, text3);
				if (i == -1)
				{
					break;
				}
			}
		}

		// Token: 0x020000BB RID: 187
		private sealed class HttpQSCollection : NameValueCollection
		{
			// Token: 0x06000A49 RID: 2633 RVA: 0x000194B0 File Offset: 0x000176B0
			public override string ToString()
			{
				int count = this.Count;
				if (count == 0)
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				string[] allKeys = this.AllKeys;
				for (int i = 0; i < count; i++)
				{
					stringBuilder.AppendFormat("{0}={1}&", allKeys[i], HttpUtility.UrlEncode(base[allKeys[i]]));
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
				}
				return stringBuilder.ToString();
			}
		}
	}
}
