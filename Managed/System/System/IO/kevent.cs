using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020003DE RID: 990
	internal struct kevent : IDisposable
	{
		// Token: 0x06001E2F RID: 7727 RVA: 0x00077FAA File Offset: 0x000761AA
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x04001AA1 RID: 6817
		public UIntPtr ident;

		// Token: 0x04001AA2 RID: 6818
		public EventFilter filter;

		// Token: 0x04001AA3 RID: 6819
		public EventFlags flags;

		// Token: 0x04001AA4 RID: 6820
		public FilterFlags fflags;

		// Token: 0x04001AA5 RID: 6821
		public IntPtr data;

		// Token: 0x04001AA6 RID: 6822
		public IntPtr udata;
	}
}
