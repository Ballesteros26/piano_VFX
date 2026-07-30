using System;
using System.Security;

namespace System.IO
{
	// Token: 0x0200039A RID: 922
	internal class FileInfoResultHandler : SearchResultHandler<FileInfo>
	{
		// Token: 0x06002AE2 RID: 10978 RVA: 0x000992D8 File Offset: 0x000974D8
		[SecurityCritical]
		internal override bool IsResultIncluded(SearchResult result)
		{
			return FileSystemEnumerableHelpers.IsFile(result.FindData);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000992E5 File Offset: 0x000974E5
		[SecurityCritical]
		internal override FileInfo CreateObject(SearchResult result)
		{
			FileInfo fileInfo = new FileInfo(result.FullPath, false);
			fileInfo.InitializeFrom(result.FindData);
			return fileInfo;
		}
	}
}
