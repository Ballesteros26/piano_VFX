using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000108 RID: 264
	internal static class HDShaderPassNames
	{
		// Token: 0x040009F7 RID: 2551
		public static readonly string s_EmptyStr = "";

		// Token: 0x040009F8 RID: 2552
		public static readonly string s_ForwardStr = "Forward";

		// Token: 0x040009F9 RID: 2553
		public static readonly string s_DepthOnlyStr = "DepthOnly";

		// Token: 0x040009FA RID: 2554
		public static readonly string s_DepthForwardOnlyStr = "DepthForwardOnly";

		// Token: 0x040009FB RID: 2555
		public static readonly string s_ForwardOnlyStr = "ForwardOnly";

		// Token: 0x040009FC RID: 2556
		public static readonly string s_GBufferStr = "GBuffer";

		// Token: 0x040009FD RID: 2557
		public static readonly string s_GBufferWithPrepassStr = "GBufferWithPrepass";

		// Token: 0x040009FE RID: 2558
		public static readonly string s_SRPDefaultUnlitStr = "SRPDefaultUnlit";

		// Token: 0x040009FF RID: 2559
		public static readonly string s_MotionVectorsStr = "MotionVectors";

		// Token: 0x04000A00 RID: 2560
		public static readonly string s_DistortionVectorsStr = "DistortionVectors";

		// Token: 0x04000A01 RID: 2561
		public static readonly string s_TransparentDepthPrepassStr = "TransparentDepthPrepass";

		// Token: 0x04000A02 RID: 2562
		public static readonly string s_TransparentBackfaceStr = "TransparentBackface";

		// Token: 0x04000A03 RID: 2563
		public static readonly string s_TransparentDepthPostpassStr = "TransparentDepthPostpass";

		// Token: 0x04000A04 RID: 2564
		public static readonly string s_MetaStr = "META";

		// Token: 0x04000A05 RID: 2565
		public static readonly string s_ShadowCasterStr = "ShadowCaster";

		// Token: 0x04000A06 RID: 2566
		public static readonly string s_MeshDecalsMStr = DecalSystem.s_MaterialDecalPassNames[8];

		// Token: 0x04000A07 RID: 2567
		public static readonly string s_MeshDecalsSStr = DecalSystem.s_MaterialDecalPassNames[11];

		// Token: 0x04000A08 RID: 2568
		public static readonly string s_MeshDecalsMSStr = DecalSystem.s_MaterialDecalPassNames[12];

		// Token: 0x04000A09 RID: 2569
		public static readonly string s_MeshDecalsAOStr = DecalSystem.s_MaterialDecalPassNames[9];

		// Token: 0x04000A0A RID: 2570
		public static readonly string s_MeshDecalsMAOStr = DecalSystem.s_MaterialDecalPassNames[10];

		// Token: 0x04000A0B RID: 2571
		public static readonly string s_MeshDecalsAOSStr = DecalSystem.s_MaterialDecalPassNames[13];

		// Token: 0x04000A0C RID: 2572
		public static readonly string s_MeshDecalsMAOSStr = DecalSystem.s_MaterialDecalPassNames[14];

		// Token: 0x04000A0D RID: 2573
		public static readonly string s_MeshDecals3RTStr = DecalSystem.s_MaterialDecalPassNames[0];

		// Token: 0x04000A0E RID: 2574
		public static readonly string s_ShaderGraphMeshDecals4RT = DecalSystem.s_MaterialSGDecalPassNames[4];

		// Token: 0x04000A0F RID: 2575
		public static readonly string s_ShaderGraphMeshDecals3RT = DecalSystem.s_MaterialSGDecalPassNames[3];

		// Token: 0x04000A10 RID: 2576
		public static readonly string s_MeshDecalsForwardEmissive = DecalSystem.s_MaterialDecalPassNames[16];

		// Token: 0x04000A11 RID: 2577
		public static readonly string s_ShaderGraphMeshDecalForwardEmissive = DecalSystem.s_MaterialSGDecalPassNames[5];

		// Token: 0x04000A12 RID: 2578
		public static readonly ShaderTagId s_EmptyName = new ShaderTagId(HDShaderPassNames.s_EmptyStr);

		// Token: 0x04000A13 RID: 2579
		public static readonly ShaderTagId s_ForwardName = new ShaderTagId(HDShaderPassNames.s_ForwardStr);

		// Token: 0x04000A14 RID: 2580
		public static readonly ShaderTagId s_DepthOnlyName = new ShaderTagId(HDShaderPassNames.s_DepthOnlyStr);

		// Token: 0x04000A15 RID: 2581
		public static readonly ShaderTagId s_DepthForwardOnlyName = new ShaderTagId(HDShaderPassNames.s_DepthForwardOnlyStr);

		// Token: 0x04000A16 RID: 2582
		public static readonly ShaderTagId s_ForwardOnlyName = new ShaderTagId(HDShaderPassNames.s_ForwardOnlyStr);

		// Token: 0x04000A17 RID: 2583
		public static readonly ShaderTagId s_GBufferName = new ShaderTagId(HDShaderPassNames.s_GBufferStr);

		// Token: 0x04000A18 RID: 2584
		public static readonly ShaderTagId s_GBufferWithPrepassName = new ShaderTagId(HDShaderPassNames.s_GBufferWithPrepassStr);

		// Token: 0x04000A19 RID: 2585
		public static readonly ShaderTagId s_SRPDefaultUnlitName = new ShaderTagId(HDShaderPassNames.s_SRPDefaultUnlitStr);

		// Token: 0x04000A1A RID: 2586
		public static readonly ShaderTagId s_MotionVectorsName = new ShaderTagId(HDShaderPassNames.s_MotionVectorsStr);

		// Token: 0x04000A1B RID: 2587
		public static readonly ShaderTagId s_DistortionVectorsName = new ShaderTagId(HDShaderPassNames.s_DistortionVectorsStr);

		// Token: 0x04000A1C RID: 2588
		public static readonly ShaderTagId s_TransparentDepthPrepassName = new ShaderTagId(HDShaderPassNames.s_TransparentDepthPrepassStr);

		// Token: 0x04000A1D RID: 2589
		public static readonly ShaderTagId s_TransparentBackfaceName = new ShaderTagId(HDShaderPassNames.s_TransparentBackfaceStr);

		// Token: 0x04000A1E RID: 2590
		public static readonly ShaderTagId s_TransparentDepthPostpassName = new ShaderTagId(HDShaderPassNames.s_TransparentDepthPostpassStr);

		// Token: 0x04000A1F RID: 2591
		public static readonly ShaderTagId s_MeshDecalsMName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsMStr);

		// Token: 0x04000A20 RID: 2592
		public static readonly ShaderTagId s_MeshDecalsSName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsSStr);

		// Token: 0x04000A21 RID: 2593
		public static readonly ShaderTagId s_MeshDecalsMSName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsMSStr);

		// Token: 0x04000A22 RID: 2594
		public static readonly ShaderTagId s_MeshDecalsAOName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsAOStr);

		// Token: 0x04000A23 RID: 2595
		public static readonly ShaderTagId s_MeshDecalsMAOName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsMAOStr);

		// Token: 0x04000A24 RID: 2596
		public static readonly ShaderTagId s_MeshDecalsAOSName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsAOSStr);

		// Token: 0x04000A25 RID: 2597
		public static readonly ShaderTagId s_MeshDecalsMAOSName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsMAOSStr);

		// Token: 0x04000A26 RID: 2598
		public static readonly ShaderTagId s_MeshDecals3RTName = new ShaderTagId(HDShaderPassNames.s_MeshDecals3RTStr);

		// Token: 0x04000A27 RID: 2599
		public static readonly ShaderTagId s_ShaderGraphMeshDecalsName4RT = new ShaderTagId(HDShaderPassNames.s_ShaderGraphMeshDecals4RT);

		// Token: 0x04000A28 RID: 2600
		public static readonly ShaderTagId s_ShaderGraphMeshDecalsName3RT = new ShaderTagId(HDShaderPassNames.s_ShaderGraphMeshDecals3RT);

		// Token: 0x04000A29 RID: 2601
		public static readonly ShaderTagId s_MeshDecalsForwardEmissiveName = new ShaderTagId(HDShaderPassNames.s_MeshDecalsForwardEmissive);

		// Token: 0x04000A2A RID: 2602
		public static readonly ShaderTagId s_ShaderGraphMeshDecalsForwardEmissiveName = new ShaderTagId(HDShaderPassNames.s_ShaderGraphMeshDecalForwardEmissive);

		// Token: 0x04000A2B RID: 2603
		public static readonly ShaderTagId s_AlwaysName = new ShaderTagId("Always");

		// Token: 0x04000A2C RID: 2604
		public static readonly ShaderTagId s_ForwardBaseName = new ShaderTagId("ForwardBase");

		// Token: 0x04000A2D RID: 2605
		public static readonly ShaderTagId s_DeferredName = new ShaderTagId("Deferred");

		// Token: 0x04000A2E RID: 2606
		public static readonly ShaderTagId s_PrepassBaseName = new ShaderTagId("PrepassBase");

		// Token: 0x04000A2F RID: 2607
		public static readonly ShaderTagId s_VertexName = new ShaderTagId("Vertex");

		// Token: 0x04000A30 RID: 2608
		public static readonly ShaderTagId s_VertexLMRGBMName = new ShaderTagId("VertexLMRGBM");

		// Token: 0x04000A31 RID: 2609
		public static readonly ShaderTagId s_VertexLMName = new ShaderTagId("VertexLM");
	}
}
