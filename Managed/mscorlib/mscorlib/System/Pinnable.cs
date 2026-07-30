using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CA RID: 202
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x04000693 RID: 1683
		public T Data;
	}
}
