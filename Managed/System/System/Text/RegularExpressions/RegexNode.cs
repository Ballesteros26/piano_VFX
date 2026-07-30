using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000152 RID: 338
	internal sealed class RegexNode
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x00033A2D File Offset: 0x00031C2D
		internal RegexNode(int type, RegexOptions options)
		{
			this._type = type;
			this._options = options;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00033A43 File Offset: 0x00031C43
		internal RegexNode(int type, RegexOptions options, char ch)
		{
			this._type = type;
			this._options = options;
			this._ch = ch;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00033A60 File Offset: 0x00031C60
		internal RegexNode(int type, RegexOptions options, string str)
		{
			this._type = type;
			this._options = options;
			this._str = str;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00033A7D File Offset: 0x00031C7D
		internal RegexNode(int type, RegexOptions options, int m)
		{
			this._type = type;
			this._options = options;
			this._m = m;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00033A9A File Offset: 0x00031C9A
		internal RegexNode(int type, RegexOptions options, int m, int n)
		{
			this._type = type;
			this._options = options;
			this._m = m;
			this._n = n;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00033ABF File Offset: 0x00031CBF
		internal bool UseOptionR()
		{
			return (this._options & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00033ACD File Offset: 0x00031CCD
		internal RegexNode ReverseLeft()
		{
			if (this.UseOptionR() && this._type == 25 && this._children != null)
			{
				this._children.Reverse(0, this._children.Count);
			}
			return this;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00033B01 File Offset: 0x00031D01
		internal void MakeRep(int type, int min, int max)
		{
			this._type += type - 9;
			this._m = min;
			this._n = max;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00033B24 File Offset: 0x00031D24
		internal RegexNode Reduce()
		{
			int num = this.Type();
			RegexNode regexNode;
			if (num != 5 && num != 11)
			{
				switch (num)
				{
				case 24:
					return this.ReduceAlternation();
				case 25:
					return this.ReduceConcatenation();
				case 26:
				case 27:
					return this.ReduceRep();
				case 29:
					return this.ReduceGroup();
				}
				regexNode = this;
			}
			else
			{
				regexNode = this.ReduceSet();
			}
			return regexNode;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00033B94 File Offset: 0x00031D94
		internal RegexNode StripEnation(int emptyType)
		{
			int num = this.ChildCount();
			if (num == 0)
			{
				return new RegexNode(emptyType, this._options);
			}
			if (num != 1)
			{
				return this;
			}
			return this.Child(0);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00033BC8 File Offset: 0x00031DC8
		internal RegexNode ReduceGroup()
		{
			RegexNode regexNode = this;
			while (regexNode.Type() == 29)
			{
				regexNode = regexNode.Child(0);
			}
			return regexNode;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00033BEC File Offset: 0x00031DEC
		internal RegexNode ReduceRep()
		{
			RegexNode regexNode = this;
			int num = this.Type();
			int num2 = this._m;
			int num3 = this._n;
			while (regexNode.ChildCount() != 0)
			{
				RegexNode regexNode2 = regexNode.Child(0);
				if (regexNode2.Type() != num)
				{
					int num4 = regexNode2.Type();
					if ((num4 < 3 || num4 > 5 || num != 26) && (num4 < 6 || num4 > 8 || num != 27))
					{
						break;
					}
				}
				if ((regexNode._m == 0 && regexNode2._m > 1) || regexNode2._n < regexNode2._m * 2)
				{
					break;
				}
				regexNode = regexNode2;
				if (regexNode._m > 0)
				{
					num2 = (regexNode._m = ((2147483646 / regexNode._m < num2) ? int.MaxValue : (regexNode._m * num2)));
				}
				if (regexNode._n > 0)
				{
					num3 = (regexNode._n = ((2147483646 / regexNode._n < num3) ? int.MaxValue : (regexNode._n * num3)));
				}
			}
			if (num2 != 2147483647)
			{
				return regexNode;
			}
			return new RegexNode(22, this._options);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00033D00 File Offset: 0x00031F00
		internal RegexNode ReduceSet()
		{
			if (RegexCharClass.IsEmpty(this._str))
			{
				this._type = 22;
				this._str = null;
			}
			else if (RegexCharClass.IsSingleton(this._str))
			{
				this._ch = RegexCharClass.SingletonChar(this._str);
				this._str = null;
				this._type += -2;
			}
			else if (RegexCharClass.IsSingletonInverse(this._str))
			{
				this._ch = RegexCharClass.SingletonChar(this._str);
				this._str = null;
				this._type += -1;
			}
			return this;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00033D98 File Offset: 0x00031F98
		internal RegexNode ReduceAlternation()
		{
			if (this._children == null)
			{
				return new RegexNode(22, this._options);
			}
			bool flag = false;
			bool flag2 = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this._children.Count)
			{
				RegexNode regexNode = this._children[i];
				if (num < i)
				{
					this._children[num] = regexNode;
				}
				if (regexNode._type == 24)
				{
					for (int j = 0; j < regexNode._children.Count; j++)
					{
						regexNode._children[j]._next = this;
					}
					this._children.InsertRange(i + 1, regexNode._children);
					num--;
				}
				else if (regexNode._type == 11 || regexNode._type == 9)
				{
					RegexOptions regexOptions2 = regexNode._options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (regexNode._type == 11)
					{
						if (!flag || regexOptions != regexOptions2 || flag2 || !RegexCharClass.IsMergeable(regexNode._str))
						{
							flag = true;
							flag2 = !RegexCharClass.IsMergeable(regexNode._str);
							regexOptions = regexOptions2;
							goto IL_01D0;
						}
					}
					else if (!flag || regexOptions != regexOptions2 || flag2)
					{
						flag = true;
						flag2 = false;
						regexOptions = regexOptions2;
						goto IL_01D0;
					}
					num--;
					RegexNode regexNode2 = this._children[num];
					RegexCharClass regexCharClass;
					if (regexNode2._type == 9)
					{
						regexCharClass = new RegexCharClass();
						regexCharClass.AddChar(regexNode2._ch);
					}
					else
					{
						regexCharClass = RegexCharClass.Parse(regexNode2._str);
					}
					if (regexNode._type == 9)
					{
						regexCharClass.AddChar(regexNode._ch);
					}
					else
					{
						RegexCharClass regexCharClass2 = RegexCharClass.Parse(regexNode._str);
						regexCharClass.AddCharClass(regexCharClass2);
					}
					regexNode2._type = 11;
					regexNode2._str = regexCharClass.ToStringClass();
				}
				else if (regexNode._type == 22)
				{
					num--;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
				IL_01D0:
				i++;
				num++;
			}
			if (num < i)
			{
				this._children.RemoveRange(num, i - num);
			}
			return this.StripEnation(22);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00033FB4 File Offset: 0x000321B4
		internal RegexNode ReduceConcatenation()
		{
			if (this._children == null)
			{
				return new RegexNode(23, this._options);
			}
			bool flag = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this._children.Count)
			{
				RegexNode regexNode = this._children[i];
				if (num < i)
				{
					this._children[num] = regexNode;
				}
				if (regexNode._type == 25 && (regexNode._options & RegexOptions.RightToLeft) == (this._options & RegexOptions.RightToLeft))
				{
					for (int j = 0; j < regexNode._children.Count; j++)
					{
						regexNode._children[j]._next = this;
					}
					this._children.InsertRange(i + 1, regexNode._children);
					num--;
				}
				else if (regexNode._type == 12 || regexNode._type == 9)
				{
					RegexOptions regexOptions2 = regexNode._options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (!flag || regexOptions != regexOptions2)
					{
						flag = true;
						regexOptions = regexOptions2;
					}
					else
					{
						RegexNode regexNode2 = this._children[--num];
						if (regexNode2._type == 9)
						{
							regexNode2._type = 12;
							regexNode2._str = Convert.ToString(regexNode2._ch, CultureInfo.InvariantCulture);
						}
						if ((regexOptions2 & RegexOptions.RightToLeft) == RegexOptions.None)
						{
							if (regexNode._type == 9)
							{
								RegexNode regexNode3 = regexNode2;
								regexNode3._str += regexNode._ch.ToString();
							}
							else
							{
								RegexNode regexNode4 = regexNode2;
								regexNode4._str += regexNode._str;
							}
						}
						else if (regexNode._type == 9)
						{
							regexNode2._str = regexNode._ch.ToString() + regexNode2._str;
						}
						else
						{
							regexNode2._str = regexNode._str + regexNode2._str;
						}
					}
				}
				else if (regexNode._type == 23)
				{
					num--;
				}
				else
				{
					flag = false;
				}
				i++;
				num++;
			}
			if (num < i)
			{
				this._children.RemoveRange(num, i - num);
			}
			return this.StripEnation(23);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000341CC File Offset: 0x000323CC
		internal RegexNode MakeQuantifier(bool lazy, int min, int max)
		{
			if (min == 0 && max == 0)
			{
				return new RegexNode(23, this._options);
			}
			if (min == 1 && max == 1)
			{
				return this;
			}
			int type = this._type;
			if (type - 9 <= 2)
			{
				this.MakeRep(lazy ? 6 : 3, min, max);
				return this;
			}
			RegexNode regexNode = new RegexNode(lazy ? 27 : 26, this._options, min, max);
			regexNode.AddChild(this);
			return regexNode;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00034234 File Offset: 0x00032434
		internal void AddChild(RegexNode newChild)
		{
			if (this._children == null)
			{
				this._children = new List<RegexNode>(4);
			}
			RegexNode regexNode = newChild.Reduce();
			this._children.Add(regexNode);
			regexNode._next = this;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0003426F File Offset: 0x0003246F
		internal RegexNode Child(int i)
		{
			return this._children[i];
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0003427D File Offset: 0x0003247D
		internal int ChildCount()
		{
			if (this._children != null)
			{
				return this._children.Count;
			}
			return 0;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00034294 File Offset: 0x00032494
		internal int Type()
		{
			return this._type;
		}

		// Token: 0x04000EE7 RID: 3815
		internal const int Oneloop = 3;

		// Token: 0x04000EE8 RID: 3816
		internal const int Notoneloop = 4;

		// Token: 0x04000EE9 RID: 3817
		internal const int Setloop = 5;

		// Token: 0x04000EEA RID: 3818
		internal const int Onelazy = 6;

		// Token: 0x04000EEB RID: 3819
		internal const int Notonelazy = 7;

		// Token: 0x04000EEC RID: 3820
		internal const int Setlazy = 8;

		// Token: 0x04000EED RID: 3821
		internal const int One = 9;

		// Token: 0x04000EEE RID: 3822
		internal const int Notone = 10;

		// Token: 0x04000EEF RID: 3823
		internal const int Set = 11;

		// Token: 0x04000EF0 RID: 3824
		internal const int Multi = 12;

		// Token: 0x04000EF1 RID: 3825
		internal const int Ref = 13;

		// Token: 0x04000EF2 RID: 3826
		internal const int Bol = 14;

		// Token: 0x04000EF3 RID: 3827
		internal const int Eol = 15;

		// Token: 0x04000EF4 RID: 3828
		internal const int Boundary = 16;

		// Token: 0x04000EF5 RID: 3829
		internal const int Nonboundary = 17;

		// Token: 0x04000EF6 RID: 3830
		internal const int ECMABoundary = 41;

		// Token: 0x04000EF7 RID: 3831
		internal const int NonECMABoundary = 42;

		// Token: 0x04000EF8 RID: 3832
		internal const int Beginning = 18;

		// Token: 0x04000EF9 RID: 3833
		internal const int Start = 19;

		// Token: 0x04000EFA RID: 3834
		internal const int EndZ = 20;

		// Token: 0x04000EFB RID: 3835
		internal const int End = 21;

		// Token: 0x04000EFC RID: 3836
		internal const int Nothing = 22;

		// Token: 0x04000EFD RID: 3837
		internal const int Empty = 23;

		// Token: 0x04000EFE RID: 3838
		internal const int Alternate = 24;

		// Token: 0x04000EFF RID: 3839
		internal const int Concatenate = 25;

		// Token: 0x04000F00 RID: 3840
		internal const int Loop = 26;

		// Token: 0x04000F01 RID: 3841
		internal const int Lazyloop = 27;

		// Token: 0x04000F02 RID: 3842
		internal const int Capture = 28;

		// Token: 0x04000F03 RID: 3843
		internal const int Group = 29;

		// Token: 0x04000F04 RID: 3844
		internal const int Require = 30;

		// Token: 0x04000F05 RID: 3845
		internal const int Prevent = 31;

		// Token: 0x04000F06 RID: 3846
		internal const int Greedy = 32;

		// Token: 0x04000F07 RID: 3847
		internal const int Testref = 33;

		// Token: 0x04000F08 RID: 3848
		internal const int Testgroup = 34;

		// Token: 0x04000F09 RID: 3849
		internal int _type;

		// Token: 0x04000F0A RID: 3850
		internal List<RegexNode> _children;

		// Token: 0x04000F0B RID: 3851
		internal string _str;

		// Token: 0x04000F0C RID: 3852
		internal char _ch;

		// Token: 0x04000F0D RID: 3853
		internal int _m;

		// Token: 0x04000F0E RID: 3854
		internal int _n;

		// Token: 0x04000F0F RID: 3855
		internal RegexOptions _options;

		// Token: 0x04000F10 RID: 3856
		internal RegexNode _next;
	}
}
