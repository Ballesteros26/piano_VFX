using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200002F RID: 47
	public sealed class NoteCsvConversionSettings
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007358 File Offset: 0x00005558
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00007360 File Offset: 0x00005560
		public TimeSpanType TimeType
		{
			get
			{
				return this._timeType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("value", value);
				this._timeType = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007374 File Offset: 0x00005574
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000737C File Offset: 0x0000557C
		public TimeSpanType NoteLengthType
		{
			get
			{
				return this._noteLengthType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("value", value);
				this._noteLengthType = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007390 File Offset: 0x00005590
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00007398 File Offset: 0x00005598
		public NoteNumberFormat NoteNumberFormat
		{
			get
			{
				return this._noteNumberFormat;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<NoteNumberFormat>("value", value);
				this._noteNumberFormat = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000073AC File Offset: 0x000055AC
		public CsvSettings CsvSettings { get; } = new CsvSettings();

		// Token: 0x040000B7 RID: 183
		private TimeSpanType _timeType = TimeSpanType.Midi;

		// Token: 0x040000B8 RID: 184
		private TimeSpanType _noteLengthType = TimeSpanType.Midi;

		// Token: 0x040000B9 RID: 185
		private NoteNumberFormat _noteNumberFormat;
	}
}
