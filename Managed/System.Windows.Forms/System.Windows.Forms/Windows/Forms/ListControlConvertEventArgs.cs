using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListControl.Format" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021B RID: 539
	public class ListControlConvertEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> class with the specified object, type, and list item.</summary>
		/// <param name="value">The value displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> for the displayed item.</param>
		/// <param name="listItem">The data source item to be displayed in the <see cref="T:System.Windows.Forms.ListControl" />.</param>
		// Token: 0x060021FA RID: 8698 RVA: 0x0007EF2C File Offset: 0x0007D12C
		public ListControlConvertEventArgs(object value, Type desiredType, object listItem)
			: base(value, desiredType)
		{
			this.list_item = listItem;
		}

		/// <summary>Gets a data source item.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents an item in the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0007EF40 File Offset: 0x0007D140
		public object ListItem
		{
			get
			{
				return this.list_item;
			}
		}

		// Token: 0x040011ED RID: 4589
		private object list_item;
	}
}
