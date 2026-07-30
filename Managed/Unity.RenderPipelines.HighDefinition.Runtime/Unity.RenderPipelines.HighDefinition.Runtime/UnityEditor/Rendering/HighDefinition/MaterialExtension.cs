using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEditor.Rendering.HighDefinition
{
	// Token: 0x0200000F RID: 15
	internal static class MaterialExtension
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static SurfaceType GetSurfaceType(this Material material)
		{
			if (!material.HasProperty("_SurfaceType"))
			{
				return SurfaceType.Opaque;
			}
			return (SurfaceType)material.GetFloat("_SurfaceType");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		public static MaterialId GetMaterialId(this Material material)
		{
			if (!material.HasProperty("_MaterialID"))
			{
				return MaterialId.LitStandard;
			}
			return (MaterialId)material.GetFloat("_MaterialID");
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208A File Offset: 0x0000028A
		public static BlendMode GetBlendMode(this Material material)
		{
			if (!material.HasProperty("_BlendMode"))
			{
				return BlendMode.Additive;
			}
			return (BlendMode)material.GetFloat("_BlendMode");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A7 File Offset: 0x000002A7
		public static int GetLayerCount(this Material material)
		{
			if (!material.HasProperty("_LayerCount"))
			{
				return 1;
			}
			return material.GetInt("_LayerCount");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C3 File Offset: 0x000002C3
		public static bool GetZWrite(this Material material)
		{
			return material.HasProperty("_ZWrite") && material.GetInt("_ZWrite") == 1;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E2 File Offset: 0x000002E2
		public static bool GetTransparentZWrite(this Material material)
		{
			return material.HasProperty("_TransparentZWrite") && material.GetInt("_TransparentZWrite") == 1;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public static CullMode GetTransparentCullMode(this Material material)
		{
			if (!material.HasProperty("_TransparentCullMode"))
			{
				return CullMode.Back;
			}
			return (CullMode)material.GetInt("_TransparentCullMode");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211D File Offset: 0x0000031D
		public static CompareFunction GetTransparentZTest(this Material material)
		{
			if (!material.HasProperty("_ZTestTransparent"))
			{
				return CompareFunction.LessEqual;
			}
			return (CompareFunction)material.GetInt("_ZTestTransparent");
		}
	}
}
