using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/VFX/Public/VFXManager.h")]
	[RequiredByNativeCode]
	[StaticAccessor("GetVFXManager()", StaticAccessorType.Dot)]
	public static class VFXManager
	{
		// Token: 0x06000066 RID: 102
		[MethodImpl(4096)]
		public static extern VisualEffect[] GetComponents();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		public static extern float fixedTimeStep
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		public static extern float maxDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006B RID: 107
		internal static extern string renderPipeSettingsPath
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000026FE File Offset: 0x000008FE
		public static void ProcessCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam);
			VFXManager.ProcessCameraCommand(cam, null);
		}

		// Token: 0x0600006D RID: 109
		[MethodImpl(4096)]
		public static extern void PrepareCamera(Camera cam);

		// Token: 0x0600006E RID: 110
		[MethodImpl(4096)]
		public static extern void ProcessCameraCommand(Camera cam, CommandBuffer cmd);

		// Token: 0x0600006F RID: 111
		[MethodImpl(4096)]
		public static extern VFXCameraBufferTypes IsCameraBufferNeeded(Camera cam);

		// Token: 0x06000070 RID: 112
		[MethodImpl(4096)]
		public static extern void SetCameraBuffer(Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height);
	}
}
