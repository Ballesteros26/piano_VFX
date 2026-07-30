using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000059 RID: 89
	public static class CoreUtils
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A940 File Offset: 0x00008B40
		public static Cubemap blackCubeTexture
		{
			get
			{
				if (CoreUtils.m_BlackCubeTexture == null)
				{
					CoreUtils.m_BlackCubeTexture = new Cubemap(1, TextureFormat.ARGB32, false);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_BlackCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.black);
					}
					CoreUtils.m_BlackCubeTexture.Apply();
				}
				return CoreUtils.m_BlackCubeTexture;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A994 File Offset: 0x00008B94
		public static Cubemap magentaCubeTexture
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTexture == null)
				{
					CoreUtils.m_MagentaCubeTexture = new Cubemap(1, TextureFormat.ARGB32, false);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_MagentaCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.magenta);
					}
					CoreUtils.m_MagentaCubeTexture.Apply();
				}
				return CoreUtils.m_MagentaCubeTexture;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		public static CubemapArray magentaCubeTextureArray
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTextureArray == null)
				{
					CoreUtils.m_MagentaCubeTextureArray = new CubemapArray(1, 1, TextureFormat.RGBAFloat, false);
					for (int i = 0; i < 6; i++)
					{
						Color[] array = new Color[] { Color.magenta };
						CoreUtils.m_MagentaCubeTextureArray.SetPixels(array, (CubemapFace)i, 0);
					}
					CoreUtils.m_MagentaCubeTextureArray.Apply();
				}
				return CoreUtils.m_MagentaCubeTextureArray;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public static Cubemap whiteCubeTexture
		{
			get
			{
				if (CoreUtils.m_WhiteCubeTexture == null)
				{
					CoreUtils.m_WhiteCubeTexture = new Cubemap(1, TextureFormat.ARGB32, false);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_WhiteCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.white);
					}
					CoreUtils.m_WhiteCubeTexture.Apply();
				}
				return CoreUtils.m_WhiteCubeTexture;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public static RenderTexture emptyUAV
		{
			get
			{
				if (CoreUtils.m_EmptyUAV == null)
				{
					CoreUtils.m_EmptyUAV = new RenderTexture(1, 1, 0);
					CoreUtils.m_EmptyUAV.enableRandomWrite = true;
					CoreUtils.m_EmptyUAV.Create();
				}
				return CoreUtils.m_EmptyUAV;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public static Texture3D blackVolumeTexture
		{
			get
			{
				if (CoreUtils.m_BlackVolumeTexture == null)
				{
					Color[] array = new Color[] { Color.black };
					CoreUtils.m_BlackVolumeTexture = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false);
					CoreUtils.m_BlackVolumeTexture.SetPixels(array, 0);
					CoreUtils.m_BlackVolumeTexture.Apply();
				}
				return CoreUtils.m_BlackVolumeTexture;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000AB2F File Offset: 0x00008D2F
		public static void ClearRenderTarget(CommandBuffer cmd, ClearFlag clearFlag, Color clearColor)
		{
			if (clearFlag != ClearFlag.None)
			{
				cmd.ClearRenderTarget((clearFlag & ClearFlag.Depth) > ClearFlag.None, (clearFlag & ClearFlag.Color) > ClearFlag.None, clearColor);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000AB47 File Offset: 0x00008D47
		private static int FixupDepthSlice(int depthSlice, RTHandle buffer)
		{
			if (depthSlice == -1 && buffer.rt.dimension == TextureDimension.Cube)
			{
				depthSlice = 0;
			}
			return depthSlice;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AB5F File Offset: 0x00008D5F
		private static int FixupDepthSlice(int depthSlice, CubemapFace cubemapFace)
		{
			if (depthSlice == -1 && cubemapFace != CubemapFace.Unknown)
			{
				depthSlice = 0;
			}
			return depthSlice;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AB6D File Offset: 0x00008D6D
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AB8F File Offset: 0x00008D8F
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000ABA3 File Offset: 0x00008DA3
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000ABCE File Offset: 0x00008DCE
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(colorBuffer, depthBuffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, ClearFlag.None, Color.clear);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AC02 File Offset: 0x00008E02
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AC12 File Offset: 0x00008E12
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, -1);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AC28 File Offset: 0x00008E28
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AC3D File Offset: 0x00008E3D
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, Color.clear);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AC4F File Offset: 0x00008E4F
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, Color.clear);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AC8F File Offset: 0x00008E8F
		private static void SetViewportAndClear(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor)
		{
			CoreUtils.SetViewport(cmd, buffer);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, buffer);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000ACC7 File Offset: 0x00008EC7
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AD80 File Offset: 0x00008F80
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer.rt, depthBuffer.rt, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.rt, ClearFlag.None, Color.clear);
			CoreUtils.SetViewport(cmd, depthBuffer);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AE0A File Offset: 0x0000900A
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.rt);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AE2B File Offset: 0x0000902B
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, clearColor);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AE48 File Offset: 0x00009048
		public static void SetViewport(CommandBuffer cmd, RTHandle target)
		{
			if (target.useScaling)
			{
				Vector2Int scaledSize = target.GetScaledSize(target.rtHandleProperties.currentViewportSize);
				cmd.SetViewport(new Rect(0f, 0f, (float)scaledSize.x, (float)scaledSize.y));
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AE94 File Offset: 0x00009094
		public static string GetRenderTargetAutoName(int width, int height, int depth, RenderTextureFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), name, mips, enableMSAA, msaaSamples);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AEB3 File Offset: 0x000090B3
		public static string GetRenderTargetAutoName(int width, int height, int depth, GraphicsFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), name, mips, enableMSAA, msaaSamples);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000AED4 File Offset: 0x000090D4
		private static string GetRenderTargetAutoName(int width, int height, int depth, string format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			string text = string.Format("{0}_{1}x{2}", name, width, height);
			if (depth > 1)
			{
				text = string.Format("{0}x{1}", text, depth);
			}
			if (mips)
			{
				text = string.Format("{0}_{1}", text, "Mips");
			}
			text = string.Format("{0}_{1}", text, format);
			if (enableMSAA)
			{
				text = string.Format("{0}_{1}", text, msaaSamples.ToString());
			}
			return text;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AF50 File Offset: 0x00009150
		public static string GetTextureAutoName(int width, int height, TextureFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AF6D File Offset: 0x0000916D
		public static string GetTextureAutoName(int width, int height, GraphicsFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AF8C File Offset: 0x0000918C
		private static string GetTextureAutoName(int width, int height, string format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			string text;
			if (depth == 0)
			{
				text = string.Format("{0}x{1}{2}_{3}", new object[]
				{
					width,
					height,
					mips ? "_Mips" : "",
					format
				});
			}
			else
			{
				text = string.Format("{0}x{1}x{2}{3}_{4}", new object[]
				{
					width,
					height,
					depth,
					mips ? "_Mips" : "",
					format
				});
			}
			return string.Format("{0}_{1}_{2}", (name == "") ? "Texture" : name, (dim == TextureDimension.None) ? "" : dim.ToString(), text);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B058 File Offset: 0x00009258
		public static void ClearCubemap(CommandBuffer cmd, RenderTexture renderTexture, Color clearColor, bool clearMips = false)
		{
			int num = 1;
			if (renderTexture.useMipMap && clearMips)
			{
				num = (int)Mathf.Log((float)renderTexture.width, 2f) + 1;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num; j++)
				{
					CoreUtils.SetRenderTarget(cmd, new RenderTargetIdentifier(renderTexture), ClearFlag.Color, clearColor, j, (CubemapFace)i, -1);
				}
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B0AF File Offset: 0x000092AF
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000B0C2 File Offset: 0x000092C2
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B0DD File Offset: 0x000092DD
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B0FD File Offset: 0x000092FD
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffers, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B11D File Offset: 0x0000931D
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.DrawFullScreen(commandBuffer, material, colorBuffers, colorBuffers[0], properties, shaderPassId);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B131 File Offset: 0x00009331
		public static Color ConvertSRGBToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color;
			}
			return color.linear;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B144 File Offset: 0x00009344
		public static Color ConvertLinearToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color.gamma;
			}
			return color;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B158 File Offset: 0x00009358
		public static Material CreateEngineMaterial(string shaderPath)
		{
			Shader shader = Shader.Find(shaderPath);
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader " + shaderPath + " could not be found");
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B19A File Offset: 0x0000939A
		public static Material CreateEngineMaterial(Shader shader)
		{
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader is null");
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B1BF File Offset: 0x000093BF
		public static bool HasFlag<T>(T mask, T flag) where T : IConvertible
		{
			return (mask.ToUInt32(null) & flag.ToUInt32(null)) > 0U;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B20B File Offset: 0x0000940B
		public static void SetKeyword(CommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.EnableShaderKeyword(keyword);
				return;
			}
			cmd.DisableShaderKeyword(keyword);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B21F File Offset: 0x0000941F
		public static void SetKeyword(Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword);
				return;
			}
			material.DisableKeyword(keyword);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B233 File Offset: 0x00009433
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B244 File Offset: 0x00009444
		public static IEnumerable<Type> GetAllAssemblyTypes()
		{
			if (CoreUtils.m_AssemblyTypes == null)
			{
				CoreUtils.m_AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly t)
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
			return CoreUtils.m_AssemblyTypes;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B290 File Offset: 0x00009490
		public static IEnumerable<Type> GetAllTypesDerivedFrom<T>()
		{
			return from t in CoreUtils.GetAllAssemblyTypes()
				where t.IsSubclassOf(typeof(T))
				select t;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B2BB File Offset: 0x000094BB
		public static void SafeRelease(ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Release();
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public static Mesh CreateCubeMesh(Vector3 min, Vector3 max)
		{
			return new Mesh
			{
				vertices = new Vector3[]
				{
					new Vector3(min.x, min.y, min.z),
					new Vector3(max.x, min.y, min.z),
					new Vector3(max.x, max.y, min.z),
					new Vector3(min.x, max.y, min.z),
					new Vector3(min.x, min.y, max.z),
					new Vector3(max.x, min.y, max.z),
					new Vector3(max.x, max.y, max.z),
					new Vector3(min.x, max.y, max.z)
				},
				triangles = new int[]
				{
					0, 2, 1, 0, 3, 2, 1, 6, 5, 1,
					2, 6, 5, 7, 4, 5, 6, 7, 4, 3,
					0, 4, 7, 3, 3, 6, 2, 3, 7, 6,
					4, 1, 5, 4, 0, 1
				}
			};
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B492 File Offset: 0x00009692
		public static bool ArePostProcessesEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B492 File Offset: 0x00009692
		public static bool AreAnimatedMaterialsEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00005672 File Offset: 0x00003872
		public static bool IsSceneLightingDisabled(Camera camera)
		{
			return false;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B492 File Offset: 0x00009692
		public static bool IsSceneViewFogEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x04000173 RID: 371
		public static readonly Vector3[] lookAtList = new Vector3[]
		{
			new Vector3(1f, 0f, 0f),
			new Vector3(-1f, 0f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, -1f, 0f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 0f, -1f)
		};

		// Token: 0x04000174 RID: 372
		public static readonly Vector3[] upVectorList = new Vector3[]
		{
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f)
		};

		// Token: 0x04000175 RID: 373
		public const int editMenuPriority1 = 320;

		// Token: 0x04000176 RID: 374
		public const int editMenuPriority2 = 331;

		// Token: 0x04000177 RID: 375
		public const int editMenuPriority3 = 342;

		// Token: 0x04000178 RID: 376
		public const int editMenuPriority4 = 353;

		// Token: 0x04000179 RID: 377
		public const int assetCreateMenuPriority1 = 230;

		// Token: 0x0400017A RID: 378
		public const int assetCreateMenuPriority2 = 241;

		// Token: 0x0400017B RID: 379
		public const int assetCreateMenuPriority3 = 300;

		// Token: 0x0400017C RID: 380
		public const int gameObjectMenuPriority = 10;

		// Token: 0x0400017D RID: 381
		private static Cubemap m_BlackCubeTexture;

		// Token: 0x0400017E RID: 382
		private static Cubemap m_MagentaCubeTexture;

		// Token: 0x0400017F RID: 383
		private static CubemapArray m_MagentaCubeTextureArray;

		// Token: 0x04000180 RID: 384
		private static Cubemap m_WhiteCubeTexture;

		// Token: 0x04000181 RID: 385
		private static RenderTexture m_EmptyUAV;

		// Token: 0x04000182 RID: 386
		private static Texture3D m_BlackVolumeTexture;

		// Token: 0x04000183 RID: 387
		private static IEnumerable<Type> m_AssemblyTypes;
	}
}
