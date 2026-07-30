using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies options used for key creation.</summary>
	// Token: 0x02000070 RID: 112
	[Flags]
	public enum CngKeyCreationOptions
	{
		/// <summary>No key creation options are used.</summary>
		// Token: 0x040002C6 RID: 710
		None = 0,
		/// <summary>A machine-wide key is created.</summary>
		// Token: 0x040002C7 RID: 711
		MachineKey = 32,
		/// <summary>The existing key is overwritten during key creation.</summary>
		// Token: 0x040002C8 RID: 712
		OverwriteExistingKey = 128
	}
}
