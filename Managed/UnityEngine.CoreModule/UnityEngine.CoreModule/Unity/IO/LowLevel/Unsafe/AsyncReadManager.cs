using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000050 RID: 80
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00002D24 File Offset: 0x00000F24
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount)
		{
			ReadHandle readHandle;
			AsyncReadManager.ReadInternal_Injected(filename, cmds, cmdCount, out readHandle);
			return readHandle;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002D3C File Offset: 0x00000F3C
		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount)
		{
			return AsyncReadManager.ReadInternal(filename, (void*)readCmds, readCmdCount);
		}

		// Token: 0x060000D2 RID: 210
		[MethodImpl(4096)]
		private unsafe static extern void ReadInternal_Injected(string filename, void* cmds, uint cmdCount, out ReadHandle ret);
	}
}
