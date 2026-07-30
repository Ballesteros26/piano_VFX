using System;

namespace System.Globalization
{
	// Token: 0x02000405 RID: 1029
	internal class TokenHashValue
	{
		// Token: 0x06003112 RID: 12562 RVA: 0x000B03BA File Offset: 0x000AE5BA
		internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
		{
			this.tokenString = tokenString;
			this.tokenType = tokenType;
			this.tokenValue = tokenValue;
		}

		// Token: 0x040019A7 RID: 6567
		internal string tokenString;

		// Token: 0x040019A8 RID: 6568
		internal TokenType tokenType;

		// Token: 0x040019A9 RID: 6569
		internal int tokenValue;
	}
}
