using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000359 RID: 857
	[ListBindable(false)]
	[Editor("System.Windows.Forms.Design.ToolStripCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class ToolStripItemCollection : ArrangedElementCollection, ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> class with the specified container <see cref="T:System.Windows.Forms.ToolStrip" /> and the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStrip" /> to which this <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> belongs. </param>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the initial controls for this <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="owner" /> parameter is null.</exception>
		// Token: 0x06003DFE RID: 15870 RVA: 0x000F7860 File Offset: 0x000F5A60
		public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (value == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			this.owner = owner;
			foreach (ToolStripItem toolStripItem in value)
			{
				this.AddNoOwnerOrLayout(toolStripItem);
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000F78C0 File Offset: 0x000F5AC0
		internal ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value, bool internalcreated)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.internal_created = internalcreated;
			this.owner = owner;
			if (value != null)
			{
				foreach (ToolStripItem toolStripItem in value)
				{
					this.AddNoOwnerOrLayout(toolStripItem);
				}
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The location at which <paramref name="value" /> was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		// Token: 0x06003E00 RID: 15872 RVA: 0x000F791C File Offset: 0x000F5B1C
		int IList.Add(object value)
		{
			return this.Add((ToolStripItem)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06003E01 RID: 15873 RVA: 0x000F792C File Offset: 0x000F5B2C
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines if the collection contains a specified item.</summary>
		/// <returns>true if <paramref name="value" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06003E02 RID: 15874 RVA: 0x000F7934 File Offset: 0x000F5B34
		bool IList.Contains(object value)
		{
			return this.Contains((ToolStripItem)value);
		}

		/// <summary>Determines the location of a specified item in the collection.</summary>
		/// <returns>The index of the item in the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06003E03 RID: 15875 RVA: 0x000F7944 File Offset: 0x000F5B44
		int IList.IndexOf(object value)
		{
			return this.IndexOf((ToolStripItem)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The item to insert into the collection.</param>
		// Token: 0x06003E04 RID: 15876 RVA: 0x000F7954 File Offset: 0x000F5B54
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (ToolStripItem)value);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x000F7964 File Offset: 0x000F5B64
		bool IList.IsFixedSize
		{
			get
			{
				return base.IsFixedSize;
			}
		}

		/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		// Token: 0x06003E06 RID: 15878 RVA: 0x000F796C File Offset: 0x000F5B6C
		void IList.Remove(object value)
		{
			this.Remove((ToolStripItem)value);
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06003E07 RID: 15879 RVA: 0x000F797C File Offset: 0x000F5B7C
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Retrieves the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get.</param>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x000F7988 File Offset: 0x000F5B88
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x000F7994 File Offset: 0x000F5B94
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000F799C File Offset: 0x000F5B9C
		public override bool IsReadOnly
		{
			get
			{
				return base.IsReadOnly;
			}
		}

		/// <summary>Gets the item at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified position in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</returns>
		/// <param name="index">The zero-based index of the item to get.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700103C RID: 4156
		public virtual ToolStripItem this[int index]
		{
			get
			{
				return (ToolStripItem)base[index];
			}
		}

		/// <summary>Gets the item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified name.</returns>
		/// <param name="key">The name of the item to get.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700103D RID: 4157
		public virtual ToolStripItem this[string key]
		{
			get
			{
				foreach (object obj in this)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.Name == key)
					{
						return toolStripItem;
					}
				}
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image to the collection.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06003E0D RID: 15885 RVA: 0x000F7A34 File Offset: 0x000F5C34
		public ToolStripItem Add(Image image)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(string.Empty, image, null);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified text to the collection.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06003E0E RID: 15886 RVA: 0x000F7A60 File Offset: 0x000F5C60
		public ToolStripItem Add(string text)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, null, null);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds the specified item to the end of the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to add to the end of the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E0F RID: 15887 RVA: 0x000F7A88 File Offset: 0x000F5C88
		public int Add(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			value.InternalOwner = this.owner;
			if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != Keys.None)
			{
				ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
			}
			int num = base.Add(value);
			if (this.internal_created)
			{
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			return num;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06003E10 RID: 15888 RVA: 0x000F7B00 File Offset: 0x000F5D00
		public ToolStripItem Add(string text, Image image)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, image, null);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		// Token: 0x06003E11 RID: 15889 RVA: 0x000F7B28 File Offset: 0x000F5D28
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, image, onClick);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls to the collection.</summary>
		/// <param name="toolStripItems">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E12 RID: 15890 RVA: 0x000F7B50 File Offset: 0x000F5D50
		public void AddRange(ToolStripItem[] toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			this.owner.SuspendLayout();
			foreach (ToolStripItem toolStripItem in toolStripItems)
			{
				this.Add(toolStripItem);
			}
			this.owner.ResumeLayout();
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to the current collection.</summary>
		/// <param name="toolStripItems">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to be added to the current collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E13 RID: 15891 RVA: 0x000F7BBC File Offset: 0x000F5DBC
		public void AddRange(ToolStripItemCollection toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			this.owner.SuspendLayout();
			foreach (object obj in toolStripItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				this.Add(toolStripItem);
			}
			this.owner.ResumeLayout();
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E14 RID: 15892 RVA: 0x000F7C64 File Offset: 0x000F5E64
		public new virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			base.Clear();
			this.owner.PerformLayout();
		}

		/// <summary>Determines whether the specified item is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is a member of the current <see cref="T:System.Windows.Forms.ToolStripItemCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E15 RID: 15893 RVA: 0x000F7C90 File Offset: 0x000F5E90
		public bool Contains(ToolStripItem value)
		{
			return base.Contains(value);
		}

		/// <summary>Determines whether the collection contains an item with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> contains a <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E16 RID: 15894 RVA: 0x000F7C9C File Offset: 0x000F5E9C
		public virtual bool ContainsKey(string key)
		{
			return this[key] != null;
		}

		/// <summary>Copies the collection into the specified position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
		/// <param name="array">The array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> to which to copy the collection. </param>
		/// <param name="index">The position in the <see cref="T:System.Windows.Forms.ToolStripItem" /> array at which to paste the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E17 RID: 15895 RVA: 0x000F7CAC File Offset: 0x000F5EAC
		public void CopyTo(ToolStripItem[] array, int index)
		{
			base.CopyTo(array, index);
		}

		/// <summary>Searches for items by their name and returns an array of all matching controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of the search results.</returns>
		/// <param name="key">The item name to search the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> for.</param>
		/// <param name="searchAllChildren">true to search child items of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		// Token: 0x06003E18 RID: 15896 RVA: 0x000F7CB8 File Offset: 0x000F5EB8
		[MonoTODO("searchAllChildren parameter isn't used")]
		public ToolStripItem[] Find(string key, bool searchAllChildren)
		{
			if (key == null || key.Length == 0)
			{
				throw new ArgumentNullException("key");
			}
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object obj in this)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (string.Compare(toolStripItem.Name, key, true) == 0)
				{
					list.Add(toolStripItem);
					if (searchAllChildren)
					{
					}
				}
			}
			return list.ToArray();
		}

		/// <summary>Retrieves the index of the specified item in the collection.</summary>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E19 RID: 15897 RVA: 0x000F7D64 File Offset: 0x000F5F64
		public int IndexOf(ToolStripItem value)
		{
			return base.IndexOf(value);
		}

		/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
		/// <returns>A zero-based index value that represents the position of the first occurrence of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E1A RID: 15898 RVA: 0x000F7D70 File Offset: 0x000F5F70
		public virtual int IndexOfKey(string key)
		{
			ToolStripItem toolStripItem = this[key];
			if (toolStripItem == null)
			{
				return -1;
			}
			return this.IndexOf(toolStripItem);
		}

		/// <summary>Inserts the specified item into the collection at the specified index.</summary>
		/// <param name="index">The location in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> at which to insert the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E1B RID: 15899 RVA: 0x000F7D94 File Offset: 0x000F5F94
		public void Insert(int index, ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is ToolStripMenuItem && (value as ToolStripMenuItem).ShortcutKeys != Keys.None)
			{
				ToolStripManager.AddToolStripMenuItem((ToolStripMenuItem)value);
			}
			if (value.Owner != null)
			{
				value.Owner.Items.Remove(value);
			}
			base.Insert(index, value);
			if (this.internal_created)
			{
				value.InternalOwner = this.owner;
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			if (this.owner.Created)
			{
				this.owner.PerformLayout();
			}
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E1C RID: 15900 RVA: 0x000F7E40 File Offset: 0x000F6040
		public void Remove(ToolStripItem value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			base.Remove(value);
			if (value != null && this.internal_created)
			{
				value.InternalOwner = null;
				value.Parent = null;
			}
			if (this.internal_created)
			{
				this.owner.OnItemRemoved(new ToolStripItemEventArgs(value));
			}
			if (this.owner.Created)
			{
				this.owner.PerformLayout();
			}
		}

		/// <summary>Removes an item from the specified index in the collection.</summary>
		/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E1D RID: 15901 RVA: 0x000F7EC0 File Offset: 0x000F60C0
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			ToolStripItem toolStripItem = (ToolStripItem)base[index];
			this.Remove(toolStripItem);
		}

		/// <summary>Removes the item that has the specified key.</summary>
		/// <param name="key">The key of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E1E RID: 15902 RVA: 0x000F7EF8 File Offset: 0x000F60F8
		public virtual void RemoveByKey(string key)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("This collection is read-only");
			}
			ToolStripItem toolStripItem = this[key];
			if (toolStripItem != null)
			{
				this.Remove(toolStripItem);
			}
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x000F7F30 File Offset: 0x000F6130
		internal int AddNoOwnerOrLayout(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.Add(value);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000F7F58 File Offset: 0x000F6158
		internal void InsertNoOwnerOrLayout(int index, ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index > this.Count)
			{
				base.Add(value);
			}
			else
			{
				base.Insert(index, value);
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000F7F98 File Offset: 0x000F6198
		internal void RemoveNoOwnerOrLayout(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.Remove(value);
		}

		// Token: 0x04001AE4 RID: 6884
		private ToolStrip owner;

		// Token: 0x04001AE5 RID: 6885
		private bool internal_created;
	}
}
