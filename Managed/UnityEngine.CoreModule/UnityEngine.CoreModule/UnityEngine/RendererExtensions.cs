using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000EC RID: 236
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class RendererExtensions
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0000C64E File Offset: 0x0000A84E
		public static void UpdateGIMaterials(this Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer(renderer);
		}

		// Token: 0x0600082B RID: 2091
		[FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")]
		[MethodImpl(4096)]
		internal static extern void UpdateGIMaterialsForRenderer(Renderer renderer);
	}
}
