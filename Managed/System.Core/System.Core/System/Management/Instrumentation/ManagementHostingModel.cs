using System;

namespace System.Management.Instrumentation
{
	/// <summary>Defines values that specify the hosting model for the provider.</summary>
	// Token: 0x02000372 RID: 882
	public enum ManagementHostingModel
	{
		/// <summary>Activates the provider as a decoupled provider.</summary>
		// Token: 0x04000BCA RID: 3018
		Decoupled,
		/// <summary>Activates the provider in the provider host process that is running under the LocalService account.</summary>
		// Token: 0x04000BCB RID: 3019
		LocalService = 2,
		/// <summary>Activates the provider in the provider host process that is running under the LocalSystem account.</summary>
		// Token: 0x04000BCC RID: 3020
		LocalSystem,
		/// <summary>Activates the provider in the provider host process that is running under the NetworkService account.</summary>
		// Token: 0x04000BCD RID: 3021
		NetworkService = 1
	}
}
