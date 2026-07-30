using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Contains information about a <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" />. </summary>
	// Token: 0x02000080 RID: 128
	public enum SyncFromAllServersErrorCategory
	{
		/// <summary>The server could not be contacted for replication.</summary>
		// Token: 0x04000157 RID: 343
		ErrorContactingServer,
		/// <summary>The replication operation failed to complete.</summary>
		// Token: 0x04000158 RID: 344
		ErrorReplicating,
		/// <summary>The server is not reachable.</summary>
		// Token: 0x04000159 RID: 345
		ServerUnreachable
	}
}
