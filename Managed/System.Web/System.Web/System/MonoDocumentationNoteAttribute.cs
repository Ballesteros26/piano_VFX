using System;

namespace System
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002C3D File Offset: 0x00000E3D
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
