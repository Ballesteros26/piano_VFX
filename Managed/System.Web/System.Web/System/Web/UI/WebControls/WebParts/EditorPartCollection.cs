using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls used for editing the properties, layout, appearance, and behavior of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. This class cannot be inherited. </summary>
	// Token: 0x02000484 RID: 1156
	public sealed class EditorPartCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class.</summary>
		// Token: 0x06003467 RID: 13415 RVA: 0x0008A99C File Offset: 0x00088B9C
		public EditorPartCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls. </param>
		// Token: 0x06003468 RID: 13416 RVA: 0x0008AB24 File Offset: 0x00088D24
		public EditorPartCollection(ICollection editorParts)
		{
			foreach (object obj in editorParts)
			{
				base.InnerList.Add(obj);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls, and an <see cref="T:System.Collections.ICollection" /> collection of additional <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="existingEditorParts">An <see cref="T:System.Collections.ICollection" /> of existing <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone. </param>
		/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls not in a zone, but created programmatically. </param>
		// Token: 0x06003469 RID: 13417 RVA: 0x0008AB80 File Offset: 0x00088D80
		public EditorPartCollection(EditorPartCollection existingEditorParts, ICollection editorParts)
		{
			foreach (object obj in existingEditorParts)
			{
				base.InnerList.Add(obj);
			}
			foreach (object obj2 in editorParts)
			{
				base.InnerList.Add(obj2);
			}
		}

		/// <summary>Returns a value that indicates whether a particular control is in the collection.</summary>
		/// <returns>A Boolean value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> is in the collection.</returns>
		/// <param name="editorPart">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> being tested for its status as a member of the collection. </param>
		// Token: 0x0600346A RID: 13418 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		public bool Contains(EditorPart editorPart)
		{
			return base.InnerList.Contains(editorPart);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="array">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> to contain the copied collection of controls. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x0600346B RID: 13419 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(EditorPart[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that corresponds to the index of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control in the collection.</returns>
		/// <param name="editorPart">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> that is a member of the collection. </param>
		// Token: 0x0600346C RID: 13420 RVA: 0x0008A9F5 File Offset: 0x00088BF5
		public int IndexOf(EditorPart editorPart)
		{
			return base.InnerList.IndexOf(editorPart);
		}

		/// <summary>Returns a specific member of the collection according to a unique identifier.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> at the specified index in the collection. </returns>
		/// <param name="index">The index of a particular <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> in a collection. </param>
		// Token: 0x17001077 RID: 4215
		public EditorPart this[int index]
		{
			get
			{
				return (EditorPart)base.InnerList[index];
			}
		}

		/// <summary>References a static, read-only, empty instance of the collection. </summary>
		// Token: 0x04001D0E RID: 7438
		public static readonly EditorPartCollection Empty = new EditorPartCollection();
	}
}
