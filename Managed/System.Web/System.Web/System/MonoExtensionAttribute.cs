using System;

namespace System
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002C3D File Offset: 0x00000E3D
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
