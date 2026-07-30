using System;

namespace System.Web.Hosting
{
	// Token: 0x02000536 RID: 1334
	internal struct HTTP_COOKED_URL
	{
		// Token: 0x04001FC9 RID: 8137
		internal readonly ushort FullUrlLength;

		// Token: 0x04001FCA RID: 8138
		internal readonly ushort HostLength;

		// Token: 0x04001FCB RID: 8139
		internal readonly ushort AbsPathLength;

		// Token: 0x04001FCC RID: 8140
		internal readonly ushort QueryStringLength;

		// Token: 0x04001FCD RID: 8141
		internal unsafe readonly char* pFullUrl;

		// Token: 0x04001FCE RID: 8142
		internal unsafe readonly char* pHost;

		// Token: 0x04001FCF RID: 8143
		internal unsafe readonly char* pAbsPath;

		// Token: 0x04001FD0 RID: 8144
		internal unsafe readonly char* pQueryString;
	}
}
