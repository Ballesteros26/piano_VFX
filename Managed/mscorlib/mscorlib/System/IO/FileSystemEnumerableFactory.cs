using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x02000395 RID: 917
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x06002AC1 RID: 10945 RVA: 0x00098A04 File Offset: 0x00096C04
		internal static IEnumerable<string> CreateFileNameIterator(string path, string originalUserPath, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
		{
			SearchResultHandler<string> searchResultHandler = new StringResultHandler(includeFiles, includeDirs);
			return new FileSystemEnumerableIterator<string>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, checkHost);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00098A28 File Offset: 0x00096C28
		internal static IEnumerable<FileInfo> CreateFileInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileInfo> searchResultHandler = new FileInfoResultHandler();
			return new FileSystemEnumerableIterator<FileInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00098A48 File Offset: 0x00096C48
		internal static IEnumerable<DirectoryInfo> CreateDirectoryInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<DirectoryInfo> searchResultHandler = new DirectoryInfoResultHandler();
			return new FileSystemEnumerableIterator<DirectoryInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00098A68 File Offset: 0x00096C68
		internal static IEnumerable<FileSystemInfo> CreateFileSystemInfoIterator(string path, string originalUserPath, string searchPattern, SearchOption searchOption)
		{
			SearchResultHandler<FileSystemInfo> searchResultHandler = new FileSystemInfoResultHandler();
			return new FileSystemEnumerableIterator<FileSystemInfo>(path, originalUserPath, searchPattern, searchOption, searchResultHandler, true);
		}
	}
}
