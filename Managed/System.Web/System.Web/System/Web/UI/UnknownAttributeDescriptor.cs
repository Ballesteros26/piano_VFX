using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x0200023E RID: 574
	internal class UnknownAttributeDescriptor
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x00040747 File Offset: 0x0003E947
		public UnknownAttributeDescriptor(MemberInfo memberInfo, object value)
		{
			this.Info = memberInfo;
			this.Value = value;
		}

		// Token: 0x040015F2 RID: 5618
		public MemberInfo Info;

		// Token: 0x040015F3 RID: 5619
		public object Value;
	}
}
