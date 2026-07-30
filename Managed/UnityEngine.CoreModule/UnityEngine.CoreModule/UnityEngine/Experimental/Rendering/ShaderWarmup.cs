using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003E0 RID: 992
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public static class ShaderWarmup
	{
		// Token: 0x0600222F RID: 8751 RVA: 0x0003996D File Offset: 0x00037B6D
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShader")]
		public static void WarmupShader(Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShader_Injected(shader, ref setup);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00039977 File Offset: 0x00037B77
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShaderFromCollection")]
		public static void WarmupShaderFromCollection(ShaderVariantCollection collection, Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShaderFromCollection_Injected(collection, shader, ref setup);
		}

		// Token: 0x06002231 RID: 8753
		[MethodImpl(4096)]
		private static extern void WarmupShader_Injected(Shader shader, ref ShaderWarmupSetup setup);

		// Token: 0x06002232 RID: 8754
		[MethodImpl(4096)]
		private static extern void WarmupShaderFromCollection_Injected(ShaderVariantCollection collection, Shader shader, ref ShaderWarmupSetup setup);
	}
}
