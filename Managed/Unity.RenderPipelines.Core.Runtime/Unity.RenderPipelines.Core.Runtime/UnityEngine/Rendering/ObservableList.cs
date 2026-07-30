using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002D RID: 45
	public class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000F4 RID: 244 RVA: 0x00005544 File Offset: 0x00003744
		// (remove) Token: 0x060000F5 RID: 245 RVA: 0x0000557C File Offset: 0x0000377C
		public event ListChangedEventHandler<T> ItemAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000F6 RID: 246 RVA: 0x000055B4 File Offset: 0x000037B4
		// (remove) Token: 0x060000F7 RID: 247 RVA: 0x000055EC File Offset: 0x000037EC
		public event ListChangedEventHandler<T> ItemRemoved;

		// Token: 0x17000015 RID: 21
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				this.OnEvent(this.ItemRemoved, index, this.m_List[index]);
				this.m_List[index] = value;
				this.OnEvent(this.ItemAdded, index, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005665 File Offset: 0x00003865
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005672 File Offset: 0x00003872
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005675 File Offset: 0x00003875
		public ObservableList()
			: this(0)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000567E File Offset: 0x0000387E
		public ObservableList(int capacity)
		{
			this.m_List = new List<T>(capacity);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005692 File Offset: 0x00003892
		public ObservableList(IEnumerable<T> collection)
		{
			this.m_List = new List<T>(collection);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000056A6 File Offset: 0x000038A6
		private void OnEvent(ListChangedEventHandler<T> e, int index, T item)
		{
			if (e != null)
			{
				e(this, new ListChangedEventArgs<T>(index, item));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000056B9 File Offset: 0x000038B9
		public bool Contains(T item)
		{
			return this.m_List.Contains(item);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000056C7 File Offset: 0x000038C7
		public int IndexOf(T item)
		{
			return this.m_List.IndexOf(item);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000056D5 File Offset: 0x000038D5
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.OnEvent(this.ItemAdded, this.m_List.IndexOf(item), item);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000056FC File Offset: 0x000038FC
		public void Add(params T[] items)
		{
			foreach (T t in items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005728 File Offset: 0x00003928
		public void Insert(int index, T item)
		{
			this.m_List.Insert(index, item);
			this.OnEvent(this.ItemAdded, index, item);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005748 File Offset: 0x00003948
		public bool Remove(T item)
		{
			int num = this.m_List.IndexOf(item);
			bool flag = this.m_List.Remove(item);
			if (flag)
			{
				this.OnEvent(this.ItemRemoved, num, item);
			}
			return flag;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005780 File Offset: 0x00003980
		public int Remove(params T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T t in items)
			{
				num += (this.Remove(t) ? 1 : 0);
			}
			return num;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057C0 File Offset: 0x000039C0
		public void RemoveAt(int index)
		{
			T t = this.m_List[index];
			this.m_List.RemoveAt(index);
			this.OnEvent(this.ItemRemoved, index, t);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000057F4 File Offset: 0x000039F4
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005819 File Offset: 0x00003A19
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005828 File Offset: 0x00003A28
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005835 File Offset: 0x00003A35
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000C0 RID: 192
		private IList<T> m_List;
	}
}
