using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200002F RID: 47
	public static class TMP_MaterialManager
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000D65C File Offset: 0x0000B85C
		static TMP_MaterialManager()
		{
			Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(TMP_MaterialManager.OnPreRender));
			Canvas.willRenderCanvases += TMP_MaterialManager.OnPreRenderCanvas;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		private static void OnPreRender(Camera cam)
		{
			if (TMP_MaterialManager.isFallbackListDirty)
			{
				TMP_MaterialManager.CleanupFallbackMaterials();
				TMP_MaterialManager.isFallbackListDirty = false;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		private static void OnPreRenderCanvas()
		{
			if (TMP_MaterialManager.isFallbackListDirty)
			{
				TMP_MaterialManager.CleanupFallbackMaterials();
				TMP_MaterialManager.isFallbackListDirty = false;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public static Material GetStencilMaterial(Material baseMaterial, int stencilID)
		{
			if (!baseMaterial.HasProperty(ShaderUtilities.ID_StencilID))
			{
				Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
				return baseMaterial;
			}
			int instanceID = baseMaterial.GetInstanceID();
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				if (TMP_MaterialManager.m_materialList[i].baseMaterial.GetInstanceID() == instanceID && TMP_MaterialManager.m_materialList[i].stencilID == stencilID)
				{
					TMP_MaterialManager.m_materialList[i].count++;
					return TMP_MaterialManager.m_materialList[i].stencilMaterial;
				}
			}
			Material material = new Material(baseMaterial);
			material.hideFlags = HideFlags.HideAndDontSave;
			material.shaderKeywords = baseMaterial.shaderKeywords;
			ShaderUtilities.GetShaderPropertyIDs();
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
			maskingMaterial.baseMaterial = baseMaterial;
			maskingMaterial.stencilMaterial = material;
			maskingMaterial.stencilID = stencilID;
			maskingMaterial.count = 1;
			TMP_MaterialManager.m_materialList.Add(maskingMaterial);
			return material;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public static void ReleaseStencilMaterial(Material stencilMaterial)
		{
			int instanceID = stencilMaterial.GetInstanceID();
			int i = 0;
			while (i < TMP_MaterialManager.m_materialList.Count)
			{
				if (TMP_MaterialManager.m_materialList[i].stencilMaterial.GetInstanceID() == instanceID)
				{
					if (TMP_MaterialManager.m_materialList[i].count > 1)
					{
						TMP_MaterialManager.m_materialList[i].count--;
						return;
					}
					global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
					TMP_MaterialManager.m_materialList.RemoveAt(i);
					stencilMaterial = null;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D870 File Offset: 0x0000BA70
		public static Material GetBaseMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				return null;
			}
			return TMP_MaterialManager.m_materialList[num].baseMaterial;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D8B7 File Offset: 0x0000BAB7
		public static Material SetStencil(Material material, int stencilID)
		{
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			if (stencilID == 0)
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 8f);
			}
			else
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			}
			return material;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public static void AddMaskingMaterial(Material baseMaterial, Material stencilMaterial, int stencilID)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
				maskingMaterial.baseMaterial = baseMaterial;
				maskingMaterial.stencilMaterial = stencilMaterial;
				maskingMaterial.stencilID = stencilID;
				maskingMaterial.count = 1;
				TMP_MaterialManager.m_materialList.Add(maskingMaterial);
				return;
			}
			stencilMaterial = TMP_MaterialManager.m_materialList[num].stencilMaterial;
			TMP_MaterialManager.m_materialList[num].count++;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D984 File Offset: 0x0000BB84
		public static void RemoveStencilMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num != -1)
			{
				TMP_MaterialManager.m_materialList.RemoveAt(num);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public static void ReleaseBaseMaterial(Material baseMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.baseMaterial == baseMaterial);
			if (num == -1)
			{
				Debug.Log("No Masking Material exists for " + baseMaterial.name);
				return;
			}
			if (TMP_MaterialManager.m_materialList[num].count > 1)
			{
				TMP_MaterialManager.m_materialList[num].count--;
				Debug.Log(string.Concat(new object[]
				{
					"Removed (1) reference to ",
					TMP_MaterialManager.m_materialList[num].stencilMaterial.name,
					". There are ",
					TMP_MaterialManager.m_materialList[num].count,
					" references left."
				}));
				return;
			}
			Debug.Log(string.Concat(new object[]
			{
				"Removed last reference to ",
				TMP_MaterialManager.m_materialList[num].stencilMaterial.name,
				" with ID ",
				TMP_MaterialManager.m_materialList[num].stencilMaterial.GetInstanceID()
			}));
			global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[num].stencilMaterial);
			TMP_MaterialManager.m_materialList.RemoveAt(num);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public static void ClearMaterials()
		{
			if (TMP_MaterialManager.m_materialList.Count == 0)
			{
				Debug.Log("Material List has already been cleared.");
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
				TMP_MaterialManager.m_materialList.RemoveAt(i);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public static int GetStencilID(GameObject obj)
		{
			int num = 0;
			Transform transform = obj.transform;
			Transform transform2 = TMP_MaterialManager.FindRootSortOverrideCanvas(transform);
			if (transform == transform2)
			{
				return num;
			}
			Transform transform3 = transform.parent;
			List<Mask> list = TMP_ListPool<Mask>.Get();
			while (transform3 != null)
			{
				transform3.GetComponents<Mask>(list);
				for (int i = 0; i < list.Count; i++)
				{
					Mask mask = list[i];
					if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
					{
						num++;
						break;
					}
				}
				if (transform3 == transform2)
				{
					break;
				}
				transform3 = transform3.parent;
			}
			TMP_ListPool<Mask>.Release(list);
			return Mathf.Min((1 << num) - 1, 255);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000DC28 File Offset: 0x0000BE28
		public static Material GetMaterialForRendering(MaskableGraphic graphic, Material baseMaterial)
		{
			if (baseMaterial == null)
			{
				return null;
			}
			List<IMaterialModifier> list = TMP_ListPool<IMaterialModifier>.Get();
			graphic.GetComponents<IMaterialModifier>(list);
			Material material = baseMaterial;
			for (int i = 0; i < list.Count; i++)
			{
				material = list[i].GetModifiedMaterial(material);
			}
			TMP_ListPool<IMaterialModifier>.Release(list);
			return material;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private static Transform FindRootSortOverrideCanvas(Transform start)
		{
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			start.GetComponentsInParent<Canvas>(false, list);
			Canvas canvas = null;
			for (int i = 0; i < list.Count; i++)
			{
				canvas = list[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		internal static Material GetFallbackMaterial(TMP_FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = fontAsset.atlasTextures[atlasIndex];
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				return fallbackMaterial.fallbackMaterial;
			}
			Material material = new Material(sourceMaterial);
			material.SetTexture(ShaderUtilities.ID_MainTex, texture);
			material.hideFlags = HideFlags.HideAndDontSave;
			fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
			fallbackMaterial.baseID = instanceID;
			fallbackMaterial.baseMaterial = fontAsset.material;
			fallbackMaterial.fallbackID = num;
			fallbackMaterial.fallbackMaterial = material;
			fallbackMaterial.count = 0;
			TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
			TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
			return material;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			int instanceID = sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = ((long)instanceID << 32) | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				return fallbackMaterial.fallbackMaterial;
			}
			Material material;
			if (sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
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
			fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
			fallbackMaterial.baseID = instanceID;
			fallbackMaterial.baseMaterial = sourceMaterial;
			fallbackMaterial.fallbackID = num;
			fallbackMaterial.fallbackMaterial = material;
			fallbackMaterial.count = 0;
			TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
			TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
			return material;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public static void AddFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				fallbackMaterial.count++;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000DF2C File Offset: 0x0000C12C
		public static void RemoveFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				fallbackMaterial.count--;
				if (fallbackMaterial.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial);
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000DF8C File Offset: 0x0000C18C
		public static void CleanupFallbackMaterials()
		{
			if (TMP_MaterialManager.m_fallbackCleanupList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_fallbackCleanupList.Count; i++)
			{
				TMP_MaterialManager.FallbackMaterial fallbackMaterial = TMP_MaterialManager.m_fallbackCleanupList[i];
				if (fallbackMaterial.count < 1)
				{
					Material fallbackMaterial2 = fallbackMaterial.fallbackMaterial;
					TMP_MaterialManager.m_fallbackMaterials.Remove(fallbackMaterial.fallbackID);
					TMP_MaterialManager.m_fallbackMaterialLookup.Remove(fallbackMaterial2.GetInstanceID());
					global::UnityEngine.Object.DestroyImmediate(fallbackMaterial2);
				}
			}
			TMP_MaterialManager.m_fallbackCleanupList.Clear();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E00C File Offset: 0x0000C20C
		public static void ReleaseFallbackMaterial(Material fallackMaterial)
		{
			if (fallackMaterial == null)
			{
				return;
			}
			int instanceID = fallackMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				fallbackMaterial.count--;
				if (fallbackMaterial.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial);
				}
			}
			TMP_MaterialManager.isFallbackListDirty = true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E070 File Offset: 0x0000C270
		public static void CopyMaterialPresetProperties(Material source, Material destination)
		{
			if (!source.HasProperty(ShaderUtilities.ID_GradientScale) || !destination.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				return;
			}
			Texture texture = destination.GetTexture(ShaderUtilities.ID_MainTex);
			float @float = destination.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = destination.GetFloat(ShaderUtilities.ID_TextureWidth);
			float float3 = destination.GetFloat(ShaderUtilities.ID_TextureHeight);
			float float4 = destination.GetFloat(ShaderUtilities.ID_WeightNormal);
			float float5 = destination.GetFloat(ShaderUtilities.ID_WeightBold);
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(ShaderUtilities.ID_MainTex, texture);
			destination.SetFloat(ShaderUtilities.ID_GradientScale, @float);
			destination.SetFloat(ShaderUtilities.ID_TextureWidth, float2);
			destination.SetFloat(ShaderUtilities.ID_TextureHeight, float3);
			destination.SetFloat(ShaderUtilities.ID_WeightNormal, float4);
			destination.SetFloat(ShaderUtilities.ID_WeightBold, float5);
		}

		// Token: 0x04000178 RID: 376
		private static List<TMP_MaterialManager.MaskingMaterial> m_materialList = new List<TMP_MaterialManager.MaskingMaterial>();

		// Token: 0x04000179 RID: 377
		private static Dictionary<long, TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials = new Dictionary<long, TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x0400017A RID: 378
		private static Dictionary<int, long> m_fallbackMaterialLookup = new Dictionary<int, long>();

		// Token: 0x0400017B RID: 379
		private static List<TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList = new List<TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x0400017C RID: 380
		private static bool isFallbackListDirty;

		// Token: 0x02000095 RID: 149
		private class FallbackMaterial
		{
			// Token: 0x0400056B RID: 1387
			public int baseID;

			// Token: 0x0400056C RID: 1388
			public Material baseMaterial;

			// Token: 0x0400056D RID: 1389
			public long fallbackID;

			// Token: 0x0400056E RID: 1390
			public Material fallbackMaterial;

			// Token: 0x0400056F RID: 1391
			public int count;
		}

		// Token: 0x02000096 RID: 150
		private class MaskingMaterial
		{
			// Token: 0x04000570 RID: 1392
			public Material baseMaterial;

			// Token: 0x04000571 RID: 1393
			public Material stencilMaterial;

			// Token: 0x04000572 RID: 1394
			public int count;

			// Token: 0x04000573 RID: 1395
			public int stencilID;
		}
	}
}
