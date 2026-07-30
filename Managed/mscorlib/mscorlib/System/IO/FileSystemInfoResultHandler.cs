using System;
using System.Security;

namespace System.IO
{
	// Token: 0x0200039C RID: 924
	internal class FileSystemInfoResultHandler : SearchResultHandler<FileSystemInfo>
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x00099338 File Offset: 0x00097538
		[SecurityCritical]
		internal override bool IsResultIncluded(SearchResult result)
		{
			bool flag = FileSystemEnumerableHelpers.IsFile(result.FindData);
			return FileSystemEnumerableHelpers.IsDir(result.FindData) || flag;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00099360 File Offset: 0x00097560
		[SecurityCritical]
		internal override FileSystemInfo CreateObject(SearchResult result)
		{
			FileSystemEnumerableHelpers.IsFile(result.FindData);
			if (FileSystemEnumerableHelpers.IsDir(result.FindData))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(result.FullPath, false);
				directoryInfo.InitializeFrom(result.FindData);
				return directoryInfo;
			}
			FileInfo fileInfo = new FileInfo(result.FullPath, false);
			fileInfo.InitializeFrom(result.FindData);
			return fileInfo;
		}
	}
}
