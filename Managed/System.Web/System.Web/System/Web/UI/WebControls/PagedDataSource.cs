using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the paging-related properties of a data-bound control (such as <see cref="T:System.Web.UI.WebControls.DataGrid" />, <see cref="T:System.Web.UI.WebControls.GridView" />, <see cref="T:System.Web.UI.WebControls.DetailsView" />, and <see cref="T:System.Web.UI.WebControls.FormView" />) that allow it to perform paging. This class cannot be inherited.</summary>
	// Token: 0x020003E0 RID: 992
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagedDataSource : ICollection, IEnumerable, ITypedList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> class.</summary>
		// Token: 0x06002B6B RID: 11115 RVA: 0x000734E3 File Offset: 0x000716E3
		public PagedDataSource()
		{
			this.page_size = 10;
		}

		/// <summary>Gets or sets a value indicating whether custom paging is enabled in a data-bound control.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000734F3 File Offset: 0x000716F3
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000734FB File Offset: 0x000716FB
		public bool AllowCustomPaging
		{
			get
			{
				return this.allow_custom_paging;
			}
			set
			{
				this.allow_custom_paging = value;
				if (this.allow_custom_paging)
				{
					this.allow_server_paging = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether paging is enabled in a data-bound control.</summary>
		/// <returns>true if paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x00073513 File Offset: 0x00071713
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x0007351B File Offset: 0x0007171B
		public bool AllowPaging
		{
			get
			{
				return this.allow_paging;
			}
			set
			{
				this.allow_paging = value;
			}
		}

		/// <summary>Gets the number of items to be used from the data source.</summary>
		/// <returns>The number of items to be used from the data source.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x00073524 File Offset: 0x00071724
		public int Count
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (!this.IsPagingEnabled)
				{
					return this.DataSourceCount;
				}
				if (this.IsCustomPagingEnabled || !this.IsLastPage)
				{
					return this.page_size;
				}
				return this.DataSourceCount - this.FirstIndexInPage;
			}
		}

		/// <summary>Gets or sets the index of the current page.</summary>
		/// <returns>The index of the current page.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x00073563 File Offset: 0x00071763
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x0007356B File Offset: 0x0007176B
		public int CurrentPageIndex
		{
			get
			{
				return this.current_page_index;
			}
			set
			{
				this.current_page_index = value;
			}
		}

		/// <summary>Gets or sets the data source.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerable" /> implemented object that represents the data source.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x00073574 File Offset: 0x00071774
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x0007357C File Offset: 0x0007177C
		public IEnumerable DataSource
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets the number of items in the data source.</summary>
		/// <returns>The number of items in the data source.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source is not an <see cref="T:System.Collections.ICollection" /> implemented object.</exception>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x00073588 File Offset: 0x00071788
		public int DataSourceCount
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (this.IsCustomPagingEnabled || this.IsServerPagingEnabled)
				{
					return this.virtual_count;
				}
				if (this.source is ICollection)
				{
					return ((ICollection)this.source).Count;
				}
				throw new HttpException("The data source must implement ICollection");
			}
		}

		/// <summary>Gets the index of the first record displayed on the page.</summary>
		/// <returns>The index of the first record displayed on the page.</returns>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000735DE File Offset: 0x000717DE
		public int FirstIndexInPage
		{
			get
			{
				if (!this.IsPagingEnabled || this.IsCustomPagingEnabled || this.IsServerPagingEnabled || this.source == null)
				{
					return 0;
				}
				return this.current_page_index * this.page_size;
			}
		}

		/// <summary>Gets a value indicating whether custom paging is enabled.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x0007360F File Offset: 0x0007180F
		public bool IsCustomPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allow_custom_paging;
			}
		}

		/// <summary>Gets a value indicating whether server-side paging support is enabled.</summary>
		/// <returns>true if paging is enabled and server-side paging is indicated using the <see cref="P:System.Web.UI.WebControls.PagedDataSource.AllowServerPaging" /> property; otherwise, false.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x00073621 File Offset: 0x00071821
		public bool IsServerPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allow_server_paging;
			}
		}

		/// <summary>Gets a value indicating whether the current page is the first page.</summary>
		/// <returns>true if the current page is the first page; otherwise, false.</returns>
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00073633 File Offset: 0x00071833
		public bool IsFirstPage
		{
			get
			{
				return !this.allow_paging || this.current_page_index == 0;
			}
		}

		/// <summary>Gets a value indicating whether the current page is the last page.</summary>
		/// <returns>true if the current page is the last page; otherwise, false.</returns>
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00073648 File Offset: 0x00071848
		public bool IsLastPage
		{
			get
			{
				return !this.allow_paging || this.page_size == 0 || this.current_page_index == this.PageCount - 1;
			}
		}

		/// <summary>Gets a value indicating whether paging is enabled.</summary>
		/// <returns>true if paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x0007366C File Offset: 0x0007186C
		public bool IsPagingEnabled
		{
			get
			{
				return this.allow_paging && this.page_size != 0;
			}
		}

		/// <summary>Gets a value indicating whether the data source is read-only.</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the data source is synchronized (thread-safe).</summary>
		/// <returns>false for all cases.</returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the total number of pages necessary to display all items in the data source.</summary>
		/// <returns>The number of pages necessary to display all items in the data source.</returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x00073681 File Offset: 0x00071881
		public int PageCount
		{
			get
			{
				if (this.source == null)
				{
					return 0;
				}
				if (!this.IsPagingEnabled || this.DataSourceCount == 0 || this.page_size == 0)
				{
					return 1;
				}
				return (this.DataSourceCount + this.page_size - 1) / this.page_size;
			}
		}

		/// <summary>Gets or sets the number of items to display on a single page.</summary>
		/// <returns>The number of items to display on a single page.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000736BD File Offset: 0x000718BD
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x000736C5 File Offset: 0x000718C5
		public int PageSize
		{
			get
			{
				return this.page_size;
			}
			set
			{
				this.page_size = value;
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the virtual number of items in the data source when custom paging is used.</summary>
		/// <returns>The virtual number of items in the data source when custom paging is used.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000736CE File Offset: 0x000718CE
		// (set) Token: 0x06002B83 RID: 11139 RVA: 0x000736D6 File Offset: 0x000718D6
		public int VirtualCount
		{
			get
			{
				return this.virtual_count;
			}
			set
			{
				this.virtual_count = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether server-side paging is enabled.</summary>
		/// <returns>true if server-side paging is enabled; otherwise, false.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000736DF File Offset: 0x000718DF
		// (set) Token: 0x06002B85 RID: 11141 RVA: 0x000736E7 File Offset: 0x000718E7
		public bool AllowServerPaging
		{
			get
			{
				return this.allow_server_paging;
			}
			set
			{
				this.allow_server_paging = value;
				if (this.allow_server_paging)
				{
					this.allow_custom_paging = false;
				}
			}
		}

		/// <summary>Copies all the items from the data source to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the data source. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		// Token: 0x06002B86 RID: 11142 RVA: 0x00073700 File Offset: 0x00071900
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this.source)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</returns>
		// Token: 0x06002B87 RID: 11143 RVA: 0x0007375C File Offset: 0x0007195C
		public IEnumerator GetEnumerator()
		{
			IList list = this.source as IList;
			if (list != null)
			{
				int num = this.FirstIndexInPage;
				int num2 = ((ICollection)this.source).Count;
				int num3 = ((num + this.page_size > num2) ? (num2 - num) : this.page_size);
				return this.GetListEnum(list, num, num + num3);
			}
			ICollection collection = this.source as ICollection;
			if (collection != null)
			{
				int num = this.FirstIndexInPage;
				int num2 = collection.Count;
				int num4 = ((num + this.page_size > num2) ? (num2 - num) : this.page_size);
				return this.GetEnumeratorEnum(collection.GetEnumerator(), num, num + this.page_size);
			}
			return this.source.GetEnumerator();
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be null.</param>
		// Token: 0x06002B88 RID: 11144 RVA: 0x00073810 File Offset: 0x00071A10
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ITypedList typedList = this.source as ITypedList;
			if (typedList == null)
			{
				return null;
			}
			return typedList.GetItemProperties(listAccessors);
		}

		/// <summary>Returns the name of the list. This method does not apply to this class.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" /> for all cases.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be null. </param>
		// Token: 0x06002B89 RID: 11145 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00073835 File Offset: 0x00071A35
		private IEnumerator GetListEnum(IList list, int start, int end)
		{
			if (!this.AllowPaging)
			{
				end = list.Count;
			}
			else if (start >= list.Count)
			{
				yield break;
			}
			int num;
			for (int i = start; i < end; i = num + 1)
			{
				yield return list[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00073859 File Offset: 0x00071A59
		private IEnumerator GetEnumeratorEnum(IEnumerator e, int start, int end)
		{
			for (int j = 0; j < start; j++)
			{
				e.MoveNext();
			}
			int i = start;
			while ((!this.allow_paging || i < end) && e.MoveNext())
			{
				yield return e.Current;
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x04001B15 RID: 6933
		private int page_size;

		// Token: 0x04001B16 RID: 6934
		private int current_page_index;

		// Token: 0x04001B17 RID: 6935
		private int virtual_count;

		// Token: 0x04001B18 RID: 6936
		private bool allow_paging;

		// Token: 0x04001B19 RID: 6937
		private bool allow_custom_paging;

		// Token: 0x04001B1A RID: 6938
		private IEnumerable source;

		// Token: 0x04001B1B RID: 6939
		private bool allow_server_paging;
	}
}
