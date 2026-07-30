using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000032 RID: 50
	public class DirectorControlPlayable : PlayableBehaviour
	{
		// Token: 0x06000263 RID: 611 RVA: 0x000085B8 File Offset: 0x000067B8
		public static ScriptPlayable<DirectorControlPlayable> Create(PlayableGraph graph, PlayableDirector director)
		{
			if (director == null)
			{
				return ScriptPlayable<DirectorControlPlayable>.Null;
			}
			ScriptPlayable<DirectorControlPlayable> scriptPlayable = ScriptPlayable<DirectorControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().director = director;
			return scriptPlayable;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000085EA File Offset: 0x000067EA
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				this.director.Stop();
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008618 File Offset: 0x00006818
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			this.m_SyncTime |= info.evaluationType == FrameData.EvaluationType.Evaluate || this.DetectDiscontinuity(playable, info);
			this.SyncSpeed((double)info.effectiveSpeed);
			this.SyncPlayState(playable.GetGraph<Playable>(), playable.GetTime<Playable>());
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008695 File Offset: 0x00006895
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_SyncTime = true;
			if (this.director != null && this.director.playableAsset != null)
			{
				this.m_AssetDuration = this.director.playableAsset.duration;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000086D8 File Offset: 0x000068D8
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.director != null && this.director.playableAsset != null)
			{
				if (info.effectivePlayState == PlayState.Playing)
				{
					this.director.Pause();
					return;
				}
				this.director.Stop();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008728 File Offset: 0x00006928
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (this.director == null || !this.director.isActiveAndEnabled || this.director.playableAsset == null)
			{
				return;
			}
			if (this.m_SyncTime || this.DetectOutOfSync(playable))
			{
				this.UpdateTime(playable);
				this.director.Evaluate();
			}
			this.m_SyncTime = false;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008790 File Offset: 0x00006990
		private void SyncSpeed(double speed)
		{
			if (this.director.playableGraph.IsValid())
			{
				int rootPlayableCount = this.director.playableGraph.GetRootPlayableCount();
				for (int i = 0; i < rootPlayableCount; i++)
				{
					Playable rootPlayable = this.director.playableGraph.GetRootPlayable(i);
					if (rootPlayable.IsValid<Playable>())
					{
						rootPlayable.SetSpeed(speed);
					}
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000087F8 File Offset: 0x000069F8
		private void SyncPlayState(PlayableGraph graph, double playableTime)
		{
			bool flag = playableTime >= this.m_AssetDuration && this.director.extrapolationMode == DirectorWrapMode.None;
			if (graph.IsPlaying() && !flag)
			{
				this.director.Play();
				return;
			}
			this.director.Pause();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008843 File Offset: 0x00006A43
		private bool DetectDiscontinuity(Playable playable, FrameData info)
		{
			return Math.Abs(playable.GetTime<Playable>() - playable.GetPreviousTime<Playable>() - info.m_DeltaTime * (double)info.m_EffectiveSpeed) > DiscreteTime.tickValue;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008870 File Offset: 0x00006A70
		private bool DetectOutOfSync(Playable playable)
		{
			double num = playable.GetTime<Playable>();
			if (playable.GetTime<Playable>() >= this.m_AssetDuration)
			{
				if (this.director.extrapolationMode == DirectorWrapMode.None)
				{
					return false;
				}
				if (this.director.extrapolationMode == DirectorWrapMode.Hold)
				{
					num = this.m_AssetDuration;
				}
				else if (this.m_AssetDuration > 1.401298464324817E-45)
				{
					num %= this.m_AssetDuration;
				}
			}
			return !Mathf.Approximately((float)num, (float)this.director.time);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000088EC File Offset: 0x00006AEC
		private void UpdateTime(Playable playable)
		{
			double num = Math.Max(0.1, this.director.playableAsset.duration);
			switch (this.director.extrapolationMode)
			{
			case DirectorWrapMode.Hold:
				this.director.time = Math.Min(num, Math.Max(0.0, playable.GetTime<Playable>()));
				return;
			case DirectorWrapMode.Loop:
				this.director.time = Math.Max(0.0, playable.GetTime<Playable>() % num);
				return;
			case DirectorWrapMode.None:
				this.director.time = playable.GetTime<Playable>();
				return;
			default:
				return;
			}
		}

		// Token: 0x040000D1 RID: 209
		public PlayableDirector director;

		// Token: 0x040000D2 RID: 210
		private bool m_SyncTime;

		// Token: 0x040000D3 RID: 211
		private double m_AssetDuration = double.MaxValue;
	}
}
