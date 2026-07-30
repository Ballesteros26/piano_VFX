using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class AnimationCurveParameter : VolumeParameter<AnimationCurve>
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000D924 File Offset: 0x0000BB24
		public AnimationCurveParameter(AnimationCurve value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
