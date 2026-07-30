using System;

namespace System.Data
{
	/// <summary>Occurs when a target and source DataRow have the same primary key value, and the <see cref="P:System.Data.DataSet.EnforceConstraints" /> property is set to true.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D4 RID: 212
	public class MergeFailedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.MergeFailedEventArgs" /> class with the <see cref="T:System.Data.DataTable" /> and a description of the merge conflict.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> object. </param>
		/// <param name="conflict">A description of the merge conflict. </param>
		// Token: 0x06000BCC RID: 3020 RVA: 0x00035213 File Offset: 0x00033413
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.Table = table;
			this.Conflict = conflict;
		}

		/// <summary>Returns the <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00035229 File Offset: 0x00033429
		public DataTable Table { get; }

		/// <summary>Returns a description of the merge conflict.</summary>
		/// <returns>A description of the merge conflict.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00035231 File Offset: 0x00033431
		public string Conflict { get; }
	}
}
