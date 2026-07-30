using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a collection of connections for a control in a Web Parts zone. This class cannot be inherited.</summary>
	// Token: 0x020006B9 RID: 1721
	[Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class WebPartConnectionCollection : CollectionBase
	{
		// Token: 0x06004902 RID: 18690 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal WebPartConnectionCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x000CA08C File Offset: 0x000C828C
		public bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartConnection get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object with the specified connection ID.</summary>
		/// <returns>The first occurrence of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> with the specified connection ID in the collection.</returns>
		/// <param name="id">The connection ID of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to be retrieved.</param>
		// Token: 0x17001679 RID: 5753
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

		/// <summary>Adds a member to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> collection.</summary>
		/// <returns>An integer indicating the index where the connection will be added to the collection.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" />  to be added to the collection.</param>
		// Token: 0x06004907 RID: 18695 RVA: 0x000CA0A8 File Offset: 0x000C82A8
		public int Add(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object is in the collection.</summary>
		/// <returns>true if the object is in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to search for.</param>
		// Token: 0x06004908 RID: 18696 RVA: 0x000CA0C4 File Offset: 0x000C82C4
		public bool Contains(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> collection to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array into which the elements from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnectionCollection" /> are copied. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index " />is less than zero.</exception>
		// Token: 0x06004909 RID: 18697 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(WebPartConnection[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to determine the index of.</param>
		// Token: 0x0600490A RID: 18698 RVA: 0x000CA0E0 File Offset: 0x000C82E0
		public int IndexOf(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" />.</param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to insert.</param>
		// Token: 0x0600490B RID: 18699 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Insert(int index, WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartConnection" /> to be removed.</param>
		// Token: 0x0600490C RID: 18700 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(WebPartConnection value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
