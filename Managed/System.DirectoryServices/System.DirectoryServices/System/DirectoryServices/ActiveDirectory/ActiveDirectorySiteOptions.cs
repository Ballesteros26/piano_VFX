using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies the bit identifiers for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object site options.</summary>
	// Token: 0x02000045 RID: 69
	[Flags]
	public enum ActiveDirectorySiteOptions
	{
		/// <summary>No site options are set.</summary>
		// Token: 0x040000B9 RID: 185
		None = 0,
		/// <summary>Inter-site topology generation is disabled.</summary>
		// Token: 0x040000BA RID: 186
		AutoTopologyDisabled = 1,
		/// <summary>Topology cleanup is disabled.</summary>
		// Token: 0x040000BB RID: 187
		TopologyCleanupDisabled = 2,
		/// <summary>Automatic minimum hops topology is disabled.</summary>
		// Token: 0x040000BC RID: 188
		AutoMinimumHopDisabled = 4,
		/// <summary>Stale server detection is disabled.</summary>
		// Token: 0x040000BD RID: 189
		StaleServerDetectDisabled = 8,
		/// <summary>Automatic intra-site topology generation is disabled.</summary>
		// Token: 0x040000BE RID: 190
		AutoInterSiteTopologyDisabled = 16,
		/// <summary>Group memberships for users is enabled.</summary>
		// Token: 0x040000BF RID: 191
		GroupMembershipCachingEnabled = 32,
		/// <summary>The KCC (Knowledge Consistency Checker) is forced to operate in Windows Server 2003 behavior mode.</summary>
		// Token: 0x040000C0 RID: 192
		ForceKccWindows2003Behavior = 64,
		/// <summary>The KCC is forced to use the Windows 2000 ISTG election algorithm.</summary>
		// Token: 0x040000C1 RID: 193
		UseWindows2000IstgElection = 128,
		/// <summary>The KCC can randomly pick a bridgehead server when creating a connection.</summary>
		// Token: 0x040000C2 RID: 194
		RandomBridgeHeaderServerSelectionDisabled = 256,
		/// <summary>The KCC is allowed to use hashing when creating a replication schedule.</summary>
		// Token: 0x040000C3 RID: 195
		UseHashingForReplicationSchedule = 512,
		/// <summary>Creation of static failover connections is enabled.</summary>
		// Token: 0x040000C4 RID: 196
		RedundantServerTopologyEnabled = 1024
	}
}
