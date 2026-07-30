using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines a set of flags used when registering assemblies.</summary>
	// Token: 0x020008E7 RID: 2279
	[Flags]
	[ComVisible(true)]
	public enum AssemblyRegistrationFlags
	{
		/// <summary>Indicates no special settings.</summary>
		// Token: 0x04002CD6 RID: 11478
		None = 0,
		/// <summary>Indicates that the code base key for the assembly should be set in the registry.</summary>
		// Token: 0x04002CD7 RID: 11479
		SetCodeBase = 1
	}
}
