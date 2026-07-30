using System;
using System.Collections;
using Unity;

namespace System.Web.Management
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Management.WebBaseEvent" /> objects. This class cannot be inherited. </summary>
	// Token: 0x02000744 RID: 1860
	public sealed class WebBaseEventCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEventCollection" /> class. </summary>
		/// <param name="events">The collection of <see cref="T:System.Web.Management.WebBaseEvent" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="events" /> is null.</exception>
		// Token: 0x06004C91 RID: 19601 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebBaseEventCollection(ICollection events)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Management.WebBaseEvent" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebBaseEvent" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Management.WebBaseEvent" /> object you want to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of the collection.</exception>
		// Token: 0x1700178F RID: 6031
		public WebBaseEvent this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Indicates whether the collection contains the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object.</summary>
		/// <returns>true if the collection contains the specified element; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to search for.</param>
		// Token: 0x06004C93 RID: 19603 RVA: 0x000CAF88 File Offset: 0x000C9188
		public bool Contains(WebBaseEvent value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Web.Management.WebBaseEvent" /> object for which to obtain the index.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The object is not in the collection.</exception>
		// Token: 0x06004C94 RID: 19604 RVA: 0x000CAFA4 File Offset: 0x000C91A4
		public int IndexOf(WebBaseEvent value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
