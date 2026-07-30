using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EF RID: 495
	public class ItemChangedEventArgs : EventArgs
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x00074E64 File Offset: 0x00073064
		internal ItemChangedEventArgs(int index)
		{
			this.index = index;
		}

		/// <summary>Indicates the position of the item being changed within the list.</summary>
		/// <returns>The zero-based index to the item being changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x00074E74 File Offset: 0x00073074
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04001045 RID: 4165
		private int index;
	}
}
