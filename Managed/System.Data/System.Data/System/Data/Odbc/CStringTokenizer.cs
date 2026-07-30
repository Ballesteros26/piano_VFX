using System;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002B5 RID: 693
	internal sealed class CStringTokenizer
	{
		// Token: 0x06001D92 RID: 7570 RVA: 0x00091E3C File Offset: 0x0009003C
		internal CStringTokenizer(string text, char quote, char escape)
		{
			this._token = new StringBuilder();
			this._quote = quote;
			this._escape = escape;
			this._sqlstatement = text;
			if (text != null)
			{
				int num = text.IndexOf('\0');
				this._len = ((0 > num) ? text.Length : num);
				return;
			}
			this._len = 0;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00091E95 File Offset: 0x00090095
		internal int CurrentPosition
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00091EA0 File Offset: 0x000900A0
		internal string NextToken()
		{
			if (this._token.Length != 0)
			{
				this._idx += this._token.Length;
				this._token.Remove(0, this._token.Length);
			}
			while (this._idx < this._len && char.IsWhiteSpace(this._sqlstatement[this._idx]))
			{
				this._idx++;
			}
			if (this._idx == this._len)
			{
				return string.Empty;
			}
			int i = this._idx;
			bool flag = false;
			while (!flag && i < this._len)
			{
				if (this.IsValidNameChar(this._sqlstatement[i]))
				{
					while (i < this._len)
					{
						if (!this.IsValidNameChar(this._sqlstatement[i]))
						{
							break;
						}
						this._token.Append(this._sqlstatement[i]);
						i++;
					}
				}
				else
				{
					char c = this._sqlstatement[i];
					if (c == '[')
					{
						i = this.GetTokenFromBracket(i);
					}
					else
					{
						if (' ' == this._quote || c != this._quote)
						{
							if (!char.IsWhiteSpace(c))
							{
								if (c == ',')
								{
									if (i == this._idx)
									{
										this._token.Append(c);
									}
								}
								else
								{
									this._token.Append(c);
								}
							}
							break;
						}
						i = this.GetTokenFromQuote(i);
					}
				}
			}
			if (this._token.Length <= 0)
			{
				return string.Empty;
			}
			return this._token.ToString();
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00092033 File Offset: 0x00090233
		private int GetTokenFromBracket(int curidx)
		{
			while (curidx < this._len)
			{
				this._token.Append(this._sqlstatement[curidx]);
				curidx++;
				if (this._sqlstatement[curidx - 1] == ']')
				{
					break;
				}
			}
			return curidx;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00092070 File Offset: 0x00090270
		private int GetTokenFromQuote(int curidx)
		{
			int i;
			for (i = curidx; i < this._len; i++)
			{
				this._token.Append(this._sqlstatement[i]);
				if (this._sqlstatement[i] == this._quote && i > curidx && this._sqlstatement[i - 1] != this._escape && i + 1 < this._len && this._sqlstatement[i + 1] != this._quote)
				{
					return i + 1;
				}
			}
			return i;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000920FC File Offset: 0x000902FC
		private bool IsValidNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.' || ch == '$' || ch == '#' || ch == '@' || ch == '~' || ch == '`' || ch == '%' || ch == '^' || ch == '&' || ch == '|';
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00092150 File Offset: 0x00090350
		internal int FindTokenIndex(string tokenString)
		{
			string text;
			do
			{
				text = this.NextToken();
				if (this._idx == this._len || string.IsNullOrEmpty(text))
				{
					return -1;
				}
			}
			while (string.Compare(tokenString, text, StringComparison.OrdinalIgnoreCase) != 0);
			return this._idx;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0009218C File Offset: 0x0009038C
		internal bool StartsWith(string tokenString)
		{
			int num = 0;
			while (num < this._len && char.IsWhiteSpace(this._sqlstatement[num]))
			{
				num++;
			}
			if (this._len - num < tokenString.Length)
			{
				return false;
			}
			if (string.Compare(this._sqlstatement, num, tokenString, 0, tokenString.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._idx = 0;
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x040015A2 RID: 5538
		private readonly StringBuilder _token;

		// Token: 0x040015A3 RID: 5539
		private readonly string _sqlstatement;

		// Token: 0x040015A4 RID: 5540
		private readonly char _quote;

		// Token: 0x040015A5 RID: 5541
		private readonly char _escape;

		// Token: 0x040015A6 RID: 5542
		private int _len;

		// Token: 0x040015A7 RID: 5543
		private int _idx;
	}
}
