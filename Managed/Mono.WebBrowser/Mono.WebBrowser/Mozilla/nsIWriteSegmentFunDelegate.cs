using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000068 RID: 104
	// (Invoke) Token: 0x060002E7 RID: 743
	internal delegate void nsIWriteSegmentFunDelegate([MarshalAs(UnmanagedType.Interface)] nsIInputStream aInStream, IntPtr aClosure, string aFromSegment, uint aToOffset, uint aCount, out uint aWriteCount);
}
