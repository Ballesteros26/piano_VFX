using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Class)]
	internal class MenuCategoryAttribute : Attribute
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x00009393 File Offset: 0x00007593
		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}

		// Token: 0x040000F1 RID: 241
		public readonly string category;
	}
}
