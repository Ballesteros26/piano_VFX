using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000018 RID: 24
	[NotKeyable]
	[Serializable]
	public class ControlPlayableAsset : PlayableAsset, IPropertyPreview, ITimelineClipAsset
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000666E File Offset: 0x0000486E
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006676 File Offset: 0x00004876
		internal bool controllingDirectors { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000667F File Offset: 0x0000487F
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00006687 File Offset: 0x00004887
		internal bool controllingParticles { get; private set; }

		// Token: 0x0600019B RID: 411 RVA: 0x00006690 File Offset: 0x00004890
		public void OnEnable()
		{
			if (this.particleRandomSeed == 0U)
			{
				this.particleRandomSeed = (uint)Random.Range(1, 10000);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000066AB File Offset: 0x000048AB
		public override double duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000066B3 File Offset: 0x000048B3
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | (this.m_SupportLoop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000066C4 File Offset: 0x000048C4
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.prefabGameObject != null)
			{
				if (ControlPlayableAsset.s_CreatedPrefabs.Contains(this.prefabGameObject))
				{
					Debug.LogWarningFormat("Control Track Clip ({0}) is causing a prefab to instantiate itself recursively. Aborting further instances.", new object[] { base.name });
					return Playable.Create(graph, 0);
				}
				ControlPlayableAsset.s_CreatedPrefabs.Add(this.prefabGameObject);
			}
			Playable playable = Playable.Null;
			List<Playable> list = new List<Playable>();
			GameObject gameObject = this.sourceGameObject.Resolve(graph.GetResolver());
			if (this.prefabGameObject != null)
			{
				Transform transform = ((gameObject != null) ? gameObject.transform : null);
				ScriptPlayable<PrefabControlPlayable> scriptPlayable = PrefabControlPlayable.Create(graph, this.prefabGameObject, transform);
				gameObject = scriptPlayable.GetBehaviour().prefabInstance;
				list.Add(scriptPlayable);
			}
			this.m_Duration = PlayableBinding.DefaultDuration;
			this.m_SupportLoop = false;
			this.controllingParticles = false;
			this.controllingDirectors = false;
			if (gameObject != null)
			{
				IList<PlayableDirector> list3;
				if (!this.updateDirector)
				{
					IList<PlayableDirector> list2 = ControlPlayableAsset.k_EmptyDirectorsList;
					list3 = list2;
				}
				else
				{
					list3 = this.GetComponent<PlayableDirector>(gameObject);
				}
				IList<PlayableDirector> list4 = list3;
				IList<ParticleSystem> list6;
				if (!this.updateParticle)
				{
					IList<ParticleSystem> list5 = ControlPlayableAsset.k_EmptyParticlesList;
					list6 = list5;
				}
				else
				{
					list6 = this.GetParticleSystemRoots(gameObject);
				}
				IList<ParticleSystem> list7 = list6;
				this.UpdateDurationAndLoopFlag(list4, list7);
				PlayableDirector component = go.GetComponent<PlayableDirector>();
				if (component != null)
				{
					this.m_ControlDirectorAsset = component.playableAsset;
				}
				if (go == gameObject && this.prefabGameObject == null)
				{
					Debug.LogWarningFormat("Control Playable ({0}) is referencing the same PlayableDirector component than the one in which it is playing.", new object[] { base.name });
					this.active = false;
					if (!this.searchHierarchy)
					{
						this.updateDirector = false;
					}
				}
				if (this.active)
				{
					this.CreateActivationPlayable(gameObject, graph, list);
				}
				if (this.updateDirector)
				{
					this.SearchHierarchyAndConnectDirector(list4, graph, list, this.prefabGameObject != null);
				}
				if (this.updateParticle)
				{
					this.SearchHiearchyAndConnectParticleSystem(list7, graph, list);
				}
				if (this.updateITimeControl)
				{
					ControlPlayableAsset.SearchHierarchyAndConnectControlableScripts(ControlPlayableAsset.GetControlableScripts(gameObject), graph, list);
				}
				playable = ControlPlayableAsset.ConnectPlayablesToMixer(graph, list);
			}
			if (this.prefabGameObject != null)
			{
				ControlPlayableAsset.s_CreatedPrefabs.Remove(this.prefabGameObject);
			}
			if (!playable.IsValid<Playable>())
			{
				playable = Playable.Create(graph, 0);
			}
			return playable;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000068F0 File Offset: 0x00004AF0
		private static Playable ConnectPlayablesToMixer(PlayableGraph graph, List<Playable> playables)
		{
			Playable playable = Playable.Create(graph, playables.Count);
			for (int num = 0; num != playables.Count; num++)
			{
				ControlPlayableAsset.ConnectMixerAndPlayable(graph, playable, playables[num], num);
			}
			playable.SetPropagateSetTime(true);
			return playable;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006934 File Offset: 0x00004B34
		private void CreateActivationPlayable(GameObject root, PlayableGraph graph, List<Playable> outplayables)
		{
			ScriptPlayable<ActivationControlPlayable> scriptPlayable = ActivationControlPlayable.Create(graph, root, this.postPlayback);
			if (scriptPlayable.IsValid<ScriptPlayable<ActivationControlPlayable>>())
			{
				outplayables.Add(scriptPlayable);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006964 File Offset: 0x00004B64
		private void SearchHiearchyAndConnectParticleSystem(IEnumerable<ParticleSystem> particleSystems, PlayableGraph graph, List<Playable> outplayables)
		{
			foreach (ParticleSystem particleSystem in particleSystems)
			{
				if (particleSystem != null)
				{
					this.controllingParticles = true;
					outplayables.Add(ParticleControlPlayable.Create(graph, particleSystem, this.particleRandomSeed));
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000069D0 File Offset: 0x00004BD0
		private void SearchHierarchyAndConnectDirector(IEnumerable<PlayableDirector> directors, PlayableGraph graph, List<Playable> outplayables, bool disableSelfReferences)
		{
			foreach (PlayableDirector playableDirector in directors)
			{
				if (playableDirector != null)
				{
					if (playableDirector.playableAsset != this.m_ControlDirectorAsset)
					{
						outplayables.Add(DirectorControlPlayable.Create(graph, playableDirector));
						this.controllingDirectors = true;
					}
					else if (disableSelfReferences)
					{
						playableDirector.enabled = false;
					}
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006A54 File Offset: 0x00004C54
		private static void SearchHierarchyAndConnectControlableScripts(IEnumerable<MonoBehaviour> controlableScripts, PlayableGraph graph, List<Playable> outplayables)
		{
			foreach (MonoBehaviour monoBehaviour in controlableScripts)
			{
				outplayables.Add(TimeControlPlayable.Create(graph, (ITimeControl)monoBehaviour));
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006AAC File Offset: 0x00004CAC
		private static void ConnectMixerAndPlayable(PlayableGraph graph, Playable mixer, Playable playable, int portIndex)
		{
			graph.Connect<Playable, Playable>(playable, 0, mixer, portIndex);
			mixer.SetInputWeight(playable, 1f);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal IList<T> GetComponent<T>(GameObject gameObject)
		{
			List<T> list = new List<T>();
			if (gameObject != null)
			{
				if (this.searchHierarchy)
				{
					gameObject.GetComponentsInChildren<T>(true, list);
				}
				else
				{
					gameObject.GetComponents<T>(list);
				}
			}
			return list;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006AFE File Offset: 0x00004CFE
		private static IEnumerable<MonoBehaviour> GetControlableScripts(GameObject root)
		{
			if (root == null)
			{
				yield break;
			}
			foreach (MonoBehaviour monoBehaviour in root.GetComponentsInChildren<MonoBehaviour>())
			{
				if (monoBehaviour is ITimeControl)
				{
					yield return monoBehaviour;
				}
			}
			MonoBehaviour[] array = null;
			yield break;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006B10 File Offset: 0x00004D10
		internal void UpdateDurationAndLoopFlag(IList<PlayableDirector> directors, IList<ParticleSystem> particleSystems)
		{
			if (directors.Count == 0 && particleSystems.Count == 0)
			{
				return;
			}
			double num = double.NegativeInfinity;
			bool flag = false;
			foreach (PlayableDirector playableDirector in directors)
			{
				if (playableDirector.playableAsset != null)
				{
					double num2 = playableDirector.playableAsset.duration;
					if (playableDirector.playableAsset is TimelineAsset && num2 > 0.0)
					{
						num2 = (double)((DiscreteTime)num2).OneTickAfter();
					}
					num = Math.Max(num, num2);
					flag = flag || playableDirector.extrapolationMode == DirectorWrapMode.Loop;
				}
			}
			foreach (ParticleSystem particleSystem in particleSystems)
			{
				num = Math.Max(num, (double)particleSystem.main.duration);
				flag = flag || particleSystem.main.loop;
			}
			this.m_Duration = (double.IsNegativeInfinity(num) ? PlayableBinding.DefaultDuration : num);
			this.m_SupportLoop = flag;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006C58 File Offset: 0x00004E58
		private IList<ParticleSystem> GetParticleSystemRoots(GameObject go)
		{
			if (this.searchHierarchy)
			{
				List<ParticleSystem> list = new List<ParticleSystem>();
				ControlPlayableAsset.GetParticleSystemRoots(go.transform, list);
				return list;
			}
			return this.GetComponent<ParticleSystem>(go);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006C88 File Offset: 0x00004E88
		private static void GetParticleSystemRoots(Transform t, ICollection<ParticleSystem> roots)
		{
			ParticleSystem component = t.GetComponent<ParticleSystem>();
			if (component != null)
			{
				roots.Add(component);
				return;
			}
			for (int i = 0; i < t.childCount; i++)
			{
				ControlPlayableAsset.GetParticleSystemRoots(t.GetChild(i), roots);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006CCC File Offset: 0x00004ECC
		public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			if (director == null)
			{
				return;
			}
			if (ControlPlayableAsset.s_ProcessedDirectors.Contains(director))
			{
				return;
			}
			ControlPlayableAsset.s_ProcessedDirectors.Add(director);
			GameObject gameObject = this.sourceGameObject.Resolve(director);
			if (gameObject != null)
			{
				if (this.updateParticle)
				{
					foreach (ParticleSystem particleSystem in gameObject.GetComponentsInChildren<ParticleSystem>(true))
					{
						driver.AddFromName<ParticleSystem>(particleSystem.gameObject, "randomSeed");
						driver.AddFromName<ParticleSystem>(particleSystem.gameObject, "autoRandomSeed");
					}
				}
				if (this.active)
				{
					driver.AddFromName(gameObject, "m_IsActive");
				}
				if (this.updateITimeControl)
				{
					foreach (MonoBehaviour monoBehaviour in ControlPlayableAsset.GetControlableScripts(gameObject))
					{
						IPropertyPreview propertyPreview = monoBehaviour as IPropertyPreview;
						if (propertyPreview != null)
						{
							propertyPreview.GatherProperties(director, driver);
						}
						else
						{
							driver.AddFromComponent(monoBehaviour.gameObject, monoBehaviour);
						}
					}
				}
				if (this.updateDirector)
				{
					foreach (PlayableDirector playableDirector in this.GetComponent<PlayableDirector>(gameObject))
					{
						if (!(playableDirector == null))
						{
							TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
							if (!(timelineAsset == null))
							{
								timelineAsset.GatherProperties(playableDirector, driver);
							}
						}
					}
				}
			}
			ControlPlayableAsset.s_ProcessedDirectors.Remove(director);
		}

		// Token: 0x04000096 RID: 150
		private const int k_MaxRandInt = 10000;

		// Token: 0x04000097 RID: 151
		private static readonly List<PlayableDirector> k_EmptyDirectorsList = new List<PlayableDirector>(0);

		// Token: 0x04000098 RID: 152
		private static readonly List<ParticleSystem> k_EmptyParticlesList = new List<ParticleSystem>(0);

		// Token: 0x04000099 RID: 153
		[SerializeField]
		public ExposedReference<GameObject> sourceGameObject;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		public GameObject prefabGameObject;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		public bool updateParticle = true;

		// Token: 0x0400009C RID: 156
		[SerializeField]
		public uint particleRandomSeed;

		// Token: 0x0400009D RID: 157
		[SerializeField]
		public bool updateDirector = true;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		public bool updateITimeControl = true;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		public bool searchHierarchy = true;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		public bool active = true;

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		public ActivationControlPlayable.PostPlaybackState postPlayback = ActivationControlPlayable.PostPlaybackState.Revert;

		// Token: 0x040000A2 RID: 162
		private PlayableAsset m_ControlDirectorAsset;

		// Token: 0x040000A3 RID: 163
		private double m_Duration = PlayableBinding.DefaultDuration;

		// Token: 0x040000A4 RID: 164
		private bool m_SupportLoop;

		// Token: 0x040000A5 RID: 165
		private static HashSet<PlayableDirector> s_ProcessedDirectors = new HashSet<PlayableDirector>();

		// Token: 0x040000A6 RID: 166
		private static HashSet<GameObject> s_CreatedPrefabs = new HashSet<GameObject>();
	}
}
