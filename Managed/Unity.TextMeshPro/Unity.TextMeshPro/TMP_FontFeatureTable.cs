using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class TMP_FontFeatureTable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007D6F File Offset: 0x00005F6F
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00007D77 File Offset: 0x00005F77
		internal List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
			set
			{
				this.m_GlyphPairAdjustmentRecords = value;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007D80 File Offset: 0x00005F80
		public TMP_FontFeatureTable()
		{
			this.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<uint, TMP_GlyphPairAdjustmentRecord>();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public void SortGlyphPairAdjustmentRecords()
		{
			if (this.m_GlyphPairAdjustmentRecords.Count > 0)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<TMP_GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x040000F1 RID: 241
		[SerializeField]
		internal List<TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x040000F2 RID: 242
		internal Dictionary<uint, TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookupDictionary;
	}
}
