using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200031D RID: 797
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendOp
	{
		// Token: 0x04000887 RID: 2183
		Add,
		// Token: 0x04000888 RID: 2184
		Subtract,
		// Token: 0x04000889 RID: 2185
		ReverseSubtract,
		// Token: 0x0400088A RID: 2186
		Min,
		// Token: 0x0400088B RID: 2187
		Max,
		// Token: 0x0400088C RID: 2188
		LogicalClear,
		// Token: 0x0400088D RID: 2189
		LogicalSet,
		// Token: 0x0400088E RID: 2190
		LogicalCopy,
		// Token: 0x0400088F RID: 2191
		LogicalCopyInverted,
		// Token: 0x04000890 RID: 2192
		LogicalNoop,
		// Token: 0x04000891 RID: 2193
		LogicalInvert,
		// Token: 0x04000892 RID: 2194
		LogicalAnd,
		// Token: 0x04000893 RID: 2195
		LogicalNand,
		// Token: 0x04000894 RID: 2196
		LogicalOr,
		// Token: 0x04000895 RID: 2197
		LogicalNor,
		// Token: 0x04000896 RID: 2198
		LogicalXor,
		// Token: 0x04000897 RID: 2199
		LogicalEquivalence,
		// Token: 0x04000898 RID: 2200
		LogicalAndReverse,
		// Token: 0x04000899 RID: 2201
		LogicalAndInverted,
		// Token: 0x0400089A RID: 2202
		LogicalOrReverse,
		// Token: 0x0400089B RID: 2203
		LogicalOrInverted,
		// Token: 0x0400089C RID: 2204
		Multiply,
		// Token: 0x0400089D RID: 2205
		Screen,
		// Token: 0x0400089E RID: 2206
		Overlay,
		// Token: 0x0400089F RID: 2207
		Darken,
		// Token: 0x040008A0 RID: 2208
		Lighten,
		// Token: 0x040008A1 RID: 2209
		ColorDodge,
		// Token: 0x040008A2 RID: 2210
		ColorBurn,
		// Token: 0x040008A3 RID: 2211
		HardLight,
		// Token: 0x040008A4 RID: 2212
		SoftLight,
		// Token: 0x040008A5 RID: 2213
		Difference,
		// Token: 0x040008A6 RID: 2214
		Exclusion,
		// Token: 0x040008A7 RID: 2215
		HSLHue,
		// Token: 0x040008A8 RID: 2216
		HSLSaturation,
		// Token: 0x040008A9 RID: 2217
		HSLColor,
		// Token: 0x040008AA RID: 2218
		HSLLuminosity
	}
}
