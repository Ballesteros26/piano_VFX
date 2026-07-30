using System;

namespace System
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(32767, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000210C File Offset: 0x0000030C
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
