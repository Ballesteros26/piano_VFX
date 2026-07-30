using System;

namespace System.Management.Instrumentation
{
	/// <summary>Represents the possible commit behaviors of a read/write property. It is used as the value of a parameter of the <see cref="T:System.Management.Instrumentation.ManagementConfigurationAttribute" /> attribute.</summary>
	// Token: 0x0200036E RID: 878
	public enum ManagementConfigurationType
	{
		/// <summary>Set values take effect only when Commit is called.</summary>
		// Token: 0x04000BC7 RID: 3015
		Apply,
		/// <summary>Set values are applied immediately.</summary>
		// Token: 0x04000BC8 RID: 3016
		OnCommit
	}
}
