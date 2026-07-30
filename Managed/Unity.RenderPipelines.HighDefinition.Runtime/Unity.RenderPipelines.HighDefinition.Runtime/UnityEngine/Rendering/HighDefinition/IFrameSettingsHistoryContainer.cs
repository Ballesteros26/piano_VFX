using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200013B RID: 315
	internal interface IFrameSettingsHistoryContainer : IDebugData
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000939 RID: 2361
		// (set) Token: 0x0600093A RID: 2362
		FrameSettingsHistory frameSettingsHistory { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600093B RID: 2363
		FrameSettingsOverrideMask frameSettingsMask { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600093C RID: 2364
		FrameSettings frameSettings { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600093D RID: 2365
		bool hasCustomFrameSettings { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600093E RID: 2366
		string panelName { get; }
	}
}
