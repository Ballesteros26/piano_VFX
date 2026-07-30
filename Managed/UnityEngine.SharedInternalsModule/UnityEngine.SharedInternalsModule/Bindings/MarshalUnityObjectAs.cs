using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(4)]
	[VisibleToOtherModules]
	internal class MarshalUnityObjectAs : Attribute, IBindingsAttribute
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000025C8 File Offset: 0x000007C8
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000025D0 File Offset: 0x000007D0
		public Type MarshalAsType { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x000025D9 File Offset: 0x000007D9
		public MarshalUnityObjectAs(Type marshalAsType)
		{
			this.MarshalAsType = marshalAsType;
		}
	}
}
