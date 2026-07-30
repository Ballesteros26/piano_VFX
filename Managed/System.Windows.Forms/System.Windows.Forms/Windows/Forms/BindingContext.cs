using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Manages the collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for any object that inherits from the <see cref="T:System.Windows.Forms.Control" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005C RID: 92
	[DefaultEvent("CollectionChanged")]
	public class BindingContext : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingContext" /> class.</summary>
		// Token: 0x06000384 RID: 900 RVA: 0x00012C50 File Offset: 0x00010E50
		public BindingContext()
		{
			this.managers = new Hashtable();
			this.onCollectionChangedHandler = null;
		}

		/// <summary>Always raises a <see cref="T:System.NotImplementedException" /> when handled.</summary>
		/// <exception cref="T:System.NotImplementedException">Occurs in all cases.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000385 RID: 901 RVA: 0x00012C6C File Offset: 0x00010E6C
		// (remove) Token: 0x06000386 RID: 902 RVA: 0x00012C74 File Offset: 0x00010E74
		[Browsable(false)]
		[EditorBrowsable(1)]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
			}
		}

		/// <summary>Copies the elements of the collection into a specified array, starting at the collection index.</summary>
		/// <param name="ar">An <see cref="T:System.Array" /> to copy into. </param>
		/// <param name="index">The collection index to begin copying from. </param>
		// Token: 0x06000387 RID: 903 RVA: 0x00012C78 File Offset: 0x00010E78
		void ICollection.CopyTo(Array ar, int index)
		{
			this.managers.CopyTo(ar, index);
		}

		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.CurrencyManager" /> objects managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
		/// <returns>The number of data sources managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00012C88 File Offset: 0x00010E88
		int ICollection.Count
		{
			get
			{
				return this.managers.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized.</summary>
		/// <returns>true if the collection is thread safe; otherwise, false.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00012C98 File Offset: 0x00010E98
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to use for synchronization (thread safety).</summary>
		/// <returns>This property is derived from <see cref="T:System.Collections.ICollection" />, and is overridden to always return null.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00012C9C File Offset: 0x00010E9C
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x0600038B RID: 907 RVA: 0x00012CA0 File Offset: 0x00010EA0
		[MonoInternalNote("our enumerator is slightly different.  in MS's implementation the Values are WeakReferences to the managers.")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.managers.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00012CB0 File Offset: 0x00010EB0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source.</returns>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CD RID: 205
		public BindingManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, string.Empty];
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source and data member.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source and data member.</returns>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <param name="dataMember">A navigation path containing the information that resolves to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <exception cref="T:System.Exception">The specified <paramref name="dataMember" /> does not exist within the data source. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CE RID: 206
		public BindingManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				if (dataMember == null)
				{
					dataMember = string.Empty;
				}
				ICurrencyManagerProvider currencyManagerProvider = dataSource as ICurrencyManagerProvider;
				if (currencyManagerProvider != null)
				{
					if (dataMember.Length == 0)
					{
						return currencyManagerProvider.CurrencyManager;
					}
					return currencyManagerProvider.GetRelatedCurrencyManager(dataMember);
				}
				else
				{
					BindingContext.HashKey hashKey = new BindingContext.HashKey(dataSource, dataMember);
					BindingManagerBase bindingManagerBase = this.managers[hashKey] as BindingManagerBase;
					if (bindingManagerBase != null)
					{
						return bindingManagerBase;
					}
					bindingManagerBase = this.CreateBindingManager(dataSource, dataMember);
					if (bindingManagerBase == null)
					{
						return null;
					}
					this.managers[hashKey] = bindingManagerBase;
					return bindingManagerBase;
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012D58 File Offset: 0x00010F58
		private BindingManagerBase CreateBindingManager(object data_source, string data_member)
		{
			if (data_member == string.Empty)
			{
				if (this.IsListType(data_source.GetType()))
				{
					return new CurrencyManager(data_source);
				}
				return new PropertyManager(data_source);
			}
			else
			{
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(data_member);
				BindingManagerBase bindingManagerBase = this[data_source, bindingMemberInfo.BindingPath];
				PropertyDescriptor propertyDescriptor = ((bindingManagerBase != null) ? bindingManagerBase.GetItemProperties().Find(bindingMemberInfo.BindingField, true) : null);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(string.Format("Cannot create a child list for field {0}.", bindingMemberInfo.BindingField));
				}
				if (this.IsListType(propertyDescriptor.PropertyType))
				{
					return new RelatedCurrencyManager(bindingManagerBase, propertyDescriptor);
				}
				return new RelatedPropertyManager(bindingManagerBase, bindingMemberInfo.BindingField);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012E10 File Offset: 0x00011010
		private bool IsListType(Type t)
		{
			return typeof(IList).IsAssignableFrom(t) || typeof(IListSource).IsAssignableFrom(t);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, false.</returns>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000391 RID: 913 RVA: 0x00012E48 File Offset: 0x00011048
		public bool Contains(object dataSource)
		{
			return this.Contains(dataSource, string.Empty);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source and data member.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, false.</returns>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
		/// <param name="dataMember">The information needed to resolve to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000392 RID: 914 RVA: 0x00012E58 File Offset: 0x00011058
		public bool Contains(object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (dataMember == null)
			{
				dataMember = string.Empty;
			}
			BindingContext.HashKey hashKey = new BindingContext.HashKey(dataSource, dataMember);
			return this.managers[hashKey] != null;
		}

		/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
		/// <param name="dataSource">The <see cref="T:System.Object" /> associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add. </param>
		// Token: 0x06000393 RID: 915 RVA: 0x00012EA0 File Offset: 0x000110A0
		protected internal void Add(object dataSource, BindingManagerBase listManager)
		{
			this.AddCore(dataSource, listManager);
			this.OnCollectionChanged(new CollectionChangeEventArgs(1, dataSource));
		}

		/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
		/// <param name="dataSource">The object associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataSource" /> is null.-or-<paramref name="listManager" /> is null.</exception>
		// Token: 0x06000394 RID: 916 RVA: 0x00012EB8 File Offset: 0x000110B8
		protected virtual void AddCore(object dataSource, BindingManagerBase listManager)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (listManager == null)
			{
				throw new ArgumentNullException("listManager");
			}
			BindingContext.HashKey hashKey = new BindingContext.HashKey(dataSource, string.Empty);
			this.managers[hashKey] = listManager;
		}

		/// <summary>Clears the collection of any <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects.</summary>
		// Token: 0x06000395 RID: 917 RVA: 0x00012F00 File Offset: 0x00011100
		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(3, null));
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x06000396 RID: 918 RVA: 0x00012F18 File Offset: 0x00011118
		protected virtual void ClearCore()
		{
			this.managers.Clear();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingContext.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000397 RID: 919 RVA: 0x00012F28 File Offset: 0x00011128
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangedHandler != null)
			{
				this.onCollectionChangedHandler.Invoke(this, ccevent);
			}
		}

		/// <summary>Deletes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove. </param>
		// Token: 0x06000398 RID: 920 RVA: 0x00012F44 File Offset: 0x00011144
		protected internal void Remove(object dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.RemoveCore(dataSource);
			this.OnCollectionChanged(new CollectionChangeEventArgs(2, dataSource));
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove.</param>
		// Token: 0x06000399 RID: 921 RVA: 0x00012F6C File Offset: 0x0001116C
		protected virtual void RemoveCore(object dataSource)
		{
			BindingContext.HashKey[] array = new BindingContext.HashKey[this.managers.Keys.Count];
			this.managers.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].source == dataSource)
				{
					this.managers.Remove(array[i]);
				}
			}
		}

		/// <summary>Associates a <see cref="T:System.Windows.Forms.Binding" /> with a new <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
		/// <param name="newBindingContext">The new <see cref="T:System.Windows.Forms.BindingContext" /> to associate with the <see cref="T:System.Windows.Forms.Binding" />.</param>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to associate with the new <see cref="T:System.Windows.Forms.BindingContext" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039A RID: 922 RVA: 0x00012FD4 File Offset: 0x000111D4
		[MonoTODO("Stub, does nothing")]
		public static void UpdateBinding(BindingContext newBindingContext, Binding binding)
		{
		}

		// Token: 0x0400062F RID: 1583
		private Hashtable managers;

		// Token: 0x04000630 RID: 1584
		private EventHandler onCollectionChangedHandler;

		// Token: 0x0200005D RID: 93
		private class HashKey
		{
			// Token: 0x0600039B RID: 923 RVA: 0x00012FD8 File Offset: 0x000111D8
			public HashKey(object source, string member)
			{
				this.source = source;
				this.member = member;
			}

			// Token: 0x0600039C RID: 924 RVA: 0x00012FF0 File Offset: 0x000111F0
			public override int GetHashCode()
			{
				return this.source.GetHashCode() ^ this.member.GetHashCode();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0001300C File Offset: 0x0001120C
			public override bool Equals(object o)
			{
				BindingContext.HashKey hashKey = o as BindingContext.HashKey;
				return hashKey != null && hashKey.source == this.source && hashKey.member == this.member;
			}

			// Token: 0x04000631 RID: 1585
			public object source;

			// Token: 0x04000632 RID: 1586
			public string member;
		}
	}
}
