using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
	[EditorBrowsable(1)]
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	public sealed class MovieTexture : Texture
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private static void FeatureRemoved()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00002C05 File Offset: 0x00000E05
		private MovieTexture()
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002C0F File Offset: 0x00000E0F
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Play()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00002C0F File Offset: 0x00000E0F
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Stop()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00002C0F File Offset: 0x00000E0F
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Pause()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00002C18 File Offset: 0x00000E18
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public AudioClip audioClip
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00002C0F File Offset: 0x00000E0F
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool loop
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
			set
			{
				MovieTexture.FeatureRemoved();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00002C50 File Offset: 0x00000E50
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isPlaying
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002C6C File Offset: 0x00000E6C
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isReadyToPlay
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002C88 File Offset: 0x00000E88
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public float duration
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return 1f;
			}
		}
	}
}
