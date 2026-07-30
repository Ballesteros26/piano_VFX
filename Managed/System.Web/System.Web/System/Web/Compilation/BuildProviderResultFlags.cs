using System;

namespace System.Web.Compilation
{
	/// <summary>Indicates the required behavior when a virtual path is built.</summary>
	// Token: 0x02000647 RID: 1607
	[Flags]
	public enum BuildProviderResultFlags
	{
		/// <summary>The default value; no special action is required after compilation.</summary>
		// Token: 0x040024C9 RID: 9417
		Default = 0,
		/// <summary>The compilation of the virtual path requires the containing <see cref="T:System.AppDomain" /> to be unloaded and restarted. This is only used in advanced compilation scenarios; typically, you should use the <see cref="F:System.Web.Compilation.BuildProviderResultFlags.Default" /> value.</summary>
		// Token: 0x040024CA RID: 9418
		ShutdownAppDomainOnChange = 1
	}
}
