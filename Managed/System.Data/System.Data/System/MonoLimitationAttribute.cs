using System;

namespace System
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00005E33 File Offset: 0x00004033
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
