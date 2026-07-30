using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Audio;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioRenderer.bindings.h")]
	public class AudioRenderer
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00002B64 File Offset: 0x00000D64
		public static bool Start()
		{
			return AudioRenderer.Internal_AudioRenderer_Start();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002B7C File Offset: 0x00000D7C
		public static bool Stop()
		{
			return AudioRenderer.Internal_AudioRenderer_Stop();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002B94 File Offset: 0x00000D94
		public static int GetSampleCountForCaptureFrame()
		{
			return AudioRenderer.Internal_AudioRenderer_GetSampleCountForCaptureFrame();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00002BAC File Offset: 0x00000DAC
		internal static bool AddMixerGroupSink(AudioMixerGroup mixerGroup, NativeArray<float> buffer, bool excludeFromMix)
		{
			return AudioRenderer.Internal_AudioRenderer_AddMixerGroupSink(mixerGroup, buffer.GetUnsafePtr<float>(), buffer.Length, excludeFromMix);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static bool Render(NativeArray<float> buffer)
		{
			return AudioRenderer.Internal_AudioRenderer_Render(buffer.GetUnsafePtr<float>(), buffer.Length);
		}

		// Token: 0x06000139 RID: 313
		[MethodImpl(4096)]
		internal static extern bool Internal_AudioRenderer_Start();

		// Token: 0x0600013A RID: 314
		[MethodImpl(4096)]
		internal static extern bool Internal_AudioRenderer_Stop();

		// Token: 0x0600013B RID: 315
		[MethodImpl(4096)]
		internal static extern int Internal_AudioRenderer_GetSampleCountForCaptureFrame();

		// Token: 0x0600013C RID: 316
		[MethodImpl(4096)]
		internal unsafe static extern bool Internal_AudioRenderer_AddMixerGroupSink(AudioMixerGroup mixerGroup, void* ptr, int length, bool excludeFromMix);

		// Token: 0x0600013D RID: 317
		[MethodImpl(4096)]
		internal unsafe static extern bool Internal_AudioRenderer_Render(void* ptr, int length);
	}
}
