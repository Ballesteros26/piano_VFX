using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000019 RID: 25
	public enum TypeAffinity
	{
		// Token: 0x04000080 RID: 128
		Uninitialized,
		// Token: 0x04000081 RID: 129
		Int64,
		// Token: 0x04000082 RID: 130
		Double,
		// Token: 0x04000083 RID: 131
		Text,
		// Token: 0x04000084 RID: 132
		Blob,
		// Token: 0x04000085 RID: 133
		Null,
		// Token: 0x04000086 RID: 134
		DateTime = 10,
		// Token: 0x04000087 RID: 135
		None
	}
}
