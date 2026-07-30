using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(64)]
	[VisibleToOtherModules]
	internal class FreeFunctionAttribute : NativeMethodAttribute
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000024E5 File Offset: 0x000006E5
		public FreeFunctionAttribute()
		{
			base.IsFreeFunction = true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000024F7 File Offset: 0x000006F7
		public FreeFunctionAttribute(string name)
			: base(name, true)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002503 File Offset: 0x00000703
		public FreeFunctionAttribute(string name, bool isThreadSafe)
			: base(name, true, isThreadSafe)
		{
		}
	}
}
