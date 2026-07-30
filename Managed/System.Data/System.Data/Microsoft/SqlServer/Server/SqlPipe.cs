using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Allows managed stored procedures running in-process on a SQL Server database to return results back to the caller. This class cannot be inherited.</summary>
	// Token: 0x020003C4 RID: 964
	public sealed class SqlPipe
	{
		// Token: 0x06002E43 RID: 11843 RVA: 0x00005C14 File Offset: 0x00003E14
		private SqlPipe()
		{
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> is in the mode of sending single result sets back to the client. This property is read-only.</summary>
		/// <returns>true if the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method has been called and the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> is in the mode of sending single result sets back to the client; otherwise false.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000061D5 File Offset: 0x000043D5
		public bool IsSendingResults
		{
			get
			{
				return false;
			}
		}

		/// <summary>Executes the command passed as a parameter and sends the results to the client.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to be executed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method is not supported on commands bound to out-of-process connections.</exception>
		// Token: 0x06002E45 RID: 11845 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void ExecuteAndSend(SqlCommand command)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a string message directly to the client or current output consumer.</summary>
		/// <param name="message">The message string to be sent to the client.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="message" /> is greater than 4,000 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="message" /> is null. </exception>
		// Token: 0x06002E46 RID: 11846 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void Send(string message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a multirow result set directly to the client or current output consumer.</summary>
		/// <param name="reader">The multirow result set to be sent to the client: a <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> is null. </exception>
		// Token: 0x06002E47 RID: 11847 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void Send(SqlDataReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a single-row result set directly to the client or current output consumer.</summary>
		/// <param name="record">The single-row result set sent to the client: a <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is null. </exception>
		// Token: 0x06002E48 RID: 11848 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void Send(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the beginning of a result set to be sent back to the client, and uses the record parameter to construct the metadata that describes the result set.</summary>
		/// <param name="record">A <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object from which metadata is extracted and used to describe the result set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="record" /> has no columns or has not been initialized.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method other than <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsRow(Microsoft.SqlServer.Server.SqlDataRecord)" /> or <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsEnd" /> was called after the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method.</exception>
		// Token: 0x06002E49 RID: 11849 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void SendResultsStart(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a single row of data back to the client.</summary>
		/// <param name="record">A <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object with the column values for the row to be sent to the client. The schema for the record must match the schema described by the metadata of the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> passed to the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method was not previously called.</exception>
		// Token: 0x06002E4A RID: 11850 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void SendResultsRow(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the end of a result set, and returns the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> instance back to the initial state.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method was not previously called.</exception>
		// Token: 0x06002E4B RID: 11851 RVA: 0x0005A8C6 File Offset: 0x00058AC6
		public void SendResultsEnd()
		{
			throw new NotImplementedException();
		}
	}
}
