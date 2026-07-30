using System;

namespace System.Web
{
	/// <summary>Specifies why the <see cref="T:System.AppDomain" /> class shut down.</summary>
	// Token: 0x02000062 RID: 98
	public enum ApplicationShutdownReason
	{
		/// <summary>No shutdown reason was provided. </summary>
		// Token: 0x04000E3A RID: 3642
		None,
		/// <summary>The hosting environment shut down the application domain.</summary>
		// Token: 0x04000E3B RID: 3643
		HostingEnvironment,
		/// <summary>A change was made to the Global.asax file. </summary>
		// Token: 0x04000E3C RID: 3644
		ChangeInGlobalAsax,
		/// <summary>A change was made to the application-level configuration file.</summary>
		// Token: 0x04000E3D RID: 3645
		ConfigurationChange,
		/// <summary>A call was made to <see cref="M:System.Web.HttpRuntime.UnloadAppDomain" />. </summary>
		// Token: 0x04000E3E RID: 3646
		UnloadAppDomainCalled,
		/// <summary>A change was made in the code access security policy file. </summary>
		// Token: 0x04000E3F RID: 3647
		ChangeInSecurityPolicyFile,
		/// <summary>A change was made to the Bin folder or to files in it. </summary>
		// Token: 0x04000E40 RID: 3648
		BinDirChangeOrDirectoryRename,
		/// <summary>A change was made to the App_Browsers folder or to files in it. </summary>
		// Token: 0x04000E41 RID: 3649
		BrowsersDirChangeOrDirectoryRename,
		/// <summary>A change was made to the App_Code folder or to files in it. </summary>
		// Token: 0x04000E42 RID: 3650
		CodeDirChangeOrDirectoryRename,
		/// <summary>A change was made to the App_GlobalResources folder or to files in it. </summary>
		// Token: 0x04000E43 RID: 3651
		ResourcesDirChangeOrDirectoryRename,
		/// <summary>The maximum idle time limit was reached. </summary>
		// Token: 0x04000E44 RID: 3652
		IdleTimeout,
		/// <summary>A change was made to the physical path of the application. </summary>
		// Token: 0x04000E45 RID: 3653
		PhysicalApplicationPathChanged,
		/// <summary>A call was made to <see cref="M:System.Web.HttpRuntime.Close" />. </summary>
		// Token: 0x04000E46 RID: 3654
		HttpRuntimeClose,
		/// <summary>An <see cref="T:System.AppDomain" /> initialization error occurred. </summary>
		// Token: 0x04000E47 RID: 3655
		InitializationError,
		/// <summary>The maximum number of dynamic recompiles of resources was reached.</summary>
		// Token: 0x04000E48 RID: 3656
		MaxRecompilationsReached,
		/// <summary>The compilation system shut the application domain. The <see cref="F:System.Web.ApplicationShutdownReason.BuildManagerChange" /> member is introduced in the .NET Framework version 3.5.  For more information, see .NET Framework Versions and Dependencies.</summary>
		// Token: 0x04000E49 RID: 3657
		BuildManagerChange
	}
}
