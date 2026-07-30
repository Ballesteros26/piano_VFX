using System;

namespace System
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002C3D File Offset: 0x00000E3D
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
