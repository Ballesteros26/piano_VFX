using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	internal class TextElement
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006EAC File Offset: 0x000050AC
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006EC4 File Offset: 0x000050C4
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00006EDC File Offset: 0x000050DC
		public uint unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006EE8 File Offset: 0x000050E8
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00006F00 File Offset: 0x00005100
		public Glyph glyph
		{
			get
			{
				return this.m_Glyph;
			}
			set
			{
				this.m_Glyph = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006F0C File Offset: 0x0000510C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00006F24 File Offset: 0x00005124
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006F30 File Offset: 0x00005130
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006F48 File Offset: 0x00005148
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x0400017D RID: 381
		[SerializeField]
		protected TextElementType m_ElementType;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private uint m_Unicode;

		// Token: 0x0400017F RID: 383
		private Glyph m_Glyph;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		private uint m_GlyphIndex;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		private float m_Scale;
	}
}
