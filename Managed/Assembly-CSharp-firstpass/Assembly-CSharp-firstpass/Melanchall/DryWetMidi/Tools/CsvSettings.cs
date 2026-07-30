using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000017 RID: 23
	public sealed class CsvSettings
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004A49 File Offset: 0x00002C49
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004A51 File Offset: 0x00002C51
		public char CsvDelimiter { get; set; } = ',';

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004A5A File Offset: 0x00002C5A
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004A62 File Offset: 0x00002C62
		public int IoBufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Buffer size is zero or negative.");
				this._bufferSize = value;
			}
		}

		// Token: 0x04000083 RID: 131
		private int _bufferSize = 1024;
	}
}
