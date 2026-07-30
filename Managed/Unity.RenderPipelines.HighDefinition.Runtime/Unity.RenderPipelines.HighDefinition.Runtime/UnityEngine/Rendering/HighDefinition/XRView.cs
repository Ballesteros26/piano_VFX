using System;
using UnityEngine.XR;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000153 RID: 339
	internal struct XRView
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0004DDFB File Offset: 0x0004BFFB
		internal XRView(Camera camera, Camera.StereoscopicEye eye, int dstSlice)
		{
			this.projMatrix = camera.GetStereoProjectionMatrix(eye);
			this.viewMatrix = camera.GetStereoViewMatrix(eye);
			this.viewport = camera.pixelRect;
			this.occlusionMesh = null;
			this.textureArraySlice = dstSlice;
			this.legacyStereoEye = eye;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0004DE38 File Offset: 0x0004C038
		internal XRView(Matrix4x4 proj, Matrix4x4 view, Rect vp, int dstSlice)
		{
			this.projMatrix = proj;
			this.viewMatrix = view;
			this.viewport = vp;
			this.occlusionMesh = null;
			this.textureArraySlice = dstSlice;
			this.legacyStereoEye = (Camera.StereoscopicEye)(-1);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0004DE68 File Offset: 0x0004C068
		internal XRView(XRDisplaySubsystem.XRRenderPass renderPass, XRDisplaySubsystem.XRRenderParameter renderParameter)
		{
			this.projMatrix = renderParameter.projection;
			this.viewMatrix = renderParameter.view;
			this.viewport = renderParameter.viewport;
			this.occlusionMesh = renderParameter.occlusionMesh;
			this.textureArraySlice = renderParameter.textureArraySlice;
			this.legacyStereoEye = (Camera.StereoscopicEye)(-1);
			this.viewport.x = this.viewport.x * (float)renderPass.renderTargetDesc.width;
			this.viewport.width = this.viewport.width * (float)renderPass.renderTargetDesc.width;
			this.viewport.y = this.viewport.y * (float)renderPass.renderTargetDesc.height;
			this.viewport.height = this.viewport.height * (float)renderPass.renderTargetDesc.height;
		}

		// Token: 0x04000F40 RID: 3904
		internal readonly Matrix4x4 projMatrix;

		// Token: 0x04000F41 RID: 3905
		internal readonly Matrix4x4 viewMatrix;

		// Token: 0x04000F42 RID: 3906
		internal readonly Rect viewport;

		// Token: 0x04000F43 RID: 3907
		internal readonly Mesh occlusionMesh;

		// Token: 0x04000F44 RID: 3908
		internal readonly int textureArraySlice;

		// Token: 0x04000F45 RID: 3909
		internal readonly Camera.StereoscopicEye legacyStereoEye;
	}
}
