using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	[EditorBrowsable(1)]
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	public sealed class GUITexture
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000BBBA File Offset: 0x00009DBA
		private static void FeatureRemoved()
		{
			throw new Exception("GUITexture has been removed from Unity. Use UI.Image instead.");
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Color color
		{
			get
			{
				GUITexture.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000BC00 File Offset: 0x00009E00
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Texture texture
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BC1C File Offset: 0x00009E1C
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Rect pixelInset
		{
			get
			{
				GUITexture.FeatureRemoved();
				return default(Rect);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BC40 File Offset: 0x00009E40
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public RectOffset border
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}
	}
}
