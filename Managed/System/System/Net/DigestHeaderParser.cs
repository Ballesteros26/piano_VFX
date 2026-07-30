using System;

namespace System.Net
{
	// Token: 0x02000504 RID: 1284
	internal class DigestHeaderParser
	{
		// Token: 0x0600265D RID: 9821 RVA: 0x00094288 File Offset: 0x00092488
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000942AE File Offset: 0x000924AE
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000942B8 File Offset: 0x000924B8
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000942C2 File Offset: 0x000924C2
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000942CC File Offset: 0x000924CC
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000942D6 File Offset: 0x000924D6
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000942E0 File Offset: 0x000924E0
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string text;
				string text2;
				if (!this.GetKeywordAndValue(out text, out text2))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, text);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text2;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000943AC File Offset: 0x000925AC
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				c = text[num];
			}
			this.pos--;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0009440C File Offset: 0x0009260C
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00094478 File Offset: 0x00092678
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 < this.length)
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				if (text[num] == '=')
				{
					this.SkipWhitespace();
					if (this.pos + 1 >= this.length)
					{
						return false;
					}
					bool flag = false;
					if (this.header[this.pos] == '"')
					{
						this.pos++;
						flag = true;
					}
					int num2 = this.pos;
					if (flag)
					{
						this.pos = this.header.IndexOf('"', this.pos);
						if (this.pos == -1)
						{
							return false;
						}
					}
					else
					{
						do
						{
							char c = this.header[this.pos];
							if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
							{
								break;
							}
							num = this.pos + 1;
							this.pos = num;
						}
						while (num < this.length);
						if (this.pos >= this.length && num2 == this.pos)
						{
							return false;
						}
					}
					value = this.header.Substring(num2, this.pos - num2);
					this.pos += (flag ? 2 : 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002106 RID: 8454
		private string header;

		// Token: 0x04002107 RID: 8455
		private int length;

		// Token: 0x04002108 RID: 8456
		private int pos;

		// Token: 0x04002109 RID: 8457
		private static string[] keywords = new string[] { "realm", "opaque", "nonce", "algorithm", "qop" };

		// Token: 0x0400210A RID: 8458
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
