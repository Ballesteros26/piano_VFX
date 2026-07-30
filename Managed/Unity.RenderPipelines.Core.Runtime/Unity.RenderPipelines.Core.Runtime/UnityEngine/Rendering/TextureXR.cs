using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004E RID: 78
	public static class TextureXR
	{
		// Token: 0x17000043 RID: 67
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00008C49 File Offset: 0x00006E49
		public static int maxViews
		{
			set
			{
				TextureXR.m_MaxViews = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008C51 File Offset: 0x00006E51
		public static int slices
		{
			get
			{
				return TextureXR.m_MaxViews;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008C58 File Offset: 0x00006E58
		public static bool useTexArray
		{
			get
			{
				GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
				if (graphicsDeviceType <= GraphicsDeviceType.PlayStation4)
				{
					if (graphicsDeviceType != GraphicsDeviceType.Direct3D11)
					{
						if (graphicsDeviceType != GraphicsDeviceType.PlayStation4)
						{
							return false;
						}
						return true;
					}
				}
				else if (graphicsDeviceType != GraphicsDeviceType.Direct3D12)
				{
					if (graphicsDeviceType != GraphicsDeviceType.Vulkan)
					{
						return false;
					}
					return true;
				}
				return SystemInfo.graphicsDeviceType != GraphicsDeviceType.XboxOne;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008C99 File Offset: 0x00006E99
		public static TextureDimension dimension
		{
			get
			{
				if (!TextureXR.useTexArray)
				{
					return TextureDimension.Tex2D;
				}
				return TextureDimension.Tex2DArray;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008CA5 File Offset: 0x00006EA5
		public static RTHandle GetBlackUIntTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackUIntTextureRTH;
			}
			return TextureXR.m_BlackUIntTexture2DArrayRTH;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008CB9 File Offset: 0x00006EB9
		public static RTHandle GetClearTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_ClearTextureRTH;
			}
			return TextureXR.m_ClearTexture2DArrayRTH;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008CCD File Offset: 0x00006ECD
		public static RTHandle GetMagentaTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_MagentaTextureRTH;
			}
			return TextureXR.m_MagentaTexture2DArrayRTH;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008CE1 File Offset: 0x00006EE1
		public static RTHandle GetBlackTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackTextureRTH;
			}
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008CF5 File Offset: 0x00006EF5
		public static RTHandle GetBlackTextureArray()
		{
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008CFC File Offset: 0x00006EFC
		public static RTHandle GetWhiteTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_WhiteTextureRTH;
			}
			return TextureXR.m_WhiteTexture2DArrayRTH;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008D10 File Offset: 0x00006F10
		public static void Initialize(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			if (TextureXR.m_BlackUIntTexture2DArray == null)
			{
				RTHandles.Release(TextureXR.m_BlackUIntTexture2DArrayRTH);
				TextureXR.m_BlackUIntTexture2DArray = TextureXR.CreateBlackUIntTextureArray(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackUIntTextureRTH);
				TextureXR.m_BlackUIntTexture = TextureXR.CreateBlackUintTexture(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTextureRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture);
				RTHandles.Release(TextureXR.m_ClearTextureRTH);
				TextureXR.m_ClearTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
				{
					name = "Clear Texture"
				};
				TextureXR.m_ClearTexture.SetPixel(0, 0, Color.clear);
				TextureXR.m_ClearTexture.Apply();
				TextureXR.m_ClearTextureRTH = RTHandles.Alloc(TextureXR.m_ClearTexture);
				RTHandles.Release(TextureXR.m_ClearTexture2DArrayRTH);
				TextureXR.m_ClearTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_ClearTexture, "Clear Texture2DArray");
				TextureXR.m_ClearTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_ClearTexture2DArray);
				RTHandles.Release(TextureXR.m_MagentaTextureRTH);
				TextureXR.m_MagentaTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
				{
					name = "Magenta Texture"
				};
				TextureXR.m_MagentaTexture.SetPixel(0, 0, Color.magenta);
				TextureXR.m_MagentaTexture.Apply();
				TextureXR.m_MagentaTextureRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture);
				RTHandles.Release(TextureXR.m_MagentaTexture2DArrayRTH);
				TextureXR.m_MagentaTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_MagentaTexture, "Magenta Texture2DArray");
				TextureXR.m_MagentaTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTextureRTH);
				TextureXR.m_BlackTextureRTH = RTHandles.Alloc(Texture2D.blackTexture);
				RTHandles.Release(TextureXR.m_BlackTexture2DArrayRTH);
				TextureXR.m_BlackTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(Texture2D.blackTexture, "Black Texture2DArray");
				TextureXR.m_BlackTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackTexture2DArray);
				RTHandles.Release(TextureXR.m_WhiteTextureRTH);
				TextureXR.m_WhiteTextureRTH = RTHandles.Alloc(Texture2D.whiteTexture);
				RTHandles.Release(TextureXR.m_WhiteTexture2DArrayRTH);
				TextureXR.m_WhiteTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(Texture2D.whiteTexture, "White Texture2DArray");
				TextureXR.m_WhiteTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_WhiteTexture2DArray);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008EF8 File Offset: 0x000070F8
		private static Texture2DArray CreateTexture2DArrayFromTexture2D(Texture2D source, string name)
		{
			Texture2DArray texture2DArray = new Texture2DArray(source.width, source.height, TextureXR.slices, source.format, false)
			{
				name = name
			};
			for (int i = 0; i < TextureXR.slices; i++)
			{
				Graphics.CopyTexture(source, 0, 0, texture2DArray, i, 0);
			}
			return texture2DArray;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008F48 File Offset: 0x00007148
		private static Texture CreateBlackUIntTextureArray(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2DArray,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int num = clearR32_UIntShader.FindKernel("ClearUIntTextureArray");
			cmd.SetComputeTextureParam(clearR32_UIntShader, num, "_TargetArray", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, num, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008FC8 File Offset: 0x000071C8
		private static Texture CreateBlackUintTexture(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2D,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int num = clearR32_UIntShader.FindKernel("ClearUIntTexture");
			cmd.SetComputeTextureParam(clearR32_UIntShader, num, "_Target", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, num, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x04000154 RID: 340
		private static int m_MaxViews = 1;

		// Token: 0x04000155 RID: 341
		private static Texture m_BlackUIntTexture2DArray;

		// Token: 0x04000156 RID: 342
		private static Texture m_BlackUIntTexture;

		// Token: 0x04000157 RID: 343
		private static RTHandle m_BlackUIntTexture2DArrayRTH;

		// Token: 0x04000158 RID: 344
		private static RTHandle m_BlackUIntTextureRTH;

		// Token: 0x04000159 RID: 345
		private static Texture2DArray m_ClearTexture2DArray;

		// Token: 0x0400015A RID: 346
		private static Texture2D m_ClearTexture;

		// Token: 0x0400015B RID: 347
		private static RTHandle m_ClearTexture2DArrayRTH;

		// Token: 0x0400015C RID: 348
		private static RTHandle m_ClearTextureRTH;

		// Token: 0x0400015D RID: 349
		private static Texture2DArray m_MagentaTexture2DArray;

		// Token: 0x0400015E RID: 350
		private static Texture2D m_MagentaTexture;

		// Token: 0x0400015F RID: 351
		private static RTHandle m_MagentaTexture2DArrayRTH;

		// Token: 0x04000160 RID: 352
		private static RTHandle m_MagentaTextureRTH;

		// Token: 0x04000161 RID: 353
		private static Texture2DArray m_BlackTexture2DArray;

		// Token: 0x04000162 RID: 354
		private static RTHandle m_BlackTexture2DArrayRTH;

		// Token: 0x04000163 RID: 355
		private static RTHandle m_BlackTextureRTH;

		// Token: 0x04000164 RID: 356
		private static Texture2DArray m_WhiteTexture2DArray;

		// Token: 0x04000165 RID: 357
		private static RTHandle m_WhiteTexture2DArrayRTH;

		// Token: 0x04000166 RID: 358
		private static RTHandle m_WhiteTextureRTH;
	}
}
