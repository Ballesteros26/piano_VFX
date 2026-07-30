using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Video;

namespace UnityEngine.Experimental.Video
{
	// Token: 0x02000003 RID: 3
	[StaticAccessor("VideoPlayerExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("VideoScriptingClasses.h")]
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	[NativeHeader("Modules/Video/Public/ScriptBindings/VideoPlayerExtensions.bindings.h")]
	public static class VideoPlayerExtensions
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023F4 File Offset: 0x000005F4
		public static AudioSampleProvider GetAudioSampleProvider(this VideoPlayer vp, ushort trackIndex)
		{
			ushort controlledAudioTrackCount = vp.controlledAudioTrackCount;
			bool flag = trackIndex >= controlledAudioTrackCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayer is currently configured with " + controlledAudioTrackCount + " tracks.");
			}
			VideoAudioOutputMode audioOutputMode = vp.audioOutputMode;
			bool flag2 = audioOutputMode != VideoAudioOutputMode.APIOnly;
			if (flag2)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider requires audioOutputMode to be APIOnly. Current: " + audioOutputMode);
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(vp.InternalGetAudioSampleProviderId(trackIndex), vp, trackIndex);
			bool flag3 = audioSampleProvider == null;
			if (flag3)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider got null provider.");
			}
			bool flag4 = audioSampleProvider.owner != vp;
			if (flag4)
			{
				throw new InvalidOperationException("Internal error: VideoPlayer.GetAudioSampleProvider got provider used by another object.");
			}
			bool flag5 = audioSampleProvider.trackIndex != trackIndex;
			if (flag5)
			{
				throw new InvalidOperationException(string.Concat(new object[] { "Internal error: VideoPlayer.GetAudioSampleProvider got provider for track ", audioSampleProvider.trackIndex, " instead of ", trackIndex }));
			}
			return audioSampleProvider;
		}

		// Token: 0x06000020 RID: 32
		[MethodImpl(4096)]
		internal static extern uint InternalGetAudioSampleProviderId(this VideoPlayer vp, ushort trackIndex);
	}
}
