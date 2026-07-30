using System;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002118 File Offset: 0x00000318
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
