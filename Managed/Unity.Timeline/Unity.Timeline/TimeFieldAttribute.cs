using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000041 RID: 65
	internal class TimeFieldAttribute : PropertyAttribute
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000936D File Offset: 0x0000756D
		public TimeFieldAttribute.UseEditMode useEditMode { get; }

		// Token: 0x060002A1 RID: 673 RVA: 0x00009375 File Offset: 0x00007575
		public TimeFieldAttribute(TimeFieldAttribute.UseEditMode useEditMode = TimeFieldAttribute.UseEditMode.ApplyEditMode)
		{
			this.useEditMode = useEditMode;
		}

		// Token: 0x02000071 RID: 113
		public enum UseEditMode
		{
			// Token: 0x04000168 RID: 360
			None,
			// Token: 0x04000169 RID: 361
			ApplyEditMode
		}
	}
}
