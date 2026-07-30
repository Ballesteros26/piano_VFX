using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000128 RID: 296
	public sealed class MidiFileEqualityComparer : IEqualityComparer<MidiFile>
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0001E253 File Offset: 0x0001C453
		public MidiFileEqualityComparer()
			: this(null)
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001E25C File Offset: 0x0001C45C
		public MidiFileEqualityComparer(MidiFileEqualityCheckSettings settings)
		{
			this._settings = settings ?? new MidiFileEqualityCheckSettings();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001E274 File Offset: 0x0001C474
		public bool Equals(MidiFile x, MidiFile y)
		{
			string text;
			return MidiFile.Equals(x, y, this._settings, out text);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001E290 File Offset: 0x0001C490
		public int GetHashCode(MidiFile obj)
		{
			return obj.Chunks.Count.GetHashCode();
		}

		// Token: 0x0400084E RID: 2126
		private readonly MidiFileEqualityCheckSettings _settings;
	}
}
