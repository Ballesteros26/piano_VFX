using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200002F RID: 47
	internal class CodePointIndexer
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004F30 File Offset: 0x00003130
		public static Array CompressArray(Array source, Type type, CodePointIndexer indexer)
		{
			int num = 0;
			for (int i = 0; i < indexer.ranges.Length; i++)
			{
				num += indexer.ranges[i].Count;
			}
			Array array = Array.CreateInstance(type, num);
			for (int j = 0; j < indexer.ranges.Length; j++)
			{
				Array.Copy(source, indexer.ranges[j].Start, array, indexer.ranges[j].IndexStart, indexer.ranges[j].Count);
			}
			return array;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004FBC File Offset: 0x000031BC
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i == 0) ? 0 : (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count));
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005074 File Offset: 0x00003274
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000050F0 File Offset: 0x000032F0
		public int ToCodePoint(int i)
		{
			for (int j = 0; j < this.ranges.Length; j++)
			{
				if (i < this.ranges[j].IndexStart)
				{
					return this.defaultCP;
				}
				if (i < this.ranges[j].IndexEnd)
				{
					return i - this.ranges[j].IndexStart + this.ranges[j].Start;
				}
			}
			return this.defaultCP;
		}

		// Token: 0x040003CD RID: 973
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x040003CE RID: 974
		public readonly int TotalCount;

		// Token: 0x040003CF RID: 975
		private int defaultIndex;

		// Token: 0x040003D0 RID: 976
		private int defaultCP;

		// Token: 0x02000030 RID: 48
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x060000FF RID: 255 RVA: 0x0000516B File Offset: 0x0000336B
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x040003D1 RID: 977
			public readonly int Start;

			// Token: 0x040003D2 RID: 978
			public readonly int End;

			// Token: 0x040003D3 RID: 979
			public readonly int Count;

			// Token: 0x040003D4 RID: 980
			public readonly int IndexStart;

			// Token: 0x040003D5 RID: 981
			public readonly int IndexEnd;
		}
	}
}
