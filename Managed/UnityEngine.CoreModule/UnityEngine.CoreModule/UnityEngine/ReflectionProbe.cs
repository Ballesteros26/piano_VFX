using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B9 RID: 185
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	public sealed class ReflectionProbe : Behaviour
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000416 RID: 1046
		// (set) Token: 0x06000417 RID: 1047
		[Obsolete("type property has been deprecated. Starting with Unity 5.4, the only supported reflection probe type is Cube.", true)]
		[EditorBrowsable(1)]
		[NativeName("ProbeType")]
		public extern ReflectionProbeType type
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00006458 File Offset: 0x00004658
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000646E File Offset: 0x0000466E
		[NativeName("BoxSize")]
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00006478 File Offset: 0x00004678
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000648E File Offset: 0x0000468E
		[NativeName("BoxOffset")]
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600041C RID: 1052
		// (set) Token: 0x0600041D RID: 1053
		[NativeName("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600041E RID: 1054
		// (set) Token: 0x0600041F RID: 1055
		[NativeName("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000420 RID: 1056
		// (set) Token: 0x06000421 RID: 1057
		[NativeName("IntensityMultiplier")]
		public extern float intensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00006498 File Offset: 0x00004698
		[NativeName("GlobalAABB")]
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000423 RID: 1059
		// (set) Token: 0x06000424 RID: 1060
		[NativeName("HDR")]
		public extern bool hdr
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000425 RID: 1061
		// (set) Token: 0x06000426 RID: 1062
		public extern float shadowDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000427 RID: 1063
		// (set) Token: 0x06000428 RID: 1064
		public extern int resolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000429 RID: 1065
		// (set) Token: 0x0600042A RID: 1066
		public extern int cullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042B RID: 1067
		// (set) Token: 0x0600042C RID: 1068
		public extern ReflectionProbeClearFlags clearFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000064B0 File Offset: 0x000046B0
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000064C6 File Offset: 0x000046C6
		public Color backgroundColor
		{
			get
			{
				Color color;
				this.get_backgroundColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600042F RID: 1071
		// (set) Token: 0x06000430 RID: 1072
		public extern float blendDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000431 RID: 1073
		// (set) Token: 0x06000432 RID: 1074
		public extern bool boxProjection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000433 RID: 1075
		// (set) Token: 0x06000434 RID: 1076
		public extern ReflectionProbeMode mode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000435 RID: 1077
		// (set) Token: 0x06000436 RID: 1078
		public extern int importance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000437 RID: 1079
		// (set) Token: 0x06000438 RID: 1080
		public extern ReflectionProbeRefreshMode refreshMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000439 RID: 1081
		// (set) Token: 0x0600043A RID: 1082
		public extern ReflectionProbeTimeSlicingMode timeSlicingMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600043B RID: 1083
		// (set) Token: 0x0600043C RID: 1084
		public extern Texture bakedTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600043D RID: 1085
		// (set) Token: 0x0600043E RID: 1086
		public extern Texture customBakedTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600043F RID: 1087
		// (set) Token: 0x06000440 RID: 1088
		public extern RenderTexture realtimeTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000441 RID: 1089
		public extern Texture texture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000064D0 File Offset: 0x000046D0
		public Vector4 textureHDRDecodeValues
		{
			[NativeName("CalculateHDRDecodeValues")]
			get
			{
				Vector4 vector;
				this.get_textureHDRDecodeValues_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x06000443 RID: 1091
		[MethodImpl(4096)]
		public extern void Reset();

		// Token: 0x06000444 RID: 1092 RVA: 0x000064E8 File Offset: 0x000046E8
		public int RenderProbe()
		{
			return this.RenderProbe(null);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00006504 File Offset: 0x00004704
		public int RenderProbe([DefaultValue("null")] RenderTexture targetTexture)
		{
			return this.ScheduleRender(this.timeSlicingMode, targetTexture);
		}

		// Token: 0x06000446 RID: 1094
		[MethodImpl(4096)]
		public extern bool IsFinishedRendering(int renderId);

		// Token: 0x06000447 RID: 1095
		[MethodImpl(4096)]
		private extern int ScheduleRender(ReflectionProbeTimeSlicingMode timeSlicingMode, RenderTexture targetTexture);

		// Token: 0x06000448 RID: 1096
		[NativeHeader("Runtime/Camera/CubemapGPUUtility.h")]
		[FreeFunction("CubemapGPUBlend")]
		[MethodImpl(4096)]
		public static extern bool BlendCubemap(Texture src, Texture dst, float blend, RenderTexture target);

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000449 RID: 1097
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int minBakedCubemapResolution
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600044A RID: 1098
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int maxBakedCubemapResolution
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00006524 File Offset: 0x00004724
		[StaticAccessor("GetReflectionProbes()")]
		public static Vector4 defaultTextureHDRDecodeValues
		{
			get
			{
				Vector4 vector;
				ReflectionProbe.get_defaultTextureHDRDecodeValues_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600044C RID: 1100
		[StaticAccessor("GetReflectionProbes()")]
		public static extern Texture defaultTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600044D RID: 1101 RVA: 0x0000653C File Offset: 0x0000473C
		// (remove) Token: 0x0600044E RID: 1102 RVA: 0x00006570 File Offset: 0x00004770
		[field: DebuggerBrowsable(0)]
		public static event Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600044F RID: 1103 RVA: 0x000065A4 File Offset: 0x000047A4
		// (remove) Token: 0x06000450 RID: 1104 RVA: 0x000065D8 File Offset: 0x000047D8
		[field: DebuggerBrowsable(0)]
		public static event Action<Cubemap> defaultReflectionSet;

		// Token: 0x06000451 RID: 1105 RVA: 0x0000660C File Offset: 0x0000480C
		[RequiredByNativeCode]
		private static void CallReflectionProbeEvent(ReflectionProbe probe, ReflectionProbe.ReflectionProbeEvent probeEvent)
		{
			Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
			bool flag = action != null;
			if (flag)
			{
				action.Invoke(probe, probeEvent);
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00006634 File Offset: 0x00004834
		[RequiredByNativeCode]
		private static void CallSetDefaultReflection(Cubemap defaultReflectionCubemap)
		{
			Action<Cubemap> action = ReflectionProbe.defaultReflectionSet;
			bool flag = action != null;
			if (flag)
			{
				action.Invoke(defaultReflectionCubemap);
			}
		}

		// Token: 0x06000454 RID: 1108
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000455 RID: 1109
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector3 value);

		// Token: 0x06000456 RID: 1110
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000457 RID: 1111
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000458 RID: 1112
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000459 RID: 1113
		[MethodImpl(4096)]
		private extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x0600045A RID: 1114
		[MethodImpl(4096)]
		private extern void set_backgroundColor_Injected(ref Color value);

		// Token: 0x0600045B RID: 1115
		[MethodImpl(4096)]
		private extern void get_textureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x0600045C RID: 1116
		[MethodImpl(4096)]
		private static extern void get_defaultTextureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x020000BA RID: 186
		public enum ReflectionProbeEvent
		{
			// Token: 0x04000221 RID: 545
			ReflectionProbeAdded,
			// Token: 0x04000222 RID: 546
			ReflectionProbeRemoved
		}
	}
}
