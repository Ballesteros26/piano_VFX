using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000030 RID: 48
	public class ActivationControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000845C File Offset: 0x0000665C
		public static ScriptPlayable<ActivationControlPlayable> Create(PlayableGraph graph, GameObject gameObject, ActivationControlPlayable.PostPlaybackState postPlaybackState)
		{
			if (gameObject == null)
			{
				return ScriptPlayable<ActivationControlPlayable>.Null;
			}
			ScriptPlayable<ActivationControlPlayable> scriptPlayable = ScriptPlayable<ActivationControlPlayable>.Create(graph, 0);
			ActivationControlPlayable behaviour = scriptPlayable.GetBehaviour();
			behaviour.gameObject = gameObject;
			behaviour.postPlayback = postPlaybackState;
			return scriptPlayable;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008495 File Offset: 0x00006695
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.gameObject == null)
			{
				return;
			}
			this.gameObject.SetActive(true);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000084B2 File Offset: 0x000066B2
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.gameObject != null && info.effectivePlayState == PlayState.Paused)
			{
				this.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000084D7 File Offset: 0x000066D7
		public override void ProcessFrame(Playable playable, FrameData info, object userData)
		{
			if (this.gameObject != null)
			{
				this.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000084F3 File Offset: 0x000066F3
		public override void OnGraphStart(Playable playable)
		{
			if (this.gameObject != null && this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				this.m_InitialState = (this.gameObject.activeSelf ? ActivationControlPlayable.InitialState.Active : ActivationControlPlayable.InitialState.Inactive);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008524 File Offset: 0x00006724
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.gameObject == null || this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				return;
			}
			switch (this.postPlayback)
			{
			case ActivationControlPlayable.PostPlaybackState.Active:
				this.gameObject.SetActive(true);
				return;
			case ActivationControlPlayable.PostPlaybackState.Inactive:
				this.gameObject.SetActive(false);
				return;
			case ActivationControlPlayable.PostPlaybackState.Revert:
				this.gameObject.SetActive(this.m_InitialState == ActivationControlPlayable.InitialState.Active);
				return;
			default:
				return;
			}
		}

		// Token: 0x040000CE RID: 206
		public GameObject gameObject;

		// Token: 0x040000CF RID: 207
		public ActivationControlPlayable.PostPlaybackState postPlayback = ActivationControlPlayable.PostPlaybackState.Revert;

		// Token: 0x040000D0 RID: 208
		private ActivationControlPlayable.InitialState m_InitialState;

		// Token: 0x0200006D RID: 109
		public enum PostPlaybackState
		{
			// Token: 0x0400015A RID: 346
			Active,
			// Token: 0x0400015B RID: 347
			Inactive,
			// Token: 0x0400015C RID: 348
			Revert
		}

		// Token: 0x0200006E RID: 110
		private enum InitialState
		{
			// Token: 0x0400015E RID: 350
			Unset,
			// Token: 0x0400015F RID: 351
			Active,
			// Token: 0x04000160 RID: 352
			Inactive
		}
	}
}
