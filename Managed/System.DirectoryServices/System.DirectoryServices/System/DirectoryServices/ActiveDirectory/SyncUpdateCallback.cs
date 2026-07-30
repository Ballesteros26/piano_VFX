using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Receives event notifications during a replica synchronization.</summary>
	/// <returns>true if the SyncReplicaFromAllServers method invoked on a <see cref="T:System.DirectoryServices.ActiveDirectory.ADAMInstance" />,  <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object should resume; false if the SyncReplicaFromAllServers method should terminate.</returns>
	/// <param name="eventType">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersEvent" /> members that specifies the type of event.</param>
	/// <param name="targetServer">Contains the DNS name of the server that is the target of the replication. This parameter will be null if it is not used by the notification.</param>
	/// <param name="sourceServer">Contains the DNS name of the server that is the source of the replication. This parameter will be null if it is not used by the notification.</param>
	/// <param name="exception">A <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> object that contains additional information about any error that has occurred. This parameter will be null if no error has occurred.</param>
	// Token: 0x02000085 RID: 133
	// (Invoke) Token: 0x0600046B RID: 1131
	public delegate bool SyncUpdateCallback(SyncFromAllServersEvent eventType, string targetServer, string sourceServer, SyncFromAllServersOperationException exception);
}
