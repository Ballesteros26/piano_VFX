using System;

namespace System.Net
{
	// Token: 0x020004FE RID: 1278
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x00093D66 File Offset: 0x00091F66
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			if (challenge.Trim().ToLower().IndexOf("basic", StringComparison.Ordinal) == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00093D94 File Offset: 0x00091F94
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00093DCC File Offset: 0x00091FCC
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == "")
			{
				return null;
			}
			string password = credential.Password;
			string domain = credential.Domain;
			byte[] array;
			if (domain == null || domain == "" || domain.Trim() == "")
			{
				array = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				array = BasicClient.GetBytes(string.Concat(new string[] { domain, "\\", userName, ":", password }));
			}
			return new Authorization("Basic " + Convert.ToBase64String(array));
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00093EA6 File Offset: 0x000920A6
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x00093EAF File Offset: 0x000920AF
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}
	}
}
