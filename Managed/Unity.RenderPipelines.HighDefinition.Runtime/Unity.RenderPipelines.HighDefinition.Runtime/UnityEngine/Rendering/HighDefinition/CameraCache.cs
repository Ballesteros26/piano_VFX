using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000171 RID: 369
	internal class CameraCache<K> : IDisposable
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00052BA0 File Offset: 0x00050DA0
		public Camera GetOrCreate(K key, int frameCount)
		{
			if (this.m_Cache == null)
			{
				throw new ObjectDisposedException("CameraCache");
			}
			ValueTuple<Camera, int> valueTuple;
			if (!this.m_Cache.TryGetValue(key, out valueTuple) || valueTuple.Item1 == null || valueTuple.Item1.Equals(null))
			{
				valueTuple = new ValueTuple<Camera, int>(new GameObject().AddComponent<Camera>(), frameCount);
				this.m_Cache[key] = valueTuple;
			}
			else
			{
				valueTuple.Item2 = Time.frameCount;
				this.m_Cache[key] = valueTuple;
			}
			return valueTuple.Item1;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00052C2C File Offset: 0x00050E2C
		public void ClearCamerasUnusedFor(int frameWindow, int frameCount)
		{
			if (this.m_Cache == null)
			{
				throw new ObjectDisposedException("CameraCache");
			}
			if (this.cameraKeysCache.Length != this.m_Cache.Count)
			{
				this.cameraKeysCache = new K[this.m_Cache.Count];
			}
			this.m_Cache.Keys.CopyTo(this.cameraKeysCache, 0);
			foreach (K k in this.cameraKeysCache)
			{
				ValueTuple<Camera, int> valueTuple;
				this.m_Cache.TryGetValue(k, out valueTuple);
				if (frameCount - valueTuple.Item2 > frameWindow)
				{
					CoreUtils.Destroy(valueTuple.Item1.gameObject);
					this.m_Cache.Remove(k);
				}
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00052CE4 File Offset: 0x00050EE4
		public void Clear()
		{
			if (this.m_Cache == null)
			{
				throw new ObjectDisposedException("CameraCache");
			}
			foreach (KeyValuePair<K, ValueTuple<Camera, int>> keyValuePair in this.m_Cache)
			{
				CoreUtils.Destroy(keyValuePair.Value.Item1.gameObject);
			}
			this.m_Cache.Clear();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00052D64 File Offset: 0x00050F64
		public void Dispose()
		{
			this.Clear();
			this.m_Cache = null;
		}

		// Token: 0x04001010 RID: 4112
		[TupleElementNames(new string[] { "camera", "lastFrame" })]
		private Dictionary<K, ValueTuple<Camera, int>> m_Cache = new Dictionary<K, ValueTuple<Camera, int>>();

		// Token: 0x04001011 RID: 4113
		private K[] cameraKeysCache = new K[0];
	}
}
