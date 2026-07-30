using System;
using System.Collections.Generic;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000060 RID: 96
	public class TerrainSegment : MonoBehaviour
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0001533E File Offset: 0x0001353E
		private void Awake()
		{
			this.Setup(this.numberOfPoints, true, 0f);
			this.RecalculateTerrainFromCurve();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00015358 File Offset: 0x00013558
		private void CalculateUVsAndVerts(List<Vector3> newVertices)
		{
			Vector2 vector = new Vector2(1f, 1f);
			if (this.meshRenderer.sharedMaterial != null && this.meshRenderer.sharedMaterial.mainTexture != null)
			{
				vector.x = (float)this.pixelsPerUnit / (float)this.meshRenderer.sharedMaterial.mainTexture.width;
				vector.y = (float)this.pixelsPerUnit / (float)this.meshRenderer.sharedMaterial.mainTexture.height;
			}
			if (this.uvMode == UVMappingMode.STRETCH_MATCH)
			{
				float num = 0f;
				for (int i = 0; i < newVertices.Count; i++)
				{
					this.vertices[i] = newVertices[i];
					if (newVertices[i].y == this.baseDepthOfTerrain)
					{
						this.uvs[i] = new Vector2(num, 0f);
					}
					else
					{
						num = Util.Percent(0f, (float)(newVertices.Count - 2), (float)i);
						this.uvs[i] = new Vector2(num, 1f);
					}
				}
			}
			else if (this.uvMode == UVMappingMode.TILING)
			{
				if (this.meshRenderer.sharedMaterial != null && this.meshRenderer.sharedMaterial.mainTexture != null)
				{
					vector.x = (float)this.pixelsPerUnit / (float)this.meshRenderer.sharedMaterial.mainTexture.width;
					vector.y = (float)this.pixelsPerUnit / (float)this.meshRenderer.sharedMaterial.mainTexture.height;
				}
				for (int j = 0; j < newVertices.Count; j++)
				{
					this.vertices[j] = newVertices[j];
					this.uvs[j] = new Vector2(newVertices[j].x * vector.x, newVertices[j].y * vector.y);
				}
			}
			for (int k = 0; k < newVertices.Count; k++)
			{
				this.trimVertices[k] = newVertices[k];
				if (newVertices[k].y == this.baseDepthOfTerrain && k - 1 >= 0)
				{
					this.trimVertices[k].y = newVertices[k - 1].y - 1f / vector.y;
					this.trimUvs[k] = new Vector2(newVertices[k].x * vector.x, 0f);
				}
				else
				{
					this.trimUvs[k] = new Vector2(newVertices[k].x * vector.x, 1f);
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00015630 File Offset: 0x00013830
		public void Setup(int numberOfPoints, bool createEdge, float startingY)
		{
			this.numberOfPoints = numberOfPoints;
			if (this.terrainDescription.keys.Length < 2)
			{
				this.terrainDescription.AddKey(0f, 0f);
				this.terrainDescription.AddKey(1f, 0f);
			}
			this.curveLength = this.terrainDescription.keys[this.terrainDescription.keys.Length - 1].time - this.terrainDescription.keys[0].time;
			this.length = this.curveLength * this.unitScale;
			this.createEdge = createEdge;
			this.edgeCollider = base.gameObject.GetComponent<EdgeCollider2D>();
			if (this.mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.mesh);
			}
			this.mesh = new Mesh();
			this.mesh.name = "Terrain Mesh";
			base.GetComponent<MeshFilter>().mesh = this.mesh;
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			this.verticeXspacing = this.length / (float)numberOfPoints;
			Transform transform = base.transform.Find(this.trimName);
			GameObject gameObject;
			if (transform == null)
			{
				gameObject = new GameObject(this.trimName);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.localRotation = default(Quaternion);
				gameObject.AddComponent<MeshFilter>();
				gameObject.AddComponent<MeshRenderer>();
			}
			else
			{
				gameObject = transform.gameObject;
			}
			if (this.trimMesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.trimMesh);
			}
			this.trimMesh = new Mesh();
			this.trimMesh.name = "Terrain Trim Mesh";
			gameObject.GetComponent<MeshFilter>().mesh = this.trimMesh;
			this.trimMeshRenderer = gameObject.GetComponent<MeshRenderer>();
			this.GenerateLandSegment(startingY);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00015838 File Offset: 0x00013A38
		public void RecalculateTerrainFromCurve()
		{
			List<Vector3> list = new List<Vector3>();
			if (this.terrainDescription.keys.Length < 2)
			{
				this.terrainDescription.AddKey(0f, 0f);
				this.terrainDescription.AddKey(1f, 0f);
			}
			this.curveLength = this.terrainDescription.keys[this.terrainDescription.keys.Length - 1].time - this.terrainDescription.keys[0].time;
			this.length = this.curveLength * this.unitScale;
			for (int i = 0; i < this.numberOfPoints; i++)
			{
				float num = Util.Percent(0f, (float)(this.numberOfPoints - 1), (float)i);
				list.Add(new Vector3(this.length * num, this.terrainDescription.Evaluate(Util.Lerp(this.terrainDescription.keys[0].time, this.curveLength, num)) * this.maxHeight, 0f));
			}
			this.BuildHills(list);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00015958 File Offset: 0x00013B58
		public void BuildHills(List<Vector3> peaks)
		{
			this.peakVertices = peaks;
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < this.peakVertices.Count; i++)
			{
				list.Add(this.peakVertices[i]);
				list.Add(new Vector3(this.peakVertices[i].x, this.baseDepthOfTerrain, 0f));
			}
			this.vertices = new Vector3[list.Count];
			this.uvs = new Vector2[list.Count];
			this.trimVertices = new Vector3[list.Count];
			this.trimUvs = new Vector2[list.Count];
			this.CalculateUVsAndVerts(list);
			this.mesh.vertices = this.vertices;
			this.mesh.uv = this.uvs;
			this.mesh.triangles = this.triangles;
			this.mesh.RecalculateBounds();
			this.mesh.RecalculateNormals();
			this.trimMesh.vertices = this.trimVertices;
			this.trimMesh.uv = this.trimUvs;
			this.trimMesh.triangles = this.triangles;
			this.trimMesh.RecalculateBounds();
			this.trimMesh.RecalculateNormals();
			this.trimMeshRenderer.transform.localPosition = new Vector3(0f, this.trimOffset, -0.01f);
			if (this.createEdge)
			{
				this.edgeCollider.points = this.GetEdge();
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00015ADC File Offset: 0x00013CDC
		public void GenerateLandSegment(float y)
		{
			this.peakVertices = new List<Vector3>();
			this.originalPeakVertices = new List<Vector3>();
			for (int i = 0; i < this.numberOfPoints; i++)
			{
				this.peakVertices.Add(new Vector3((float)i * this.verticeXspacing, y, 0f));
				this.originalPeakVertices.Add(new Vector3((float)i * this.verticeXspacing, 1f, 0f));
			}
			List<Vector3> list = new List<Vector3>();
			for (int j = 0; j < this.numberOfPoints; j++)
			{
				list.Add(this.peakVertices[j]);
				list.Add(new Vector3(this.peakVertices[j].x, -5f, 0f));
			}
			List<int> list2 = new List<int>();
			for (int k = list.Count - 1; k > 2; k -= 2)
			{
				list2.Add(k - 3);
				list2.Add(k - 1);
				list2.Add(k);
				list2.Add(k);
				list2.Add(k - 2);
				list2.Add(k - 3);
			}
			this.vertices = new Vector3[list.Count];
			this.uvs = new Vector2[list.Count];
			this.trimVertices = new Vector3[list.Count];
			this.trimUvs = new Vector2[list.Count];
			for (int l = 0; l < list.Count; l++)
			{
				this.vertices[l] = list[l];
				if (list[l].y == 0f)
				{
					this.uvs[l] = new Vector2(list[l].x, 0f);
				}
				else
				{
					this.uvs[l] = new Vector2(list[l].x, 1f);
				}
			}
			this.triangles = new int[list2.Count];
			for (int m = 0; m < list2.Count; m++)
			{
				this.triangles[m] = list2[m];
			}
			this.mesh.vertices = this.vertices;
			this.mesh.uv = this.uvs;
			this.mesh.triangles = this.triangles;
			this.mesh.RecalculateBounds();
			this.mesh.RecalculateNormals();
			if (this.createEdge)
			{
				this.edgeCollider.points = this.GetEdge();
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00015D60 File Offset: 0x00013F60
		public Vector2[] GetEdge()
		{
			Vector2[] array = new Vector2[this.peakVertices.Count];
			for (int i = 0; i < this.peakVertices.Count; i++)
			{
				array[i] = new Vector2(this.peakVertices[i].x, this.peakVertices[i].y);
			}
			return array;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00015DC3 File Offset: 0x00013FC3
		public Vector3 GetRightVertex()
		{
			return this.peakVertices[this.peakVertices.Count - 1];
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00015DDD File Offset: 0x00013FDD
		public Mesh GetMesh()
		{
			return this.mesh;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00015DE5 File Offset: 0x00013FE5
		public Material GetTrimMaterial()
		{
			if (this.trimMeshRenderer != null)
			{
				return this.trimMeshRenderer.sharedMaterial;
			}
			return null;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00015E02 File Offset: 0x00014002
		public Material GetMaterial()
		{
			if (this.meshRenderer != null)
			{
				return this.meshRenderer.sharedMaterial;
			}
			return null;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00015E1F File Offset: 0x0001401F
		public void SetTrimMaterial(Material newMaterial)
		{
			if (this.trimMeshRenderer != null)
			{
				this.trimMeshRenderer.sharedMaterial = newMaterial;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00015E3B File Offset: 0x0001403B
		public void SetMaterial(Material newMaterial)
		{
			if (this.meshRenderer != null)
			{
				this.meshRenderer.sharedMaterial = newMaterial;
			}
		}

		// Token: 0x0400042B RID: 1067
		[HideInInspector]
		public int numberOfPoints = 50;

		// Token: 0x0400042C RID: 1068
		[HideInInspector]
		public float length = 27f;

		// Token: 0x0400042D RID: 1069
		[HideInInspector]
		public bool createEdge;

		// Token: 0x0400042E RID: 1070
		[HideInInspector]
		public float baseDepthOfTerrain = -5f;

		// Token: 0x0400042F RID: 1071
		[HideInInspector]
		public float maxHeight = 11f;

		// Token: 0x04000430 RID: 1072
		[HideInInspector]
		public AnimationCurve terrainDescription = new AnimationCurve();

		// Token: 0x04000431 RID: 1073
		[HideInInspector]
		public UVMappingMode uvMode;

		// Token: 0x04000432 RID: 1074
		[HideInInspector]
		public int pixelsPerUnit = 32;

		// Token: 0x04000433 RID: 1075
		[HideInInspector]
		public float trimOffset = 0.1f;

		// Token: 0x04000434 RID: 1076
		[HideInInspector]
		public float unitScale = 25f;

		// Token: 0x04000435 RID: 1077
		private float curveLength;

		// Token: 0x04000436 RID: 1078
		private float verticeXspacing;

		// Token: 0x04000437 RID: 1079
		private List<Vector3> peakVertices;

		// Token: 0x04000438 RID: 1080
		private List<Vector3> originalPeakVertices;

		// Token: 0x04000439 RID: 1081
		private Vector3[] vertices;

		// Token: 0x0400043A RID: 1082
		private Vector2[] uvs;

		// Token: 0x0400043B RID: 1083
		private int[] triangles;

		// Token: 0x0400043C RID: 1084
		private Mesh mesh;

		// Token: 0x0400043D RID: 1085
		private MeshRenderer meshRenderer;

		// Token: 0x0400043E RID: 1086
		private Mesh trimMesh;

		// Token: 0x0400043F RID: 1087
		private MeshRenderer trimMeshRenderer;

		// Token: 0x04000440 RID: 1088
		private Vector2[] trimUvs;

		// Token: 0x04000441 RID: 1089
		private Vector3[] trimVertices;

		// Token: 0x04000442 RID: 1090
		private string trimName = "TerrainTrim";

		// Token: 0x04000443 RID: 1091
		private EdgeCollider2D edgeCollider;
	}
}
