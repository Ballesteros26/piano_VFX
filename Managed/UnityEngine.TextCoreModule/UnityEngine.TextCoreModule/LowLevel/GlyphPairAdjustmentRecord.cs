using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000048 RID: 72
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphPairAdjustmentRecord
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0001A910 File Offset: 0x00018B10
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0001A928 File Offset: 0x00018B28
		public GlyphAdjustmentRecord firstAdjustmentRecord
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0001A934 File Offset: 0x00018B34
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0001A94C File Offset: 0x00018B4C
		public GlyphAdjustmentRecord secondAdjustmentRecord
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

		// Token: 0x060001D7 RID: 471 RVA: 0x0001A956 File Offset: 0x00018B56
		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
		}

		// Token: 0x04000387 RID: 903
		[SerializeField]
		[NativeName("firstAdjustmentRecord")]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x04000388 RID: 904
		[NativeName("secondAdjustmentRecord")]
		[SerializeField]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;
	}
}
