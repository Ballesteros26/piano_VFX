using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200039C RID: 924
	[RequiredByNativeCode]
	public struct Playable : IPlayable, IEquatable<Playable>
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x000365D0 File Offset: 0x000347D0
		public static Playable Null
		{
			get
			{
				return Playable.m_NullPlayable;
			}
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000365E8 File Offset: 0x000347E8
		public static Playable Create(PlayableGraph graph, int inputCount = 0)
		{
			Playable playable = new Playable(graph.CreatePlayableHandle());
			playable.SetInputCount(inputCount);
			return playable;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00036611 File Offset: 0x00034811
		[VisibleToOtherModules]
		internal Playable(PlayableHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0003661C File Offset: 0x0003481C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00036634 File Offset: 0x00034834
		public bool IsPlayableOfType<T>() where T : struct, IPlayable
		{
			return this.GetHandle().IsPlayableOfType<T>();
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00036654 File Offset: 0x00034854
		public Type GetPlayableType()
		{
			return this.GetHandle().GetPlayableType();
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00036674 File Offset: 0x00034874
		public bool Equals(Playable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000B90 RID: 2960
		private PlayableHandle m_Handle;

		// Token: 0x04000B91 RID: 2961
		private static readonly Playable m_NullPlayable = new Playable(PlayableHandle.Null);
	}
}
