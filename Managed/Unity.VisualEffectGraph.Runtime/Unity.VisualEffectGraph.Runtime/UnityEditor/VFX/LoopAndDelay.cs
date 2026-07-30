using System;
using UnityEngine;
using UnityEngine.VFX;

namespace UnityEditor.VFX
{
	// Token: 0x02000026 RID: 38
	internal class LoopAndDelay : VFXSpawnerCallbacks
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004A11 File Offset: 0x00002C11
		public sealed override void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			this.m_LoopMaxCount = vfxValues.GetInt(LoopAndDelay.loopCountPropertyID);
			this.m_WaitingForTotalTime = vfxValues.GetFloat(LoopAndDelay.loopDurationPropertyID);
			this.m_LoopCurrentIndex = 0;
			if (this.m_LoopMaxCount == this.m_LoopCurrentIndex)
			{
				state.playing = false;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A54 File Offset: 0x00002C54
		public sealed override void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			if (this.m_LoopCurrentIndex != this.m_LoopMaxCount && state.totalTime > this.m_WaitingForTotalTime)
			{
				if (state.playing)
				{
					this.m_WaitingForTotalTime = state.totalTime + vfxValues.GetFloat(LoopAndDelay.delayPropertyID);
					state.playing = false;
					this.m_LoopCurrentIndex = ((this.m_LoopCurrentIndex + 1 > 0) ? (this.m_LoopCurrentIndex + 1) : 0);
					return;
				}
				this.m_WaitingForTotalTime = vfxValues.GetFloat(LoopAndDelay.loopDurationPropertyID);
				state.totalTime = 0f;
				state.playing = true;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public sealed override void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent)
		{
			this.m_LoopCurrentIndex = this.m_LoopMaxCount;
		}

		// Token: 0x04000094 RID: 148
		private int m_LoopMaxCount;

		// Token: 0x04000095 RID: 149
		private int m_LoopCurrentIndex;

		// Token: 0x04000096 RID: 150
		private float m_WaitingForTotalTime;

		// Token: 0x04000097 RID: 151
		private static readonly int loopCountPropertyID = Shader.PropertyToID("LoopCount");

		// Token: 0x04000098 RID: 152
		private static readonly int loopDurationPropertyID = Shader.PropertyToID("LoopDuration");

		// Token: 0x04000099 RID: 153
		private static readonly int delayPropertyID = Shader.PropertyToID("Delay");

		// Token: 0x02000035 RID: 53
		public class InputProperties
		{
			// Token: 0x040000D2 RID: 210
			[Tooltip("Number of Loops (< 0 for infinite), evaluated when Context Start is hit")]
			public int LoopCount = 1;

			// Token: 0x040000D3 RID: 211
			[Tooltip("Duration of one loop, evaluated every loop")]
			public float LoopDuration = 4f;

			// Token: 0x040000D4 RID: 212
			[Tooltip("Duration of in-between delay (after each loop), evaluated every loop")]
			public float Delay = 1f;
		}
	}
}
