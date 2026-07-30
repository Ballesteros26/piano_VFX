using System;
using System.IO;

namespace System.Xml.Xsl
{
	// Token: 0x020004CB RID: 1227
	internal struct XmlQueryCardinality
	{
		// Token: 0x060031BD RID: 12733 RVA: 0x001209DA File Offset: 0x0011EBDA
		private XmlQueryCardinality(int value)
		{
			this.value = value;
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x001209E3 File Offset: 0x0011EBE3
		public static XmlQueryCardinality None
		{
			get
			{
				return new XmlQueryCardinality(0);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x001209EB File Offset: 0x0011EBEB
		public static XmlQueryCardinality Zero
		{
			get
			{
				return new XmlQueryCardinality(1);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x001209F3 File Offset: 0x0011EBF3
		public static XmlQueryCardinality One
		{
			get
			{
				return new XmlQueryCardinality(2);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x001209FB File Offset: 0x0011EBFB
		public static XmlQueryCardinality ZeroOrOne
		{
			get
			{
				return new XmlQueryCardinality(3);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x00120A03 File Offset: 0x0011EC03
		public static XmlQueryCardinality More
		{
			get
			{
				return new XmlQueryCardinality(4);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x00120A0B File Offset: 0x0011EC0B
		public static XmlQueryCardinality NotOne
		{
			get
			{
				return new XmlQueryCardinality(5);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x00120A13 File Offset: 0x0011EC13
		public static XmlQueryCardinality OneOrMore
		{
			get
			{
				return new XmlQueryCardinality(6);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x00120A1B File Offset: 0x0011EC1B
		public static XmlQueryCardinality ZeroOrMore
		{
			get
			{
				return new XmlQueryCardinality(7);
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x00120A23 File Offset: 0x0011EC23
		public bool Equals(XmlQueryCardinality other)
		{
			return this.value == other.value;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x00120A23 File Offset: 0x0011EC23
		public static bool operator ==(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return left.value == right.value;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x00120A33 File Offset: 0x0011EC33
		public static bool operator !=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return left.value != right.value;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00120A46 File Offset: 0x0011EC46
		public override bool Equals(object other)
		{
			return other is XmlQueryCardinality && this.Equals((XmlQueryCardinality)other);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00120A5E File Offset: 0x0011EC5E
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x00120A66 File Offset: 0x0011EC66
		public static XmlQueryCardinality operator |(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return new XmlQueryCardinality(left.value | right.value);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00120A7A File Offset: 0x0011EC7A
		public static XmlQueryCardinality operator &(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return new XmlQueryCardinality(left.value & right.value);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00120A8E File Offset: 0x0011EC8E
		public static XmlQueryCardinality operator *(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return XmlQueryCardinality.cardinalityProduct[left.value, right.value];
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x00120AA6 File Offset: 0x0011ECA6
		public static XmlQueryCardinality operator +(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return XmlQueryCardinality.cardinalitySum[left.value, right.value];
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00120ABE File Offset: 0x0011ECBE
		public static bool operator <=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return (left.value & ~right.value) == 0;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00120AD1 File Offset: 0x0011ECD1
		public static bool operator >=(XmlQueryCardinality left, XmlQueryCardinality right)
		{
			return (right.value & ~left.value) == 0;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00120AE4 File Offset: 0x0011ECE4
		public XmlQueryCardinality AtMost()
		{
			return new XmlQueryCardinality(this.value | (this.value >> 1) | (this.value >> 2));
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00120B03 File Offset: 0x0011ED03
		public bool NeverSubset(XmlQueryCardinality other)
		{
			return this.value != 0 && (this.value & other.value) == 0;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x00120B1F File Offset: 0x0011ED1F
		public string ToString(string format)
		{
			if (format == "S")
			{
				return XmlQueryCardinality.serialized[this.value];
			}
			return this.ToString();
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00120B47 File Offset: 0x0011ED47
		public override string ToString()
		{
			return XmlQueryCardinality.toString[this.value];
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00120B58 File Offset: 0x0011ED58
		public XmlQueryCardinality(string s)
		{
			this.value = 0;
			for (int i = 0; i < XmlQueryCardinality.serialized.Length; i++)
			{
				if (s == XmlQueryCardinality.serialized[i])
				{
					this.value = i;
					return;
				}
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x00120B95 File Offset: 0x0011ED95
		public void GetObjectData(BinaryWriter writer)
		{
			writer.Write((byte)this.value);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00120BA4 File Offset: 0x0011EDA4
		public XmlQueryCardinality(BinaryReader reader)
		{
			this = new XmlQueryCardinality((int)reader.ReadByte());
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQueryCardinality()
		{
			XmlQueryCardinality[,] array = new XmlQueryCardinality[8, 8];
			array[0, 0] = XmlQueryCardinality.None;
			array[0, 1] = XmlQueryCardinality.Zero;
			array[0, 2] = XmlQueryCardinality.None;
			array[0, 3] = XmlQueryCardinality.Zero;
			array[0, 4] = XmlQueryCardinality.None;
			array[0, 5] = XmlQueryCardinality.Zero;
			array[0, 6] = XmlQueryCardinality.None;
			array[0, 7] = XmlQueryCardinality.Zero;
			array[1, 0] = XmlQueryCardinality.Zero;
			array[1, 1] = XmlQueryCardinality.Zero;
			array[1, 2] = XmlQueryCardinality.Zero;
			array[1, 3] = XmlQueryCardinality.Zero;
			array[1, 4] = XmlQueryCardinality.Zero;
			array[1, 5] = XmlQueryCardinality.Zero;
			array[1, 6] = XmlQueryCardinality.Zero;
			array[1, 7] = XmlQueryCardinality.Zero;
			array[2, 0] = XmlQueryCardinality.None;
			array[2, 1] = XmlQueryCardinality.Zero;
			array[2, 2] = XmlQueryCardinality.One;
			array[2, 3] = XmlQueryCardinality.ZeroOrOne;
			array[2, 4] = XmlQueryCardinality.More;
			array[2, 5] = XmlQueryCardinality.NotOne;
			array[2, 6] = XmlQueryCardinality.OneOrMore;
			array[2, 7] = XmlQueryCardinality.ZeroOrMore;
			array[3, 0] = XmlQueryCardinality.Zero;
			array[3, 1] = XmlQueryCardinality.Zero;
			array[3, 2] = XmlQueryCardinality.ZeroOrOne;
			array[3, 3] = XmlQueryCardinality.ZeroOrOne;
			array[3, 4] = XmlQueryCardinality.NotOne;
			array[3, 5] = XmlQueryCardinality.NotOne;
			array[3, 6] = XmlQueryCardinality.ZeroOrMore;
			array[3, 7] = XmlQueryCardinality.ZeroOrMore;
			array[4, 0] = XmlQueryCardinality.None;
			array[4, 1] = XmlQueryCardinality.Zero;
			array[4, 2] = XmlQueryCardinality.More;
			array[4, 3] = XmlQueryCardinality.NotOne;
			array[4, 4] = XmlQueryCardinality.More;
			array[4, 5] = XmlQueryCardinality.NotOne;
			array[4, 6] = XmlQueryCardinality.More;
			array[4, 7] = XmlQueryCardinality.NotOne;
			array[5, 0] = XmlQueryCardinality.Zero;
			array[5, 1] = XmlQueryCardinality.Zero;
			array[5, 2] = XmlQueryCardinality.NotOne;
			array[5, 3] = XmlQueryCardinality.NotOne;
			array[5, 4] = XmlQueryCardinality.NotOne;
			array[5, 5] = XmlQueryCardinality.NotOne;
			array[5, 6] = XmlQueryCardinality.NotOne;
			array[5, 7] = XmlQueryCardinality.NotOne;
			array[6, 0] = XmlQueryCardinality.None;
			array[6, 1] = XmlQueryCardinality.Zero;
			array[6, 2] = XmlQueryCardinality.OneOrMore;
			array[6, 3] = XmlQueryCardinality.ZeroOrMore;
			array[6, 4] = XmlQueryCardinality.More;
			array[6, 5] = XmlQueryCardinality.NotOne;
			array[6, 6] = XmlQueryCardinality.OneOrMore;
			array[6, 7] = XmlQueryCardinality.ZeroOrMore;
			array[7, 0] = XmlQueryCardinality.Zero;
			array[7, 1] = XmlQueryCardinality.Zero;
			array[7, 2] = XmlQueryCardinality.ZeroOrMore;
			array[7, 3] = XmlQueryCardinality.ZeroOrMore;
			array[7, 4] = XmlQueryCardinality.NotOne;
			array[7, 5] = XmlQueryCardinality.NotOne;
			array[7, 6] = XmlQueryCardinality.ZeroOrMore;
			array[7, 7] = XmlQueryCardinality.ZeroOrMore;
			XmlQueryCardinality.cardinalityProduct = array;
			XmlQueryCardinality[,] array2 = new XmlQueryCardinality[8, 8];
			array2[0, 0] = XmlQueryCardinality.None;
			array2[0, 1] = XmlQueryCardinality.Zero;
			array2[0, 2] = XmlQueryCardinality.One;
			array2[0, 3] = XmlQueryCardinality.ZeroOrOne;
			array2[0, 4] = XmlQueryCardinality.More;
			array2[0, 5] = XmlQueryCardinality.NotOne;
			array2[0, 6] = XmlQueryCardinality.OneOrMore;
			array2[0, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[1, 0] = XmlQueryCardinality.Zero;
			array2[1, 1] = XmlQueryCardinality.Zero;
			array2[1, 2] = XmlQueryCardinality.One;
			array2[1, 3] = XmlQueryCardinality.ZeroOrOne;
			array2[1, 4] = XmlQueryCardinality.More;
			array2[1, 5] = XmlQueryCardinality.NotOne;
			array2[1, 6] = XmlQueryCardinality.OneOrMore;
			array2[1, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[2, 0] = XmlQueryCardinality.One;
			array2[2, 1] = XmlQueryCardinality.One;
			array2[2, 2] = XmlQueryCardinality.More;
			array2[2, 3] = XmlQueryCardinality.OneOrMore;
			array2[2, 4] = XmlQueryCardinality.More;
			array2[2, 5] = XmlQueryCardinality.OneOrMore;
			array2[2, 6] = XmlQueryCardinality.More;
			array2[2, 7] = XmlQueryCardinality.OneOrMore;
			array2[3, 0] = XmlQueryCardinality.ZeroOrOne;
			array2[3, 1] = XmlQueryCardinality.ZeroOrOne;
			array2[3, 2] = XmlQueryCardinality.OneOrMore;
			array2[3, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[3, 4] = XmlQueryCardinality.More;
			array2[3, 5] = XmlQueryCardinality.ZeroOrMore;
			array2[3, 6] = XmlQueryCardinality.OneOrMore;
			array2[3, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[4, 0] = XmlQueryCardinality.More;
			array2[4, 1] = XmlQueryCardinality.More;
			array2[4, 2] = XmlQueryCardinality.More;
			array2[4, 3] = XmlQueryCardinality.More;
			array2[4, 4] = XmlQueryCardinality.More;
			array2[4, 5] = XmlQueryCardinality.More;
			array2[4, 6] = XmlQueryCardinality.More;
			array2[4, 7] = XmlQueryCardinality.More;
			array2[5, 0] = XmlQueryCardinality.NotOne;
			array2[5, 1] = XmlQueryCardinality.NotOne;
			array2[5, 2] = XmlQueryCardinality.OneOrMore;
			array2[5, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[5, 4] = XmlQueryCardinality.More;
			array2[5, 5] = XmlQueryCardinality.NotOne;
			array2[5, 6] = XmlQueryCardinality.OneOrMore;
			array2[5, 7] = XmlQueryCardinality.ZeroOrMore;
			array2[6, 0] = XmlQueryCardinality.OneOrMore;
			array2[6, 1] = XmlQueryCardinality.OneOrMore;
			array2[6, 2] = XmlQueryCardinality.More;
			array2[6, 3] = XmlQueryCardinality.OneOrMore;
			array2[6, 4] = XmlQueryCardinality.More;
			array2[6, 5] = XmlQueryCardinality.OneOrMore;
			array2[6, 6] = XmlQueryCardinality.More;
			array2[6, 7] = XmlQueryCardinality.OneOrMore;
			array2[7, 0] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 1] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 2] = XmlQueryCardinality.OneOrMore;
			array2[7, 3] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 4] = XmlQueryCardinality.More;
			array2[7, 5] = XmlQueryCardinality.ZeroOrMore;
			array2[7, 6] = XmlQueryCardinality.OneOrMore;
			array2[7, 7] = XmlQueryCardinality.ZeroOrMore;
			XmlQueryCardinality.cardinalitySum = array2;
			XmlQueryCardinality.toString = new string[] { "", "?", "", "?", "+", "*", "+", "*" };
			XmlQueryCardinality.serialized = new string[] { "None", "Zero", "One", "ZeroOrOne", "More", "NotOne", "OneOrMore", "ZeroOrMore" };
		}

		// Token: 0x0400206B RID: 8299
		private int value;

		// Token: 0x0400206C RID: 8300
		private static readonly XmlQueryCardinality[,] cardinalityProduct;

		// Token: 0x0400206D RID: 8301
		private static readonly XmlQueryCardinality[,] cardinalitySum;

		// Token: 0x0400206E RID: 8302
		private static readonly string[] toString;

		// Token: 0x0400206F RID: 8303
		private static readonly string[] serialized;
	}
}
