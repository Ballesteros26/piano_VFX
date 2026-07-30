using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies categories of functionality potentially harmful to the host if invoked by a method or class.</summary>
	// Token: 0x02000593 RID: 1427
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum HostProtectionResource
	{
		/// <summary>Exposes no host resources.</summary>
		// Token: 0x04002049 RID: 8265
		None = 0,
		/// <summary>Exposes synchronization.</summary>
		// Token: 0x0400204A RID: 8266
		Synchronization = 1,
		/// <summary>Exposes state that might be shared between threads.</summary>
		// Token: 0x0400204B RID: 8267
		SharedState = 2,
		/// <summary>Might create or destroy other processes.</summary>
		// Token: 0x0400204C RID: 8268
		ExternalProcessMgmt = 4,
		/// <summary>Might exit the current process, terminating the server.</summary>
		// Token: 0x0400204D RID: 8269
		SelfAffectingProcessMgmt = 8,
		/// <summary>Creates or manipulates threads other than its own, which might be harmful to the host.</summary>
		// Token: 0x0400204E RID: 8270
		ExternalThreading = 16,
		/// <summary>Manipulates threads in a way that only affects user code.</summary>
		// Token: 0x0400204F RID: 8271
		SelfAffectingThreading = 32,
		/// <summary>Exposes the security infrastructure.</summary>
		// Token: 0x04002050 RID: 8272
		SecurityInfrastructure = 64,
		/// <summary>Exposes the user interface.</summary>
		// Token: 0x04002051 RID: 8273
		UI = 128,
		/// <summary>Might cause a resource leak on termination, if not protected by a safe handle or some other means of ensuring the release of resources.</summary>
		// Token: 0x04002052 RID: 8274
		MayLeakOnAbort = 256,
		/// <summary>Exposes all host resources.</summary>
		// Token: 0x04002053 RID: 8275
		All = 511
	}
}
