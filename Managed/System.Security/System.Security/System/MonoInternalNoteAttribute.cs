using System;

namespace System
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000028F0 File Offset: 0x00000AF0
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
