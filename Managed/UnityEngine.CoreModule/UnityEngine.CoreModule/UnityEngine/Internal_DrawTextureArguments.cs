using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000EA RID: 234
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	internal struct Internal_DrawTextureArguments
	{
		// Token: 0x04000290 RID: 656
		public Rect screenRect;

		// Token: 0x04000291 RID: 657
		public Rect sourceRect;

		// Token: 0x04000292 RID: 658
		public int leftBorder;

		// Token: 0x04000293 RID: 659
		public int rightBorder;

		// Token: 0x04000294 RID: 660
		public int topBorder;

		// Token: 0x04000295 RID: 661
		public int bottomBorder;

		// Token: 0x04000296 RID: 662
		public Color leftBorderColor;

		// Token: 0x04000297 RID: 663
		public Color rightBorderColor;

		// Token: 0x04000298 RID: 664
		public Color topBorderColor;

		// Token: 0x04000299 RID: 665
		public Color bottomBorderColor;

		// Token: 0x0400029A RID: 666
		public Color color;

		// Token: 0x0400029B RID: 667
		public Vector4 borderWidths;

		// Token: 0x0400029C RID: 668
		public Vector4 cornerRadiuses;

		// Token: 0x0400029D RID: 669
		public bool smoothCorners;

		// Token: 0x0400029E RID: 670
		public int pass;

		// Token: 0x0400029F RID: 671
		public Texture texture;

		// Token: 0x040002A0 RID: 672
		public Material mat;
	}
}
