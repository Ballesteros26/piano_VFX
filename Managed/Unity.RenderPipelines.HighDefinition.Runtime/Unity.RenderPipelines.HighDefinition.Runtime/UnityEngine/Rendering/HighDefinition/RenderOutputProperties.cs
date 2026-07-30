using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000124 RID: 292
	public struct RenderOutputProperties
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x000487A4 File Offset: 0x000469A4
		public RenderOutputProperties(Vector2Int outputSize, Matrix4x4 cameraToWorldMatrixRhs, Matrix4x4 projectionMatrix)
		{
			this.outputSize = outputSize;
			this.cameraToWorldMatrixRHS = cameraToWorldMatrixRhs;
			this.projectionMatrix = projectionMatrix;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000487BB File Offset: 0x000469BB
		internal static RenderOutputProperties From(HDCamera hdCamera)
		{
			return new RenderOutputProperties(new Vector2Int(hdCamera.actualWidth, hdCamera.actualHeight), hdCamera.camera.cameraToWorldMatrix, hdCamera.mainViewConstants.projMatrix);
		}

		// Token: 0x04000D9D RID: 3485
		public readonly Vector2Int outputSize;

		// Token: 0x04000D9E RID: 3486
		public readonly Matrix4x4 cameraToWorldMatrixRHS;

		// Token: 0x04000D9F RID: 3487
		public readonly Matrix4x4 projectionMatrix;
	}
}
