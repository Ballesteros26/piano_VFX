using System;

namespace System
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C3D File Offset: 0x00000E3D
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
