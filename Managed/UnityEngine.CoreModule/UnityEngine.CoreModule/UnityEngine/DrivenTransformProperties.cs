using System;

namespace UnityEngine
{
	// Token: 0x020001F4 RID: 500
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x040006DE RID: 1758
		None = 0,
		// Token: 0x040006DF RID: 1759
		All = -1,
		// Token: 0x040006E0 RID: 1760
		AnchoredPositionX = 2,
		// Token: 0x040006E1 RID: 1761
		AnchoredPositionY = 4,
		// Token: 0x040006E2 RID: 1762
		AnchoredPositionZ = 8,
		// Token: 0x040006E3 RID: 1763
		Rotation = 16,
		// Token: 0x040006E4 RID: 1764
		ScaleX = 32,
		// Token: 0x040006E5 RID: 1765
		ScaleY = 64,
		// Token: 0x040006E6 RID: 1766
		ScaleZ = 128,
		// Token: 0x040006E7 RID: 1767
		AnchorMinX = 256,
		// Token: 0x040006E8 RID: 1768
		AnchorMinY = 512,
		// Token: 0x040006E9 RID: 1769
		AnchorMaxX = 1024,
		// Token: 0x040006EA RID: 1770
		AnchorMaxY = 2048,
		// Token: 0x040006EB RID: 1771
		SizeDeltaX = 4096,
		// Token: 0x040006EC RID: 1772
		SizeDeltaY = 8192,
		// Token: 0x040006ED RID: 1773
		PivotX = 16384,
		// Token: 0x040006EE RID: 1774
		PivotY = 32768,
		// Token: 0x040006EF RID: 1775
		AnchoredPosition = 6,
		// Token: 0x040006F0 RID: 1776
		AnchoredPosition3D = 14,
		// Token: 0x040006F1 RID: 1777
		Scale = 224,
		// Token: 0x040006F2 RID: 1778
		AnchorMin = 768,
		// Token: 0x040006F3 RID: 1779
		AnchorMax = 3072,
		// Token: 0x040006F4 RID: 1780
		Anchors = 3840,
		// Token: 0x040006F5 RID: 1781
		SizeDelta = 12288,
		// Token: 0x040006F6 RID: 1782
		Pivot = 49152
	}
}
