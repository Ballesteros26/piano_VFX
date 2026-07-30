using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000066 RID: 102
	// (Invoke) Token: 0x060002DF RID: 735
	internal delegate void nsIReadSegmentFunDelegate([MarshalAs(UnmanagedType.Interface)] nsIOutputStream aInStream, IntPtr aClosure, string aFromSegment, uint aCount, out uint aWriteCount);
}
