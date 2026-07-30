using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000365 RID: 869
	internal class SequencePointList
	{
		// Token: 0x06002736 RID: 10038 RVA: 0x0008B7D8 File Offset: 0x000899D8
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0008B7E7 File Offset: 0x000899E7
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0008B7F0 File Offset: 0x000899F0
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0008B830 File Offset: 0x00089A30
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0008B870 File Offset: 0x00089A70
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0008B8B0 File Offset: 0x00089AB0
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0008B8F0 File Offset: 0x00089AF0
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x0008B92F File Offset: 0x00089B2F
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0008B942 File Offset: 0x00089B42
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0008B95C File Offset: 0x00089B5C
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0008B96F File Offset: 0x00089B6F
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0008B98C File Offset: 0x00089B8C
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] array = new SequencePoint[this.count + 10];
				Array.Copy(this.points, array, this.points.Length);
				this.points = array;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x04001454 RID: 5204
		private ISymbolDocumentWriter doc;

		// Token: 0x04001455 RID: 5205
		private SequencePoint[] points;

		// Token: 0x04001456 RID: 5206
		private int count;

		// Token: 0x04001457 RID: 5207
		private const int arrayGrow = 10;
	}
}
