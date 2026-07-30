using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000392 RID: 914
	public struct FrameData
	{
		// Token: 0x06001FE4 RID: 8164 RVA: 0x00036424 File Offset: 0x00034624
		private bool HasFlags(FrameData.Flags flag)
		{
			return (this.m_Flags & flag) == flag;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00036444 File Offset: 0x00034644
		public ulong frameId
		{
			get
			{
				return this.m_FrameID;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0003645C File Offset: 0x0003465C
		public float deltaTime
		{
			get
			{
				return (float)this.m_DeltaTime;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00036478 File Offset: 0x00034678
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00036490 File Offset: 0x00034690
		public float effectiveWeight
		{
			get
			{
				return this.m_EffectiveWeight;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000364A8 File Offset: 0x000346A8
		[Obsolete("effectiveParentDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public double effectiveParentDelay
		{
			get
			{
				return this.m_EffectiveParentDelay;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x000364C0 File Offset: 0x000346C0
		public float effectiveParentSpeed
		{
			get
			{
				return this.m_EffectiveParentSpeed;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000364D8 File Offset: 0x000346D8
		public float effectiveSpeed
		{
			get
			{
				return this.m_EffectiveSpeed;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x000364F0 File Offset: 0x000346F0
		public FrameData.EvaluationType evaluationType
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Evaluate) ? FrameData.EvaluationType.Evaluate : FrameData.EvaluationType.Playback;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00036510 File Offset: 0x00034710
		public bool seekOccurred
		{
			get
			{
				return this.HasFlags(FrameData.Flags.SeekOccured);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0003652C File Offset: 0x0003472C
		public bool timeLooped
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Loop);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00036548 File Offset: 0x00034748
		public bool timeHeld
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Hold);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00036564 File Offset: 0x00034764
		public PlayableOutput output
		{
			get
			{
				return this.m_Output;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0003657C File Offset: 0x0003477C
		public PlayState effectivePlayState
		{
			get
			{
				bool flag = this.HasFlags(FrameData.Flags.EffectivePlayStateDelayed);
				PlayState playState;
				if (flag)
				{
					playState = PlayState.Delayed;
				}
				else
				{
					bool flag2 = this.HasFlags(FrameData.Flags.EffectivePlayStatePlaying);
					if (flag2)
					{
						playState = PlayState.Playing;
					}
					else
					{
						playState = PlayState.Paused;
					}
				}
				return playState;
			}
		}

		// Token: 0x04000B78 RID: 2936
		internal ulong m_FrameID;

		// Token: 0x04000B79 RID: 2937
		internal double m_DeltaTime;

		// Token: 0x04000B7A RID: 2938
		internal float m_Weight;

		// Token: 0x04000B7B RID: 2939
		internal float m_EffectiveWeight;

		// Token: 0x04000B7C RID: 2940
		internal double m_EffectiveParentDelay;

		// Token: 0x04000B7D RID: 2941
		internal float m_EffectiveParentSpeed;

		// Token: 0x04000B7E RID: 2942
		internal float m_EffectiveSpeed;

		// Token: 0x04000B7F RID: 2943
		internal FrameData.Flags m_Flags;

		// Token: 0x04000B80 RID: 2944
		internal PlayableOutput m_Output;

		// Token: 0x02000393 RID: 915
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000B82 RID: 2946
			Evaluate = 1,
			// Token: 0x04000B83 RID: 2947
			SeekOccured = 2,
			// Token: 0x04000B84 RID: 2948
			Loop = 4,
			// Token: 0x04000B85 RID: 2949
			Hold = 8,
			// Token: 0x04000B86 RID: 2950
			EffectivePlayStateDelayed = 16,
			// Token: 0x04000B87 RID: 2951
			EffectivePlayStatePlaying = 32
		}

		// Token: 0x02000394 RID: 916
		public enum EvaluationType
		{
			// Token: 0x04000B89 RID: 2953
			Evaluate,
			// Token: 0x04000B8A RID: 2954
			Playback
		}
	}
}
