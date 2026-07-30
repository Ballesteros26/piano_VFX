using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects.</summary>
	// Token: 0x02000414 RID: 1044
	public class StyleCollection : StateManagedCollection
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x00064F65 File Offset: 0x00063165
		internal StyleCollection()
		{
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location in the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />.</returns>
		/// <param name="i">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />. </param>
		// Token: 0x17000EF7 RID: 3831
		public Style this[int i]
		{
			get
			{
				return (Style)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.Style" /> object to the end of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <returns>The index at which the style was added to the collection.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to add to the collection.</param>
		// Token: 0x06002F0D RID: 12045 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(Style style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified style is contained within the collection.</summary>
		/// <returns>true if the style is in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
		// Token: 0x06002F0E RID: 12046 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(Style style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> to a one-dimensional <see cref="T:System.Web.UI.WebControls.Style" /> array, starting at the specified index of the target array.</summary>
		/// <param name="styleArray">The <see cref="T:System.Array" /> that is the destination of the copied styles. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="styleArray" /> at which copying begins.</param>
		// Token: 0x06002F0F RID: 12047 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(Style[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class, based on the single element collection returned by the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class.</returns>
		/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" />, of the type of the <see cref="T:System.Web.UI.IStateManager" /> object to create. Because the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method of <see cref="T:System.Web.UI.WebControls.StyleCollection" /> returns a list with only one type, the input <paramref name="index" /> is ignored.</param>
		// Token: 0x06002F10 RID: 12048 RVA: 0x0007CAF6 File Offset: 0x0007ACF6
		protected override object CreateKnownType(int index)
		{
			return new Style();
		}

		/// <summary>Gets an array of the <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain.</summary>
		/// <returns>An array containing one <see cref="T:System.Type" /> object for the <see cref="T:System.Web.UI.WebControls.Style" /> class, which indicates that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain <see cref="T:System.Web.UI.WebControls.Style" /> objects.</returns>
		// Token: 0x06002F11 RID: 12049 RVA: 0x0007CAFD File Offset: 0x0007ACFD
		protected override Type[] GetKnownTypes()
		{
			return new Type[] { typeof(Style) };
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the collection; otherwise, -1 if the style is not in the collection.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
		// Token: 0x06002F12 RID: 12050 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(Style style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.Style" /> object. </param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to insert into the collection. </param>
		// Token: 0x06002F13 RID: 12051 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, Style style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.Style" /> object from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to remove from the collection. </param>
		// Token: 0x06002F14 RID: 12052 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(Style style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object to remove. </param>
		// Token: 0x06002F15 RID: 12053 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Instructs the input <see cref="T:System.Web.UI.WebControls.Style" /> object contained in the collection to record its entire state to view state, rather than recording only change information.</summary>
		/// <param name="o">The <see cref="T:System.Web.UI.WebControls.Style" /> object that should serialize itself completely.</param>
		// Token: 0x06002F16 RID: 12054 RVA: 0x0007CB14 File Offset: 0x0007AD14
		protected override void SetDirtyObject(object o)
		{
			Style style = o as Style;
			if (style == null)
			{
				return;
			}
			style.SetDirty();
		}
	}
}
