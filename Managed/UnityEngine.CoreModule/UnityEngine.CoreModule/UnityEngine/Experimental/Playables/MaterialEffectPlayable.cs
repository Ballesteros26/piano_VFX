using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003C8 RID: 968
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h")]
	[StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h")]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		// Token: 0x060021A4 RID: 8612 RVA: 0x000391F4 File Offset: 0x000373F4
		public static MaterialEffectPlayable Create(PlayableGraph graph, Material material, int pass = -1)
		{
			PlayableHandle playableHandle = MaterialEffectPlayable.CreateHandle(graph, material, pass);
			return new MaterialEffectPlayable(playableHandle);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00039218 File Offset: 0x00037418
		private static PlayableHandle CreateHandle(PlayableGraph graph, Material material, int pass)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !MaterialEffectPlayable.InternalCreateMaterialEffectPlayable(ref graph, material, pass, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0003924C File Offset: 0x0003744C
		internal MaterialEffectPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<MaterialEffectPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an MaterialEffectPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00039288 File Offset: 0x00037488
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000392A0 File Offset: 0x000374A0
		public static implicit operator Playable(MaterialEffectPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000392C0 File Offset: 0x000374C0
		public static explicit operator MaterialEffectPlayable(Playable playable)
		{
			return new MaterialEffectPlayable(playable.GetHandle());
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000392E0 File Offset: 0x000374E0
		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00039304 File Offset: 0x00037504
		public Material GetMaterial()
		{
			return MaterialEffectPlayable.GetMaterialInternal(ref this.m_Handle);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x00039321 File Offset: 0x00037521
		public void SetMaterial(Material value)
		{
			MaterialEffectPlayable.SetMaterialInternal(ref this.m_Handle, value);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00039334 File Offset: 0x00037534
		public int GetPass()
		{
			return MaterialEffectPlayable.GetPassInternal(ref this.m_Handle);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00039351 File Offset: 0x00037551
		public void SetPass(int value)
		{
			MaterialEffectPlayable.SetPassInternal(ref this.m_Handle, value);
		}

		// Token: 0x060021AF RID: 8623
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern Material GetMaterialInternal(ref PlayableHandle hdl);

		// Token: 0x060021B0 RID: 8624
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMaterialInternal(ref PlayableHandle hdl, Material material);

		// Token: 0x060021B1 RID: 8625
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetPassInternal(ref PlayableHandle hdl);

		// Token: 0x060021B2 RID: 8626
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetPassInternal(ref PlayableHandle hdl, int pass);

		// Token: 0x060021B3 RID: 8627
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalCreateMaterialEffectPlayable(ref PlayableGraph graph, Material material, int pass, ref PlayableHandle handle);

		// Token: 0x060021B4 RID: 8628
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000C44 RID: 3140
		private PlayableHandle m_Handle;
	}
}
