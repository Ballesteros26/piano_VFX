using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000055 RID: 85
	public sealed class PostProcessManager
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
		public static PostProcessManager instance
		{
			get
			{
				if (PostProcessManager.s_Instance == null)
				{
					PostProcessManager.s_Instance = new PostProcessManager();
				}
				return PostProcessManager.s_Instance;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		private PostProcessManager()
		{
			this.m_SortedVolumes = new Dictionary<int, List<PostProcessVolume>>();
			this.m_Volumes = new List<PostProcessVolume>();
			this.m_SortNeeded = new Dictionary<int, bool>();
			this.m_BaseSettings = new List<PostProcessEffectSettings>();
			this.m_TempColliders = new List<Collider>(5);
			this.settingsTypes = new Dictionary<Type, PostProcessAttribute>();
			this.ReloadBaseTypes();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000C258 File Offset: 0x0000A458
		private void CleanBaseTypes()
		{
			this.settingsTypes.Clear();
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.m_BaseSettings)
			{
				RuntimeUtilities.Destroy(postProcessEffectSettings);
			}
			this.m_BaseSettings.Clear();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		private void ReloadBaseTypes()
		{
			this.CleanBaseTypes();
			foreach (Type type in from t in RuntimeUtilities.GetAllTypesDerivedFrom<PostProcessEffectSettings>()
				where t.IsDefined(typeof(PostProcessAttribute), false) && !t.IsAbstract
				select t)
			{
				this.settingsTypes.Add(type, type.GetAttribute<PostProcessAttribute>());
				PostProcessEffectSettings postProcessEffectSettings = (PostProcessEffectSettings)ScriptableObject.CreateInstance(type);
				postProcessEffectSettings.SetAllOverridesTo(true, false);
				this.m_BaseSettings.Add(postProcessEffectSettings);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000C364 File Offset: 0x0000A564
		public void GetActiveVolumes(PostProcessLayer layer, List<PostProcessVolume> results, bool skipDisabled = true, bool skipZeroWeight = true)
		{
			int value = layer.volumeLayer.value;
			Transform volumeTrigger = layer.volumeTrigger;
			bool flag = volumeTrigger == null;
			Vector3 vector = (flag ? Vector3.zero : volumeTrigger.position);
			foreach (PostProcessVolume postProcessVolume in this.GrabVolumes(value))
			{
				if ((!skipDisabled || postProcessVolume.enabled) && !(postProcessVolume.profileRef == null) && (!skipZeroWeight || postProcessVolume.weight > 0f))
				{
					if (postProcessVolume.isGlobal)
					{
						results.Add(postProcessVolume);
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						postProcessVolume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = ((collider.ClosestPoint(vector) - vector) / 2f).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = postProcessVolume.blendDistance * postProcessVolume.blendDistance;
							if (num <= num2)
							{
								results.Add(postProcessVolume);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000C510 File Offset: 0x0000A710
		public PostProcessVolume GetHighestPriorityVolume(PostProcessLayer layer)
		{
			if (layer == null)
			{
				throw new ArgumentNullException("layer");
			}
			return this.GetHighestPriorityVolume(layer.volumeLayer);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000C534 File Offset: 0x0000A734
		public PostProcessVolume GetHighestPriorityVolume(LayerMask mask)
		{
			float num = float.NegativeInfinity;
			PostProcessVolume postProcessVolume = null;
			List<PostProcessVolume> list;
			if (this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				foreach (PostProcessVolume postProcessVolume2 in list)
				{
					if (postProcessVolume2.priority > num)
					{
						num = postProcessVolume2.priority;
						postProcessVolume = postProcessVolume2;
					}
				}
			}
			return postProcessVolume;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public PostProcessVolume QuickVolume(int layer, float priority, params PostProcessEffectSettings[] settings)
		{
			PostProcessVolume postProcessVolume = new GameObject
			{
				name = "Quick Volume",
				layer = layer,
				hideFlags = HideFlags.HideAndDontSave
			}.AddComponent<PostProcessVolume>();
			postProcessVolume.priority = priority;
			postProcessVolume.isGlobal = true;
			PostProcessProfile profile = postProcessVolume.profile;
			foreach (PostProcessEffectSettings postProcessEffectSettings in settings)
			{
				profile.AddSettings(postProcessEffectSettings);
			}
			return postProcessVolume;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000C618 File Offset: 0x0000A818
		internal void SetLayerDirty(int layer)
		{
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				int key = keyValuePair.Key;
				if ((key & (1 << layer)) != 0)
				{
					this.m_SortNeeded[key] = true;
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C684 File Offset: 0x0000A884
		internal void UpdateVolumeLayer(PostProcessVolume volume, int prevLayer, int newLayer)
		{
			this.Unregister(volume, prevLayer);
			this.Register(volume, newLayer);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000C698 File Offset: 0x0000A898
		private void Register(PostProcessVolume volume, int layer)
		{
			this.m_Volumes.Add(volume);
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & (1 << layer)) != 0)
				{
					keyValuePair.Value.Add(volume);
				}
			}
			this.SetLayerDirty(layer);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C714 File Offset: 0x0000A914
		internal void Register(PostProcessVolume volume)
		{
			int layer = volume.gameObject.layer;
			this.Register(volume, layer);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000C738 File Offset: 0x0000A938
		private void Unregister(PostProcessVolume volume, int layer)
		{
			this.m_Volumes.Remove(volume);
			foreach (KeyValuePair<int, List<PostProcessVolume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & (1 << layer)) != 0)
				{
					keyValuePair.Value.Remove(volume);
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		internal void Unregister(PostProcessVolume volume)
		{
			int layer = volume.gameObject.layer;
			this.Unregister(volume, layer);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		private void ReplaceData(PostProcessLayer postProcessLayer)
		{
			foreach (PostProcessEffectSettings postProcessEffectSettings in this.m_BaseSettings)
			{
				PostProcessEffectSettings settings = postProcessLayer.GetBundle(postProcessEffectSettings.GetType()).settings;
				int count = postProcessEffectSettings.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					settings.parameters[i].SetValue(postProcessEffectSettings.parameters[i]);
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C870 File Offset: 0x0000AA70
		internal void UpdateSettings(PostProcessLayer postProcessLayer, Camera camera)
		{
			this.ReplaceData(postProcessLayer);
			int value = postProcessLayer.volumeLayer.value;
			Transform volumeTrigger = postProcessLayer.volumeTrigger;
			bool flag = volumeTrigger == null;
			Vector3 vector = (flag ? Vector3.zero : volumeTrigger.position);
			foreach (PostProcessVolume postProcessVolume in this.GrabVolumes(value))
			{
				if (postProcessVolume.enabled && !(postProcessVolume.profileRef == null) && postProcessVolume.weight > 0f)
				{
					List<PostProcessEffectSettings> settings = postProcessVolume.profileRef.settings;
					if (postProcessVolume.isGlobal)
					{
						postProcessLayer.OverrideSettings(settings, Mathf.Clamp01(postProcessVolume.weight));
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						postProcessVolume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = ((collider.ClosestPoint(vector) - vector) / 2f).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = postProcessVolume.blendDistance * postProcessVolume.blendDistance;
							if (num <= num2)
							{
								float num3 = 1f;
								if (num2 > 0f)
								{
									num3 = 1f - num / num2;
								}
								postProcessLayer.OverrideSettings(settings, num3 * Mathf.Clamp01(postProcessVolume.weight));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000CA60 File Offset: 0x0000AC60
		private List<PostProcessVolume> GrabVolumes(LayerMask mask)
		{
			List<PostProcessVolume> list;
			if (!this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				list = new List<PostProcessVolume>();
				foreach (PostProcessVolume postProcessVolume in this.m_Volumes)
				{
					if ((mask & (1 << postProcessVolume.gameObject.layer)) != 0)
					{
						list.Add(postProcessVolume);
						this.m_SortNeeded[mask] = true;
					}
				}
				this.m_SortedVolumes.Add(mask, list);
			}
			bool flag;
			if (this.m_SortNeeded.TryGetValue(mask, out flag) && flag)
			{
				this.m_SortNeeded[mask] = false;
				PostProcessManager.SortByPriority(list);
			}
			return list;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		private static void SortByPriority(List<PostProcessVolume> volumes)
		{
			for (int i = 1; i < volumes.Count; i++)
			{
				PostProcessVolume postProcessVolume = volumes[i];
				int num = i - 1;
				while (num >= 0 && volumes[num].priority > postProcessVolume.priority)
				{
					volumes[num + 1] = volumes[num];
					num--;
				}
				volumes[num + 1] = postProcessVolume;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004DDA File Offset: 0x00002FDA
		private static bool IsVolumeRenderedByCamera(PostProcessVolume volume, Camera camera)
		{
			return true;
		}

		// Token: 0x04000156 RID: 342
		private static PostProcessManager s_Instance;

		// Token: 0x04000157 RID: 343
		private const int k_MaxLayerCount = 32;

		// Token: 0x04000158 RID: 344
		private readonly Dictionary<int, List<PostProcessVolume>> m_SortedVolumes;

		// Token: 0x04000159 RID: 345
		private readonly List<PostProcessVolume> m_Volumes;

		// Token: 0x0400015A RID: 346
		private readonly Dictionary<int, bool> m_SortNeeded;

		// Token: 0x0400015B RID: 347
		private readonly List<PostProcessEffectSettings> m_BaseSettings;

		// Token: 0x0400015C RID: 348
		private readonly List<Collider> m_TempColliders;

		// Token: 0x0400015D RID: 349
		public readonly Dictionary<Type, PostProcessAttribute> settingsTypes;
	}
}
