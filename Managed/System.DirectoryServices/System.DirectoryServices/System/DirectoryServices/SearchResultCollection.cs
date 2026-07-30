using System;
using System.Collections;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.SearchResultCollection" /> class contains the <see cref="T:System.DirectoryServices.SearchResult" /> instances that the Active Directory hierarchy returned during a <see cref="T:System.DirectoryServices.DirectorySearcher" /> query.</summary>
	// Token: 0x0200002E RID: 46
	public class SearchResultCollection : MarshalByRefObject, ICollection, IEnumerable, IDisposable
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000495E File Offset: 0x00002B5E
		internal SearchResultCollection()
		{
		}

		/// <summary>Gets the number of <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection.</summary>
		/// <returns>The number of <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004971 File Offset: 0x00002B71
		public int Count
		{
			get
			{
				return this.sValues.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000497E File Offset: 0x00002B7E
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.sValues.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000498B File Offset: 0x00002B8B
		object ICollection.SyncRoot
		{
			get
			{
				return this.sValues.SyncRoot;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06000192 RID: 402 RVA: 0x00004998 File Offset: 0x00002B98
		void ICollection.CopyTo(Array oArray, int iArrayIndex)
		{
			this.sValues.CopyTo(oArray, iArrayIndex);
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.SearchResult" /> objects in this collection to the specific array, starting at the specified index in the target array.</summary>
		// Token: 0x06000193 RID: 403 RVA: 0x000049A7 File Offset: 0x00002BA7
		public void CopyTo(SearchResult[] results, int index)
		{
			((ICollection)this).CopyTo(results, index);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000049B1 File Offset: 0x00002BB1
		internal void Add(object oValue)
		{
			this.sValues.Add(oValue);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000049C0 File Offset: 0x00002BC0
		private bool Contains(object oValues)
		{
			return this.sValues.Contains(oValues);
		}

		/// <summary>Determines if a specified <see cref="T:System.DirectoryServices.SearchResult" /> object is in this collection.</summary>
		/// <returns>true if the specified property belongs to this collection; otherwise, false.</returns>
		// Token: 0x06000196 RID: 406 RVA: 0x000049C0 File Offset: 0x00002BC0
		public bool Contains(SearchResult result)
		{
			return this.sValues.Contains(result);
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.SearchResult" /> object that is located at a specified index in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.SearchResult" /> object that is located at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.SearchResult" /> object to retrieve.</param>
		// Token: 0x1700006F RID: 111
		public SearchResult this[int index]
		{
			get
			{
				return (SearchResult)this.sValues[index];
			}
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.SearchResult" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.SearchResult" /> object.</returns>
		/// <param name="result">The <see cref="T:System.DirectoryServices.SearchResult" /> object to search for in this collection.</param>
		// Token: 0x06000198 RID: 408 RVA: 0x000049E1 File Offset: 0x00002BE1
		public int IndexOf(SearchResult result)
		{
			return this.sValues.IndexOf(result);
		}

		/// <summary>Returns an enumerator that you can use to iterate through this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that you can use to iterate through this collection.</returns>
		// Token: 0x06000199 RID: 409 RVA: 0x000049EF File Offset: 0x00002BEF
		public IEnumerator GetEnumerator()
		{
			return this.sValues.GetEnumerator();
		}

		/// <summary>Releases all resources that are used by the <see cref="T:System.DirectoryServices.SearchResultCollection" /> object.</summary>
		// Token: 0x0600019A RID: 410 RVA: 0x000049FC File Offset: 0x00002BFC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.SearchResultCollection" /> object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600019B RID: 411 RVA: 0x00004060 File Offset: 0x00002260
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectorySearcher" /> properties that were specified before the search was executed.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> that contains the properties that were specified in the <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property collection before the search was executed.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000208C File Offset: 0x0000028C
		public string[] PropertiesLoaded
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the handle that is returned by the IDirectorySearch::ExecuteSearch method that performs the actual search. For more information, see the IDirectorySearch::ExecuteSearch topic in the MSDN Library at http://msdn.microsoft.com/library.</summary>
		/// <returns>The ADS_SEARCH_HANDLE value that this collection uses.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000208C File Offset: 0x0000028C
		public IntPtr Handle
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004A0C File Offset: 0x00002C0C
		~SearchResultCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x040000A8 RID: 168
		private ArrayList sValues = new ArrayList();
	}
}
