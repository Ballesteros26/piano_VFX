using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000047 RID: 71
	public abstract class QuantizingSettings<TObject>
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000921F File Offset: 0x0000741F
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009227 File Offset: 0x00007427
		public TimeSpanType DistanceCalculationType
		{
			get
			{
				return this._distanceCalculationType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("value", value);
				this._distanceCalculationType = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000923B File Offset: 0x0000743B
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009244 File Offset: 0x00007444
		public double QuantizingLevel
		{
			get
			{
				return this._quantizingLevel;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 0.0, 1.0, string.Format("Value is out of [{0}; {1}] range.", 0.0, 1.0));
				this._quantizingLevel = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000929B File Offset: 0x0000749B
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000092A3 File Offset: 0x000074A3
		public Predicate<TObject> Filter { get; set; }

		// Token: 0x040000D9 RID: 217
		private const double NoQuantizingLevel = 0.0;

		// Token: 0x040000DA RID: 218
		private const double FullQuantizingLevel = 1.0;

		// Token: 0x040000DB RID: 219
		private TimeSpanType _distanceCalculationType = TimeSpanType.Midi;

		// Token: 0x040000DC RID: 220
		private double _quantizingLevel = 1.0;
	}
}
