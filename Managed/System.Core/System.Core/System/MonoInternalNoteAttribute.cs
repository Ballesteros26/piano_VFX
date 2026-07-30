using System;

namespace System
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000022E4 File Offset: 0x000004E4
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
