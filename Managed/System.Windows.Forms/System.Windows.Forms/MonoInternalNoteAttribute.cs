using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002124 File Offset: 0x00000324
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
