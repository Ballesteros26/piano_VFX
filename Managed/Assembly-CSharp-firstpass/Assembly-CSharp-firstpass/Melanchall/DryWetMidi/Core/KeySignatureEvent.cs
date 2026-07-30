using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000145 RID: 325
	public sealed class KeySignatureEvent : MetaEvent
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0001EE43 File Offset: 0x0001D043
		public KeySignatureEvent()
			: base(MidiEventType.KeySignature)
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001EE4D File Offset: 0x0001D04D
		public KeySignatureEvent(sbyte key, byte scale)
			: this()
		{
			this.Key = key;
			this.Scale = scale;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001EE63 File Offset: 0x0001D063
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0001EE6B File Offset: 0x0001D06B
		public sbyte Key
		{
			get
			{
				return this._key;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", (int)value, -7, 7, "Key is out of range.");
				this._key = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001EE87 File Offset: 0x0001D087
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001EE8F File Offset: 0x0001D08F
		public byte Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", (int)value, 0, 1, "Scale is out of range.");
				this._scale = value;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001EEAA File Offset: 0x0001D0AA
		private int ProcessValue(int value, string property, int min, int max, InvalidMetaEventParameterValuePolicy policy)
		{
			if (value >= min && value <= max)
			{
				return value;
			}
			if (policy == InvalidMetaEventParameterValuePolicy.Abort)
			{
				throw new InvalidMetaEventParameterValueException(base.GetType(), property, value);
			}
			if (policy != InvalidMetaEventParameterValuePolicy.SnapToLimits)
			{
				return value;
			}
			return Math.Min(Math.Max(value, min), max);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			InvalidMetaEventParameterValuePolicy invalidMetaEventParameterValuePolicy = settings.InvalidMetaEventParameterValuePolicy;
			this.Key = (sbyte)this.ProcessValue((int)reader.ReadSByte(), "Key", -7, 7, invalidMetaEventParameterValuePolicy);
			this.Scale = (byte)this.ProcessValue((int)reader.ReadByte(), "Scale", 0, 1, invalidMetaEventParameterValuePolicy);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001EF2B File Offset: 0x0001D12B
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteSByte(this.Key);
			writer.WriteByte(this.Scale);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001EF45 File Offset: 0x0001D145
		protected override int GetContentSize(WritingSettings settings)
		{
			return 2;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001EF48 File Offset: 0x0001D148
		protected override MidiEvent CloneEvent()
		{
			return new KeySignatureEvent(this.Key, this.Scale);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001EF5B File Offset: 0x0001D15B
		public override string ToString()
		{
			return string.Format("Key Signature ({0}, {1})", this.Key, this.Scale);
		}

		// Token: 0x0400089D RID: 2205
		public const sbyte DefaultKey = 0;

		// Token: 0x0400089E RID: 2206
		public const byte DefaultScale = 0;

		// Token: 0x0400089F RID: 2207
		private const sbyte MinKey = -7;

		// Token: 0x040008A0 RID: 2208
		private const sbyte MaxKey = 7;

		// Token: 0x040008A1 RID: 2209
		private const byte MinScale = 0;

		// Token: 0x040008A2 RID: 2210
		private const byte MaxScale = 1;

		// Token: 0x040008A3 RID: 2211
		private sbyte _key;

		// Token: 0x040008A4 RID: 2212
		private byte _scale;
	}
}
