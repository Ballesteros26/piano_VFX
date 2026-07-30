using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public class TMP_TextInfo
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00020C30 File Offset: 0x0001EE30
		public TMP_TextInfo()
		{
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00020C8C File Offset: 0x0001EE8C
		public TMP_TextInfo(TMP_Text textComponent)
		{
			this.textComponent = textComponent;
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[4];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
			this.meshInfo[0].mesh = textComponent.mesh;
			this.materialCount = 1;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00020D70 File Offset: 0x0001EF70
		public void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public void ClearAllMeshInfo()
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(true);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		public void ResetVertexLayout(bool isVolumetric)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].ResizeMeshInfo(0, isVolumetric);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00020E0C File Offset: 0x0001F00C
		public void ClearUnusedVertices(MaterialReference[] materials)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				int num = 0;
				this.meshInfo[i].ClearUnusedVertices(num);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00020E40 File Offset: 0x0001F040
		public void ClearLineInfo()
		{
			if (this.lineInfo == null)
			{
				this.lineInfo = new TMP_LineInfo[2];
			}
			int num = this.lineInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].width = 0f;
				this.lineInfo[i].ascender = TMP_TextInfo.k_InfinityVectorNegative.x;
				this.lineInfo[i].descender = TMP_TextInfo.k_InfinityVectorPositive.x;
				this.lineInfo[i].lineExtents.min = TMP_TextInfo.k_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TMP_TextInfo.k_InfinityVectorNegative;
				this.lineInfo[i].maxAdvance = 0f;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00020F5C File Offset: 0x0001F15C
		internal void ClearPageInfo()
		{
			if (this.pageInfo == null)
			{
				this.pageInfo = new TMP_PageInfo[2];
			}
			int num = this.pageInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.pageInfo[i].firstCharacterIndex = 0;
				this.pageInfo[i].lastCharacterIndex = 0;
				this.pageInfo[i].ascender = -32767f;
				this.pageInfo[i].baseLine = 0f;
				this.pageInfo[i].descender = 32767f;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		public TMP_MeshInfo[] CopyMeshInfoVertexData()
		{
			if (this.m_CachedMeshInfo == null || this.m_CachedMeshInfo.Length != this.meshInfo.Length)
			{
				this.m_CachedMeshInfo = new TMP_MeshInfo[this.meshInfo.Length];
				for (int i = 0; i < this.m_CachedMeshInfo.Length; i++)
				{
					int num = this.meshInfo[i].vertices.Length;
					this.m_CachedMeshInfo[i].vertices = new Vector3[num];
					this.m_CachedMeshInfo[i].uvs0 = new Vector2[num];
					this.m_CachedMeshInfo[i].uvs2 = new Vector2[num];
					this.m_CachedMeshInfo[i].colors32 = new Color32[num];
				}
			}
			for (int j = 0; j < this.m_CachedMeshInfo.Length; j++)
			{
				int num2 = this.meshInfo[j].vertices.Length;
				if (this.m_CachedMeshInfo[j].vertices.Length != num2)
				{
					this.m_CachedMeshInfo[j].vertices = new Vector3[num2];
					this.m_CachedMeshInfo[j].uvs0 = new Vector2[num2];
					this.m_CachedMeshInfo[j].uvs2 = new Vector2[num2];
					this.m_CachedMeshInfo[j].colors32 = new Color32[num2];
				}
				Array.Copy(this.meshInfo[j].vertices, this.m_CachedMeshInfo[j].vertices, num2);
				Array.Copy(this.meshInfo[j].uvs0, this.m_CachedMeshInfo[j].uvs0, num2);
				Array.Copy(this.meshInfo[j].uvs2, this.m_CachedMeshInfo[j].uvs2, num2);
				Array.Copy(this.meshInfo[j].colors32, this.m_CachedMeshInfo[j].colors32, num2);
			}
			return this.m_CachedMeshInfo;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00021200 File Offset: 0x0001F400
		public static void Resize<T>(ref T[] array, int size)
		{
			int num = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0002122C File Offset: 0x0001F42C
		public static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			if (size == array.Length)
			{
				return;
			}
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x04000421 RID: 1057
		internal static Vector2 k_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x04000422 RID: 1058
		internal static Vector2 k_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x04000423 RID: 1059
		public TMP_Text textComponent;

		// Token: 0x04000424 RID: 1060
		public int characterCount;

		// Token: 0x04000425 RID: 1061
		public int spriteCount;

		// Token: 0x04000426 RID: 1062
		public int spaceCount;

		// Token: 0x04000427 RID: 1063
		public int wordCount;

		// Token: 0x04000428 RID: 1064
		public int linkCount;

		// Token: 0x04000429 RID: 1065
		public int lineCount;

		// Token: 0x0400042A RID: 1066
		public int pageCount;

		// Token: 0x0400042B RID: 1067
		public int materialCount;

		// Token: 0x0400042C RID: 1068
		public TMP_CharacterInfo[] characterInfo;

		// Token: 0x0400042D RID: 1069
		public TMP_WordInfo[] wordInfo;

		// Token: 0x0400042E RID: 1070
		public TMP_LinkInfo[] linkInfo;

		// Token: 0x0400042F RID: 1071
		public TMP_LineInfo[] lineInfo;

		// Token: 0x04000430 RID: 1072
		public TMP_PageInfo[] pageInfo;

		// Token: 0x04000431 RID: 1073
		public TMP_MeshInfo[] meshInfo;

		// Token: 0x04000432 RID: 1074
		private TMP_MeshInfo[] m_CachedMeshInfo;
	}
}
