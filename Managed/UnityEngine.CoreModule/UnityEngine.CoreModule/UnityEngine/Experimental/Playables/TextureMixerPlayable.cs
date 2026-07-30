using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003C9 RID: 969
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h")]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		// Token: 0x060021B5 RID: 8629 RVA: 0x00039364 File Offset: 0x00037564
		public static TextureMixerPlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = TextureMixerPlayable.CreateHandle(graph);
			return new TextureMixerPlayable(playableHandle);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00039384 File Offset: 0x00037584
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !TextureMixerPlayable.CreateTextureMixerPlayableInternal(ref graph, ref @null);
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

		// Token: 0x060021B7 RID: 8631 RVA: 0x000393B8 File Offset: 0x000375B8
		internal TextureMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<TextureMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TextureMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000393F4 File Offset: 0x000375F4
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0003940C File Offset: 0x0003760C
		public static implicit operator Playable(TextureMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0003942C File Offset: 0x0003762C
		public static explicit operator TextureMixerPlayable(Playable playable)
		{
			return new TextureMixerPlayable(playable.GetHandle());
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0003944C File Offset: 0x0003764C
		public bool Equals(TextureMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060021BC RID: 8636
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool CreateTextureMixerPlayableInternal(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000C45 RID: 3141
		private PlayableHandle m_Handle;
	}
}
