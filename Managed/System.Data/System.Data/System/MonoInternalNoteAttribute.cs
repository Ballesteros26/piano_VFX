using System;

namespace System
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00005E33 File Offset: 0x00004033
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
