using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000180 RID: 384
	internal class SceneObjectIDMapSceneAsset : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x000543B4 File Offset: 0x000525B4
		public void GetALLIDsFor<TCategory>(TCategory category, List<GameObject> outGameObjects, List<int> outIndices) where TCategory : struct, IConvertible
		{
			if (outGameObjects == null)
			{
				throw new ArgumentNullException("outGameObjects");
			}
			if (outIndices == null)
			{
				throw new ArgumentNullException("outIndices");
			}
			this.CleanDestroyedGameObjects();
			int num = Convert.ToInt32(category);
			for (int i = this.m_Entries.Count - 1; i >= 0; i--)
			{
				if (this.m_Entries[i].category == num)
				{
					outIndices.Add(this.m_Entries[i].id);
					outGameObjects.Add(this.m_Entries[i].gameObject);
				}
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0005444C File Offset: 0x0005264C
		internal bool TryGetSceneIDFor<TCategory>(GameObject gameObject, out int index, out TCategory category) where TCategory : struct, IConvertible
		{
			if (!typeof(TCategory).IsEnum)
			{
				throw new ArgumentException("'TCategory' must be an Enum type.");
			}
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			int num;
			if (this.m_IndexByGameObject.TryGetValue(gameObject, out num))
			{
				if (num < this.m_Entries.Count)
				{
					category = (TCategory)((object)this.m_Entries[num].category);
					index = this.m_Entries[num].id;
					return true;
				}
				this.m_IndexByGameObject.Remove(gameObject);
			}
			category = default(TCategory);
			index = -1;
			return false;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000544F8 File Offset: 0x000526F8
		internal bool TryInsert<TCategory>(GameObject gameObject, TCategory category, out int index) where TCategory : struct, IConvertible
		{
			if (!typeof(TCategory).IsEnum)
			{
				throw new ArgumentException("'TCategory' must be an Enum type.");
			}
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			if (gameObject.scene != base.gameObject.scene)
			{
				index = -1;
				return false;
			}
			TCategory tcategory;
			if (this.TryGetSceneIDFor<TCategory>(gameObject, out index, out tcategory))
			{
				return false;
			}
			index = this.Insert<TCategory>(gameObject, category);
			return true;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0005456C File Offset: 0x0005276C
		private int Insert<TCategory>(GameObject gameObject, TCategory category) where TCategory : struct, IConvertible
		{
			SceneObjectIDMapSceneAsset.Entry entry = new SceneObjectIDMapSceneAsset.Entry
			{
				gameObject = gameObject,
				category = Convert.ToInt32(category)
			};
			int num = -1;
			if (this.m_Entries.Count > 0 && this.m_Entries[0].id != 0)
			{
				num = 0;
				entry.id = 0;
			}
			else
			{
				for (int i = 0; i < this.m_Entries.Count - 1; i++)
				{
					if (this.m_Entries[i].id + 1 != this.m_Entries[i + 1].id)
					{
						num = i + 1;
						entry.id = this.m_Entries[i].id + 1;
						break;
					}
				}
			}
			if (num == -1)
			{
				num = this.m_Entries.Count;
				entry.id = this.m_Entries.Count;
			}
			this.m_IndexByGameObject.Add(gameObject, num);
			this.m_Entries.Insert(num, entry);
			return this.m_Entries[num].id;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00054679 File Offset: 0x00052879
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.BuildIndex();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00054681 File Offset: 0x00052881
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.CleanDestroyedGameObjects();
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0005468C File Offset: 0x0005288C
		private void CleanDestroyedGameObjects()
		{
			bool flag = false;
			for (int i = this.m_Entries.Count - 1; i >= 0; i--)
			{
				if (this.m_Entries[i].gameObject == null)
				{
					this.m_Entries.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.BuildIndex();
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000546E4 File Offset: 0x000528E4
		private void BuildIndex()
		{
			this.m_IndexByGameObject.Clear();
			for (int i = 0; i < this.m_Entries.Count; i++)
			{
				this.m_IndexByGameObject[this.m_Entries[i].gameObject] = i;
			}
		}

		// Token: 0x04001065 RID: 4197
		internal const string k_GameObjectName = "SceneIDMap";

		// Token: 0x04001066 RID: 4198
		[SerializeField]
		private List<SceneObjectIDMapSceneAsset.Entry> m_Entries = new List<SceneObjectIDMapSceneAsset.Entry>();

		// Token: 0x04001067 RID: 4199
		private Dictionary<GameObject, int> m_IndexByGameObject = new Dictionary<GameObject, int>();

		// Token: 0x020002A2 RID: 674
		[Serializable]
		private struct Entry
		{
			// Token: 0x04001728 RID: 5928
			public int id;

			// Token: 0x04001729 RID: 5929
			public int category;

			// Token: 0x0400172A RID: 5930
			public GameObject gameObject;
		}
	}
}
