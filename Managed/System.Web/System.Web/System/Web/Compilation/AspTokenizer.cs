using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System.Web.Compilation
{
	// Token: 0x0200062F RID: 1583
	internal class AspTokenizer
	{
		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000B70E2 File Offset: 0x000B52E2
		public MD5 Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x000B70EC File Offset: 0x000B52EC
		public AspTokenizer(TextReader reader)
		{
			this.sr = reader;
			this.sb = new StringBuilder();
			this.odds = new StringBuilder();
			this.col = (this.line = 1);
			this.hasPutBack = (this.inTag = false);
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000B7153 File Offset: 0x000B5353
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x000B715B File Offset: 0x000B535B
		public bool Verbatim
		{
			get
			{
				return this.verbatim;
			}
			set
			{
				this.verbatim = value;
			}
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000B7164 File Offset: 0x000B5364
		public void put_back()
		{
			if (this.hasPutBack && !this.inTag)
			{
				throw new HttpException("put_back called twice!");
			}
			this.hasPutBack = true;
			if (this.putBackBuffer == null)
			{
				this.putBackBuffer = new Stack();
			}
			string value = this.Value;
			this.putBackBuffer.Push(new AspTokenizer.PutBackItem(value, this.position, this.current_token, this.inTag));
			this.position -= value.Length;
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000B71E4 File Offset: 0x000B53E4
		public int get_token()
		{
			if (this.hasPutBack)
			{
				AspTokenizer.PutBackItem putBackItem;
				if (this.verbatim)
				{
					putBackItem = this.putBackBuffer.Pop() as AspTokenizer.PutBackItem;
					string value = putBackItem.Value;
					int length = value.Length;
					if (length != 0)
					{
						if (length == 1)
						{
							putBackItem = new AspTokenizer.PutBackItem(string.Empty, putBackItem.Position, (int)value[0], false);
						}
						else
						{
							putBackItem = new AspTokenizer.PutBackItem(value, putBackItem.Position, (int)value[0], false);
						}
					}
				}
				else
				{
					putBackItem = this.putBackBuffer.Pop() as AspTokenizer.PutBackItem;
				}
				this.hasPutBack = this.putBackBuffer.Count > 0;
				this.position = putBackItem.Position;
				this.have_value = false;
				this.val = null;
				this.sb = new StringBuilder(putBackItem.Value);
				this.current_token = putBackItem.CurrentToken;
				this.inTag = putBackItem.InTag;
				return this.current_token;
			}
			this.begline = this.line;
			this.begcol = this.col;
			this.have_value = false;
			this.current_token = this.NextToken();
			return this.current_token;
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000B72FC File Offset: 0x000B54FC
		private bool is_identifier_start_character(char c)
		{
			return char.IsLetter(c) || c == '_';
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000B730D File Offset: 0x000B550D
		private bool is_identifier_part_character(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '-';
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000B7323 File Offset: 0x000B5523
		private void ungetc(int value)
		{
			this.have_unget = true;
			this.unget_value = value;
			this.position--;
			this.col--;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000B7350 File Offset: 0x000B5550
		private void TransformNextBlock(int count, bool final)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(this.checksum_buf, 0, count);
			if (this.checksum == null)
			{
				this.checksum = MD5.Create();
			}
			if (final)
			{
				this.checksum.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			else
			{
				this.checksum.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			this.checksum_buf_pos = -1;
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000B73B8 File Offset: 0x000B55B8
		private void UpdateChecksum(int c)
		{
			if (c != -1)
			{
				if (this.checksum_buf_pos + 1 >= 8192)
				{
					this.TransformNextBlock(this.checksum_buf_pos + 1, false);
				}
				char[] array = this.checksum_buf;
				int num = this.checksum_buf_pos + 1;
				this.checksum_buf_pos = num;
				array[num] = (ushort)c;
				return;
			}
			this.TransformNextBlock(this.checksum_buf_pos + 1, true);
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000B7414 File Offset: 0x000B5614
		private int read_char()
		{
			int num;
			if (this.have_unget)
			{
				num = this.unget_value;
				this.have_unget = false;
			}
			else
			{
				num = this.sr.Read();
				this.UpdateChecksum(num);
			}
			if (num == 13 && this.sr.Peek() == 10)
			{
				num = this.sr.Read();
				this.UpdateChecksum(num);
				this.position++;
			}
			if (num == 10)
			{
				this.col = -1;
				this.line++;
			}
			if (num != -1)
			{
				this.col++;
				this.position++;
			}
			return num;
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000B74BC File Offset: 0x000B56BC
		private int ReadAttValue(int start)
		{
			int num = 0;
			bool flag = false;
			if (start == 34 || start == 39)
			{
				num = start;
				flag = true;
			}
			else
			{
				this.sb.Append((char)start);
			}
			int num2 = 0;
			bool flag2 = false;
			this.alternatingQuotes = true;
			int num3;
			while ((num3 = this.sr.Peek()) != -1)
			{
				if (num3 == 37 && num2 == 60)
				{
					flag2 = true;
				}
				else if (flag2 && num3 == 62 && num2 == 37)
				{
					flag2 = false;
				}
				else if (!flag2)
				{
					if (!flag && num3 == 47)
					{
						this.read_char();
						num3 = this.sr.Peek();
						if (num3 == -1)
						{
							num3 = 47;
						}
						else if (num3 == 62)
						{
							this.ungetc(47);
							break;
						}
					}
					else
					{
						if (!flag && (num3 == 62 || char.IsWhiteSpace((char)num3)))
						{
							break;
						}
						if (flag && num3 == num && num2 != 92)
						{
							this.read_char();
							break;
						}
					}
				}
				else if (flag && num3 == num)
				{
					this.alternatingQuotes = false;
				}
				this.sb.Append((char)num3);
				this.read_char();
				num2 = num3;
			}
			return 2097155;
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000B75B8 File Offset: 0x000B57B8
		private int NextToken()
		{
			this.sb.Length = 0;
			this.odds.Length = 0;
			int num;
			while ((num = this.read_char()) != -1)
			{
				if (this.verbatim)
				{
					this.inTag = false;
					this.sb.Append((char)num);
					return num;
				}
				if (this.inTag && this.expectAttrValue && (num == 34 || num == 39))
				{
					return this.ReadAttValue(num);
				}
				if (num == 60)
				{
					this.inTag = true;
					this.sb.Append((char)num);
					return num;
				}
				if (num == 62)
				{
					this.inTag = false;
					this.sb.Append((char)num);
					return num;
				}
				if (this.current_token == 60 && "%/!".IndexOf((char)num) != -1)
				{
					this.sb.Append((char)num);
					return num;
				}
				if (this.inTag && this.current_token == 37 && "@#=".IndexOf((char)num) != -1)
				{
					if (this.odds.Length == 0 || this.odds.ToString().IndexOfAny(AspTokenizer.lfcr) < 0)
					{
						this.sb.Append((char)num);
						return num;
					}
					this.sb.Append((char)num);
				}
				else
				{
					if (this.inTag && num == 45 && this.sr.Peek() == 45)
					{
						this.sb.Append("--");
						this.read_char();
						return 2097157;
					}
					if (!this.inTag)
					{
						this.sb.Append((char)num);
						while ((num = this.sr.Peek()) != -1 && num != 60)
						{
							this.sb.Append((char)this.read_char());
						}
						if (num == -1 && this.sb.Length <= 0)
						{
							return 2097152;
						}
						return 2097156;
					}
					else
					{
						if (this.inTag && this.current_token == 61 && !char.IsWhiteSpace((char)num))
						{
							return this.ReadAttValue(num);
						}
						if (this.inTag && this.is_identifier_start_character((char)num))
						{
							this.sb.Append((char)num);
							while ((num = this.sr.Peek()) != -1 && (this.is_identifier_part_character((char)num) || num == 58))
							{
								this.sb.Append((char)this.read_char());
							}
							if (this.current_token == 64 && Directive.IsDirective(this.sb.ToString()))
							{
								return 2097154;
							}
							return 2097153;
						}
						else
						{
							if (!char.IsWhiteSpace((char)num))
							{
								this.sb.Append((char)num);
								return num;
							}
							this.odds.Append((char)num);
						}
					}
				}
			}
			return 2097152;
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x000B785E File Offset: 0x000B5A5E
		public string Value
		{
			get
			{
				if (this.have_value)
				{
					return this.val;
				}
				this.have_value = true;
				this.val = this.sb.ToString();
				return this.val;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x000B788D File Offset: 0x000B5A8D
		public string Odds
		{
			get
			{
				return this.odds.ToString();
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x000B789A File Offset: 0x000B5A9A
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x000B78A2 File Offset: 0x000B5AA2
		public bool InTag
		{
			get
			{
				return this.inTag;
			}
			set
			{
				this.inTag = value;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x000B78AB File Offset: 0x000B5AAB
		// (set) Token: 0x060043E2 RID: 17378 RVA: 0x000B78B3 File Offset: 0x000B5AB3
		public bool ExpectAttrValue
		{
			get
			{
				return this.expectAttrValue;
			}
			set
			{
				this.expectAttrValue = value;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x000B78BC File Offset: 0x000B5ABC
		public bool AlternatingQuotes
		{
			get
			{
				return this.alternatingQuotes;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000B78C4 File Offset: 0x000B5AC4
		public int BeginLine
		{
			get
			{
				return this.begline;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x000B78CC File Offset: 0x000B5ACC
		public int BeginColumn
		{
			get
			{
				return this.begcol;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000B78D4 File Offset: 0x000B5AD4
		public int EndLine
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x000B78DC File Offset: 0x000B5ADC
		public int EndColumn
		{
			get
			{
				return this.col;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x000B78E4 File Offset: 0x000B5AE4
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04002441 RID: 9281
		private const int CHECKSUM_BUF_SIZE = 8192;

		// Token: 0x04002442 RID: 9282
		private static char[] lfcr = new char[] { '\n', '\r' };

		// Token: 0x04002443 RID: 9283
		private TextReader sr;

		// Token: 0x04002444 RID: 9284
		private int current_token;

		// Token: 0x04002445 RID: 9285
		private StringBuilder sb;

		// Token: 0x04002446 RID: 9286
		private StringBuilder odds;

		// Token: 0x04002447 RID: 9287
		private int col;

		// Token: 0x04002448 RID: 9288
		private int line;

		// Token: 0x04002449 RID: 9289
		private int begcol;

		// Token: 0x0400244A RID: 9290
		private int begline;

		// Token: 0x0400244B RID: 9291
		private int position;

		// Token: 0x0400244C RID: 9292
		private bool inTag;

		// Token: 0x0400244D RID: 9293
		private bool expectAttrValue;

		// Token: 0x0400244E RID: 9294
		private bool alternatingQuotes;

		// Token: 0x0400244F RID: 9295
		private bool hasPutBack;

		// Token: 0x04002450 RID: 9296
		private bool verbatim;

		// Token: 0x04002451 RID: 9297
		private bool have_value;

		// Token: 0x04002452 RID: 9298
		private bool have_unget;

		// Token: 0x04002453 RID: 9299
		private int unget_value;

		// Token: 0x04002454 RID: 9300
		private string val;

		// Token: 0x04002455 RID: 9301
		private Stack putBackBuffer;

		// Token: 0x04002456 RID: 9302
		private MD5 checksum;

		// Token: 0x04002457 RID: 9303
		private char[] checksum_buf = new char[8192];

		// Token: 0x04002458 RID: 9304
		private int checksum_buf_pos = -1;

		// Token: 0x02000630 RID: 1584
		private class PutBackItem
		{
			// Token: 0x060043EA RID: 17386 RVA: 0x000B7903 File Offset: 0x000B5B03
			public PutBackItem(string value, int position, int currentToken, bool inTag)
			{
				this.Value = value;
				this.Position = position;
				this.CurrentToken = currentToken;
				this.InTag = inTag;
			}

			// Token: 0x04002459 RID: 9305
			public readonly string Value;

			// Token: 0x0400245A RID: 9306
			public readonly int Position;

			// Token: 0x0400245B RID: 9307
			public readonly int CurrentToken;

			// Token: 0x0400245C RID: 9308
			public readonly bool InTag;
		}
	}
}
