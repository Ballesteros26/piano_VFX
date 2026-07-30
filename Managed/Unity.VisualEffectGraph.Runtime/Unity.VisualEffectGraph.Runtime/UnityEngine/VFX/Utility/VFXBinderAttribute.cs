using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Class)]
	public class VFXBinderAttribute : PropertyAttribute
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004803 File Offset: 0x00002A03
		public VFXBinderAttribute(string menuPath)
		{
			this.MenuPath = menuPath;
		}

		// Token: 0x0400008E RID: 142
		public string MenuPath;
	}
}
