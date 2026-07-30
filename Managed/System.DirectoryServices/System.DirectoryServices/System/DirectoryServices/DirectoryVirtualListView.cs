using System;
using System.ComponentModel;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class specifies how to conduct a virtual list view search. A virtual list view search enables users to view search results as address-book style virtual list views. It is specifically designed for very large result sets. Search data is retrieved in contiguous subsets of a sorted directory search.          </summary>
	// Token: 0x0200001E RID: 30
	public class DirectoryVirtualListView
	{
		/// <summary>Gets or sets a value to indicate the number of entries before the target entry that the client is requesting from the server.          </summary>
		/// <returns>An integer value that represents the number of entries before the target entry that the client is requesting from the server.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.BeforeCount" /> property is set to a value less than 0.</exception>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000208C File Offset: 0x0000028C
		[DefaultValue(0)]
		[DSDescription("DSBeforeCount")]
		public int BeforeCount
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.          </summary>
		/// <returns>An integer value that represents the number of entries after the target entry that the client is requesting from the server.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.AfterCount" /> property is set to a value less than zero.</exception>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000208C File Offset: 0x0000028C
		[DSDescription("DSAfterCount")]
		[DefaultValue(0)]
		public int AfterCount
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value to indicate the target entry's offset within the list.  </summary>
		/// <returns>An integer value that represents the target entry's estimated offset within the list.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.Offset" /> property is set to a value less than 0.</exception>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000208C File Offset: 0x0000028C
		[DSDescription("DSOffset")]
		[DefaultValue(0)]
		public int Offset
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.TargetPercentage" /> property gets or sets a value to indicate the estimated target entry's requested offset within the list, as a percentage of the total number of items in the list.  </summary>
		/// <returns>An integer value that represents the estimated percentage offset within the list of the target entry.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.TargetPercentage" /> property is set to a value greater than 100 or less than 0.</exception>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000208C File Offset: 0x0000028C
		[DefaultValue(0)]
		[DSDescription("DSTargetPercentage")]
		public int TargetPercentage
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.Target" /> property gets or sets a value to indicate the target entry that was requested by the client.          </summary>
		/// <returns>A string that contains the target entry that was requested by the client.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000208C File Offset: 0x0000028C
		[DSDescription("DSTarget")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Target
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value to indicate the estimated total count of items in the list.          </summary>
		/// <returns>An integer value that represents the estimated total count of items in the list.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.ApproximateTotal" /> property is set to a value less than zero.</exception>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000208C File Offset: 0x0000028C
		[DSDescription("DSApproximateTotal")]
		[DefaultValue(0)]
		public int ApproximateTotal
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value to indicate the virtual list view search response.          </summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> that indicates the virtual list view search response.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000208C File Offset: 0x0000028C
		[DefaultValue(null)]
		[DSDescription("DSDirectoryVirtualListViewContext")]
		public DirectoryVirtualListViewContext DirectoryVirtualListViewContext
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class.          </summary>
		// Token: 0x06000113 RID: 275 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the after count set.          </summary>
		/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		// Token: 0x06000114 RID: 276 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView(int afterCount)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, and offset set.          </summary>
		/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="offset">An <see cref="T:System.Int32" /> data type that gets or sets a value to indicate the estimated target entry's requested offset within the list.</param>
		// Token: 0x06000115 RID: 277 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView(int beforeCount, int afterCount, int offset)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, and target set.          </summary>
		/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="target">A <see cref="T:System.String" /> that gets or sets a value to indicate the desired target entry requested by the client.</param>
		// Token: 0x06000116 RID: 278 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView(int beforeCount, int afterCount, string target)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, offset and context set.          </summary>
		/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="offset">An <see cref="T:System.Int32" /> data type that gets or sets a value to indicate the estimated target entry's requested offset within the list.</param>
		/// <param name="context">A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> data type objects that gets or sets a value to indicate the virtual list view search response.</param>
		// Token: 0x06000117 RID: 279 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView(int beforeCount, int afterCount, int offset, DirectoryVirtualListViewContext context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, target and context set.          </summary>
		/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
		/// <param name="target">A <see cref="T:System.String" /> that gets or sets a value to indicate the desired target entry requested by the client.</param>
		/// <param name="context">A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> data type objects that gets or sets a value to indicate the virtual list view search response.</param>
		// Token: 0x06000118 RID: 280 RVA: 0x00002050 File Offset: 0x00000250
		public DirectoryVirtualListView(int beforeCount, int afterCount, string target, DirectoryVirtualListViewContext context)
		{
		}
	}
}
