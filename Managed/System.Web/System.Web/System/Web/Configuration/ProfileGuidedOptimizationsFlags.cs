using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the optimization mode for an application deployment environment.</summary>
	// Token: 0x02000572 RID: 1394
	[Flags]
	public enum ProfileGuidedOptimizationsFlags
	{
		/// <summary>No optimizations are performed based on the deployment environment of the application.</summary>
		// Token: 0x04002048 RID: 8264
		None = 0,
		/// <summary>All optimizations are performed based on the deployment environment of the application.</summary>
		// Token: 0x04002049 RID: 8265
		All = 1
	}
}
