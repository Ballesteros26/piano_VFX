using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/VR/HoloLens/HolographicEmulation/HolographicEmulationManager.h")]
	[StaticAccessor("HolographicEmulation::HolographicEmulationManager::Get()", StaticAccessorType.Dot)]
	[NativeConditional("ENABLE_HOLOLENS_MODULE")]
	internal class HolographicAutomation
	{
		// Token: 0x06000052 RID: 82
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void Initialize();

		// Token: 0x06000053 RID: 83
		[MethodImpl(4096)]
		internal static extern void Shutdown();

		// Token: 0x06000054 RID: 84
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void LoadRoom(string id);

		// Token: 0x06000055 RID: 85
		[MethodImpl(4096)]
		internal static extern void SetEmulationMode(EmulationMode mode);

		// Token: 0x06000056 RID: 86
		[MethodImpl(4096)]
		internal static extern void SetPlaymodeInputType(PlaymodeInputType inputType);

		// Token: 0x06000057 RID: 87
		[NativeName("ResetEmulationState")]
		[MethodImpl(4096)]
		internal static extern void Reset();

		// Token: 0x06000058 RID: 88
		[MethodImpl(4096)]
		internal static extern void PerformGesture(Handedness hand, SimulatedGesture gesture);

		// Token: 0x06000059 RID: 89
		[MethodImpl(4096)]
		internal static extern void PerformButtonPress(Handedness hand, SimulatedControllerPress buttonPress);

		// Token: 0x0600005A RID: 90 RVA: 0x00002270 File Offset: 0x00000470
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "Vector3f::zero")]
		internal static Vector3 GetBodyPosition()
		{
			Vector3 vector;
			HolographicAutomation.GetBodyPosition_Injected(out vector);
			return vector;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002285 File Offset: 0x00000485
		internal static void SetBodyPosition(Vector3 position)
		{
			HolographicAutomation.SetBodyPosition_Injected(ref position);
		}

		// Token: 0x0600005C RID: 92
		[MethodImpl(4096)]
		internal static extern float GetBodyRotation();

		// Token: 0x0600005D RID: 93
		[MethodImpl(4096)]
		internal static extern void SetBodyRotation(float degrees);

		// Token: 0x0600005E RID: 94
		[MethodImpl(4096)]
		internal static extern float GetBodyHeight();

		// Token: 0x0600005F RID: 95
		[MethodImpl(4096)]
		internal static extern void SetBodyHeight(float degrees);

		// Token: 0x06000060 RID: 96 RVA: 0x00002290 File Offset: 0x00000490
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "Vector3f::zero")]
		internal static Vector3 GetHeadRotation()
		{
			Vector3 vector;
			HolographicAutomation.GetHeadRotation_Injected(out vector);
			return vector;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000022A5 File Offset: 0x000004A5
		internal static void SetHeadRotation(Vector3 degrees)
		{
			HolographicAutomation.SetHeadRotation_Injected(ref degrees);
		}

		// Token: 0x06000062 RID: 98
		[MethodImpl(4096)]
		internal static extern float GetHeadDiameter();

		// Token: 0x06000063 RID: 99
		[MethodImpl(4096)]
		internal static extern void SetHeadDiameter(float degrees);

		// Token: 0x06000064 RID: 100 RVA: 0x000022B0 File Offset: 0x000004B0
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "Vector3f::zero")]
		internal static Vector3 GetHandPosition(Handedness hand)
		{
			Vector3 vector;
			HolographicAutomation.GetHandPosition_Injected(hand, out vector);
			return vector;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000022C6 File Offset: 0x000004C6
		internal static void SetHandPosition(Handedness hand, Vector3 position)
		{
			HolographicAutomation.SetHandPosition_Injected(hand, ref position);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000022D0 File Offset: 0x000004D0
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "Quaternionf::identity()")]
		internal static Quaternion GetHandOrientation(Handedness hand)
		{
			Quaternion quaternion;
			HolographicAutomation.GetHandOrientation_Injected(hand, out quaternion);
			return quaternion;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000022E6 File Offset: 0x000004E6
		internal static bool TrySetHandOrientation(Handedness hand, Quaternion orientation)
		{
			return HolographicAutomation.TrySetHandOrientation_Injected(hand, ref orientation);
		}

		// Token: 0x06000068 RID: 104
		[MethodImpl(4096)]
		internal static extern bool GetHandActivated(Handedness hand);

		// Token: 0x06000069 RID: 105
		[MethodImpl(4096)]
		internal static extern void SetHandActivated(Handedness hand, bool activated);

		// Token: 0x0600006A RID: 106
		[MethodImpl(4096)]
		internal static extern bool GetHandVisible(Handedness hand);

		// Token: 0x0600006B RID: 107
		[MethodImpl(4096)]
		internal static extern void EnsureHandVisible(Handedness hand);

		// Token: 0x0600006C RID: 108 RVA: 0x000022F0 File Offset: 0x000004F0
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "Vector3f::zero")]
		internal static Vector3 GetControllerPosition(Handedness hand)
		{
			Vector3 vector;
			HolographicAutomation.GetControllerPosition_Injected(hand, out vector);
			return vector;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002306 File Offset: 0x00000506
		internal static bool TrySetControllerPosition(Handedness hand, Vector3 position)
		{
			return HolographicAutomation.TrySetControllerPosition_Injected(hand, ref position);
		}

		// Token: 0x0600006E RID: 110
		[MethodImpl(4096)]
		internal static extern bool GetControllerActivated(Handedness hand);

		// Token: 0x0600006F RID: 111
		[MethodImpl(4096)]
		internal static extern bool TrySetControllerActivated(Handedness hand, bool activated);

		// Token: 0x06000070 RID: 112
		[MethodImpl(4096)]
		internal static extern bool GetControllerVisible(Handedness hand);

		// Token: 0x06000071 RID: 113
		[MethodImpl(4096)]
		internal static extern bool TryEnsureControllerVisible(Handedness hand);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002310 File Offset: 0x00000510
		public static SimulatedBody simulatedBody
		{
			get
			{
				return HolographicAutomation.s_Body;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002328 File Offset: 0x00000528
		public static SimulatedHead simulatedHead
		{
			get
			{
				return HolographicAutomation.s_Head;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002340 File Offset: 0x00000540
		public static SimulatedHand simulatedLeftHand
		{
			get
			{
				return HolographicAutomation.s_LeftHand;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002358 File Offset: 0x00000558
		public static SimulatedHand simulatedRightHand
		{
			get
			{
				return HolographicAutomation.s_RightHand;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002370 File Offset: 0x00000570
		public static SimulatedSpatialController simulatedLeftController
		{
			get
			{
				return HolographicAutomation.s_LeftController;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002388 File Offset: 0x00000588
		public static SimulatedSpatialController simulatedRightController
		{
			get
			{
				return HolographicAutomation.s_RightController;
			}
		}

		// Token: 0x0600007A RID: 122
		[MethodImpl(4096)]
		private static extern void GetBodyPosition_Injected(out Vector3 ret);

		// Token: 0x0600007B RID: 123
		[MethodImpl(4096)]
		private static extern void SetBodyPosition_Injected(ref Vector3 position);

		// Token: 0x0600007C RID: 124
		[MethodImpl(4096)]
		private static extern void GetHeadRotation_Injected(out Vector3 ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(4096)]
		private static extern void SetHeadRotation_Injected(ref Vector3 degrees);

		// Token: 0x0600007E RID: 126
		[MethodImpl(4096)]
		private static extern void GetHandPosition_Injected(Handedness hand, out Vector3 ret);

		// Token: 0x0600007F RID: 127
		[MethodImpl(4096)]
		private static extern void SetHandPosition_Injected(Handedness hand, ref Vector3 position);

		// Token: 0x06000080 RID: 128
		[MethodImpl(4096)]
		private static extern void GetHandOrientation_Injected(Handedness hand, out Quaternion ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(4096)]
		private static extern bool TrySetHandOrientation_Injected(Handedness hand, ref Quaternion orientation);

		// Token: 0x06000082 RID: 130
		[MethodImpl(4096)]
		private static extern void GetControllerPosition_Injected(Handedness hand, out Vector3 ret);

		// Token: 0x06000083 RID: 131
		[MethodImpl(4096)]
		private static extern bool TrySetControllerPosition_Injected(Handedness hand, ref Vector3 position);

		// Token: 0x04000030 RID: 48
		private static SimulatedBody s_Body = new SimulatedBody();

		// Token: 0x04000031 RID: 49
		private static SimulatedHead s_Head = new SimulatedHead();

		// Token: 0x04000032 RID: 50
		private static SimulatedHand s_LeftHand = new SimulatedHand(Handedness.Left);

		// Token: 0x04000033 RID: 51
		private static SimulatedHand s_RightHand = new SimulatedHand(Handedness.Right);

		// Token: 0x04000034 RID: 52
		private static SimulatedSpatialController s_LeftController = new SimulatedSpatialController(Handedness.Left);

		// Token: 0x04000035 RID: 53
		private static SimulatedSpatialController s_RightController = new SimulatedSpatialController(Handedness.Right);
	}
}
