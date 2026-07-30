using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Collections
{
	// Token: 0x02000047 RID: 71
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00015F21 File Offset: 0x00014121
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015F48 File Offset: 0x00014148
		public bool AddUnique(T item)
		{
			if (this.m_Dictionary.ContainsKey(item))
			{
				return false;
			}
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
			return true;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015F80 File Offset: 0x00014180
		public bool Remove(T item)
		{
			int num = -1;
			if (!this.m_Dictionary.TryGetValue(item, out num))
			{
				return false;
			}
			this.RemoveAt(num);
			return true;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015FA9 File Offset: 0x000141A9
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00015FB0 File Offset: 0x000141B0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015FB8 File Offset: 0x000141B8
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Dictionary.Clear();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015FD0 File Offset: 0x000141D0
		public bool Contains(T item)
		{
			return this.m_Dictionary.ContainsKey(item);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015FDE File Offset: 0x000141DE
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00015FED File Offset: 0x000141ED
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008CC2 File Offset: 0x00006EC2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015FFC File Offset: 0x000141FC
		public int IndexOf(T item)
		{
			int num = -1;
			if (this.m_Dictionary.TryGetValue(item, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001601E File Offset: 0x0001421E
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001602C File Offset: 0x0001422C
		public void RemoveAt(int index)
		{
			T t = this.m_List[index];
			this.m_Dictionary.Remove(t);
			if (index == this.m_List.Count - 1)
			{
				this.m_List.RemoveAt(index);
				return;
			}
			int num = this.m_List.Count - 1;
			T t2 = this.m_List[num];
			this.m_List[index] = t2;
			this.m_Dictionary[t2] = index;
			this.m_List.RemoveAt(num);
		}

		// Token: 0x17000147 RID: 327
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				T t = this.m_List[index];
				this.m_Dictionary.Remove(t);
				this.m_List[index] = value;
				this.m_Dictionary.Add(t, index);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00016104 File Offset: 0x00014304
		public void RemoveAll(Predicate<T> match)
		{
			int i = 0;
			while (i < this.m_List.Count)
			{
				T t = this.m_List[i];
				if (match(t))
				{
					this.Remove(t);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016148 File Offset: 0x00014348
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.m_List.Sort(sortLayoutFunction);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				T t = this.m_List[i];
				this.m_Dictionary[t] = i;
			}
		}

		// Token: 0x0400018C RID: 396
		private readonly List<T> m_List = new List<T>();

		// Token: 0x0400018D RID: 397
		private Dictionary<T, int> m_Dictionary = new Dictionary<T, int>();
	}
}
