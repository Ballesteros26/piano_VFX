using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000017 RID: 23
	internal static class TimelineClipCapsExtensions
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00006603 File Offset: 0x00004803
		public static bool SupportsLooping(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Looping) > ClipCaps.None;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006615 File Offset: 0x00004815
		public static bool SupportsExtrapolation(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Extrapolation) > ClipCaps.None;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006627 File Offset: 0x00004827
		public static bool SupportsClipIn(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.ClipIn) > ClipCaps.None;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006639 File Offset: 0x00004839
		public static bool SupportsSpeedMultiplier(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.SpeedMultiplier) > ClipCaps.None;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000664B File Offset: 0x0000484B
		public static bool SupportsBlending(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Blending) > ClipCaps.None;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000665E File Offset: 0x0000485E
		public static bool HasAll(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) == flags;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006666 File Offset: 0x00004866
		public static bool HasAny(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) > ClipCaps.None;
		}
	}
}
