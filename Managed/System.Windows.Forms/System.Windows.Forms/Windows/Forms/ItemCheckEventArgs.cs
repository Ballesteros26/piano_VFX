using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event of the <see cref="T:System.Windows.Forms.CheckedListBox" /> and <see cref="T:System.Windows.Forms.ListView" /> controls. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F0 RID: 496
	[ComVisible(true)]
	public class ItemCheckEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> class.</summary>
		/// <param name="index">The zero-based index of the item to change. </param>
		/// <param name="newCheckValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether to change the check box for the item to be checked, unchecked, or indeterminate. </param>
		/// <param name="currentValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether the check box for the item is currently checked, unchecked, or indeterminate. </param>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x00074E7C File Offset: 0x0007307C
		public ItemCheckEventArgs(int index, CheckState newCheckValue, CheckState currentValue)
		{
			this.index = index;
			this.newValue = newCheckValue;
			this.currentValue = currentValue;
		}

		/// <summary>Gets a value indicating the current state of the item's check box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00074E9C File Offset: 0x0007309C
		public CheckState CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		/// <summary>Gets the zero-based index of the item to change.</summary>
		/// <returns>The zero-based index of the item to change.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00074EA4 File Offset: 0x000730A4
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets or sets a value indicating whether to set the check box for the item to be checked, unchecked, or indeterminate.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00074EAC File Offset: 0x000730AC
		// (set) Token: 0x06001EFC RID: 7932 RVA: 0x00074EB4 File Offset: 0x000730B4
		public CheckState NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		// Token: 0x04001046 RID: 4166
		private CheckState currentValue;

		// Token: 0x04001047 RID: 4167
		private int index;

		// Token: 0x04001048 RID: 4168
		private CheckState newValue;
	}
}
