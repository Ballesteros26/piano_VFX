using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010C RID: 268
	internal class TreeViewItem<T> : ITreeViewItem
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0002113C File Offset: 0x0001F33C
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00021144 File Offset: 0x0001F344
		public int id { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0002114D File Offset: 0x0001F34D
		public ITreeViewItem parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00021158 File Offset: 0x0001F358
		public IEnumerable<ITreeViewItem> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00021170 File Offset: 0x0001F370
		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0002119B File Offset: 0x0001F39B
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x000211A3 File Offset: 0x0001F3A3
		public T data { get; private set; }

		// Token: 0x06000816 RID: 2070 RVA: 0x000211AC File Offset: 0x0001F3AC
		public TreeViewItem(int id, T data, List<TreeViewItem<T>> children = null)
		{
			this.id = id;
			this.data = data;
			bool flag = children != null;
			if (flag)
			{
				foreach (TreeViewItem<T> treeViewItem in children)
				{
					this.AddChild(treeViewItem);
				}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00021220 File Offset: 0x0001F420
		public void AddChild(ITreeViewItem child)
		{
			TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
			bool flag = treeViewItem == null;
			if (!flag)
			{
				bool flag2 = this.m_Children == null;
				if (flag2)
				{
					this.m_Children = new List<ITreeViewItem>();
				}
				this.m_Children.Add(treeViewItem);
				treeViewItem.m_Parent = this;
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002126C File Offset: 0x0001F46C
		public void AddChildren(IList<ITreeViewItem> children)
		{
			foreach (ITreeViewItem treeViewItem in children)
			{
				this.AddChild(treeViewItem);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000212B8 File Offset: 0x0001F4B8
		public void RemoveChild(ITreeViewItem child)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				TreeViewItem<T> treeViewItem = child as TreeViewItem<T>;
				bool flag2 = treeViewItem == null;
				if (!flag2)
				{
					this.m_Children.Remove(treeViewItem);
				}
			}
		}

		// Token: 0x040003A5 RID: 933
		internal TreeViewItem<T> m_Parent;

		// Token: 0x040003A6 RID: 934
		private List<ITreeViewItem> m_Children;
	}
}
