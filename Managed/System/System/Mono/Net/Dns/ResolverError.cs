using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000A0 RID: 160
	internal enum ResolverError
	{
		// Token: 0x040008F8 RID: 2296
		NoError,
		// Token: 0x040008F9 RID: 2297
		FormatError,
		// Token: 0x040008FA RID: 2298
		ServerFailure,
		// Token: 0x040008FB RID: 2299
		NameError,
		// Token: 0x040008FC RID: 2300
		NotImplemented,
		// Token: 0x040008FD RID: 2301
		Refused,
		// Token: 0x040008FE RID: 2302
		ResponseHeaderError,
		// Token: 0x040008FF RID: 2303
		ResponseFormatError,
		// Token: 0x04000900 RID: 2304
		Timeout
	}
}
