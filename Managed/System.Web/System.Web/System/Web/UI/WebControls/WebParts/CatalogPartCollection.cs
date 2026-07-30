using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls used to provide catalogs of Web server controls that end users can add to a Web page. This class cannot be inherited.</summary>
	// Token: 0x020007A3 RID: 1955
	public sealed class CatalogPartCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> class.</summary>
		// Token: 0x06004ED0 RID: 20176 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CatalogPartCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> class by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls.</summary>
		/// <param name="catalogParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls. </param>
		// Token: 0x06004ED1 RID: 20177 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CatalogPartCollection(ICollection catalogParts)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> class by passing in an <see cref="T:System.Collections.ICollection" /> collection of the existing <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a zone, and an additional collection of controls.</summary>
		/// <param name="existingCatalogParts">An <see cref="T:System.Collections.ICollection" /> of existing <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a zone. </param>
		/// <param name="catalogParts">An <see cref="T:System.Collections.ICollection" /> of additional <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls. </param>
		// Token: 0x06004ED2 RID: 20178 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CatalogPartCollection(CatalogPartCollection existingCatalogParts, ICollection catalogParts)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public CatalogPart get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a member of the collection based on a unique string identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> in the collection whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">The unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" />  in a collection. </param>
		// Token: 0x170017F3 RID: 6131
		public CatalogPart this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular control exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular control is in the collection.</returns>
		/// <param name="catalogPart">A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" />  that is checked to determine whether it is in the collection. </param>
		// Token: 0x06004ED5 RID: 20181 RVA: 0x000CB58C File Offset: 0x000C978C
		public bool Contains(CatalogPart catalogPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" />  objects to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004ED6 RID: 20182 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(CatalogPart[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> that is a member of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" />.</returns>
		/// <param name="catalogPart">A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" />  that is a member of the collection.</param>
		// Token: 0x06004ED7 RID: 20183 RVA: 0x000CB5A8 File Offset: 0x000C97A8
		public int IndexOf(CatalogPart catalogPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>References a static, read-only, empty instance of the collection.</summary>
		// Token: 0x040025F1 RID: 9713
		public static readonly CatalogPartCollection Empty;
	}
}
