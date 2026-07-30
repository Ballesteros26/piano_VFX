using System;
using System.Runtime.InteropServices;

namespace System.Web.Services.Interop
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct CallId
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0001232D File Offset: 0x0001052D
		public CallId(string machine, int pid, IntPtr userThread, long stackPtr, string entryPoint, string destMachine)
		{
			this.szMachine = machine;
			this.dwPid = pid;
			this.userThread = userThread;
			this.addStackPointer = stackPtr;
			this.szEntryPoint = entryPoint;
			this.szDestinationMachine = destMachine;
		}

		// Token: 0x04000312 RID: 786
		public string szMachine;

		// Token: 0x04000313 RID: 787
		public int dwPid;

		// Token: 0x04000314 RID: 788
		public IntPtr userThread;

		// Token: 0x04000315 RID: 789
		public long addStackPointer;

		// Token: 0x04000316 RID: 790
		public string szEntryPoint;

		// Token: 0x04000317 RID: 791
		public string szDestinationMachine;
	}
}
