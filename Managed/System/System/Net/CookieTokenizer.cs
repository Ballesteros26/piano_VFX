using System;

namespace System.Net
{
	// Token: 0x020004B2 RID: 1202
	internal class CookieTokenizer
	{
		// Token: 0x0600237D RID: 9085 RVA: 0x00089A98 File Offset: 0x00087C98
		internal CookieTokenizer(string tokenStream)
		{
			this.m_length = tokenStream.Length;
			this.m_tokenStream = tokenStream;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00089AB3 File Offset: 0x00087CB3
		// (set) Token: 0x0600237F RID: 9087 RVA: 0x00089ABB File Offset: 0x00087CBB
		internal bool EndOfCookie
		{
			get
			{
				return this.m_eofCookie;
			}
			set
			{
				this.m_eofCookie = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x00089AC4 File Offset: 0x00087CC4
		internal bool Eof
		{
			get
			{
				return this.m_index >= this.m_length;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00089AD7 File Offset: 0x00087CD7
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x00089ADF File Offset: 0x00087CDF
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x00089AE8 File Offset: 0x00087CE8
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x00089AF0 File Offset: 0x00087CF0
		internal bool Quoted
		{
			get
			{
				return this.m_quoted;
			}
			set
			{
				this.m_quoted = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x00089AF9 File Offset: 0x00087CF9
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x00089B01 File Offset: 0x00087D01
		internal CookieToken Token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00089B0A File Offset: 0x00087D0A
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x00089B12 File Offset: 0x00087D12
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00089B1C File Offset: 0x00087D1C
		internal string Extract()
		{
			string text = string.Empty;
			if (this.m_tokenLength != 0)
			{
				text = this.m_tokenStream.Substring(this.m_start, this.m_tokenLength);
				if (!this.Quoted)
				{
					text = text.Trim();
				}
			}
			return text;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00089B60 File Offset: 0x00087D60
		internal CookieToken FindNext(bool ignoreComma, bool ignoreEquals)
		{
			this.m_tokenLength = 0;
			this.m_start = this.m_index;
			while (this.m_index < this.m_length && char.IsWhiteSpace(this.m_tokenStream[this.m_index]))
			{
				this.m_index++;
				this.m_start++;
			}
			CookieToken cookieToken = CookieToken.End;
			int num = 1;
			if (!this.Eof)
			{
				if (this.m_tokenStream[this.m_index] == '"')
				{
					this.Quoted = true;
					this.m_index++;
					bool flag = false;
					while (this.m_index < this.m_length)
					{
						char c = this.m_tokenStream[this.m_index];
						if (!flag && c == '"')
						{
							break;
						}
						if (flag)
						{
							flag = false;
						}
						else if (c == '\\')
						{
							flag = true;
						}
						this.m_index++;
					}
					if (this.m_index < this.m_length)
					{
						this.m_index++;
					}
					this.m_tokenLength = this.m_index - this.m_start;
					num = 0;
					ignoreComma = false;
				}
				while (this.m_index < this.m_length && this.m_tokenStream[this.m_index] != ';' && (ignoreEquals || this.m_tokenStream[this.m_index] != '=') && (ignoreComma || this.m_tokenStream[this.m_index] != ','))
				{
					if (this.m_tokenStream[this.m_index] == ',')
					{
						this.m_start = this.m_index + 1;
						this.m_tokenLength = -1;
						ignoreComma = false;
					}
					this.m_index++;
					this.m_tokenLength += num;
				}
				if (!this.Eof)
				{
					char c2 = this.m_tokenStream[this.m_index];
					if (c2 != ';')
					{
						if (c2 != '=')
						{
							cookieToken = CookieToken.EndCookie;
						}
						else
						{
							cookieToken = CookieToken.Equals;
						}
					}
					else
					{
						cookieToken = CookieToken.EndToken;
					}
					this.m_index++;
				}
			}
			return cookieToken;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00089D64 File Offset: 0x00087F64
		internal CookieToken Next(bool first, bool parseResponseCookies)
		{
			this.Reset();
			CookieToken cookieToken = this.FindNext(false, false);
			if (cookieToken == CookieToken.EndCookie)
			{
				this.EndOfCookie = true;
			}
			if (cookieToken == CookieToken.End || cookieToken == CookieToken.EndCookie)
			{
				if ((this.Name = this.Extract()).Length != 0)
				{
					this.Token = this.TokenFromName(parseResponseCookies);
					return CookieToken.Attribute;
				}
				return cookieToken;
			}
			else
			{
				this.Name = this.Extract();
				if (first)
				{
					this.Token = CookieToken.CookieName;
				}
				else
				{
					this.Token = this.TokenFromName(parseResponseCookies);
				}
				if (cookieToken == CookieToken.Equals)
				{
					cookieToken = this.FindNext(!first && this.Token == CookieToken.Expires, true);
					if (cookieToken == CookieToken.EndCookie)
					{
						this.EndOfCookie = true;
					}
					this.Value = this.Extract();
					return CookieToken.NameValuePair;
				}
				return CookieToken.Attribute;
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00089E16 File Offset: 0x00088016
		internal void Reset()
		{
			this.m_eofCookie = false;
			this.m_name = string.Empty;
			this.m_quoted = false;
			this.m_start = this.m_index;
			this.m_token = CookieToken.Nothing;
			this.m_tokenLength = 0;
			this.m_value = string.Empty;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00089E58 File Offset: 0x00088058
		internal CookieToken TokenFromName(bool parseResponseCookies)
		{
			if (!parseResponseCookies)
			{
				for (int i = 0; i < CookieTokenizer.RecognizedServerAttributes.Length; i++)
				{
					if (CookieTokenizer.RecognizedServerAttributes[i].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedServerAttributes[i].Token;
					}
				}
			}
			else
			{
				for (int j = 0; j < CookieTokenizer.RecognizedAttributes.Length; j++)
				{
					if (CookieTokenizer.RecognizedAttributes[j].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedAttributes[j].Token;
					}
				}
			}
			return CookieToken.Unknown;
		}

		// Token: 0x04001FC1 RID: 8129
		private bool m_eofCookie;

		// Token: 0x04001FC2 RID: 8130
		private int m_index;

		// Token: 0x04001FC3 RID: 8131
		private int m_length;

		// Token: 0x04001FC4 RID: 8132
		private string m_name;

		// Token: 0x04001FC5 RID: 8133
		private bool m_quoted;

		// Token: 0x04001FC6 RID: 8134
		private int m_start;

		// Token: 0x04001FC7 RID: 8135
		private CookieToken m_token;

		// Token: 0x04001FC8 RID: 8136
		private int m_tokenLength;

		// Token: 0x04001FC9 RID: 8137
		private string m_tokenStream;

		// Token: 0x04001FCA RID: 8138
		private string m_value;

		// Token: 0x04001FCB RID: 8139
		private static CookieTokenizer.RecognizedAttribute[] RecognizedAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("Max-Age", CookieToken.MaxAge),
			new CookieTokenizer.RecognizedAttribute("Expires", CookieToken.Expires),
			new CookieTokenizer.RecognizedAttribute("Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("Secure", CookieToken.Secure),
			new CookieTokenizer.RecognizedAttribute("Discard", CookieToken.Discard),
			new CookieTokenizer.RecognizedAttribute("Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("Comment", CookieToken.Comment),
			new CookieTokenizer.RecognizedAttribute("CommentURL", CookieToken.CommentUrl),
			new CookieTokenizer.RecognizedAttribute("HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x04001FCC RID: 8140
		private static CookieTokenizer.RecognizedAttribute[] RecognizedServerAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("$Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("$Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("$Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("$Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("$HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x020004B3 RID: 1203
		private struct RecognizedAttribute
		{
			// Token: 0x0600238F RID: 9103 RVA: 0x0008A038 File Offset: 0x00088238
			internal RecognizedAttribute(string name, CookieToken token)
			{
				this.m_name = name;
				this.m_token = token;
			}

			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06002390 RID: 9104 RVA: 0x0008A048 File Offset: 0x00088248
			internal CookieToken Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x06002391 RID: 9105 RVA: 0x0008A050 File Offset: 0x00088250
			internal bool IsEqualTo(string value)
			{
				return string.Compare(this.m_name, value, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04001FCD RID: 8141
			private string m_name;

			// Token: 0x04001FCE RID: 8142
			private CookieToken m_token;
		}
	}
}
