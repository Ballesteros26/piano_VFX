using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperation" /> class represents an Active Directory Domain Services replication operation.</summary>
	// Token: 0x02000079 RID: 121
	public class ReplicationOperation
	{
		/// <summary>Contains the time that this replication operation was added to the operation queue.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time that this replication operation was added to the operation queue.</returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime TimeEnqueued
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Contains the operation number of this replication operation.</summary>
		/// <returns>An integer that contains the operation number of this replication operation.</returns>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000208C File Offset: 0x0000028C
		public int OperationNumber
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Contains the priority of this replication operation.</summary>
		/// <returns>An integer that contains the priority of this replication operation.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000208C File Offset: 0x0000028C
		public int Priority
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Contains the type of replication operation that this operation represents.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationType" /> members that indicates the type of replication operation that this operation represents.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000208C File Offset: 0x0000028C
		public ReplicationOperationType OperationType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Contains the distinguished name of the partition that is associated with this replication operation.</summary>
		/// <returns>A string that contains the distinguished name of the partition that is associated with this replication operation.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000208C File Offset: 0x0000028C
		public string PartitionName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Contains the DNS name of the source server for this replication operation.</summary>
		/// <returns>A string that contains the DNS name of the source server for this replication operation.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000208C File Offset: 0x0000028C
		public string SourceServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
