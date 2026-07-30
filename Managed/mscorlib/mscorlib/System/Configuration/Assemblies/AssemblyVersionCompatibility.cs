using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	/// <summary>Defines the different types of assembly version compatibility. This feature is not available in version 1.0 of the .NET Framework.</summary>
	// Token: 0x02000262 RID: 610
	[ComVisible(true)]
	[Serializable]
	public enum AssemblyVersionCompatibility
	{
		/// <summary>The assembly cannot execute with other versions if they are executing on the same machine.</summary>
		// Token: 0x04000FB3 RID: 4019
		SameMachine = 1,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same process.</summary>
		// Token: 0x04000FB4 RID: 4020
		SameProcess,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same application domain.</summary>
		// Token: 0x04000FB5 RID: 4021
		SameDomain
	}
}
