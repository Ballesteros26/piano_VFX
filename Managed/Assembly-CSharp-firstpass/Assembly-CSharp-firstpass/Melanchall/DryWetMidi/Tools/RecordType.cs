using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000021 RID: 33
	internal enum RecordType
	{
		// Token: 0x04000096 RID: 150
		Header,
		// Token: 0x04000097 RID: 151
		TrackChunkStart,
		// Token: 0x04000098 RID: 152
		TrackChunkEnd,
		// Token: 0x04000099 RID: 153
		FileEnd,
		// Token: 0x0400009A RID: 154
		Event,
		// Token: 0x0400009B RID: 155
		Note
	}
}
