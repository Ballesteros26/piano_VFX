using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020003E4 RID: 996
	internal class UnexceptionalStreamReader : StreamReader
	{
		// Token: 0x06002EE5 RID: 12005 RVA: 0x000A7C9C File Offset: 0x000A5E9C
		static UnexceptionalStreamReader()
		{
			string newLine = Environment.NewLine;
			if (newLine.Length == 1)
			{
				UnexceptionalStreamReader.newlineChar = newLine[0];
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
		public UnexceptionalStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000A7CE4 File Offset: 0x000A5EE4
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000A7D10 File Offset: 0x000A5F10
		public override int Read()
		{
			try
			{
				return base.Read();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000A7D3C File Offset: 0x000A5F3C
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (dest_buffer == null)
			{
				throw new ArgumentNullException("dest_buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest_buffer.Length - count)
			{
				throw new ArgumentException("index + count > dest_buffer.Length");
			}
			int num = 0;
			char c = UnexceptionalStreamReader.newlineChar;
			try
			{
				while (count > 0)
				{
					int num2 = base.Read();
					if (num2 < 0)
					{
						break;
					}
					num++;
					count--;
					dest_buffer[index] = (char)num2;
					if (c != '\0')
					{
						if ((char)num2 == c)
						{
							return num;
						}
					}
					else if (this.CheckEOL((char)num2))
					{
						return num;
					}
					index++;
				}
			}
			catch (IOException)
			{
			}
			return num;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000A7DF0 File Offset: 0x000A5FF0
		private bool CheckEOL(char current)
		{
			int i = 0;
			while (i < UnexceptionalStreamReader.newline.Length)
			{
				if (!UnexceptionalStreamReader.newline[i])
				{
					if (current == Environment.NewLine[i])
					{
						UnexceptionalStreamReader.newline[i] = true;
						return i == UnexceptionalStreamReader.newline.Length - 1;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < UnexceptionalStreamReader.newline.Length; j++)
			{
				UnexceptionalStreamReader.newline[j] = false;
			}
			return false;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000A7E58 File Offset: 0x000A6058
		public override string ReadLine()
		{
			try
			{
				return base.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000A7E84 File Offset: 0x000A6084
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04001850 RID: 6224
		private static bool[] newline = new bool[Environment.NewLine.Length];

		// Token: 0x04001851 RID: 6225
		private static char newlineChar;
	}
}
