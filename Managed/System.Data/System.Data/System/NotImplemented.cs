using System;

namespace System
{
	// Token: 0x02000020 RID: 32
	internal static class NotImplemented
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005E05 File Offset: 0x00004005
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005E0C File Offset: 0x0000400C
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005E05 File Offset: 0x00004005
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
