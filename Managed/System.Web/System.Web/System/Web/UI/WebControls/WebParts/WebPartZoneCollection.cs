using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> controls.</summary>
	// Token: 0x020006C3 RID: 1731
	public sealed class WebPartZoneCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneCollection" /> class.</summary>
		// Token: 0x06004981 RID: 18817 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartZoneCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneCollection" /> class by passing in a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> objects.</summary>
		/// <param name="webPartZones">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection of zones is null. </exception>
		/// <exception cref="T:System.ArgumentException">One of the objects in the collection is null or is not of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" />.</exception>
		// Token: 0x06004982 RID: 18818 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartZoneCollection(ICollection webPartZones)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public WebPartZoneBase get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a specific member of the collection by passing in a unique identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> in the collection whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">The unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> within the collection. </param>
		// Token: 0x1700169F RID: 5791
		public WebPartZoneBase this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular zone exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular zone is in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> that is checked to determine whether it is in the collection. </param>
		// Token: 0x06004985 RID: 18821 RVA: 0x000CA428 File Offset: 0x000C8628
		public bool Contains(WebPartZoneBase value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> objects.</summary>
		/// <param name="array">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> array to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004986 RID: 18822 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(WebPartZoneBase[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that is a member of the collection. </param>
		// Token: 0x06004987 RID: 18823 RVA: 0x000CA444 File Offset: 0x000C8644
		public int IndexOf(WebPartZoneBase value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
