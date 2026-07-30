using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the location where an environment variable is stored or retrieved in a set or get operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000215 RID: 533
	[ComVisible(true)]
	public enum EnvironmentVariableTarget
	{
		/// <summary>The environment variable is stored or retrieved from the environment block associated with the current process. </summary>
		// Token: 0x04000CB7 RID: 3255
		Process,
		/// <summary>The environment variable is stored or retrieved from the HKEY_CURRENT_USER\Environment key in the Windows operating system registry. </summary>
		// Token: 0x04000CB8 RID: 3256
		User,
		/// <summary>The environment variable is stored or retrieved from the HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment key in the Windows operating system registry. </summary>
		// Token: 0x04000CB9 RID: 3257
		Machine
	}
}
