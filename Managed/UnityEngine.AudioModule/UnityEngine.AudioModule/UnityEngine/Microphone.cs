using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class Microphone
	{
		// Token: 0x06000127 RID: 295
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern int GetMicrophoneDeviceIDFromName(string name);

		// Token: 0x06000128 RID: 296
		[MethodImpl(4096)]
		private static extern AudioClip StartRecord(int deviceID, bool loop, float lengthSec, int frequency);

		// Token: 0x06000129 RID: 297
		[MethodImpl(4096)]
		private static extern void EndRecord(int deviceID);

		// Token: 0x0600012A RID: 298
		[MethodImpl(4096)]
		private static extern bool IsRecording(int deviceID);

		// Token: 0x0600012B RID: 299
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern int GetRecordPosition(int deviceID);

		// Token: 0x0600012C RID: 300
		[MethodImpl(4096)]
		private static extern void GetDeviceCaps(int deviceID, out int minFreq, out int maxFreq);

		// Token: 0x0600012D RID: 301 RVA: 0x00002A00 File Offset: 0x00000C00
		public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (flag)
			{
				throw new ArgumentException("Couldn't acquire device ID for device name " + deviceName);
			}
			bool flag2 = lengthSec <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of recording must be greater than zero seconds (was: " + lengthSec + " seconds)");
			}
			bool flag3 = lengthSec > 3600;
			if (flag3)
			{
				throw new ArgumentException("Length of recording must be less than one hour (was: " + lengthSec + " seconds)");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency of recording must be greater than zero (was: " + frequency + " Hz)");
			}
			return Microphone.StartRecord(microphoneDeviceIDFromName, loop, (float)lengthSec, frequency);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public static void End(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.EndRecord(microphoneDeviceIDFromName);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012F RID: 303
		public static extern string[] devices
		{
			[NativeName("GetRecordDevices")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static bool IsRecording(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			return !flag && Microphone.IsRecording(microphoneDeviceIDFromName);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002B08 File Offset: 0x00000D08
		public static int GetPosition(string deviceName)
		{
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = Microphone.GetRecordPosition(microphoneDeviceIDFromName);
			}
			return num;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002B34 File Offset: 0x00000D34
		public static void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq)
		{
			minFreq = 0;
			maxFreq = 0;
			int microphoneDeviceIDFromName = Microphone.GetMicrophoneDeviceIDFromName(deviceName);
			bool flag = microphoneDeviceIDFromName == -1;
			if (!flag)
			{
				Microphone.GetDeviceCaps(microphoneDeviceIDFromName, out minFreq, out maxFreq);
			}
		}
	}
}
