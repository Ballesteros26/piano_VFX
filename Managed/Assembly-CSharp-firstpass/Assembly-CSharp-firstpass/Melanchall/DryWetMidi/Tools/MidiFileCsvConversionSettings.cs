using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000024 RID: 36
	public sealed class MidiFileCsvConversionSettings
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000604E File Offset: 0x0000424E
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006056 File Offset: 0x00004256
		public MidiFileCsvLayout CsvLayout
		{
			get
			{
				return this._csvLayout;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<MidiFileCsvLayout>("value", value);
				this._csvLayout = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000606A File Offset: 0x0000426A
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006072 File Offset: 0x00004272
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006086 File Offset: 0x00004286
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000608E File Offset: 0x0000428E
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000060A2 File Offset: 0x000042A2
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000060AA File Offset: 0x000042AA
		public NoteFormat NoteFormat
		{
			get
			{
				return this._noteFormat;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<NoteFormat>("value", value);
				this._noteFormat = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000060BE File Offset: 0x000042BE
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000060C6 File Offset: 0x000042C6
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000060DA File Offset: 0x000042DA
		public CsvSettings CsvSettings { get; } = new CsvSettings();

		// Token: 0x040000A7 RID: 167
		private MidiFileCsvLayout _csvLayout;

		// Token: 0x040000A8 RID: 168
		private TimeSpanType _timeType = TimeSpanType.Midi;

		// Token: 0x040000A9 RID: 169
		private TimeSpanType _noteLengthType = TimeSpanType.Midi;

		// Token: 0x040000AA RID: 170
		private NoteFormat _noteFormat = NoteFormat.Events;

		// Token: 0x040000AB RID: 171
		private NoteNumberFormat _noteNumberFormat;
	}
}
