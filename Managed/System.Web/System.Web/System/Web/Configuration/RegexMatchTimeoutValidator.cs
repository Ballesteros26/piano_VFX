using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000573 RID: 1395
	internal sealed class RegexMatchTimeoutValidator : TimeSpanValidator
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x0009F0F3 File Offset: 0x0009D2F3
		public RegexMatchTimeoutValidator()
			: base(RegexMatchTimeoutValidator._minValue, RegexMatchTimeoutValidator._maxValue)
		{
		}

		// Token: 0x0400204A RID: 8266
		private static readonly TimeSpan _minValue = TimeSpan.Zero;

		// Token: 0x0400204B RID: 8267
		private static readonly TimeSpan _maxValue = TimeSpan.FromMilliseconds(2147483646.0);
	}
}
