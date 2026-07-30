using System;

namespace System
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000022E4 File Offset: 0x000004E4
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
