using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000061 RID: 97
	public static class RuntimeUtilities
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
		public static Texture2D whiteTexture
		{
			get
			{
				if (RuntimeUtilities.m_WhiteTexture == null)
				{
					RuntimeUtilities.m_WhiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "White Texture"
					};
					RuntimeUtilities.m_WhiteTexture.SetPixel(0, 0, Color.white);
					RuntimeUtilities.m_WhiteTexture.Apply();
				}
				return RuntimeUtilities.m_WhiteTexture;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public static Texture3D whiteTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_WhiteTexture3D == null)
				{
					RuntimeUtilities.m_WhiteTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "White Texture 3D"
					};
					RuntimeUtilities.m_WhiteTexture3D.SetPixels(new Color[] { Color.white });
					RuntimeUtilities.m_WhiteTexture3D.Apply();
				}
				return RuntimeUtilities.m_WhiteTexture3D;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000E654 File Offset: 0x0000C854
		public static Texture2D blackTexture
		{
			get
			{
				if (RuntimeUtilities.m_BlackTexture == null)
				{
					RuntimeUtilities.m_BlackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "Black Texture"
					};
					RuntimeUtilities.m_BlackTexture.SetPixel(0, 0, Color.black);
					RuntimeUtilities.m_BlackTexture.Apply();
				}
				return RuntimeUtilities.m_BlackTexture;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public static Texture3D blackTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_BlackTexture3D == null)
				{
					RuntimeUtilities.m_BlackTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "Black Texture 3D"
					};
					RuntimeUtilities.m_BlackTexture3D.SetPixels(new Color[] { Color.black });
					RuntimeUtilities.m_BlackTexture3D.Apply();
				}
				return RuntimeUtilities.m_BlackTexture3D;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000E708 File Offset: 0x0000C908
		public static Texture2D transparentTexture
		{
			get
			{
				if (RuntimeUtilities.m_TransparentTexture == null)
				{
					RuntimeUtilities.m_TransparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "Transparent Texture"
					};
					RuntimeUtilities.m_TransparentTexture.SetPixel(0, 0, Color.clear);
					RuntimeUtilities.m_TransparentTexture.Apply();
				}
				return RuntimeUtilities.m_TransparentTexture;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000E75C File Offset: 0x0000C95C
		public static Texture3D transparentTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_TransparentTexture3D == null)
				{
					RuntimeUtilities.m_TransparentTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "Transparent Texture 3D"
					};
					RuntimeUtilities.m_TransparentTexture3D.SetPixels(new Color[] { Color.clear });
					RuntimeUtilities.m_TransparentTexture3D.Apply();
				}
				return RuntimeUtilities.m_TransparentTexture3D;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public static Texture2D GetLutStrip(int size)
		{
			Texture2D texture2D;
			if (!RuntimeUtilities.m_LutStrips.TryGetValue(size, out texture2D))
			{
				int num = size * size;
				Color[] array = new Color[num * size];
				float num2 = 1f / ((float)size - 1f);
				for (int i = 0; i < size; i++)
				{
					int num3 = i * size;
					float num4 = (float)i * num2;
					for (int j = 0; j < size; j++)
					{
						float num5 = (float)j * num2;
						for (int k = 0; k < size; k++)
						{
							float num6 = (float)k * num2;
							array[j * num + num3 + k] = new Color(num6, num5, num4);
						}
					}
				}
				TextureFormat textureFormat = TextureFormat.RGBAHalf;
				if (!textureFormat.IsSupported())
				{
					textureFormat = TextureFormat.ARGB32;
				}
				texture2D = new Texture2D(size * size, size, textureFormat, false, true)
				{
					name = "Strip Lut" + size,
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0
				};
				texture2D.SetPixels(array);
				texture2D.Apply();
				RuntimeUtilities.m_LutStrips.Add(size, texture2D);
			}
			return texture2D;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		public static Mesh fullscreenTriangle
		{
			get
			{
				if (RuntimeUtilities.s_FullscreenTriangle != null)
				{
					return RuntimeUtilities.s_FullscreenTriangle;
				}
				RuntimeUtilities.s_FullscreenTriangle = new Mesh
				{
					name = "Fullscreen Triangle"
				};
				RuntimeUtilities.s_FullscreenTriangle.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 3f, 0f),
					new Vector3(3f, -1f, 0f)
				});
				RuntimeUtilities.s_FullscreenTriangle.SetIndices(new int[] { 0, 1, 2 }, MeshTopology.Triangles, 0, false);
				RuntimeUtilities.s_FullscreenTriangle.UploadMeshData(false);
				return RuntimeUtilities.s_FullscreenTriangle;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000E990 File Offset: 0x0000CB90
		public static Material copyStdMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyStdMaterial != null)
				{
					return RuntimeUtilities.s_CopyStdMaterial;
				}
				RuntimeUtilities.s_CopyStdMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStd)
				{
					name = "PostProcess - CopyStd",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyStdMaterial;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public static Material copyStdFromDoubleWideMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyStdFromDoubleWideMaterial != null)
				{
					return RuntimeUtilities.s_CopyStdFromDoubleWideMaterial;
				}
				RuntimeUtilities.s_CopyStdFromDoubleWideMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStdFromDoubleWide)
				{
					name = "PostProcess - CopyStdFromDoubleWide",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyStdFromDoubleWideMaterial;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000EA38 File Offset: 0x0000CC38
		public static Material copyMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyMaterial != null)
				{
					return RuntimeUtilities.s_CopyMaterial;
				}
				RuntimeUtilities.s_CopyMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copy)
				{
					name = "PostProcess - Copy",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyMaterial;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		public static Material copyFromTexArrayMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyFromTexArrayMaterial != null)
				{
					return RuntimeUtilities.s_CopyFromTexArrayMaterial;
				}
				RuntimeUtilities.s_CopyFromTexArrayMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStdFromTexArray)
				{
					name = "PostProcess - CopyFromTexArray",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyFromTexArrayMaterial;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000EADD File Offset: 0x0000CCDD
		public static PropertySheet copySheet
		{
			get
			{
				if (RuntimeUtilities.s_CopySheet == null)
				{
					RuntimeUtilities.s_CopySheet = new PropertySheet(RuntimeUtilities.copyMaterial);
				}
				return RuntimeUtilities.s_CopySheet;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000EAFA File Offset: 0x0000CCFA
		public static PropertySheet copyFromTexArraySheet
		{
			get
			{
				if (RuntimeUtilities.s_CopyFromTexArraySheet == null)
				{
					RuntimeUtilities.s_CopyFromTexArraySheet = new PropertySheet(RuntimeUtilities.copyFromTexArrayMaterial);
				}
				return RuntimeUtilities.s_CopyFromTexArraySheet;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000EB18 File Offset: 0x0000CD18
		internal static void UpdateResources(PostProcessResources resources)
		{
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyStdMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyFromTexArrayMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyStdFromDoubleWideMaterial);
			RuntimeUtilities.s_CopyMaterial = null;
			RuntimeUtilities.s_CopyStdMaterial = null;
			RuntimeUtilities.s_CopyFromTexArrayMaterial = null;
			RuntimeUtilities.s_CopyStdFromDoubleWideMaterial = null;
			RuntimeUtilities.s_CopySheet = null;
			RuntimeUtilities.s_CopyFromTexArraySheet = null;
			RuntimeUtilities.s_Resources = resources;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public static void SetRenderTargetWithLoadStoreAction(this CommandBuffer cmd, RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			cmd.SetRenderTarget(rt, loadAction, storeAction);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000EB82 File Offset: 0x0000CD82
		public static void SetRenderTargetWithLoadStoreAction(this CommandBuffer cmd, RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			cmd.SetRenderTarget(color, colorLoadAction, colorStoreAction, depth, depthLoadAction, depthStoreAction);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000EB94 File Offset: 0x0000CD94
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, bool clear = false, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetRenderTargetWithLoadStoreAction(destination, (viewport == null) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, RuntimeUtilities.copyMaterial, 0, 0);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, RenderBufferLoadAction loadAction, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			bool flag = loadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				loadAction = RenderBufferLoadAction.DontCare;
			}
			cmd.SetRenderTargetWithLoadStoreAction(destination, (viewport == null) ? loadAction : RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (flag)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null)
		{
			cmd.BlitFullscreenTriangle(source, destination, propertySheet, pass, clear ? RenderBufferLoadAction.Clear : RenderBufferLoadAction.DontCare, viewport);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000EC98 File Offset: 0x0000CE98
		public static void BlitFullscreenTriangleFromDoubleWide(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass, int eye)
		{
			Vector4 vector = new Vector4(0.5f, 1f, 0f, 0f);
			if (eye == 1)
			{
				vector.z = 0.5f;
			}
			cmd.SetGlobalVector(ShaderIDs.UVScaleOffset, vector);
			cmd.BuiltinBlit(source, destination, material, pass);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public static void BlitFullscreenTriangleToDoubleWide(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, int eye)
		{
			Vector4 vector = new Vector4(0.5f, 1f, -0.5f, 0f);
			if (eye == 1)
			{
				vector.z = 0.5f;
			}
			propertySheet.EnableKeyword("STEREO_DOUBLEWIDE_TARGET");
			propertySheet.properties.SetVector(ShaderIDs.PosScaleOffset, vector);
			cmd.BlitFullscreenTriangle(source, destination, propertySheet, 0, false, null);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public static void BlitFullscreenTriangleFromTexArray(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, int depthSlice = -1)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetGlobalFloat(ShaderIDs.DepthSlice, (float)depthSlice);
			cmd.SetRenderTargetWithLoadStoreAction(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public static void BlitFullscreenTriangleToTexArray(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, int depthSlice = -1)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetGlobalFloat(ShaderIDs.DepthSlice, (float)depthSlice);
			cmd.SetRenderTarget(destination, 0, CubemapFace.Unknown, -1);
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000EE18 File Offset: 0x0000D018
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, RenderTargetIdentifier depth, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			RenderBufferLoadAction renderBufferLoadAction = ((viewport == null) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			if (clear)
			{
				cmd.SetRenderTargetWithLoadStoreAction(destination, renderBufferLoadAction, RenderBufferStoreAction.Store, depth, renderBufferLoadAction, RenderBufferStoreAction.Store);
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			else
			{
				cmd.SetRenderTargetWithLoadStoreAction(destination, renderBufferLoadAction, RenderBufferStoreAction.Store, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
			}
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier[] destinations, RenderTargetIdentifier depth, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetRenderTarget(destinations, depth);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000EF0D File Offset: 0x0000D10D
		public static void BuiltinBlit(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			destination = BuiltinRenderTextureType.CurrentActive;
			cmd.Blit(source, destination);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000EF28 File Offset: 0x0000D128
		public static void BuiltinBlit(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material mat, int pass = 0)
		{
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			destination = BuiltinRenderTextureType.CurrentActive;
			cmd.Blit(source, destination, mat, pass);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000EF48 File Offset: 0x0000D148
		public static void CopyTexture(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			if (SystemInfo.copyTextureSupport > CopyTextureSupport.None)
			{
				cmd.CopyTexture(source, destination);
				return;
			}
			cmd.BlitFullscreenTriangle(source, destination, false, null);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000EF78 File Offset: 0x0000D178
		public static bool scriptableRenderPipelineActive
		{
			get
			{
				return GraphicsSettings.renderPipelineAsset != null;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000EF85 File Offset: 0x0000D185
		public static bool supportsDeferredShading
		{
			get
			{
				return RuntimeUtilities.scriptableRenderPipelineActive || GraphicsSettings.GetShaderMode(BuiltinShaderType.DeferredShading) > BuiltinShaderMode.Disabled;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000EF99 File Offset: 0x0000D199
		public static bool supportsDepthNormals
		{
			get
			{
				return RuntimeUtilities.scriptableRenderPipelineActive || GraphicsSettings.GetShaderMode(BuiltinShaderType.DepthNormals) > BuiltinShaderMode.Disabled;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public static bool isSinglePassStereoEnabled
		{
			get
			{
				return XRSettings.eyeTextureDesc.vrUsage == VRTextureUsage.TwoEyes;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000EFCD File Offset: 0x0000D1CD
		public static bool isVREnabled
		{
			get
			{
				return XRSettings.enabled;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public static bool isAndroidOpenGL
		{
			get
			{
				return Application.platform == RuntimePlatform.Android && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000EFED File Offset: 0x0000D1ED
		public static RenderTextureFormat defaultHDRRenderTextureFormat
		{
			get
			{
				return RenderTextureFormat.DefaultHDR;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000EFF1 File Offset: 0x0000D1F1
		public static bool isFloatingPointFormat(RenderTextureFormat format)
		{
			return format == RenderTextureFormat.DefaultHDR || format == RenderTextureFormat.ARGBHalf || format == RenderTextureFormat.ARGBFloat || format == RenderTextureFormat.RGFloat || format == RenderTextureFormat.RGHalf || format == RenderTextureFormat.RFloat || format == RenderTextureFormat.RHalf || format == RenderTextureFormat.RGB111110Float;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000F01C File Offset: 0x0000D21C
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000F02D File Offset: 0x0000D22D
		public static bool isLinearColorSpace
		{
			get
			{
				return QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000F038 File Offset: 0x0000D238
		public static bool IsResolvedDepthAvailable(Camera camera)
		{
			GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
			return camera.actualRenderingPath == RenderingPath.DeferredShading && (graphicsDeviceType == GraphicsDeviceType.Direct3D11 || graphicsDeviceType == GraphicsDeviceType.Direct3D12 || graphicsDeviceType == GraphicsDeviceType.XboxOne);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000F068 File Offset: 0x0000D268
		public static void DestroyProfile(PostProcessProfile profile, bool destroyEffects)
		{
			if (destroyEffects)
			{
				foreach (PostProcessEffectSettings postProcessEffectSettings in profile.settings)
				{
					RuntimeUtilities.Destroy(postProcessEffectSettings);
				}
			}
			RuntimeUtilities.Destroy(profile);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public static void DestroyVolume(PostProcessVolume volume, bool destroyProfile, bool destroyGameObject = false)
		{
			if (destroyProfile)
			{
				RuntimeUtilities.DestroyProfile(volume.profileRef, true);
			}
			GameObject gameObject = volume.gameObject;
			RuntimeUtilities.Destroy(volume);
			if (destroyGameObject)
			{
				RuntimeUtilities.Destroy(gameObject);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000F0F6 File Offset: 0x0000D2F6
		public static bool IsPostProcessingActive(PostProcessLayer layer)
		{
			return layer != null && layer.enabled;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000F109 File Offset: 0x0000D309
		public static bool IsTemporalAntialiasingActive(PostProcessLayer layer)
		{
			return RuntimeUtilities.IsPostProcessingActive(layer) && layer.antialiasingMode == PostProcessLayer.Antialiasing.TemporalAntialiasing && layer.temporalAntialiasing.IsSupported();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000F129 File Offset: 0x0000D329
		public static IEnumerable<T> GetAllSceneObjects<T>() where T : Component
		{
			Queue<Transform> queue = new Queue<Transform>();
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			foreach (GameObject gameObject in rootGameObjects)
			{
				queue.Enqueue(gameObject.transform);
				T component = gameObject.GetComponent<T>();
				if (component != null)
				{
					yield return component;
				}
			}
			GameObject[] array = null;
			while (queue.Count > 0)
			{
				foreach (object obj in queue.Dequeue())
				{
					Transform transform = (Transform)obj;
					queue.Enqueue(transform);
					T component2 = transform.GetComponent<T>();
					if (component2 != null)
					{
						yield return component2;
					}
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000F132 File Offset: 0x0000D332
		public static void CreateIfNull<T>(ref T obj) where T : class, new()
		{
			if (obj == null)
			{
				obj = new T();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000F14C File Offset: 0x0000D34C
		public static float Exp2(float x)
		{
			return Mathf.Exp(x * 0.6931472f);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000F15C File Offset: 0x0000D35C
		public static Matrix4x4 GetJitteredPerspectiveProjectionMatrix(Camera camera, Vector2 offset)
		{
			float nearClipPlane = camera.nearClipPlane;
			float farClipPlane = camera.farClipPlane;
			float num = Mathf.Tan(0.008726646f * camera.fieldOfView) * nearClipPlane;
			float num2 = num * camera.aspect;
			offset.x *= num2 / (0.5f * (float)camera.pixelWidth);
			offset.y *= num / (0.5f * (float)camera.pixelHeight);
			Matrix4x4 projectionMatrix = camera.projectionMatrix;
			ref Matrix4x4 ptr = ref projectionMatrix;
			ptr[0, 2] = ptr[0, 2] + offset.x / num2;
			ptr = ref projectionMatrix;
			ptr[1, 2] = ptr[1, 2] + offset.y / num;
			return projectionMatrix;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000F210 File Offset: 0x0000D410
		public static Matrix4x4 GetJitteredOrthographicProjectionMatrix(Camera camera, Vector2 offset)
		{
			float orthographicSize = camera.orthographicSize;
			float num = orthographicSize * camera.aspect;
			offset.x *= num / (0.5f * (float)camera.pixelWidth);
			offset.y *= orthographicSize / (0.5f * (float)camera.pixelHeight);
			float num2 = offset.x - num;
			float num3 = offset.x + num;
			float num4 = offset.y + orthographicSize;
			float num5 = offset.y - orthographicSize;
			return Matrix4x4.Ortho(num2, num3, num5, num4, camera.nearClipPlane, camera.farClipPlane);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000F29C File Offset: 0x0000D49C
		public static Matrix4x4 GenerateJitteredProjectionMatrixFromOriginal(PostProcessRenderContext context, Matrix4x4 origProj, Vector2 jitter)
		{
			FrustumPlanes decomposeProjection = origProj.decomposeProjection;
			float num = Math.Abs(decomposeProjection.top) + Math.Abs(decomposeProjection.bottom);
			float num2 = Math.Abs(decomposeProjection.left) + Math.Abs(decomposeProjection.right);
			Vector2 vector = new Vector2(jitter.x * num2 / (float)context.screenWidth, jitter.y * num / (float)context.screenHeight);
			decomposeProjection.left += vector.x;
			decomposeProjection.right += vector.x;
			decomposeProjection.top += vector.y;
			decomposeProjection.bottom += vector.y;
			return Matrix4x4.Frustum(decomposeProjection);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000F354 File Offset: 0x0000D554
		public static IEnumerable<Type> GetAllAssemblyTypes()
		{
			if (RuntimeUtilities.m_AssemblyTypes == null)
			{
				RuntimeUtilities.m_AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly t)
				{
					Type[] array = new Type[0];
					try
					{
						array = t.GetTypes();
					}
					catch
					{
					}
					return array;
				});
			}
			return RuntimeUtilities.m_AssemblyTypes;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public static IEnumerable<Type> GetAllTypesDerivedFrom<T>()
		{
			return from t in RuntimeUtilities.GetAllAssemblyTypes()
				where t.IsSubclassOf(typeof(T))
				select t;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000F3CB File Offset: 0x0000D5CB
		public static T GetAttribute<T>(this Type type) where T : Attribute
		{
			return (T)((object)type.GetCustomAttributes(typeof(T), false)[0]);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public static Attribute[] GetMemberAttributes<TType, TValue>(Expression<Func<TType, TValue>> expr)
		{
			Expression expression = expr;
			if (expression is LambdaExpression)
			{
				expression = ((LambdaExpression)expression).Body;
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return ((FieldInfo)((MemberExpression)expression).Member).GetCustomAttributes(false).Cast<Attribute>().ToArray<Attribute>();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000F440 File Offset: 0x0000D640
		public static string GetFieldPath<TType, TValue>(Expression<Func<TType, TValue>> expr)
		{
			ExpressionType nodeType = expr.Body.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = expr.Body as MemberExpression;
				List<string> list = new List<string>();
				while (memberExpression != null)
				{
					list.Add(memberExpression.Member.Name);
					memberExpression = memberExpression.Expression as MemberExpression;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
					if (i > 0)
					{
						stringBuilder.Append('.');
					}
				}
				return stringBuilder.ToString();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x040001A1 RID: 417
		private static Texture2D m_WhiteTexture;

		// Token: 0x040001A2 RID: 418
		private static Texture3D m_WhiteTexture3D;

		// Token: 0x040001A3 RID: 419
		private static Texture2D m_BlackTexture;

		// Token: 0x040001A4 RID: 420
		private static Texture3D m_BlackTexture3D;

		// Token: 0x040001A5 RID: 421
		private static Texture2D m_TransparentTexture;

		// Token: 0x040001A6 RID: 422
		private static Texture3D m_TransparentTexture3D;

		// Token: 0x040001A7 RID: 423
		private static Dictionary<int, Texture2D> m_LutStrips = new Dictionary<int, Texture2D>();

		// Token: 0x040001A8 RID: 424
		private static PostProcessResources s_Resources;

		// Token: 0x040001A9 RID: 425
		private static Mesh s_FullscreenTriangle;

		// Token: 0x040001AA RID: 426
		private static Material s_CopyStdMaterial;

		// Token: 0x040001AB RID: 427
		private static Material s_CopyStdFromDoubleWideMaterial;

		// Token: 0x040001AC RID: 428
		private static Material s_CopyMaterial;

		// Token: 0x040001AD RID: 429
		private static Material s_CopyFromTexArrayMaterial;

		// Token: 0x040001AE RID: 430
		private static PropertySheet s_CopySheet;

		// Token: 0x040001AF RID: 431
		private static PropertySheet s_CopyFromTexArraySheet;

		// Token: 0x040001B0 RID: 432
		private static IEnumerable<Type> m_AssemblyTypes;
	}
}
