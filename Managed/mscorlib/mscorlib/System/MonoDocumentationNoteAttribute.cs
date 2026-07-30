using System;

namespace System
{
	// Token: 0x020000F3 RID: 243
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
