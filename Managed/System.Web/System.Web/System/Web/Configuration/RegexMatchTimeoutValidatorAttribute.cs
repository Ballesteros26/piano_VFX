using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000574 RID: 1396
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class RegexMatchTimeoutValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x0009F124 File Offset: 0x0009D324
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new RegexMatchTimeoutValidator();
			}
		}
	}
}
