using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000038 RID: 56
	public static class StencilMaterial
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00013BAD File Offset: 0x00011DAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Material.Add instead.", true)]
		public static Material Add(Material baseMat, int stencilID)
		{
			return null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00013BB0 File Offset: 0x00011DB0
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask)
		{
			return StencilMaterial.Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 255, 255);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013BC8 File Offset: 0x00011DC8
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask)
		{
			if ((stencilID <= 0 && colorWriteMask == ColorWriteMask.All) || baseMat == null)
			{
				return baseMat;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilOp"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilComp"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilWriteMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_ColorMask"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
				return baseMat;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilId == stencilID && matEntry.operation == operation && matEntry.compareFunction == compareFunction && matEntry.readMask == readMask && matEntry.writeMask == writeMask && matEntry.colorMask == colorWriteMask)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			StencilMaterial.MatEntry matEntry2 = new StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new Material(baseMat);
			matEntry2.customMat.hideFlags = HideFlags.HideAndDontSave;
			matEntry2.stencilId = stencilID;
			matEntry2.operation = operation;
			matEntry2.compareFunction = compareFunction;
			matEntry2.readMask = readMask;
			matEntry2.writeMask = writeMask;
			matEntry2.colorMask = colorWriteMask;
			matEntry2.useAlphaClip = operation != StencilOp.Keep && writeMask > 0;
			matEntry2.customMat.name = string.Format("Stencil Id:{0}, Op:{1}, Comp:{2}, WriteMask:{3}, ReadMask:{4}, ColorMask:{5} AlphaClip:{6} ({7})", new object[] { stencilID, operation, compareFunction, writeMask, readMask, colorWriteMask, matEntry2.useAlphaClip, baseMat.name });
			matEntry2.customMat.SetInt("_Stencil", stencilID);
			matEntry2.customMat.SetInt("_StencilOp", (int)operation);
			matEntry2.customMat.SetInt("_StencilComp", (int)compareFunction);
			matEntry2.customMat.SetInt("_StencilReadMask", readMask);
			matEntry2.customMat.SetInt("_StencilWriteMask", writeMask);
			matEntry2.customMat.SetInt("_ColorMask", (int)colorWriteMask);
			matEntry2.customMat.SetInt("_UseUIAlphaClip", matEntry2.useAlphaClip ? 1 : 0);
			if (matEntry2.useAlphaClip)
			{
				matEntry2.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
			}
			else
			{
				matEntry2.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
			}
			StencilMaterial.m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00013EFC File Offset: 0x000120FC
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					StencilMaterial.MatEntry matEntry2 = matEntry;
					int num = matEntry2.count - 1;
					matEntry2.count = num;
					if (num == 0)
					{
						Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013F74 File Offset: 0x00012174
		public static void ClearAll()
		{
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				Misc.DestroyImmediate(matEntry.customMat);
				matEntry.baseMat = null;
			}
			StencilMaterial.m_List.Clear();
		}

		// Token: 0x04000160 RID: 352
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x020000AB RID: 171
		private class MatEntry
		{
			// Token: 0x040002EA RID: 746
			public Material baseMat;

			// Token: 0x040002EB RID: 747
			public Material customMat;

			// Token: 0x040002EC RID: 748
			public int count;

			// Token: 0x040002ED RID: 749
			public int stencilId;

			// Token: 0x040002EE RID: 750
			public StencilOp operation;

			// Token: 0x040002EF RID: 751
			public CompareFunction compareFunction = CompareFunction.Always;

			// Token: 0x040002F0 RID: 752
			public int readMask;

			// Token: 0x040002F1 RID: 753
			public int writeMask;

			// Token: 0x040002F2 RID: 754
			public bool useAlphaClip;

			// Token: 0x040002F3 RID: 755
			public ColorWriteMask colorMask;
		}
	}
}
