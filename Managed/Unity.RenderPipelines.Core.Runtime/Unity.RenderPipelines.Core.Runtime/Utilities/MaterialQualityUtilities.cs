using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utilities
{
	// Token: 0x02000004 RID: 4
	public static class MaterialQualityUtilities
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public static MaterialQuality GetHighestQuality(this MaterialQuality levels)
		{
			for (int i = MaterialQualityUtilities.Keywords.Length - 1; i >= 0; i--)
			{
				MaterialQuality materialQuality = (MaterialQuality)(1 << i);
				if ((levels & materialQuality) != (MaterialQuality)0)
				{
					return materialQuality;
				}
			}
			return (MaterialQuality)0;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		public static MaterialQuality GetClosestQuality(this MaterialQuality availableLevels, MaterialQuality requestedLevel)
		{
			if (availableLevels == (MaterialQuality)0)
			{
				return MaterialQuality.Low;
			}
			int num = requestedLevel.ToFirstIndex();
			MaterialQuality materialQuality = (MaterialQuality)0;
			for (int i = num; i >= 0; i--)
			{
				MaterialQuality materialQuality2 = MaterialQualityUtilities.FromIndex(i);
				if ((materialQuality2 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality2;
					break;
				}
			}
			if (materialQuality != (MaterialQuality)0)
			{
				return materialQuality;
			}
			for (int j = num + 1; j < MaterialQualityUtilities.Keywords.Length; j++)
			{
				MaterialQuality materialQuality3 = MaterialQualityUtilities.FromIndex(j);
				Math.Abs(requestedLevel - materialQuality3);
				if ((materialQuality3 & availableLevels) != (MaterialQuality)0)
				{
					materialQuality = materialQuality3;
					break;
				}
			}
			return materialQuality;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public static void SetGlobalShaderKeywords(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					Shader.EnableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					Shader.DisableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public static void SetGlobalShaderKeywords(this MaterialQuality level, CommandBuffer cmd)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					cmd.EnableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					cmd.DisableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public static int ToFirstIndex(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C4 File Offset: 0x000003C4
		public static MaterialQuality FromIndex(int index)
		{
			return (MaterialQuality)(1 << index);
		}

		// Token: 0x04000006 RID: 6
		public static string[] KeywordNames = new string[] { "MATERIAL_QUALITY_LOW", "MATERIAL_QUALITY_MEDIUM", "MATERIAL_QUALITY_HIGH" };

		// Token: 0x04000007 RID: 7
		public static string[] EnumNames = Enum.GetNames(typeof(MaterialQuality));

		// Token: 0x04000008 RID: 8
		public static ShaderKeyword[] Keywords = new ShaderKeyword[]
		{
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[0]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[1]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[2])
		};
	}
}
