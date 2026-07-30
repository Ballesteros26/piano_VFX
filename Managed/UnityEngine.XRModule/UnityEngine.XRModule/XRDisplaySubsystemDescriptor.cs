using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000021 RID: 33
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	[UsedByNativeCode]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010F RID: 271
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyVr
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000110 RID: 272
		[NativeConditional("ENABLE_XR")]
		public extern bool enableBackBufferMSAA
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000111 RID: 273
		[NativeMethod("TryGetAvailableMirrorModeCount")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern int GetAvailableMirrorBlitModeCount();

		// Token: 0x06000112 RID: 274
		[NativeMethod("TryGetMirrorModeByIndex")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode);
	}
}
