using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000016 RID: 22
	internal sealed class CsvRecord
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004A14 File Offset: 0x00002C14
		public CsvRecord(int lineNumber, int linesCount, string[] values)
		{
			this.LineNumber = lineNumber;
			this.LinesCount = linesCount;
			this.Values = values;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004A31 File Offset: 0x00002C31
		public int LineNumber { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004A39 File Offset: 0x00002C39
		public int LinesCount { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004A41 File Offset: 0x00002C41
		public string[] Values { get; }
	}
}
