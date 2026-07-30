using System;
using System.Reflection;

namespace System.Web.Services
{
	// Token: 0x02000010 RID: 16
	internal class WebMethod
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000026D8 File Offset: 0x000008D8
		internal WebMethod(MethodInfo declaration, WebServiceBindingAttribute binding, WebMethodAttribute attribute)
		{
			this.declaration = declaration;
			this.binding = binding;
			this.attribute = attribute;
		}

		// Token: 0x04000073 RID: 115
		internal MethodInfo declaration;

		// Token: 0x04000074 RID: 116
		internal WebServiceBindingAttribute binding;

		// Token: 0x04000075 RID: 117
		internal WebMethodAttribute attribute;
	}
}
