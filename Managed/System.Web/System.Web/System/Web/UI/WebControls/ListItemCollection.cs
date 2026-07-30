using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in a list control. This class cannot be inherited.</summary>
	// Token: 0x020003C2 RID: 962
	[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ListItemCollection : IList, ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> class.</summary>
		// Token: 0x060027F2 RID: 10226 RVA: 0x00067D8D File Offset: 0x00065F8D
		public ListItemCollection()
		{
			this.items = new ArrayList();
		}

		/// <summary>Gets or sets the maximum number of items that the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> can store.</summary>
		/// <returns>The maximum number of items that the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> can store.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x00067DA0 File Offset: 0x00065FA0
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x00067DAD File Offset: 0x00065FAD
		public int Capacity
		{
			get
			{
				return this.items.Capacity;
			}
			set
			{
				this.items.Capacity = value;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the collection.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00067DBB File Offset: 0x00065FBB
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00067DC8 File Offset: 0x00065FC8
		public bool IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00067DD5 File Offset: 0x00065FD5
		public bool IsSynchronized
		{
			get
			{
				return this.items.IsSynchronized;
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00067DE2 File Offset: 0x00065FE2
		public object SyncRoot
		{
			get
			{
				return this.items.SyncRoot;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.ListItem" /> at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.ListItem" /> object at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to retrieve from the collection. </param>
		// Token: 0x17000CB5 RID: 3253
		public ListItem this[int index]
		{
			get
			{
				return (ListItem)this.items[index];
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> to the end of the collection.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to append to the collection. </param>
		// Token: 0x060027FA RID: 10234 RVA: 0x00067E02 File Offset: 0x00066002
		public void Add(ListItem item)
		{
			this.items.Add(item);
			if (this.tracking)
			{
				item.TrackViewState();
				this.SetDirty();
			}
		}

		/// <summary>Appends a <see cref="T:System.Web.UI.WebControls.ListItem" /> to the end of the collection that represents the specified string.</summary>
		/// <param name="item">A <see cref="T:System.String" /> that represents the item to add to the end of the collection. </param>
		// Token: 0x060027FB RID: 10235 RVA: 0x00067E28 File Offset: 0x00066028
		public void Add(string item)
		{
			ListItem listItem = new ListItem(item);
			this.items.Add(listItem);
			if (this.tracking)
			{
				listItem.TrackViewState();
				this.SetDirty();
			}
		}

		/// <summary>Adds the items in an array of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects to the collection.</summary>
		/// <param name="items">An array of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects that contain the items to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is null.</exception>
		// Token: 0x060027FC RID: 10236 RVA: 0x00067E60 File Offset: 0x00066060
		public void AddRange(ListItem[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				this.Add(items[i]);
				if (this.tracking)
				{
					items[i].TrackViewState();
					this.SetDirty();
				}
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects from the collection.</summary>
		// Token: 0x060027FD RID: 10237 RVA: 0x00067E9A File Offset: 0x0006609A
		public void Clear()
		{
			this.items.Clear();
			if (this.tracking)
			{
				this.SetDirty();
			}
		}

		/// <summary>Determines whether the collection contains the specified item.</summary>
		/// <returns>true if the collection contains the specified item; otherwise, false.</returns>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.ListItem" /> to search for in the collection. </param>
		// Token: 0x060027FE RID: 10238 RVA: 0x00067EB5 File Offset: 0x000660B5
		public bool Contains(ListItem item)
		{
			return this.items.Contains(item);
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />. </param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
		// Token: 0x060027FF RID: 10239 RVA: 0x00067EC3 File Offset: 0x000660C3
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Searches the collection for a <see cref="T:System.Web.UI.WebControls.ListItem" /> with a <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property that equals the specified text.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that contains the text specified by the <paramref name="text" /> parameter.</returns>
		/// <param name="text">The text to search for. </param>
		// Token: 0x06002800 RID: 10240 RVA: 0x00067ED4 File Offset: 0x000660D4
		public ListItem FindByText(string text)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (text == this[i].Text)
				{
					return this[i];
				}
			}
			return null;
		}

		/// <summary>Searches the collection for a <see cref="T:System.Web.UI.WebControls.ListItem" /> with a <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property that contains the specified value.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that contains the value specified by the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to search for. </param>
		// Token: 0x06002801 RID: 10241 RVA: 0x00067F14 File Offset: 0x00066114
		public ListItem FindByValue(string value)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (value == this[i].Value)
				{
					return this[i];
				}
			}
			return null;
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</returns>
		// Token: 0x06002802 RID: 10242 RVA: 0x00067F54 File Offset: 0x00066154
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Determines the index value that represents the position of the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection.</summary>
		/// <returns>The index position of the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection.</returns>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.ListItem" /> to search for in the collection. </param>
		// Token: 0x06002803 RID: 10243 RVA: 0x00067F61 File Offset: 0x00066161
		public int IndexOf(ListItem item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x00067F70 File Offset: 0x00066170
		internal int IndexOf(string value)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (value == this[i].Value)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection at the specified index location.</summary>
		/// <param name="index">The location in the collection to insert the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to add to the collection. </param>
		// Token: 0x06002805 RID: 10245 RVA: 0x00067FAA File Offset: 0x000661AA
		public void Insert(int index, ListItem item)
		{
			this.items.Insert(index, item);
			if (this.tracking)
			{
				item.TrackViewState();
				this.lastDirty = index;
				this.SetDirty();
			}
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.ListItem" /> which represents the specified string in the collection at the specified index location.</summary>
		/// <param name="index">The location in the collection to insert the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
		/// <param name="item">A <see cref="T:System.String" /> that represents the item to insert in the collection. </param>
		// Token: 0x06002806 RID: 10246 RVA: 0x00067FD4 File Offset: 0x000661D4
		public void Insert(int index, string item)
		{
			ListItem listItem = new ListItem(item);
			this.items.Insert(index, listItem);
			if (this.tracking)
			{
				listItem.TrackViewState();
				this.lastDirty = index;
				this.SetDirty();
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> from the collection.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to remove from the collection. </param>
		// Token: 0x06002807 RID: 10247 RVA: 0x00068010 File Offset: 0x00066210
		public void Remove(ListItem item)
		{
			this.items.Remove(item);
			if (this.tracking)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.ListItem" /> from the collection that represents the specified string.</summary>
		/// <param name="item">A <see cref="T:System.String" /> that represents the item to remove from the collection. </param>
		// Token: 0x06002808 RID: 10248 RVA: 0x0006802C File Offset: 0x0006622C
		public void Remove(string item)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (item == this[i].Value)
				{
					this.items.RemoveAt(i);
					if (this.tracking)
					{
						this.SetDirty();
					}
				}
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.ListItem" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to remove. </param>
		// Token: 0x06002809 RID: 10249 RVA: 0x0006807D File Offset: 0x0006627D
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
			if (this.tracking)
			{
				this.SetDirty();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false. </returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x00068099 File Offset: 0x00066299
		bool IList.IsFixedSize
		{
			get
			{
				return this.items.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element as the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get. </param>
		// Token: 0x17000CB7 RID: 3255
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (index >= 0 && index < this.items.Count)
				{
					this.items[index] = (ListItem)value;
					if (this.tracking)
					{
						((ListItem)value).TrackViewState();
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The index at which the item has been added. </returns>
		/// <param name="item">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600280D RID: 10253 RVA: 0x000680E8 File Offset: 0x000662E8
		int IList.Add(object value)
		{
			int num = this.items.Add((ListItem)value);
			if (this.tracking)
			{
				((IStateManager)value).TrackViewState();
				this.SetDirty();
			}
			return num;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false. </returns>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600280E RID: 10254 RVA: 0x00068114 File Offset: 0x00066314
		bool IList.Contains(object value)
		{
			return this.Contains((ListItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />. </summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1. </returns>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600280F RID: 10255 RVA: 0x00068122 File Offset: 0x00066322
		int IList.IndexOf(object value)
		{
			return this.IndexOf((ListItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />. </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="item">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06002810 RID: 10256 RVA: 0x00068130 File Offset: 0x00066330
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (ListItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />. </summary>
		/// <param name="item">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06002811 RID: 10257 RVA: 0x0006813F File Offset: 0x0006633F
		void IList.Remove(object value)
		{
			this.Remove((ListItem)value);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true if the server control is tracking its view state change; otherwise, false.</returns>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0006814D File Offset: 0x0006634D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</param>
		// Token: 0x06002813 RID: 10259 RVA: 0x00068158 File Offset: 0x00066358
		void IStateManager.LoadViewState(object savedState)
		{
			Pair pair = savedState as Pair;
			if (pair == null)
			{
				return;
			}
			bool flag = (bool)pair.First;
			object[] array = (object[])pair.Second;
			int num = ((array == null) ? 0 : array.Length);
			if (flag)
			{
				if (num > 0)
				{
					this.items = new ArrayList(num);
				}
				else
				{
					this.items = new ArrayList();
				}
			}
			for (int i = 0; i < num; i++)
			{
				ListItem listItem = new ListItem();
				if (flag)
				{
					listItem.LoadViewState(array[i]);
					listItem.SetDirty();
					this.Add(listItem);
				}
				else if (array[i] != null)
				{
					listItem.LoadViewState(array[i]);
					listItem.SetDirty();
					this.items[i] = listItem;
				}
			}
		}

		/// <summary>Returns object containing state changes. </summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</returns>
		// Token: 0x06002814 RID: 10260 RVA: 0x00068210 File Offset: 0x00066410
		object IStateManager.SaveViewState()
		{
			bool flag = false;
			int count = this.items.Count;
			if (count == 0 && !this.dirty)
			{
				return null;
			}
			object[] array = null;
			if (count > 0)
			{
				array = new object[count];
			}
			for (int i = 0; i < count; i++)
			{
				array[i] = ((IStateManager)this.items[i]).SaveViewState();
				if (array[i] != null)
				{
					flag = true;
				}
			}
			if (!this.dirty && !flag)
			{
				return null;
			}
			return new Pair(this.dirty, array);
		}

		/// <summary>Starts tracking state of changes.</summary>
		// Token: 0x06002815 RID: 10261 RVA: 0x00068290 File Offset: 0x00066490
		void IStateManager.TrackViewState()
		{
			this.tracking = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				((ListItem)this.items[i]).TrackViewState();
			}
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000682D0 File Offset: 0x000664D0
		private void SetDirty()
		{
			this.dirty = true;
			for (int i = this.lastDirty; i < this.items.Count; i++)
			{
				((ListItem)this.items[i]).SetDirty();
			}
			this.lastDirty = this.items.Count - 1;
			if (this.lastDirty < 0)
			{
				this.lastDirty = 0;
			}
		}

		// Token: 0x04001A6D RID: 6765
		private ArrayList items;

		// Token: 0x04001A6E RID: 6766
		private bool tracking;

		// Token: 0x04001A6F RID: 6767
		private bool dirty;

		// Token: 0x04001A70 RID: 6768
		private int lastDirty;
	}
}
