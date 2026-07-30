using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001F RID: 31
	internal class RuntimeClip : RuntimeClipBase
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007830 File Offset: 0x00005A30
		public override double start
		{
			get
			{
				return this.m_Clip.extrapolatedStart;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000783D File Offset: 0x00005A3D
		public override double duration
		{
			get
			{
				return this.m_Clip.extrapolatedDuration;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000784A File Offset: 0x00005A4A
		public RuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.Create(clip, clipPlayable, parentMixer);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000785B File Offset: 0x00005A5B
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007878 File Offset: 0x00005A78
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007880 File Offset: 0x00005A80
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007888 File Offset: 0x00005A88
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x17000096 RID: 150
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00007890 File Offset: 0x00005A90
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
					this.SetTime(this.m_Clip.clipIn);
					return;
				}
				if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007909 File Offset: 0x00005B09
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007917 File Offset: 0x00005B17
		public void SetDuration(double duration)
		{
			this.m_Playable.SetDuration(duration);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007928 File Offset: 0x00005B28
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.enable = true;
			float num;
			if (this.clip.IsPreExtrapolatedTime(localTime))
			{
				num = this.clip.EvaluateMixIn((double)((float)this.clip.start));
			}
			else if (this.clip.IsPostExtrapolatedTime(localTime))
			{
				num = this.clip.EvaluateMixOut((double)((float)this.clip.end));
			}
			else
			{
				num = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			}
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, num);
			}
			double num2 = this.clip.ToLocalTime(localTime);
			if (num2.CompareTo(0.0) >= 0)
			{
				this.SetTime(num2);
			}
			this.SetDuration(this.clip.extrapolatedDuration);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007A05 File Offset: 0x00005C05
		public override void Reset()
		{
			this.SetTime(this.m_Clip.clipIn);
		}

		// Token: 0x040000B9 RID: 185
		private TimelineClip m_Clip;

		// Token: 0x040000BA RID: 186
		private Playable m_Playable;

		// Token: 0x040000BB RID: 187
		private Playable m_ParentMixer;
	}
}
