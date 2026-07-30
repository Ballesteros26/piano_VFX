using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A1 RID: 417
	public class MeshGenerationContext
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002BD48 File Offset: 0x00029F48
		public VisualElement visualElement
		{
			get
			{
				return this.painter.visualElement;
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002BD65 File Offset: 0x00029F65
		internal MeshGenerationContext(IStylePainter painter)
		{
			this.painter = painter;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002BD78 File Offset: 0x00029F78
		public MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture = null)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, null, MeshGenerationContext.MeshFlags.None);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002BD9C File Offset: 0x00029F9C
		internal MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, material, flags);
		}

		// Token: 0x0400050A RID: 1290
		internal IStylePainter painter;

		// Token: 0x020001A2 RID: 418
		[Flags]
		internal enum MeshFlags
		{
			// Token: 0x0400050C RID: 1292
			None = 0,
			// Token: 0x0400050D RID: 1293
			UVisDisplacement = 1,
			// Token: 0x0400050E RID: 1294
			IsSVGGradients = 2,
			// Token: 0x0400050F RID: 1295
			IsCustomSVGGradients = 3
		}
	}
}
