using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x0200001E RID: 30
	[StaticAccessor("HolographicSettings::GetInstance()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/VR/HoloLens/HolographicSettings.h")]
	public class HolographicSettings
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000026CB File Offset: 0x000008CB
		public static void SetFocusPointForFrame(Vector3 position)
		{
			HolographicSettings.InternalSetFocusPointForFrameP(position);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000026D5 File Offset: 0x000008D5
		public static void SetFocusPointForFrame(Vector3 position, Vector3 normal)
		{
			HolographicSettings.InternalSetFocusPointForFramePN(position, normal);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000026E0 File Offset: 0x000008E0
		public static void SetFocusPointForFrame(Vector3 position, Vector3 normal, Vector3 velocity)
		{
			HolographicSettings.InternalSetFocusPointForFramePNV(position, normal, velocity);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000026EC File Offset: 0x000008EC
		[NativeName("SetFocusPointForFrame")]
		[NativeConditional("ENABLE_HOLOLENS_MODULE")]
		private static void InternalSetFocusPointForFrameP(Vector3 position)
		{
			HolographicSettings.InternalSetFocusPointForFrameP_Injected(ref position);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000026F5 File Offset: 0x000008F5
		[NativeName("SetFocusPointForFrame")]
		[NativeConditional("ENABLE_HOLOLENS_MODULE")]
		private static void InternalSetFocusPointForFramePN(Vector3 position, Vector3 normal)
		{
			HolographicSettings.InternalSetFocusPointForFramePN_Injected(ref position, ref normal);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002700 File Offset: 0x00000900
		[NativeName("SetFocusPointForFrame")]
		[NativeConditional("ENABLE_HOLOLENS_MODULE")]
		private static void InternalSetFocusPointForFramePNV(Vector3 position, Vector3 normal, Vector3 velocity)
		{
			HolographicSettings.InternalSetFocusPointForFramePNV_Injected(ref position, ref normal, ref velocity);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002710 File Offset: 0x00000910
		public static bool IsDisplayOpaque
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		[NativeConditional("ENABLE_HOLOLENS_MODULE")]
		public static extern bool IsContentProtectionEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002724 File Offset: 0x00000924
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002737 File Offset: 0x00000937
		public static HolographicSettings.HolographicReprojectionMode ReprojectionMode
		{
			get
			{
				return HolographicSettings.HolographicReprojectionMode.Disabled;
			}
			set
			{
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002737 File Offset: 0x00000937
		[Obsolete("Support for toggling latent frame presentation has been removed", true)]
		public static void ActivateLatentFramePresentation(bool activated)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000273C File Offset: 0x0000093C
		[Obsolete("Support for toggling latent frame presentation has been removed, and IsLatentFramePresentation will always return true", false)]
		public static bool IsLatentFramePresentation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000BC RID: 188
		[MethodImpl(4096)]
		private static extern void InternalSetFocusPointForFrameP_Injected(ref Vector3 position);

		// Token: 0x060000BD RID: 189
		[MethodImpl(4096)]
		private static extern void InternalSetFocusPointForFramePN_Injected(ref Vector3 position, ref Vector3 normal);

		// Token: 0x060000BE RID: 190
		[MethodImpl(4096)]
		private static extern void InternalSetFocusPointForFramePNV_Injected(ref Vector3 position, ref Vector3 normal, ref Vector3 velocity);

		// Token: 0x0200001F RID: 31
		public enum HolographicReprojectionMode
		{
			// Token: 0x0400004E RID: 78
			PositionAndOrientation,
			// Token: 0x0400004F RID: 79
			OrientationOnly,
			// Token: 0x04000050 RID: 80
			Disabled
		}
	}
}
