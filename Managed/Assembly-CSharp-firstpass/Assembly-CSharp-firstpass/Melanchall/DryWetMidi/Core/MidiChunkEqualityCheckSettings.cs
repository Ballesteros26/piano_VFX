using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000121 RID: 289
	public sealed class MidiChunkEqualityCheckSettings
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001DBED File Offset: 0x0001BDED
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001DBF5 File Offset: 0x0001BDF5
		public MidiEventEqualityCheckSettings EventEqualityCheckSettings { get; set; } = new MidiEventEqualityCheckSettings();
	}
}
