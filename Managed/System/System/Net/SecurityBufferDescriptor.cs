using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000449 RID: 1097
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityBufferDescriptor
	{
		// Token: 0x060020B9 RID: 8377 RVA: 0x0007F3DE File Offset: 0x0007D5DE
		public SecurityBufferDescriptor(int count)
		{
			this.Version = 0;
			this.Count = count;
			this.UnmanagedPointer = null;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("TRAVE")]
		internal void DebugDump()
		{
		}

		// Token: 0x04001D44 RID: 7492
		public readonly int Version;

		// Token: 0x04001D45 RID: 7493
		public readonly int Count;

		// Token: 0x04001D46 RID: 7494
		public unsafe void* UnmanagedPointer;
	}
}
