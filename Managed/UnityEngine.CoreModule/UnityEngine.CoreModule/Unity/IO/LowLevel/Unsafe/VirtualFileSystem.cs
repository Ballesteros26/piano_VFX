using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000051 RID: 81
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	public static class VirtualFileSystem
	{
		// Token: 0x060000D3 RID: 211
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool GetLocalFileSystemName(string vfsFileName, out string localFileName, out ulong localFileOffset, out ulong localFileSize);
	}
}
