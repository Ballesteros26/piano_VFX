using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005C9 RID: 1481
	internal class ProfilePropertyNameValidator : StringValidator
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x000A8AA9 File Offset: 0x000A6CA9
		public ProfilePropertyNameValidator()
			: base(1)
		{
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000A8AB4 File Offset: 0x000A6CB4
		public override void Validate(object value)
		{
			base.Validate(value);
			string text = value as string;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text2 = text.Trim();
			if (string.IsNullOrEmpty(text2))
			{
				throw new ArgumentException("name cannot be empty.");
			}
			if (text2.Contains("."))
			{
				throw new ArgumentException("name cannot contain period");
			}
		}
	}
}
