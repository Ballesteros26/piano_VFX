using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020003AE RID: 942
	[RequiredByNativeCode]
	public struct ScriptPlayableOutput : IPlayableOutput
	{
		// Token: 0x06002153 RID: 8531 RVA: 0x00037F38 File Offset: 0x00036138
		public static ScriptPlayableOutput Create(PlayableGraph graph, string name)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !graph.CreateScriptOutputInternal(name, out playableOutputHandle);
			ScriptPlayableOutput scriptPlayableOutput;
			if (flag)
			{
				scriptPlayableOutput = ScriptPlayableOutput.Null;
			}
			else
			{
				scriptPlayableOutput = new ScriptPlayableOutput(playableOutputHandle);
			}
			return scriptPlayableOutput;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00037F6C File Offset: 0x0003616C
		internal ScriptPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<ScriptPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a ScriptPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x00037FA8 File Offset: 0x000361A8
		public static ScriptPlayableOutput Null
		{
			get
			{
				return new ScriptPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00037FC4 File Offset: 0x000361C4
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00037FDC File Offset: 0x000361DC
		public static implicit operator PlayableOutput(ScriptPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00037FFC File Offset: 0x000361FC
		public static explicit operator ScriptPlayableOutput(PlayableOutput output)
		{
			return new ScriptPlayableOutput(output.GetHandle());
		}

		// Token: 0x04000BB5 RID: 2997
		private PlayableOutputHandle m_Handle;
	}
}
