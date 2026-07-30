using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200003F RID: 63
	public class VertexHelper : IDisposable
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00005114 File Offset: 0x00003314
		public VertexHelper()
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000152D0 File Offset: 0x000134D0
		public VertexHelper(Mesh m)
		{
			this.InitializeListIfRequired();
			this.m_Positions.AddRange(m.vertices);
			this.m_Colors.AddRange(m.colors32);
			this.m_Uv0S.AddRange(m.uv);
			this.m_Uv1S.AddRange(m.uv2);
			this.m_Uv2S.AddRange(m.uv3);
			this.m_Uv3S.AddRange(m.uv4);
			this.m_Normals.AddRange(m.normals);
			this.m_Tangents.AddRange(m.tangents);
			this.m_Indices.AddRange(m.GetIndices(0));
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00015384 File Offset: 0x00013584
		private void InitializeListIfRequired()
		{
			if (!this.m_ListsInitalized)
			{
				this.m_Positions = ListPool<Vector3>.Get();
				this.m_Colors = ListPool<Color32>.Get();
				this.m_Uv0S = ListPool<Vector2>.Get();
				this.m_Uv1S = ListPool<Vector2>.Get();
				this.m_Uv2S = ListPool<Vector2>.Get();
				this.m_Uv3S = ListPool<Vector2>.Get();
				this.m_Normals = ListPool<Vector3>.Get();
				this.m_Tangents = ListPool<Vector4>.Get();
				this.m_Indices = ListPool<int>.Get();
				this.m_ListsInitalized = true;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015404 File Offset: 0x00013604
		public void Dispose()
		{
			if (this.m_ListsInitalized)
			{
				ListPool<Vector3>.Release(this.m_Positions);
				ListPool<Color32>.Release(this.m_Colors);
				ListPool<Vector2>.Release(this.m_Uv0S);
				ListPool<Vector2>.Release(this.m_Uv1S);
				ListPool<Vector2>.Release(this.m_Uv2S);
				ListPool<Vector2>.Release(this.m_Uv3S);
				ListPool<Vector3>.Release(this.m_Normals);
				ListPool<Vector4>.Release(this.m_Tangents);
				ListPool<int>.Release(this.m_Indices);
				this.m_Positions = null;
				this.m_Colors = null;
				this.m_Uv0S = null;
				this.m_Uv1S = null;
				this.m_Uv2S = null;
				this.m_Uv3S = null;
				this.m_Normals = null;
				this.m_Tangents = null;
				this.m_Indices = null;
				this.m_ListsInitalized = false;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000154C8 File Offset: 0x000136C8
		public void Clear()
		{
			if (this.m_ListsInitalized)
			{
				this.m_Positions.Clear();
				this.m_Colors.Clear();
				this.m_Uv0S.Clear();
				this.m_Uv1S.Clear();
				this.m_Uv2S.Clear();
				this.m_Uv3S.Clear();
				this.m_Normals.Clear();
				this.m_Tangents.Clear();
				this.m_Indices.Clear();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00015540 File Offset: 0x00013740
		public int currentVertCount
		{
			get
			{
				if (this.m_Positions == null)
				{
					return 0;
				}
				return this.m_Positions.Count;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00015557 File Offset: 0x00013757
		public int currentIndexCount
		{
			get
			{
				if (this.m_Indices == null)
				{
					return 0;
				}
				return this.m_Indices.Count;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015570 File Offset: 0x00013770
		public void PopulateUIVertex(ref UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			vertex.position = this.m_Positions[i];
			vertex.color = this.m_Colors[i];
			vertex.uv0 = this.m_Uv0S[i];
			vertex.uv1 = this.m_Uv1S[i];
			vertex.uv2 = this.m_Uv2S[i];
			vertex.uv3 = this.m_Uv3S[i];
			vertex.normal = this.m_Normals[i];
			vertex.tangent = this.m_Tangents[i];
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015614 File Offset: 0x00013814
		public void SetUIVertex(UIVertex vertex, int i)
		{
			this.InitializeListIfRequired();
			this.m_Positions[i] = vertex.position;
			this.m_Colors[i] = vertex.color;
			this.m_Uv0S[i] = vertex.uv0;
			this.m_Uv1S[i] = vertex.uv1;
			this.m_Uv2S[i] = vertex.uv2;
			this.m_Uv3S[i] = vertex.uv3;
			this.m_Normals[i] = vertex.normal;
			this.m_Tangents[i] = vertex.tangent;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000156B8 File Offset: 0x000138B8
		public void FillMesh(Mesh mesh)
		{
			this.InitializeListIfRequired();
			mesh.Clear();
			if (this.m_Positions.Count >= 65000)
			{
				throw new ArgumentException("Mesh can not have more than 65000 vertices");
			}
			mesh.SetVertices(this.m_Positions);
			mesh.SetColors(this.m_Colors);
			mesh.SetUVs(0, this.m_Uv0S);
			mesh.SetUVs(1, this.m_Uv1S);
			mesh.SetUVs(2, this.m_Uv2S);
			mesh.SetUVs(3, this.m_Uv3S);
			mesh.SetNormals(this.m_Normals);
			mesh.SetTangents(this.m_Tangents);
			mesh.SetTriangles(this.m_Indices, 0);
			mesh.RecalculateBounds();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015768 File Offset: 0x00013968
		public void AddVert(Vector3 position, Color32 color, Vector2 uv0, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector3 normal, Vector4 tangent)
		{
			this.InitializeListIfRequired();
			this.m_Positions.Add(position);
			this.m_Colors.Add(color);
			this.m_Uv0S.Add(uv0);
			this.m_Uv1S.Add(uv1);
			this.m_Uv2S.Add(uv2);
			this.m_Uv3S.Add(uv3);
			this.m_Normals.Add(normal);
			this.m_Tangents.Add(tangent);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000157E0 File Offset: 0x000139E0
		public void AddVert(Vector3 position, Color32 color, Vector2 uv0, Vector2 uv1, Vector3 normal, Vector4 tangent)
		{
			this.AddVert(position, color, uv0, uv1, Vector2.zero, Vector2.zero, normal, tangent);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015806 File Offset: 0x00013A06
		public void AddVert(Vector3 position, Color32 color, Vector2 uv0)
		{
			this.AddVert(position, color, uv0, Vector2.zero, VertexHelper.s_DefaultNormal, VertexHelper.s_DefaultTangent);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015820 File Offset: 0x00013A20
		public void AddVert(UIVertex v)
		{
			this.AddVert(v.position, v.color, v.uv0, v.uv1, v.uv2, v.uv3, v.normal, v.tangent);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015863 File Offset: 0x00013A63
		public void AddTriangle(int idx0, int idx1, int idx2)
		{
			this.InitializeListIfRequired();
			this.m_Indices.Add(idx0);
			this.m_Indices.Add(idx1);
			this.m_Indices.Add(idx2);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015890 File Offset: 0x00013A90
		public void AddUIVertexQuad(UIVertex[] verts)
		{
			int currentVertCount = this.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				this.AddVert(verts[i].position, verts[i].color, verts[i].uv0, verts[i].uv1, verts[i].normal, verts[i].tangent);
			}
			this.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			this.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015918 File Offset: 0x00013B18
		public void AddUIVertexStream(List<UIVertex> verts, List<int> indices)
		{
			this.InitializeListIfRequired();
			if (verts != null)
			{
				CanvasRenderer.AddUIVertexStream(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents);
			}
			if (indices != null)
			{
				this.m_Indices.AddRange(indices);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015974 File Offset: 0x00013B74
		public void AddUIVertexTriangleStream(List<UIVertex> verts)
		{
			if (verts == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.SplitUIVertexStreams(verts, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000159C8 File Offset: 0x00013BC8
		public void GetUIVertexStream(List<UIVertex> stream)
		{
			if (stream == null)
			{
				return;
			}
			this.InitializeListIfRequired();
			CanvasRenderer.CreateUIVertexStream(stream, this.m_Positions, this.m_Colors, this.m_Uv0S, this.m_Uv1S, this.m_Uv2S, this.m_Uv3S, this.m_Normals, this.m_Tangents, this.m_Indices);
		}

		// Token: 0x0400017B RID: 379
		private List<Vector3> m_Positions;

		// Token: 0x0400017C RID: 380
		private List<Color32> m_Colors;

		// Token: 0x0400017D RID: 381
		private List<Vector2> m_Uv0S;

		// Token: 0x0400017E RID: 382
		private List<Vector2> m_Uv1S;

		// Token: 0x0400017F RID: 383
		private List<Vector2> m_Uv2S;

		// Token: 0x04000180 RID: 384
		private List<Vector2> m_Uv3S;

		// Token: 0x04000181 RID: 385
		private List<Vector3> m_Normals;

		// Token: 0x04000182 RID: 386
		private List<Vector4> m_Tangents;

		// Token: 0x04000183 RID: 387
		private List<int> m_Indices;

		// Token: 0x04000184 RID: 388
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x04000185 RID: 389
		private static readonly Vector3 s_DefaultNormal = Vector3.back;

		// Token: 0x04000186 RID: 390
		private bool m_ListsInitalized;
	}
}
