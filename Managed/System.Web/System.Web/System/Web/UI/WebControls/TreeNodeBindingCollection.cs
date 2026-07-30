using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200042E RID: 1070
	public sealed class TreeNodeBindingCollection : StateManagedCollection
	{
		// Token: 0x060030D0 RID: 12496 RVA: 0x00064F65 File Offset: 0x00063165
		internal TreeNodeBindingCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <returns>The zero-based index of the location of the added <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to append. </param>
		// Token: 0x060030D1 RID: 12497 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(TreeNodeBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> is in the collection; otherwise, false.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to find.</param>
		// Token: 0x060030D2 RID: 12498 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(TreeNodeBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="bindingArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</param>
		/// <param name="index">The position in <paramref name="bindingArray" /> at which to start receiving the copied content.</param>
		// Token: 0x060030D3 RID: 12499 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(TreeNodeBinding[] bindingArray, int index)
		{
			((ICollection)this).CopyTo(bindingArray, index);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x00080A47 File Offset: 0x0007EC47
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeBinding();
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x00080A4E File Offset: 0x0007EC4E
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeBindingCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="binding" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />, if found; otherwise, -1.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to locate.</param>
		// Token: 0x060030D6 RID: 12502 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(TreeNodeBinding binding)
		{
			return ((IList)this).IndexOf(binding);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" />. </param>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to insert. </param>
		// Token: 0x060030D7 RID: 12503 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, TreeNodeBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
		// Token: 0x060030D8 RID: 12504 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(TreeNodeBinding binding)
		{
			((IList)this).Remove(binding);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
		// Token: 0x060030D9 RID: 12505 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to retrieve. </param>
		// Token: 0x17000F86 RID: 3974
		public TreeNodeBinding this[int i]
		{
			get
			{
				return (TreeNodeBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x00080A63 File Offset: 0x0007EC63
		protected override void SetDirtyObject(object o)
		{
			((TreeNodeBinding)o).SetDirty();
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0006E85A File Offset: 0x0006CA5A
		protected override void OnClear()
		{
			base.OnClear();
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0006E862 File Offset: 0x0006CA62
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x00055606 File Offset: 0x00053806
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		// Token: 0x04001C1A RID: 7194
		private static Type[] types = new Type[] { typeof(TreeNodeBinding) };
	}
}
