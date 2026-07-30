using System;

namespace System
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000028F0 File Offset: 0x00000AF0
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
