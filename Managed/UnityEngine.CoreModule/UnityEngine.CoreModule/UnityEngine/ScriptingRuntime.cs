using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B4 RID: 436
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Export/Scripting/ScriptingRuntime.h")]
	internal class ScriptingRuntime
	{
		// Token: 0x060013E4 RID: 5092
		[MethodImpl(4096)]
		public static extern string[] GetAllUserAssemblies();
	}
}
