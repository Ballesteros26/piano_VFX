using System;

namespace System
{
	// Token: 0x020000F5 RID: 245
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
