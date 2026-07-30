using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(64)]
	[VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002510 File Offset: 0x00000710
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
}
