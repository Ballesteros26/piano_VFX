using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000040 RID: 64
	internal sealed class LastVelocityMerger : VelocityMerger
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00008F74 File Offset: 0x00007174
		public override void Merge(SevenBitNumber velocity)
		{
			this._velocity = velocity;
		}
	}
}
