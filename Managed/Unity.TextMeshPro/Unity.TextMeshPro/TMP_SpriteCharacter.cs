using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class TMP_SpriteCharacter : TMP_TextElement
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00011576 File Offset: 0x0000F776
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0001157E File Offset: 0x0000F77E
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value == this.m_Name)
				{
					return;
				}
				this.m_Name = value;
				this.m_HashCode = TMP_TextParsingUtilities.GetHashCodeCaseSensitive(this.m_Name);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000115A7 File Offset: 0x0000F7A7
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000115AF File Offset: 0x0000F7AF
		public TMP_SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000115BE File Offset: 0x0000F7BE
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x040002A9 RID: 681
		[SerializeField]
		private string m_Name;

		// Token: 0x040002AA RID: 682
		[SerializeField]
		private int m_HashCode;
	}
}
