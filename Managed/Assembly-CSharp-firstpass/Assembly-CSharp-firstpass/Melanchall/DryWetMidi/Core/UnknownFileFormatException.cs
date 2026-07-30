using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200017C RID: 380
	public sealed class UnknownFileFormatException : MidiException
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00020848 File Offset: 0x0001EA48
		internal UnknownFileFormatException(ushort fileFormat)
			: base(string.Format("File format {0} is unknown.", fileFormat))
		{
			this.FileFormat = fileFormat;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00020867 File Offset: 0x0001EA67
		public ushort FileFormat { get; }
	}
}
