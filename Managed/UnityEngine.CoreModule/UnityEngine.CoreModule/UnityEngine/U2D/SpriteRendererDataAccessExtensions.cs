using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x0200020E RID: 526
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	public static class SpriteRendererDataAccessExtensions
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x00025F08 File Offset: 0x00024108
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<byte> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("spriteRenderer does not have a valid sprite set.", new object[0]));
			}
			bool flag2 = src.Length != SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite);
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("custom sprite vertex data size must match sprite asset's vertex data size {0} {1}", src.Length, SpriteDataAccessExtensions.GetPrimaryVertexStreamSize(spriteRenderer.sprite)));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<byte>(), src.Length);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00025F98 File Offset: 0x00024198
		internal static void SetDeformableBuffer(this SpriteRenderer spriteRenderer, NativeArray<Vector3> src)
		{
			bool flag = spriteRenderer.sprite == null;
			if (flag)
			{
				throw new InvalidOperationException("spriteRenderer does not have a valid sprite set.");
			}
			bool flag2 = src.Length != spriteRenderer.sprite.GetVertexCount();
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The src length {0} must match the vertex count of source Sprite {1}.", src.Length, spriteRenderer.sprite.GetVertexCount()));
			}
			SpriteRendererDataAccessExtensions.SetDeformableBuffer(spriteRenderer, src.GetUnsafeReadOnlyPtr<Vector3>(), src.Length);
		}

		// Token: 0x06001764 RID: 5988
		[MethodImpl(4096)]
		public static extern void DeactivateDeformableBuffer([NotNull] this SpriteRenderer renderer);

		// Token: 0x06001765 RID: 5989 RVA: 0x0002601C File Offset: 0x0002421C
		internal static void SetLocalAABB([NotNull] this SpriteRenderer renderer, Bounds aabb)
		{
			SpriteRendererDataAccessExtensions.SetLocalAABB_Injected(renderer, ref aabb);
		}

		// Token: 0x06001766 RID: 5990
		[MethodImpl(4096)]
		private unsafe static extern void SetDeformableBuffer([NotNull] SpriteRenderer spriteRenderer, void* src, int count);

		// Token: 0x06001767 RID: 5991
		[MethodImpl(4096)]
		private static extern void SetLocalAABB_Injected(SpriteRenderer renderer, ref Bounds aabb);
	}
}
