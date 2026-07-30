using System;

namespace System
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000028F0 File Offset: 0x00000AF0
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
