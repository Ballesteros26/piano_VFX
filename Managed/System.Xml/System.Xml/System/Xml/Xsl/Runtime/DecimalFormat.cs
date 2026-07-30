using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005CC RID: 1484
	internal class DecimalFormat
	{
		// Token: 0x06003ADB RID: 15067 RVA: 0x0014C61C File Offset: 0x0014A81C
		internal DecimalFormat(NumberFormatInfo info, char digit, char zeroDigit, char patternSeparator)
		{
			this.info = info;
			this.digit = digit;
			this.zeroDigit = zeroDigit;
			this.patternSeparator = patternSeparator;
		}

		// Token: 0x0400266A RID: 9834
		public NumberFormatInfo info;

		// Token: 0x0400266B RID: 9835
		public char digit;

		// Token: 0x0400266C RID: 9836
		public char zeroDigit;

		// Token: 0x0400266D RID: 9837
		public char patternSeparator;
	}
}
