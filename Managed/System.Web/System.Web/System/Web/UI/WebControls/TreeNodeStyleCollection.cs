using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that is in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x02000432 RID: 1074
	public sealed class TreeNodeStyleCollection : StateManagedCollection
	{
		// Token: 0x06003109 RID: 12553 RVA: 0x00064F65 File Offset: 0x00063165
		internal TreeNodeStyleCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <returns>The position into which the new <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> was inserted.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to append. </param>
		// Token: 0x0600310A RID: 12554 RVA: 0x0008157B File Offset: 0x0007F77B
		public int Add(TreeNodeStyle style)
		{
			style.Font.Underline = style.Font.Underline;
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is in the collection.</summary>
		/// <returns>true, if the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to find. </param>
		// Token: 0x0600310B RID: 12555 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(TreeNodeStyle style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x0600310C RID: 12556 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(TreeNodeStyle[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0008159A File Offset: 0x0007F79A
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeStyle();
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000815A1 File Offset: 0x0007F7A1
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeStyleCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />, if found; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to locate.</param>
		// Token: 0x0600310F RID: 12559 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(TreeNodeStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" />. </param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to insert. </param>
		// Token: 0x06003110 RID: 12560 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, TreeNodeStyle style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
		// Token: 0x06003111 RID: 12561 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(TreeNodeStyle style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
		// Token: 0x06003112 RID: 12562 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to retrieve. </param>
		// Token: 0x17000F91 RID: 3985
		public TreeNodeStyle this[int i]
		{
			get
			{
				return (TreeNodeStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000815B6 File Offset: 0x0007F7B6
		protected override void SetDirtyObject(object o)
		{
			((TreeNodeStyle)o).SetDirty();
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0006F09C File Offset: 0x0006D29C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
		}

		// Token: 0x04001C2B RID: 7211
		private static Type[] types = new Type[] { typeof(TreeNodeStyle) };
	}
}
