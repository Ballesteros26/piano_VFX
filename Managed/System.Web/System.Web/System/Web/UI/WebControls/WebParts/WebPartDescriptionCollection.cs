using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Consists of a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> objects to be used with catalogs of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. This class cannot be inherited.</summary>
	// Token: 0x020007A4 RID: 1956
	public sealed class WebPartDescriptionCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> class. </summary>
		// Token: 0x06004ED8 RID: 20184 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDescriptionCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> object by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> objects.</summary>
		/// <param name="webPartDescriptions">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> objects that correspond to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a catalog. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartDescriptions" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A member of <paramref name="webPartDescriptions" /> is null-or-A member of <paramref name="webPartDescriptions" /> is not an object of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" />-or-A member of <paramref name="webPartDescriptions" /> has a duplicate <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDescription.ID" /> property</exception>
		// Token: 0x06004ED9 RID: 20185 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDescriptionCollection(ICollection webPartDescriptions)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartDescription get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a member of the collection based on a unique string identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> in the collection whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">The string that serves as a unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> in a collection. </param>
		// Token: 0x170017F4 RID: 6132
		public WebPartDescription this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular control exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> exists in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" />.</returns>
		/// <param name="value">A particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" />. </param>
		// Token: 0x06004EDC RID: 20188 RVA: 0x000CB5C4 File Offset: 0x000C97C4
		public bool Contains(WebPartDescription value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> objects to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004EDD RID: 20189 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(WebPartDescription[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that is a member of the collection. </param>
		// Token: 0x06004EDE RID: 20190 RVA: 0x000CB5E0 File Offset: 0x000C97E0
		public int IndexOf(WebPartDescription value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
