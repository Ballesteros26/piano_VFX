using System;
using System.IO;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000051 RID: 81
	public class SchemaTokenCreator
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		private void Initialise()
		{
			this.ctype = new sbyte[256];
			this.buf = new char[20];
			this.peekchar = int.MaxValue;
			this.WordCharacters(97, 122);
			this.WordCharacters(65, 90);
			this.WordCharacters(160, 255);
			this.WhitespaceCharacters(0, 32);
			this.CommentCharacter(47);
			this.QuoteCharacter(34);
			this.QuoteCharacter(39);
			this.parseNumbers();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public SchemaTokenCreator(Stream instream)
		{
			this.Initialise();
			if (instream == null)
			{
				throw new NullReferenceException();
			}
			this.input = instream;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public SchemaTokenCreator(StreamReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.reader = r;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public SchemaTokenCreator(StringReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.sreader = r;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public void pushBack()
		{
			this.pushedback = true;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000FAF1 File Offset: 0x0000DCF1
		public int CurrentLine
		{
			get
			{
				return this.linenumber;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public string ToStringValue()
		{
			int num = this.lastttype;
			string text;
			switch (num)
			{
			case -5:
				text = this.StringValue;
				break;
			case -4:
			case -2:
				text = "n=" + this.NumberValue;
				break;
			case -3:
				text = this.StringValue;
				break;
			case -1:
				text = "EOF";
				break;
			default:
				if (num != 10)
				{
					if (this.lastttype < 256 && (this.ctype[this.lastttype] & 8) != 0)
					{
						text = this.StringValue;
					}
					else
					{
						char[] array = new char[3];
						array[0] = (array[2] = '\'');
						array[1] = (char)this.lastttype;
						text = new string(array);
					}
				}
				else
				{
					text = "EOL";
				}
				break;
			}
			return text;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
		public void WordCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				sbyte[] array = this.ctype;
				int num = min++;
				array[num] |= 4;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000FBF6 File Offset: 0x0000DDF6
		public void WhitespaceCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 1;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		public void OrdinaryCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 0;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000FC5E File Offset: 0x0000DE5E
		public void OrdinaryCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 0;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public void CommentCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 16;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000FC94 File Offset: 0x0000DE94
		public void InitTable()
		{
			int num = this.ctype.Length;
			while (--num >= 0)
			{
				this.ctype[num] = 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000FCBD File Offset: 0x0000DEBD
		public void QuoteCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 8;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public void parseNumbers()
		{
			for (int i = 48; i <= 57; i++)
			{
				sbyte[] array = this.ctype;
				int num = i;
				array[num] |= 2;
			}
			sbyte[] array2 = this.ctype;
			int num2 = 46;
			array2[num2] |= 2;
			sbyte[] array3 = this.ctype;
			int num3 = 45;
			array3[num3] |= 2;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		private int read()
		{
			if (this.sreader != null)
			{
				return this.sreader.Read();
			}
			if (this.reader != null)
			{
				return this.reader.Read();
			}
			if (this.input != null)
			{
				return this.input.ReadByte();
			}
			throw new SystemException();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public int nextToken()
		{
			if (this.pushedback)
			{
				this.pushedback = false;
				return this.lastttype;
			}
			this.StringValue = null;
			int num = this.peekchar;
			if (num < 0)
			{
				num = int.MaxValue;
			}
			if (num == 2147483646)
			{
				num = this.read();
				if (num < 0)
				{
					return this.lastttype = -1;
				}
				if (num == 10)
				{
					num = int.MaxValue;
				}
			}
			if (num == 2147483647)
			{
				num = this.read();
				if (num < 0)
				{
					return this.lastttype = -1;
				}
			}
			this.lastttype = num;
			this.peekchar = int.MaxValue;
			int num2 = (int)((num < 256) ? this.ctype[num] : 4);
			while ((num2 & 1) != 0)
			{
				if (num == 13)
				{
					this.linenumber++;
					if (this.iseolsig)
					{
						this.peekchar = 2147483646;
						return this.lastttype = 10;
					}
					num = this.read();
					if (num == 10)
					{
						num = this.read();
					}
				}
				else
				{
					if (num == 10)
					{
						this.linenumber++;
						if (this.iseolsig)
						{
							return this.lastttype = 10;
						}
					}
					num = this.read();
				}
				if (num < 0)
				{
					return this.lastttype = -1;
				}
				num2 = (int)((num < 256) ? this.ctype[num] : 4);
			}
			if ((num2 & 2) != 0)
			{
				bool flag = false;
				if (num == 45)
				{
					num = this.read();
					if (num != 46 && (num < 48 || num > 57))
					{
						this.peekchar = num;
						return this.lastttype = 45;
					}
					flag = true;
				}
				double num3 = 0.0;
				int i = 0;
				int num4 = 0;
				for (;;)
				{
					if (num == 46 && num4 == 0)
					{
						num4 = 1;
					}
					else
					{
						if (48 > num || num > 57)
						{
							break;
						}
						num3 = num3 * 10.0 + (double)(num - 48);
						i += num4;
					}
					num = this.read();
				}
				this.peekchar = num;
				if (i != 0)
				{
					double num5 = 10.0;
					for (i--; i > 0; i--)
					{
						num5 *= 10.0;
					}
					num3 /= num5;
				}
				this.NumberValue = (flag ? (-num3) : num3);
				return this.lastttype = -2;
			}
			if ((num2 & 4) != 0)
			{
				int num6 = 0;
				do
				{
					if (num6 >= this.buf.Length)
					{
						char[] array = new char[this.buf.Length * 2];
						Array.Copy(this.buf, 0, array, 0, this.buf.Length);
						this.buf = array;
					}
					this.buf[num6++] = (char)num;
					num = this.read();
					num2 = (int)((num < 0) ? 1 : ((num < 256) ? this.ctype[num] : 4));
				}
				while ((num2 & 6) != 0);
				this.peekchar = num;
				this.StringValue = new string(this.buf, 0, num6);
				if (this.cidtolower)
				{
					this.StringValue = this.StringValue.ToLower();
				}
				return this.lastttype = -3;
			}
			if ((num2 & 8) != 0)
			{
				this.lastttype = num;
				int num7 = 0;
				int num8 = this.read();
				while (num8 >= 0 && num8 != this.lastttype && num8 != 10 && num8 != 13)
				{
					if (num8 == 92)
					{
						num = this.read();
						int num9 = num;
						if (num >= 48 && num <= 55)
						{
							num -= 48;
							int num10 = this.read();
							if (48 <= num10 && num10 <= 55)
							{
								num = (num << 3) + (num10 - 48);
								num10 = this.read();
								if (48 <= num10 && num10 <= 55 && num9 <= 51)
								{
									num = (num << 3) + (num10 - 48);
									num8 = this.read();
								}
								else
								{
									num8 = num10;
								}
							}
							else
							{
								num8 = num10;
							}
						}
						else
						{
							if (num <= 98)
							{
								if (num != 97)
								{
									if (num == 98)
									{
										num = 8;
									}
								}
								else
								{
									num = 7;
								}
							}
							else if (num != 102)
							{
								if (num != 110)
								{
									switch (num)
									{
									case 114:
										num = 13;
										break;
									case 116:
										num = 9;
										break;
									case 118:
										num = 11;
										break;
									}
								}
								else
								{
									num = 10;
								}
							}
							else
							{
								num = 12;
							}
							num8 = this.read();
						}
					}
					else
					{
						num = num8;
						num8 = this.read();
					}
					if (num7 >= this.buf.Length)
					{
						char[] array2 = new char[this.buf.Length * 2];
						Array.Copy(this.buf, 0, array2, 0, this.buf.Length);
						this.buf = array2;
					}
					this.buf[num7++] = (char)num;
				}
				this.peekchar = ((num8 == this.lastttype) ? int.MaxValue : num8);
				this.StringValue = new string(this.buf, 0, num7);
				return this.lastttype;
			}
			if (num == 47 && (this.cppcomments || this.ccomments))
			{
				num = this.read();
				if (num == 42 && this.ccomments)
				{
					int num11 = 0;
					while ((num = this.read()) != 47 || num11 != 42)
					{
						if (num == 13)
						{
							this.linenumber++;
							num = this.read();
							if (num == 10)
							{
								num = this.read();
							}
						}
						else if (num == 10)
						{
							this.linenumber++;
							num = this.read();
						}
						if (num < 0)
						{
							return this.lastttype = -1;
						}
						num11 = num;
					}
					return this.nextToken();
				}
				if (num == 47 && this.cppcomments)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				if ((this.ctype[47] & 16) != 0)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				this.peekchar = num;
				return this.lastttype = 47;
			}
			else
			{
				if ((num2 & 16) != 0)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				return this.lastttype = num;
			}
		}

		// Token: 0x04000212 RID: 530
		private string basestring;

		// Token: 0x04000213 RID: 531
		private bool cppcomments;

		// Token: 0x04000214 RID: 532
		private bool ccomments;

		// Token: 0x04000215 RID: 533
		private bool iseolsig;

		// Token: 0x04000216 RID: 534
		private bool cidtolower;

		// Token: 0x04000217 RID: 535
		private bool pushedback;

		// Token: 0x04000218 RID: 536
		private int peekchar;

		// Token: 0x04000219 RID: 537
		private sbyte[] ctype;

		// Token: 0x0400021A RID: 538
		private int linenumber = 1;

		// Token: 0x0400021B RID: 539
		private int ichar = 1;

		// Token: 0x0400021C RID: 540
		private char[] buf;

		// Token: 0x0400021D RID: 541
		private StreamReader reader;

		// Token: 0x0400021E RID: 542
		private StringReader sreader;

		// Token: 0x0400021F RID: 543
		private Stream input;

		// Token: 0x04000220 RID: 544
		public string StringValue;

		// Token: 0x04000221 RID: 545
		public double NumberValue;

		// Token: 0x04000222 RID: 546
		public int lastttype;
	}
}
