using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007F RID: 127
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ColorParameter : VolumeParameter<Color>
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public ColorParameter(Color value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D4EE File Offset: 0x0000B6EE
		public ColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
			: base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D528 File Offset: 0x0000B728
		public override void Interp(Color from, Color to, float t)
		{
			this.m_Value.r = from.r + (to.r - from.r) * t;
			this.m_Value.g = from.g + (to.g - from.g) * t;
			this.m_Value.b = from.b + (to.b - from.b) * t;
			this.m_Value.a = from.a + (to.a - from.a) * t;
		}

		// Token: 0x040001C3 RID: 451
		public bool hdr;

		// Token: 0x040001C4 RID: 452
		public bool showAlpha = true;

		// Token: 0x040001C5 RID: 453
		public bool showEyeDropper = true;
	}
}
