using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> class contains information about an Active Directory Domain Services replication operation.</summary>
	// Token: 0x0200007B RID: 123
	public class ReplicationOperationInformation
	{
		/// <summary>Gets the time that this replication operation started.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time that this replication operation started.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime OperationStartTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current replication operation.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperation" /> object that represents the current replication operation.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000208C File Offset: 0x0000028C
		public ReplicationOperation CurrentOperation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the replication operations that have not been run.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationCollection" /> object that contains the pending replication operations.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000208C File Offset: 0x0000028C
		public ReplicationOperationCollection PendingOperations
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
