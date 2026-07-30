using System;
using System.Text;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x020001A2 RID: 418
	public class WritingSettings
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002240F File Offset: 0x0002060F
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00022417 File Offset: 0x00020617
		public CompressionPolicy CompressionPolicy
		{
			get
			{
				return this._compressionPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<CompressionPolicy>("value", value);
				this._compressionPolicy = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002242B File Offset: 0x0002062B
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00022433 File Offset: 0x00020633
		public EventTypesCollection CustomMetaEventTypes { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002243C File Offset: 0x0002063C
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00022444 File Offset: 0x00020644
		public Encoding TextEncoding { get; set; } = SmfConstants.DefaultTextEncoding;

		// Token: 0x0400096C RID: 2412
		private CompressionPolicy _compressionPolicy;
	}
}
