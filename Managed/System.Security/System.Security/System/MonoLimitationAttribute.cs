using System;

namespace System
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000028F0 File Offset: 0x00000AF0
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
