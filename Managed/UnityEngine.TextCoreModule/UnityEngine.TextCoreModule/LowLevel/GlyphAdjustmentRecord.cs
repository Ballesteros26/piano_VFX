using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000047 RID: 71
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphAdjustmentRecord
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0001A8B8 File Offset: 0x00018AB8
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0001A8D0 File Offset: 0x00018AD0
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0001A8DC File Offset: 0x00018ADC
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0001A8F4 File Offset: 0x00018AF4
		public GlyphValueRecord glyphValueRecord
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

		// Token: 0x060001D2 RID: 466 RVA: 0x0001A8FE File Offset: 0x00018AFE
		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x04000385 RID: 901
		[SerializeField]
		[NativeName("glyphIndex")]
		private uint m_GlyphIndex;

		// Token: 0x04000386 RID: 902
		[SerializeField]
		[NativeName("glyphValueRecord")]
		private GlyphValueRecord m_GlyphValueRecord;
	}
}
