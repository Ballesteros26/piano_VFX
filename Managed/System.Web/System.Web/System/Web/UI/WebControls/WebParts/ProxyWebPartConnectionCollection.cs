using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of static <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects, which is used when the connections are declared in content pages and the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control is declared in a master page. This class cannot be inherited.</summary>
	// Token: 0x020007B7 RID: 1975
	[Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class ProxyWebPartConnectionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPartConnectionCollection" /> class. </summary>
		// Token: 0x06004FB4 RID: 20404 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProxyWebPartConnectionCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects can be added to the collection.</summary>
		/// <returns>true if connection objects cannot be added to the collection; otherwise, false.</returns>
		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x000CB90C File Offset: 0x000C9B0C
		public bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnection get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a specific member of the collection according to a unique identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> whose ID matches the value of the <paramref name="id" /> parameter. Returns null if no match is found.</returns>
		/// <param name="id">A string that contains the ID of a particular connection in the collection. </param>
		// Token: 0x1700183E RID: 6206
		public WebPartConnection this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object to the collection.</summary>
		/// <returns>An integer value that indicates where the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> was inserted into the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to add to the collection. </param>
		// Token: 0x06004FB9 RID: 20409 RVA: 0x000CB928 File Offset: 0x000C9B28
		public int Add(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a value indicating whether a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object exists in the collection.</summary>
		/// <returns>true if <paramref name="value" /> exists in the collection; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> being checked for its existence in a collection. </param>
		// Token: 0x06004FBA RID: 20410 RVA: 0x000CB944 File Offset: 0x000C9B44
		public bool Contains(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> objects to contain the copied collection. </param>
		/// <param name="index">An integer that indicates the starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004FBB RID: 20411 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(WebPartConnection[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> that is a member of the collection. </param>
		// Token: 0x06004FBC RID: 20412 RVA: 0x000CB960 File Offset: 0x000C9B60
		public int IndexOf(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object into the collection at the specified index.</summary>
		/// <param name="index">An integer indicating the ordinal position in the collection at which a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> should be inserted. </param>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to insert into the collection.  </param>
		// Token: 0x06004FBD RID: 20413 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Insert(int index, WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to be removed. </param>
		// Token: 0x06004FBE RID: 20414 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
