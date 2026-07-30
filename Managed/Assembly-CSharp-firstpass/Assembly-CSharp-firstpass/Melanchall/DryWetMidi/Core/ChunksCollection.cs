using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011E RID: 286
	public sealed class ChunksCollection : IEnumerable<MidiChunk>, IEnumerable
	{
		// Token: 0x17000115 RID: 277
		public MidiChunk this[int index]
		{
			get
			{
				ThrowIfArgument.IsInvalidIndex("index", index, this._chunks.Count);
				return this._chunks[index];
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				ThrowIfArgument.IsInvalidIndex("index", index, this._chunks.Count);
				this._chunks[index] = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001D724 File Offset: 0x0001B924
		public int Count
		{
			get
			{
				return this._chunks.Count;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001D731 File Offset: 0x0001B931
		public void Add(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			this._chunks.Add(chunk);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001D74A File Offset: 0x0001B94A
		public void AddRange(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			this._chunks.AddRange(chunks.Where((MidiChunk c) => c != null));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001D787 File Offset: 0x0001B987
		public void Insert(int index, MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			ThrowIfArgument.IsInvalidIndex("index", index, this._chunks.Count);
			this._chunks.Insert(index, chunk);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public void InsertRange(int index, IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			ThrowIfArgument.IsInvalidIndex("index", index, this._chunks.Count);
			this._chunks.InsertRange(index, chunks.Where((MidiChunk c) => c != null));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001D817 File Offset: 0x0001BA17
		public bool Remove(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			return this._chunks.Remove(chunk);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001D830 File Offset: 0x0001BA30
		public void RemoveAt(int index)
		{
			ThrowIfArgument.IsInvalidIndex("index", index, this._chunks.Count);
			this._chunks.RemoveAt(index);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001D854 File Offset: 0x0001BA54
		public int RemoveAll(Predicate<MidiChunk> match)
		{
			ThrowIfArgument.IsNull("match", match);
			return this._chunks.RemoveAll(match);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001D86D File Offset: 0x0001BA6D
		public int IndexOf(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			return this._chunks.IndexOf(chunk);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001D886 File Offset: 0x0001BA86
		public void Clear()
		{
			this._chunks.Clear();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001D893 File Offset: 0x0001BA93
		public IEnumerator<MidiChunk> GetEnumerator()
		{
			return this._chunks.GetEnumerator();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001D893 File Offset: 0x0001BA93
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._chunks.GetEnumerator();
		}

		// Token: 0x04000844 RID: 2116
		private readonly List<MidiChunk> _chunks = new List<MidiChunk>();
	}
}
