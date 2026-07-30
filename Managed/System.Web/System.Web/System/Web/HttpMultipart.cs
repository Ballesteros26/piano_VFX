using System;
using System.IO;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000A2 RID: 162
	internal class HttpMultipart
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00013F30 File Offset: 0x00012130
		public HttpMultipart(Stream data, string b, Encoding encoding)
		{
			this.data = data;
			this.boundary = b;
			this.boundary_bytes = encoding.GetBytes(b);
			this.buffer = new byte[this.boundary_bytes.Length + 2];
			this.encoding = encoding;
			this.sb = new StringBuilder();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00013F88 File Offset: 0x00012188
		private string ReadLine()
		{
			bool flag = false;
			this.sb.Length = 0;
			for (;;)
			{
				int num = this.data.ReadByte();
				if (num == -1)
				{
					break;
				}
				if (num == 10)
				{
					goto IL_003D;
				}
				flag = num == 13;
				this.sb.Append((char)num);
			}
			return null;
			IL_003D:
			if (flag)
			{
				StringBuilder stringBuilder = this.sb;
				int length = stringBuilder.Length;
				stringBuilder.Length = length - 1;
			}
			return this.sb.ToString();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00013FF8 File Offset: 0x000121F8
		private static string GetContentDispositionAttribute(string l, string name)
		{
			int num = l.IndexOf(name + "=\"");
			if (num < 0)
			{
				return null;
			}
			int num2 = num + name.Length + "=\"".Length;
			int num3 = l.IndexOf('"', num2);
			if (num3 < 0)
			{
				return null;
			}
			if (num2 == num3)
			{
				return "";
			}
			return l.Substring(num2, num3 - num2);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00014058 File Offset: 0x00012258
		private string GetContentDispositionAttributeWithEncoding(string l, string name)
		{
			int num = l.IndexOf(name + "=\"");
			if (num < 0)
			{
				return null;
			}
			int num2 = num + name.Length + "=\"".Length;
			int num3 = l.IndexOf('"', num2);
			if (num3 < 0)
			{
				return null;
			}
			if (num2 == num3)
			{
				return "";
			}
			string text = l.Substring(num2, num3 - num2);
			byte[] array = new byte[text.Length];
			for (int i = text.Length - 1; i >= 0; i--)
			{
				array[i] = (byte)text[i];
			}
			return this.encoding.GetString(array);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000140F8 File Offset: 0x000122F8
		private bool ReadBoundary()
		{
			try
			{
				string text = this.ReadLine();
				while (text == "")
				{
					text = this.ReadLine();
				}
				if (text[0] != '-' || text[1] != '-')
				{
					return false;
				}
				if (!StrUtils.EndsWith(text, this.boundary, false))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00014168 File Offset: 0x00012368
		private string ReadHeaders()
		{
			string text = this.ReadLine();
			if (text == "")
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001418C File Offset: 0x0001238C
		private bool CompareBytes(byte[] orig, byte[] other)
		{
			for (int i = orig.Length - 1; i >= 0; i--)
			{
				if (orig[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000141B4 File Offset: 0x000123B4
		private long MoveToNextBoundary()
		{
			long num = 0L;
			bool flag = false;
			int num2 = 0;
			int num3 = this.data.ReadByte();
			while (num3 != -1)
			{
				if (num2 == 0 && num3 == 10)
				{
					num = this.data.Position - 1L;
					if (flag)
					{
						num -= 1L;
					}
					num2 = 1;
					num3 = this.data.ReadByte();
				}
				else if (num2 == 0)
				{
					flag = num3 == 13;
					num3 = this.data.ReadByte();
				}
				else if (num2 == 1 && num3 == 45)
				{
					num3 = this.data.ReadByte();
					if (num3 == -1)
					{
						return -1L;
					}
					if (num3 != 45)
					{
						num2 = 0;
						flag = false;
					}
					else
					{
						int num4 = this.data.Read(this.buffer, 0, this.buffer.Length);
						int num5 = this.buffer.Length;
						if (num4 != num5)
						{
							return -1L;
						}
						if (this.CompareBytes(this.boundary_bytes, this.buffer))
						{
							if (this.buffer[num5 - 2] == 45 && this.buffer[num5 - 1] == 45)
							{
								this.at_eof = true;
							}
							else if (this.buffer[num5 - 2] != 13 || this.buffer[num5 - 1] != 10)
							{
								num2 = 0;
								this.data.Position = num + 2L;
								if (flag)
								{
									Stream stream = this.data;
									long num6 = stream.Position;
									stream.Position = num6 + 1L;
									flag = false;
								}
								num3 = this.data.ReadByte();
								continue;
							}
							this.data.Position = num + 2L;
							if (flag)
							{
								Stream stream2 = this.data;
								long num6 = stream2.Position;
								stream2.Position = num6 + 1L;
							}
							return num;
						}
						num2 = 0;
						this.data.Position = num + 2L;
						if (flag)
						{
							Stream stream3 = this.data;
							long num6 = stream3.Position;
							stream3.Position = num6 + 1L;
							flag = false;
						}
						num3 = this.data.ReadByte();
					}
				}
				else
				{
					num2 = 0;
				}
			}
			return -1L;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00014384 File Offset: 0x00012584
		public HttpMultipart.Element ReadNextElement()
		{
			if (this.at_eof || this.ReadBoundary())
			{
				return null;
			}
			HttpMultipart.Element element = new HttpMultipart.Element();
			string text;
			while ((text = this.ReadHeaders()) != null)
			{
				if (StrUtils.StartsWith(text, "Content-Disposition:", true))
				{
					element.Name = HttpMultipart.GetContentDispositionAttribute(text, "name");
					element.Filename = HttpMultipart.StripPath(this.GetContentDispositionAttributeWithEncoding(text, "filename"));
				}
				else if (StrUtils.StartsWith(text, "Content-Type:", true))
				{
					element.ContentType = text.Substring("Content-Type:".Length).Trim();
				}
			}
			long position = this.data.Position;
			element.Start = position;
			long num = this.MoveToNextBoundary();
			if (num == -1L)
			{
				return null;
			}
			element.Length = num - position;
			return element;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00014441 File Offset: 0x00012641
		private static string StripPath(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			if (path.IndexOf(":\\") != 1 && !path.StartsWith("\\\\"))
			{
				return path;
			}
			return path.Substring(path.LastIndexOf('\\') + 1);
		}

		// Token: 0x04000FB7 RID: 4023
		private Stream data;

		// Token: 0x04000FB8 RID: 4024
		private string boundary;

		// Token: 0x04000FB9 RID: 4025
		private byte[] boundary_bytes;

		// Token: 0x04000FBA RID: 4026
		private byte[] buffer;

		// Token: 0x04000FBB RID: 4027
		private bool at_eof;

		// Token: 0x04000FBC RID: 4028
		private Encoding encoding;

		// Token: 0x04000FBD RID: 4029
		private StringBuilder sb;

		// Token: 0x04000FBE RID: 4030
		private const byte HYPHEN = 45;

		// Token: 0x04000FBF RID: 4031
		private const byte LF = 10;

		// Token: 0x04000FC0 RID: 4032
		private const byte CR = 13;

		// Token: 0x020000A3 RID: 163
		public class Element
		{
			// Token: 0x06000828 RID: 2088 RVA: 0x00014480 File Offset: 0x00012680
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"ContentType ",
					this.ContentType,
					", Name ",
					this.Name,
					", Filename ",
					this.Filename,
					", Start ",
					this.Start.ToString(),
					", Length ",
					this.Length.ToString()
				});
			}

			// Token: 0x04000FC1 RID: 4033
			public string ContentType;

			// Token: 0x04000FC2 RID: 4034
			public string Name;

			// Token: 0x04000FC3 RID: 4035
			public string Filename;

			// Token: 0x04000FC4 RID: 4036
			public long Start;

			// Token: 0x04000FC5 RID: 4037
			public long Length;
		}
	}
}
