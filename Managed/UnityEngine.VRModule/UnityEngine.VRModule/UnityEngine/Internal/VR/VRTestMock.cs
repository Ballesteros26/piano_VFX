using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.XR;

namespace UnityEngine.Internal.VR
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/VR/Test/VRTestMock.bindings.h")]
	[StaticAccessor("VRTestMockBindings", StaticAccessorType.DoubleColon)]
	public static class VRTestMock
	{
		// Token: 0x06000004 RID: 4
		[MethodImpl(4096)]
		public static extern void Reset();

		// Token: 0x06000005 RID: 5
		[MethodImpl(4096)]
		public static extern void AddTrackedDevice(XRNode nodeType);

		// Token: 0x06000006 RID: 6 RVA: 0x00002059 File Offset: 0x00000259
		public static void UpdateTrackedDevice(XRNode nodeType, Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateTrackedDevice_Injected(nodeType, ref position, ref rotation);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002065 File Offset: 0x00000265
		public static void UpdateLeftEye(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateLeftEye_Injected(ref position, ref rotation);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002070 File Offset: 0x00000270
		public static void UpdateRightEye(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateRightEye_Injected(ref position, ref rotation);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000207B File Offset: 0x0000027B
		public static void UpdateCenterEye(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateCenterEye_Injected(ref position, ref rotation);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002086 File Offset: 0x00000286
		public static void UpdateHead(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateHead_Injected(ref position, ref rotation);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002091 File Offset: 0x00000291
		public static void UpdateLeftHand(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateLeftHand_Injected(ref position, ref rotation);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000209C File Offset: 0x0000029C
		public static void UpdateRightHand(Vector3 position, Quaternion rotation)
		{
			VRTestMock.UpdateRightHand_Injected(ref position, ref rotation);
		}

		// Token: 0x0600000D RID: 13
		[MethodImpl(4096)]
		public static extern void AddController(string controllerName);

		// Token: 0x0600000E RID: 14
		[MethodImpl(4096)]
		public static extern void UpdateControllerAxis(string controllerName, int axis, float value);

		// Token: 0x0600000F RID: 15
		[MethodImpl(4096)]
		public static extern void UpdateControllerButton(string controllerName, int button, bool pressed);

		// Token: 0x06000010 RID: 16
		[MethodImpl(4096)]
		private static extern void UpdateTrackedDevice_Injected(XRNode nodeType, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000011 RID: 17
		[MethodImpl(4096)]
		private static extern void UpdateLeftEye_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000012 RID: 18
		[MethodImpl(4096)]
		private static extern void UpdateRightEye_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000013 RID: 19
		[MethodImpl(4096)]
		private static extern void UpdateCenterEye_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000014 RID: 20
		[MethodImpl(4096)]
		private static extern void UpdateHead_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000015 RID: 21
		[MethodImpl(4096)]
		private static extern void UpdateLeftHand_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern void UpdateRightHand_Injected(ref Vector3 position, ref Quaternion rotation);
	}
}
