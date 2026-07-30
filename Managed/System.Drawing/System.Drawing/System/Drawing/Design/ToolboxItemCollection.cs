using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Represents a collection of toolbox items.</summary>
	// Token: 0x02000128 RID: 296
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ToolboxItemCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> to fill the new collection with. </param>
		// Token: 0x06000D85 RID: 3461 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public ToolboxItemCollection(ToolboxItemCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified array of toolbox items.</summary>
		/// <param name="value">An array of type <see cref="T:System.Drawing.Design.ToolboxItem" /> containing the toolbox items to fill the collection with. </param>
		// Token: 0x06000D86 RID: 3462 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public ToolboxItemCollection(ToolboxItem[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Design.ToolboxItem" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItem" /> at each valid index in the collection.</returns>
		/// <param name="index">The index of the object to get or set. </param>
		// Token: 0x170003A7 RID: 935
		public ToolboxItem this[int index]
		{
			get
			{
				return (ToolboxItem)base.InnerList[index];
			}
		}

		/// <summary>Indicates whether the collection contains the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to search the collection for. </param>
		// Token: 0x06000D88 RID: 3464 RVA: 0x0001D8C3 File Offset: 0x0001BAC3
		public bool Contains(ToolboxItem value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection to the specified array beginning with the specified destination index.</summary>
		/// <param name="array">The array to copy to. </param>
		/// <param name="index">The index to begin copying to. </param>
		// Token: 0x06000D89 RID: 3465 RVA: 0x0001D8D1 File Offset: 0x0001BAD1
		public void CopyTo(ToolboxItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to get the index of in the collection. </param>
		// Token: 0x06000D8A RID: 3466 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public int IndexOf(ToolboxItem value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
