using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System
{
	// Token: 0x0200023A RID: 570
	[AttributeUsage(32767, AllowMultiple = false)]
	internal sealed class MWFCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06002536 RID: 9526 RVA: 0x0008CB1C File Offset: 0x0008AD1C
		public MWFCategoryAttribute()
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0008CB24 File Offset: 0x0008AD24
		public MWFCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008CB30 File Offset: 0x0008AD30
		protected override string GetLocalizedString(string value)
		{
			return Locale.GetText(value);
		}
	}
}
