using System;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class TMP_Character : TMP_TextElement
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002736 File Offset: 0x00000936
		public TMP_Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002750 File Offset: 0x00000950
		public TMP_Character(uint unicode, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002784 File Offset: 0x00000984
		internal TMP_Character(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
