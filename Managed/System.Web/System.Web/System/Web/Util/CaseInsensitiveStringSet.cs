using System;

namespace System.Web.Util
{
	// Token: 0x02000124 RID: 292
	internal class CaseInsensitiveStringSet : StringSet
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00008B66 File Offset: 0x00006D66
		protected override bool CaseInsensitive
		{
			get
			{
				return true;
			}
		}
	}
}
