using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014E RID: 334
	public sealed class SmpteOffsetEvent : MetaEvent
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0001F25E File Offset: 0x0001D45E
		public SmpteOffsetEvent()
			: base(MidiEventType.SmpteOffset)
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001F273 File Offset: 0x0001D473
		public SmpteOffsetEvent(SmpteFormat format, byte hours, byte minutes, byte seconds, byte frames, byte subFrames)
			: this()
		{
			this.Format = format;
			this.Hours = hours;
			this.Minutes = minutes;
			this.Seconds = seconds;
			this.Frames = frames;
			this.SubFrames = subFrames;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0001F2B5 File Offset: 0x0001D4B5
		public SmpteFormat Format
		{
			get
			{
				return this._smpteData.Format;
			}
			set
			{
				this._smpteData.Format = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001F2C3 File Offset: 0x0001D4C3
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0001F2D0 File Offset: 0x0001D4D0
		public byte Hours
		{
			get
			{
				return this._smpteData.Hours;
			}
			set
			{
				this._smpteData.Hours = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001F2DE File Offset: 0x0001D4DE
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		public byte Minutes
		{
			get
			{
				return this._smpteData.Minutes;
			}
			set
			{
				this._smpteData.Minutes = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001F2F9 File Offset: 0x0001D4F9
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0001F306 File Offset: 0x0001D506
		public byte Seconds
		{
			get
			{
				return this._smpteData.Seconds;
			}
			set
			{
				this._smpteData.Seconds = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0001F314 File Offset: 0x0001D514
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0001F321 File Offset: 0x0001D521
		public byte Frames
		{
			get
			{
				return this._smpteData.Frames;
			}
			set
			{
				this._smpteData.Frames = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001F32F File Offset: 0x0001D52F
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0001F33C File Offset: 0x0001D53C
		public byte SubFrames
		{
			get
			{
				return this._smpteData.SubFrames;
			}
			set
			{
				this._smpteData.SubFrames = value;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001F34A File Offset: 0x0001D54A
		private byte ProcessValue(byte value, string property, byte max, InvalidMetaEventParameterValuePolicy policy)
		{
			if (value <= max)
			{
				return value;
			}
			if (policy == InvalidMetaEventParameterValuePolicy.Abort)
			{
				throw new InvalidMetaEventParameterValueException(base.GetType(), property, (int)value);
			}
			if (policy != InvalidMetaEventParameterValuePolicy.SnapToLimits)
			{
				return value;
			}
			return Math.Min(value, max);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001F374 File Offset: 0x0001D574
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			this._smpteData = SmpteData.Read(new Func<byte>(reader.ReadByte), (byte value, string propertyName, byte max) => this.ProcessValue(value, propertyName, max, settings.InvalidMetaEventParameterValuePolicy));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			this._smpteData.Write(new Action<byte>(writer.WriteByte));
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001F3D1 File Offset: 0x0001D5D1
		protected override int GetContentSize(WritingSettings settings)
		{
			return 5;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		protected override MidiEvent CloneEvent()
		{
			return new SmpteOffsetEvent(this.Format, this.Hours, this.Minutes, this.Seconds, this.Frames, this.SubFrames);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001F400 File Offset: 0x0001D600
		public override string ToString()
		{
			return string.Format("SMPTE Offset ({0}, {1}:{2}:{3}:{4}:{5})", new object[] { this.Format, this.Hours, this.Minutes, this.Seconds, this.Frames, this.SubFrames });
		}

		// Token: 0x040008AA RID: 2218
		private SmpteData _smpteData = new SmpteData();
	}
}
