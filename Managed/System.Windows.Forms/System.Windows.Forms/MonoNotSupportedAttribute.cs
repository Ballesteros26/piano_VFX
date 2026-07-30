using System;

namespace System
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
