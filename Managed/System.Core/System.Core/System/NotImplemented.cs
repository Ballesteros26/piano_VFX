using System;

namespace System
{
	// Token: 0x0200000B RID: 11
	internal static class NotImplemented
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022B6 File Offset: 0x000004B6
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022BD File Offset: 0x000004BD
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022B6 File Offset: 0x000004B6
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
