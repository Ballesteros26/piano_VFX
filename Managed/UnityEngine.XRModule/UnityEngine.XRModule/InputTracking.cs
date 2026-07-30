using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000002 RID: 2
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h")]
	[RequiredByNativeCode]
	[StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot)]
	public static class InputTracking
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> trackingAcquired;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> trackingLost;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> nodeAdded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		[field: DebuggerBrowsable(0)]
		public static event Action<XRNodeState> nodeRemoved;

		// Token: 0x06000009 RID: 9 RVA: 0x000021F0 File Offset: 0x000003F0
		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(InputTracking.TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			XRNodeState xrnodeState = default(XRNodeState);
			xrnodeState.uniqueID = (ulong)uniqueID;
			xrnodeState.nodeType = nodeType;
			xrnodeState.tracked = tracked;
			Action<XRNodeState> action;
			switch (eventType)
			{
			case InputTracking.TrackingStateEventType.NodeAdded:
				action = InputTracking.nodeAdded;
				break;
			case InputTracking.TrackingStateEventType.NodeRemoved:
				action = InputTracking.nodeRemoved;
				break;
			case InputTracking.TrackingStateEventType.TrackingAcquired:
				action = InputTracking.trackingAcquired;
				break;
			case InputTracking.TrackingStateEventType.TrackingLost:
				action = InputTracking.trackingLost;
				break;
			default:
				throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType);
			}
			bool flag = action != null;
			if (flag)
			{
				action.Invoke(xrnodeState);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002288 File Offset: 0x00000488
		[NativeConditional("ENABLE_VR", "Vector3f::zero")]
		[Obsolete("This API has been marked as obsolete in code, and is no longer in use. Please use InputTracking.GetNodeStates and look for the XRNodeState with the corresponding XRNode type instead.")]
		public static Vector3 GetLocalPosition(XRNode node)
		{
			Vector3 vector;
			InputTracking.GetLocalPosition_Injected(node, out vector);
			return vector;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A0 File Offset: 0x000004A0
		[NativeConditional("ENABLE_VR", "Quaternionf::identity()")]
		[Obsolete("This API has been marked as obsolete in code, and is no longer in use. Please use InputTracking.GetNodeStates and look for the XRNodeState with the corresponding XRNode type instead.")]
		public static Quaternion GetLocalRotation(XRNode node)
		{
			Quaternion quaternion;
			InputTracking.GetLocalRotation_Injected(node, out quaternion);
			return quaternion;
		}

		// Token: 0x0600000C RID: 12
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(4096)]
		public static extern void Recenter();

		// Token: 0x0600000D RID: 13
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(4096)]
		public static extern string GetNodeName(ulong uniqueId);

		// Token: 0x0600000E RID: 14 RVA: 0x000022B8 File Offset: 0x000004B8
		public static void GetNodeStates(List<XRNodeState> nodeStates)
		{
			bool flag = nodeStates == null;
			if (flag)
			{
				throw new ArgumentNullException("nodeStates");
			}
			nodeStates.Clear();
			InputTracking.GetNodeStates_Internal(nodeStates);
		}

		// Token: 0x0600000F RID: 15
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(4096)]
		private static extern void GetNodeStates_Internal([NotNull] List<XRNodeState> nodeStates);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		[NativeConditional("ENABLE_VR")]
		public static extern bool disablePositionalTracking
		{
			[NativeName("GetPositionalTrackingDisabled")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetPositionalTrackingDisabled")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000012 RID: 18
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		internal static extern ulong GetDeviceIdAtXRNode(XRNode node);

		// Token: 0x06000013 RID: 19
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		internal static extern void GetDeviceIdsAtXRNode_Internal(XRNode node, [NotNull] List<ulong> deviceIds);

		// Token: 0x06000014 RID: 20 RVA: 0x000022E7 File Offset: 0x000004E7
		// Note: this type is marked as 'beforefieldinit'.
		static InputTracking()
		{
			InputTracking.trackingAcquired = null;
			InputTracking.trackingLost = null;
			InputTracking.nodeAdded = null;
			InputTracking.nodeRemoved = null;
		}

		// Token: 0x06000015 RID: 21
		[MethodImpl(4096)]
		private static extern void GetLocalPosition_Injected(XRNode node, out Vector3 ret);

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern void GetLocalRotation_Injected(XRNode node, out Quaternion ret);

		// Token: 0x02000003 RID: 3
		private enum TrackingStateEventType
		{
			// Token: 0x04000006 RID: 6
			NodeAdded,
			// Token: 0x04000007 RID: 7
			NodeRemoved,
			// Token: 0x04000008 RID: 8
			TrackingAcquired,
			// Token: 0x04000009 RID: 9
			TrackingLost
		}
	}
}
