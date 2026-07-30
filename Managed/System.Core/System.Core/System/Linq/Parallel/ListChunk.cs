using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000209 RID: 521
	internal class ListChunk<TInputOutput> : IEnumerable<TInputOutput>, IEnumerable
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002B42E File Offset: 0x0002962E
		internal ListChunk(int size)
		{
			this._chunk = new TInputOutput[size];
			this._chunkCount = 0;
			this._tailChunk = this;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002B450 File Offset: 0x00029650
		internal void Add(TInputOutput e)
		{
			ListChunk<TInputOutput> listChunk = this._tailChunk;
			if (listChunk._chunkCount == listChunk._chunk.Length)
			{
				this._tailChunk = new ListChunk<TInputOutput>(listChunk._chunkCount * 2);
				listChunk = (listChunk._nextChunk = this._tailChunk);
			}
			TInputOutput[] chunk = listChunk._chunk;
			ListChunk<TInputOutput> listChunk2 = listChunk;
			int chunkCount = listChunk2._chunkCount;
			listChunk2._chunkCount = chunkCount + 1;
			chunk[chunkCount] = e;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002B4B4 File Offset: 0x000296B4
		internal ListChunk<TInputOutput> Next
		{
			get
			{
				return this._nextChunk;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002B4BC File Offset: 0x000296BC
		internal int Count
		{
			get
			{
				return this._chunkCount;
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002B4C4 File Offset: 0x000296C4
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			for (ListChunk<TInputOutput> curr = this; curr != null; curr = curr._nextChunk)
			{
				int num;
				for (int i = 0; i < curr._chunkCount; i = num + 1)
				{
					yield return curr._chunk[i];
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0000877A File Offset: 0x0000697A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TInputOutput>)this).GetEnumerator();
		}

		// Token: 0x04000811 RID: 2065
		internal TInputOutput[] _chunk;

		// Token: 0x04000812 RID: 2066
		private int _chunkCount;

		// Token: 0x04000813 RID: 2067
		private ListChunk<TInputOutput> _nextChunk;

		// Token: 0x04000814 RID: 2068
		private ListChunk<TInputOutput> _tailChunk;
	}
}
