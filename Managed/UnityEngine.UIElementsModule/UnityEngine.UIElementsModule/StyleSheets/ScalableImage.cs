using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	internal struct ScalableImage
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x0004FF5C File Offset: 0x0004E15C
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "normalImage", this.normalImage, "highResolutionImage", this.highResolutionImage });
		}

		// Token: 0x040008FC RID: 2300
		public Texture2D normalImage;

		// Token: 0x040008FD RID: 2301
		public Texture2D highResolutionImage;
	}
}
