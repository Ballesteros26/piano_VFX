using System;

namespace System
{
	// Token: 0x020000F6 RID: 246
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
