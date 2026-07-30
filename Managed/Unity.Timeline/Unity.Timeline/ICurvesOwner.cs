using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000D RID: 13
	internal interface ICurvesOwner
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008D RID: 141
		AnimationClip curves { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008E RID: 142
		bool hasCurves { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008F RID: 143
		double duration { get; }

		// Token: 0x06000090 RID: 144
		void CreateCurves(string curvesClipName);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000091 RID: 145
		string defaultCurvesName { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000092 RID: 146
		Object asset { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147
		Object assetOwner { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000094 RID: 148
		TrackAsset targetTrack { get; }
	}
}
