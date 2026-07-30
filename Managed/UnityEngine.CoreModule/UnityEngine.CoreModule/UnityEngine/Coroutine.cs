using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A3 RID: 419
	[NativeHeader("Runtime/Mono/Coroutine.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		private Coroutine()
		{
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		~Coroutine()
		{
			Coroutine.ReleaseCoroutine(this.m_Ptr);
		}

		// Token: 0x06001340 RID: 4928
		[FreeFunction("Coroutine::CleanupCoroutineGC", true)]
		[MethodImpl(4096)]
		private static extern void ReleaseCoroutine(IntPtr ptr);

		// Token: 0x04000647 RID: 1607
		internal IntPtr m_Ptr;
	}
}
