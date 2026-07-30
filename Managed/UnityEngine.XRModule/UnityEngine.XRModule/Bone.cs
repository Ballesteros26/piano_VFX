using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000016 RID: 22
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeConditional("ENABLE_VR")]
	[RequiredByNativeCode]
	public struct Bone : IEquatable<Bone>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003B44 File Offset: 0x00001D44
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003B5C File Offset: 0x00001D5C
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B74 File Offset: 0x00001D74
		public bool TryGetPosition(out Vector3 position)
		{
			return Bone.Bone_TryGetPosition(this, out position);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B92 File Offset: 0x00001D92
		private static bool Bone_TryGetPosition(Bone bone, out Vector3 position)
		{
			return Bone.Bone_TryGetPosition_Injected(ref bone, out position);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B9C File Offset: 0x00001D9C
		public bool TryGetRotation(out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation(this, out rotation);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003BBA File Offset: 0x00001DBA
		private static bool Bone_TryGetRotation(Bone bone, out Quaternion rotation)
		{
			return Bone.Bone_TryGetRotation_Injected(ref bone, out rotation);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public bool TryGetParentBone(out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone(this, out parentBone);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BE2 File Offset: 0x00001DE2
		private static bool Bone_TryGetParentBone(Bone bone, out Bone parentBone)
		{
			return Bone.Bone_TryGetParentBone_Injected(ref bone, out parentBone);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BEC File Offset: 0x00001DEC
		public bool TryGetChildBones(List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones(this, childBones);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C0A File Offset: 0x00001E0A
		private static bool Bone_TryGetChildBones(Bone bone, [NotNull] List<Bone> childBones)
		{
			return Bone.Bone_TryGetChildBones_Injected(ref bone, childBones);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C14 File Offset: 0x00001E14
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Bone);
			return !flag && this.Equals((Bone)obj);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C48 File Offset: 0x00001E48
		public bool Equals(Bone other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C7C File Offset: 0x00001E7C
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public static bool operator ==(Bone a, Bone b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static bool operator !=(Bone a, Bone b)
		{
			return !(a == b);
		}

		// Token: 0x060000B0 RID: 176
		[MethodImpl(4096)]
		private static extern bool Bone_TryGetPosition_Injected(ref Bone bone, out Vector3 position);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(4096)]
		private static extern bool Bone_TryGetRotation_Injected(ref Bone bone, out Quaternion rotation);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(4096)]
		private static extern bool Bone_TryGetParentBone_Injected(ref Bone bone, out Bone parentBone);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(4096)]
		private static extern bool Bone_TryGetChildBones_Injected(ref Bone bone, List<Bone> childBones);

		// Token: 0x040000AA RID: 170
		private ulong m_DeviceId;

		// Token: 0x040000AB RID: 171
		private uint m_FeatureIndex;
	}
}
