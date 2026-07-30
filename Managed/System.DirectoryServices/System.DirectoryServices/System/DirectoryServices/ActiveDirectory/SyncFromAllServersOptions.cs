using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies additional options when performing a synchronization.</summary>
	// Token: 0x02000084 RID: 132
	[Flags]
	public enum SyncFromAllServersOptions
	{
		/// <summary>No synchronization options.</summary>
		// Token: 0x04000160 RID: 352
		None = 0,
		/// <summary>Aborts the synchronization if any server cannot be contacted or if any server is unreachable.</summary>
		// Token: 0x04000161 RID: 353
		AbortIfServerUnavailable = 1,
		/// <summary>Disables transitive replication. Synchronization is performed only with adjacent servers.</summary>
		// Token: 0x04000162 RID: 354
		SyncAdjacentServerOnly = 2,
		/// <summary>Disables all synchronization. The topology is analyzed and unavailable or unreachable servers are identified.</summary>
		// Token: 0x04000163 RID: 355
		CheckServerAlivenessOnly = 8,
		/// <summary>Assumes that all servers are responding. This will speed up the operation of this method, but if some servers are not responding, some transitive replications might be blocked.</summary>
		// Token: 0x04000164 RID: 356
		SkipInitialCheck = 16,
		/// <summary>Pushes changes from the home server out to all partners using transitive replication. This reverses the direction of replication and the order of execution of the replication sets from the usual mode of execution.</summary>
		// Token: 0x04000165 RID: 357
		PushChangeOutward = 32,
		/// <summary>Synchronizes across site boundaries. By default, this method attempts to synchronize only with domain controllers in the same site as the home system. Set this flag to attempt to synchronize with all domain controllers in the enterprise forest. However, the domain controllers can be synchronized only if connected by a synchronous (RPC) transport.</summary>
		// Token: 0x04000166 RID: 358
		CrossSite = 64
	}
}
