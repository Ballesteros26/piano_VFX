using System;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000022 RID: 34
	internal class ScheduleRuntimeClip : RuntimeClipBase
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007A5A File Offset: 0x00005C5A
		public override double start
		{
			get
			{
				return Math.Max(0.0, this.m_Clip.start - this.m_StartDelay);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007A7C File Offset: 0x00005C7C
		public override double duration
		{
			get
			{
				return this.m_Clip.duration + this.m_FinishTail + this.m_Clip.start - this.start;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007AA3 File Offset: 0x00005CA3
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00007AB1 File Offset: 0x00005CB1
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007AB9 File Offset: 0x00005CB9
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007AC1 File Offset: 0x00005CC1
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public ScheduleRuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay = 0.2, double finishTail = 0.1)
		{
			this.Create(clip, clipPlayable, parentMixer, startDelay, finishTail);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007ADE File Offset: 0x00005CDE
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay, double finishTail)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			this.m_StartDelay = startDelay;
			this.m_FinishTail = finishTail;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00007B0C File Offset: 0x00005D0C
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
				}
				else if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
				this.m_Started = this.m_Started && value;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007B84 File Offset: 0x00005D84
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			if (frameData.timeHeld)
			{
				this.enable = false;
				return;
			}
			bool flag = frameData.seekOccurred || frameData.timeLooped || frameData.evaluationType == FrameData.EvaluationType.Evaluate;
			if (localTime > this.start + this.duration - this.m_FinishTail)
			{
				return;
			}
			float num = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, num);
			}
			if (!this.m_Started || flag)
			{
				double num2 = this.clip.ToLocalTime(Math.Max(localTime, this.clip.start));
				double num3 = Math.Max(this.clip.start - localTime, 0.0) * this.clip.timeScale;
				double num4 = this.m_Clip.duration * this.clip.timeScale;
				if (this.m_Playable.IsPlayableOfType<AudioClipPlayable>())
				{
					((AudioClipPlayable)this.m_Playable).Seek(num2, num3, num4);
				}
				this.m_Started = true;
			}
		}

		// Token: 0x040000BD RID: 189
		private TimelineClip m_Clip;

		// Token: 0x040000BE RID: 190
		private Playable m_Playable;

		// Token: 0x040000BF RID: 191
		private Playable m_ParentMixer;

		// Token: 0x040000C0 RID: 192
		private double m_StartDelay;

		// Token: 0x040000C1 RID: 193
		private double m_FinishTail;

		// Token: 0x040000C2 RID: 194
		private bool m_Started;
	}
}
