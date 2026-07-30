using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200003E RID: 62
	internal sealed class AverageVelocityMerger : VelocityMerger
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008EFB File Offset: 0x000070FB
		public override SevenBitNumber Velocity
		{
			get
			{
				return (SevenBitNumber)((byte)MathUtilities.Round(this._velocities.Average((SevenBitNumber v) => (int)v)));
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008F32 File Offset: 0x00007132
		public override void Initialize(SevenBitNumber velocity)
		{
			this._velocities.Clear();
			this._velocities.Add(velocity);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008F4B File Offset: 0x0000714B
		public override void Merge(SevenBitNumber velocity)
		{
			this._velocities.Add(velocity);
		}

		// Token: 0x040000CC RID: 204
		private readonly List<SevenBitNumber> _velocities = new List<SevenBitNumber>();
	}
}
