using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020003A9 RID: 937
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00037684 File Offset: 0x00035884
		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0003769B File Offset: 0x0003589B
		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000376A8 File Offset: 0x000358A8
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000376C0 File Offset: 0x000358C0
		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000376E0 File Offset: 0x000358E0
		public Type GetPlayableOutputType()
		{
			return this.GetHandle().GetPlayableOutputType();
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00037700 File Offset: 0x00035900
		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000BAE RID: 2990
		private PlayableOutputHandle m_Handle;

		// Token: 0x04000BAF RID: 2991
		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);
	}
}
