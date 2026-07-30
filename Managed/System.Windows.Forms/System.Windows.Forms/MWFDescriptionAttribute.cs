using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System
{
	// Token: 0x0200023B RID: 571
	[AttributeUsage(32767, AllowMultiple = false)]
	internal sealed class MWFDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06002539 RID: 9529 RVA: 0x0008CB38 File Offset: 0x0008AD38
		public MWFDescriptionAttribute()
		{
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0008CB40 File Offset: 0x0008AD40
		public MWFDescriptionAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x0008CB4C File Offset: 0x0008AD4C
		public override string Description
		{
			get
			{
				return Locale.GetText(base.Description);
			}
		}
	}
}
