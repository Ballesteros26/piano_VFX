using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x02000505 RID: 1285
	internal class DigestSession
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x00094611 File Offset: 0x00092811
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.UtcNow;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0009462B File Offset: 0x0009282B
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00094638 File Offset: 0x00092838
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x00094645 File Offset: 0x00092845
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x00094652 File Offset: 0x00092852
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0009465F File Offset: 0x0009285F
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x0009466C File Offset: 0x0009286C
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000946B0 File Offset: 0x000928B0
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = MD5.Create();
			}
			return true;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00094710 File Offset: 0x00092910
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0009477C File Offset: 0x0009297C
		private string HA1(string username, string password)
		{
			string text = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				text = string.Format("{0}:{1}:{2}", this.HashToHexString(text), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(text);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000947E0 File Offset: 0x000929E0
		private string HA2(HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			this.QOP == "auth-int";
			return this.HashToHexString(text);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00094824 File Offset: 0x00092A24
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00094894 File Offset: 0x00092A94
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.UtcNow;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00094A48 File Offset: 0x00092C48
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x0400210B RID: 8459
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x0400210C RID: 8460
		private DateTime lastUse;

		// Token: 0x0400210D RID: 8461
		private int _nc;

		// Token: 0x0400210E RID: 8462
		private HashAlgorithm hash;

		// Token: 0x0400210F RID: 8463
		private DigestHeaderParser parser;

		// Token: 0x04002110 RID: 8464
		private string _cnonce;
	}
}
