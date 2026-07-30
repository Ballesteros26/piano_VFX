using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class SerializedDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005840 File Offset: 0x00003A40
		public void OnBeforeSerialize()
		{
			this.m_Keys.Clear();
			this.m_Values.Clear();
			foreach (KeyValuePair<K, V> keyValuePair in this)
			{
				this.m_Keys.Add(keyValuePair.Key);
				this.m_Values.Add(keyValuePair.Value);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000058C4 File Offset: 0x00003AC4
		public void OnAfterDeserialize()
		{
			for (int i = 0; i < this.m_Keys.Count; i++)
			{
				base.Add(this.m_Keys[i], this.m_Values[i]);
			}
			this.m_Keys.Clear();
			this.m_Values.Clear();
		}

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private List<K> m_Keys = new List<K>();

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		private List<V> m_Values = new List<V>();
	}
}
