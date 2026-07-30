using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005E RID: 94
	internal static class MeshUtilities
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000E35C File Offset: 0x0000C55C
		internal static Mesh GetColliderMesh(Collider collider)
		{
			Type type = collider.GetType();
			if (type == typeof(MeshCollider))
			{
				return ((MeshCollider)collider).sharedMesh;
			}
			return MeshUtilities.GetPrimitive(MeshUtilities.s_ColliderPrimitives[type]);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		internal static Mesh GetPrimitive(PrimitiveType primitiveType)
		{
			Mesh builtinMesh;
			if (!MeshUtilities.s_Primitives.TryGetValue(primitiveType, out builtinMesh))
			{
				builtinMesh = MeshUtilities.GetBuiltinMesh(primitiveType);
				MeshUtilities.s_Primitives.Add(primitiveType, builtinMesh);
			}
			return builtinMesh;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		private static Mesh GetBuiltinMesh(PrimitiveType primitiveType)
		{
			GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
			Mesh sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
			RuntimeUtilities.Destroy(gameObject);
			return sharedMesh;
		}

		// Token: 0x0400019C RID: 412
		private static Dictionary<PrimitiveType, Mesh> s_Primitives = new Dictionary<PrimitiveType, Mesh>();

		// Token: 0x0400019D RID: 413
		private static Dictionary<Type, PrimitiveType> s_ColliderPrimitives = new Dictionary<Type, PrimitiveType>
		{
			{
				typeof(BoxCollider),
				PrimitiveType.Cube
			},
			{
				typeof(SphereCollider),
				PrimitiveType.Sphere
			},
			{
				typeof(CapsuleCollider),
				PrimitiveType.Capsule
			}
		};
	}
}
