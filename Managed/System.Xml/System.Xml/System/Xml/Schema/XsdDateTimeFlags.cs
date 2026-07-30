using System;

namespace System.Xml.Schema
{
	// Token: 0x020004A8 RID: 1192
	[Flags]
	internal enum XsdDateTimeFlags
	{
		// Token: 0x04001F9D RID: 8093
		DateTime = 1,
		// Token: 0x04001F9E RID: 8094
		Time = 2,
		// Token: 0x04001F9F RID: 8095
		Date = 4,
		// Token: 0x04001FA0 RID: 8096
		GYearMonth = 8,
		// Token: 0x04001FA1 RID: 8097
		GYear = 16,
		// Token: 0x04001FA2 RID: 8098
		GMonthDay = 32,
		// Token: 0x04001FA3 RID: 8099
		GDay = 64,
		// Token: 0x04001FA4 RID: 8100
		GMonth = 128,
		// Token: 0x04001FA5 RID: 8101
		XdrDateTimeNoTz = 256,
		// Token: 0x04001FA6 RID: 8102
		XdrDateTime = 512,
		// Token: 0x04001FA7 RID: 8103
		XdrTimeNoTz = 1024,
		// Token: 0x04001FA8 RID: 8104
		AllXsd = 255
	}
}
