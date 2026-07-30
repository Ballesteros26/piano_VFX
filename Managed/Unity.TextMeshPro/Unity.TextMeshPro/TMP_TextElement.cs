using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class TMP_TextElement
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00020BE3 File Offset: 0x0001EDE3
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00020BEB File Offset: 0x0001EDEB
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00020BF3 File Offset: 0x0001EDF3
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00020BFC File Offset: 0x0001EDFC
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00020C04 File Offset: 0x0001EE04
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00020C0D File Offset: 0x0001EE0D
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00020C15 File Offset: 0x0001EE15
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00020C1E File Offset: 0x0001EE1E
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00020C26 File Offset: 0x0001EE26
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

		// Token: 0x04000413 RID: 1043
		[SerializeField]
		protected TextElementType m_ElementType;

		// Token: 0x04000414 RID: 1044
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x04000415 RID: 1045
		internal Glyph m_Glyph;

		// Token: 0x04000416 RID: 1046
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000417 RID: 1047
		[SerializeField]
		internal float m_Scale;
	}
}
