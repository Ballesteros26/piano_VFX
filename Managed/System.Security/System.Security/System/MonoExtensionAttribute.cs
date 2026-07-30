using System;

namespace System
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000028F0 File Offset: 0x00000AF0
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
