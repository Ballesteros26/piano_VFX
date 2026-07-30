using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Used in the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncUpdateCallback" /> delegate to specify the type of synchronization event. </summary>
	// Token: 0x02000082 RID: 130
	public enum SyncFromAllServersEvent
	{
		/// <summary>An error occurred.</summary>
		// Token: 0x0400015B RID: 347
		Error,
		/// <summary>Synchronization of two servers has started.</summary>
		// Token: 0x0400015C RID: 348
		SyncStarted,
		/// <summary>Synchronization of two servers has just completed.</summary>
		// Token: 0x0400015D RID: 349
		SyncCompleted,
		/// <summary>The entire replication process has completed.</summary>
		// Token: 0x0400015E RID: 350
		Finished
	}
}
