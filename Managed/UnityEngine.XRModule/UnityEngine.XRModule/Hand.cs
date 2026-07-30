using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000013 RID: 19
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Hand : IEquatable<Hand>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000037C8 File Offset: 0x000019C8
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000037E0 File Offset: 0x000019E0
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037F8 File Offset: 0x000019F8
		public bool TryGetRootBone(out Bone boneOut)
		{
			return Hand.Hand_TryGetRootBone(this, out boneOut);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003816 File Offset: 0x00001A16
		private static bool Hand_TryGetRootBone(Hand hand, out Bone boneOut)
		{
			return Hand.Hand_TryGetRootBone_Injected(ref hand, out boneOut);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003820 File Offset: 0x00001A20
		public bool TryGetFingerBones(HandFinger finger, List<Bone> bonesOut)
		{
			bool flag = bonesOut == null;
			if (flag)
			{
				throw new ArgumentNullException("bonesOut");
			}
			return Hand.Hand_TryGetFingerBonesAsList(this, finger, bonesOut);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003852 File Offset: 0x00001A52
		private static bool Hand_TryGetFingerBonesAsList(Hand hand, HandFinger finger, [NotNull] List<Bone> bonesOut)
		{
			return Hand.Hand_TryGetFingerBonesAsList_Injected(ref hand, finger, bonesOut);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003860 File Offset: 0x00001A60
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Hand);
			return !flag && this.Equals((Hand)obj);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003894 File Offset: 0x00001A94
		public bool Equals(Hand other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000038C8 File Offset: 0x00001AC8
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000038FC File Offset: 0x00001AFC
		public static bool operator ==(Hand a, Hand b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003918 File Offset: 0x00001B18
		public static bool operator !=(Hand a, Hand b)
		{
			return !(a == b);
		}

		// Token: 0x06000089 RID: 137
		[MethodImpl(4096)]
		private static extern bool Hand_TryGetRootBone_Injected(ref Hand hand, out Bone boneOut);

		// Token: 0x0600008A RID: 138
		[MethodImpl(4096)]
		private static extern bool Hand_TryGetFingerBonesAsList_Injected(ref Hand hand, HandFinger finger, List<Bone> bonesOut);

		// Token: 0x040000A3 RID: 163
		private ulong m_DeviceId;

		// Token: 0x040000A4 RID: 164
		private uint m_FeatureIndex;
	}
}
