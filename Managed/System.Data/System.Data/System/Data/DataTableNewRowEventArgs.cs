using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.NewRow" /> method.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000092 RID: 146
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Data.DataTableNewRowEventArgs" />.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> being added.</param>
		// Token: 0x060008C2 RID: 2242 RVA: 0x00028A4F File Offset: 0x00026C4F
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.Row = dataRow;
		}

		/// <summary>Gets the row that is being added.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> that is being added. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00028A5E File Offset: 0x00026C5E
		public DataRow Row { get; }
	}
}
