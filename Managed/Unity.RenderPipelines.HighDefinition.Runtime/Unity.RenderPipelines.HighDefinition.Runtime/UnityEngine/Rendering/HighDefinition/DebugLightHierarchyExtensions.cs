using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000030 RID: 48
	internal static class DebugLightHierarchyExtensions
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000099EC File Offset: 0x00007BEC
		public static bool IsEnabledFor(this DebugLightFilterMode mode, GPULightType gpuLightType, SpotLightShape spotLightShape)
		{
			switch (gpuLightType)
			{
			case GPULightType.Directional:
				return (mode & DebugLightFilterMode.DirectDirectional) > DebugLightFilterMode.None;
			case GPULightType.Point:
				return (mode & DebugLightFilterMode.DirectPunctual) > DebugLightFilterMode.None;
			case GPULightType.Spot:
			case GPULightType.ProjectorPyramid:
			case GPULightType.ProjectorBox:
				switch (spotLightShape)
				{
				case SpotLightShape.Cone:
					return (mode & DebugLightFilterMode.DirectSpotCone) > DebugLightFilterMode.None;
				case SpotLightShape.Pyramid:
					return (mode & DebugLightFilterMode.DirectSpotPyramid) > DebugLightFilterMode.None;
				case SpotLightShape.Box:
					return (mode & DebugLightFilterMode.DirectSpotBox) > DebugLightFilterMode.None;
				default:
					throw new ArgumentOutOfRangeException("spotLightShape");
				}
				break;
			case GPULightType.Tube:
				return (mode & DebugLightFilterMode.DirectTube) > DebugLightFilterMode.None;
			case GPULightType.Rectangle:
				return (mode & DebugLightFilterMode.DirectRectangle) > DebugLightFilterMode.None;
			default:
				throw new ArgumentOutOfRangeException("gpuLightType");
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009A7A File Offset: 0x00007C7A
		public static bool IsEnabledFor(this DebugLightFilterMode mode, ProbeSettings.ProbeType probeType)
		{
			if (probeType == ProbeSettings.ProbeType.ReflectionProbe)
			{
				return (mode & DebugLightFilterMode.IndirectReflectionProbe) > DebugLightFilterMode.None;
			}
			if (probeType == ProbeSettings.ProbeType.PlanarProbe)
			{
				return (mode & DebugLightFilterMode.IndirectPlanarProbe) > DebugLightFilterMode.None;
			}
			throw new ArgumentOutOfRangeException("probeType");
		}
	}
}
