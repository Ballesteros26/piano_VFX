using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public sealed class ExposureModeParameter : VolumeParameter<ExposureMode>
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x000387E4 File Offset: 0x000369E4
		public ExposureModeParameter(ExposureMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
