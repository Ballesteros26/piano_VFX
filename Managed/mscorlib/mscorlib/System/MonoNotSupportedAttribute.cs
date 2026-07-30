using System;

namespace System
{
	// Token: 0x020000F7 RID: 247
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
