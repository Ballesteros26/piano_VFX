using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000080 RID: 128
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		public NoInterpColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D5D1 File Offset: 0x0000B7D1
		public NoInterpColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x040001C6 RID: 454
		public bool hdr;

		// Token: 0x040001C7 RID: 455
		public bool showAlpha = true;

		// Token: 0x040001C8 RID: 456
		public bool showEyeDropper = true;
	}
}
