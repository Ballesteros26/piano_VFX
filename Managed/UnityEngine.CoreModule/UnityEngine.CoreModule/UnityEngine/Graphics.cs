using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000DE RID: 222
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/CopyTexture.h")]
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public class Graphics
	{
		// Token: 0x0600067F RID: 1663
		[FreeFunction("GraphicsScripting::GetMaxDrawMeshInstanceCount")]
		[MethodImpl(4096)]
		private static extern int Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x06000680 RID: 1664
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern ColorGamut GetActiveColorGamut();

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public static ColorGamut activeColorGamut
		{
			get
			{
				return Graphics.GetActiveColorGamut();
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000682 RID: 1666
		// (set) Token: 0x06000683 RID: 1667
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsTier activeTier
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000684 RID: 1668
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetPreserveFramebufferAlpha")]
		[MethodImpl(4096)]
		internal static extern bool GetPreserveFramebufferAlpha();

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public static bool preserveFramebufferAlpha
		{
			get
			{
				return Graphics.GetPreserveFramebufferAlpha();
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000A500 File Offset: 0x00008700
		[FreeFunction("GraphicsScripting::GetActiveColorBuffer")]
		private static RenderBuffer GetActiveColorBuffer()
		{
			RenderBuffer renderBuffer;
			Graphics.GetActiveColorBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000A518 File Offset: 0x00008718
		[FreeFunction("GraphicsScripting::GetActiveDepthBuffer")]
		private static RenderBuffer GetActiveDepthBuffer()
		{
			RenderBuffer renderBuffer;
			Graphics.GetActiveDepthBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x06000688 RID: 1672
		[FreeFunction("GraphicsScripting::SetNullRT")]
		[MethodImpl(4096)]
		private static extern void Internal_SetNullRT();

		// Token: 0x06000689 RID: 1673 RVA: 0x0000A52D File Offset: 0x0000872D
		[NativeMethod(Name = "GraphicsScripting::SetRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetRTSimple(RenderBuffer color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple_Injected(ref color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000A53C File Offset: 0x0000873C
		[NativeMethod(Name = "GraphicsScripting::SetMRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetMRTSimple([NotNull] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetMRTSimple_Injected(color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000A54C File Offset: 0x0000874C
		[NativeMethod(Name = "GraphicsScripting::SetMRTFull", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetMRTFullSetup([NotNull] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice, [NotNull] RenderBufferLoadAction[] colorLA, [NotNull] RenderBufferStoreAction[] colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA)
		{
			Graphics.Internal_SetMRTFullSetup_Injected(color, ref depth, mip, face, depthSlice, colorLA, colorSA, depthLA, depthSA);
		}

		// Token: 0x0600068C RID: 1676
		[NativeMethod(Name = "GraphicsScripting::SetRandomWriteTargetRT", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav);

		// Token: 0x0600068D RID: 1677
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		[MethodImpl(4096)]
		private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav, bool preserveCounterValue);

		// Token: 0x0600068E RID: 1678
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		[MethodImpl(4096)]
		private static extern void Internal_SetRandomWriteTargetGraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue);

		// Token: 0x0600068F RID: 1679
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x06000690 RID: 1680
		[FreeFunction("CopyTexture")]
		[MethodImpl(4096)]
		private static extern void CopyTexture_Full(Texture src, Texture dst);

		// Token: 0x06000691 RID: 1681
		[FreeFunction("CopyTexture")]
		[MethodImpl(4096)]
		private static extern void CopyTexture_Slice_AllMips(Texture src, int srcElement, Texture dst, int dstElement);

		// Token: 0x06000692 RID: 1682
		[FreeFunction("CopyTexture")]
		[MethodImpl(4096)]
		private static extern void CopyTexture_Slice(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip);

		// Token: 0x06000693 RID: 1683
		[FreeFunction("CopyTexture")]
		[MethodImpl(4096)]
		private static extern void CopyTexture_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x06000694 RID: 1684
		[FreeFunction("ConvertTexture")]
		[MethodImpl(4096)]
		private static extern bool ConvertTexture_Full(Texture src, Texture dst);

		// Token: 0x06000695 RID: 1685
		[FreeFunction("ConvertTexture")]
		[MethodImpl(4096)]
		private static extern bool ConvertTexture_Slice(Texture src, int srcElement, Texture dst, int dstElement);

		// Token: 0x06000696 RID: 1686 RVA: 0x0000A56D File Offset: 0x0000876D
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow1(Mesh mesh, int subsetIndex, Vector3 position, Quaternion rotation)
		{
			Graphics.Internal_DrawMeshNow1_Injected(mesh, subsetIndex, ref position, ref rotation);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000A57A File Offset: 0x0000877A
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow2(Mesh mesh, int subsetIndex, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2_Injected(mesh, subsetIndex, ref matrix);
		}

		// Token: 0x06000698 RID: 1688
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[FreeFunction("GraphicsScripting::DrawTexture")]
		[MethodImpl(4096)]
		internal static extern void Internal_DrawTexture(ref Internal_DrawTextureArguments args);

		// Token: 0x06000699 RID: 1689 RVA: 0x0000A588 File Offset: 0x00008788
		[FreeFunction("GraphicsScripting::DrawMesh")]
		private static void Internal_DrawMesh(Mesh mesh, int submeshIndex, Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMesh_Injected(mesh, submeshIndex, ref matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600069A RID: 1690
		[FreeFunction("GraphicsScripting::DrawMeshInstanced")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x0600069B RID: 1691 RVA: 0x0000A5B0 File Offset: 0x000087B0
		[FreeFunction("GraphicsScripting::DrawMeshInstancedProcedural")]
		private static void Internal_DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, Bounds bounds, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedProcedural_Injected(mesh, submeshIndex, material, ref bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000A5D8 File Offset: 0x000087D8
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedIndirect_Injected(mesh, submeshIndex, material, ref bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000A604 File Offset: 0x00008804
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirectGraphicsBuffer(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(mesh, submeshIndex, material, ref bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600069E RID: 1694
		[FreeFunction("GraphicsScripting::DrawProceduralNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x0600069F RID: 1695
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndexedNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount);

		// Token: 0x060006A0 RID: 1696
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060006A1 RID: 1697
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndexedIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060006A2 RID: 1698
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060006A3 RID: 1699
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000A630 File Offset: 0x00008830
		[FreeFunction("GraphicsScripting::DrawProcedural")]
		private static void Internal_DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProcedural_Injected(material, ref bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000A654 File Offset: 0x00008854
		[FreeFunction("GraphicsScripting::DrawProceduralIndexed")]
		private static void Internal_DrawProceduralIndexed(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexed_Injected(material, ref bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000A67C File Offset: 0x0000887C
		[FreeFunction("GraphicsScripting::DrawProceduralIndirect")]
		private static void Internal_DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndirect_Injected(material, ref bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000A6A0 File Offset: 0x000088A0
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirect")]
		private static void Internal_DrawProceduralIndexedIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexedIndirect_Injected(material, ref bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006A8 RID: 1704
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMaterial5(Texture source, RenderTexture dest, [NotNull] Material mat, int pass, bool setRT);

		// Token: 0x060006A9 RID: 1705
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMaterial6(Texture source, RenderTexture dest, [NotNull] Material mat, int pass, bool setRT, int destDepthSlice);

		// Token: 0x060006AA RID: 1706
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMultiTap4(Texture source, RenderTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets);

		// Token: 0x060006AB RID: 1707
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMultiTap5(Texture source, RenderTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets, int destDepthSlice);

		// Token: 0x060006AC RID: 1708
		[FreeFunction("GraphicsScripting::Blit")]
		[MethodImpl(4096)]
		private static extern void Blit2(Texture source, RenderTexture dest);

		// Token: 0x060006AD RID: 1709
		[FreeFunction("GraphicsScripting::Blit")]
		[MethodImpl(4096)]
		private static extern void Blit3(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x060006AE RID: 1710 RVA: 0x0000A6C5 File Offset: 0x000088C5
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit4(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4_Injected(source, dest, ref scale, ref offset);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000A6D2 File Offset: 0x000088D2
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit5(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5_Injected(source, dest, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060006B0 RID: 1712
		[NativeMethod(Name = "GraphicsScripting::CreateGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern IntPtr CreateGPUFenceImpl(GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x060006B1 RID: 1713
		[NativeMethod(Name = "GraphicsScripting::WaitOnGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void WaitOnGPUFenceImpl(IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x060006B2 RID: 1714
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBuffer", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ExecuteCommandBuffer([NotNull] CommandBuffer buffer);

		// Token: 0x060006B3 RID: 1715
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBufferAsync", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ExecuteCommandBufferAsync([NotNull] CommandBuffer buffer, ComputeQueueType queueType);

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000A6E4 File Offset: 0x000088E4
		internal static void CheckLoadActionValid(RenderBufferLoadAction load, string bufferType)
		{
			bool flag = load != RenderBufferLoadAction.Load && load != RenderBufferLoadAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} LoadAction provided.", new object[] { bufferType }));
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000A720 File Offset: 0x00008920
		internal static void CheckStoreActionValid(RenderBufferStoreAction store, string bufferType)
		{
			bool flag = store != RenderBufferStoreAction.Store && store != RenderBufferStoreAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} StoreAction provided.", new object[] { bufferType }));
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000A75C File Offset: 0x0000895C
		internal static void SetRenderTargetImpl(RenderTargetSetup setup)
		{
			bool flag = setup.color.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
			}
			bool flag2 = setup.color.Length != setup.colorLoad.Length;
			if (flag2)
			{
				throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
			}
			bool flag3 = setup.color.Length != setup.colorStore.Length;
			if (flag3)
			{
				throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");
			}
			foreach (RenderBufferLoadAction renderBufferLoadAction in setup.colorLoad)
			{
				Graphics.CheckLoadActionValid(renderBufferLoadAction, "Color");
			}
			foreach (RenderBufferStoreAction renderBufferStoreAction in setup.colorStore)
			{
				Graphics.CheckStoreActionValid(renderBufferStoreAction, "Color");
			}
			Graphics.CheckLoadActionValid(setup.depthLoad, "Depth");
			Graphics.CheckStoreActionValid(setup.depthStore, "Depth");
			bool flag4 = setup.cubemapFace < CubemapFace.Unknown || setup.cubemapFace > CubemapFace.NegativeZ;
			if (flag4)
			{
				throw new ArgumentException("Bad CubemapFace provided");
			}
			Graphics.Internal_SetMRTFullSetup(setup.color, setup.depth, setup.mipLevel, setup.cubemapFace, setup.depthSlice, setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000A8AC File Offset: 0x00008AAC
		internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000A8BC File Offset: 0x00008ABC
		internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
		{
			bool flag = rt;
			if (flag)
			{
				Graphics.SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetMRTSimple(colorBuffers, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000A910 File Offset: 0x00008B10
		public static void SetRenderTarget(RenderTexture rt, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000A91D File Offset: 0x00008B1D
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000A92C File Offset: 0x00008B2C
		public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000A93A File Offset: 0x00008B3A
		public static void SetRenderTarget(RenderTargetSetup setup)
		{
			Graphics.SetRenderTargetImpl(setup);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000A944 File Offset: 0x00008B44
		public static RenderBuffer activeColorBuffer
		{
			get
			{
				return Graphics.GetActiveColorBuffer();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000A95C File Offset: 0x00008B5C
		public static RenderBuffer activeDepthBuffer
		{
			get
			{
				return Graphics.GetActiveDepthBuffer();
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000A974 File Offset: 0x00008B74
		public static void SetRandomWriteTarget(int index, RenderTexture uav)
		{
			bool flag = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetRT(index, uav);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000AA40 File Offset: 0x00008C40
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetGraphicsBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000AABE File Offset: 0x00008CBE
		public static void CopyTexture(Texture src, Texture dst)
		{
			Graphics.CopyTexture_Full(src, dst);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000AAC9 File Offset: 0x00008CC9
		public static void CopyTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips(src, srcElement, dst, dstElement);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000AAD6 File Offset: 0x00008CD6
		public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000AB10 File Offset: 0x00008D10
		public static bool ConvertTexture(Texture src, Texture dst)
		{
			return Graphics.ConvertTexture_Full(src, dst);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public static bool ConvertTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice(src, srcElement, dst, dstElement);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000AB48 File Offset: 0x00008D48
		public static GraphicsFence CreateAsyncGraphicsFence([DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000AB68 File Offset: 0x00008D68
		public static GraphicsFence CreateAsyncGraphicsFence()
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000AB84 File Offset: 0x00008D84
		public static GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStageFlags stage)
		{
			GraphicsFence graphicsFence = default(GraphicsFence);
			graphicsFence.m_FenceType = fenceType;
			graphicsFence.m_Ptr = Graphics.CreateGPUFenceImpl(fenceType, stage);
			graphicsFence.InitPostAllocation();
			graphicsFence.Validate();
			return graphicsFence;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			Graphics.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.PixelProcessing);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Graphics.WaitOnGraphicsFence can only be called with fences created with GraphicsFenceType.AsyncQueueSynchronization.");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				Graphics.WaitOnGPUFenceImpl(fence.m_Ptr, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000AC1C File Offset: 0x00008E1C
		private static void DrawTextureImpl(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat, int pass)
		{
			Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
			internal_DrawTextureArguments.screenRect = screenRect;
			internal_DrawTextureArguments.sourceRect = sourceRect;
			internal_DrawTextureArguments.leftBorder = leftBorder;
			internal_DrawTextureArguments.rightBorder = rightBorder;
			internal_DrawTextureArguments.topBorder = topBorder;
			internal_DrawTextureArguments.bottomBorder = bottomBorder;
			internal_DrawTextureArguments.color = color;
			internal_DrawTextureArguments.leftBorderColor = Color.black;
			internal_DrawTextureArguments.topBorderColor = Color.black;
			internal_DrawTextureArguments.rightBorderColor = Color.black;
			internal_DrawTextureArguments.bottomBorderColor = Color.black;
			internal_DrawTextureArguments.pass = pass;
			internal_DrawTextureArguments.texture = texture;
			internal_DrawTextureArguments.smoothCorners = true;
			internal_DrawTextureArguments.mat = mat;
			Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000ACEC File Offset: 0x00008EEC
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Color32 color = new Color32(128, 128, 128, 128);
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000AD30 File Offset: 0x00008F30
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000AD69 File Offset: 0x00008F69
		public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, 0, 0, 0, 0, mat, pass);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow1(mesh, materialIndex, position, rotation);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000ADAC File Offset: 0x00008FAC
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow2(mesh, materialIndex, matrix);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000ADD9 File Offset: 0x00008FD9
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.DrawMeshNow(mesh, position, rotation, -1);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.DrawMeshNow(mesh, matrix, -1);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000AE34 File Offset: 0x00009034
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000AE70 File Offset: 0x00009070
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [DefaultValue("matrices.Length")] int count, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			bool flag8 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag8)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag9 = count > 0;
			if (flag9)
			{
				Graphics.Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000B028 File Offset: 0x00009228
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = matrices == null;
			if (flag)
			{
				throw new ArgumentNullException("matrices");
			}
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(matrices), matrices.Count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000B06C File Offset: 0x0000926C
		public static void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, Bounds bounds, int count, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes, LightProbeProxyVolume lightProbeProxyVolume = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag7 = count > 0;
			if (flag7)
			{
				Graphics.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000B148 File Offset: 0x00009348
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000B218 File Offset: 0x00009418
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public static void DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount = 1)
		{
			Graphics.Internal_DrawProceduralNow(topology, vertexCount, instanceCount);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public static void DrawProceduralNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexedNow(topology, indexBuffer, indexCount, instanceCount);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000B320 File Offset: 0x00009520
		public static void DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNow(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000B34C File Offset: 0x0000954C
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNow(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000B38C File Offset: 0x0000958C
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNowGraphicsBuffer(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			Graphics.Internal_DrawProcedural(material, bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000B41C File Offset: 0x0000961C
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexed(material, bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000B458 File Offset: 0x00009658
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirect(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000B490 File Offset: 0x00009690
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirect(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000B4DD File Offset: 0x000096DD
		public static void Blit(Texture source, RenderTexture dest)
		{
			Graphics.Blit2(source, dest);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public static void Blit(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit3(source, dest, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000B4F5 File Offset: 0x000096F5
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4(source, dest, scale, offset);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000B502 File Offset: 0x00009702
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000B513 File Offset: 0x00009713
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, dest, mat, pass, true);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000B521 File Offset: 0x00009721
		public static void Blit(Texture source, RenderTexture dest, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, dest, mat, pass, true, destDepthSlice);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000B531 File Offset: 0x00009731
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			Graphics.Blit(source, dest, mat, -1);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000B53E File Offset: 0x0000973E
		public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, null, mat, pass, false);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000B54C File Offset: 0x0000974C
		public static void Blit(Texture source, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, null, mat, pass, false, destDepthSlice);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000B55B File Offset: 0x0000975B
		public static void Blit(Texture source, Material mat)
		{
			Graphics.Blit(source, mat, -1);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000B568 File Offset: 0x00009768
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap4(source, dest, mat, offsets);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000B59C File Offset: 0x0000979C
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap5(source, dest, mat, offsets, destDepthSlice);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000B5D0 File Offset: 0x000097D0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000B5FC File Offset: 0x000097FC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000B62C File Offset: 0x0000982C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000B65C File Offset: 0x0000985C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000B68C File Offset: 0x0000988C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000B6C4 File Offset: 0x000098C4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000B6FC File Offset: 0x000098FC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000B72C File Offset: 0x0000992C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000B760 File Offset: 0x00009960
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000B794 File Offset: 0x00009994
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000B7B4 File Offset: 0x000099B4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000B7D8 File Offset: 0x000099D8
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000B7FC File Offset: 0x000099FC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000B820 File Offset: 0x00009A20
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000B84C File Offset: 0x00009A4C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000B878 File Offset: 0x00009A78
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000B89C File Offset: 0x00009A9C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000B91C File Offset: 0x00009B1C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage)
		{
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, null);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000B944 File Offset: 0x00009B44
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, matrices.Length, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000B968 File Offset: 0x00009B68
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000B98C File Offset: 0x00009B8C
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000BA20 File Offset: 0x00009C20
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000BA48 File Offset: 0x00009C48
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000BA70 File Offset: 0x00009C70
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000BA90 File Offset: 0x00009C90
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000BB1C File Offset: 0x00009D1C
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000BB40 File Offset: 0x00009D40
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000BB68 File Offset: 0x00009D68
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000BB94 File Offset: 0x00009D94
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, -1);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, null, -1);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000BC08 File Offset: 0x00009E08
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000BC2C File Offset: 0x00009E2C
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000BC4C File Offset: 0x00009E4C
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000BC60 File Offset: 0x00009E60
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000BC73 File Offset: 0x00009E73
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, mat, -1);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000BC80 File Offset: 0x00009E80
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Graphics.DrawTexture(screenRect, texture, null, -1);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000BC8D File Offset: 0x00009E8D
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.SetRenderTarget(rt, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000BC9A File Offset: 0x00009E9A
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.SetRenderTarget(rt, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000BCA7 File Offset: 0x00009EA7
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(rt, mipLevel, face, 0);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000BCC2 File Offset: 0x00009EC2
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, face, 0);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000BCDE File Offset: 0x00009EDE
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000BCEA File Offset: 0x00009EEA
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x0600072D RID: 1837
		[MethodImpl(4096)]
		private static extern void GetActiveColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x0600072E RID: 1838
		[MethodImpl(4096)]
		private static extern void GetActiveDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x0600072F RID: 1839
		[MethodImpl(4096)]
		private static extern void Internal_SetRTSimple_Injected(ref RenderBuffer color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000730 RID: 1840
		[MethodImpl(4096)]
		private static extern void Internal_SetMRTSimple_Injected(RenderBuffer[] color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000731 RID: 1841
		[MethodImpl(4096)]
		private static extern void Internal_SetMRTFullSetup_Injected(RenderBuffer[] color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice, RenderBufferLoadAction[] colorLA, RenderBufferStoreAction[] colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA);

		// Token: 0x06000732 RID: 1842
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshNow1_Injected(Mesh mesh, int subsetIndex, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000733 RID: 1843
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshNow2_Injected(Mesh mesh, int subsetIndex, ref Matrix4x4 matrix);

		// Token: 0x06000734 RID: 1844
		[MethodImpl(4096)]
		private static extern void Internal_DrawMesh_Injected(Mesh mesh, int submeshIndex, ref Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x06000735 RID: 1845
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshInstancedProcedural_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x06000736 RID: 1846
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshInstancedIndirect_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x06000737 RID: 1847
		[MethodImpl(4096)]
		private static extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x06000738 RID: 1848
		[MethodImpl(4096)]
		private static extern void Internal_DrawProcedural_Injected(Material material, ref Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000739 RID: 1849
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndexed_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x0600073A RID: 1850
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndirect_Injected(Material material, ref Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x0600073B RID: 1851
		[MethodImpl(4096)]
		private static extern void Internal_DrawProceduralIndexedIndirect_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x0600073C RID: 1852
		[MethodImpl(4096)]
		private static extern void Blit4_Injected(Texture source, RenderTexture dest, ref Vector2 scale, ref Vector2 offset);

		// Token: 0x0600073D RID: 1853
		[MethodImpl(4096)]
		private static extern void Blit5_Injected(Texture source, RenderTexture dest, ref Vector2 scale, ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x0400026E RID: 622
		internal static readonly int kMaxDrawMeshInstanceCount = Graphics.Internal_GetMaxDrawMeshInstanceCount();
	}
}
