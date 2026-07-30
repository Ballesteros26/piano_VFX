using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004C RID: 76
	public class RTHandleSystem : IDisposable
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007F11 File Offset: 0x00006111
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleProperties;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007F19 File Offset: 0x00006119
		public RTHandleSystem()
		{
			this.m_AutoSizedRTs = new HashSet<RTHandle>();
			this.m_ResizeOnDemandRTs = new HashSet<RTHandle>();
			this.m_MaxWidths = 1;
			this.m_MaxHeights = 1;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007F4C File Offset: 0x0000614C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007F55 File Offset: 0x00006155
		public void Initialize(int width, int height, bool scaledRTsupportsMSAA, MSAASamples scaledRTMSAASamples)
		{
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.m_ScaledRTSupportsMSAA = scaledRTsupportsMSAA;
			this.m_ScaledRTCurrentMSAASamples = scaledRTMSAASamples;
			this.m_HardwareDynamicResRequested = DynamicResolutionHandler.instance.RequestsHardwareDynamicResolution();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007F84 File Offset: 0x00006184
		public void Release(RTHandle rth)
		{
			if (rth != null)
			{
				rth.Release();
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007F8F File Offset: 0x0000618F
		internal void Remove(RTHandle rth)
		{
			this.m_AutoSizedRTs.Remove(rth);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007FA0 File Offset: 0x000061A0
		public void SetReferenceSize(int width, int height, MSAASamples msaaSamples)
		{
			this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
			this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
			Vector2 vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			width = Mathf.Max(width, 1);
			height = Mathf.Max(height, 1);
			bool flag = width > this.GetMaxWidth() || height > this.GetMaxHeight();
			bool flag2 = msaaSamples != this.m_ScaledRTCurrentMSAASamples;
			if (flag || flag2)
			{
				this.Resize(width, height, msaaSamples, flag, flag2);
			}
			this.m_RTHandleProperties.currentViewportSize = new Vector2Int(width, height);
			this.m_RTHandleProperties.currentRenderTargetSize = new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight());
			if (this.m_RTHandleProperties.previousViewportSize.x == 0)
			{
				this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
				this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
				vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			}
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled())
			{
				this.m_RTHandleProperties.rtHandleScale = new Vector4(1f, 1f, 1f, 1f);
				return;
			}
			Vector2 vector2 = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			Vector2 vector3 = this.m_RTHandleProperties.currentViewportSize / vector2;
			Vector2 vector4 = this.m_RTHandleProperties.previousViewportSize / vector;
			this.m_RTHandleProperties.rtHandleScale = new Vector4(vector3.x, vector3.y, vector4.x, vector4.y);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008158 File Offset: 0x00006358
		public void SetHardwareDynamicResolutionState(bool enableHWDynamicRes)
		{
			if (enableHWDynamicRes != this.m_HardwareDynamicResRequested && this.m_AutoSizedRTsArray != null)
			{
				this.m_HardwareDynamicResRequested = enableHWDynamicRes;
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rthandle = this.m_AutoSizedRTsArray[i];
					RenderTexture rt = rthandle.m_RT;
					if (rt)
					{
						rt.Release();
						rt.useDynamicScale = this.m_HardwareDynamicResRequested && rthandle.m_EnableHWDynamicScale;
						rt.Create();
					}
					i++;
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000081D0 File Offset: 0x000063D0
		internal void SwitchResizeMode(RTHandle rth, RTHandleSystem.ResizeMode mode)
		{
			if (!rth.useScaling)
			{
				return;
			}
			if (mode != RTHandleSystem.ResizeMode.Auto)
			{
				if (mode == RTHandleSystem.ResizeMode.OnDemand)
				{
					this.m_AutoSizedRTs.Remove(rth);
					this.m_ResizeOnDemandRTs.Add(rth);
					return;
				}
			}
			else
			{
				if (this.m_ResizeOnDemandRTs.Contains(rth))
				{
					this.DemandResize(rth);
				}
				this.m_ResizeOnDemandRTs.Remove(rth);
				this.m_AutoSizedRTs.Add(rth);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008238 File Offset: 0x00006438
		private void DemandResize(RTHandle rth)
		{
			RenderTexture rt = rth.m_RT;
			rth.referenceSize = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Vector2Int vector2Int = rth.GetScaledSize(rth.referenceSize);
			vector2Int = Vector2Int.Max(Vector2Int.one, vector2Int);
			bool flag = rt.width != vector2Int.x || rt.height != vector2Int.y;
			bool flag2 = rth.m_EnableMSAA && rt.antiAliasing != (int)this.m_ScaledRTCurrentMSAASamples;
			if (flag || flag2)
			{
				rt.Release();
				if (rth.m_EnableMSAA)
				{
					rt.antiAliasing = (int)this.m_ScaledRTCurrentMSAASamples;
				}
				rt.width = vector2Int.x;
				rt.height = vector2Int.y;
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.format, rth.m_Name, rt.useMipMap, rth.m_EnableMSAA, this.m_ScaledRTCurrentMSAASamples);
				rt.Create();
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008337 File Offset: 0x00006537
		public int GetMaxWidth()
		{
			return this.m_MaxWidths;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000833F File Offset: 0x0000653F
		public int GetMaxHeight()
		{
			return this.m_MaxHeights;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008348 File Offset: 0x00006548
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rthandle = this.m_AutoSizedRTsArray[i];
					this.Release(rthandle);
					i++;
				}
				this.m_AutoSizedRTs.Clear();
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_ResizeOnDemandRTs.Count);
				this.m_ResizeOnDemandRTs.CopyTo(this.m_AutoSizedRTsArray);
				int j = 0;
				int num2 = this.m_AutoSizedRTsArray.Length;
				while (j < num2)
				{
					RTHandle rthandle2 = this.m_AutoSizedRTsArray[j];
					this.Release(rthandle2);
					j++;
				}
				this.m_ResizeOnDemandRTs.Clear();
				this.m_AutoSizedRTsArray = null;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008414 File Offset: 0x00006614
		private void Resize(int width, int height, MSAASamples msaaSamples, bool sizeChanged, bool msaaSampleChanged)
		{
			this.m_MaxWidths = Math.Max(width, this.m_MaxWidths);
			this.m_MaxHeights = Math.Max(height, this.m_MaxHeights);
			this.m_ScaledRTCurrentMSAASamples = msaaSamples;
			Vector2Int vector2Int = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RTHandle rthandle = this.m_AutoSizedRTsArray[i];
				if (sizeChanged || !msaaSampleChanged || rthandle.m_EnableMSAA)
				{
					rthandle.referenceSize = vector2Int;
					RenderTexture rt = rthandle.m_RT;
					rt.Release();
					Vector2Int scaledSize = rthandle.GetScaledSize(vector2Int);
					rt.width = Mathf.Max(scaledSize.x, 1);
					rt.height = Mathf.Max(scaledSize.y, 1);
					if (rthandle.m_EnableMSAA)
					{
						rt.antiAliasing = (int)this.m_ScaledRTCurrentMSAASamples;
					}
					rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.format, rthandle.m_Name, rt.useMipMap, rthandle.m_EnableMSAA, this.m_ScaledRTCurrentMSAASamples);
					rt.Create();
				}
				i++;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008564 File Offset: 0x00006764
		public RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			bool flag = msaaSamples != MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat renderTextureFormat = (isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, renderTextureFormat, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, renderTextureFormat, name, useMipMap, flag, msaaSamples)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, GraphicsFormatUtility.GetRenderTextureFormat(colorFormat), name, useMipMap, flag, msaaSamples)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.useScaling = false;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.m_EnableMSAA = flag;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			rthandle.referenceSize = new Vector2Int(width, height);
			return rthandle;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008724 File Offset: 0x00006924
		public RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, bool enableMSAA = false, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			int num = Mathf.Max(Mathf.RoundToInt(scaleFactor.x * (float)this.GetMaxWidth()), 1);
			int num2 = Mathf.Max(Mathf.RoundToInt(scaleFactor.y * (float)this.GetMaxHeight()), 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, enableMSAA, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFactor = scaleFactor;
			return rthandle;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000087A4 File Offset: 0x000069A4
		public RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, bool enableMSAA = false, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			Vector2Int vector2Int = scaleFunc(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()));
			int num = Mathf.Max(vector2Int.x, 1);
			int num2 = Mathf.Max(vector2Int.y, 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, enableMSAA, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFunc = scaleFunc;
			return rthandle;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008824 File Offset: 0x00006A24
		private RTHandle AllocAutoSizedRenderTexture(int width, int height, int slices, DepthBits depthBufferBits, GraphicsFormat colorFormat, FilterMode filterMode, TextureWrapMode wrapMode, TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, bool enableMSAA, bool bindTextureMS, bool useDynamicScale, RenderTextureMemoryless memoryless, string name)
		{
			if (!enableMSAA && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			bool flag = this.m_ScaledRTSupportsMSAA && enableMSAA;
			if (!flag)
			{
				bindTextureMS = false;
			}
			bool flag2 = enableRandomWrite;
			if (flag && flag2)
			{
				Debug.LogWarning("RTHandle that is MSAA-enabled cannot allocate MSAA RT with 'enableRandomWrite = true'.");
				flag2 = false;
			}
			int num = (int)(flag ? this.m_ScaledRTCurrentMSAASamples : MSAASamples.None);
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat renderTextureFormat = (isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				GraphicsFormat graphicsFormat = (isShadowMap ? GraphicsFormat.None : GraphicsFormat.R8_UInt);
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, renderTextureFormat, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = flag2,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = num,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					stencilFormat = graphicsFormat,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, GraphicsFormatUtility.GetRenderTextureFormat(colorFormat), name, useMipMap, flag, this.m_ScaledRTCurrentMSAASamples)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = flag2,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = num,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, GraphicsFormatUtility.GetRenderTextureFormat(colorFormat), name, useMipMap, flag, this.m_ScaledRTCurrentMSAASamples)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.m_EnableMSAA = enableMSAA;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.useScaling = true;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			this.m_AutoSizedRTs.Add(rthandle);
			return rthandle;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008A3D File Offset: 0x00006C3D
		public RTHandle Alloc(Texture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = "";
			return rthandle;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008A73 File Offset: 0x00006C73
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008A80 File Offset: 0x00006C80
		internal string DumpRTInfo()
		{
			string text = "";
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RenderTexture rt = this.m_AutoSizedRTsArray[i].rt;
				text = string.Format("{0}\nRT ({1})\t Format: {2} W: {3} H {4}\n", new object[] { text, i, rt.format, rt.width, rt.height });
				i++;
			}
			return text;
		}

		// Token: 0x0400014A RID: 330
		private bool m_HardwareDynamicResRequested;

		// Token: 0x0400014B RID: 331
		private bool m_ScaledRTSupportsMSAA;

		// Token: 0x0400014C RID: 332
		private MSAASamples m_ScaledRTCurrentMSAASamples = MSAASamples.None;

		// Token: 0x0400014D RID: 333
		private HashSet<RTHandle> m_AutoSizedRTs;

		// Token: 0x0400014E RID: 334
		private RTHandle[] m_AutoSizedRTsArray;

		// Token: 0x0400014F RID: 335
		private HashSet<RTHandle> m_ResizeOnDemandRTs;

		// Token: 0x04000150 RID: 336
		private RTHandleProperties m_RTHandleProperties;

		// Token: 0x04000151 RID: 337
		private int m_MaxWidths;

		// Token: 0x04000152 RID: 338
		private int m_MaxHeights;

		// Token: 0x020000D9 RID: 217
		internal enum ResizeMode
		{
			// Token: 0x040002B9 RID: 697
			Auto,
			// Token: 0x040002BA RID: 698
			OnDemand
		}
	}
}
