using System;
using System.ComponentModel;

namespace System.Web.Services
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.All)]
	internal class WebServicesDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002AE3 File Offset: 0x00000CE3
		internal WebServicesDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002AEC File Offset: 0x00000CEC
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = Res.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000081 RID: 129
		private bool replaced;
	}
}
