using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000092 RID: 146
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false)]
	internal struct HDDirectionalShadowData
	{
		// Token: 0x04000601 RID: 1537
		[FixedBuffer(typeof(float), 16)]
		[HLSLArray(4, typeof(Vector4))]
		public HDDirectionalShadowData.<sphereCascades>e__FixedBuffer sphereCascades;

		// Token: 0x04000602 RID: 1538
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public Vector4 cascadeDirection;

		// Token: 0x04000603 RID: 1539
		[FixedBuffer(typeof(float), 4)]
		[HLSLArray(4, typeof(float))]
		[SurfaceDataAttributes("", false, false, FieldPrecision.Default, precision = FieldPrecision.Real)]
		public HDDirectionalShadowData.<cascadeBorders>e__FixedBuffer cascadeBorders;

		// Token: 0x0200021C RID: 540
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <sphereCascades>e__FixedBuffer
		{
			// Token: 0x040013E3 RID: 5091
			public float FixedElementField;
		}

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <cascadeBorders>e__FixedBuffer
		{
			// Token: 0x040013E4 RID: 5092
			public float FixedElementField;
		}
	}
}
