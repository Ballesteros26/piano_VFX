using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
	[StructLayout(0)]
	public sealed class DownloadHandlerMovieTexture : DownloadHandler
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002149 File Offset: 0x00000349
		public DownloadHandlerMovieTexture()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000215C File Offset: 0x0000035C
		protected override byte[] GetData()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002175 File Offset: 0x00000375
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for movies");
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002184 File Offset: 0x00000384
		public MovieTexture movieTexture
		{
			get
			{
				DownloadHandlerMovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021A0 File Offset: 0x000003A0
		public static MovieTexture GetContent(UnityWebRequest uwr)
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021B9 File Offset: 0x000003B9
		private static void FeatureRemoved()
		{
			throw new Exception("Movie texture has been removed, use VideoPlayer instead");
		}
	}
}
