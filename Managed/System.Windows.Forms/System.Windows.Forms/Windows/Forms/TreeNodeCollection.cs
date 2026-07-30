using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038B RID: 907
	[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class TreeNodeCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06004246 RID: 16966 RVA: 0x00105CC8 File Offset: 0x00103EC8
		private TreeNodeCollection()
		{
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00105CD0 File Offset: 0x00103ED0
		internal TreeNodeCollection(TreeNode owner)
		{
			this.owner = owner;
			this.nodes = new TreeNode[TreeNodeCollection.OrigSize];
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x00105CFC File Offset: 0x00103EFC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x00105D00 File Offset: 0x00103F00
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x00105D04 File Offset: 0x00103F04
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the tree node at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified index in the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x00105D08 File Offset: 0x00103F08
		// (set) Token: 0x0600424D RID: 16973 RVA: 0x00105D14 File Offset: 0x00103F14
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is TreeNode))
				{
					throw new ArgumentException("Parameter must be of type TreeNode.", "value");
				}
				this[index] = (TreeNode)value;
			}
		}

		/// <summary>Adds an object to the end of the tree node collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the tree node collection.</returns>
		/// <param name="node">The object to add to the tree node collection.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" /> control.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		// Token: 0x0600424E RID: 16974 RVA: 0x00105D4C File Offset: 0x00103F4C
		int IList.Add(object node)
		{
			return this.Add((TreeNode)node);
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <returns>true if <paramref name="node" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="node">The object to find in the collection.</param>
		// Token: 0x0600424F RID: 16975 RVA: 0x00105D5C File Offset: 0x00103F5C
		bool IList.Contains(object node)
		{
			return this.Contains((TreeNode)node);
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
		// Token: 0x06004250 RID: 16976 RVA: 0x00105D6C File Offset: 0x00103F6C
		int IList.IndexOf(object node)
		{
			return this.IndexOf((TreeNode)node);
		}

		/// <summary>Inserts an existing tree node in the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.-or-<paramref name="node" /> is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x06004251 RID: 16977 RVA: 0x00105D7C File Offset: 0x00103F7C
		void IList.Insert(int index, object node)
		{
			this.Insert(index, (TreeNode)node);
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove from the collection.</param>
		// Token: 0x06004252 RID: 16978 RVA: 0x00105D8C File Offset: 0x00103F8C
		void IList.Remove(object node)
		{
			this.Remove((TreeNode)node);
		}

		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</summary>
		/// <returns>The total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x00105D9C File Offset: 0x00103F9C
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x00105DA4 File Offset: 0x00103FA4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</returns>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.TreeNode" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0 or is greater than the number of tree nodes in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001147 RID: 4423
		public virtual TreeNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.SetupNode(value);
				this.nodes[index] = value;
			}
		}

		/// <summary>Gets the tree node with the specified key from the collection. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to retrieve from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001148 RID: 4424
		public virtual TreeNode this[string key]
		{
			get
			{
				for (int i = 0; i < this.count; i++)
				{
					if (string.Compare(key, this.nodes[i].Name, true) == 0)
					{
						return this.nodes[i];
					}
				}
				return null;
			}
		}

		/// <summary>Adds a new tree node with the specified label text to the end of the current tree node collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node being added to the collection.</returns>
		/// <param name="text">The label text displayed by the <see cref="T:System.Windows.Forms.TreeNode" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004258 RID: 16984 RVA: 0x00105E60 File Offset: 0x00104060
		public virtual TreeNode Add(string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Adds a previously created tree node to the end of the tree node collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> added to the tree node collection.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004259 RID: 16985 RVA: 0x00105E80 File Offset: 0x00104080
		public virtual int Add(TreeNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			int num;
			if (treeView != null && treeView.Sorted)
			{
				num = this.AddSorted(node);
			}
			else
			{
				if (this.count >= this.nodes.Length)
				{
					this.Grow();
				}
				this.nodes[this.count] = node;
				num = this.count;
				this.count++;
			}
			this.SetupNode(node);
			if (treeView != null)
			{
				treeView.OnUIACollectionChanged(this.owner, new CollectionChangeEventArgs(1, node));
			}
			return num;
		}

		/// <summary>Creates a new tree node with the specified key and text, and adds it to the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425A RID: 16986 RVA: 0x00105F34 File Offset: 0x00104134
		public virtual TreeNode Add(string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425B RID: 16987 RVA: 0x00105F58 File Offset: 0x00104158
		public virtual TreeNode Add(string key, string text, int imageIndex)
		{
			TreeNode treeNode = this.Add(key, text);
			treeNode.ImageIndex = imageIndex;
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The image to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425C RID: 16988 RVA: 0x00105F78 File Offset: 0x00104178
		public virtual TreeNode Add(string key, string text, string imageKey)
		{
			TreeNode treeNode = this.Add(key, text);
			treeNode.ImageKey = imageKey;
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
		/// <returns>The tree node that was added to the collection.</returns>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <param name="selectedImageIndex">The index of the image to be displayed in the tree node when it is in a selected state.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425D RID: 16989 RVA: 0x00105F98 File Offset: 0x00104198
		public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = this.Add(key, text);
			treeNode.ImageIndex = imageIndex;
			treeNode.SelectedImageIndex = selectedImageIndex;
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <param name="selectedImageKey">The key of the image to display when the node is in a selected state.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425E RID: 16990 RVA: 0x00105FC0 File Offset: 0x001041C0
		public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = this.Add(key, text);
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			return treeNode;
		}

		/// <summary>Adds an array of previously created tree nodes to the collection.</summary>
		/// <param name="nodes">An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects representing the tree nodes to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nodes" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="nodes" /> is the child of another <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600425F RID: 16991 RVA: 0x00105FE8 File Offset: 0x001041E8
		public virtual void AddRange(TreeNode[] nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			for (int i = 0; i < nodes.Length; i++)
			{
				this.Add(nodes[i]);
			}
		}

		/// <summary>Removes all tree nodes from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004260 RID: 16992 RVA: 0x00106024 File Offset: 0x00104224
		public virtual void Clear()
		{
			while (this.count > 0)
			{
				this.RemoveAt(0, false);
			}
			Array.Clear(this.nodes, 0, this.count);
			this.count = 0;
			if (this.owner != null)
			{
				TreeView treeView = this.owner.TreeView;
				if (treeView != null)
				{
					treeView.UpdateBelow(this.owner);
					treeView.RecalculateVisibleOrder(this.owner);
					treeView.UpdateScrollBars(false);
				}
			}
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.TreeNode" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004261 RID: 16993 RVA: 0x001060A4 File Offset: 0x001042A4
		public bool Contains(TreeNode node)
		{
			return Array.IndexOf<TreeNode>(this.nodes, node, 0, this.count) != -1;
		}

		/// <summary>Determines whether the collection contains a tree node with the specified key.</summary>
		/// <returns>true to indicate the collection contains a <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key; otherwise, false. </returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to search for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004262 RID: 16994 RVA: 0x001060C0 File Offset: 0x001042C0
		public virtual bool ContainsKey(string key)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (string.Compare(this.nodes[i].Name, key, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array. </param>
		/// <param name="index">The index in the destination array at which storing begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004263 RID: 16995 RVA: 0x00106108 File Offset: 0x00104308
		public void CopyTo(Array dest, int index)
		{
			Array.Copy(this.nodes, index, dest, index, this.count);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the tree node collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004264 RID: 16996 RVA: 0x00106120 File Offset: 0x00104320
		public IEnumerator GetEnumerator()
		{
			return new TreeNodeCollection.TreeNodeEnumerator(this);
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004265 RID: 16997 RVA: 0x00106128 File Offset: 0x00104328
		public int IndexOf(TreeNode node)
		{
			return Array.IndexOf<TreeNode>(this.nodes, node);
		}

		/// <summary>Returns the index of the first occurrence of a tree node with the specified key.</summary>
		/// <returns>The zero-based index of the first occurrence of a tree node with the specified key, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the tree node to search for.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004266 RID: 16998 RVA: 0x00106138 File Offset: 0x00104338
		public virtual int IndexOfKey(string key)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (string.Compare(this.nodes[i].Name, key, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Creates a tree node with the specified text and inserts it at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004267 RID: 16999 RVA: 0x00106180 File Offset: 0x00104380
		public virtual TreeNode Insert(int index, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Inserts an existing tree node into the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004268 RID: 17000 RVA: 0x001061A0 File Offset: 0x001043A0
		public virtual void Insert(int index, TreeNode node)
		{
			if (this.count >= this.nodes.Length)
			{
				this.Grow();
			}
			Array.Copy(this.nodes, index, this.nodes, index + 1, this.count - index);
			this.nodes[index] = node;
			this.count++;
			this.SetupNode(node);
		}

		/// <summary>Creates a tree node with the specified text and key, and inserts it into the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004269 RID: 17001 RVA: 0x00106204 File Offset: 0x00104404
		public virtual TreeNode Insert(int index, string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426A RID: 17002 RVA: 0x00106228 File Offset: 0x00104428
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426B RID: 17003 RVA: 0x00106254 File Offset: 0x00104454
		public virtual TreeNode Insert(int index, string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <param name="selectedImageIndex">The index of the image to display in the tree node when it is in a selected state.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426C RID: 17004 RVA: 0x00106280 File Offset: 0x00104480
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <param name="selectedImageKey">The key of the image to display in the tree node when it is in a selected state.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426D RID: 17005 RVA: 0x001062A8 File Offset: 0x001044A8
		public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426E RID: 17006 RVA: 0x001062DC File Offset: 0x001044DC
		public void Remove(TreeNode node)
		{
			if (node == null)
			{
				throw new NullReferenceException();
			}
			int num = this.IndexOf(node);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes a tree node from the tree node collection at a specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TreeNode" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600426F RID: 17007 RVA: 0x0010630C File Offset: 0x0010450C
		public virtual void RemoveAt(int index)
		{
			this.RemoveAt(index, true);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x00106318 File Offset: 0x00104518
		private void RemoveAt(int index, bool update)
		{
			TreeNode treeNode = this.nodes[index];
			TreeNode prevNode = this.GetPrevNode(treeNode);
			TreeNode treeNode2 = null;
			bool flag = false;
			bool isVisible = treeNode.IsVisible;
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			if (treeView != null)
			{
				treeView.RecalculateVisibleOrder(prevNode);
				if (treeNode == treeView.SelectedNode)
				{
					flag = true;
					OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
					if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
					{
						treeNode2 = openTreeNodeEnumerator.CurrentNode;
					}
					else
					{
						openTreeNodeEnumerator = new OpenTreeNodeEnumerator(treeNode);
						openTreeNodeEnumerator.MovePrevious();
						treeNode2 = ((openTreeNodeEnumerator.CurrentNode != treeNode) ? openTreeNodeEnumerator.CurrentNode : null);
					}
				}
			}
			Array.Copy(this.nodes, index + 1, this.nodes, index, this.count - index - 1);
			this.count--;
			this.nodes[this.count] = null;
			if (this.nodes.Length > TreeNodeCollection.OrigSize && this.nodes.Length > this.count * 2)
			{
				this.Shrink();
			}
			if (treeView != null && flag)
			{
				treeView.SelectedNode = treeNode2;
			}
			TreeNode parent = treeNode.parent;
			treeNode.parent = null;
			if (update && treeView != null && isVisible)
			{
				treeView.RecalculateVisibleOrder(prevNode);
				treeView.UpdateScrollBars(false);
				treeView.UpdateBelow(parent);
			}
			if (treeView != null)
			{
				treeView.OnUIACollectionChanged(this.owner, new CollectionChangeEventArgs(2, treeNode));
			}
		}

		/// <summary>Removes the tree node with the specified key from the collection.</summary>
		/// <param name="key">The name of the tree node to remove from the collection.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004271 RID: 17009 RVA: 0x001064A8 File Offset: 0x001046A8
		public virtual void RemoveByKey(string key)
		{
			TreeNode treeNode = this[key];
			if (treeNode != null)
			{
				this.Remove(treeNode);
			}
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x001064CC File Offset: 0x001046CC
		private TreeNode GetPrevNode(TreeNode node)
		{
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(node);
			if (openTreeNodeEnumerator.MovePrevious() && openTreeNodeEnumerator.MovePrevious())
			{
				return openTreeNodeEnumerator.CurrentNode;
			}
			return null;
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x00106500 File Offset: 0x00104700
		private void SetupNode(TreeNode node)
		{
			node.Remove();
			node.parent = this.owner;
			TreeView treeView = null;
			if (this.owner != null)
			{
				treeView = this.owner.TreeView;
			}
			if (treeView != null)
			{
				TreeNode prevNode = this.GetPrevNode(node);
				if (treeView.IsHandleCreated && node.ArePreviousNodesExpanded)
				{
					treeView.RecalculateVisibleOrder(prevNode);
				}
				if (this.owner == treeView.root_node || (node.Parent.IsVisible && node.Parent.IsExpanded))
				{
					treeView.UpdateScrollBars(false);
				}
			}
			if (this.owner != null && treeView != null && (this.owner.IsExpanded || this.owner.IsRoot))
			{
				treeView.UpdateBelow(this.owner);
			}
			else if (this.owner != null && treeView != null)
			{
				treeView.UpdateBelow(this.owner);
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x001065FC File Offset: 0x001047FC
		private int AddSorted(TreeNode node)
		{
			if (this.count >= this.nodes.Length)
			{
				this.Grow();
			}
			CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
			int num = 0;
			bool flag = false;
			for (int i = 0; i < this.count; i++)
			{
				num = i;
				int num2 = compareInfo.Compare(node.Text, this.nodes[i].Text);
				if (num2 < 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num = this.count;
			}
			for (int j = this.count - 1; j >= num; j--)
			{
				this.nodes[j + 1] = this.nodes[j];
			}
			this.count++;
			this.nodes[num] = node;
			return this.count;
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x001066D0 File Offset: 0x001048D0
		internal void Sort(IComparer sorter)
		{
			Array array = this.nodes;
			int num = 0;
			int num2 = this.count;
			IComparer comparer2;
			if (sorter == null)
			{
				IComparer comparer = new TreeNodeCollection.TreeNodeComparer(Application.CurrentCulture.CompareInfo);
				comparer2 = comparer;
			}
			else
			{
				comparer2 = sorter;
			}
			Array.Sort(array, num, num2, comparer2);
			for (int i = 0; i < this.count; i++)
			{
				this.nodes[i].Nodes.Sort(sorter);
			}
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x00106738 File Offset: 0x00104938
		private void Grow()
		{
			TreeNode[] array = new TreeNode[this.nodes.Length + 50];
			Array.Copy(this.nodes, array, this.nodes.Length);
			this.nodes = array;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x00106774 File Offset: 0x00104974
		private void Shrink()
		{
			int num = ((this.count + 1 <= TreeNodeCollection.OrigSize) ? TreeNodeCollection.OrigSize : (this.count + 1));
			TreeNode[] array = new TreeNode[num];
			Array.Copy(this.nodes, array, this.count);
			this.nodes = array;
		}

		/// <summary>Finds the tree nodes with specified key, optionally searching subnodes.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects whose <see cref="P:System.Windows.Forms.TreeNode.Name" /> property matches the specified key.</returns>
		/// <param name="key">The name of the tree node to search for.</param>
		/// <param name="searchAllChildren">true to search child nodes of tree nodes; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004278 RID: 17016 RVA: 0x001067C8 File Offset: 0x001049C8
		public TreeNode[] Find(string key, bool searchAllChildren)
		{
			List<TreeNode> list = new List<TreeNode>(0);
			TreeNodeCollection.Find(key, searchAllChildren, this, list);
			return list.ToArray();
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x001067EC File Offset: 0x001049EC
		private static void Find(string key, bool searchAllChildren, TreeNodeCollection nodes, List<TreeNode> results)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				TreeNode treeNode = nodes[i];
				if (string.Compare(treeNode.Name, key, true, CultureInfo.InvariantCulture) == 0)
				{
					results.Add(treeNode);
				}
			}
			if (searchAllChildren)
			{
				for (int j = 0; j < nodes.Count; j++)
				{
					TreeNodeCollection treeNodeCollection = nodes[j].Nodes;
					if (treeNodeCollection.Count > 0)
					{
						TreeNodeCollection.Find(key, searchAllChildren, treeNodeCollection, results);
					}
				}
			}
		}

		// Token: 0x04001BE5 RID: 7141
		private static readonly int OrigSize = 50;

		// Token: 0x04001BE6 RID: 7142
		private TreeNode owner;

		// Token: 0x04001BE7 RID: 7143
		private int count;

		// Token: 0x04001BE8 RID: 7144
		private TreeNode[] nodes;

		// Token: 0x0200038C RID: 908
		internal class TreeNodeEnumerator : IEnumerator
		{
			// Token: 0x0600427A RID: 17018 RVA: 0x00106878 File Offset: 0x00104A78
			public TreeNodeEnumerator(TreeNodeCollection collection)
			{
				this.collection = collection;
			}

			// Token: 0x17001149 RID: 4425
			// (get) Token: 0x0600427B RID: 17019 RVA: 0x00106890 File Offset: 0x00104A90
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x0600427C RID: 17020 RVA: 0x001068B4 File Offset: 0x00104AB4
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x0600427D RID: 17021 RVA: 0x001068E0 File Offset: 0x00104AE0
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04001BE9 RID: 7145
			private TreeNodeCollection collection;

			// Token: 0x04001BEA RID: 7146
			private int index = -1;
		}

		// Token: 0x0200038D RID: 909
		private class TreeNodeComparer : IComparer
		{
			// Token: 0x0600427E RID: 17022 RVA: 0x001068EC File Offset: 0x00104AEC
			public TreeNodeComparer(CompareInfo compare)
			{
				this.compare = compare;
			}

			// Token: 0x0600427F RID: 17023 RVA: 0x001068FC File Offset: 0x00104AFC
			public int Compare(object x, object y)
			{
				TreeNode treeNode = (TreeNode)x;
				TreeNode treeNode2 = (TreeNode)y;
				int num = this.compare.Compare(treeNode.Text, treeNode2.Text);
				return (num != 0) ? num : (treeNode.Index - treeNode2.Index);
			}

			// Token: 0x04001BEB RID: 7147
			private CompareInfo compare;
		}
	}
}
