using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002790 File Offset: 0x00000990
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000027B4 File Offset: 0x000009B4
		internal InputFeatureType internalType
		{
			get
			{
				return this.m_InternalType;
			}
			set
			{
				this.m_InternalType = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027C0 File Offset: 0x000009C0
		public Type type
		{
			get
			{
				Type type;
				switch (this.m_InternalType)
				{
				case InputFeatureType.Custom:
					type = typeof(byte[]);
					break;
				case InputFeatureType.Binary:
					type = typeof(bool);
					break;
				case InputFeatureType.DiscreteStates:
					type = typeof(uint);
					break;
				case InputFeatureType.Axis1D:
					type = typeof(float);
					break;
				case InputFeatureType.Axis2D:
					type = typeof(Vector2);
					break;
				case InputFeatureType.Axis3D:
					type = typeof(Vector3);
					break;
				case InputFeatureType.Rotation:
					type = typeof(Quaternion);
					break;
				case InputFeatureType.Hand:
					type = typeof(Hand);
					break;
				case InputFeatureType.Bone:
					type = typeof(Bone);
					break;
				case InputFeatureType.Eyes:
					type = typeof(Eyes);
					break;
				default:
					throw new InvalidCastException("No valid managed type for unknown native type.");
				}
				return type;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002899 File Offset: 0x00000A99
		internal InputFeatureUsage(string name, InputFeatureType type)
		{
			this.m_Name = name;
			this.m_InternalType = type;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028AC File Offset: 0x00000AAC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage);
			return !flag && this.Equals((InputFeatureUsage)obj);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028E0 File Offset: 0x00000AE0
		public bool Equals(InputFeatureUsage other)
		{
			return this.name == other.name && this.internalType == other.internalType;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002918 File Offset: 0x00000B18
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ (this.internalType.GetHashCode() << 1);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000294C File Offset: 0x00000B4C
		public static bool operator ==(InputFeatureUsage a, InputFeatureUsage b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002968 File Offset: 0x00000B68
		public static bool operator !=(InputFeatureUsage a, InputFeatureUsage b)
		{
			return !(a == b);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002984 File Offset: 0x00000B84
		public InputFeatureUsage<T> As<T>()
		{
			bool flag = this.type != typeof(T);
			if (flag)
			{
				throw new ArgumentException("InputFeatureUsage type does not match out variable type.");
			}
			return new InputFeatureUsage<T>(this.name);
		}

		// Token: 0x0400005B RID: 91
		internal string m_Name;

		// Token: 0x0400005C RID: 92
		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;
	}
}
