using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x02000624 RID: 1572
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x04002850 RID: 10320
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x04002851 RID: 10321
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x04002852 RID: 10322
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x04002853 RID: 10323
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x04002854 RID: 10324
		Hybrid = 8
	}
}
