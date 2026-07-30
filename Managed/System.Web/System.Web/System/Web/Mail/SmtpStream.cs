using System;
using System.IO;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x02000100 RID: 256
	internal class SmtpStream
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x00024931 File Offset: 0x00022B31
		public SmtpStream(Stream stream)
		{
			this.stream = stream;
			this.encoding = new ASCIIEncoding();
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00024956 File Offset: 0x00022B56
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0002495E File Offset: 0x00022B5E
		public SmtpResponse LastResponse
		{
			get
			{
				return this.lastResponse;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00024966 File Offset: 0x00022B66
		public void WriteRset()
		{
			this.command = "RSET";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00024990 File Offset: 0x00022B90
		public void WriteAuthLogin()
		{
			this.command = "AUTH LOGIN";
			this.WriteLine(this.command);
			this.ReadResponse();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000249AF File Offset: 0x00022BAF
		public bool WriteStartTLS()
		{
			this.command = "STARTTLS";
			this.WriteLine(this.command);
			this.ReadResponse();
			return this.LastResponse.StatusCode == 220;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000249E0 File Offset: 0x00022BE0
		public void WriteEhlo(string hostName)
		{
			this.command = "EHLO " + hostName;
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00024A10 File Offset: 0x00022C10
		public void WriteHelo(string hostName)
		{
			this.command = "HELO " + hostName;
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00024A40 File Offset: 0x00022C40
		public void WriteMailFrom(string from)
		{
			this.command = "MAIL FROM: <" + from + ">";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00024A75 File Offset: 0x00022C75
		public void WriteRcptTo(string to)
		{
			this.command = "RCPT TO: <" + to + ">";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00024AAA File Offset: 0x00022CAA
		public void WriteData()
		{
			this.command = "DATA";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(354);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00024AD4 File Offset: 0x00022CD4
		public void WriteQuit()
		{
			this.command = "QUIT";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(221);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00024AFE File Offset: 0x00022CFE
		public void WriteBoundary(string boundary)
		{
			this.WriteLine("\r\n--{0}", new object[] { boundary });
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00024B15 File Offset: 0x00022D15
		public void WriteFinalBoundary(string boundary)
		{
			this.WriteLine("\r\n--{0}--", new object[] { boundary });
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00024B2C File Offset: 0x00022D2C
		public void WriteDataEndTag()
		{
			this.command = "\r\n.";
			this.WriteLine(this.command);
			this.ReadResponse();
			this.CheckForStatusCode(250);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00024B58 File Offset: 0x00022D58
		public void WriteHeader(MailHeader header)
		{
			foreach (string text in header.Data.AllKeys)
			{
				this.WriteLine("{0}: {1}", new object[]
				{
					text,
					header.Data[text]
				});
			}
			this.WriteLine("");
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public void CheckForStatusCode(int statusCode)
		{
			if (this.LastResponse.StatusCode != statusCode)
			{
				throw new SmtpException(string.Concat(new object[]
				{
					"Server reponse: '",
					this.lastResponse.RawResponse,
					"';Status code: '",
					this.lastResponse.StatusCode,
					"';Expected status code: '",
					statusCode,
					"';Last command: '",
					this.command,
					"'"
				}));
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00024C3C File Offset: 0x00022E3C
		public void WriteBytes(byte[] buffer)
		{
			this.stream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00024C4E File Offset: 0x00022E4E
		public void WriteLine(string format, params object[] args)
		{
			this.WriteLine(string.Format(format, args));
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00024C60 File Offset: 0x00022E60
		public void WriteLine(string line)
		{
			byte[] bytes = this.encoding.GetBytes(line + "\r\n");
			this.stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00024C94 File Offset: 0x00022E94
		public void ReadResponse()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 > 0)
				{
					int num3 = num + num2 - 1;
					if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
					{
						int num4 = num3 - 3;
						while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
						{
							num4--;
						}
						flag = array[num4 + 4] == 32;
					}
					num += num2;
					if (num == array.Length)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, 0, array2, 0, array.Length);
						array = array2;
					}
				}
			}
			while (!flag);
			string @string = this.encoding.GetString(array, 0, num - 1);
			this.lastResponse = SmtpResponse.Parse(@string);
		}

		// Token: 0x04001157 RID: 4439
		protected Stream stream;

		// Token: 0x04001158 RID: 4440
		protected Encoding encoding;

		// Token: 0x04001159 RID: 4441
		protected SmtpResponse lastResponse;

		// Token: 0x0400115A RID: 4442
		protected string command = "";
	}
}
