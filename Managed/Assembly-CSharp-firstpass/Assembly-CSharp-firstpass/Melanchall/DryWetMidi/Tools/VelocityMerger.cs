using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000043 RID: 67
	internal abstract class VelocityMerger
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008FC3 File Offset: 0x000071C3
		public virtual SevenBitNumber Velocity
		{
			get
			{
				return this._velocity;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008F74 File Offset: 0x00007174
		public virtual void Initialize(SevenBitNumber velocity)
		{
			this._velocity = velocity;
		}

		// Token: 0x0600019A RID: 410
		public abstract void Merge(SevenBitNumber velocity);

		// Token: 0x040000CD RID: 205
		protected SevenBitNumber _velocity;
	}
}
