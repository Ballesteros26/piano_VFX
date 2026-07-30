using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates a specific type of replication operation.</summary>
	// Token: 0x0200007C RID: 124
	public enum ReplicationOperationType
	{
		/// <summary>Indicates an inbound replication over an existing replication agreement from a direct replication partner.</summary>
		// Token: 0x04000145 RID: 325
		Sync,
		/// <summary>Indicates the addition of a replication agreement for a new direct replication partner.</summary>
		// Token: 0x04000146 RID: 326
		Add,
		/// <summary>Indicates the removal of a replication agreement for an existing direct replication partner.</summary>
		// Token: 0x04000147 RID: 327
		Delete,
		/// <summary>Indicates the modification of a replication agreement for an existing direct replication partner.</summary>
		// Token: 0x04000148 RID: 328
		Modify,
		/// <summary>Indicates the addition, deletion, or update of outbound change notification data for a direct replication partner.</summary>
		// Token: 0x04000149 RID: 329
		UpdateReference
	}
}
