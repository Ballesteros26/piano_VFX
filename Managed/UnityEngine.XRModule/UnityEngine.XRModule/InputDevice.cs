using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct InputDevice : IEquatable<InputDevice>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002F86 File Offset: 0x00001186
		internal InputDevice(ulong deviceId)
		{
			this.m_DeviceId = deviceId;
			this.m_Initialized = true;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F98 File Offset: 0x00001198
		private ulong deviceId
		{
			get
			{
				return this.m_Initialized ? this.m_DeviceId : ulong.MaxValue;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002FBC File Offset: 0x000011BC
		public XRInputSubsystem subsystem
		{
			get
			{
				bool flag = InputDevice.s_InputSubsystemCache == null;
				if (flag)
				{
					InputDevice.s_InputSubsystemCache = new List<XRInputSubsystem>();
				}
				bool initialized = this.m_Initialized;
				if (initialized)
				{
					uint num = (uint)(this.m_DeviceId >> 32);
					SubsystemManager.GetInstances<XRInputSubsystem>(InputDevice.s_InputSubsystemCache);
					for (int i = 0; i < InputDevice.s_InputSubsystemCache.Count; i++)
					{
						bool flag2 = num == InputDevice.s_InputSubsystemCache[i].GetIndex();
						if (flag2)
						{
							return InputDevice.s_InputSubsystemCache[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003050 File Offset: 0x00001250
		public bool isValid
		{
			get
			{
				return this.IsValidId() && InputDevices.IsDeviceValid(this.m_DeviceId);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003078 File Offset: 0x00001278
		public string name
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceName(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000030A0 File Offset: 0x000012A0
		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevice.characteristics instead.")]
		public InputDeviceRole role
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceRole(this.m_DeviceId) : InputDeviceRole.Unknown;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000030C8 File Offset: 0x000012C8
		public string manufacturer
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceManufacturer(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000030F0 File Offset: 0x000012F0
		public string serialNumber
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceSerialNumber(this.m_DeviceId) : null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003118 File Offset: 0x00001318
		public InputDeviceCharacteristics characteristics
		{
			get
			{
				return this.IsValidId() ? InputDevices.GetDeviceCharacteristics(this.m_DeviceId) : InputDeviceCharacteristics.None;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003140 File Offset: 0x00001340
		private bool IsValidId()
		{
			return this.deviceId != ulong.MaxValue;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003160 File Offset: 0x00001360
		public bool SendHapticImpulse(uint channel, float amplitude, float duration = 1f)
		{
			bool flag = !this.IsValidId();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = amplitude < 0f;
				if (flag3)
				{
					throw new ArgumentException("Amplitude of SendHapticImpulse cannot be negative.");
				}
				bool flag4 = duration < 0f;
				if (flag4)
				{
					throw new ArgumentException("Duration of SendHapticImpulse cannot be negative.");
				}
				flag2 = InputDevices.SendHapticImpulse(this.m_DeviceId, channel, amplitude, duration);
			}
			return flag2;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031C0 File Offset: 0x000013C0
		public bool SendHapticBuffer(uint channel, byte[] buffer)
		{
			bool flag = !this.IsValidId();
			return !flag && InputDevices.SendHapticBuffer(this.m_DeviceId, channel, buffer);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031F0 File Offset: 0x000013F0
		public bool TryGetHapticCapabilities(out HapticCapabilities capabilities)
		{
			bool flag = this.CheckValidAndSetDefault<HapticCapabilities>(out capabilities);
			return flag && InputDevices.TryGetHapticCapabilities(this.m_DeviceId, out capabilities);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003220 File Offset: 0x00001420
		public void StopHaptics()
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				InputDevices.StopHaptics(this.m_DeviceId);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003244 File Offset: 0x00001444
		public bool TryGetFeatureUsages(List<InputFeatureUsage> featureUsages)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureUsages(this.m_DeviceId, featureUsages);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003270 File Offset: 0x00001470
		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValue_bool(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032A4 File Offset: 0x000014A4
		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032D8 File Offset: 0x000014D8
		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValue_float(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000330C File Offset: 0x0000150C
		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector2f(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003340 File Offset: 0x00001540
		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValue_Vector3f(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003374 File Offset: 0x00001574
		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValue_Quaternionf(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000033A8 File Offset: 0x000015A8
		public bool TryGetFeatureValue(InputFeatureUsage<Hand> usage, out Hand value)
		{
			bool flag = this.CheckValidAndSetDefault<Hand>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRHand(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033DC File Offset: 0x000015DC
		public bool TryGetFeatureValue(InputFeatureUsage<Bone> usage, out Bone value)
		{
			bool flag = this.CheckValidAndSetDefault<Bone>(out value);
			return flag && InputDevices.TryGetFeatureValue_XRBone(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003410 File Offset: 0x00001610
		public bool TryGetFeatureValue(InputFeatureUsage<Eyes> usage, out Eyes value)
		{
			bool flag = this.CheckValidAndSetDefault<Eyes>(out value);
			return flag && InputDevices.TryGetFeatureValue_XREyes(this.m_DeviceId, usage.name, out value);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003444 File Offset: 0x00001644
		public bool TryGetFeatureValue(InputFeatureUsage<byte[]> usage, byte[] value)
		{
			bool flag = this.IsValidId();
			return flag && InputDevices.TryGetFeatureValue_Custom(this.m_DeviceId, usage.name, value);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003478 File Offset: 0x00001678
		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				uint num = 0U;
				bool flag2 = InputDevices.TryGetFeatureValue_UInt32(this.m_DeviceId, usage.name, out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034BC File Offset: 0x000016BC
		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, DateTime time, out bool value)
		{
			bool flag = this.CheckValidAndSetDefault<bool>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_bool(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034F8 File Offset: 0x000016F8
		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, DateTime time, out uint value)
		{
			bool flag = this.CheckValidAndSetDefault<uint>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003534 File Offset: 0x00001734
		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, DateTime time, out float value)
		{
			bool flag = this.CheckValidAndSetDefault<float>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_float(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003570 File Offset: 0x00001770
		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, DateTime time, out Vector2 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector2>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector2f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035AC File Offset: 0x000017AC
		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, DateTime time, out Vector3 value)
		{
			bool flag = this.CheckValidAndSetDefault<Vector3>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Vector3f(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035E8 File Offset: 0x000017E8
		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, DateTime time, out Quaternion value)
		{
			bool flag = this.CheckValidAndSetDefault<Quaternion>(out value);
			return flag && InputDevices.TryGetFeatureValueAtTime_Quaternionf(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003624 File Offset: 0x00001824
		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, DateTime time, out InputTrackingState value)
		{
			bool flag = this.IsValidId();
			if (flag)
			{
				uint num = 0U;
				bool flag2 = InputDevices.TryGetFeatureValueAtTime_UInt32(this.m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out num);
				if (flag2)
				{
					value = (InputTrackingState)num;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003670 File Offset: 0x00001870
		private bool CheckValidAndSetDefault<T>(out T value)
		{
			value = default(T);
			return this.IsValidId();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003690 File Offset: 0x00001890
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputDevice);
			return !flag && this.Equals((InputDevice)obj);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000036C4 File Offset: 0x000018C4
		public bool Equals(InputDevice other)
		{
			return this.deviceId == other.deviceId;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036E8 File Offset: 0x000018E8
		public override int GetHashCode()
		{
			return this.deviceId.GetHashCode();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003708 File Offset: 0x00001908
		public static bool operator ==(InputDevice a, InputDevice b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003724 File Offset: 0x00001924
		public static bool operator !=(InputDevice a, InputDevice b)
		{
			return !(a == b);
		}

		// Token: 0x04000099 RID: 153
		private static List<XRInputSubsystem> s_InputSubsystemCache;

		// Token: 0x0400009A RID: 154
		private ulong m_DeviceId;

		// Token: 0x0400009B RID: 155
		private bool m_Initialized;
	}
}
