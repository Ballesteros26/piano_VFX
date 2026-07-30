using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000139 RID: 313
	internal class DataSourceHelper
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00002050 File Offset: 0x00000250
		private DataSourceHelper()
		{
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000277E5 File Offset: 0x000259E5
		[Obsolete("Use DataSourceResolver")]
		public static IEnumerable GetResolvedDataSource(object o, string data_member)
		{
			return DataSourceResolver.ResolveDataSource(o, data_member);
		}
	}
}
