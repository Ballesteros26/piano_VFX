using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005F RID: 95
	internal interface IStylePainter
	{
		// Token: 0x06000232 RID: 562
		MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags);

		// Token: 0x06000233 RID: 563
		void DrawText(MeshGenerationContextUtils.TextParams textParams, TextHandle handle, float pixelsPerPoint);

		// Token: 0x06000234 RID: 564
		void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams);

		// Token: 0x06000235 RID: 565
		void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams);

		// Token: 0x06000236 RID: 566
		void DrawImmediate(Action callback, bool cullingEnabled);

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000237 RID: 567
		VisualElement visualElement { get; }
	}
}
