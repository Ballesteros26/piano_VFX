using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies which sections of a security descriptor to save or load.</summary>
	// Token: 0x020005C4 RID: 1476
	[Flags]
	public enum AccessControlSections
	{
		/// <summary>No sections.</summary>
		// Token: 0x04002119 RID: 8473
		None = 0,
		/// <summary>The system access control list (SACL).</summary>
		// Token: 0x0400211A RID: 8474
		Audit = 1,
		/// <summary>The discretionary access control list (DACL).</summary>
		// Token: 0x0400211B RID: 8475
		Access = 2,
		/// <summary>The owner.</summary>
		// Token: 0x0400211C RID: 8476
		Owner = 4,
		/// <summary>The primary group.</summary>
		// Token: 0x0400211D RID: 8477
		Group = 8,
		/// <summary>The entire security descriptor.</summary>
		// Token: 0x0400211E RID: 8478
		All = 15
	}
}
