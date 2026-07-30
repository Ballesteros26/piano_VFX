using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class AudioSettings
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(4096)]
		private static extern AudioSpeakerMode GetSpeakerMode();

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		[NativeMethod(Name = "AudioSettings::SetConfiguration", IsFreeFunction = true)]
		[NativeThrows]
		private static bool SetConfiguration(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration_Injected(ref config);
		}

		// Token: 0x06000003 RID: 3
		[NativeMethod(Name = "AudioSettings::GetSampleRate", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern int GetSampleRate();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4
		public static extern AudioSpeakerMode driverCapabilities
		{
			[NativeName("GetSpeakerModeCaps")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000205C File Offset: 0x0000025C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00000274
		public static AudioSpeakerMode speakerMode
		{
			get
			{
				return AudioSettings.GetSpeakerMode();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.speakerMode is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.speakerMode = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.speakerMode failed");
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		internal static extern int profilerCaptureFlags
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8
		public static extern double dspTime
		{
			[NativeMethod(Name = "GetDSPTime", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		public static int outputSampleRate
		{
			get
			{
				return AudioSettings.GetSampleRate();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.outputSampleRate is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = AudioSettings.GetConfiguration();
				configuration.sampleRate = value;
				bool flag = !AudioSettings.SetConfiguration(configuration);
				if (flag)
				{
					Debug.LogWarning("Setting AudioSettings.outputSampleRate failed");
				}
			}
		}

		// Token: 0x0600000B RID: 11
		[NativeMethod(Name = "AudioSettings::GetDSPBufferSize", IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

		// Token: 0x0600000C RID: 12 RVA: 0x0000210C File Offset: 0x0000030C
		[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
		public static void SetDSPBufferSize(int bufferLength, int numBuffers)
		{
			Debug.LogWarning("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
			AudioConfiguration configuration = AudioSettings.GetConfiguration();
			configuration.dspBufferSize = bufferLength;
			bool flag = !AudioSettings.SetConfiguration(configuration);
			if (flag)
			{
				Debug.LogWarning("SetDSPBufferSize failed");
			}
		}

		// Token: 0x0600000D RID: 13
		[NativeName("GetCurrentSpatializerDefinitionName")]
		[MethodImpl(4096)]
		public static extern string GetSpatializerPluginName();

		// Token: 0x0600000E RID: 14 RVA: 0x0000214C File Offset: 0x0000034C
		public static AudioConfiguration GetConfiguration()
		{
			AudioConfiguration audioConfiguration;
			AudioSettings.GetConfiguration_Injected(out audioConfiguration);
			return audioConfiguration;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002164 File Offset: 0x00000364
		public static bool Reset(AudioConfiguration config)
		{
			return AudioSettings.SetConfiguration(config);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000010 RID: 16 RVA: 0x0000217C File Offset: 0x0000037C
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x000021B0 File Offset: 0x000003B0
		[field: DebuggerBrowsable(0)]
		public static event AudioSettings.AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		// Token: 0x06000012 RID: 18 RVA: 0x000021E4 File Offset: 0x000003E4
		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			bool flag = AudioSettings.OnAudioConfigurationChanged != null;
			if (flag)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19
		internal static extern bool unityAudioDisabled
		{
			[NativeName("IsAudioDisabled")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000014 RID: 20
		[NativeMethod(Name = "AudioSettings::GetCurrentAmbisonicDefinitionName", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern string GetAmbisonicDecoderPluginName();

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern bool SetConfiguration_Injected(ref AudioConfiguration config);

		// Token: 0x06000017 RID: 23
		[MethodImpl(4096)]
		private static extern void GetConfiguration_Injected(out AudioConfiguration ret);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000019 RID: 25
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);

		// Token: 0x0200000E RID: 14
		public static class Mobile
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002214 File Offset: 0x00000414
			public static bool muteState
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002228 File Offset: 0x00000428
			// (set) Token: 0x0600001E RID: 30 RVA: 0x0000223B File Offset: 0x0000043B
			public static bool stopAudioOutputOnMute
			{
				get
				{
					return false;
				}
				set
				{
					Debug.LogWarning("Setting AudioSettings.Mobile.stopAudioOutputOnMute is possible on iOS and Android only");
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600001F RID: 31 RVA: 0x0000224C File Offset: 0x0000044C
			public static bool audioOutputStarted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x14000002 RID: 2
			// (add) Token: 0x06000020 RID: 32 RVA: 0x00002260 File Offset: 0x00000460
			// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002294 File Offset: 0x00000494
			[field: DebuggerBrowsable(0)]
			public static event Action<bool> OnMuteStateChanged;

			// Token: 0x06000022 RID: 34 RVA: 0x000022C7 File Offset: 0x000004C7
			public static void StartAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StartAudioOutput is implemented for iOS and Android only");
			}

			// Token: 0x06000023 RID: 35 RVA: 0x000022D5 File Offset: 0x000004D5
			public static void StopAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StopAudioOutput is implemented for iOS and Android only");
			}
		}
	}
}
