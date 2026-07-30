using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects.</summary>
	// Token: 0x0200010F RID: 271
	public class DesignerActionItemCollection : CollectionBase
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x170001D3 RID: 467
		public DesignerActionItem this[int index]
		{
			get
			{
				return (DesignerActionItem)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the supplied <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to the current collection.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> index at which the value has been added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" />  to add.</param>
		// Token: 0x060007E8 RID: 2024 RVA: 0x0000D6B5 File Offset: 0x0000B8B5
		public int Add(DesignerActionItem value)
		{
			return base.List.Add(value);
		}

		/// <summary>Determines whether the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> contains a specific element.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> contains the specified value; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to locate in the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</param>
		// Token: 0x060007E9 RID: 2025 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		public bool Contains(DesignerActionItem value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements of the current collection into the supplied array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060007EA RID: 2026 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
		public void CopyTo(DesignerActionItem[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to locate in the collection.</param>
		// Token: 0x060007EB RID: 2027 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public int IndexOf(DesignerActionItem value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to insert.</param>
		// Token: 0x060007EC RID: 2028 RVA: 0x0000D6EE File Offset: 0x0000B8EE
		public void Insert(int index, DesignerActionItem value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> to remove from the <see cref="T:System.ComponentModel.Design.DesignerActionItemCollection" />.</param>
		// Token: 0x060007ED RID: 2029 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		public void Remove(DesignerActionItem value)
		{
			base.List.Remove(value);
		}
	}
}
