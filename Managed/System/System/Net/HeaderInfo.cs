using System;

namespace System.Net
{
	// Token: 0x02000488 RID: 1160
	internal class HeaderInfo
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x0008548D File Offset: 0x0008368D
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04001EE1 RID: 7905
		internal readonly bool IsRequestRestricted;

		// Token: 0x04001EE2 RID: 7906
		internal readonly bool IsResponseRestricted;

		// Token: 0x04001EE3 RID: 7907
		internal readonly HeaderParser Parser;

		// Token: 0x04001EE4 RID: 7908
		internal readonly string HeaderName;

		// Token: 0x04001EE5 RID: 7909
		internal readonly bool AllowMultiValues;
	}
}
