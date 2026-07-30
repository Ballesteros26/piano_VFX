using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class TimelineAsset : PlayableAsset, ISerializationCallbackReceiver, ITimelineClipAsset, IPropertyPreview
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000028DC File Offset: 0x00000ADC
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003FE0 File Offset: 0x000021E0
		public TimelineAsset.EditorSettings editorSettings
		{
			get
			{
				return this.m_EditorSettings;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003FE8 File Offset: 0x000021E8
		public override double duration
		{
			get
			{
				if (this.m_DurationMode == TimelineAsset.DurationMode.BasedOnClips)
				{
					return this.CalculateDuration();
				}
				return this.m_FixedDuration;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004000 File Offset: 0x00002200
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000403E File Offset: 0x0000223E
		public double fixedDuration
		{
			get
			{
				DiscreteTime discreteTime = (DiscreteTime)this.m_FixedDuration;
				if (discreteTime <= 0)
				{
					return 0.0;
				}
				return (double)discreteTime.OneTickBefore();
			}
			set
			{
				this.m_FixedDuration = Math.Max(0.0, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004055 File Offset: 0x00002255
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000405D File Offset: 0x0000225D
		public TimelineAsset.DurationMode durationMode
		{
			get
			{
				return this.m_DurationMode;
			}
			set
			{
				this.m_DurationMode = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004066 File Offset: 0x00002266
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				foreach (TrackAsset trackAsset in this.GetOutputTracks())
				{
					foreach (PlayableBinding playableBinding in trackAsset.outputs)
					{
						yield return playableBinding;
					}
					IEnumerator<PlayableBinding> enumerator2 = null;
				}
				IEnumerator<TrackAsset> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004078 File Offset: 0x00002278
		public ClipCaps clipCaps
		{
			get
			{
				ClipCaps clipCaps = ClipCaps.All;
				foreach (TrackAsset trackAsset in this.GetRootTracks())
				{
					foreach (TimelineClip timelineClip in trackAsset.clips)
					{
						clipCaps &= timelineClip.clipCaps;
					}
				}
				return clipCaps;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000040E4 File Offset: 0x000022E4
		public int outputTrackCount
		{
			get
			{
				this.UpdateOutputTrackCache();
				return this.m_CacheOutputTracks.Length;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000040F4 File Offset: 0x000022F4
		public int rootTrackCount
		{
			get
			{
				this.UpdateRootTrackCache();
				return this.m_CacheRootTracks.Count;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004107 File Offset: 0x00002307
		private void OnValidate()
		{
			this.editorSettings.fps = TimelineAsset.GetValidFramerate(this.editorSettings.fps);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004124 File Offset: 0x00002324
		private static float GetValidFramerate(float framerate)
		{
			return Mathf.Clamp(framerate, TimelineAsset.EditorSettings.kMinFps, TimelineAsset.EditorSettings.kMaxFps);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004136 File Offset: 0x00002336
		public TrackAsset GetRootTrack(int index)
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks[index];
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000414A File Offset: 0x0000234A
		public IEnumerable<TrackAsset> GetRootTracks()
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004158 File Offset: 0x00002358
		public TrackAsset GetOutputTrack(int index)
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks[index];
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004168 File Offset: 0x00002368
		public IEnumerable<TrackAsset> GetOutputTracks()
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004178 File Offset: 0x00002378
		private void UpdateRootTrackCache()
		{
			if (this.m_CacheRootTracks == null)
			{
				if (this.m_Tracks == null)
				{
					this.m_CacheRootTracks = new List<TrackAsset>();
					return;
				}
				this.m_CacheRootTracks = new List<TrackAsset>(this.m_Tracks.Count);
				if (this.markerTrack != null)
				{
					this.m_CacheRootTracks.Add(this.markerTrack);
				}
				foreach (ScriptableObject scriptableObject in this.m_Tracks)
				{
					TrackAsset trackAsset = scriptableObject as TrackAsset;
					if (trackAsset != null)
					{
						this.m_CacheRootTracks.Add(trackAsset);
					}
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004234 File Offset: 0x00002434
		private void UpdateOutputTrackCache()
		{
			if (this.m_CacheOutputTracks == null)
			{
				List<TrackAsset> list = new List<TrackAsset>();
				foreach (TrackAsset trackAsset in this.flattenedTracks)
				{
					if (trackAsset != null && trackAsset.GetType() != typeof(GroupTrack) && !trackAsset.isSubTrack)
					{
						list.Add(trackAsset);
					}
				}
				this.m_CacheOutputTracks = list.ToArray();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000042C4 File Offset: 0x000024C4
		internal IEnumerable<TrackAsset> flattenedTracks
		{
			get
			{
				if (this.m_CacheFlattenedTracks == null)
				{
					this.m_CacheFlattenedTracks = new List<TrackAsset>(this.m_Tracks.Count * 2);
					this.UpdateRootTrackCache();
					this.m_CacheFlattenedTracks.AddRange(this.m_CacheRootTracks);
					for (int i = 0; i < this.m_CacheRootTracks.Count; i++)
					{
						TimelineAsset.AddSubTracksRecursive(this.m_CacheRootTracks[i], ref this.m_CacheFlattenedTracks);
					}
				}
				return this.m_CacheFlattenedTracks;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000433B File Offset: 0x0000253B
		public MarkerTrack markerTrack
		{
			get
			{
				return this.m_MarkerTrack;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004343 File Offset: 0x00002543
		internal List<ScriptableObject> trackObjects
		{
			get
			{
				return this.m_Tracks;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000434B File Offset: 0x0000254B
		internal void AddTrackInternal(TrackAsset track)
		{
			this.m_Tracks.Add(track);
			track.parent = this;
			this.Invalidate();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004368 File Offset: 0x00002568
		internal void RemoveTrack(TrackAsset track)
		{
			this.m_Tracks.Remove(track);
			this.Invalidate();
			TrackAsset trackAsset = track.parent as TrackAsset;
			if (trackAsset != null)
			{
				trackAsset.RemoveSubTrack(track);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000043A8 File Offset: 0x000025A8
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			bool flag = false;
			bool flag2 = graph.GetPlayableCount() == 0;
			ScriptPlayable<TimelinePlayable> scriptPlayable = TimelinePlayable.Create(graph, this.GetOutputTracks(), go, flag, flag2);
			scriptPlayable.SetPropagateSetTime(true);
			if (!scriptPlayable.IsValid<ScriptPlayable<TimelinePlayable>>())
			{
				return Playable.Null;
			}
			return scriptPlayable;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000043ED File Offset: 0x000025ED
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 0;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000043F6 File Offset: 0x000025F6
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.Invalidate();
			if (this.m_Version < 0)
			{
				this.UpgradeToLatestVersion();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004410 File Offset: 0x00002610
		private void __internalAwake()
		{
			if (this.m_Tracks == null)
			{
				this.m_Tracks = new List<ScriptableObject>();
			}
			for (int i = this.m_Tracks.Count - 1; i >= 0; i--)
			{
				TrackAsset trackAsset = this.m_Tracks[i] as TrackAsset;
				if (trackAsset != null)
				{
					trackAsset.parent = this;
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000446C File Offset: 0x0000266C
		public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			foreach (TrackAsset trackAsset in this.GetOutputTracks())
			{
				trackAsset.GatherProperties(director, driver);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000044B8 File Offset: 0x000026B8
		public void CreateMarkerTrack()
		{
			if (this.m_MarkerTrack == null)
			{
				this.m_MarkerTrack = ScriptableObject.CreateInstance<MarkerTrack>();
				TimelineCreateUtilities.SaveAssetIntoObject(this.m_MarkerTrack, this);
				this.m_MarkerTrack.parent = this;
				this.m_MarkerTrack.name = "Markers";
				this.Invalidate();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000450C File Offset: 0x0000270C
		internal void Invalidate()
		{
			this.m_CacheRootTracks = null;
			this.m_CacheOutputTracks = null;
			this.m_CacheFlattenedTracks = null;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004524 File Offset: 0x00002724
		private double CalculateDuration()
		{
			DiscreteTime discreteTime = new DiscreteTime(0);
			foreach (TrackAsset trackAsset in this.flattenedTracks)
			{
				if (!trackAsset.muted)
				{
					discreteTime = DiscreteTime.Max(discreteTime, (DiscreteTime)trackAsset.end);
				}
			}
			if (discreteTime <= 0)
			{
				return 0.0;
			}
			return (double)discreteTime.OneTickBefore();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000045B4 File Offset: 0x000027B4
		private static void AddSubTracksRecursive(TrackAsset track, ref List<TrackAsset> allTracks)
		{
			if (track == null)
			{
				return;
			}
			allTracks.AddRange(track.GetChildTracks());
			foreach (TrackAsset trackAsset in track.GetChildTracks())
			{
				TimelineAsset.AddSubTracksRecursive(trackAsset, ref allTracks);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004618 File Offset: 0x00002818
		public TrackAsset CreateTrack(Type type, TrackAsset parent, string name)
		{
			if (parent != null && parent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			if (parent != null && !TimelineCreateUtilities.ValidateParentTrack(parent, type))
			{
				throw new InvalidOperationException("Cannot assign a child of type " + type.Name + " to a parent of type " + parent.GetType().Name);
			}
			PlayableAsset playableAsset = ((parent != null) ? parent : this);
			string text = name;
			if (string.IsNullOrEmpty(text))
			{
				text = type.Name;
			}
			string text2;
			if (parent != null)
			{
				text2 = TimelineCreateUtilities.GenerateUniqueActorName(parent.subTracksObjects, text);
			}
			else
			{
				text2 = TimelineCreateUtilities.GenerateUniqueActorName(this.trackObjects, text);
			}
			TrackAsset trackAsset = this.AllocateTrack(parent, text2, type);
			if (trackAsset != null)
			{
				trackAsset.name = text2;
				TimelineCreateUtilities.SaveAssetIntoObject(trackAsset, playableAsset);
			}
			return trackAsset;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004704 File Offset: 0x00002904
		public T CreateTrack<T>(TrackAsset parent, string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), parent, trackName));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000471D File Offset: 0x0000291D
		public T CreateTrack<T>(string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, trackName));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004736 File Offset: 0x00002936
		public T CreateTrack<T>() where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, null));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004750 File Offset: 0x00002950
		public bool DeleteClip(TimelineClip clip)
		{
			if (clip == null || clip.parentTrack == null)
			{
				return false;
			}
			if (this != clip.parentTrack.timelineAsset)
			{
				Debug.LogError("Cannot delete a clip from this timeline");
				return false;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.parentTrack, clip.curves, "Delete Curves");
			}
			if (clip.asset != null)
			{
				this.DeleteRecordedAnimation(clip);
				TimelineUndo.PushDestroyUndo(this, clip.parentTrack, clip.asset, "Delete Clip Asset");
			}
			TrackAsset parentTrack = clip.parentTrack;
			parentTrack.RemoveClip(clip);
			parentTrack.CalculateExtrapolationTimes();
			return true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000047F4 File Offset: 0x000029F4
		public bool DeleteTrack(TrackAsset track)
		{
			if (track.timelineAsset != this)
			{
				return false;
			}
			track.parent as TrackAsset != null;
			foreach (TrackAsset trackAsset in track.GetChildTracks())
			{
				this.DeleteTrack(trackAsset);
			}
			this.DeleteRecordedAnimation(track);
			foreach (TimelineClip timelineClip in new List<TimelineClip>(track.clips))
			{
				this.DeleteClip(timelineClip);
			}
			this.RemoveTrack(track);
			TimelineUndo.PushDestroyUndo(this, this, track, "Delete Track");
			return true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Tracks == null || this.m_Tracks.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject scriptableObject = this.m_Tracks[this.m_Tracks.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Tracks.Count - 1; i++)
			{
				if (this.m_Tracks[i] == asset)
				{
					for (int j = this.m_Tracks.Count - 1; j > i; j--)
					{
						this.m_Tracks[j] = this.m_Tracks[j - 1];
					}
					this.m_Tracks[i] = scriptableObject;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000498C File Offset: 0x00002B8C
		internal TrackAsset AllocateTrack(TrackAsset trackAssetParent, string trackName, Type trackType)
		{
			if (trackAssetParent != null && trackAssetParent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(trackType))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			TrackAsset trackAsset = (TrackAsset)ScriptableObject.CreateInstance(trackType);
			trackAsset.name = trackName;
			if (trackAssetParent != null)
			{
				trackAssetParent.AddChild(trackAsset);
			}
			else
			{
				this.AddTrackInternal(trackAsset);
			}
			return trackAsset;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004A08 File Offset: 0x00002C08
		private void DeleteRecordedAnimation(TrackAsset track)
		{
			AnimationTrack animationTrack = track as AnimationTrack;
			if (animationTrack != null && animationTrack.infiniteClip != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, animationTrack.infiniteClip, "Delete Track");
			}
			if (track.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, track.curves, "Delete Track Parameters");
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004A68 File Offset: 0x00002C68
		private void DeleteRecordedAnimation(TimelineClip clip)
		{
			if (clip == null)
			{
				return;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.parentTrack, clip.curves, "Delete Clip Parameters");
			}
			if (!clip.recordable)
			{
				return;
			}
			AnimationPlayableAsset animationPlayableAsset = clip.asset as AnimationPlayableAsset;
			if (animationPlayableAsset == null || animationPlayableAsset.clip == null)
			{
				return;
			}
			TimelineUndo.PushDestroyUndo(this, animationPlayableAsset, animationPlayableAsset.clip, "Delete Recording");
		}

		// Token: 0x04000062 RID: 98
		private const int k_LatestVersion = 0;

		// Token: 0x04000063 RID: 99
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000064 RID: 100
		[HideInInspector]
		[SerializeField]
		private List<ScriptableObject> m_Tracks;

		// Token: 0x04000065 RID: 101
		[HideInInspector]
		[SerializeField]
		private double m_FixedDuration;

		// Token: 0x04000066 RID: 102
		[HideInInspector]
		[NonSerialized]
		private TrackAsset[] m_CacheOutputTracks;

		// Token: 0x04000067 RID: 103
		[HideInInspector]
		[NonSerialized]
		private List<TrackAsset> m_CacheRootTracks;

		// Token: 0x04000068 RID: 104
		[HideInInspector]
		[NonSerialized]
		private List<TrackAsset> m_CacheFlattenedTracks;

		// Token: 0x04000069 RID: 105
		[HideInInspector]
		[SerializeField]
		private TimelineAsset.EditorSettings m_EditorSettings = new TimelineAsset.EditorSettings();

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private TimelineAsset.DurationMode m_DurationMode;

		// Token: 0x0400006B RID: 107
		[HideInInspector]
		[SerializeField]
		private MarkerTrack m_MarkerTrack;

		// Token: 0x0200005D RID: 93
		private enum Versions
		{
			// Token: 0x04000121 RID: 289
			Initial
		}

		// Token: 0x0200005E RID: 94
		private static class TimelineAssetUpgrade
		{
		}

		// Token: 0x0200005F RID: 95
		[Obsolete("MediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
		public enum MediaType
		{
			// Token: 0x04000123 RID: 291
			Animation,
			// Token: 0x04000124 RID: 292
			Audio,
			// Token: 0x04000125 RID: 293
			Texture,
			// Token: 0x04000126 RID: 294
			[Obsolete("Use Texture MediaType instead. (UnityUpgradable) -> UnityEngine.Timeline.TimelineAsset/MediaType.Texture", false)]
			Video = 2,
			// Token: 0x04000127 RID: 295
			Script,
			// Token: 0x04000128 RID: 296
			Hybrid,
			// Token: 0x04000129 RID: 297
			Group
		}

		// Token: 0x02000060 RID: 96
		public enum DurationMode
		{
			// Token: 0x0400012B RID: 299
			BasedOnClips,
			// Token: 0x0400012C RID: 300
			FixedLength
		}

		// Token: 0x02000061 RID: 97
		[Serializable]
		public class EditorSettings
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002FB RID: 763 RVA: 0x0000A86A File Offset: 0x00008A6A
			// (set) Token: 0x060002FC RID: 764 RVA: 0x0000A872 File Offset: 0x00008A72
			public float fps
			{
				get
				{
					return this.m_Framerate;
				}
				set
				{
					this.m_Framerate = TimelineAsset.GetValidFramerate(value);
				}
			}

			// Token: 0x0400012D RID: 301
			internal static readonly float kMinFps = (float)TimeUtility.kFrameRateEpsilon;

			// Token: 0x0400012E RID: 302
			internal static readonly float kMaxFps = 1000f;

			// Token: 0x0400012F RID: 303
			internal static readonly float kDefaultFps = 60f;

			// Token: 0x04000130 RID: 304
			[HideInInspector]
			[SerializeField]
			private float m_Framerate = TimelineAsset.EditorSettings.kDefaultFps;
		}
	}
}
