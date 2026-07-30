using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal sealed class DiffusionProfileSettingsParameter : VolumeParameter<DiffusionProfileSettings[]>
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000B517 File Offset: 0x00009717
		public DiffusionProfileSettingsParameter(DiffusionProfileSettings[] value, bool overrideState = true)
			: base(value, overrideState)
		{
		}
	}
}
