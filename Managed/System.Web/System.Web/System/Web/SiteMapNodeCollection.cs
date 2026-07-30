using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web
{
	/// <summary>Provides a strongly typed collection for <see cref="T:System.Web.SiteMapNode" /> objects and implements the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface to support navigating through the collection. </summary>
	// Token: 0x020000D4 RID: 212
	public class SiteMapNodeCollection : IList, ICollection, IEnumerable, IHierarchicalEnumerable
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0001ECC8 File Offset: 0x0001CEC8
		static SiteMapNodeCollection()
		{
			SiteMapNodeCollection.EmptyList.list = ArrayList.ReadOnly(new ArrayList());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class, which is the default instance.</summary>
		// Token: 0x06000B6D RID: 2925 RVA: 0x00002050 File Offset: 0x00000250
		public SiteMapNodeCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class with the specified initial capacity.</summary>
		/// <param name="capacity">The initial capacity of the <see cref="T:System.Web.SiteMapNodeCollection" />.</param>
		// Token: 0x06000B6E RID: 2926 RVA: 0x0001ECE8 File Offset: 0x0001CEE8
		public SiteMapNodeCollection(int capacity)
		{
			this.list = new ArrayList(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds the <see cref="T:System.Web.SiteMapNode" /> object to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
		/// <param name="value">A <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000B6F RID: 2927 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		public SiteMapNodeCollection(SiteMapNode value)
		{
			this.Add(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds the array of type <see cref="T:System.Web.SiteMapNode" /> to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000B70 RID: 2928 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		public SiteMapNodeCollection(SiteMapNode[] value)
		{
			this.AddRangeInternal(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds all the list items of the specified <see cref="T:System.Web.SiteMapNodeCollection" /> collection to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
		/// <param name="value">A <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000B71 RID: 2929 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		public SiteMapNodeCollection(SiteMapNodeCollection value)
		{
			this.AddRangeInternal(value);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0001ED1B File Offset: 0x0001CF1B
		internal static SiteMapNodeCollection EmptyCollection
		{
			get
			{
				return SiteMapNodeCollection.EmptyList;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001ED22 File Offset: 0x0001CF22
		private ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001ED3D File Offset: 0x0001CF3D
		public virtual int Count
		{
			get
			{
				if (this.list != null)
				{
					return this.list.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a Boolean value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access is synchronized; otherwise, false. The default is false.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the  collection. </summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00002058 File Offset: 0x00000258
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Retrieves a reference to an enumerator object, which is used to iterate over the collection. </summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000B77 RID: 2935 RVA: 0x0001ED54 File Offset: 0x0001CF54
		public virtual IEnumerator GetEnumerator()
		{
			if (this.list == null)
			{
				return Type.EmptyTypes.GetEnumerator();
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.</exception>
		// Token: 0x06000B78 RID: 2936 RVA: 0x0001ED74 File Offset: 0x0001CF74
		public virtual void Clear()
		{
			if (this.list != null)
			{
				this.list.Clear();
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.SiteMapNode" /> object at the specified index of the  collection.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.- or -The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed sized.</exception>
		// Token: 0x06000B79 RID: 2937 RVA: 0x0001ED89 File Offset: 0x0001CF89
		public virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		/// <summary>Adds a single <see cref="T:System.Web.SiteMapNode" /> object to the  collection.</summary>
		/// <returns>The index of the <see cref="P:System.Collections.CollectionBase.InnerList" /> where the <see cref="T:System.Web.SiteMapNode" /> was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
		// Token: 0x06000B7A RID: 2938 RVA: 0x0001ED97 File Offset: 0x0001CF97
		public virtual int Add(SiteMapNode value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.List.Add(value);
		}

		/// <summary>Adds an array of type <see cref="T:System.Web.SiteMapNode" /> to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Web.SiteMapNode" /> to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
		// Token: 0x06000B7B RID: 2939 RVA: 0x0001EDB3 File Offset: 0x0001CFB3
		public virtual void AddRange(SiteMapNode[] value)
		{
			this.AddRangeInternal(value);
		}

		/// <summary>Adds the nodes in the specified  <see cref="T:System.Web.SiteMapNodeCollection" /> to the current collection.</summary>
		/// <param name="value">A <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> objects to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
		// Token: 0x06000B7C RID: 2940 RVA: 0x0001EDB3 File Offset: 0x0001CFB3
		public virtual void AddRange(SiteMapNodeCollection value)
		{
			this.AddRangeInternal(value);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		internal virtual void AddRangeInternal(IList value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.List.AddRange(value);
		}

		/// <summary>Determines whether the collection contains a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.SiteMapNodeCollection" /> contains the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to locate in the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		// Token: 0x06000B7E RID: 2942 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		public virtual bool Contains(SiteMapNode value)
		{
			return this.List.Contains(value);
		}

		/// <summary>Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that must have zero-based indexing and is the destination of the elements copied from the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of <see cref="T:System.Web.SiteMapNode" /> objects in the source <see cref="T:System.Web.SiteMapNodeCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		// Token: 0x06000B7F RID: 2943 RVA: 0x0001EDE6 File Offset: 0x0001CFE6
		public virtual void CopyTo(SiteMapNode[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>Searches for the specified <see cref="T:System.Web.SiteMapNode" /> object, and then returns the zero-based index of the first occurrence within the entire collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Web.SiteMapNodeCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to locate in the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		// Token: 0x06000B80 RID: 2944 RVA: 0x0001EDF5 File Offset: 0x0001CFF5
		public virtual int IndexOf(SiteMapNode value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.SiteMapNode" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Web.SiteMapNode" /> is inserted. </param>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.-or- The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed size. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000B81 RID: 2945 RVA: 0x0001EE03 File Offset: 0x0001D003
		public virtual void Insert(int index, SiteMapNode value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to validate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Web.SiteMapNode" />.</exception>
		// Token: 0x06000B82 RID: 2946 RVA: 0x0001EE12 File Offset: 0x0001D012
		protected virtual void OnValidate(object value)
		{
			if (!(value is SiteMapNode))
			{
				throw new ArgumentException("Invalid type");
			}
		}

		/// <summary>Returns a read-only collection that contains the nodes in the specified <see cref="T:System.Web.SiteMapNodeCollection" /> collection.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> with the same <see cref="T:System.Web.SiteMapNode" /> elements and structure as the original <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		/// <param name="collection">The <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> objects to add to the read-only <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06000B83 RID: 2947 RVA: 0x0001EE28 File Offset: 0x0001D028
		public static SiteMapNodeCollection ReadOnly(SiteMapNodeCollection collection)
		{
			SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
			if (collection.list != null)
			{
				siteMapNodeCollection.list = ArrayList.ReadOnly(collection.list);
			}
			else
			{
				siteMapNodeCollection.list = ArrayList.ReadOnly(new ArrayList());
			}
			return siteMapNodeCollection;
		}

		/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to remove from the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> does not exist in the collection. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.-or- The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed size. </exception>
		// Token: 0x06000B84 RID: 2948 RVA: 0x0001EE67 File Offset: 0x0001D067
		public virtual void Remove(SiteMapNode value)
		{
			this.List.Remove(value);
		}

		/// <summary>Returns a hierarchical data item for the specified enumerated item.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchyData" /> that represents the object passed to the <see cref="M:System.Web.SiteMapNodeCollection.GetHierarchyData(System.Object)" />.</returns>
		/// <param name="enumeratedItem">The object for which to return an <see cref="T:System.Web.UI.IHierarchyData" />.</param>
		// Token: 0x06000B85 RID: 2949 RVA: 0x0001EE75 File Offset: 0x0001D075
		public virtual IHierarchyData GetHierarchyData(object enumeratedItem)
		{
			return enumeratedItem as IHierarchyData;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> object that is associated with the nodes in the current collection.</summary>
		/// <returns>A named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> for the <see cref="T:System.Web.SiteMapNode" /> objects in the current <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control that the view is associated with.</param>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x06000B86 RID: 2950 RVA: 0x0001EE7D File Offset: 0x0001D07D
		public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
		{
			return new SiteMapDataSourceView(owner, viewName, this);
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> object that is associated with the nodes in the current collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> for the <see cref="T:System.Web.SiteMapNode" /> objects in the current <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		// Token: 0x06000B87 RID: 2951 RVA: 0x0001EE87 File Offset: 0x0001D087
		public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
		{
			return new SiteMapHierarchicalDataSourceView(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an element in the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.SiteMapNode" /> to find. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is great than the <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value supplied to the setter is null.</exception>
		// Token: 0x1700040E RID: 1038
		public virtual SiteMapNode this[int index]
		{
			get
			{
				return (SiteMapNode)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>Gets a Boolean value indicating whether nodes can be added to or subtracted from the collection.</summary>
		/// <returns>true if you can add <see cref="T:System.Web.SiteMapNode" /> objects to or remove <see cref="T:System.Web.SiteMapNode" /> objects from the <see cref="T:System.Web.SiteMapNodeCollection" />; otherwise, false. </returns>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001EEB1 File Offset: 0x0001D0B1
		public virtual bool IsFixedSize
		{
			get
			{
				return this.List.IsFixedSize;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the collection is read-only.</summary>
		/// <returns>true if you can modify the <see cref="T:System.Web.SiteMapNodeCollection" />; otherwise, false.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001EEBE File Offset: 0x0001D0BE
		public virtual bool IsReadOnly
		{
			get
			{
				return this.list != null && this.list.IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.IList" /> element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x17000411 RID: 1041
		object IList.this[int index]
		{
			get
			{
				return this.List[index];
			}
			set
			{
				this.OnValidate(value);
				this.List[index] = value;
			}
		}

		/// <summary>Adds an item to the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The object to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000B8E RID: 2958 RVA: 0x0001EEF9 File Offset: 0x0001D0F9
		int IList.Add(object value)
		{
			this.OnValidate(value);
			return this.List.Add(value);
		}

		/// <summary>Determines whether the collection in the <see cref="T:System.Collections.IList" /> interface contains the specified Boolean value.</summary>
		/// <returns>true if the object is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000B8F RID: 2959 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		bool IList.Contains(object value)
		{
			return this.List.Contains(value);
		}

		/// <summary>Determines the index of the specific item in the collection that is returned by the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The index of the value, in the list, if found; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000B90 RID: 2960 RVA: 0x0001EDF5 File Offset: 0x0001CFF5
		int IList.IndexOf(object value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>Inserts an item into the collection in the <see cref="T:System.Collections.IList" /> interface at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based <paramref name="index" /> at which to insert <paramref name="value" />.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000B91 RID: 2961 RVA: 0x0001EF0E File Offset: 0x0001D10E
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			this.List.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specified object from the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000B92 RID: 2962 RVA: 0x0001EF24 File Offset: 0x0001D124
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			this.List.Remove(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> interface to an array, starting at a particular array index. This class cannot be inherited.</summary>
		/// <param name="array">A one-dimensional array that must have zero-based indexing and is the destination of the elements copied from the <see cref="T:System.Collections.CollectionBase" />. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of <see cref="T:System.Web.SiteMapNode" /> objects in the source <see cref="T:System.Web.SiteMapNodeCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		// Token: 0x06000B93 RID: 2963 RVA: 0x0001EDE6 File Offset: 0x0001CFE6
		void ICollection.CopyTo(Array array, int index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>Removes all items from the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x06000B94 RID: 2964 RVA: 0x0001EF39 File Offset: 0x0001D139
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Gets a Boolean value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0001EF41 File Offset: 0x0001D141
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>true if the <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001EF49 File Offset: 0x0001D149
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06000B97 RID: 2967 RVA: 0x0001EF51 File Offset: 0x0001D151
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets the number of elements that are contained in the <see cref="T:System.Collections.ICollection" /> interface. This class cannot be inherited.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001EF5A File Offset: 0x0001D15A
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a Boolean value indicating whether access to the <see cref="T:System.Collections.ICollection" /> interface is synchronized (thread safe). This class cannot be inherited.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001EF62 File Offset: 0x0001D162
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" /> interface. This class cannot be inherited.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001EF6A File Offset: 0x0001D16A
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection. For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
		// Token: 0x06000B9B RID: 2971 RVA: 0x0001EF72 File Offset: 0x0001D172
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns a hierarchical data item for the specified enumerated item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchicalEnumerable.GetHierarchyData(System.Object)" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchyData" /> that represents the object passed to the <see cref="M:System.Web.SiteMapNodeCollection.System.Web.UI.IHierarchicalEnumerable.GetHierarchyData(System.Object)" />.</returns>
		/// <param name="enumeratedItem">The object for which to return an <see cref="T:System.Web.UI.IHierarchyData" />. </param>
		// Token: 0x06000B9C RID: 2972 RVA: 0x0001EF7A File Offset: 0x0001D17A
		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return this.GetHierarchyData(enumeratedItem);
		}

		// Token: 0x040010A3 RID: 4259
		private ArrayList list;

		// Token: 0x040010A4 RID: 4260
		internal static SiteMapNodeCollection EmptyList = new SiteMapNodeCollection();
	}
}
