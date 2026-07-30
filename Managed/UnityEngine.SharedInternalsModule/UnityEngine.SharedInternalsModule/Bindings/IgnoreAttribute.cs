using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000024 RID: 36
	[VisibleToOtherModules]
	[AttributeUsage(256)]
	internal class IgnoreAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000025B7 File Offset: 0x000007B7
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000025BF File Offset: 0x000007BF
		public bool DoesNotContributeToSize { get; set; }
	}
}
