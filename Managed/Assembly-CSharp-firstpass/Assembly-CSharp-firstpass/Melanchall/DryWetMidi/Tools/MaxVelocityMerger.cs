using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000041 RID: 65
	internal sealed class MaxVelocityMerger : VelocityMerger
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00008F7D File Offset: 0x0000717D
		public override void Merge(SevenBitNumber velocity)
		{
			this._velocity = (SevenBitNumber)Math.Max(this._velocity, velocity);
		}
	}
}
