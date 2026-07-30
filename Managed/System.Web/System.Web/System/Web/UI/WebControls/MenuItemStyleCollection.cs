using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003D7 RID: 983
	public sealed class MenuItemStyleCollection : StateManagedCollection
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x00064F65 File Offset: 0x00063165
		internal MenuItemStyleCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object to the end of the current collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to append to the end of the current collection.</param>
		// Token: 0x06002A4F RID: 10831 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(MenuItemStyle style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to find.</param>
		// Token: 0x06002A50 RID: 10832 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(MenuItemStyle style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects that received the copied items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06002A51 RID: 10833 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(MenuItemStyle[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0006F073 File Offset: 0x0006D273
		protected override object CreateKnownType(int index)
		{
			return new MenuItemStyle();
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0006F07A File Offset: 0x0006D27A
		protected override Type[] GetKnownTypes()
		{
			return MenuItemStyleCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />, if found; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to locate.</param>
		// Token: 0x06002A54 RID: 10836 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(MenuItemStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object into the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to insert.</param>
		// Token: 0x06002A55 RID: 10837 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, MenuItemStyle style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object from the collection.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
		// Token: 0x06002A56 RID: 10838 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(MenuItemStyle style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
		// Token: 0x06002A57 RID: 10839 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index from the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> at the specified index in the collection.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to retrieve.</param>
		// Token: 0x17000D89 RID: 3465
		public MenuItemStyle this[int i]
		{
			get
			{
				return (MenuItemStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0006F08F File Offset: 0x0006D28F
		protected override void SetDirtyObject(object o)
		{
			((MenuItemStyle)o).SetDirty();
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0006F09C File Offset: 0x0006D29C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
		}

		// Token: 0x04001ADA RID: 6874
		private static Type[] types = new Type[] { typeof(MenuItemStyle) };
	}
}
