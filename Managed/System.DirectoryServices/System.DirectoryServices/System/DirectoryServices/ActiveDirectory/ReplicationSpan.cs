using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Determines the span of a replication connection.</summary>
	// Token: 0x0200007E RID: 126
	public enum ReplicationSpan
	{
		/// <summary>The source and destination servers are in the same site.</summary>
		// Token: 0x0400014F RID: 335
		IntraSite,
		/// <summary>The source and destination servers are in different sites.</summary>
		// Token: 0x04000150 RID: 336
		InterSite
	}
}
