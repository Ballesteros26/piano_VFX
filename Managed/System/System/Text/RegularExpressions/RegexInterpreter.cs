using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200014C RID: 332
	internal sealed class RegexInterpreter : RegexRunner
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x000318DC File Offset: 0x0002FADC
		internal RegexInterpreter(RegexCode code, CultureInfo culture)
		{
			this.runcode = code;
			this.runcodes = code._codes;
			this.runstrings = code._strings;
			this.runfcPrefix = code._fcPrefix;
			this.runbmPrefix = code._bmPrefix;
			this.runanchors = code._anchors;
			this.runculture = culture;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00031939 File Offset: 0x0002FB39
		protected override void InitTrackCount()
		{
			this.runtrackcount = this.runcode._trackcount;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003194C File Offset: 0x0002FB4C
		private void Advance()
		{
			this.Advance(0);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00031955 File Offset: 0x0002FB55
		private void Advance(int i)
		{
			this.runcodepos += i + 1;
			this.SetOperator(this.runcodes[this.runcodepos]);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003197A File Offset: 0x0002FB7A
		private void Goto(int newpos)
		{
			if (newpos < this.runcodepos)
			{
				base.EnsureStorage();
			}
			this.SetOperator(this.runcodes[newpos]);
			this.runcodepos = newpos;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000319A0 File Offset: 0x0002FBA0
		private void Textto(int newpos)
		{
			this.runtextpos = newpos;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000319A9 File Offset: 0x0002FBA9
		private void Trackto(int newpos)
		{
			this.runtrackpos = this.runtrack.Length - newpos;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000319BB File Offset: 0x0002FBBB
		private int Textstart()
		{
			return this.runtextstart;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000319C3 File Offset: 0x0002FBC3
		private int Textpos()
		{
			return this.runtextpos;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000319CB File Offset: 0x0002FBCB
		private int Trackpos()
		{
			return this.runtrack.Length - this.runtrackpos;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000319DC File Offset: 0x0002FBDC
		private void TrackPush()
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = this.runcodepos;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00031A08 File Offset: 0x0002FC08
		private void TrackPush(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = this.runcodepos;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00031A4C File Offset: 0x0002FC4C
		private void TrackPush(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = this.runcodepos;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00031AAC File Offset: 0x0002FCAC
		private void TrackPush(int I1, int I2, int I3)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = I3;
			int[] runtrack4 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack4[num] = this.runcodepos;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00031B24 File Offset: 0x0002FD24
		private void TrackPush2(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = -this.runcodepos;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00031B6C File Offset: 0x0002FD6C
		private void TrackPush2(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = -this.runcodepos;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00031BCC File Offset: 0x0002FDCC
		private void Backtrack()
		{
			int[] runtrack = this.runtrack;
			int runtrackpos = this.runtrackpos;
			this.runtrackpos = runtrackpos + 1;
			int num = runtrack[runtrackpos];
			if (num < 0)
			{
				num = -num;
				this.SetOperator(this.runcodes[num] | 256);
			}
			else
			{
				this.SetOperator(this.runcodes[num] | 128);
			}
			if (num < this.runcodepos)
			{
				base.EnsureStorage();
			}
			this.runcodepos = num;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00031C39 File Offset: 0x0002FE39
		private void SetOperator(int op)
		{
			this.runci = (op & 512) != 0;
			this.runrtl = (op & 64) != 0;
			this.runoperator = op & -577;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00031C65 File Offset: 0x0002FE65
		private void TrackPop()
		{
			this.runtrackpos++;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00031C75 File Offset: 0x0002FE75
		private void TrackPop(int framesize)
		{
			this.runtrackpos += framesize;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00031C85 File Offset: 0x0002FE85
		private int TrackPeek()
		{
			return this.runtrack[this.runtrackpos - 1];
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00031C96 File Offset: 0x0002FE96
		private int TrackPeek(int i)
		{
			return this.runtrack[this.runtrackpos - i - 1];
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00031CAC File Offset: 0x0002FEAC
		private void StackPush(int I1)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00031CD4 File Offset: 0x0002FED4
		private void StackPush(int I1, int I2)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
			int[] runstack2 = this.runstack;
			num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack2[num] = I2;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00031D13 File Offset: 0x0002FF13
		private void StackPop()
		{
			this.runstackpos++;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00031D23 File Offset: 0x0002FF23
		private void StackPop(int framesize)
		{
			this.runstackpos += framesize;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00031D33 File Offset: 0x0002FF33
		private int StackPeek()
		{
			return this.runstack[this.runstackpos - 1];
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00031D44 File Offset: 0x0002FF44
		private int StackPeek(int i)
		{
			return this.runstack[this.runstackpos - i - 1];
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00031D57 File Offset: 0x0002FF57
		private int Operator()
		{
			return this.runoperator;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00031D5F File Offset: 0x0002FF5F
		private int Operand(int i)
		{
			return this.runcodes[this.runcodepos + i + 1];
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00031D72 File Offset: 0x0002FF72
		private int Leftchars()
		{
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00031D81 File Offset: 0x0002FF81
		private int Rightchars()
		{
			return this.runtextend - this.runtextpos;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00031D90 File Offset: 0x0002FF90
		private int Bump()
		{
			if (!this.runrtl)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00031D9D File Offset: 0x0002FF9D
		private int Forwardchars()
		{
			if (!this.runrtl)
			{
				return this.runtextend - this.runtextpos;
			}
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00031DC4 File Offset: 0x0002FFC4
		private char Forwardcharnext()
		{
			char c;
			if (!this.runrtl)
			{
				string runtext = this.runtext;
				int num = this.runtextpos;
				this.runtextpos = num + 1;
				c = runtext[num];
			}
			else
			{
				string runtext2 = this.runtext;
				int num = this.runtextpos - 1;
				this.runtextpos = num;
				c = runtext2[num];
			}
			char c2 = c;
			if (!this.runci)
			{
				return c2;
			}
			return char.ToLower(c2, this.runculture);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00031E2C File Offset: 0x0003002C
		private bool Stringmatch(string str)
		{
			int num;
			int num2;
			if (!this.runrtl)
			{
				if (this.runtextend - this.runtextpos < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos + num;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos;
			}
			if (!this.runci)
			{
				while (num != 0)
				{
					if (str[--num] != this.runtext[--num2])
					{
						return false;
					}
				}
			}
			else
			{
				while (num != 0)
				{
					if (str[--num] != char.ToLower(this.runtext[--num2], this.runculture))
					{
						return false;
					}
				}
			}
			if (!this.runrtl)
			{
				num2 += str.Length;
			}
			this.runtextpos = num2;
			return true;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00031EFC File Offset: 0x000300FC
		private bool Refmatch(int index, int len)
		{
			int num;
			if (!this.runrtl)
			{
				if (this.runtextend - this.runtextpos < len)
				{
					return false;
				}
				num = this.runtextpos + len;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < len)
				{
					return false;
				}
				num = this.runtextpos;
			}
			int num2 = index + len;
			int num3 = len;
			if (!this.runci)
			{
				while (num3-- != 0)
				{
					if (this.runtext[--num2] != this.runtext[--num])
					{
						return false;
					}
				}
			}
			else
			{
				while (num3-- != 0)
				{
					if (char.ToLower(this.runtext[--num2], this.runculture) != char.ToLower(this.runtext[--num], this.runculture))
					{
						return false;
					}
				}
			}
			if (!this.runrtl)
			{
				num += len;
			}
			this.runtextpos = num;
			return true;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00031FD9 File Offset: 0x000301D9
		private void Backwardnext()
		{
			this.runtextpos += (this.runrtl ? 1 : (-1));
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00031FF4 File Offset: 0x000301F4
		private char CharAt(int j)
		{
			return this.runtext[j];
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00032004 File Offset: 0x00030204
		protected override bool FindFirstChar()
		{
			if ((this.runanchors & 53) != 0)
			{
				if (!this.runcode._rightToLeft)
				{
					if (((this.runanchors & 1) != 0 && this.runtextpos > this.runtextbeg) || ((this.runanchors & 4) != 0 && this.runtextpos > this.runtextstart))
					{
						this.runtextpos = this.runtextend;
						return false;
					}
					if ((this.runanchors & 16) != 0 && this.runtextpos < this.runtextend - 1)
					{
						this.runtextpos = this.runtextend - 1;
					}
					else if ((this.runanchors & 32) != 0 && this.runtextpos < this.runtextend)
					{
						this.runtextpos = this.runtextend;
					}
				}
				else
				{
					if (((this.runanchors & 32) != 0 && this.runtextpos < this.runtextend) || ((this.runanchors & 16) != 0 && (this.runtextpos < this.runtextend - 1 || (this.runtextpos == this.runtextend - 1 && this.CharAt(this.runtextpos) != '\n'))) || ((this.runanchors & 4) != 0 && this.runtextpos < this.runtextstart))
					{
						this.runtextpos = this.runtextbeg;
						return false;
					}
					if ((this.runanchors & 1) != 0 && this.runtextpos > this.runtextbeg)
					{
						this.runtextpos = this.runtextbeg;
					}
				}
				return this.runbmPrefix == null || this.runbmPrefix.IsMatch(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
			}
			if (this.runbmPrefix != null)
			{
				this.runtextpos = this.runbmPrefix.Scan(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
				if (this.runtextpos == -1)
				{
					this.runtextpos = (this.runcode._rightToLeft ? this.runtextbeg : this.runtextend);
					return false;
				}
				return true;
			}
			else
			{
				if (this.runfcPrefix == null)
				{
					return true;
				}
				this.runrtl = this.runcode._rightToLeft;
				this.runci = this.runfcPrefix.CaseInsensitive;
				string prefix = this.runfcPrefix.Prefix;
				if (RegexCharClass.IsSingleton(prefix))
				{
					char c = RegexCharClass.SingletonChar(prefix);
					for (int i = this.Forwardchars(); i > 0; i--)
					{
						if (c == this.Forwardcharnext())
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				else
				{
					for (int i = this.Forwardchars(); i > 0; i--)
					{
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), prefix))
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003228C File Offset: 0x0003048C
		protected override void Go()
		{
			this.Goto(0);
			for (;;)
			{
				base.CheckTimeout();
				int num = this.Operator();
				switch (num)
				{
				case 0:
				{
					int num2 = this.Operand(1);
					if (this.Forwardchars() >= num2)
					{
						char c = (char)this.Operand(0);
						while (num2-- > 0)
						{
							if (this.Forwardcharnext() != c)
							{
								goto IL_0E4E;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 1:
				{
					int num3 = this.Operand(1);
					if (this.Forwardchars() >= num3)
					{
						char c2 = (char)this.Operand(0);
						while (num3-- > 0)
						{
							if (this.Forwardcharnext() == c2)
							{
								goto IL_0E4E;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 2:
				{
					int num4 = this.Operand(1);
					if (this.Forwardchars() >= num4)
					{
						string text = this.runstrings[this.Operand(0)];
						while (num4-- > 0)
						{
							if (!RegexCharClass.CharInClass(this.Forwardcharnext(), text))
							{
								goto IL_0E4E;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 3:
				{
					int num5 = this.Operand(1);
					if (num5 > this.Forwardchars())
					{
						num5 = this.Forwardchars();
					}
					char c3 = (char)this.Operand(0);
					int i;
					for (i = num5; i > 0; i--)
					{
						if (this.Forwardcharnext() != c3)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num5 > i)
					{
						this.TrackPush(num5 - i - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 4:
				{
					int num6 = this.Operand(1);
					if (num6 > this.Forwardchars())
					{
						num6 = this.Forwardchars();
					}
					char c4 = (char)this.Operand(0);
					int j;
					for (j = num6; j > 0; j--)
					{
						if (this.Forwardcharnext() == c4)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num6 > j)
					{
						this.TrackPush(num6 - j - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 5:
				{
					int num7 = this.Operand(1);
					if (num7 > this.Forwardchars())
					{
						num7 = this.Forwardchars();
					}
					string text2 = this.runstrings[this.Operand(0)];
					int k;
					for (k = num7; k > 0; k--)
					{
						if (!RegexCharClass.CharInClass(this.Forwardcharnext(), text2))
						{
							this.Backwardnext();
							break;
						}
					}
					if (num7 > k)
					{
						this.TrackPush(num7 - k - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 6:
				case 7:
				{
					int num8 = this.Operand(1);
					if (num8 > this.Forwardchars())
					{
						num8 = this.Forwardchars();
					}
					if (num8 > 0)
					{
						this.TrackPush(num8 - 1, this.Textpos());
					}
					this.Advance(2);
					continue;
				}
				case 8:
				{
					int num9 = this.Operand(1);
					if (num9 > this.Forwardchars())
					{
						num9 = this.Forwardchars();
					}
					if (num9 > 0)
					{
						this.TrackPush(num9 - 1, this.Textpos());
					}
					this.Advance(2);
					continue;
				}
				case 9:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() == (char)this.Operand(0))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 10:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() != (char)this.Operand(0))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 11:
					if (this.Forwardchars() >= 1 && RegexCharClass.CharInClass(this.Forwardcharnext(), this.runstrings[this.Operand(0)]))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 12:
					if (this.Stringmatch(this.runstrings[this.Operand(0)]))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 13:
				{
					int num10 = this.Operand(0);
					if (base.IsMatched(num10))
					{
						if (!this.Refmatch(base.MatchIndex(num10), base.MatchLength(num10)))
						{
							break;
						}
					}
					else if ((this.runregex.roptions & RegexOptions.ECMAScript) == RegexOptions.None)
					{
						break;
					}
					this.Advance(1);
					continue;
				}
				case 14:
					if (this.Leftchars() <= 0 || this.CharAt(this.Textpos() - 1) == '\n')
					{
						this.Advance();
						continue;
					}
					break;
				case 15:
					if (this.Rightchars() <= 0 || this.CharAt(this.Textpos()) == '\n')
					{
						this.Advance();
						continue;
					}
					break;
				case 16:
					if (base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 17:
					if (!base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 18:
					if (this.Leftchars() <= 0)
					{
						this.Advance();
						continue;
					}
					break;
				case 19:
					if (this.Textpos() == this.Textstart())
					{
						this.Advance();
						continue;
					}
					break;
				case 20:
					if (this.Rightchars() <= 1 && (this.Rightchars() != 1 || this.CharAt(this.Textpos()) == '\n'))
					{
						this.Advance();
						continue;
					}
					break;
				case 21:
					if (this.Rightchars() <= 0)
					{
						this.Advance();
						continue;
					}
					break;
				case 22:
					break;
				case 23:
					this.TrackPush(this.Textpos());
					this.Advance(1);
					continue;
				case 24:
					this.StackPop();
					if (this.Textpos() - this.StackPeek() != 0)
					{
						this.TrackPush(this.StackPeek(), this.Textpos());
						this.StackPush(this.Textpos());
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush2(this.StackPeek());
					this.Advance(1);
					continue;
				case 25:
				{
					this.StackPop();
					int num11 = this.StackPeek();
					if (this.Textpos() != num11)
					{
						if (num11 != -1)
						{
							this.TrackPush(num11, this.Textpos());
						}
						else
						{
							this.TrackPush(this.Textpos(), this.Textpos());
						}
					}
					else
					{
						this.StackPush(num11);
						this.TrackPush2(this.StackPeek());
					}
					this.Advance(1);
					continue;
				}
				case 26:
					this.StackPush(-1, this.Operand(0));
					this.TrackPush();
					this.Advance(1);
					continue;
				case 27:
					this.StackPush(this.Textpos(), this.Operand(0));
					this.TrackPush();
					this.Advance(1);
					continue;
				case 28:
				{
					this.StackPop(2);
					int num12 = this.StackPeek();
					int num13 = this.StackPeek(1);
					int num14 = this.Textpos() - num12;
					if (num13 >= this.Operand(1) || (num14 == 0 && num13 >= 0))
					{
						this.TrackPush2(num12, num13);
						this.Advance(2);
						continue;
					}
					this.TrackPush(num12);
					this.StackPush(this.Textpos(), num13 + 1);
					this.Goto(this.Operand(0));
					continue;
				}
				case 29:
				{
					this.StackPop(2);
					int num15 = this.StackPeek();
					int num16 = this.StackPeek(1);
					if (num16 < 0)
					{
						this.TrackPush2(num15);
						this.StackPush(this.Textpos(), num16 + 1);
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush(num15, num16, this.Textpos());
					this.Advance(2);
					continue;
				}
				case 30:
					this.StackPush(-1);
					this.TrackPush();
					this.Advance();
					continue;
				case 31:
					this.StackPush(this.Textpos());
					this.TrackPush();
					this.Advance();
					continue;
				case 32:
					if (this.Operand(1) == -1 || base.IsMatched(this.Operand(1)))
					{
						this.StackPop();
						if (this.Operand(1) != -1)
						{
							base.TransferCapture(this.Operand(0), this.Operand(1), this.StackPeek(), this.Textpos());
						}
						else
						{
							base.Capture(this.Operand(0), this.StackPeek(), this.Textpos());
						}
						this.TrackPush(this.StackPeek());
						this.Advance(2);
						continue;
					}
					break;
				case 33:
					this.StackPop();
					this.TrackPush(this.StackPeek());
					this.Textto(this.StackPeek());
					this.Advance();
					continue;
				case 34:
					this.StackPush(this.Trackpos(), base.Crawlpos());
					this.TrackPush();
					this.Advance();
					continue;
				case 35:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					while (base.Crawlpos() != this.StackPeek(1))
					{
						base.Uncapture();
					}
					break;
				case 36:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					this.TrackPush(this.StackPeek(1));
					this.Advance();
					continue;
				case 37:
					if (base.IsMatched(this.Operand(0)))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 38:
					this.Goto(this.Operand(0));
					continue;
				case 39:
					goto IL_0E3E;
				case 40:
					return;
				case 41:
					if (base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 42:
					if (!base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				default:
					switch (num)
					{
					case 131:
					case 132:
					{
						this.TrackPop(2);
						int num17 = this.TrackPeek();
						int num18 = this.TrackPeek(1);
						this.Textto(num18);
						if (num17 > 0)
						{
							this.TrackPush(num17 - 1, num18 - this.Bump());
						}
						this.Advance(2);
						continue;
					}
					case 133:
					{
						this.TrackPop(2);
						int num19 = this.TrackPeek();
						int num20 = this.TrackPeek(1);
						this.Textto(num20);
						if (num19 > 0)
						{
							this.TrackPush(num19 - 1, num20 - this.Bump());
						}
						this.Advance(2);
						continue;
					}
					case 134:
					{
						this.TrackPop(2);
						int num21 = this.TrackPeek(1);
						this.Textto(num21);
						if (this.Forwardcharnext() == (char)this.Operand(0))
						{
							int num22 = this.TrackPeek();
							if (num22 > 0)
							{
								this.TrackPush(num22 - 1, num21 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 135:
					{
						this.TrackPop(2);
						int num23 = this.TrackPeek(1);
						this.Textto(num23);
						if (this.Forwardcharnext() != (char)this.Operand(0))
						{
							int num24 = this.TrackPeek();
							if (num24 > 0)
							{
								this.TrackPush(num24 - 1, num23 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 136:
					{
						this.TrackPop(2);
						int num25 = this.TrackPeek(1);
						this.Textto(num25);
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), this.runstrings[this.Operand(0)]))
						{
							int num26 = this.TrackPeek();
							if (num26 > 0)
							{
								this.TrackPush(num26 - 1, num25 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 137:
					case 138:
					case 139:
					case 140:
					case 141:
					case 142:
					case 143:
					case 144:
					case 145:
					case 146:
					case 147:
					case 148:
					case 149:
					case 150:
					case 163:
						goto IL_0E3E;
					case 151:
						this.TrackPop();
						this.Textto(this.TrackPeek());
						this.Goto(this.Operand(0));
						continue;
					case 152:
						this.TrackPop(2);
						this.StackPop();
						this.Textto(this.TrackPeek(1));
						this.TrackPush2(this.TrackPeek());
						this.Advance(1);
						continue;
					case 153:
					{
						this.TrackPop(2);
						int num27 = this.TrackPeek(1);
						this.TrackPush2(this.TrackPeek());
						this.StackPush(num27);
						this.Textto(num27);
						this.Goto(this.Operand(0));
						continue;
					}
					case 154:
						this.StackPop(2);
						break;
					case 155:
						this.StackPop(2);
						break;
					case 156:
						this.TrackPop();
						this.StackPop(2);
						if (this.StackPeek(1) > 0)
						{
							this.Textto(this.StackPeek());
							this.TrackPush2(this.TrackPeek(), this.StackPeek(1) - 1);
							this.Advance(2);
							continue;
						}
						this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
						break;
					case 157:
					{
						this.TrackPop(3);
						int num28 = this.TrackPeek();
						int num29 = this.TrackPeek(2);
						if (this.TrackPeek(1) < this.Operand(1) && num29 != num28)
						{
							this.Textto(num29);
							this.StackPush(num29, this.TrackPeek(1) + 1);
							this.TrackPush2(num28);
							this.Goto(this.Operand(0));
							continue;
						}
						this.StackPush(this.TrackPeek(), this.TrackPeek(1));
						break;
					}
					case 158:
					case 159:
						this.StackPop();
						break;
					case 160:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						base.Uncapture();
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							base.Uncapture();
						}
						break;
					case 161:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						break;
					case 162:
						this.StackPop(2);
						break;
					case 164:
						this.TrackPop();
						while (base.Crawlpos() != this.TrackPeek())
						{
							base.Uncapture();
						}
						break;
					default:
						switch (num)
						{
						case 280:
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_0E4E;
						case 281:
							this.StackPop();
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_0E4E;
						case 284:
							this.TrackPop(2);
							this.StackPush(this.TrackPeek(), this.TrackPeek(1));
							goto IL_0E4E;
						case 285:
							this.TrackPop();
							this.StackPop(2);
							this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
							goto IL_0E4E;
						}
						goto Block_3;
					}
					break;
				}
				IL_0E4E:
				this.Backtrack();
			}
			Block_3:
			IL_0E3E:
			throw new NotImplementedException(global::SR.GetString("Unimplemented state."));
		}

		// Token: 0x04000EC1 RID: 3777
		internal int runoperator;

		// Token: 0x04000EC2 RID: 3778
		internal int[] runcodes;

		// Token: 0x04000EC3 RID: 3779
		internal int runcodepos;

		// Token: 0x04000EC4 RID: 3780
		internal string[] runstrings;

		// Token: 0x04000EC5 RID: 3781
		internal RegexCode runcode;

		// Token: 0x04000EC6 RID: 3782
		internal RegexPrefix runfcPrefix;

		// Token: 0x04000EC7 RID: 3783
		internal RegexBoyerMoore runbmPrefix;

		// Token: 0x04000EC8 RID: 3784
		internal int runanchors;

		// Token: 0x04000EC9 RID: 3785
		internal bool runrtl;

		// Token: 0x04000ECA RID: 3786
		internal bool runci;

		// Token: 0x04000ECB RID: 3787
		internal CultureInfo runculture;
	}
}
