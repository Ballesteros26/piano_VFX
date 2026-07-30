using System;
using System.Linq;

namespace UnityEngine.TextCore
{
	// Token: 0x02000020 RID: 32
	internal static class ShaderUtilities
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000062F0 File Offset: 0x000044F0
		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				bool flag = ShaderUtilities.k_ShaderRef_MobileSDF == null;
				if (flag)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Hidden/TextCore/Distance Field SSD");
				}
				return ShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006328 File Offset: 0x00004528
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				bool flag = ShaderUtilities.k_ShaderRef_MobileBitmap == null;
				if (flag)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Hidden/Internal-GUITextureClipText");
				}
				return ShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006360 File Offset: 0x00004560
		static ShaderUtilities()
		{
			ShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000063E4 File Offset: 0x000045E4
		public static void GetShaderPropertyIDs()
		{
			bool flag = !ShaderUtilities.isInitialized;
			if (flag)
			{
				ShaderUtilities.isInitialized = true;
				ShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				ShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				ShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				ShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				ShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				ShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				ShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				ShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				ShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				ShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				ShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				ShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				ShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				ShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				ShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				ShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				ShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				ShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				ShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				ShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				ShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				ShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				ShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				ShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				ShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				ShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				ShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				ShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				ShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				ShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				ShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				ShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				ShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				ShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				ShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				ShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				ShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				ShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				ShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				ShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				ShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				ShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				ShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				ShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				ShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				ShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				ShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000066CC File Offset: 0x000048CC
		public static void UpdateShaderRatios(Material mat)
		{
			bool flag = !Enumerable.Contains<string>(mat.shaderKeywords, ShaderUtilities.Keyword_Ratios);
			float @float = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = mat.GetFloat(ShaderUtilities.ID_FaceDilate);
			float float3 = mat.GetFloat(ShaderUtilities.ID_OutlineWidth);
			float float4 = mat.GetFloat(ShaderUtilities.ID_OutlineSoftness);
			float num = Mathf.Max(mat.GetFloat(ShaderUtilities.ID_WeightNormal), mat.GetFloat(ShaderUtilities.ID_WeightBold)) / 4f;
			float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
			float num3 = (flag ? ((@float - ShaderUtilities.m_clamp) / (@float * num2)) : 1f);
			mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, num3);
			bool flag2 = mat.HasProperty(ShaderUtilities.ID_GlowOffset);
			if (flag2)
			{
				float float5 = mat.GetFloat(ShaderUtilities.ID_GlowOffset);
				float float6 = mat.GetFloat(ShaderUtilities.ID_GlowOuter);
				float num4 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, float5 + float6);
				float num5 = (flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_B, num5);
			}
			bool flag3 = mat.HasProperty(ShaderUtilities.ID_UnderlayOffsetX);
			if (flag3)
			{
				float float7 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
				float float8 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
				float float9 = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
				float float10 = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				float num6 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
				float num7 = (flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num6) / (@float * num2)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_C, num7);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000068AC File Offset: 0x00004AAC
		public static bool IsMaskingEnabled(Material material)
		{
			bool flag = material == null || !material.HasProperty(ShaderUtilities.ID_ClipRect);
			return !flag && (Enumerable.Contains<string>(material.shaderKeywords, ShaderUtilities.Keyword_MASK_SOFT) || Enumerable.Contains<string>(material.shaderKeywords, ShaderUtilities.Keyword_MASK_HARD) || Enumerable.Contains<string>(material.shaderKeywords, ShaderUtilities.Keyword_MASK_TEX));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006918 File Offset: 0x00004B18
		public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			bool flag = !ShaderUtilities.isInitialized;
			if (flag)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = material == null;
			float num;
			if (flag2)
			{
				num = 0f;
			}
			else
			{
				int num2 = (enableExtraPadding ? 4 : 0);
				bool flag3 = !material.HasProperty(ShaderUtilities.ID_GradientScale);
				if (flag3)
				{
					num = (float)num2;
				}
				else
				{
					Vector4 vector = Vector4.zero;
					Vector4 zero = Vector4.zero;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					float num10 = 0f;
					ShaderUtilities.UpdateShaderRatios(material);
					string[] shaderKeywords = material.shaderKeywords;
					bool flag4 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_A);
					if (flag4)
					{
						num6 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
					}
					bool flag5 = material.HasProperty(ShaderUtilities.ID_FaceDilate);
					if (flag5)
					{
						num3 = material.GetFloat(ShaderUtilities.ID_FaceDilate) * num6;
					}
					bool flag6 = material.HasProperty(ShaderUtilities.ID_OutlineSoftness);
					if (flag6)
					{
						num4 = material.GetFloat(ShaderUtilities.ID_OutlineSoftness) * num6;
					}
					bool flag7 = material.HasProperty(ShaderUtilities.ID_OutlineWidth);
					if (flag7)
					{
						num5 = material.GetFloat(ShaderUtilities.ID_OutlineWidth) * num6;
					}
					float num11 = num5 + num4 + num3;
					bool flag8 = material.HasProperty(ShaderUtilities.ID_GlowOffset) && Enumerable.Contains<string>(shaderKeywords, ShaderUtilities.Keyword_Glow);
					if (flag8)
					{
						bool flag9 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_B);
						if (flag9)
						{
							num7 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_B);
						}
						num9 = material.GetFloat(ShaderUtilities.ID_GlowOffset) * num7;
						num10 = material.GetFloat(ShaderUtilities.ID_GlowOuter) * num7;
					}
					num11 = Mathf.Max(num11, num3 + num9 + num10);
					bool flag10 = material.HasProperty(ShaderUtilities.ID_UnderlaySoftness) && Enumerable.Contains<string>(shaderKeywords, ShaderUtilities.Keyword_Underlay);
					if (flag10)
					{
						bool flag11 = material.HasProperty(ShaderUtilities.ID_ScaleRatio_C);
						if (flag11)
						{
							num8 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_C);
						}
						float num12 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num8;
						float num13 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num8;
						float num14 = material.GetFloat(ShaderUtilities.ID_UnderlayDilate) * num8;
						float num15 = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num8;
						vector.x = Mathf.Max(vector.x, num3 + num14 + num15 - num12);
						vector.y = Mathf.Max(vector.y, num3 + num14 + num15 - num13);
						vector.z = Mathf.Max(vector.z, num3 + num14 + num15 + num12);
						vector.w = Mathf.Max(vector.w, num3 + num14 + num15 + num13);
					}
					vector.x = Mathf.Max(vector.x, num11);
					vector.y = Mathf.Max(vector.y, num11);
					vector.z = Mathf.Max(vector.z, num11);
					vector.w = Mathf.Max(vector.w, num11);
					vector.x += (float)num2;
					vector.y += (float)num2;
					vector.z += (float)num2;
					vector.w += (float)num2;
					vector.x = Mathf.Min(vector.x, 1f);
					vector.y = Mathf.Min(vector.y, 1f);
					vector.z = Mathf.Min(vector.z, 1f);
					vector.w = Mathf.Min(vector.w, 1f);
					zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
					zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
					zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
					zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
					float @float = material.GetFloat(ShaderUtilities.ID_GradientScale);
					vector *= @float;
					num11 = Mathf.Max(vector.x, vector.y);
					num11 = Mathf.Max(vector.z, num11);
					num11 = Mathf.Max(vector.w, num11);
					num = num11 + 0.5f;
				}
			}
			return num;
		}

		// Token: 0x0400013A RID: 314
		public static int ID_MainTex;

		// Token: 0x0400013B RID: 315
		public static int ID_FaceTex;

		// Token: 0x0400013C RID: 316
		public static int ID_FaceColor;

		// Token: 0x0400013D RID: 317
		public static int ID_FaceDilate;

		// Token: 0x0400013E RID: 318
		public static int ID_Shininess;

		// Token: 0x0400013F RID: 319
		public static int ID_UnderlayColor;

		// Token: 0x04000140 RID: 320
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000141 RID: 321
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000142 RID: 322
		public static int ID_UnderlayDilate;

		// Token: 0x04000143 RID: 323
		public static int ID_UnderlaySoftness;

		// Token: 0x04000144 RID: 324
		public static int ID_WeightNormal;

		// Token: 0x04000145 RID: 325
		public static int ID_WeightBold;

		// Token: 0x04000146 RID: 326
		public static int ID_OutlineTex;

		// Token: 0x04000147 RID: 327
		public static int ID_OutlineWidth;

		// Token: 0x04000148 RID: 328
		public static int ID_OutlineSoftness;

		// Token: 0x04000149 RID: 329
		public static int ID_OutlineColor;

		// Token: 0x0400014A RID: 330
		public static int ID_GradientScale;

		// Token: 0x0400014B RID: 331
		public static int ID_ScaleX;

		// Token: 0x0400014C RID: 332
		public static int ID_ScaleY;

		// Token: 0x0400014D RID: 333
		public static int ID_PerspectiveFilter;

		// Token: 0x0400014E RID: 334
		public static int ID_TextureWidth;

		// Token: 0x0400014F RID: 335
		public static int ID_TextureHeight;

		// Token: 0x04000150 RID: 336
		public static int ID_BevelAmount;

		// Token: 0x04000151 RID: 337
		public static int ID_GlowColor;

		// Token: 0x04000152 RID: 338
		public static int ID_GlowOffset;

		// Token: 0x04000153 RID: 339
		public static int ID_GlowPower;

		// Token: 0x04000154 RID: 340
		public static int ID_GlowOuter;

		// Token: 0x04000155 RID: 341
		public static int ID_LightAngle;

		// Token: 0x04000156 RID: 342
		public static int ID_EnvMap;

		// Token: 0x04000157 RID: 343
		public static int ID_EnvMatrix;

		// Token: 0x04000158 RID: 344
		public static int ID_EnvMatrixRotation;

		// Token: 0x04000159 RID: 345
		public static int ID_MaskCoord;

		// Token: 0x0400015A RID: 346
		public static int ID_ClipRect;

		// Token: 0x0400015B RID: 347
		public static int ID_MaskSoftnessX;

		// Token: 0x0400015C RID: 348
		public static int ID_MaskSoftnessY;

		// Token: 0x0400015D RID: 349
		public static int ID_VertexOffsetX;

		// Token: 0x0400015E RID: 350
		public static int ID_VertexOffsetY;

		// Token: 0x0400015F RID: 351
		public static int ID_UseClipRect;

		// Token: 0x04000160 RID: 352
		public static int ID_StencilID;

		// Token: 0x04000161 RID: 353
		public static int ID_StencilOp;

		// Token: 0x04000162 RID: 354
		public static int ID_StencilComp;

		// Token: 0x04000163 RID: 355
		public static int ID_StencilReadMask;

		// Token: 0x04000164 RID: 356
		public static int ID_StencilWriteMask;

		// Token: 0x04000165 RID: 357
		public static int ID_ShaderFlags;

		// Token: 0x04000166 RID: 358
		public static int ID_ScaleRatio_A;

		// Token: 0x04000167 RID: 359
		public static int ID_ScaleRatio_B;

		// Token: 0x04000168 RID: 360
		public static int ID_ScaleRatio_C;

		// Token: 0x04000169 RID: 361
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x0400016A RID: 362
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x0400016B RID: 363
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x0400016C RID: 364
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x0400016D RID: 365
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x0400016E RID: 366
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x0400016F RID: 367
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x04000170 RID: 368
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x04000171 RID: 369
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x04000172 RID: 370
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x04000173 RID: 371
		private static float m_clamp = 1f;

		// Token: 0x04000174 RID: 372
		public static bool isInitialized;

		// Token: 0x04000175 RID: 373
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x04000176 RID: 374
		private static Shader k_ShaderRef_MobileBitmap;
	}
}
