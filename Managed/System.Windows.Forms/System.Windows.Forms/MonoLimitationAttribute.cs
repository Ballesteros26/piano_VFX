using System;

namespace System
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
