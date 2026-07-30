using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.All)]
	internal class WebSysDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x000074FC File Offset: 0x000056FC
		internal WebSysDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00007505 File Offset: 0x00005705
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = global::SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000752D File Offset: 0x0000572D
		public override object TypeId
		{
			get
			{
				return typeof(DescriptionAttribute);
			}
		}

		// Token: 0x04000E37 RID: 3639
		private bool replaced;
	}
}
