using System;
using System.Text;

namespace System.IO
{
	// Token: 0x020003E5 RID: 997
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06002EED RID: 12013 RVA: 0x000A7EB0 File Offset: 0x000A60B0
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding)
			: base(stream, encoding, 1024, true)
		{
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000A7EC0 File Offset: 0x000A60C0
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000A7EE8 File Offset: 0x000A60E8
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000A7F14 File Offset: 0x000A6114
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000A7F40 File Offset: 0x000A6140
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000A7F6C File Offset: 0x000A616C
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
