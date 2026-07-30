using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005C5 RID: 1477
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		// Token: 0x06003F81 RID: 16257 RVA: 0x000A7DFF File Offset: 0x000A5FFF
		public override bool CanValidate(Type t)
		{
			return t == typeof(TimeSpan);
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000A7E14 File Offset: 0x000A6014
		public override void Validate(object value)
		{
			if (((TimeSpan)value).Ticks <= 0L)
			{
				throw new ConfigurationErrorsException("TimeSpan value must be positive.");
			}
		}
	}
}
