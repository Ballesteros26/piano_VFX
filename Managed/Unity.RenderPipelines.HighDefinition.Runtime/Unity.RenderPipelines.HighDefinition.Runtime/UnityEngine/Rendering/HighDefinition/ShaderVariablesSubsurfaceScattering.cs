using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C6 RID: 198
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, needAccessors = false, omitStructDeclaration = true)]
	internal struct ShaderVariablesSubsurfaceScattering
	{
		// Token: 0x0400074E RID: 1870
		[FixedBuffer(typeof(float), 64)]
		[HLSLArray(16, typeof(Vector4))]
		public ShaderVariablesSubsurfaceScattering.<_ThicknessRemaps>e__FixedBuffer _ThicknessRemaps;

		// Token: 0x0400074F RID: 1871
		[FixedBuffer(typeof(float), 64)]
		[HLSLArray(16, typeof(Vector4))]
		public ShaderVariablesSubsurfaceScattering.<_ShapeParams>e__FixedBuffer _ShapeParams;

		// Token: 0x04000750 RID: 1872
		[FixedBuffer(typeof(float), 64)]
		[HLSLArray(16, typeof(Vector4))]
		public ShaderVariablesSubsurfaceScattering.<_TransmissionTintsAndFresnel0>e__FixedBuffer _TransmissionTintsAndFresnel0;

		// Token: 0x04000751 RID: 1873
		[FixedBuffer(typeof(float), 64)]
		[HLSLArray(16, typeof(Vector4))]
		public ShaderVariablesSubsurfaceScattering.<_WorldScales>e__FixedBuffer _WorldScales;

		// Token: 0x04000752 RID: 1874
		[FixedBuffer(typeof(uint), 16)]
		[HLSLArray(16, typeof(float))]
		public ShaderVariablesSubsurfaceScattering.<_DiffusionProfileHashTable>e__FixedBuffer _DiffusionProfileHashTable;

		// Token: 0x04000753 RID: 1875
		public uint _EnableSubsurfaceScattering;

		// Token: 0x04000754 RID: 1876
		public float _TexturingModeFlags;

		// Token: 0x04000755 RID: 1877
		public float _TransmissionFlags;

		// Token: 0x04000756 RID: 1878
		public uint _DiffusionProfileCount;

		// Token: 0x0200024C RID: 588
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <_ThicknessRemaps>e__FixedBuffer
		{
			// Token: 0x04001581 RID: 5505
			public float FixedElementField;
		}

		// Token: 0x0200024D RID: 589
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <_ShapeParams>e__FixedBuffer
		{
			// Token: 0x04001582 RID: 5506
			public float FixedElementField;
		}

		// Token: 0x0200024E RID: 590
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <_TransmissionTintsAndFresnel0>e__FixedBuffer
		{
			// Token: 0x04001583 RID: 5507
			public float FixedElementField;
		}

		// Token: 0x0200024F RID: 591
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <_WorldScales>e__FixedBuffer
		{
			// Token: 0x04001584 RID: 5508
			public float FixedElementField;
		}

		// Token: 0x02000250 RID: 592
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <_DiffusionProfileHashTable>e__FixedBuffer
		{
			// Token: 0x04001585 RID: 5509
			public uint FixedElementField;
		}
	}
}
