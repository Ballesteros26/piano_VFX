using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000023 RID: 35
	[VisibleToOtherModules]
	[AttributeUsage(192)]
	internal class NativeThrowsAttribute : Attribute, IBindingsThrowsProviderAttribute, IBindingsAttribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002582 File Offset: 0x00000782
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000258A File Offset: 0x0000078A
		public bool ThrowsException { get; set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002593 File Offset: 0x00000793
		public NativeThrowsAttribute()
		{
			this.ThrowsException = true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000025A5 File Offset: 0x000007A5
		public NativeThrowsAttribute(bool throwsException)
		{
			this.ThrowsException = throwsException;
		}
	}
}
