using System;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B9 RID: 185
	public interface ITimeSpan : IComparable
	{
		// Token: 0x06000437 RID: 1079
		ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode);

		// Token: 0x06000438 RID: 1080
		ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode);

		// Token: 0x06000439 RID: 1081
		ITimeSpan Multiply(double multiplier);

		// Token: 0x0600043A RID: 1082
		ITimeSpan Divide(double divisor);

		// Token: 0x0600043B RID: 1083
		ITimeSpan Clone();
	}
}
