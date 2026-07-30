using System;

namespace System
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000022E4 File Offset: 0x000004E4
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
