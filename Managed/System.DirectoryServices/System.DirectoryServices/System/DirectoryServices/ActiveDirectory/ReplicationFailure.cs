using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Contains information about a failed replication attempt.</summary>
	// Token: 0x02000074 RID: 116
	public class ReplicationFailure
	{
		/// <summary>Gets the DNS name of the source server.</summary>
		/// <returns>The DNS name of the source server.</returns>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000208C File Offset: 0x0000028C
		public string SourceServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the date and time that the first failure occurred.</summary>
		/// <returns>The date and time that the first failure occurred when replicating from the source server.</returns>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime FirstFailureTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of consecutive failures since the last successful replication. </summary>
		/// <returns>The number of consecutive failures since the last successful replication.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000208C File Offset: 0x0000028C
		public int ConsecutiveFailureCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the error code for the most recent failure.</summary>
		/// <returns>An HRESULT that contains the error code that is associated with the most recent failure. This will be ERROR_SUCCESS if the specific error is unavailable.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000208C File Offset: 0x0000028C
		public int LastErrorCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the error message for the most recent failure.</summary>
		/// <returns>The error message for the most recent failure.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000208C File Offset: 0x0000028C
		public string LastErrorMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
