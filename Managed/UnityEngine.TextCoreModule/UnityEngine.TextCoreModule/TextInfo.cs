using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000039 RID: 57
	internal class TextInfo
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00018A18 File Offset: 0x00016C18
		public TextInfo()
		{
			this.textElementInfo = new TextElementInfo[8];
			this.wordInfo = new WordInfo[16];
			this.linkInfo = new LinkInfo[0];
			this.lineInfo = new LineInfo[2];
			this.pageInfo = new PageInfo[4];
			this.meshInfo = new MeshInfo[1];
			this.materialCount = 0;
			this.isDirty = true;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00018A84 File Offset: 0x00016C84
		internal void Clear()
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

		// Token: 0x06000156 RID: 342 RVA: 0x00018AF0 File Offset: 0x00016CF0
		internal void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00018B28 File Offset: 0x00016D28
		internal void ClearLineInfo()
		{
			bool flag = this.lineInfo == null;
			if (flag)
			{
				this.lineInfo = new LineInfo[2];
			}
			for (int i = 0; i < this.lineInfo.Length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].width = 0f;
				this.lineInfo[i].ascender = TextInfo.s_InfinityVectorNegative.x;
				this.lineInfo[i].descender = TextInfo.s_InfinityVectorPositive.x;
				this.lineInfo[i].lineExtents.min = TextInfo.s_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TextInfo.s_InfinityVectorNegative;
				this.lineInfo[i].maxAdvance = 0f;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00018C50 File Offset: 0x00016E50
		internal static void Resize<T>(ref T[] array, int size)
		{
			int num = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00018C80 File Offset: 0x00016E80
		internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			bool flag = size == array.Length;
			if (!flag)
			{
				Array.Resize<T>(ref array, size);
			}
		}

		// Token: 0x04000318 RID: 792
		private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x04000319 RID: 793
		private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x0400031A RID: 794
		public int characterCount;

		// Token: 0x0400031B RID: 795
		public int spriteCount;

		// Token: 0x0400031C RID: 796
		public int spaceCount;

		// Token: 0x0400031D RID: 797
		public int wordCount;

		// Token: 0x0400031E RID: 798
		public int linkCount;

		// Token: 0x0400031F RID: 799
		public int lineCount;

		// Token: 0x04000320 RID: 800
		public int pageCount;

		// Token: 0x04000321 RID: 801
		public int materialCount;

		// Token: 0x04000322 RID: 802
		public TextElementInfo[] textElementInfo;

		// Token: 0x04000323 RID: 803
		public WordInfo[] wordInfo;

		// Token: 0x04000324 RID: 804
		public LinkInfo[] linkInfo;

		// Token: 0x04000325 RID: 805
		public LineInfo[] lineInfo;

		// Token: 0x04000326 RID: 806
		public PageInfo[] pageInfo;

		// Token: 0x04000327 RID: 807
		public MeshInfo[] meshInfo;

		// Token: 0x04000328 RID: 808
		public bool isDirty;
	}
}
