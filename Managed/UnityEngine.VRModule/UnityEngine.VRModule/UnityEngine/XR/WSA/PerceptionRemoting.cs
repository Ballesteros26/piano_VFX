using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/VR/HoloLens/PerceptionRemoting.h")]
	[NativeConditional("ENABLE_HOLOLENS_MODULE")]
	internal class PerceptionRemoting
	{
		// Token: 0x060000A5 RID: 165
		[MethodImpl(4096)]
		internal static extern void SetRemoteDeviceVersion(RemoteDeviceVersion remoteDeviceVersion);

		// Token: 0x060000A6 RID: 166
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Connect(string clientName);

		// Token: 0x060000A7 RID: 167
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Disconnect();

		// Token: 0x060000A8 RID: 168
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "HolographicEmulation::None")]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern HolographicStreamerConnectionFailureReason CheckForDisconnect();

		// Token: 0x060000A9 RID: 169
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "HolographicEmulation::Disconnected")]
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern HolographicStreamerConnectionState GetConnectionState();

		// Token: 0x060000AA RID: 170
		[MethodImpl(4096)]
		internal static extern void SetEnableAudio(bool enable);

		// Token: 0x060000AB RID: 171
		[MethodImpl(4096)]
		internal static extern void SetEnableVideo(bool enable);

		// Token: 0x060000AC RID: 172
		[MethodImpl(4096)]
		internal static extern void SetVideoEncodingParameters(int maxBitRate);
	}
}
