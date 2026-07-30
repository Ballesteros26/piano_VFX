using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000280 RID: 640
	internal struct StyleSyntaxToken
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x000540A7 File Offset: 0x000522A7
		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000540BF File Offset: 0x000522BF
		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000540D7 File Offset: 0x000522D7
		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}

		// Token: 0x04000987 RID: 2439
		public StyleSyntaxTokenType type;

		// Token: 0x04000988 RID: 2440
		public string text;

		// Token: 0x04000989 RID: 2441
		public int number;
	}
}
