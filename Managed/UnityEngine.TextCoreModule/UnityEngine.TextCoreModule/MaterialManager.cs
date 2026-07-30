using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	// Token: 0x02000017 RID: 23
	internal static class MaterialManager
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00005594 File Offset: 0x00003794
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (long)((ulong)instanceID2);
			Material material;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(num, ref material);
			Material material2;
			if (flag)
			{
				material2 = material;
			}
			else
			{
				bool flag2 = sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale);
				if (flag2)
				{
					material = new Material(sourceMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
					material.SetTexture(ShaderUtilities.ID_MainTex, texture);
					material.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
					material.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
					material.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
					material.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
					material.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
				}
				else
				{
					material = new Material(targetMaterial);
				}
				MaterialManager.s_FallbackMaterials.Add(num, material);
				material2 = material;
			}
			return material2;
		}

		// Token: 0x0400008F RID: 143
		private static Dictionary<long, Material> s_FallbackMaterials = new Dictionary<long, Material>();
	}
}
