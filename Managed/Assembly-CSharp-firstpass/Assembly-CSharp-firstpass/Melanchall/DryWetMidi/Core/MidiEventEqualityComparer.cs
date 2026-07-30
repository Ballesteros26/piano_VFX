using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000125 RID: 293
	public sealed class MidiEventEqualityComparer : IEqualityComparer<MidiEvent>
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001E05E File Offset: 0x0001C25E
		public MidiEventEqualityComparer()
			: this(null)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001E067 File Offset: 0x0001C267
		public MidiEventEqualityComparer(MidiEventEqualityCheckSettings settings)
		{
			this._settings = settings ?? new MidiEventEqualityCheckSettings();
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001E080 File Offset: 0x0001C280
		public bool Equals(MidiEvent x, MidiEvent y)
		{
			string text;
			return MidiEvent.Equals(x, y, this._settings, out text);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001E09C File Offset: 0x0001C29C
		public int GetHashCode(MidiEvent obj)
		{
			return obj.EventType.GetHashCode();
		}

		// Token: 0x0400084B RID: 2123
		private readonly MidiEventEqualityCheckSettings _settings;
	}
}
