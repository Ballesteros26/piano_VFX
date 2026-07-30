using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008F RID: 143
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Volume-Profile.html")]
	public sealed class VolumeProfile : ScriptableObject
	{
		// Token: 0x06000371 RID: 881 RVA: 0x0000D92E File Offset: 0x0000BB2E
		private void OnEnable()
		{
			this.components.RemoveAll((VolumeComponent x) => x == null);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D95B File Offset: 0x0000BB5B
		public void Reset()
		{
			this.isDirty = true;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000D964 File Offset: 0x0000BB64
		public T Add<T>(bool overrides = false) where T : VolumeComponent
		{
			return (T)((object)this.Add(typeof(T), overrides));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public VolumeComponent Add(Type type, bool overrides = false)
		{
			if (this.Has(type))
			{
				throw new InvalidOperationException("Component already exists in the volume");
			}
			VolumeComponent volumeComponent = (VolumeComponent)ScriptableObject.CreateInstance(type);
			volumeComponent.SetAllOverridesTo(overrides);
			this.components.Add(volumeComponent);
			this.isDirty = true;
			return volumeComponent;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public void Remove<T>() where T : VolumeComponent
		{
			this.Remove(typeof(T));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public void Remove(Type type)
		{
			int num = -1;
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				this.components.RemoveAt(num);
				this.isDirty = true;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DA31 File Offset: 0x0000BC31
		public bool Has<T>() where T : VolumeComponent
		{
			return this.Has(typeof(T));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public bool Has(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public bool HasSubclassOf(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().IsSubclassOf(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public bool TryGet<T>(out T component) where T : VolumeComponent
		{
			return this.TryGet<T>(typeof(T), out component);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public bool TryGet<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType() == type)
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public bool TryGetSubclassOf<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					component = (T)((object)volumeComponent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public bool TryGetAllSubclassOf<T>(Type type, List<T> result) where T : VolumeComponent
		{
			int count = result.Count;
			foreach (VolumeComponent volumeComponent in this.components)
			{
				if (volumeComponent.GetType().IsSubclassOf(type))
				{
					result.Add((T)((object)volumeComponent));
				}
			}
			return count != result.Count;
		}

		// Token: 0x040001CA RID: 458
		public List<VolumeComponent> components = new List<VolumeComponent>();

		// Token: 0x040001CB RID: 459
		[NonSerialized]
		public bool isDirty = true;
	}
}
