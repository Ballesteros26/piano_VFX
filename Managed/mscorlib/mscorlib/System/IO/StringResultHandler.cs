using System;
using System.Security;

namespace System.IO
{
	// Token: 0x02000399 RID: 921
	internal class StringResultHandler : SearchResultHandler<string>
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x0009927D File Offset: 0x0009747D
		internal StringResultHandler(bool includeFiles, bool includeDirs)
		{
			this._includeFiles = includeFiles;
			this._includeDirs = includeDirs;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00099294 File Offset: 0x00097494
		[SecurityCritical]
		internal override bool IsResultIncluded(SearchResult result)
		{
			bool flag = this._includeFiles && FileSystemEnumerableHelpers.IsFile(result.FindData);
			bool flag2 = this._includeDirs && FileSystemEnumerableHelpers.IsDir(result.FindData);
			return flag || flag2;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000992D0 File Offset: 0x000974D0
		[SecurityCritical]
		internal override string CreateObject(SearchResult result)
		{
			return result.UserPath;
		}

		// Token: 0x04001699 RID: 5785
		private bool _includeFiles;

		// Token: 0x0400169A RID: 5786
		private bool _includeDirs;
	}
}
