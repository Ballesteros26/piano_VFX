using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipExtensions.bindings.h")]
	[NativeHeader("Modules/Audio/Public/AudioClip.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	internal static class AudioClipExtensionsInternal
	{
		// Token: 0x060001C9 RID: 457
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern uint Internal_CreateAudioClipSampleProvider(this AudioClip audioClip, ulong start, long end, bool loop, bool allowDrop);
	}
}
