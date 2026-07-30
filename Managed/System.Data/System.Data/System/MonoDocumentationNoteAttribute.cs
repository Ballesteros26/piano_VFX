using System;

namespace System
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00005E33 File Offset: 0x00004033
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
