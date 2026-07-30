using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200018A RID: 394
	public sealed class ReaderSettings
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002175A File Offset: 0x0001F95A
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00021762 File Offset: 0x0001F962
		public int NonSeekableStreamBufferSize
		{
			get
			{
				return this._nonSeekableStreamBufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				this._nonSeekableStreamBufferSize = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002177B File Offset: 0x0001F97B
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00021783 File Offset: 0x0001F983
		public int NonSeekableStreamIncrementalBytesReadingThreshold
		{
			get
			{
				return this._nonSeekableStreamIncrementalBytesReadingThreshold;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Value is negative.");
				this._nonSeekableStreamIncrementalBytesReadingThreshold = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0002179C File Offset: 0x0001F99C
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x000217A4 File Offset: 0x0001F9A4
		public int NonSeekableStreamIncrementalBytesReadingStep
		{
			get
			{
				return this._nonSeekableStreamIncrementalBytesReadingStep;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				this._nonSeekableStreamIncrementalBytesReadingStep = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000217BD File Offset: 0x0001F9BD
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000217C5 File Offset: 0x0001F9C5
		public bool ReadFromMemory { get; set; }

		// Token: 0x04000913 RID: 2323
		private int _nonSeekableStreamBufferSize = 1024;

		// Token: 0x04000914 RID: 2324
		private int _nonSeekableStreamIncrementalBytesReadingThreshold = 16384;

		// Token: 0x04000915 RID: 2325
		private int _nonSeekableStreamIncrementalBytesReadingStep = 2048;
	}
}
