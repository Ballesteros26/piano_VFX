using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Editor", StaticAccessorType.DoubleColon)]
	[NativeConditional("UNITY_EDITOR")]
	public static class EditorHelpers
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6
		internal static extern int tileSize
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000007 RID: 7
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern bool ValidateTextureStack([NotNull] Texture[] textures, out string errorMessage);

		// Token: 0x06000008 RID: 8
		[NativeConditional("UNITY_EDITOR", "{}")]
		[MethodImpl(4096)]
		public static extern GraphicsFormat[] QuerySupportedFormats();
	}
}
