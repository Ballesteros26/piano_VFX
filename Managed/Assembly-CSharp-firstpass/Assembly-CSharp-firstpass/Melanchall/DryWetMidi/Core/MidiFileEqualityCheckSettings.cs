using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000127 RID: 295
	public sealed class MidiFileEqualityCheckSettings
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001E217 File Offset: 0x0001C417
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0001E21F File Offset: 0x0001C41F
		public bool CompareOriginalFormat { get; set; } = true;

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001E228 File Offset: 0x0001C428
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0001E230 File Offset: 0x0001C430
		public MidiChunkEqualityCheckSettings ChunkEqualityCheckSettings { get; set; } = new MidiChunkEqualityCheckSettings();
	}
}
