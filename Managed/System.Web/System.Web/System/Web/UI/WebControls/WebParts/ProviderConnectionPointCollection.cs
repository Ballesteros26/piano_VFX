using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of all <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects associated with a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control acting as a provider in a connection. This class cannot be inherited.</summary>
	// Token: 0x020006DA RID: 1754
	public sealed class ProviderConnectionPointCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" /> class. </summary>
		// Token: 0x06004A53 RID: 19027 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProviderConnectionPointCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" /> class using the specified collection of provider connection points.</summary>
		/// <param name="connectionPoints">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects used to create the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="connectionPoints" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The collection contains a null connection point.- or -The collection contains an object that is not of type <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" />.- or -There are duplicate IDs in the collection of connection points. </exception>
		// Token: 0x06004A54 RID: 19028 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ProviderConnectionPointCollection(ICollection connectionPoints)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default connection point from the collection of provider connection points associated with a particular control.</summary>
		/// <returns>The default <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> from a <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" />.</returns>
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProviderConnectionPoint Default
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ProviderConnectionPoint get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a member of the collection based on a unique string identifier. </summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">A <see cref="T:System.String" /> serving as the unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> in the collection.</param>
		// Token: 0x170016F2 RID: 5874
		public ProviderConnectionPoint this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> object exists in the collection. </summary>
		/// <returns>true if the provider connection point is contained in the collection; otherwise, false.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> to search for.</param>
		// Token: 0x06004A58 RID: 19032 RVA: 0x000CA738 File Offset: 0x000C8938
		public bool Contains(ProviderConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects that receives the copied items of the collection.</param>
		/// <param name="index">The starting point in the array at which to insert the collection contents.</param>
		// Token: 0x06004A59 RID: 19033 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(ProviderConnectionPoint[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> object within a collection.</summary>
		/// <returns>An integer that indicates the zero-based index position of the specified <paramref name="connectionPoint" /> within a <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" />.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> to locate.</param>
		// Token: 0x06004A5A RID: 19034 RVA: 0x000CA754 File Offset: 0x000C8954
		public int IndexOf(ProviderConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
