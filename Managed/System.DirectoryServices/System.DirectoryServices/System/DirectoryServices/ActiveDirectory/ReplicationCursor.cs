using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> class represents a replication operation occurrence.</summary>
	// Token: 0x02000072 RID: 114
	public class ReplicationCursor
	{
		/// <summary>Gets the name of the partition to which this replication operation was applied.</summary>
		/// <returns>The name of the partition represented by this replication cursor.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000208C File Offset: 0x0000028C
		public string PartitionName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the invocation identifier of the replication source server.</summary>
		/// <returns>The invocation identifier of the replication source server</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000208C File Offset: 0x0000028C
		public Guid SourceInvocationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the maximum update sequence number (USN) for which the destination server has accepted changes from the source server.</summary>
		/// <returns>The maximum update sequence number (USN) for which the destination server has accepted changes from the source server.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000208C File Offset: 0x0000028C
		public long UpToDatenessUsn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the replication source server.</summary>
		/// <returns>The name of the replication source server.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000208C File Offset: 0x0000028C
		public string SourceServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time of the last successful replication synchronization with the replication source server.</summary>
		/// <returns>The time at which the last successful replication synchronization occurred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not supported on Windows 2000.</exception>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime LastSuccessfulSyncTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
