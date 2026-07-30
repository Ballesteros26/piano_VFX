using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the direction of a trust.</summary>
	// Token: 0x0200008A RID: 138
	public enum TrustDirection
	{
		/// <summary>This is a trusting domain or forest. The other domain or forest has access to the resources of this domain or forest. This domain or forest does not have access to resources that belong to the other domain or forest.</summary>
		// Token: 0x04000172 RID: 370
		Inbound = 1,
		/// <summary>This is a trusted domain or forest. This domain or forest has access to resources of the other domain or forest. The other domain or forest does not have access to the resources of this domain or forest.</summary>
		// Token: 0x04000173 RID: 371
		Outbound,
		/// <summary>Each domain or forest has access to the resources of the other domain or forest.</summary>
		// Token: 0x04000174 RID: 372
		Bidirectional
	}
}
