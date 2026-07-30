using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class AudioPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006293 File Offset: 0x00004493
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000629B File Offset: 0x0000449B
		internal float bufferingTime
		{
			get
			{
				return this.m_bufferingTime;
			}
			set
			{
				this.m_bufferingTime = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000062A4 File Offset: 0x000044A4
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000062AC File Offset: 0x000044AC
		public AudioClip clip
		{
			get
			{
				return this.m_Clip;
			}
			set
			{
				this.m_Clip = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000062B5 File Offset: 0x000044B5
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000062BD File Offset: 0x000044BD
		public bool loop
		{
			get
			{
				return this.m_Loop;
			}
			set
			{
				this.m_Loop = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000062C6 File Offset: 0x000044C6
		public override double duration
		{
			get
			{
				if (this.m_Clip == null)
				{
					return base.duration;
				}
				return (double)this.m_Clip.samples / (double)this.m_Clip.frequency;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000062F6 File Offset: 0x000044F6
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006308 File Offset: 0x00004508
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.m_Clip == null)
			{
				return Playable.Null;
			}
			AudioClipPlayable audioClipPlayable = AudioClipPlayable.Create(graph, this.m_Clip, this.m_Loop);
			audioClipPlayable.GetHandle().SetScriptInstance(this.m_ClipProperties.Clone());
			return audioClipPlayable;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000635C File Offset: 0x0000455C
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Blending | (this.m_Loop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x04000089 RID: 137
		[SerializeField]
		private AudioClip m_Clip;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private bool m_Loop;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		[HideInInspector]
		private float m_bufferingTime = 0.1f;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private AudioClipProperties m_ClipProperties = new AudioClipProperties();
	}
}
