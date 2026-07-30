using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the action that a custom application domain manager takes when initializing a new domain.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000213 RID: 531
	[ComVisible(true)]
	[Flags]
	public enum AppDomainManagerInitializationOptions
	{
		/// <summary>No initialization action.</summary>
		// Token: 0x04000CB3 RID: 3251
		None = 0,
		/// <summary>Register the COM callable wrapper for the current <see cref="T:System.AppDomainManager" /> with the unmanaged host. </summary>
		// Token: 0x04000CB4 RID: 3252
		RegisterWithHost = 1
	}
}
