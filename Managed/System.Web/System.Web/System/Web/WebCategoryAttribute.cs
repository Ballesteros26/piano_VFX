using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x020000EA RID: 234
	[AttributeUsage(AttributeTargets.All)]
	internal class WebCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x0002216C File Offset: 0x0002036C
		public WebCategoryAttribute(string category)
			: base(category)
		{
		}
	}
}
