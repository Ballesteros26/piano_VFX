using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000088 RID: 136
	public class ColumnClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> class.</summary>
		/// <param name="column">The zero-based index of the column that is clicked. </param>
		// Token: 0x06000628 RID: 1576 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		public ColumnClickEventArgs(int column)
		{
			this.column = column;
		}

		/// <summary>Gets the zero-based index of the column that is clicked.</summary>
		/// <returns>The zero-based index within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the column that is clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04000724 RID: 1828
		private int column;
	}
}
