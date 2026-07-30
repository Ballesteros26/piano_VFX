using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B4 RID: 180
	internal interface IVisualTreeUpdater : IDisposable
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600053C RID: 1340
		// (set) Token: 0x0600053D RID: 1341
		BaseVisualElementPanel panel { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600053E RID: 1342
		ProfilerMarker profilerMarker { get; }

		// Token: 0x0600053F RID: 1343
		void Update();

		// Token: 0x06000540 RID: 1344
		void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
