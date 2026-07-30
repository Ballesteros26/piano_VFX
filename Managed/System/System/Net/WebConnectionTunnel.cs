using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000557 RID: 1367
	internal class WebConnectionTunnel
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000A4AFB File Offset: 0x000A2CFB
		public HttpWebRequest Request { get; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000A4B03 File Offset: 0x000A2D03
		public Uri ConnectUri { get; }

		// Token: 0x06002AAA RID: 10922 RVA: 0x000A4B0B File Offset: 0x000A2D0B
		public WebConnectionTunnel(HttpWebRequest request, Uri connectUri)
		{
			this.Request = request;
			this.ConnectUri = connectUri;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x000A4B21 File Offset: 0x000A2D21
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x000A4B29 File Offset: 0x000A2D29
		public bool Success { get; private set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x000A4B32 File Offset: 0x000A2D32
		// (set) Token: 0x06002AAE RID: 10926 RVA: 0x000A4B3A File Offset: 0x000A2D3A
		public bool CloseConnection { get; private set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x000A4B43 File Offset: 0x000A2D43
		// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x000A4B4B File Offset: 0x000A2D4B
		public int StatusCode { get; private set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x000A4B54 File Offset: 0x000A2D54
		// (set) Token: 0x06002AB2 RID: 10930 RVA: 0x000A4B5C File Offset: 0x000A2D5C
		public string StatusDescription { get; private set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000A4B65 File Offset: 0x000A2D65
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x000A4B6D File Offset: 0x000A2D6D
		public string[] Challenge { get; private set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000A4B76 File Offset: 0x000A2D76
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x000A4B7E File Offset: 0x000A2D7E
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000A4B87 File Offset: 0x000A2D87
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x000A4B8F File Offset: 0x000A2D8F
		public Version ProxyVersion { get; private set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000A4B98 File Offset: 0x000A2D98
		// (set) Token: 0x06002ABA RID: 10938 RVA: 0x000A4BA0 File Offset: 0x000A2DA0
		public byte[] Data { get; private set; }

		// Token: 0x06002ABB RID: 10939 RVA: 0x000A4BAC File Offset: 0x000A2DAC
		internal async Task Initialize(Stream stream, CancellationToken cancellationToken)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CONNECT ");
			stringBuilder.Append(this.Request.Address.Host);
			stringBuilder.Append(':');
			stringBuilder.Append(this.Request.Address.Port);
			stringBuilder.Append(" HTTP/");
			if (this.Request.ProtocolVersion == HttpVersion.Version11)
			{
				stringBuilder.Append("1.1");
			}
			else
			{
				stringBuilder.Append("1.0");
			}
			stringBuilder.Append("\r\nHost: ");
			stringBuilder.Append(this.Request.Address.Authority);
			bool flag = false;
			string[] challenge = this.Challenge;
			this.Challenge = null;
			string text = this.Request.Headers["Proxy-Authorization"];
			bool have_auth = text != null;
			if (have_auth)
			{
				stringBuilder.Append("\r\nProxy-Authorization: ");
				stringBuilder.Append(text);
				flag = text.ToUpper().Contains("NTLM");
			}
			else if (challenge != null && this.StatusCode == 407)
			{
				ICredentials credentials = this.Request.Proxy.Credentials;
				have_auth = true;
				if (this.connectRequest == null)
				{
					this.connectRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new object[]
					{
						this.ConnectUri.Scheme,
						"://",
						this.ConnectUri.Host,
						":",
						this.ConnectUri.Port,
						"/"
					}));
					this.connectRequest.Method = "CONNECT";
					this.connectRequest.Credentials = credentials;
				}
				if (credentials != null)
				{
					for (int i = 0; i < challenge.Length; i++)
					{
						Authorization authorization = AuthenticationManager.Authenticate(challenge[i], this.connectRequest, credentials);
						if (authorization != null)
						{
							flag = authorization.ModuleAuthenticationType == "NTLM";
							stringBuilder.Append("\r\nProxy-Authorization: ");
							stringBuilder.Append(authorization.Message);
							break;
						}
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append("\r\nProxy-Connection: keep-alive");
				this.ntlmAuthState++;
			}
			stringBuilder.Append("\r\n\r\n");
			this.StatusCode = 0;
			byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			ValueTuple<WebHeaderCollection, byte[], int> valueTuple = await this.ReadHeaders(stream, cancellationToken).ConfigureAwait(false);
			this.Headers = valueTuple.Item1;
			this.Data = valueTuple.Item2;
			this.StatusCode = valueTuple.Item3;
			if ((!have_auth || this.ntlmAuthState == WebConnectionTunnel.NtlmAuthState.Challenge) && this.Headers != null && this.StatusCode == 407)
			{
				string text2 = this.Headers["Connection"];
				if (!string.IsNullOrEmpty(text2) && text2.ToLower() == "close")
				{
					this.CloseConnection = true;
				}
				this.Challenge = this.Headers.GetValues("Proxy-Authenticate");
				this.Success = false;
			}
			else
			{
				this.Success = this.StatusCode == 200 && this.Headers != null;
			}
			if (this.Challenge == null && (this.StatusCode == 401 || this.StatusCode == 407))
			{
				HttpWebResponse httpWebResponse = new HttpWebResponse(this.ConnectUri, "CONNECT", (HttpStatusCode)this.StatusCode, this.Headers);
				throw new WebException((this.StatusCode == 407) ? "(407) Proxy Authentication Required" : "(401) Unauthorized", null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000A4C04 File Offset: 0x000A2E04
		private async Task<ValueTuple<WebHeaderCollection, byte[], int>> ReadHeaders(Stream stream, CancellationToken cancellationToken)
		{
			byte[] retBuffer = null;
			int status = 200;
			byte[] buffer = new byte[1024];
			MemoryStream ms = new MemoryStream();
			int num2;
			WebHeaderCollection webHeaderCollection;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num = await stream.ReadAsync(buffer, 0, 1024, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				ms.Write(buffer, 0, num);
				num2 = 0;
				string text = null;
				bool flag = false;
				webHeaderCollection = new WebHeaderCollection();
				while (WebConnection.ReadLine(ms.GetBuffer(), ref num2, (int)ms.Length, ref text))
				{
					if (text == null)
					{
						goto Block_2;
					}
					if (flag)
					{
						webHeaderCollection.Add(text);
					}
					else
					{
						string[] array = text.Split(new char[] { ' ' });
						if (array.Length < 2)
						{
							goto Block_6;
						}
						if (string.Compare(array[0], "HTTP/1.1", true) == 0)
						{
							this.ProxyVersion = HttpVersion.Version11;
						}
						else
						{
							if (string.Compare(array[0], "HTTP/1.0", true) != 0)
							{
								goto IL_0232;
							}
							this.ProxyVersion = HttpVersion.Version10;
						}
						status = (int)uint.Parse(array[1]);
						if (array.Length >= 3)
						{
							this.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
						}
						flag = true;
					}
				}
			}
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_2:
			string text2 = webHeaderCollection["Content-Length"];
			int num3;
			if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num3))
			{
				num3 = 0;
			}
			if (ms.Length - (long)num2 - (long)num3 > 0L)
			{
				retBuffer = new byte[ms.Length - (long)num2 - (long)num3];
				Buffer.BlockCopy(ms.GetBuffer(), num2 + num3, retBuffer, 0, retBuffer.Length);
			}
			else
			{
				this.FlushContents(stream, num3 - (int)(ms.Length - (long)num2));
			}
			return new ValueTuple<WebHeaderCollection, byte[], int>(webHeaderCollection, retBuffer, status);
			Block_6:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_0232:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000A4C5C File Offset: 0x000A2E5C
		private void FlushContents(Stream stream, int contentLength)
		{
			while (contentLength > 0)
			{
				byte[] array = new byte[contentLength];
				int num = stream.Read(array, 0, contentLength);
				if (num <= 0)
				{
					break;
				}
				contentLength -= num;
			}
		}

		// Token: 0x04002341 RID: 9025
		private HttpWebRequest connectRequest;

		// Token: 0x04002342 RID: 9026
		private WebConnectionTunnel.NtlmAuthState ntlmAuthState;

		// Token: 0x02000558 RID: 1368
		private enum NtlmAuthState
		{
			// Token: 0x0400234C RID: 9036
			None,
			// Token: 0x0400234D RID: 9037
			Challenge,
			// Token: 0x0400234E RID: 9038
			Response
		}
	}
}
