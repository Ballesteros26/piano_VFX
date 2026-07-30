using System;
using System.Reflection;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000072 RID: 114
	internal class SoapReflectedHeader
	{
		// Token: 0x040002AA RID: 682
		internal Type headerType;

		// Token: 0x040002AB RID: 683
		internal MemberInfo memberInfo;

		// Token: 0x040002AC RID: 684
		internal SoapHeaderDirection direction;

		// Token: 0x040002AD RID: 685
		internal bool repeats;

		// Token: 0x040002AE RID: 686
		internal bool custom;
	}
}
