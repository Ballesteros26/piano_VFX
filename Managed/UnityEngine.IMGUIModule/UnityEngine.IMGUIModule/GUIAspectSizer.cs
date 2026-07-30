using System;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0000C974 File Offset: 0x0000AB74
		public GUIAspectSizer(float aspect, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x040000F3 RID: 243
		private float aspect;
	}
}
