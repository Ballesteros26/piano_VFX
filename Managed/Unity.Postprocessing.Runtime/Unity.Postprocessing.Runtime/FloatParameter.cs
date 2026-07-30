using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public sealed class FloatParameter : ParameterOverride<float>
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000093C8 File Offset: 0x000075C8
		public override void Interp(float from, float to, float t)
		{
			this.value = from + (to - from) * t;
		}
	}
}
