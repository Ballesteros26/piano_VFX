using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanging" /> event. </summary>
	// Token: 0x02000090 RID: 144
	public class ColumnWidthChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class with the specified column index and width.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width for the column.</param>
		// Token: 0x06000669 RID: 1641 RVA: 0x0001D85C File Offset: 0x0001BA5C
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth)
			: this(columnIndex, newWidth, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class, specifying the column index and width and whether to cancel the event.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width of the column.</param>
		/// <param name="cancel">true to cancel the width change; otherwise, false.</param>
		// Token: 0x0600066A RID: 1642 RVA: 0x0001D868 File Offset: 0x0001BA68
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth, bool cancel)
			: base(cancel)
		{
			this.column_index = columnIndex;
			this.new_width = newWidth;
		}

		/// <summary>Gets the index of the column whose width is changing.</summary>
		/// <returns>The index of the column whose width is changing.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001D880 File Offset: 0x0001BA80
		public int ColumnIndex
		{
			get
			{
				return this.column_index;
			}
		}

		/// <summary>Gets or sets the new width for the column.</summary>
		/// <returns>The new width for the column.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001D888 File Offset: 0x0001BA88
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001D890 File Offset: 0x0001BA90
		public int NewWidth
		{
			get
			{
				return this.new_width;
			}
			set
			{
				this.new_width = value;
			}
		}

		// Token: 0x0400073F RID: 1855
		private int column_index;

		// Token: 0x04000740 RID: 1856
		private int new_width;
	}
}
