using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a read-only collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
	// Token: 0x020006B8 RID: 1720
	public sealed class TransformerTypeCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class with no members in the collection. </summary>
		// Token: 0x060048FB RID: 18683 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TransformerTypeCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class containing the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="transformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="transformerTypes" /> contains objects that are not transformers.</exception>
		// Token: 0x060048FC RID: 18684 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TransformerTypeCollection(ICollection transformerTypes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class by combining an existing <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> collection with the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="existingTransformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects already contained in a <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" />. </param>
		/// <param name="transformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects to combine with the collection in the <paramref name="existingTransformerTypes" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="transformerTypes" /> contains objects that are not transformers.</exception>
		// Token: 0x060048FD RID: 18685 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TransformerTypeCollection(TransformerTypeCollection existingTransformerTypes, ICollection transformerTypes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a member of the collection based on its position in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> located at <paramref name="index" />.</returns>
		/// <param name="index">The index of a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> in a collection. </param>
		// Token: 0x17001677 RID: 5751
		public Type this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular transformer exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular transformer is in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is checked to determine whether it is in the collection. </param>
		// Token: 0x060048FF RID: 18687 RVA: 0x000CA054 File Offset: 0x000C8254
		public bool Contains(Type value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004900 RID: 18688 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void CopyTo(Type[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member in the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is a member of the collection.</param>
		// Token: 0x06004901 RID: 18689 RVA: 0x000CA070 File Offset: 0x000C8270
		public int IndexOf(Type value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Creates a collection for storing transformers. </summary>
		// Token: 0x040025DB RID: 9691
		public static readonly TransformerTypeCollection Empty;
	}
}
