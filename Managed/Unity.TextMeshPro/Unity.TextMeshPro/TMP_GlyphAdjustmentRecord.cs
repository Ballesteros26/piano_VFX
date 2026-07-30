using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007F4E File Offset: 0x0000614E
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007F57 File Offset: 0x00006157
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007F5F File Offset: 0x0000615F
		public TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007F68 File Offset: 0x00006168
		public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007F78 File Offset: 0x00006178
		internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
		{
			this.m_GlyphIndex = adjustmentRecord.glyphIndex;
			this.m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}

		// Token: 0x040000FB RID: 251
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x040000FC RID: 252
		[SerializeField]
		internal TMP_GlyphValueRecord m_GlyphValueRecord;
	}
}
