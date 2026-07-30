using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	internal class Character : TextElement
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		public Character(uint unicode, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}
	}
}
