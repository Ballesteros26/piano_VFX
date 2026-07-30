using System;
using System.Text;

namespace System.IO
{
	// Token: 0x020003E7 RID: 999
	internal class CStreamWriter : StreamWriter
	{
		// Token: 0x06002EF9 RID: 12025 RVA: 0x000A80FC File Offset: 0x000A62FC
		public CStreamWriter(Stream stream, Encoding encoding, bool leaveOpen)
			: base(stream, encoding, 1024, leaveOpen)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000A811C File Offset: 0x000A631C
		public override void Write(char[] buffer, int index, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (!this.driver.Initialized)
			{
				try
				{
					base.Write(buffer, index, count);
				}
				catch (IOException)
				{
				}
				return;
			}
			lock (this)
			{
				int num = index + count;
				int num2 = index;
				int num3 = 0;
				do
				{
					char c = buffer[num2++];
					if (this.driver.IsSpecialKey(c))
					{
						if (num3 > 0)
						{
							try
							{
								base.Write(buffer, index, num3);
							}
							catch (IOException)
							{
							}
							num3 = 0;
						}
						this.driver.WriteSpecialKey(c);
						index = num2;
					}
					else
					{
						num3++;
					}
				}
				while (num2 < num);
				if (num3 > 0)
				{
					try
					{
						base.Write(buffer, index, num3);
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000A8200 File Offset: 0x000A6400
		public override void Write(char val)
		{
			lock (this)
			{
				try
				{
					if (this.driver.IsSpecialKey(val))
					{
						this.driver.WriteSpecialKey(val);
					}
					else
					{
						this.InternalWriteChar(val);
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000A8268 File Offset: 0x000A6468
		public void InternalWriteString(string val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000A8294 File Offset: 0x000A6494
		public void InternalWriteChar(char val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000A82C0 File Offset: 0x000A64C0
		public void InternalWriteChars(char[] buffer, int n)
		{
			try
			{
				base.Write(buffer, 0, n);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000A82EC File Offset: 0x000A64EC
		public override void Write(char[] val)
		{
			this.Write(val, 0, val.Length);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000A82FC File Offset: 0x000A64FC
		public override void Write(string val)
		{
			if (val == null)
			{
				return;
			}
			if (this.driver.Initialized)
			{
				this.Write(val.ToCharArray());
				return;
			}
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x04001853 RID: 6227
		private TermInfoDriver driver;
	}
}
