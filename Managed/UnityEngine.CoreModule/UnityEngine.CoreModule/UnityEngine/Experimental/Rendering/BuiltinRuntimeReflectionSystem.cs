using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003CD RID: 973
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x060021CA RID: 8650 RVA: 0x000395E0 File Offset: 0x000377E0
		public bool TickRealtimeProbes()
		{
			return BuiltinRuntimeReflectionSystem.BuiltinUpdate();
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x000395F7 File Offset: 0x000377F7
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00002EC3 File Offset: 0x000010C3
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x060021CD RID: 8653
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern bool BuiltinUpdate();

		// Token: 0x060021CE RID: 8654 RVA: 0x00039604 File Offset: 0x00037804
		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}
	}
}
