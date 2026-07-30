using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007F99 File Offset: 0x00006199
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007FA1 File Offset: 0x000061A1
		public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007FAA File Offset: 0x000061AA
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00007FB2 File Offset: 0x000061B2
		public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007FBB File Offset: 0x000061BB
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00007FC3 File Offset: 0x000061C3
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007FCC File Offset: 0x000061CC
		public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007FE9 File Offset: 0x000061E9
		internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			this.m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x040000FD RID: 253
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x040000FE RID: 254
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x040000FF RID: 255
		[SerializeField]
		internal FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
