using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000090 RID: 144
	public sealed class VolumeStack : IDisposable
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000268C File Offset: 0x0000088C
		internal VolumeStack()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000DC98 File Offset: 0x0000BE98
		internal void Reload(IEnumerable<Type> baseTypes)
		{
			if (this.components == null)
			{
				this.components = new Dictionary<Type, VolumeComponent>();
			}
			else
			{
				this.components.Clear();
			}
			foreach (Type type in baseTypes)
			{
				VolumeComponent volumeComponent = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.components.Add(type, volumeComponent);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public T GetComponent<T>() where T : VolumeComponent
		{
			return (T)((object)this.GetComponent(typeof(T)));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public VolumeComponent GetComponent(Type type)
		{
			VolumeComponent volumeComponent;
			this.components.TryGetValue(type, out volumeComponent);
			return volumeComponent;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public void Dispose()
		{
			foreach (KeyValuePair<Type, VolumeComponent> keyValuePair in this.components)
			{
				CoreUtils.Destroy(keyValuePair.Value);
			}
			this.components.Clear();
		}

		// Token: 0x040001CC RID: 460
		internal Dictionary<Type, VolumeComponent> components;
	}
}
