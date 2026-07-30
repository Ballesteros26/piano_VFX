using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000150 RID: 336
	internal struct XRLayout
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
		internal XRPass CreatePass(XRPassCreateInfo passCreateInfo)
		{
			XRPass xrpass = XRPass.Create(passCreateInfo);
			this.xrSystem.AddPassToFrame(this.camera, xrpass);
			return xrpass;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0004DDDB File Offset: 0x0004BFDB
		internal void AddViewToPass(XRViewCreateInfo viewCreateInfo, XRPass pass)
		{
			pass.AddView(viewCreateInfo.projMatrix, viewCreateInfo.viewMatrix, viewCreateInfo.viewport, viewCreateInfo.textureArraySlice);
		}

		// Token: 0x04000F35 RID: 3893
		internal Camera camera;

		// Token: 0x04000F36 RID: 3894
		internal XRSystem xrSystem;
	}
}
