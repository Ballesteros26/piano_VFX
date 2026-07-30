using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000020 RID: 32
	internal sealed class Record
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005F06 File Offset: 0x00004106
		public Record(int lineNumber, int? trackNumber, ITimeSpan time, string recordType, string[] parameters)
		{
			this.LineNumber = lineNumber;
			this.TrackNumber = trackNumber;
			this.Time = time;
			this.RecordType = recordType;
			this.Parameters = parameters;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005F33 File Offset: 0x00004133
		public int LineNumber { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005F3B File Offset: 0x0000413B
		public int? TrackNumber { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005F43 File Offset: 0x00004143
		public ITimeSpan Time { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005F4B File Offset: 0x0000414B
		public string RecordType { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005F53 File Offset: 0x00004153
		public string[] Parameters { get; }
	}
}
