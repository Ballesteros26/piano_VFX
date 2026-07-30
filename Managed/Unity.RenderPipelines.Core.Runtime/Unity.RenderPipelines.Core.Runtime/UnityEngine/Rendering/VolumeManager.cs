using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x02000067 RID: 103
	public sealed class VolumeManager
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
		public static VolumeManager instance
		{
			get
			{
				return VolumeManager.s_Instance.Value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000C6FA File Offset: 0x0000A8FA
		public VolumeStack stack { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C703 File Offset: 0x0000A903
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000C70B File Offset: 0x0000A90B
		public IEnumerable<Type> baseComponentTypes { get; private set; }

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C714 File Offset: 0x0000A914
		private VolumeManager()
		{
			this.m_SortedVolumes = new Dictionary<int, List<Volume>>();
			this.m_Volumes = new List<Volume>();
			this.m_SortNeeded = new Dictionary<int, bool>();
			this.m_TempColliders = new List<Collider>(8);
			this.m_ComponentsDefaultState = new List<VolumeComponent>();
			this.ReloadBaseTypes();
			this.stack = this.CreateStack();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C771 File Offset: 0x0000A971
		public VolumeStack CreateStack()
		{
			VolumeStack volumeStack = new VolumeStack();
			volumeStack.Reload(this.baseComponentTypes);
			return volumeStack;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C784 File Offset: 0x0000A984
		public void DestroyStack(VolumeStack stack)
		{
			stack.Dispose();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C78C File Offset: 0x0000A98C
		private void ReloadBaseTypes()
		{
			this.m_ComponentsDefaultState.Clear();
			this.baseComponentTypes = from t in CoreUtils.GetAllTypesDerivedFrom<VolumeComponent>()
				where !t.IsAbstract
				select t;
			foreach (Type type in this.baseComponentTypes)
			{
				VolumeComponent volumeComponent = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.m_ComponentsDefaultState.Add(volumeComponent);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C824 File Offset: 0x0000AA24
		public void Register(Volume volume, int layer)
		{
			this.m_Volumes.Add(volume);
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & (1 << layer)) != 0)
				{
					keyValuePair.Value.Add(volume);
				}
			}
			this.SetLayerDirty(layer);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		public void Unregister(Volume volume, int layer)
		{
			this.m_Volumes.Remove(volume);
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				if ((keyValuePair.Key & (1 << layer)) != 0)
				{
					keyValuePair.Value.Remove(volume);
				}
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C918 File Offset: 0x0000AB18
		public bool IsComponentActiveInMask<T>(LayerMask layerMask) where T : VolumeComponent
		{
			int value = layerMask.value;
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				if (keyValuePair.Key == value)
				{
					foreach (Volume volume in keyValuePair.Value)
					{
						T t;
						if (volume.enabled && !(volume.profileRef == null) && volume.profileRef.TryGet<T>(out t) && t.active)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		internal void SetLayerDirty(int layer)
		{
			foreach (KeyValuePair<int, List<Volume>> keyValuePair in this.m_SortedVolumes)
			{
				int key = keyValuePair.Key;
				if ((key & (1 << layer)) != 0)
				{
					this.m_SortNeeded[key] = true;
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000CA64 File Offset: 0x0000AC64
		internal void UpdateVolumeLayer(Volume volume, int prevLayer, int newLayer)
		{
			this.Unregister(volume, prevLayer);
			this.Register(volume, newLayer);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000CA78 File Offset: 0x0000AC78
		private void OverrideData(VolumeStack stack, List<VolumeComponent> components, float interpFactor)
		{
			foreach (VolumeComponent volumeComponent in components)
			{
				if (volumeComponent.active)
				{
					VolumeComponent component = stack.GetComponent(volumeComponent.GetType());
					volumeComponent.Override(component, interpFactor);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000CADC File Offset: 0x0000ACDC
		private void ReplaceData(VolumeStack stack, List<VolumeComponent> components)
		{
			foreach (VolumeComponent volumeComponent in components)
			{
				VolumeComponent component = stack.GetComponent(volumeComponent.GetType());
				int count = volumeComponent.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					component.parameters[i].SetValue(volumeComponent.parameters[i]);
				}
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		[Conditional("UNITY_EDITOR")]
		public void CheckBaseTypes()
		{
			if (this.m_ComponentsDefaultState == null || (this.m_ComponentsDefaultState.Count > 0 && this.m_ComponentsDefaultState[0] == null))
			{
				this.ReloadBaseTypes();
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		[Conditional("UNITY_EDITOR")]
		public void CheckStack(VolumeStack stack)
		{
			Dictionary<Type, VolumeComponent> components = stack.components;
			if (components == null)
			{
				stack.Reload(this.baseComponentTypes);
				return;
			}
			foreach (KeyValuePair<Type, VolumeComponent> keyValuePair in components)
			{
				if (keyValuePair.Key == null || keyValuePair.Value == null)
				{
					stack.Reload(this.baseComponentTypes);
					break;
				}
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		public void Update(Transform trigger, LayerMask layerMask)
		{
			this.Update(this.stack, trigger, layerMask);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		public void Update(VolumeStack stack, Transform trigger, LayerMask layerMask)
		{
			this.ReplaceData(stack, this.m_ComponentsDefaultState);
			bool flag = trigger == null;
			Vector3 vector = (flag ? Vector3.zero : trigger.position);
			List<Volume> list = this.GrabVolumes(layerMask);
			Camera camera = null;
			if (!flag)
			{
				trigger.TryGetComponent<Camera>(out camera);
			}
			foreach (Volume volume in list)
			{
				if (volume.enabled && !(volume.profileRef == null) && volume.weight > 0f)
				{
					if (volume.isGlobal)
					{
						this.OverrideData(stack, volume.profileRef.components, Mathf.Clamp01(volume.weight));
					}
					else if (!flag)
					{
						List<Collider> tempColliders = this.m_TempColliders;
						volume.GetComponents<Collider>(tempColliders);
						if (tempColliders.Count != 0)
						{
							float num = float.PositiveInfinity;
							foreach (Collider collider in tempColliders)
							{
								if (collider.enabled)
								{
									float sqrMagnitude = (collider.ClosestPoint(vector) - vector).sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
									}
								}
							}
							tempColliders.Clear();
							float num2 = volume.blendDistance * volume.blendDistance;
							if (num <= num2)
							{
								float num3 = 1f;
								if (num2 > 0f)
								{
									num3 = 1f - num / num2;
								}
								this.OverrideData(stack, volume.profileRef.components, num3 * Mathf.Clamp01(volume.weight));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000CE28 File Offset: 0x0000B028
		private List<Volume> GrabVolumes(LayerMask mask)
		{
			List<Volume> list;
			if (!this.m_SortedVolumes.TryGetValue(mask, out list))
			{
				list = new List<Volume>();
				foreach (Volume volume in this.m_Volumes)
				{
					if ((mask & (1 << volume.gameObject.layer)) != 0)
					{
						list.Add(volume);
						this.m_SortNeeded[mask] = true;
					}
				}
				this.m_SortedVolumes.Add(mask, list);
			}
			bool flag;
			if (this.m_SortNeeded.TryGetValue(mask, out flag) && flag)
			{
				this.m_SortNeeded[mask] = false;
				VolumeManager.SortByPriority(list);
			}
			return list;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000CF04 File Offset: 0x0000B104
		private static void SortByPriority(List<Volume> volumes)
		{
			for (int i = 1; i < volumes.Count; i++)
			{
				Volume volume = volumes[i];
				int num = i - 1;
				while (num >= 0 && volumes[num].priority > volume.priority)
				{
					volumes[num + 1] = volumes[num];
					num--;
				}
				volumes[num + 1] = volume;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B492 File Offset: 0x00009692
		private static bool IsVolumeRenderedByCamera(Volume volume, Camera camera)
		{
			return true;
		}

		// Token: 0x040001A2 RID: 418
		internal static bool needIsolationFilteredByRenderer = false;

		// Token: 0x040001A3 RID: 419
		private static readonly Lazy<VolumeManager> s_Instance = new Lazy<VolumeManager>(() => new VolumeManager());

		// Token: 0x040001A6 RID: 422
		private const int k_MaxLayerCount = 32;

		// Token: 0x040001A7 RID: 423
		private readonly Dictionary<int, List<Volume>> m_SortedVolumes;

		// Token: 0x040001A8 RID: 424
		private readonly List<Volume> m_Volumes;

		// Token: 0x040001A9 RID: 425
		private readonly Dictionary<int, bool> m_SortNeeded;

		// Token: 0x040001AA RID: 426
		private readonly List<VolumeComponent> m_ComponentsDefaultState;

		// Token: 0x040001AB RID: 427
		private readonly List<Collider> m_TempColliders;
	}
}
