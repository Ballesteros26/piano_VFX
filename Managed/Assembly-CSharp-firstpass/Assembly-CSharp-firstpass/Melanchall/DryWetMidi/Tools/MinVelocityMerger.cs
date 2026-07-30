using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000042 RID: 66
	internal sealed class MinVelocityMerger : VelocityMerger
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00008FA0 File Offset: 0x000071A0
		public override void Merge(SevenBitNumber velocity)
		{
			this._velocity = (SevenBitNumber)Math.Min(this._velocity, velocity);
		}
	}
}
