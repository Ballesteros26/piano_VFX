using System;

namespace System.Data
{
	/// <summary>Provides additional information for the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F9 RID: 249
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.StatementCompletedEventArgs" /> class.</summary>
		/// <param name="recordCount">Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" />  event to occur.</param>
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003C228 File Offset: 0x0003A428
		public StatementCompletedEventArgs(int recordCount)
		{
			this.RecordCount = recordCount;
		}

		/// <summary>Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event to occur.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003C237 File Offset: 0x0003A437
		public int RecordCount { get; }
	}
}
