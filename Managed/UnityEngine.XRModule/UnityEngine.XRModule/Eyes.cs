using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Eyes : IEquatable<Eyes>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003934 File Offset: 0x00001B34
		internal ulong deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000394C File Offset: 0x00001B4C
		internal uint featureIndex
		{
			get
			{
				return this.m_FeatureIndex;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003964 File Offset: 0x00001B64
		public bool TryGetLeftEyePosition(out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition(this, EyeSide.Left, out position);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003984 File Offset: 0x00001B84
		public bool TryGetRightEyePosition(out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition(this, EyeSide.Right, out position);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039A4 File Offset: 0x00001BA4
		public bool TryGetLeftEyeRotation(out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation(this, EyeSide.Left, out rotation);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039C4 File Offset: 0x00001BC4
		public bool TryGetRightEyeRotation(out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation(this, EyeSide.Right, out rotation);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039E3 File Offset: 0x00001BE3
		private static bool Eyes_TryGetEyePosition(Eyes eyes, EyeSide chirality, out Vector3 position)
		{
			return Eyes.Eyes_TryGetEyePosition_Injected(ref eyes, chirality, out position);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039EE File Offset: 0x00001BEE
		private static bool Eyes_TryGetEyeRotation(Eyes eyes, EyeSide chirality, out Quaternion rotation)
		{
			return Eyes.Eyes_TryGetEyeRotation_Injected(ref eyes, chirality, out rotation);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000039FC File Offset: 0x00001BFC
		public bool TryGetFixationPoint(out Vector3 fixationPoint)
		{
			return Eyes.Eyes_TryGetFixationPoint(this, out fixationPoint);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A1A File Offset: 0x00001C1A
		private static bool Eyes_TryGetFixationPoint(Eyes eyes, out Vector3 fixationPoint)
		{
			return Eyes.Eyes_TryGetFixationPoint_Injected(ref eyes, out fixationPoint);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A24 File Offset: 0x00001C24
		public bool TryGetLeftEyeOpenAmount(out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount(this, EyeSide.Left, out openAmount);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A44 File Offset: 0x00001C44
		public bool TryGetRightEyeOpenAmount(out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount(this, EyeSide.Right, out openAmount);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A63 File Offset: 0x00001C63
		private static bool Eyes_TryGetEyeOpenAmount(Eyes eyes, EyeSide chirality, out float openAmount)
		{
			return Eyes.Eyes_TryGetEyeOpenAmount_Injected(ref eyes, chirality, out openAmount);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A70 File Offset: 0x00001C70
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Eyes);
			return !flag && this.Equals((Eyes)obj);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public bool Equals(Eyes other)
		{
			return this.deviceId == other.deviceId && this.featureIndex == other.featureIndex;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode() ^ (this.featureIndex.GetHashCode() << 1);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B0C File Offset: 0x00001D0C
		public static bool operator ==(Eyes a, Eyes b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003B28 File Offset: 0x00001D28
		public static bool operator !=(Eyes a, Eyes b)
		{
			return !(a == b);
		}

		// Token: 0x0600009D RID: 157
		[MethodImpl(4096)]
		private static extern bool Eyes_TryGetEyePosition_Injected(ref Eyes eyes, EyeSide chirality, out Vector3 position);

		// Token: 0x0600009E RID: 158
		[MethodImpl(4096)]
		private static extern bool Eyes_TryGetEyeRotation_Injected(ref Eyes eyes, EyeSide chirality, out Quaternion rotation);

		// Token: 0x0600009F RID: 159
		[MethodImpl(4096)]
		private static extern bool Eyes_TryGetFixationPoint_Injected(ref Eyes eyes, out Vector3 fixationPoint);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(4096)]
		private static extern bool Eyes_TryGetEyeOpenAmount_Injected(ref Eyes eyes, EyeSide chirality, out float openAmount);

		// Token: 0x040000A8 RID: 168
		private ulong m_DeviceId;

		// Token: 0x040000A9 RID: 169
		private uint m_FeatureIndex;
	}
}
