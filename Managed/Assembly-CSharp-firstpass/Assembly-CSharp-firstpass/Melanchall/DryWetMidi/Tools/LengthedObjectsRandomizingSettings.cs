using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200005C RID: 92
	public abstract class LengthedObjectsRandomizingSettings<TObject> : RandomizingSettings<TObject> where TObject : ILengthedObject
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00009DAC File Offset: 0x00007FAC
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00009DB4 File Offset: 0x00007FB4
		public LengthedObjectTarget RandomizingTarget
		{
			get
			{
				return this._randomizingTarget;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<LengthedObjectTarget>("value", value);
				this._randomizingTarget = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00009DC8 File Offset: 0x00007FC8
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00009DD0 File Offset: 0x00007FD0
		public bool FixOppositeEnd { get; set; }

		// Token: 0x040000F1 RID: 241
		private LengthedObjectTarget _randomizingTarget;
	}
}
