using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.XR.WSA;

namespace UnityEngineInternal.XR.WSA
{
	// Token: 0x02000002 RID: 2
	[NativeConditional("ENABLE_HOLOLENS_MODULE")]
	[NativeHeader("Modules/VR/HoloLens/PerceptionRemoting.h")]
	public class RemoteSpeechAccess
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(4096)]
		public static extern void EnableRemoteSpeech(RemoteDeviceVersion remoteDeviceVersion);

		// Token: 0x06000002 RID: 2
		[MethodImpl(4096)]
		public static extern void DisableRemoteSpeech();
	}
}
