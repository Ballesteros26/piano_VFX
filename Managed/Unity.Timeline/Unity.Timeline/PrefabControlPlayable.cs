using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000036 RID: 54
	public class PrefabControlPlayable : PlayableBehaviour
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00008C58 File Offset: 0x00006E58
		public static ScriptPlayable<PrefabControlPlayable> Create(PlayableGraph graph, GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				return ScriptPlayable<PrefabControlPlayable>.Null;
			}
			ScriptPlayable<PrefabControlPlayable> scriptPlayable = ScriptPlayable<PrefabControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(prefabGameObject, parentTransform);
			return scriptPlayable;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008C8C File Offset: 0x00006E8C
		public GameObject prefabInstance
		{
			get
			{
				return this.m_Instance;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008C94 File Offset: 0x00006E94
		public GameObject Initialize(GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				throw new ArgumentNullException("Prefab cannot be null");
			}
			if (this.m_Instance != null)
			{
				Debug.LogWarningFormat("Prefab Control Playable ({0}) has already been initialized with a Prefab ({1}).", new object[]
				{
					prefabGameObject.name,
					this.m_Instance.name
				});
			}
			else
			{
				this.m_Instance = Object.Instantiate<GameObject>(prefabGameObject, parentTransform, false);
				this.m_Instance.name = prefabGameObject.name + " [Timeline]";
				this.m_Instance.SetActive(false);
				PrefabControlPlayable.SetHideFlagsRecursive(this.m_Instance);
			}
			return this.m_Instance;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008D32 File Offset: 0x00006F32
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.m_Instance)
			{
				if (Application.isPlaying)
				{
					Object.Destroy(this.m_Instance);
					return;
				}
				Object.DestroyImmediate(this.m_Instance);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008D5F File Offset: 0x00006F5F
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_Instance == null)
			{
				return;
			}
			this.m_Instance.SetActive(true);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008D7C File Offset: 0x00006F7C
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_Instance != null && info.effectivePlayState == PlayState.Paused)
			{
				this.m_Instance.SetActive(false);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008DA4 File Offset: 0x00006FA4
		private static void SetHideFlagsRecursive(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}
			gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
			if (!Application.isPlaying)
			{
				gameObject.hideFlags |= HideFlags.HideInHierarchy;
			}
			foreach (object obj in gameObject.transform)
			{
				PrefabControlPlayable.SetHideFlagsRecursive(((Transform)obj).gameObject);
			}
		}

		// Token: 0x040000DD RID: 221
		private GameObject m_Instance;
	}
}
