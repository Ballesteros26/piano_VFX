using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class TextureCurveParameter : VolumeParameter<TextureCurve>
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000BF51 File Offset: 0x0000A151
		public TextureCurveParameter(TextureCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
