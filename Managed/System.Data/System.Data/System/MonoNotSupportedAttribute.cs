using System;

namespace System
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005E33 File Offset: 0x00004033
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
