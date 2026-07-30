using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore
{
	// Token: 0x02000018 RID: 24
	internal struct MaterialReference
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000056D0 File Offset: 0x000038D0
		public MaterialReference(int index, FontAsset fontAsset, TextSpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isDefaultMaterial = material.GetInstanceID() == fontAsset.material.GetInstanceID();
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005734 File Offset: 0x00003934
		public static bool Contains(MaterialReference[] materialReferences, FontAsset fontAsset)
		{
			int instanceID = fontAsset.GetInstanceID();
			int num = 0;
			while (num < materialReferences.Length && materialReferences[num].fontAsset != null)
			{
				bool flag = materialReferences[num].fontAsset.GetInstanceID() == instanceID;
				if (flag)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005798 File Offset: 0x00003998
		public static int AddMaterialReference(Material material, FontAsset fontAsset, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, ref count);
			int num;
			if (flag)
			{
				num = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = fontAsset;
				materialReferences[count].spriteAsset = null;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = instanceID == fontAsset.material.GetInstanceID();
				materialReferences[count].referenceCount = 0;
				num = count;
			}
			return num;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005830 File Offset: 0x00003A30
		public static int AddMaterialReference(Material material, TextSpriteAsset spriteAsset, MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(instanceID, ref count);
			int num;
			if (flag)
			{
				num = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[instanceID] = count;
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = materialReferences[0].fontAsset;
				materialReferences[count].spriteAsset = spriteAsset;
				materialReferences[count].material = material;
				materialReferences[count].isDefaultMaterial = true;
				materialReferences[count].referenceCount = 0;
				num = count;
			}
			return num;
		}

		// Token: 0x04000090 RID: 144
		public int index;

		// Token: 0x04000091 RID: 145
		public FontAsset fontAsset;

		// Token: 0x04000092 RID: 146
		public TextSpriteAsset spriteAsset;

		// Token: 0x04000093 RID: 147
		public Material material;

		// Token: 0x04000094 RID: 148
		public bool isDefaultMaterial;

		// Token: 0x04000095 RID: 149
		public bool isFallbackMaterial;

		// Token: 0x04000096 RID: 150
		public Material fallbackMaterial;

		// Token: 0x04000097 RID: 151
		public float padding;

		// Token: 0x04000098 RID: 152
		public int referenceCount;
	}
}
