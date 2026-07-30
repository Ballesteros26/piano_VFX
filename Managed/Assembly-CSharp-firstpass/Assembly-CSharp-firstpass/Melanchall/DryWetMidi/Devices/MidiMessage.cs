using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000110 RID: 272
	internal enum MidiMessage
	{
		// Token: 0x04000822 RID: 2082
		MIM_CLOSE = 962,
		// Token: 0x04000823 RID: 2083
		MIM_DATA,
		// Token: 0x04000824 RID: 2084
		MIM_ERROR = 965,
		// Token: 0x04000825 RID: 2085
		MIM_LONGDATA = 964,
		// Token: 0x04000826 RID: 2086
		MIM_LONGERROR = 966,
		// Token: 0x04000827 RID: 2087
		MIM_MOREDATA = 972,
		// Token: 0x04000828 RID: 2088
		MIM_OPEN = 961,
		// Token: 0x04000829 RID: 2089
		MOM_CLOSE = 968,
		// Token: 0x0400082A RID: 2090
		MOM_DONE,
		// Token: 0x0400082B RID: 2091
		MOM_OPEN = 967,
		// Token: 0x0400082C RID: 2092
		MOM_POSITIONCB = 970
	}
}
