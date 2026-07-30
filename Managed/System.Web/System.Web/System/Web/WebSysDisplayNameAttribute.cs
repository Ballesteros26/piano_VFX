using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
	internal sealed class WebSysDisplayNameAttribute : DisplayNameAttribute
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x00007539 File Offset: 0x00005739
		internal WebSysDisplayNameAttribute(string DisplayName)
			: base(DisplayName)
		{
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00007542 File Offset: 0x00005742
		public override string DisplayName
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DisplayNameValue = global::SR.GetString(base.DisplayName);
				}
				return base.DisplayName;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000756A File Offset: 0x0000576A
		public override object TypeId
		{
			get
			{
				return typeof(DisplayNameAttribute);
			}
		}

		// Token: 0x04000E38 RID: 3640
		private bool replaced;
	}
}
