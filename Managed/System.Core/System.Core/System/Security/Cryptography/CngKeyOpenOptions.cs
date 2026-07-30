using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options for opening a key.</summary>
	// Token: 0x02000071 RID: 113
	[Flags]
	public enum CngKeyOpenOptions
	{
		/// <summary>No key open options are specified.</summary>
		// Token: 0x040002CA RID: 714
		None = 0,
		/// <summary>If the <see cref="F:System.Security.Cryptography.CngKeyOpenOptions.MachineKey" /> value is not specified, a user key is opened instead.</summary>
		// Token: 0x040002CB RID: 715
		UserKey = 0,
		/// <summary>A machine-wide key is opened.</summary>
		// Token: 0x040002CC RID: 716
		MachineKey = 32,
		/// <summary>UI prompting is suppressed.</summary>
		// Token: 0x040002CD RID: 717
		Silent = 64
	}
}
