using System;

namespace System
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000022E4 File Offset: 0x000004E4
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
