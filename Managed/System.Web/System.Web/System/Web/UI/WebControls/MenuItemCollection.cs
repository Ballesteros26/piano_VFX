using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of menu items in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003D4 RID: 980
	public sealed class MenuItemCollection : ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> class using the default values.</summary>
		// Token: 0x06002A2A RID: 10794 RVA: 0x0006E886 File Offset: 0x0006CA86
		public MenuItemCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> class using the specified parent menu item (or owner).</summary>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the parent menu item of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
		// Token: 0x06002A2B RID: 10795 RVA: 0x0006E899 File Offset: 0x0006CA99
		public MenuItemCollection(MenuItem owner)
		{
			this.parent = owner;
			this.menu = owner.Menu;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0006E8BF File Offset: 0x0006CABF
		internal MenuItemCollection(Menu menu)
		{
			this.menu = menu;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0006E8DC File Offset: 0x0006CADC
		internal void SetMenu(Menu menu)
		{
			this.menu = menu;
			foreach (object obj in this.items)
			{
				((MenuItem)obj).Menu = menu;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object at the specified index in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItem" /> at the specified index in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> to retrieve.</param>
		// Token: 0x17000D81 RID: 3457
		public MenuItem this[int index]
		{
			get
			{
				return (MenuItem)this.items[index];
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to the end of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to append to the end of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="child" /> is null.</exception>
		// Token: 0x06002A2F RID: 10799 RVA: 0x0006E94F File Offset: 0x0006CB4F
		public void Add(MenuItem child)
		{
			child.Index = this.items.Add(child);
			child.Menu = this.menu;
			child.SetParent(this.parent);
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0006E990 File Offset: 0x0006CB90
		internal void SetDirty()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetDirty();
			}
			this.dirty = true;
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to insert.</param>
		// Token: 0x06002A31 RID: 10801 RVA: 0x0006E9C4 File Offset: 0x0006CBC4
		public void AddAt(int index, MenuItem child)
		{
			this.items.Insert(index, child);
			child.Index = index;
			child.Menu = this.menu;
			child.SetParent(this.parent);
			for (int i = index + 1; i < this.items.Count; i++)
			{
				((MenuItem)this.items[i]).Index = i;
			}
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		/// <summary>Removes all items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		// Token: 0x06002A32 RID: 10802 RVA: 0x0006EA40 File Offset: 0x0006CC40
		public void Clear()
		{
			if (this.menu != null || this.parent != null)
			{
				foreach (object obj in this.items)
				{
					MenuItem menuItem = (MenuItem)obj;
					menuItem.Menu = null;
					menuItem.SetParent(null);
				}
			}
			this.items.Clear();
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="c">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to find.</param>
		// Token: 0x06002A33 RID: 10803 RVA: 0x0006EAC8 File Offset: 0x0006CCC8
		public bool Contains(MenuItem c)
		{
			return this.items.Contains(c);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not an array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects.</exception>
		// Token: 0x06002A34 RID: 10804 RVA: 0x0006EAD6 File Offset: 0x0006CCD6
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06002A35 RID: 10805 RVA: 0x0006EAD6 File Offset: 0x0006CCD6
		public void CopyTo(MenuItem[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the items in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <returns>An enumerator that can be used to iterate through the items in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
		// Token: 0x06002A36 RID: 10806 RVA: 0x0006EAE5 File Offset: 0x0006CCE5
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to locate.</param>
		// Token: 0x06002A37 RID: 10807 RVA: 0x0006EAF2 File Offset: 0x0006CCF2
		public int IndexOf(MenuItem value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06002A38 RID: 10808 RVA: 0x0006EB00 File Offset: 0x0006CD00
		public void Remove(MenuItem value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return;
			}
			this.items.RemoveAt(num);
			if (this.menu != null)
			{
				value.Menu = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object at the specified index location from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the menu item to remove.</param>
		// Token: 0x06002A39 RID: 10809 RVA: 0x0006EB44 File Offset: 0x0006CD44
		public void RemoveAt(int index)
		{
			MenuItem menuItem = (MenuItem)this.items[index];
			this.items.RemoveAt(index);
			if (this.menu != null)
			{
				menuItem.Menu = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Gets the number of menu items contained in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <returns>The number of menu items contained in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</returns>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x0006EB8C File Offset: 0x0006CD8C
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x0006EB99 File Offset: 0x0006CD99
		public object SyncRoot
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0006EAD6 File Offset: 0x0006CCD6
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Loads the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
		// Token: 0x06002A3E RID: 10814 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
		void IStateManager.LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			this.dirty = (bool)array[0];
			if (this.dirty)
			{
				this.items.Clear();
				for (int i = 1; i < array.Length; i++)
				{
					MenuItem menuItem = new MenuItem();
					this.Add(menuItem);
					object obj = array[i];
					if (obj != null)
					{
						((IStateManager)menuItem).LoadViewState(obj);
					}
				}
				return;
			}
			for (int j = 1; j < array.Length; j++)
			{
				Pair pair = (Pair)array[j];
				int num = (int)pair.First;
				((IStateManager)((MenuItem)this.items[num])).LoadViewState(pair.Second);
			}
		}

		/// <summary>Saves the changes to view state to an <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06002A3F RID: 10815 RVA: 0x0006EC50 File Offset: 0x0006CE50
		object IStateManager.SaveViewState()
		{
			object[] array = null;
			bool flag = false;
			if (this.dirty)
			{
				if (this.items.Count > 0)
				{
					flag = true;
					array = new object[this.items.Count + 1];
					array[0] = true;
					for (int i = 0; i < this.items.Count; i++)
					{
						object obj = ((IStateManager)(this.items[i] as MenuItem)).SaveViewState();
						array[i + 1] = obj;
					}
				}
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < this.items.Count; j++)
				{
					object obj2 = ((IStateManager)(this.items[j] as MenuItem)).SaveViewState();
					if (obj2 != null)
					{
						flag = true;
						arrayList.Add(new Pair(j, obj2));
					}
				}
				if (flag)
				{
					arrayList.Insert(0, false);
					array = arrayList.ToArray();
				}
			}
			if (flag)
			{
				return array;
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to track changes to its view state.</summary>
		// Token: 0x06002A40 RID: 10816 RVA: 0x0006ED44 File Offset: 0x0006CF44
		void IStateManager.TrackViewState()
		{
			this.marked = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				((IStateManager)this.items[i]).TrackViewState();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x0006ED84 File Offset: 0x0006CF84
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x04001AD1 RID: 6865
		private ArrayList items = new ArrayList();

		// Token: 0x04001AD2 RID: 6866
		private Menu menu;

		// Token: 0x04001AD3 RID: 6867
		private MenuItem parent;

		// Token: 0x04001AD4 RID: 6868
		private bool marked;

		// Token: 0x04001AD5 RID: 6869
		private bool dirty;
	}
}
