using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003CB RID: 971
	[NativeHeader("Runtime/Export/Director/TexturePlayableGraphExtensions.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("TexturePlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class TexturePlayableGraphExtensions
	{
		// Token: 0x060021BF RID: 8639
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool InternalCreateTextureOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);
	}
}
