using System;
using Unity.Collections;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019C RID: 412
	public class MeshWriteData
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0002B290 File Offset: 0x00029490
		internal MeshWriteData()
		{
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002B29C File Offset: 0x0002949C
		public int vertexCount
		{
			get
			{
				return this.m_Vertices.Length;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002B2BC File Offset: 0x000294BC
		public int indexCount
		{
			get
			{
				return this.m_Indices.Length;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002B2DC File Offset: 0x000294DC
		public Rect uvRegion
		{
			get
			{
				return this.m_UVRegion;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002B2F4 File Offset: 0x000294F4
		public void SetNextVertex(Vertex vertex)
		{
			int num = this.currentVertex;
			this.currentVertex = num + 1;
			this.m_Vertices[num] = vertex;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002B320 File Offset: 0x00029520
		public void SetNextIndex(ushort index)
		{
			int num = this.currentIndex;
			this.currentIndex = num + 1;
			this.m_Indices[num] = index;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002B34C File Offset: 0x0002954C
		public void SetAllVertices(Vertex[] vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002B394 File Offset: 0x00029594
		public void SetAllVertices(NativeSlice<Vertex> vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002B3DC File Offset: 0x000295DC
		public void SetAllIndices(ushort[] indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002B424 File Offset: 0x00029624
		public void SetAllIndices(NativeSlice<ushort> indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002B46C File Offset: 0x0002966C
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = new Rect(0f, 0f, 1f, 1f);
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002B4B8 File Offset: 0x000296B8
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Rect uvRegion)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.m_UVRegion = uvRegion;
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x040004D8 RID: 1240
		internal NativeSlice<Vertex> m_Vertices;

		// Token: 0x040004D9 RID: 1241
		internal NativeSlice<ushort> m_Indices;

		// Token: 0x040004DA RID: 1242
		internal Rect m_UVRegion;

		// Token: 0x040004DB RID: 1243
		internal int currentIndex;

		// Token: 0x040004DC RID: 1244
		internal int currentVertex;
	}
}
