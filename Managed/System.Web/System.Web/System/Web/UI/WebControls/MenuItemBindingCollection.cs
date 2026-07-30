using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects.</summary>
	// Token: 0x020003D3 RID: 979
	public sealed class MenuItemBindingCollection : StateManagedCollection
	{
		// Token: 0x06002A19 RID: 10777 RVA: 0x00064F65 File Offset: 0x00063165
		internal MenuItemBindingCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the end of the collection.</summary>
		/// <returns>The index at which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> was inserted in the collection.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to append to the end of the collection.</param>
		// Token: 0x06002A1A RID: 10778 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(MenuItemBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to find.</param>
		// Token: 0x06002A1B RID: 10779 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(MenuItemBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects that receives the copied items from the collection.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06002A1C RID: 10780 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(MenuItemBinding[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0006E827 File Offset: 0x0006CA27
		protected override object CreateKnownType(int index)
		{
			return new MenuItemBinding();
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0006E82E File Offset: 0x0006CA2E
		protected override Type[] GetKnownTypes()
		{
			return MenuItemBindingCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to determine the index of.</param>
		// Token: 0x06002A1F RID: 10783 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(MenuItemBinding value)
		{
			return ((IList)this).IndexOf(value);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</param>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to insert.</param>
		// Token: 0x06002A20 RID: 10784 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, MenuItemBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to remove from the collection.</param>
		// Token: 0x06002A21 RID: 10785 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(MenuItemBinding binding)
		{
			((IList)this).Remove(binding);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The zero-based index location of the menu item binding to remove.</param>
		// Token: 0x06002A22 RID: 10786 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index from the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> at the specified index in the collection.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to retrieve.</param>
		// Token: 0x17000D80 RID: 3456
		public MenuItemBinding this[int i]
		{
			get
			{
				return (MenuItemBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0006E84D File Offset: 0x0006CA4D
		protected override void SetDirtyObject(object o)
		{
			((MenuItemBinding)o).SetDirty();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0006E85A File Offset: 0x0006CA5A
		protected override void OnClear()
		{
			base.OnClear();
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0006E862 File Offset: 0x0006CA62
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00055606 File Offset: 0x00053806
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		// Token: 0x04001AD0 RID: 6864
		private static Type[] types = new Type[] { typeof(MenuItemBinding) };
	}
}
