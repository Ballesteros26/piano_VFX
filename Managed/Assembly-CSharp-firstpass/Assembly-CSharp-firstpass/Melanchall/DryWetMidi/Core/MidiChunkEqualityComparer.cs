using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000122 RID: 290
	public sealed class MidiChunkEqualityComparer : IEqualityComparer<MidiChunk>
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0001DC11 File Offset: 0x0001BE11
		public MidiChunkEqualityComparer()
			: this(null)
		{
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001DC1A File Offset: 0x0001BE1A
		public MidiChunkEqualityComparer(MidiChunkEqualityCheckSettings settings)
		{
			this._settings = settings ?? new MidiChunkEqualityCheckSettings();
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public bool Equals(MidiChunk x, MidiChunk y)
		{
			string text;
			return MidiChunk.Equals(x, y, this._settings, out text);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001DC50 File Offset: 0x0001BE50
		public int GetHashCode(MidiChunk obj)
		{
			return obj.ChunkId.GetHashCode();
		}

		// Token: 0x04000847 RID: 2119
		private readonly MidiChunkEqualityCheckSettings _settings;
	}
}
