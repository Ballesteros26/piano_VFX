using System;
using System.ComponentModel;

namespace UnityEngine.VR
{
	// Token: 0x02000029 RID: 41
	[EditorBrowsable(1)]
	[Obsolete("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead (UnityUpgradable) -> UnityEngine.XR.XRNodeState", true)]
	public struct VRNodeState
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000027D4 File Offset: 0x000009D4
		public ulong uniqueID
		{
			get
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000027D4 File Offset: 0x000009D4
		public VRNode nodeType
		{
			get
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool tracked
		{
			get
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x17000048 RID: 72
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000027D4 File Offset: 0x000009D4
		public Vector3 position
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x17000049 RID: 73
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000027D4 File Offset: 0x000009D4
		public Quaternion rotation
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000027D4 File Offset: 0x000009D4
		public Vector3 velocity
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x1700004B RID: 75
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000027D4 File Offset: 0x000009D4
		public Vector3 angularVelocity
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x1700004C RID: 76
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000027D4 File Offset: 0x000009D4
		public Vector3 acceleration
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x1700004D RID: 77
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000027D4 File Offset: 0x000009D4
		public Vector3 angularAcceleration
		{
			set
			{
				throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000027E1 File Offset: 0x000009E1
		public bool TryGetPosition(out Vector3 position)
		{
			position = default(Vector3);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000027F5 File Offset: 0x000009F5
		public bool TryGetRotation(out Quaternion rotation)
		{
			rotation = default(Quaternion);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000027E1 File Offset: 0x000009E1
		public bool TryGetVelocity(out Vector3 velocity)
		{
			velocity = default(Vector3);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000027E1 File Offset: 0x000009E1
		public bool TryGetAngularVelocity(out Vector3 angularVelocity)
		{
			angularVelocity = default(Vector3);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000027E1 File Offset: 0x000009E1
		public bool TryGetAcceleration(out Vector3 acceleration)
		{
			acceleration = default(Vector3);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000027E1 File Offset: 0x000009E1
		public bool TryGetAngularAcceleration(out Vector3 angularAcceleration)
		{
			angularAcceleration = default(Vector3);
			throw new NotSupportedException("VRNodeState has been moved and renamed.  Use UnityEngine.XR.XRNodeState instead.");
		}
	}
}
