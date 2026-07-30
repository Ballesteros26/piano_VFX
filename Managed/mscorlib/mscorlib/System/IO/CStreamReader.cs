using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020003E6 RID: 998
	internal class CStreamReader : StreamReader
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x000A7F98 File Offset: 0x000A6198
		public CStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000A7FB4 File Offset: 0x000A61B4
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

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000A7FE0 File Offset: 0x000A61E0
		public override int Read()
		{
			try
			{
				return (int)Console.ReadKey().KeyChar;
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000A8014 File Offset: 0x000A6214
		public override int Read([In] [Out] char[] dest, int index, int count)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest.Length - count)
			{
				throw new ArgumentException("index + count > dest.Length");
			}
			try
			{
				return this.driver.Read(dest, index, count);
			}
			catch (IOException)
			{
			}
			return 0;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000A8094 File Offset: 0x000A6294
		public override string ReadLine()
		{
			try
			{
				return this.driver.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000A80C8 File Offset: 0x000A62C8
		public override string ReadToEnd()
		{
			try
			{
				return this.driver.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04001852 RID: 6226
		private TermInfoDriver driver;
	}
}
