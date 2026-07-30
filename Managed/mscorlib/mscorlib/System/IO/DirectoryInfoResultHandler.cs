using System;
using System.Security;

namespace System.IO
{
	// Token: 0x0200039B RID: 923
	internal class DirectoryInfoResultHandler : SearchResultHandler<DirectoryInfo>
	{
		// Token: 0x06002AE5 RID: 10981 RVA: 0x00099307 File Offset: 0x00097507
		[SecurityCritical]
		internal override bool IsResultIncluded(SearchResult result)
		{
			return FileSystemEnumerableHelpers.IsDir(result.FindData);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00099314 File Offset: 0x00097514
		[SecurityCritical]
		internal override DirectoryInfo CreateObject(SearchResult result)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(result.FullPath, false);
			directoryInfo.InitializeFrom(result.FindData);
			return directoryInfo;
		}
	}
}
