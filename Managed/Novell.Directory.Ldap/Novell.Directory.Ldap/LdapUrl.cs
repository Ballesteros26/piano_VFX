using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003C RID: 60
	public class LdapUrl : ICloneable
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000AEFD File Offset: 0x000090FD
		private void InitBlock()
		{
			this.scope = LdapUrl.DEFAULT_SCOPE;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000AF0A File Offset: 0x0000910A
		public virtual string[] AttributeArray
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000AF12 File Offset: 0x00009112
		public virtual IEnumerator Attributes
		{
			get
			{
				return new ArrayEnumeration(this.attrs);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000AF1F File Offset: 0x0000911F
		public virtual string[] Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000AF27 File Offset: 0x00009127
		public virtual string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000AF2F File Offset: 0x0000912F
		public virtual string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000AF37 File Offset: 0x00009137
		public virtual int Port
		{
			get
			{
				if (this.port == 0)
				{
					return 389;
				}
				return this.port;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000AF4D File Offset: 0x0000914D
		public virtual int Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000AF55 File Offset: 0x00009155
		public virtual bool Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AF5D File Offset: 0x0000915D
		public LdapUrl(string url)
		{
			this.InitBlock();
			this.parseURL(url);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AF72 File Offset: 0x00009172
		public LdapUrl(string host, int port, string dn)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AF98 File Offset: 0x00009198
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = new string[attrNames.Length];
			attrNames.CopyTo(this.attrs, 0);
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B010 File Offset: 0x00009210
		public LdapUrl(string host, int port, string dn, string[] attrNames, int scope, string filter, string[] extensions, bool secure)
		{
			this.InitBlock();
			this.host = host;
			this.port = port;
			this.dn = dn;
			this.attrs = attrNames;
			this.scope = scope;
			this.filter = filter;
			this.extensions = new string[extensions.Length];
			extensions.CopyTo(this.extensions, 0);
			this.secure = secure;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B07C File Offset: 0x0000927C
		public object Clone()
		{
			object obj;
			try
			{
				obj = base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B0B0 File Offset: 0x000092B0
		public static string decode(string URLEncoded)
		{
			int num = 0;
			int i = URLEncoded.IndexOf("%", num);
			if (i < 0)
			{
				return URLEncoded;
			}
			int num2 = 0;
			int length = URLEncoded.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			while (i <= length - 3)
			{
				if (i < 0)
				{
					i = length;
				}
				stringBuilder.Append(URLEncoded.Substring(num2, i - num2));
				i++;
				if (i < length)
				{
					num2 = i + 2;
					try
					{
						stringBuilder.Append((char)Convert.ToInt32(URLEncoded.Substring(i, num2 - i), 16));
					}
					catch (FormatException ex)
					{
						throw new UriFormatException("LdapUrl.decode: error converting hex characters to integer \"" + ex.Message + "\"");
					}
					num = num2;
					if (num != length)
					{
						i = URLEncoded.IndexOf("%", num);
						continue;
					}
				}
				return stringBuilder.ToString();
			}
			throw new UriFormatException("LdapUrl.decode: must be two hex characters following escape character '%'");
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B184 File Offset: 0x00009384
		public static string encode(string toEncode)
		{
			StringBuilder stringBuilder = new StringBuilder(toEncode.Length);
			foreach (char c in toEncode)
			{
				if (c <= '\u001f' || c == '\u007f' || (c >= '\u0080' && c <= 'ÿ') || c == '<' || c == '>' || c == '"' || c == '#' || c == '%' || c == '{' || c == '}' || c == '|' || c == '\\' || c == '^' || c == '~' || c == '[' || c == '\'' || c == ';' || c == '/' || c == '?' || c == ':' || c == '@' || c == '=' || c == '&')
				{
					string text = Convert.ToString((int)c, 16);
					if (text.Length == 1)
					{
						stringBuilder.Append("%0" + text);
					}
					else
					{
						stringBuilder.Append("%" + Convert.ToString((int)c, 16));
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B289 File Offset: 0x00009489
		public virtual string getDN()
		{
			return this.dn;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B291 File Offset: 0x00009491
		internal virtual void setDN(string dn)
		{
			this.dn = dn;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B29C File Offset: 0x0000949C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (this.secure)
			{
				stringBuilder.Append("ldaps://");
			}
			else
			{
				stringBuilder.Append("ldap://");
			}
			if (this.ipV6)
			{
				stringBuilder.Append("[" + this.host + "]");
			}
			else
			{
				stringBuilder.Append(this.host);
			}
			if (this.port != 0)
			{
				stringBuilder.Append(":" + this.port);
			}
			if (this.dn == null && this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("/");
			if (this.dn != null)
			{
				stringBuilder.Append(this.dn);
			}
			if (this.attrs == null && this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.attrs != null)
			{
				for (int i = 0; i < this.attrs.Length; i++)
				{
					stringBuilder.Append(this.attrs[i]);
					if (i < this.attrs.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			if (this.scope == LdapUrl.DEFAULT_SCOPE && this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.scope != LdapUrl.DEFAULT_SCOPE)
			{
				if (this.scope == 1)
				{
					stringBuilder.Append("one");
				}
				else
				{
					stringBuilder.Append("sub");
				}
			}
			if (this.filter == null && this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			if (this.filter == null)
			{
				stringBuilder.Append("?");
			}
			else
			{
				stringBuilder.Append("?" + this.Filter);
			}
			if (this.extensions == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append("?");
			if (this.extensions != null)
			{
				for (int j = 0; j < this.extensions.Length; j++)
				{
					stringBuilder.Append(this.extensions[j]);
					if (j < this.extensions.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B508 File Offset: 0x00009708
		private string[] parseList(string listStr, char delimiter, int listStart, int listEnd)
		{
			if (listEnd - listStart < 1)
			{
				return null;
			}
			int i = listStart;
			int num = 0;
			while (i > 0)
			{
				num++;
				int num2 = listStr.IndexOf(delimiter, i);
				if (num2 <= 0 || num2 >= listEnd)
				{
					break;
				}
				i = num2 + 1;
			}
			i = listStart;
			string[] array = new string[num];
			num = 0;
			while (i > 0)
			{
				int num2 = listStr.IndexOf(delimiter, i);
				if (i > listEnd)
				{
					break;
				}
				if (num2 < 0)
				{
					num2 = listEnd;
				}
				if (num2 > listEnd)
				{
					num2 = listEnd;
				}
				array[num] = listStr.Substring(i, num2 - i);
				i = num2 + 1;
				num++;
			}
			return array;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B588 File Offset: 0x00009788
		private void parseURL(string url)
		{
			int num = 0;
			int num2 = url.Length;
			if (url == null)
			{
				throw new UriFormatException("LdapUrl: URL cannot be null");
			}
			if (url[num] == '<')
			{
				if (url[num2 - 1] != '>')
				{
					throw new UriFormatException("LdapUrl: URL bad enclosure");
				}
				num++;
				num2--;
			}
			if (url.Substring(num, num + 4 - num).ToUpper().Equals("URL:".ToUpper()))
			{
				num += 4;
			}
			if (url.Substring(num, num + 7 - num).ToUpper().Equals("ldap://".ToUpper()))
			{
				num += 7;
				this.port = 389;
			}
			else
			{
				if (!url.Substring(num, num + 8 - num).ToUpper().Equals("ldaps://".ToUpper()))
				{
					throw new UriFormatException("LdapUrl: URL scheme is not ldap");
				}
				this.secure = true;
				num += 8;
				this.port = 636;
			}
			int num3 = url.IndexOf("/", num);
			int num4 = num2;
			bool flag = false;
			if (num3 < 0)
			{
				num3 = url.IndexOf("?", num);
				if (num3 > 0)
				{
					if (url[num3 + 1] == '?')
					{
						num4 = num3;
						num3++;
						flag = true;
					}
					else
					{
						num3 = -1;
					}
				}
			}
			else
			{
				num4 = num3;
			}
			if (url[num] == '[')
			{
				int num5 = url.IndexOf(']', num + 1);
				if (num5 >= num4 || num5 == -1)
				{
					throw new UriFormatException("LdapUrl: \"]\" is missing on IPV6 host name");
				}
				this.host = url.Substring(num + 1, num5 - (num + 1));
				int num6 = url.IndexOf(":", num5);
				if (num6 < num4 && num6 != -1)
				{
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			else
			{
				int num6 = url.IndexOf(":", num);
				if (num6 < 0 || num6 > num4)
				{
					this.host = url.Substring(num, num4 - num);
				}
				else
				{
					this.host = url.Substring(num, num6 - num);
					this.port = int.Parse(url.Substring(num6 + 1, num4 - (num6 + 1)));
				}
			}
			num = num4 + 1;
			if (num >= num2 || num3 < 0)
			{
				return;
			}
			num = num3 + 1;
			int num7 = url.IndexOf('?', num);
			if (num7 < 0)
			{
				this.dn = url.Substring(num, num2 - num);
			}
			else
			{
				this.dn = url.Substring(num, num7 - num);
			}
			num = num7 + 1;
			if (num >= num2 || num7 < 0 || flag)
			{
				return;
			}
			int num8 = url.IndexOf('?', num);
			if (num8 < 0)
			{
				num8 = num2 - 1;
			}
			this.attrs = this.parseList(url, ',', num7 + 1, num8);
			num = num8 + 1;
			if (num >= num2)
			{
				return;
			}
			int num9 = url.IndexOf('?', num);
			string text;
			if (num9 < 0)
			{
				text = url.Substring(num, num2 - num);
			}
			else
			{
				text = url.Substring(num, num9 - num);
			}
			if (text.ToUpper().Equals("".ToUpper()))
			{
				this.scope = 0;
			}
			else if (text.ToUpper().Equals("base".ToUpper()))
			{
				this.scope = 0;
			}
			else if (text.ToUpper().Equals("one".ToUpper()))
			{
				this.scope = 1;
			}
			else
			{
				if (!text.ToUpper().Equals("sub".ToUpper()))
				{
					throw new UriFormatException("LdapUrl: URL invalid scope");
				}
				this.scope = 2;
			}
			num = num9 + 1;
			if (num >= num2 || num9 < 0)
			{
				return;
			}
			num = num9 + 1;
			int num10 = url.IndexOf('?', num);
			string text2;
			if (num10 < 0)
			{
				text2 = url.Substring(num, num2 - num);
			}
			else
			{
				text2 = url.Substring(num, num10 - num);
			}
			if (!text2.Equals(""))
			{
				this.filter = text2;
			}
			num = num10 + 1;
			if (num >= num2 || num10 < 0)
			{
				return;
			}
			if (url.IndexOf('?', num) > 0)
			{
				throw new UriFormatException("LdapUrl: URL has too many ? fields");
			}
			this.extensions = this.parseList(url, ',', num, num2);
		}

		// Token: 0x04000170 RID: 368
		private static readonly int DEFAULT_SCOPE;

		// Token: 0x04000171 RID: 369
		private bool secure;

		// Token: 0x04000172 RID: 370
		private bool ipV6;

		// Token: 0x04000173 RID: 371
		private string host;

		// Token: 0x04000174 RID: 372
		private int port;

		// Token: 0x04000175 RID: 373
		private string dn;

		// Token: 0x04000176 RID: 374
		private string[] attrs;

		// Token: 0x04000177 RID: 375
		private string filter;

		// Token: 0x04000178 RID: 376
		private int scope;

		// Token: 0x04000179 RID: 377
		private string[] extensions;
	}
}
