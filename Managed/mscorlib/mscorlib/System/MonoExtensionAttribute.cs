using System;

namespace System
{
	// Token: 0x020000F4 RID: 244
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
