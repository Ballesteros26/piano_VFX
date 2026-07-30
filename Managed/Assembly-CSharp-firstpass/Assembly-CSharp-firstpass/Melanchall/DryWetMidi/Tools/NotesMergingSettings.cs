using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200003D RID: 61
	public sealed class NotesMergingSettings
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008E8D File Offset: 0x0000708D
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00008E95 File Offset: 0x00007095
		public VelocityMergingPolicy VelocityMergingPolicy
		{
			get
			{
				return this._velocityMergingPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<VelocityMergingPolicy>("value", value);
				this._velocityMergingPolicy = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008EA9 File Offset: 0x000070A9
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00008EB1 File Offset: 0x000070B1
		public VelocityMergingPolicy OffVelocityMergingPolicy
		{
			get
			{
				return this._offVelocityMergingPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<VelocityMergingPolicy>("value", value);
				this._offVelocityMergingPolicy = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008EC5 File Offset: 0x000070C5
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00008ECD File Offset: 0x000070CD
		public ITimeSpan Tolerance
		{
			get
			{
				return this._tolerance;
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				this._tolerance = value;
			}
		}

		// Token: 0x040000C9 RID: 201
		private VelocityMergingPolicy _velocityMergingPolicy;

		// Token: 0x040000CA RID: 202
		private VelocityMergingPolicy _offVelocityMergingPolicy = VelocityMergingPolicy.Last;

		// Token: 0x040000CB RID: 203
		private ITimeSpan _tolerance = new MidiTimeSpan();
	}
}
