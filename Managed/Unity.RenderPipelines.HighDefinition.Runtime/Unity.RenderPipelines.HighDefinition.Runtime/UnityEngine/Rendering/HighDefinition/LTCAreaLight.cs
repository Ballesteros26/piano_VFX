using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AA RID: 170
	internal class LTCAreaLight
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000344CC File Offset: 0x000326CC
		public static LTCAreaLight instance
		{
			get
			{
				if (LTCAreaLight.s_Instance == null)
				{
					LTCAreaLight.s_Instance = new LTCAreaLight();
				}
				return LTCAreaLight.s_Instance;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000344E4 File Offset: 0x000326E4
		private LTCAreaLight()
		{
			this.m_refCounting = 0;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000344F4 File Offset: 0x000326F4
		public static void LoadLUT(Texture2DArray tex, int arrayElement, TextureFormat format, float[] LUTScalar)
		{
			Color[] array = new Color[4096];
			for (int i = 0; i < 4096; i++)
			{
				array[i] = new Color(0f, 0f, 0f, LUTScalar[i]);
			}
			tex.SetPixels(array, arrayElement);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00034544 File Offset: 0x00032744
		public static void LoadLUT(Texture2DArray tex, int arrayElement, TextureFormat format, double[,] LUTTransformInv)
		{
			Color[] array = new Color[4096];
			float num = ((format == TextureFormat.RGBAHalf) ? 65504f : float.MaxValue);
			for (int i = 0; i < 4096; i++)
			{
				array[i] = new Color(Mathf.Min(num, (float)LUTTransformInv[i, 0]), Mathf.Min(num, (float)LUTTransformInv[i, 2]), Mathf.Min(num, (float)LUTTransformInv[i, 4]), Mathf.Min(num, (float)LUTTransformInv[i, 6]));
			}
			tex.SetPixels(array, arrayElement);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000345D0 File Offset: 0x000327D0
		public void Build()
		{
			if (this.m_refCounting == 0)
			{
				this.m_LtcData = new Texture2DArray(64, 64, 3, TextureFormat.RGBAHalf, false, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Bilinear,
					name = CoreUtils.GetTextureAutoName(64, 64, TextureFormat.RGBAHalf, TextureDimension.Tex2DArray, "LTC_LUT", false, 2)
				};
				LTCAreaLight.LoadLUT(this.m_LtcData, 0, TextureFormat.RGBAHalf, LTCAreaLight.s_LtcGGXMatrixData);
				LTCAreaLight.LoadLUT(this.m_LtcData, 1, TextureFormat.RGBAHalf, LTCAreaLight.s_LtcDisneyDiffuseMatrixData);
				this.m_LtcData.Apply();
			}
			this.m_refCounting++;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00034667 File Offset: 0x00032867
		public void Cleanup()
		{
			this.m_refCounting--;
			if (this.m_refCounting == 0)
			{
				CoreUtils.Destroy(this.m_LtcData);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003468A File Offset: 0x0003288A
		public void Bind(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture("_LtcData", this.m_LtcData);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000346A4 File Offset: 0x000328A4
		// Note: this type is marked as 'beforefieldinit'.
		static LTCAreaLight()
		{
			/*
An exception occurred when decompiling this method (06000656)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void UnityEngine.Rendering.HighDefinition.LTCAreaLight::.cctor()

 ---> System.ArgumentException: Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.
   at System.Collections.Generic.List`1.GetRange(Int32 index, Int32 count)
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformByteCode(ILExpression byteCode) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 608
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformExpression(ILExpression expr) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 407
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformByteCode(ILExpression byteCode) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 488
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformExpression(ILExpression expr) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 407
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformNode(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 268
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.TransformBlock(ILBlock block) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 252
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 150
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1683
*/;
		}

		// Token: 0x040006AB RID: 1707
		public static double[,] s_LtcMatrixData_GGX;

		// Token: 0x040006AC RID: 1708
		private static LTCAreaLight s_Instance;

		// Token: 0x040006AD RID: 1709
		private int m_refCounting;

		// Token: 0x040006AE RID: 1710
		private Texture2DArray m_LtcData;

		// Token: 0x040006AF RID: 1711
		public const int k_LtcLUTMatrixDim = 3;

		// Token: 0x040006B0 RID: 1712
		public const int k_LtcLUTResolution = 64;

		// Token: 0x040006B1 RID: 1713
		public static double[,] s_LtcDisneyDiffuseMatrixData;

		// Token: 0x040006B2 RID: 1714
		public static double[,] s_LtcGGXMatrixData;
	}
}
