using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSourceExtensions.bindings.h")]
	internal static class AudioSourceExtensionsInternal
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00003BB5 File Offset: 0x00001DB5
		public static void RegisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_RegisterSampleProviderWithAudioSource(source, provider.id);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public static void UnregisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			AudioSourceExtensionsInternal.Internal_UnregisterSampleProviderFromAudioSource(source, provider.id);
		}

		// Token: 0x06000213 RID: 531
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void Internal_RegisterSampleProviderWithAudioSource(AudioSource source, uint providerId);

		// Token: 0x06000214 RID: 532
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void Internal_UnregisterSampleProviderFromAudioSource(AudioSource source, uint providerId);
	}
}
