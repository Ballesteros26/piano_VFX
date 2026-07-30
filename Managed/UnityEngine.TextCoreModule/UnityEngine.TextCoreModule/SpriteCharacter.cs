using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal class SpriteCharacter : TextElement
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006D7C File Offset: 0x00004F7C
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006D94 File Offset: 0x00004F94
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value == this.m_Name;
				if (!flag)
				{
					this.m_Name = value;
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006DCC File Offset: 0x00004FCC
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006DF5 File Offset: 0x00004FF5
		public SpriteCharacter(uint unicode, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x04000177 RID: 375
		[SerializeField]
		private string m_Name;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		private int m_HashCode;
	}
}
