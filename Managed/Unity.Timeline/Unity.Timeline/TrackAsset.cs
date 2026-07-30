using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x02000010 RID: 16
	[IgnoreOnPlayableTrack]
	[Serializable]
	public abstract class TrackAsset : PlayableAsset, ISerializationCallbackReceiver, IPropertyPreview, ICurvesOwner
	{
		// Token: 0x06000116 RID: 278 RVA: 0x000028DC File Offset: 0x00000ADC
		protected virtual void OnBeforeTrackSerialize()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000028DC File Offset: 0x00000ADC
		protected virtual void OnAfterTrackDeserialize()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000028DC File Offset: 0x00000ADC
		internal virtual void OnUpgradeFromVersion(int oldVersion)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004AF0 File Offset: 0x00002CF0
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 3;
			if (this.m_Children != null)
			{
				for (int i = this.m_Children.Count - 1; i >= 0; i--)
				{
					TrackAsset trackAsset = this.m_Children[i] as TrackAsset;
					if (trackAsset != null && trackAsset.parent != this)
					{
						trackAsset.parent = this;
					}
				}
			}
			this.OnBeforeTrackSerialize();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004B5C File Offset: 0x00002D5C
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_ClipsCache = null;
			this.Invalidate();
			if (this.m_Version < 3)
			{
				this.UpgradeToLatestVersion();
				this.OnUpgradeFromVersion(this.m_Version);
			}
			foreach (IMarker marker in this.GetMarkers())
			{
				marker.Initialize(this);
			}
			this.OnAfterTrackDeserialize();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000028DC File Offset: 0x00000ADC
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600011C RID: 284 RVA: 0x00004BD8 File Offset: 0x00002DD8
		// (remove) Token: 0x0600011D RID: 285 RVA: 0x00004C0C File Offset: 0x00002E0C
		internal static event Action<TimelineClip, GameObject, Playable> OnClipPlayableCreate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600011E RID: 286 RVA: 0x00004C40 File Offset: 0x00002E40
		// (remove) Token: 0x0600011F RID: 287 RVA: 0x00004C74 File Offset: 0x00002E74
		internal static event Action<TrackAsset, GameObject, Playable> OnTrackAnimationPlayableCreate;

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public double start
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_Start;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004CBB File Offset: 0x00002EBB
		public double end
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_End;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004CCF File Offset: 0x00002ECF
		public sealed override double duration
		{
			get
			{
				this.UpdateDuration();
				return (double)(this.m_End - this.m_Start);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004CEE File Offset: 0x00002EEE
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004CF6 File Offset: 0x00002EF6
		public bool muted
		{
			get
			{
				return this.m_Muted;
			}
			set
			{
				this.m_Muted = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004D00 File Offset: 0x00002F00
		public bool mutedInHierarchy
		{
			get
			{
				if (this.muted)
				{
					return true;
				}
				TrackAsset trackAsset = this;
				while (trackAsset.parent as TrackAsset != null)
				{
					trackAsset = (TrackAsset)trackAsset.parent;
					if (trackAsset as GroupTrack != null)
					{
						return trackAsset.mutedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004D50 File Offset: 0x00002F50
		public TimelineAsset timelineAsset
		{
			get
			{
				TrackAsset trackAsset = this;
				while (trackAsset != null)
				{
					if (trackAsset.parent == null)
					{
						return null;
					}
					TimelineAsset timelineAsset = trackAsset.parent as TimelineAsset;
					if (timelineAsset != null)
					{
						return timelineAsset;
					}
					trackAsset = trackAsset.parent as TrackAsset;
				}
				return null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004D9E File Offset: 0x00002F9E
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public PlayableAsset parent
		{
			get
			{
				return this.m_Parent;
			}
			internal set
			{
				this.m_Parent = value;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004DAF File Offset: 0x00002FAF
		public IEnumerable<TimelineClip> GetClips()
		{
			return this.clips;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004DB7 File Offset: 0x00002FB7
		internal TimelineClip[] clips
		{
			get
			{
				if (this.m_Clips == null)
				{
					this.m_Clips = new List<TimelineClip>();
				}
				if (this.m_ClipsCache == null)
				{
					this.m_CacheSorted = false;
					this.m_ClipsCache = this.m_Clips.ToArray();
				}
				return this.m_ClipsCache;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004DF2 File Offset: 0x00002FF2
		public virtual bool isEmpty
		{
			get
			{
				return !this.hasClips && !this.hasCurves && this.GetMarkerCount() == 0;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004E0F File Offset: 0x0000300F
		public bool hasClips
		{
			get
			{
				return this.m_Clips != null && this.m_Clips.Count != 0;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004E29 File Offset: 0x00003029
		public bool hasCurves
		{
			get
			{
				return this.m_Curves != null && !this.m_Curves.empty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004E4C File Offset: 0x0000304C
		public bool isSubTrack
		{
			get
			{
				TrackAsset trackAsset = this.parent as TrackAsset;
				return trackAsset != null && trackAsset.GetType() == base.GetType();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004E81 File Offset: 0x00003081
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				TrackBindingTypeAttribute trackBindingTypeAttribute;
				if (!TrackAsset.s_TrackBindingTypeAttributeCache.TryGetValue(base.GetType(), out trackBindingTypeAttribute))
				{
					trackBindingTypeAttribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TrackBindingTypeAttribute));
					TrackAsset.s_TrackBindingTypeAttributeCache.Add(base.GetType(), trackBindingTypeAttribute);
				}
				Type type = ((trackBindingTypeAttribute != null) ? trackBindingTypeAttribute.type : null);
				yield return ScriptPlayableBinding.Create(base.name, this, type);
				yield break;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004E91 File Offset: 0x00003091
		public IEnumerable<TrackAsset> GetChildTracks()
		{
			this.UpdateChildTrackCache();
			return this.m_ChildTrackCache;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004E9F File Offset: 0x0000309F
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00004EA7 File Offset: 0x000030A7
		internal string customPlayableTypename
		{
			get
			{
				return this.m_CustomPlayableFullTypename;
			}
			set
			{
				this.m_CustomPlayableFullTypename = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004EB0 File Offset: 0x000030B0
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00004EB8 File Offset: 0x000030B8
		public AnimationClip curves
		{
			get
			{
				return this.m_Curves;
			}
			internal set
			{
				this.m_Curves = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004EC1 File Offset: 0x000030C1
		string ICurvesOwner.defaultCurvesName
		{
			get
			{
				return "Track Parameters";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004EC8 File Offset: 0x000030C8
		Object ICurvesOwner.asset
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004ECB File Offset: 0x000030CB
		Object ICurvesOwner.assetOwner
		{
			get
			{
				return this.timelineAsset;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004EC8 File Offset: 0x000030C8
		TrackAsset ICurvesOwner.targetTrack
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004ED3 File Offset: 0x000030D3
		internal List<ScriptableObject> subTracksObjects
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004EDB File Offset: 0x000030DB
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004EE3 File Offset: 0x000030E3
		public bool locked
		{
			get
			{
				return this.m_Locked;
			}
			set
			{
				this.m_Locked = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004EEC File Offset: 0x000030EC
		public bool lockedInHierarchy
		{
			get
			{
				if (this.locked)
				{
					return true;
				}
				TrackAsset trackAsset = this;
				while (trackAsset.parent as TrackAsset != null)
				{
					trackAsset = (TrackAsset)trackAsset.parent;
					if (trackAsset as GroupTrack != null)
					{
						return trackAsset.lockedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004F3C File Offset: 0x0000313C
		public bool supportsNotifications
		{
			get
			{
				if (this.m_SupportsNotifications == null)
				{
					this.m_SupportsNotifications = new bool?(NotificationUtilities.TrackTypeSupportsNotifications(base.GetType()));
				}
				return this.m_SupportsNotifications.Value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004F6C File Offset: 0x0000316C
		private void __internalAwake()
		{
			if (this.m_Clips == null)
			{
				this.m_Clips = new List<TimelineClip>();
			}
			this.m_ChildTrackCache = null;
			if (this.m_Children == null)
			{
				this.m_Children = new List<ScriptableObject>();
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004F9B File Offset: 0x0000319B
		public void CreateCurves(string curvesClipName)
		{
			if (this.m_Curves != null)
			{
				return;
			}
			this.m_Curves = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? "Track Parameters" : curvesClipName, this, true);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004FC9 File Offset: 0x000031C9
		public virtual Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Create(graph, inputCount);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002C48 File Offset: 0x00000E48
		public sealed override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return Playable.Null;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004FD4 File Offset: 0x000031D4
		public TimelineClip CreateDefaultClip()
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			Type type = null;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				TrackClipTypeAttribute trackClipTypeAttribute = array[i] as TrackClipTypeAttribute;
				if (trackClipTypeAttribute != null && typeof(IPlayableAsset).IsAssignableFrom(trackClipTypeAttribute.inspectedType) && typeof(ScriptableObject).IsAssignableFrom(trackClipTypeAttribute.inspectedType))
				{
					type = trackClipTypeAttribute.inspectedType;
					break;
				}
			}
			if (type == null)
			{
				Debug.LogWarning("Cannot create a default clip for type " + base.GetType());
				return null;
			}
			return this.CreateAndAddNewClipOfType(type);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005072 File Offset: 0x00003272
		public TimelineClip CreateClip<T>() where T : ScriptableObject, IPlayableAsset
		{
			return this.CreateClip(typeof(T));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005084 File Offset: 0x00003284
		public IMarker CreateMarker(Type type, double time)
		{
			return this.m_Markers.CreateMarker(type, time, this);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005094 File Offset: 0x00003294
		public T CreateMarker<T>(double time) where T : ScriptableObject, IMarker
		{
			return (T)((object)this.CreateMarker(typeof(T), time));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000050AC File Offset: 0x000032AC
		public bool DeleteMarker(IMarker marker)
		{
			return this.m_Markers.Remove(marker);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000050BA File Offset: 0x000032BA
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.m_Markers.GetMarkers();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000050C7 File Offset: 0x000032C7
		public int GetMarkerCount()
		{
			return this.m_Markers.Count;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000050D4 File Offset: 0x000032D4
		public IMarker GetMarker(int idx)
		{
			return this.m_Markers[idx];
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000050E2 File Offset: 0x000032E2
		internal TimelineClip CreateClip(Type requestedType)
		{
			if (this.ValidateClipType(requestedType))
			{
				return this.CreateAndAddNewClipOfType(requestedType);
			}
			throw new InvalidOperationException(string.Concat(new object[]
			{
				"Clips of type ",
				requestedType,
				" are not permitted on tracks of type ",
				base.GetType()
			}));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005124 File Offset: 0x00003324
		internal TimelineClip CreateAndAddNewClipOfType(Type requestedType)
		{
			TimelineClip timelineClip = this.CreateClipOfType(requestedType);
			this.AddClip(timelineClip);
			return timelineClip;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005144 File Offset: 0x00003344
		internal TimelineClip CreateClipOfType(Type requestedType)
		{
			if (!this.ValidateClipType(requestedType))
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Clips of type ",
					requestedType,
					" are not permitted on tracks of type ",
					base.GetType()
				}));
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateInstance(requestedType);
			if (scriptableObject == null)
			{
				throw new InvalidOperationException("Could not create an instance of the ScriptableObject type " + requestedType.Name);
			}
			scriptableObject.name = requestedType.Name;
			TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, this);
			return this.CreateClipFromAsset(scriptableObject);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000051C8 File Offset: 0x000033C8
		internal TimelineClip CreateClipFromPlayableAsset(IPlayableAsset asset)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (asset as ScriptableObject == null)
			{
				throw new ArgumentException("CreateClipFromPlayableAsset  only supports ScriptableObject-derived Types");
			}
			if (!this.ValidateClipType(asset.GetType()))
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Clips of type ",
					asset.GetType(),
					" are not permitted on tracks of type ",
					base.GetType()
				}));
			}
			return this.CreateClipFromAsset(asset as ScriptableObject);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000524C File Offset: 0x0000344C
		private TimelineClip CreateClipFromAsset(ScriptableObject playableAsset)
		{
			TimelineClip timelineClip = this.CreateNewClipContainerInternal();
			timelineClip.displayName = playableAsset.name;
			timelineClip.asset = playableAsset;
			IPlayableAsset playableAsset2 = playableAsset as IPlayableAsset;
			if (playableAsset2 != null)
			{
				double duration = playableAsset2.duration;
				if (!double.IsInfinity(duration) && duration > 0.0)
				{
					timelineClip.duration = Math.Min(Math.Max(duration, TimelineClip.kMinDuration), TimelineClip.kMaxTimeValue);
				}
			}
			try
			{
				this.OnCreateClip(timelineClip);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message, playableAsset);
				return null;
			}
			return timelineClip;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000052E0 File Offset: 0x000034E0
		internal IEnumerable<ScriptableObject> GetMarkersRaw()
		{
			return this.m_Markers.GetRawMarkerList();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000052ED File Offset: 0x000034ED
		internal void ClearMarkers()
		{
			this.m_Markers.Clear();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000052FA File Offset: 0x000034FA
		internal void AddMarker(ScriptableObject e)
		{
			this.m_Markers.Add(e);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005308 File Offset: 0x00003508
		internal bool DeleteMarkerRaw(ScriptableObject marker)
		{
			return this.m_Markers.Remove(marker, this.timelineAsset, this);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005320 File Offset: 0x00003520
		private int GetTimeRangeHash()
		{
			double num = double.MaxValue;
			double num2 = double.MinValue;
			foreach (IMarker marker in this.GetMarkers())
			{
				if (marker is INotification)
				{
					if (marker.time < num)
					{
						num = marker.time;
					}
					if (marker.time > num2)
					{
						num2 = marker.time;
					}
				}
			}
			return num.GetHashCode().CombineHash(num2.GetHashCode());
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000053B8 File Offset: 0x000035B8
		internal void AddClip(TimelineClip newClip)
		{
			if (!this.m_Clips.Contains(newClip))
			{
				this.m_Clips.Add(newClip);
				this.m_ClipsCache = null;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000053DC File Offset: 0x000035DC
		private Playable CreateNotificationsPlayable(PlayableGraph graph, Playable mixerPlayable, GameObject go, Playable timelinePlayable)
		{
			TrackAsset.s_BuildData.markerList.Clear();
			this.GatherNotificiations(TrackAsset.s_BuildData.markerList);
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = NotificationUtilities.CreateNotificationsPlayable(graph, TrackAsset.s_BuildData.markerList, go);
			if (scriptPlayable.IsValid<ScriptPlayable<TimeNotificationBehaviour>>())
			{
				scriptPlayable.GetBehaviour().timeSource = timelinePlayable;
				if (mixerPlayable.IsValid<Playable>())
				{
					scriptPlayable.SetInputCount(1);
					graph.Connect<Playable, ScriptPlayable<TimeNotificationBehaviour>>(mixerPlayable, 0, scriptPlayable, 0);
					scriptPlayable.SetInputWeight(mixerPlayable, 1f);
				}
			}
			return scriptPlayable;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005460 File Offset: 0x00003660
		internal Playable CreatePlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree, Playable timelinePlayable)
		{
			this.UpdateDuration();
			Playable playable = Playable.Null;
			if (this.CanCompileClipsRecursive())
			{
				playable = this.OnCreateClipPlayableGraph(graph, go, tree);
			}
			Playable playable2 = this.CreateNotificationsPlayable(graph, playable, go, timelinePlayable);
			if (!playable2.IsValid<Playable>() && !playable.IsValid<Playable>())
			{
				Debug.LogErrorFormat("Track {0} of type {1} has no notifications and returns an invalid mixer Playable", new object[]
				{
					base.name,
					base.GetType().FullName
				});
				return Playable.Create(graph, 0);
			}
			if (!playable2.IsValid<Playable>())
			{
				return playable;
			}
			return playable2;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000054E4 File Offset: 0x000036E4
		internal virtual Playable CompileClips(PlayableGraph graph, GameObject go, IList<TimelineClip> timelineClips, IntervalTree<RuntimeElement> tree)
		{
			Playable playable = this.CreateTrackMixer(graph, go, timelineClips.Count);
			for (int i = 0; i < timelineClips.Count; i++)
			{
				Playable playable2 = this.CreatePlayable(graph, go, timelineClips[i]);
				if (playable2.IsValid<Playable>())
				{
					playable2.SetDuration(timelineClips[i].duration);
					RuntimeClip runtimeClip = new RuntimeClip(timelineClips[i], playable2, playable);
					tree.Add(runtimeClip);
					graph.Connect<Playable, Playable>(playable2, 0, playable, i);
					playable.SetInputWeight(i, 0f);
				}
			}
			this.ConfigureTrackAnimation(tree, go, playable);
			return playable;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005578 File Offset: 0x00003778
		private void GatherCompilableTracks(IList<TrackAsset> tracks)
		{
			if (!this.muted && this.CanCompileClips())
			{
				tracks.Add(this);
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherCompilableTracks(tracks);
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000055E8 File Offset: 0x000037E8
		private void GatherNotificiations(List<IMarker> markers)
		{
			if (!this.muted && this.CanCompileNotifications())
			{
				markers.AddRange(this.GetMarkers());
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherNotificiations(markers);
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000565C File Offset: 0x0000385C
		internal virtual Playable OnCreateClipPlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree)
		{
			if (tree == null)
			{
				throw new ArgumentException("IntervalTree argument cannot be null", "tree");
			}
			if (go == null)
			{
				throw new ArgumentException("GameObject argument cannot be null", "go");
			}
			TrackAsset.s_BuildData.Clear();
			this.GatherCompilableTracks(TrackAsset.s_BuildData.trackList);
			if (TrackAsset.s_BuildData.trackList.Count == 0)
			{
				return Playable.Null;
			}
			Playable playable = Playable.Null;
			ILayerable layerable = this as ILayerable;
			if (layerable != null)
			{
				playable = layerable.CreateLayerMixer(graph, go, TrackAsset.s_BuildData.trackList.Count);
			}
			if (playable.IsValid<Playable>())
			{
				for (int i = 0; i < TrackAsset.s_BuildData.trackList.Count; i++)
				{
					Playable playable2 = TrackAsset.s_BuildData.trackList[i].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[i].clips, tree);
					if (playable2.IsValid<Playable>())
					{
						graph.Connect<Playable, Playable>(playable2, 0, playable, i);
						playable.SetInputWeight(i, 1f);
					}
				}
				return playable;
			}
			if (TrackAsset.s_BuildData.trackList.Count == 1)
			{
				return TrackAsset.s_BuildData.trackList[0].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[0].clips, tree);
			}
			for (int j = 0; j < TrackAsset.s_BuildData.trackList.Count; j++)
			{
				TrackAsset.s_BuildData.clipList.AddRange(TrackAsset.s_BuildData.trackList[j].clips);
			}
			return this.CompileClips(graph, go, TrackAsset.s_BuildData.clipList, tree);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000057F6 File Offset: 0x000039F6
		internal void ConfigureTrackAnimation(IntervalTree<RuntimeElement> tree, GameObject go, Playable blend)
		{
			if (!this.hasCurves)
			{
				return;
			}
			blend.SetAnimatedProperties(this.m_Curves);
			tree.Add(new InfiniteRuntimeClip(blend));
			if (TrackAsset.OnTrackAnimationPlayableCreate != null)
			{
				TrackAsset.OnTrackAnimationPlayableCreate(this, go, blend);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005830 File Offset: 0x00003A30
		internal void SortClips()
		{
			TimelineClip[] clips = this.clips;
			if (!this.m_CacheSorted)
			{
				Array.Sort<TimelineClip>(this.clips, (TimelineClip clip1, TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
				this.m_CacheSorted = true;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000587D File Offset: 0x00003A7D
		internal void ClearClipsInternal()
		{
			this.m_Clips = new List<TimelineClip>();
			this.m_ClipsCache = null;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005891 File Offset: 0x00003A91
		internal void ClearSubTracksInternal()
		{
			this.m_Children = new List<ScriptableObject>();
			this.Invalidate();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000058A4 File Offset: 0x00003AA4
		internal void OnClipMove()
		{
			this.m_CacheSorted = false;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000058B0 File Offset: 0x00003AB0
		internal TimelineClip CreateNewClipContainerInternal()
		{
			TimelineClip timelineClip = new TimelineClip(this);
			timelineClip.asset = null;
			double num = 0.0;
			for (int i = 0; i < this.m_Clips.Count - 1; i++)
			{
				double num2 = this.m_Clips[i].duration;
				if (double.IsInfinity(num2))
				{
					num2 = (double)TimelineClip.kDefaultClipDurationInSeconds;
				}
				num = Math.Max(num, this.m_Clips[i].start + num2);
			}
			timelineClip.mixInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			timelineClip.mixOutCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
			timelineClip.start = num;
			timelineClip.duration = (double)TimelineClip.kDefaultClipDurationInSeconds;
			timelineClip.displayName = "untitled";
			return timelineClip;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005985 File Offset: 0x00003B85
		internal void AddChild(TrackAsset child)
		{
			if (child == null)
			{
				return;
			}
			this.m_Children.Add(child);
			child.parent = this;
			this.Invalidate();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000059AC File Offset: 0x00003BAC
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Children == null || this.m_Children.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject scriptableObject = this.m_Children[this.m_Children.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Children.Count - 1; i++)
			{
				if (this.m_Children[i] == asset)
				{
					for (int j = this.m_Children.Count - 1; j > i; j--)
					{
						this.m_Children[j] = this.m_Children[j - 1];
					}
					this.m_Children[i] = scriptableObject;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005A6E File Offset: 0x00003C6E
		internal bool RemoveSubTrack(TrackAsset child)
		{
			if (this.m_Children.Remove(child))
			{
				this.Invalidate();
				child.parent = null;
				return true;
			}
			return false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005A8E File Offset: 0x00003C8E
		internal void RemoveClip(TimelineClip clip)
		{
			this.m_Clips.Remove(clip);
			this.m_ClipsCache = null;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005AA4 File Offset: 0x00003CA4
		internal virtual void GetEvaluationTime(out double outStart, out double outDuration)
		{
			outStart = double.PositiveInfinity;
			double num = double.NegativeInfinity;
			if (this.hasCurves)
			{
				outStart = 0.0;
				num = TimeUtility.GetAnimationClipLength(this.curves);
			}
			foreach (TimelineClip timelineClip in this.clips)
			{
				outStart = Math.Min(timelineClip.start, outStart);
				num = Math.Max(timelineClip.end, num);
			}
			if (this.HasNotifications())
			{
				double notificationDuration = this.GetNotificationDuration();
				outStart = Math.Min(notificationDuration, outStart);
				num = Math.Max(notificationDuration, num);
			}
			if (double.IsInfinity(outStart) || double.IsInfinity(num))
			{
				outStart = (outDuration = 0.0);
				return;
			}
			outDuration = num - outStart;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005B68 File Offset: 0x00003D68
		internal virtual void GetSequenceTime(out double outStart, out double outDuration)
		{
			this.GetEvaluationTime(out outStart, out outDuration);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005B74 File Offset: 0x00003D74
		public virtual void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			GameObject gameObjectBinding = this.GetGameObjectBinding(director);
			if (gameObjectBinding != null)
			{
				driver.PushActiveGameObject(gameObjectBinding);
			}
			if (this.hasCurves)
			{
				driver.AddObjectProperties(this, this.m_Curves);
			}
			foreach (TimelineClip timelineClip in this.clips)
			{
				if (timelineClip.curves != null && timelineClip.asset != null)
				{
					driver.AddObjectProperties(timelineClip.asset, timelineClip.curves);
				}
				IPropertyPreview propertyPreview = timelineClip.asset as IPropertyPreview;
				if (propertyPreview != null)
				{
					propertyPreview.GatherProperties(director, driver);
				}
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherProperties(director, driver);
				}
			}
			if (gameObjectBinding != null)
			{
				driver.PopActiveGameObject();
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005C70 File Offset: 0x00003E70
		internal GameObject GetGameObjectBinding(PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			Object genericBinding = director.GetGenericBinding(this);
			GameObject gameObject = genericBinding as GameObject;
			if (gameObject != null)
			{
				return gameObject;
			}
			Component component = genericBinding as Component;
			if (component != null)
			{
				return component.gameObject;
			}
			return null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005CBC File Offset: 0x00003EBC
		internal bool ValidateClipType(Type clipType)
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (((TrackClipTypeAttribute)customAttributes[i]).inspectedType.IsAssignableFrom(clipType))
				{
					return true;
				}
			}
			return typeof(PlayableTrack).IsAssignableFrom(base.GetType()) && typeof(IPlayableAsset).IsAssignableFrom(clipType) && typeof(ScriptableObject).IsAssignableFrom(clipType);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000028DC File Offset: 0x00000ADC
		protected virtual void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005D40 File Offset: 0x00003F40
		private void UpdateDuration()
		{
			int num = this.CalculateItemsHash();
			if (num == this.m_ItemsHash)
			{
				return;
			}
			this.m_ItemsHash = num;
			double num2;
			double num3;
			this.GetSequenceTime(out num2, out num3);
			this.m_Start = (DiscreteTime)num2;
			this.m_End = (DiscreteTime)(num2 + num3);
			this.CalculateExtrapolationTimes();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005D8F File Offset: 0x00003F8F
		protected internal virtual int CalculateItemsHash()
		{
			return HashUtility.CombineHash(this.GetClipsHash(), TrackAsset.GetAnimationClipHash(this.m_Curves), this.GetTimeRangeHash());
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005DB0 File Offset: 0x00003FB0
		protected virtual Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
		{
			if (!graph.IsValid())
			{
				throw new ArgumentException("graph must be a valid PlayableGraph");
			}
			if (clip == null)
			{
				throw new ArgumentNullException("clip");
			}
			IPlayableAsset playableAsset = clip.asset as IPlayableAsset;
			if (playableAsset != null)
			{
				Playable playable = playableAsset.CreatePlayable(graph, gameObject);
				if (playable.IsValid<Playable>())
				{
					playable.SetAnimatedProperties(clip.curves);
					playable.SetSpeed(clip.timeScale);
					if (TrackAsset.OnClipPlayableCreate != null)
					{
						TrackAsset.OnClipPlayableCreate(clip, gameObject, playable);
					}
				}
				return playable;
			}
			return Playable.Null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005E34 File Offset: 0x00004034
		internal void Invalidate()
		{
			this.m_ChildTrackCache = null;
			TimelineAsset timelineAsset = this.timelineAsset;
			if (timelineAsset != null)
			{
				timelineAsset.Invalidate();
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005E60 File Offset: 0x00004060
		internal double GetNotificationDuration()
		{
			if (!this.supportsNotifications)
			{
				return 0.0;
			}
			double num = 0.0;
			foreach (IMarker marker in this.GetMarkers())
			{
				if (marker is INotification)
				{
					num = Math.Max(num, marker.time);
				}
			}
			return num;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005ED8 File Offset: 0x000040D8
		internal virtual bool CanCompileClips()
		{
			return this.hasClips || this.hasCurves;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005EEC File Offset: 0x000040EC
		internal bool IsCompilable()
		{
			if (typeof(GroupTrack).IsAssignableFrom(base.GetType()))
			{
				return false;
			}
			bool flag = !this.mutedInHierarchy && (this.CanCompileClips() || this.CanCompileNotifications());
			if (!flag)
			{
				using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCompilable())
						{
							return true;
						}
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005F78 File Offset: 0x00004178
		private void UpdateChildTrackCache()
		{
			if (this.m_ChildTrackCache == null)
			{
				if (this.m_Children == null || this.m_Children.Count == 0)
				{
					this.m_ChildTrackCache = TrackAsset.s_EmptyCache;
					return;
				}
				List<TrackAsset> list = new List<TrackAsset>(this.m_Children.Count);
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					TrackAsset trackAsset = this.m_Children[i] as TrackAsset;
					if (trackAsset != null)
					{
						list.Add(trackAsset);
					}
				}
				this.m_ChildTrackCache = list;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005FFE File Offset: 0x000041FE
		internal virtual int Hash()
		{
			return this.clips.Length + (this.m_Markers.Count << 16);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006018 File Offset: 0x00004218
		private int GetClipsHash()
		{
			int num = 0;
			foreach (TimelineClip timelineClip in this.m_Clips)
			{
				num = num.CombineHash(timelineClip.Hash());
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006074 File Offset: 0x00004274
		protected static int GetAnimationClipHash(AnimationClip clip)
		{
			int num = 0;
			if (clip != null && !clip.empty)
			{
				num = num.CombineHash(clip.frameRate.GetHashCode()).CombineHash(clip.length.GetHashCode());
			}
			return num;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000060BD File Offset: 0x000042BD
		private bool HasNotifications()
		{
			return this.m_Markers.HasNotifications();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000060CA File Offset: 0x000042CA
		private bool CanCompileNotifications()
		{
			return this.supportsNotifications && this.m_Markers.HasNotifications();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000060E4 File Offset: 0x000042E4
		private bool CanCompileClipsRecursive()
		{
			if (this.CanCompileClips())
			{
				return true;
			}
			using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CanCompileClipsRecursive())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400006C RID: 108
		private const int k_LatestVersion = 3;

		// Token: 0x0400006D RID: 109
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x0400006E RID: 110
		[Obsolete("Please use m_InfiniteClip (on AnimationTrack) instead.", false)]
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_animClip")]
		internal AnimationClip m_AnimClip;

		// Token: 0x0400006F RID: 111
		private static TrackAsset.TransientBuildData s_BuildData = TrackAsset.TransientBuildData.Create();

		// Token: 0x04000070 RID: 112
		internal const string kDefaultCurvesName = "Track Parameters";

		// Token: 0x04000073 RID: 115
		[SerializeField]
		[HideInInspector]
		private bool m_Locked;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		[HideInInspector]
		private bool m_Muted;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		[HideInInspector]
		private string m_CustomPlayableFullTypename = string.Empty;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		[HideInInspector]
		private AnimationClip m_Curves;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		[HideInInspector]
		private PlayableAsset m_Parent;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Children;

		// Token: 0x04000079 RID: 121
		[NonSerialized]
		private int m_ItemsHash;

		// Token: 0x0400007A RID: 122
		[NonSerialized]
		private TimelineClip[] m_ClipsCache;

		// Token: 0x0400007B RID: 123
		private DiscreteTime m_Start;

		// Token: 0x0400007C RID: 124
		private DiscreteTime m_End;

		// Token: 0x0400007D RID: 125
		private bool m_CacheSorted;

		// Token: 0x0400007E RID: 126
		private bool? m_SupportsNotifications;

		// Token: 0x0400007F RID: 127
		private static TrackAsset[] s_EmptyCache = new TrackAsset[0];

		// Token: 0x04000080 RID: 128
		private IEnumerable<TrackAsset> m_ChildTrackCache;

		// Token: 0x04000081 RID: 129
		private static Dictionary<Type, TrackBindingTypeAttribute> s_TrackBindingTypeAttributeCache = new Dictionary<Type, TrackBindingTypeAttribute>();

		// Token: 0x04000082 RID: 130
		[SerializeField]
		[HideInInspector]
		protected internal List<TimelineClip> m_Clips = new List<TimelineClip>();

		// Token: 0x04000083 RID: 131
		[SerializeField]
		[HideInInspector]
		private MarkerList m_Markers = new MarkerList(0);

		// Token: 0x02000063 RID: 99
		internal enum Versions
		{
			// Token: 0x04000138 RID: 312
			Initial,
			// Token: 0x04000139 RID: 313
			RotationAsEuler,
			// Token: 0x0400013A RID: 314
			RootMotionUpgrade,
			// Token: 0x0400013B RID: 315
			AnimatedTrackProperties
		}

		// Token: 0x02000064 RID: 100
		private static class TrackAssetUpgrade
		{
		}

		// Token: 0x02000065 RID: 101
		private struct TransientBuildData
		{
			// Token: 0x06000309 RID: 777 RVA: 0x0000AAB0 File Offset: 0x00008CB0
			public static TrackAsset.TransientBuildData Create()
			{
				return new TrackAsset.TransientBuildData
				{
					trackList = new List<TrackAsset>(20),
					clipList = new List<TimelineClip>(500),
					markerList = new List<IMarker>(100)
				};
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000AAF3 File Offset: 0x00008CF3
			public void Clear()
			{
				this.trackList.Clear();
				this.clipList.Clear();
				this.markerList.Clear();
			}

			// Token: 0x0400013C RID: 316
			public List<TrackAsset> trackList;

			// Token: 0x0400013D RID: 317
			public List<TimelineClip> clipList;

			// Token: 0x0400013E RID: 318
			public List<IMarker> markerList;
		}
	}
}
