using System;

namespace System
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00005E33 File Offset: 0x00004033
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
