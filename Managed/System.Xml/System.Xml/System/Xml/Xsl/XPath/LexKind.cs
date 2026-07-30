using System;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005C2 RID: 1474
	internal enum LexKind
	{
		// Token: 0x0400261E RID: 9758
		Unknown,
		// Token: 0x0400261F RID: 9759
		Or,
		// Token: 0x04002620 RID: 9760
		And,
		// Token: 0x04002621 RID: 9761
		Eq,
		// Token: 0x04002622 RID: 9762
		Ne,
		// Token: 0x04002623 RID: 9763
		Lt,
		// Token: 0x04002624 RID: 9764
		Le,
		// Token: 0x04002625 RID: 9765
		Gt,
		// Token: 0x04002626 RID: 9766
		Ge,
		// Token: 0x04002627 RID: 9767
		Plus,
		// Token: 0x04002628 RID: 9768
		Minus,
		// Token: 0x04002629 RID: 9769
		Multiply,
		// Token: 0x0400262A RID: 9770
		Divide,
		// Token: 0x0400262B RID: 9771
		Modulo,
		// Token: 0x0400262C RID: 9772
		UnaryMinus,
		// Token: 0x0400262D RID: 9773
		Union,
		// Token: 0x0400262E RID: 9774
		LastOperator = 15,
		// Token: 0x0400262F RID: 9775
		DotDot,
		// Token: 0x04002630 RID: 9776
		ColonColon,
		// Token: 0x04002631 RID: 9777
		SlashSlash,
		// Token: 0x04002632 RID: 9778
		Number,
		// Token: 0x04002633 RID: 9779
		Axis,
		// Token: 0x04002634 RID: 9780
		Name,
		// Token: 0x04002635 RID: 9781
		String,
		// Token: 0x04002636 RID: 9782
		Eof,
		// Token: 0x04002637 RID: 9783
		FirstStringable = 21,
		// Token: 0x04002638 RID: 9784
		LastNonChar = 23,
		// Token: 0x04002639 RID: 9785
		LParens = 40,
		// Token: 0x0400263A RID: 9786
		RParens,
		// Token: 0x0400263B RID: 9787
		LBracket = 91,
		// Token: 0x0400263C RID: 9788
		RBracket = 93,
		// Token: 0x0400263D RID: 9789
		Dot = 46,
		// Token: 0x0400263E RID: 9790
		At = 64,
		// Token: 0x0400263F RID: 9791
		Comma = 44,
		// Token: 0x04002640 RID: 9792
		Star = 42,
		// Token: 0x04002641 RID: 9793
		Slash = 47,
		// Token: 0x04002642 RID: 9794
		Dollar = 36,
		// Token: 0x04002643 RID: 9795
		RBrace = 125
	}
}
