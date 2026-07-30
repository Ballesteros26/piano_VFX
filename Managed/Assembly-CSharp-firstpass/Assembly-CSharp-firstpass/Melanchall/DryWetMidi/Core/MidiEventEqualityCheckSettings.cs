using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000124 RID: 292
	public sealed class MidiEventEqualityCheckSettings
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001E022 File Offset: 0x0001C222
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0001E02A File Offset: 0x0001C22A
		public bool CompareDeltaTimes { get; set; } = true;

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001E033 File Offset: 0x0001C233
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001E03B File Offset: 0x0001C23B
		public StringComparison TextComparison
		{
			get
			{
				return this._textComparison;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<StringComparison>("value", value);
				this._textComparison = value;
			}
		}

		// Token: 0x04000849 RID: 2121
		private StringComparison _textComparison;
	}
}
