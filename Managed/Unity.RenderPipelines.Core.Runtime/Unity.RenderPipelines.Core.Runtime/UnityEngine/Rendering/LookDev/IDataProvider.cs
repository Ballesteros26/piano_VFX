using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x02000091 RID: 145
	public interface IDataProvider
	{
		// Token: 0x06000384 RID: 900
		void FirstInitScene(StageRuntimeInterface stage);

		// Token: 0x06000385 RID: 901
		void UpdateSky(Camera camera, Sky sky, StageRuntimeInterface stage);

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000386 RID: 902
		IEnumerable<string> supportedDebugModes { get; }

		// Token: 0x06000387 RID: 903
		void UpdateDebugMode(int debugIndex);

		// Token: 0x06000388 RID: 904
		void GetShadowMask(ref RenderTexture output, StageRuntimeInterface stage);

		// Token: 0x06000389 RID: 905
		void OnBeginRendering(StageRuntimeInterface stage);

		// Token: 0x0600038A RID: 906
		void OnEndRendering(StageRuntimeInterface stage);
	}
}
