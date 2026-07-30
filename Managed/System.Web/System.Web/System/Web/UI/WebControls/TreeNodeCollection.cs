using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200042F RID: 1071
	public sealed class TreeNodeCollection : ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the default values.</summary>
		// Token: 0x060030E1 RID: 12513 RVA: 0x00080A8A File Offset: 0x0007EC8A
		public TreeNodeCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> class using the specified parent node (or owner).</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object that represents the parent node of the collection. </param>
		// Token: 0x060030E2 RID: 12514 RVA: 0x00080A9D File Offset: 0x0007EC9D
		public TreeNodeCollection(TreeNode owner)
		{
			this.parent = owner;
			this.tree = owner.Tree;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x00080AC3 File Offset: 0x0007ECC3
		internal TreeNodeCollection(TreeView tree)
		{
			this.tree = tree;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x00080AE0 File Offset: 0x0007ECE0
		internal void SetTree(TreeView tree)
		{
			this.tree = tree;
			foreach (object obj in this.items)
			{
				((TreeNode)obj).Tree = tree;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to retrieve. </param>
		// Token: 0x17000F87 RID: 3975
		public TreeNode this[int index]
		{
			get
			{
				return (TreeNode)this.items[index];
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to append. </param>
		// Token: 0x060030E6 RID: 12518 RVA: 0x00080B53 File Offset: 0x0007ED53
		public void Add(TreeNode child)
		{
			this.Add(child, true);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x00080B60 File Offset: 0x0007ED60
		internal void Add(TreeNode child, bool updateParent)
		{
			int num = this.items.Add(child);
			if (this.parent != null)
			{
				this.parent.HadChildrenBeforePopulating = true;
			}
			if (!updateParent)
			{
				return;
			}
			child.Index = num;
			child.SetParent(this.parent);
			child.Tree = this.tree;
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object in a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="child" /> is null.</exception>
		// Token: 0x060030E8 RID: 12520 RVA: 0x00080BC8 File Offset: 0x0007EDC8
		public void AddAt(int index, TreeNode child)
		{
			this.items.Insert(index, child);
			child.Index = index;
			child.SetParent(this.parent);
			child.Tree = this.tree;
			for (int i = index + 1; i < this.items.Count; i++)
			{
				((TreeNode)this.items[i]).Index = i;
			}
			if (this.marked)
			{
				((IStateManager)child).TrackViewState();
				this.SetDirty();
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x00080C44 File Offset: 0x0007EE44
		internal void SetDirty()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetDirty();
			}
			this.dirty = true;
		}

		/// <summary>Empties the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		// Token: 0x060030EA RID: 12522 RVA: 0x00080C78 File Offset: 0x0007EE78
		public void Clear()
		{
			if (this.tree != null || this.parent != null)
			{
				foreach (object obj in this.items)
				{
					TreeNode treeNode = (TreeNode)obj;
					treeNode.Tree = null;
					treeNode.SetParent(null);
				}
			}
			this.items.Clear();
			if (this.marked)
			{
				this.dirty = true;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="c">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to find. </param>
		// Token: 0x060030EB RID: 12523 RVA: 0x00080D00 File Offset: 0x0007EF00
		public bool Contains(TreeNode c)
		{
			return this.items.Contains(c);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="nodeArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x060030EC RID: 12524 RVA: 0x00080D0E File Offset: 0x0007EF0E
		public void CopyTo(TreeNode[] nodeArray, int index)
		{
			this.items.CopyTo(nodeArray, index);
		}

		/// <summary>Returns an enumerator that can be used to iterate through a <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>An enumerator that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x060030ED RID: 12525 RVA: 0x00080D1D File Offset: 0x0007EF1D
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to locate. </param>
		// Token: 0x060030EE RID: 12526 RVA: 0x00080D2A File Offset: 0x0007EF2A
		public int IndexOf(TreeNode value)
		{
			return this.items.IndexOf(value);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.TreeNode" /> object to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060030EF RID: 12527 RVA: 0x00080D38 File Offset: 0x0007EF38
		public void Remove(TreeNode value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return;
			}
			this.items.RemoveAt(num);
			if (this.tree != null)
			{
				value.Tree = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNode" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the node to remove. </param>
		// Token: 0x060030F0 RID: 12528 RVA: 0x00080D7C File Offset: 0x0007EF7C
		public void RemoveAt(int index)
		{
			TreeNode treeNode = (TreeNode)this.items[index];
			this.items.RemoveAt(index);
			if (this.tree != null)
			{
				treeNode.Tree = null;
			}
			if (this.marked)
			{
				this.SetDirty();
			}
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x00080DC4 File Offset: 0x0007EFC4
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />.</returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x00080DD1 File Offset: 0x0007EFD1
		public object SyncRoot
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> object that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" />. </param>
		/// <param name="index">The position in the target array at which to start receiving the copied content. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not an array of <see cref="T:System.Web.UI.WebControls.TreeNode" /> objects.</exception>
		// Token: 0x060030F4 RID: 12532 RVA: 0x00080D0E File Offset: 0x0007EF0E
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Loads the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object's previously saved view state.</summary>
		/// <param name="state">A <see cref="T:System.Object" /> that contains the saved view state values. </param>
		// Token: 0x060030F5 RID: 12533 RVA: 0x00080DDC File Offset: 0x0007EFDC
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
					Pair pair = array[i] as Pair;
					if (pair == null)
					{
						throw new InvalidOperationException("Broken view state (item " + i + ")");
					}
					TreeNode treeNode;
					if (pair.First as Type == null)
					{
						treeNode = new TreeNode();
					}
					else
					{
						treeNode = Activator.CreateInstance(pair.First as Type) as TreeNode;
					}
					this.Add(treeNode);
					object second = pair.Second;
					if (second != null)
					{
						((IStateManager)treeNode).LoadViewState(second);
					}
				}
				return;
			}
			for (int j = 1; j < array.Length; j++)
			{
				Pair pair2 = array[j] as Pair;
				if (pair2 == null)
				{
					throw new InvalidOperationException("Broken view state " + j + ")");
				}
				int num = (int)pair2.First;
				((IStateManager)((TreeNode)this.items[num])).LoadViewState(pair2.Second);
			}
		}

		/// <summary>Saves the changes to view state to a <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060030F6 RID: 12534 RVA: 0x00080F04 File Offset: 0x0007F104
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
						TreeNode treeNode = this.items[i] as TreeNode;
						object obj = ((IStateManager)treeNode).SaveViewState();
						Type type = treeNode.GetType();
						array[i + 1] = new Pair((type == typeof(TreeNode)) ? null : type, obj);
					}
				}
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				for (int j = 0; j < this.items.Count; j++)
				{
					object obj2 = ((IStateManager)(this.items[j] as TreeNode)).SaveViewState();
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

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> to track changes to its view state.</summary>
		// Token: 0x060030F7 RID: 12535 RVA: 0x00081020 File Offset: 0x0007F220
		void IStateManager.TrackViewState()
		{
			this.marked = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				((IStateManager)this.items[i]).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TreeNodeCollection" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x00081060 File Offset: 0x0007F260
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x04001C1B RID: 7195
		private ArrayList items = new ArrayList();

		// Token: 0x04001C1C RID: 7196
		private TreeView tree;

		// Token: 0x04001C1D RID: 7197
		private TreeNode parent;

		// Token: 0x04001C1E RID: 7198
		private bool marked;

		// Token: 0x04001C1F RID: 7199
		private bool dirty;
	}
}
