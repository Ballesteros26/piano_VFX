using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the set of arguments passed to the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventHandler" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000143 RID: 323
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object.</summary>
		/// <param name="rowsCopied">An <see cref="T:System.Int64" /> that indicates the number of rows copied during the current bulk copy operation. </param>
		// Token: 0x0600101C RID: 4124 RVA: 0x00051310 File Offset: 0x0004F510
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		/// <summary>Gets or sets a value that indicates whether the bulk copy operation should be aborted.</summary>
		/// <returns>true if the bulk copy operation should be aborted; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0005131F File Offset: 0x0004F51F
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00051327 File Offset: 0x0004F527
		public bool Abort
		{
			get
			{
				return this._abort;
			}
			set
			{
				this._abort = value;
			}
		}

		/// <summary>Gets a value that returns the number of rows copied during the current bulk copy operation.</summary>
		/// <returns>int that returns the number of rows copied.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00051330 File Offset: 0x0004F530
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04000A97 RID: 2711
		private bool _abort;

		// Token: 0x04000A98 RID: 2712
		private long _rowsCopied;
	}
}
