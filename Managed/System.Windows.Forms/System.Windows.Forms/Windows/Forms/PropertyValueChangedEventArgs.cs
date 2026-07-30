using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	public class PropertyValueChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs" /> class.</summary>
		/// <param name="changedItem">The item in the grid that changed. </param>
		/// <param name="oldValue">The old property value. </param>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B1104 File Offset: 0x000AF304
		public PropertyValueChangedEventArgs(GridItem changedItem, object oldValue)
		{
			this.changed_item = changedItem;
			this.old_value = oldValue;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> that was changed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridItem" /> in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000B111C File Offset: 0x000AF31C
		public GridItem ChangedItem
		{
			get
			{
				return this.changed_item;
			}
		}

		/// <summary>The value of the grid item before it was changed.</summary>
		/// <returns>A object representing the old value of the property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000B1124 File Offset: 0x000AF324
		public object OldValue
		{
			get
			{
				return this.old_value;
			}
		}

		// Token: 0x0400160C RID: 5644
		private GridItem changed_item;

		// Token: 0x0400160D RID: 5645
		private object old_value;
	}
}
