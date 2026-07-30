using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000023 RID: 35
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h")]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		// Token: 0x06000114 RID: 276
		[MethodImpl(4096)]
		internal extern uint GetIndex();

		// Token: 0x06000115 RID: 277
		[MethodImpl(4096)]
		public extern bool TryRecenter();

		// Token: 0x06000116 RID: 278 RVA: 0x00004348 File Offset: 0x00002548
		public bool TryGetInputDevices(List<InputDevice> devices)
		{
			bool flag = devices == null;
			if (flag)
			{
				throw new ArgumentNullException("devices");
			}
			devices.Clear();
			bool flag2 = this.m_DeviceIdsCache == null;
			if (flag2)
			{
				this.m_DeviceIdsCache = new List<ulong>();
			}
			this.m_DeviceIdsCache.Clear();
			this.TryGetDeviceIds_AsList(this.m_DeviceIdsCache);
			for (int i = 0; i < this.m_DeviceIdsCache.Count; i++)
			{
				devices.Add(new InputDevice(this.m_DeviceIdsCache[i]));
			}
			return true;
		}

		// Token: 0x06000117 RID: 279
		[MethodImpl(4096)]
		public extern bool TrySetTrackingOriginMode(TrackingOriginModeFlags origin);

		// Token: 0x06000118 RID: 280
		[MethodImpl(4096)]
		public extern TrackingOriginModeFlags GetTrackingOriginMode();

		// Token: 0x06000119 RID: 281
		[MethodImpl(4096)]
		public extern TrackingOriginModeFlags GetSupportedTrackingOriginModes();

		// Token: 0x0600011A RID: 282 RVA: 0x000043DC File Offset: 0x000025DC
		public bool TryGetBoundaryPoints(List<Vector3> boundaryPoints)
		{
			bool flag = boundaryPoints == null;
			if (flag)
			{
				throw new ArgumentNullException("boundaryPoints");
			}
			return this.TryGetBoundaryPoints_AsList(boundaryPoints);
		}

		// Token: 0x0600011B RID: 283
		[MethodImpl(4096)]
		private extern bool TryGetBoundaryPoints_AsList(List<Vector3> boundaryPoints);

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600011C RID: 284 RVA: 0x00004408 File Offset: 0x00002608
		// (remove) Token: 0x0600011D RID: 285 RVA: 0x00004440 File Offset: 0x00002640
		[field: DebuggerBrowsable(0)]
		public event Action<XRInputSubsystem> trackingOriginUpdated;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600011E RID: 286 RVA: 0x00004478 File Offset: 0x00002678
		// (remove) Token: 0x0600011F RID: 287 RVA: 0x000044B0 File Offset: 0x000026B0
		[field: DebuggerBrowsable(0)]
		public event Action<XRInputSubsystem> boundaryChanged;

		// Token: 0x06000120 RID: 288 RVA: 0x000044E8 File Offset: 0x000026E8
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystem = Internal_SubsystemInstances.Internal_GetInstanceByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystem as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.trackingOriginUpdated != null;
			if (flag)
			{
				xrinputSubsystem.trackingOriginUpdated.Invoke(xrinputSubsystem);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004524 File Offset: 0x00002724
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystem = Internal_SubsystemInstances.Internal_GetInstanceByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystem as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.boundaryChanged != null;
			if (flag)
			{
				xrinputSubsystem.boundaryChanged.Invoke(xrinputSubsystem);
			}
		}

		// Token: 0x06000122 RID: 290
		[MethodImpl(4096)]
		internal extern void TryGetDeviceIds_AsList(List<ulong> deviceIds);

		// Token: 0x040000DC RID: 220
		private List<ulong> m_DeviceIdsCache;
	}
}
