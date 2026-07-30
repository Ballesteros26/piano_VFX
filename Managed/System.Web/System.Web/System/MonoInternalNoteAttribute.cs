using System;

namespace System
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002C3D File Offset: 0x00000E3D
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
