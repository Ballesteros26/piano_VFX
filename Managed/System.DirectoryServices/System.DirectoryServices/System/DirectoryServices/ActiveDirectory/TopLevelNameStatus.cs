using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the forest trust account status of a top-level domain in a forest.</summary>
	// Token: 0x02000089 RID: 137
	public enum TopLevelNameStatus
	{
		/// <summary>The forest trust account is enabled.</summary>
		// Token: 0x0400016D RID: 365
		Enabled,
		/// <summary>The forest trust account was disabled on creation.</summary>
		// Token: 0x0400016E RID: 366
		NewlyCreated,
		/// <summary>The forest trust account is disabled by administrative action.</summary>
		// Token: 0x0400016F RID: 367
		AdminDisabled,
		/// <summary>The forest trust account is disabled due to a conflict with an existing forest trust account.</summary>
		// Token: 0x04000170 RID: 368
		ConflictDisabled = 4
	}
}
