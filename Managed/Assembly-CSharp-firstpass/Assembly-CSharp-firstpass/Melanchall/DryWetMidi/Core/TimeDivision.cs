using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000197 RID: 407
	public abstract class TimeDivision
	{
		// Token: 0x060009E6 RID: 2534
		internal abstract short ToInt16();

		// Token: 0x060009E7 RID: 2535
		public abstract TimeDivision Clone();
	}
}
