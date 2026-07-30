using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Experimental.XR
{
	// Token: 0x02000004 RID: 4
	[NativeConditional("ENABLE_VR")]
	public static class Boundary
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000020A8 File Offset: 0x000002A8
		public static bool TryGetDimensions(out Vector3 dimensionsOut)
		{
			return Boundary.TryGetDimensions(out dimensionsOut, Boundary.Type.PlayArea);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020C4 File Offset: 0x000002C4
		public static bool TryGetDimensions(out Vector3 dimensionsOut, [DefaultValue("Type.PlayArea")] Boundary.Type boundaryType)
		{
			return Boundary.TryGetDimensionsInternal(out dimensionsOut, boundaryType);
		}

		// Token: 0x06000019 RID: 25
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("TryGetBoundaryDimensions")]
		[MethodImpl(4096)]
		private static extern bool TryGetDimensionsInternal(out Vector3 dimensionsOut, Boundary.Type boundaryType);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26
		// (set) Token: 0x0600001B RID: 27
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("BoundaryVisible")]
		public static extern bool visible
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28
		[NativeName("BoundaryConfigured")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool configured
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020E0 File Offset: 0x000002E0
		public static bool TryGetGeometry(List<Vector3> geometry)
		{
			return Boundary.TryGetGeometry(geometry, Boundary.Type.PlayArea);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020FC File Offset: 0x000002FC
		public static bool TryGetGeometry(List<Vector3> geometry, [DefaultValue("Type.PlayArea")] Boundary.Type boundaryType)
		{
			bool flag = geometry == null;
			if (flag)
			{
				throw new ArgumentNullException("geometry");
			}
			geometry.Clear();
			return Boundary.TryGetGeometryScriptingInternal(geometry, boundaryType);
		}

		// Token: 0x0600001F RID: 31
		[MethodImpl(4096)]
		private static extern bool TryGetGeometryScriptingInternal(List<Vector3> geometry, Boundary.Type boundaryType);

		// Token: 0x02000005 RID: 5
		public enum Type
		{
			// Token: 0x04000002 RID: 2
			PlayArea,
			// Token: 0x04000003 RID: 3
			TrackedArea
		}
	}
}
