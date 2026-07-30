using System;

namespace System.Data
{
	// Token: 0x020000BE RID: 190
	internal sealed class Operators
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00005C14 File Offset: 0x00003E14
		private Operators()
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00033A80 File Offset: 0x00031C80
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00033A9D File Offset: 0x00031C9D
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00033AB9 File Offset: 0x00031CB9
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00033AC9 File Offset: 0x00031CC9
		internal static int Priority(int op)
		{
			if (op > Operators.s_priority.Length)
			{
				return 24;
			}
			return Operators.s_priority[op];
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00033AE0 File Offset: 0x00031CE0
		internal static string ToString(int op)
		{
			string text;
			if (op <= Operators.s_looks.Length)
			{
				text = Operators.s_looks[op];
			}
			else
			{
				text = "Unknown op";
			}
			return text;
		}

		// Token: 0x04000776 RID: 1910
		internal const int Noop = 0;

		// Token: 0x04000777 RID: 1911
		internal const int Negative = 1;

		// Token: 0x04000778 RID: 1912
		internal const int UnaryPlus = 2;

		// Token: 0x04000779 RID: 1913
		internal const int Not = 3;

		// Token: 0x0400077A RID: 1914
		internal const int BetweenAnd = 4;

		// Token: 0x0400077B RID: 1915
		internal const int In = 5;

		// Token: 0x0400077C RID: 1916
		internal const int Between = 6;

		// Token: 0x0400077D RID: 1917
		internal const int EqualTo = 7;

		// Token: 0x0400077E RID: 1918
		internal const int GreaterThen = 8;

		// Token: 0x0400077F RID: 1919
		internal const int LessThen = 9;

		// Token: 0x04000780 RID: 1920
		internal const int GreaterOrEqual = 10;

		// Token: 0x04000781 RID: 1921
		internal const int LessOrEqual = 11;

		// Token: 0x04000782 RID: 1922
		internal const int NotEqual = 12;

		// Token: 0x04000783 RID: 1923
		internal const int Is = 13;

		// Token: 0x04000784 RID: 1924
		internal const int Like = 14;

		// Token: 0x04000785 RID: 1925
		internal const int Plus = 15;

		// Token: 0x04000786 RID: 1926
		internal const int Minus = 16;

		// Token: 0x04000787 RID: 1927
		internal const int Multiply = 17;

		// Token: 0x04000788 RID: 1928
		internal const int Divide = 18;

		// Token: 0x04000789 RID: 1929
		internal const int Modulo = 20;

		// Token: 0x0400078A RID: 1930
		internal const int BitwiseAnd = 22;

		// Token: 0x0400078B RID: 1931
		internal const int BitwiseOr = 23;

		// Token: 0x0400078C RID: 1932
		internal const int BitwiseXor = 24;

		// Token: 0x0400078D RID: 1933
		internal const int BitwiseNot = 25;

		// Token: 0x0400078E RID: 1934
		internal const int And = 26;

		// Token: 0x0400078F RID: 1935
		internal const int Or = 27;

		// Token: 0x04000790 RID: 1936
		internal const int Proc = 28;

		// Token: 0x04000791 RID: 1937
		internal const int Iff = 29;

		// Token: 0x04000792 RID: 1938
		internal const int Qual = 30;

		// Token: 0x04000793 RID: 1939
		internal const int Dot = 31;

		// Token: 0x04000794 RID: 1940
		internal const int Null = 32;

		// Token: 0x04000795 RID: 1941
		internal const int True = 33;

		// Token: 0x04000796 RID: 1942
		internal const int False = 34;

		// Token: 0x04000797 RID: 1943
		internal const int Date = 35;

		// Token: 0x04000798 RID: 1944
		internal const int GenUniqueId = 36;

		// Token: 0x04000799 RID: 1945
		internal const int GenGUID = 37;

		// Token: 0x0400079A RID: 1946
		internal const int GUID = 38;

		// Token: 0x0400079B RID: 1947
		internal const int IsNot = 39;

		// Token: 0x0400079C RID: 1948
		internal const int priStart = 0;

		// Token: 0x0400079D RID: 1949
		internal const int priSubstr = 1;

		// Token: 0x0400079E RID: 1950
		internal const int priParen = 2;

		// Token: 0x0400079F RID: 1951
		internal const int priLow = 3;

		// Token: 0x040007A0 RID: 1952
		internal const int priImp = 4;

		// Token: 0x040007A1 RID: 1953
		internal const int priEqv = 5;

		// Token: 0x040007A2 RID: 1954
		internal const int priXor = 6;

		// Token: 0x040007A3 RID: 1955
		internal const int priOr = 7;

		// Token: 0x040007A4 RID: 1956
		internal const int priAnd = 8;

		// Token: 0x040007A5 RID: 1957
		internal const int priNot = 9;

		// Token: 0x040007A6 RID: 1958
		internal const int priIs = 10;

		// Token: 0x040007A7 RID: 1959
		internal const int priBetweenInLike = 11;

		// Token: 0x040007A8 RID: 1960
		internal const int priBetweenAnd = 12;

		// Token: 0x040007A9 RID: 1961
		internal const int priRelOp = 13;

		// Token: 0x040007AA RID: 1962
		internal const int priConcat = 14;

		// Token: 0x040007AB RID: 1963
		internal const int priContains = 15;

		// Token: 0x040007AC RID: 1964
		internal const int priPlusMinus = 16;

		// Token: 0x040007AD RID: 1965
		internal const int priMod = 17;

		// Token: 0x040007AE RID: 1966
		internal const int priIDiv = 18;

		// Token: 0x040007AF RID: 1967
		internal const int priMulDiv = 19;

		// Token: 0x040007B0 RID: 1968
		internal const int priNeg = 20;

		// Token: 0x040007B1 RID: 1969
		internal const int priExp = 21;

		// Token: 0x040007B2 RID: 1970
		internal const int priProc = 22;

		// Token: 0x040007B3 RID: 1971
		internal const int priDot = 23;

		// Token: 0x040007B4 RID: 1972
		internal const int priMax = 24;

		// Token: 0x040007B5 RID: 1973
		private static readonly int[] s_priority = new int[]
		{
			0, 20, 20, 9, 12, 11, 11, 13, 13, 13,
			13, 13, 13, 10, 11, 16, 16, 19, 19, 18,
			17, 21, 8, 7, 6, 9, 8, 7, 2, 22,
			23, 23, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24
		};

		// Token: 0x040007B6 RID: 1974
		private static readonly string[] s_looks = new string[]
		{
			"", "-", "+", "Not", "BetweenAnd", "In", "Between", "=", ">", "<",
			">=", "<=", "<>", "Is", "Like", "+", "-", "*", "/", "\\",
			"Mod", "**", "&", "|", "^", "~", "And", "Or", "Proc", "Iff",
			".", ".", "Null", "True", "False", "Date", "GenUniqueId()", "GenGuid()", "Guid {..}", "Is Not"
		};
	}
}
