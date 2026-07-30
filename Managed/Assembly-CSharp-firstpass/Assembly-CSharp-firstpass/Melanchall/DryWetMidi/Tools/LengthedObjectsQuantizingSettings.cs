using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200004B RID: 75
	public abstract class LengthedObjectsQuantizingSettings<TObject> : QuantizingSettings<TObject> where TObject : ILengthedObject
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000095F0 File Offset: 0x000077F0
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000095F8 File Offset: 0x000077F8
		public TimeSpanType LengthType
		{
			get
			{
				return this._lengthType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("value", value);
				this._lengthType = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000960C File Offset: 0x0000780C
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009614 File Offset: 0x00007814
		public LengthedObjectTarget QuantizingTarget
		{
			get
			{
				return this._quantizingTarget;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("value", value);
				this._quantizingTarget = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009628 File Offset: 0x00007828
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009630 File Offset: 0x00007830
		public QuantizingBeyondZeroPolicy QuantizingBeyondZeroPolicy
		{
			get
			{
				return this._quantizingBeyondZeroPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<QuantizingBeyondZeroPolicy>("value", value);
				this._quantizingBeyondZeroPolicy = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009644 File Offset: 0x00007844
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000964C File Offset: 0x0000784C
		public QuantizingBeyondFixedEndPolicy QuantizingBeyondFixedEndPolicy
		{
			get
			{
				return this._quantizingBeyondFixedEndPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<QuantizingBeyondFixedEndPolicy>("value", value);
				this._quantizingBeyondFixedEndPolicy = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009660 File Offset: 0x00007860
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00009668 File Offset: 0x00007868
		public bool FixOppositeEnd { get; set; }

		// Token: 0x040000DE RID: 222
		private TimeSpanType _lengthType = TimeSpanType.Midi;

		// Token: 0x040000DF RID: 223
		private LengthedObjectTarget _quantizingTarget;

		// Token: 0x040000E0 RID: 224
		private QuantizingBeyondZeroPolicy _quantizingBeyondZeroPolicy;

		// Token: 0x040000E1 RID: 225
		private QuantizingBeyondFixedEndPolicy _quantizingBeyondFixedEndPolicy;
	}
}
