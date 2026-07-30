using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004D RID: 77
	public static class RTHandles
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008B26 File Offset: 0x00006D26
		public static int maxWidth
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxWidth();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008B32 File Offset: 0x00006D32
		public static int maxHeight
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxHeight();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008B3E File Offset: 0x00006D3E
		public static RTHandleProperties rtHandleProperties
		{
			get
			{
				return RTHandles.s_DefaultInstance.rtHandleProperties;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008B4C File Offset: 0x00006D4C
		public static RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008B88 File Offset: 0x00006D88
		public static RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, bool enableMSAA = false, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFactor, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, enableMSAA, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public static RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, bool enableMSAA = false, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFunc, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, enableMSAA, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008BF7 File Offset: 0x00006DF7
		public static RTHandle Alloc(Texture tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008A73 File Offset: 0x00006C73
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008C04 File Offset: 0x00006E04
		public static void Initialize(int width, int height, bool scaledRTsupportsMSAA, MSAASamples scaledRTMSAASamples)
		{
			RTHandles.s_DefaultInstance.Initialize(width, height, scaledRTsupportsMSAA, scaledRTMSAASamples);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008C14 File Offset: 0x00006E14
		public static void Release(RTHandle rth)
		{
			RTHandles.s_DefaultInstance.Release(rth);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008C21 File Offset: 0x00006E21
		public static void SetHardwareDynamicResolutionState(bool hwDynamicResRequested)
		{
			RTHandles.s_DefaultInstance.SetHardwareDynamicResolutionState(hwDynamicResRequested);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008C2E File Offset: 0x00006E2E
		public static void SetReferenceSize(int width, int height, MSAASamples msaaSamples)
		{
			RTHandles.s_DefaultInstance.SetReferenceSize(width, height, msaaSamples);
		}

		// Token: 0x04000153 RID: 339
		private static RTHandleSystem s_DefaultInstance = new RTHandleSystem();
	}
}
