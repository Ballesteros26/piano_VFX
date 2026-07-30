using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001C6 RID: 454
	internal class SessionStateRecord
	{
		// Token: 0x04000E3D RID: 3645
		internal bool _recoverable;

		// Token: 0x04000E3E RID: 3646
		internal uint _version;

		// Token: 0x04000E3F RID: 3647
		internal int _dataLength;

		// Token: 0x04000E40 RID: 3648
		internal byte[] _data;
	}
}
