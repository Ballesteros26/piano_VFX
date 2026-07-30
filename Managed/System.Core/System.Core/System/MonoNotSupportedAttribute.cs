using System;

namespace System
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000022E4 File Offset: 0x000004E4
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
