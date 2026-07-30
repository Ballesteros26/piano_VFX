using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200038D RID: 909
	[ComVisible(true)]
	public static class Directory
	{
		/// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory.</returns>
		/// <param name="path">The directory from which to retrieve the files. </param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A3D RID: 10813 RVA: 0x000975B2 File Offset: 0x000957B2
		public static string[] GetFiles(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A3E RID: 10814 RVA: 0x000975CE File Offset: 0x000957CE
		public static string[] GetFiles(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. -or- <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchpattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.-or-A network error has occurred. </exception>
		// Token: 0x06002A3F RID: 10815 RVA: 0x000975F4 File Offset: 0x000957F4
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalGetFiles(path, searchPattern, searchOption);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00097641 File Offset: 0x00095841
		private static string[] InternalGetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, true);
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0009764F File Offset: 0x0009584F
		[SecurityCritical]
		internal static string[] UnsafeGetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, false);
		}

		/// <summary>Gets the names of subdirectories (including their paths) in the specified directory.</summary>
		/// <returns>An array of the full names (including paths) of subdirectories in the specified path.</returns>
		/// <param name="path">The path for which an array of subdirectory names is returned. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A42 RID: 10818 RVA: 0x0009765D File Offset: 0x0009585D
		public static string[] GetDirectories(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Gets the names of subdirectories (including their paths) that match the specified search pattern in the current directory.</summary>
		/// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern.</returns>
		/// <param name="path">The path to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A43 RID: 10819 RVA: 0x00097679 File Offset: 0x00095879
		public static string[] GetDirectories(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Gets the names of the subdirectories (including their paths) that match the specified search pattern in the current directory, and optionally searches subdirectories.</summary>
		/// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern.</returns>
		/// <param name="path">The path to search. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		// Token: 0x06002A44 RID: 10820 RVA: 0x000976A0 File Offset: 0x000958A0
		public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalGetDirectories(path, searchPattern, searchOption);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000976ED File Offset: 0x000958ED
		private static string[] InternalGetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, true);
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000976FB File Offset: 0x000958FB
		[SecurityCritical]
		internal static string[] UnsafeGetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, false);
		}

		/// <summary>Returns the names of all files and subdirectories in the specified directory.</summary>
		/// <returns>An array of the names of files and subdirectories in the specified directory.</returns>
		/// <param name="path">The directory for which file and subdirectory names are returned. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A47 RID: 10823 RVA: 0x00097709 File Offset: 0x00095909
		public static string[] GetFileSystemEntries(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an array of file system entries that match the specified search criteria.</summary>
		/// <returns>An array of file system entries that match the specified search criteria.</returns>
		/// <param name="path">The path to be searched. </param>
		/// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />. The <paramref name="searchPattern" /> parameter cannot end in two periods ("..") or contain two periods ("..") followed by <see cref="F:System.IO.Path.DirectorySeparatorChar" /> or <see cref="F:System.IO.Path.AltDirectorySeparatorChar" />, nor can it contain any of the characters in <see cref="F:System.IO.Path.InvalidPathChars" />. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- <paramref name="searchPattern" /> does not contain a valid pattern. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A48 RID: 10824 RVA: 0x00097725 File Offset: 0x00095925
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Gets an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <returns>An array of file system entries that match the specified search criteria.</returns>
		/// <param name="path">The directory to search.</param>
		/// <param name="searchPattern">The string used to search for all files or directories that match its search pattern. The default pattern is for all files and directories: "*"</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A49 RID: 10825 RVA: 0x0009774C File Offset: 0x0009594C
		public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalGetFileSystemEntries(path, searchPattern, searchOption);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00097799 File Offset: 0x00095999
		private static string[] InternalGetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, true, searchOption, true);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000977A7 File Offset: 0x000959A7
		internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
		{
			return new List<string>(FileSystemEnumerableFactory.CreateFileNameIterator(path, userPathOriginal, searchPattern, includeFiles, includeDirs, searchOption, checkHost)).ToArray();
		}

		/// <summary>Returns an enumerable collection of directory names in a specified path.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" />.</returns>
		/// <param name="path">The directory to search.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A4C RID: 10828 RVA: 0x000977C2 File Offset: 0x000959C2
		public static IEnumerable<string> EnumerateDirectories(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <param name="path">The directory to search.</param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A4D RID: 10829 RVA: 0x000977DE File Offset: 0x000959DE
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A4E RID: 10830 RVA: 0x00097804 File Offset: 0x00095A04
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalEnumerateDirectories(path, searchPattern, searchOption);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00097851 File Offset: 0x00095A51
		private static IEnumerable<string> InternalEnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, false, true);
		}

		/// <summary>Returns an enumerable collection of file names in a specified path.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
		/// <param name="path">The directory to search. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A50 RID: 10832 RVA: 0x0009785D File Offset: 0x00095A5D
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A51 RID: 10833 RVA: 0x00097879 File Offset: 0x00095A79
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A52 RID: 10834 RVA: 0x000978A0 File Offset: 0x00095AA0
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalEnumerateFiles(path, searchPattern, searchOption);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000978ED File Offset: 0x00095AED
		private static IEnumerable<string> InternalEnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, false);
		}

		/// <summary>Returns an enumerable collection of file-system entries in a specified path.</summary>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" />.</returns>
		/// <param name="path">The directory to search. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A54 RID: 10836 RVA: 0x000978F9 File Offset: 0x00095AF9
		public static IEnumerable<string> EnumerateFileSystemEntries(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file-system entries that match a search pattern in a specified path.</summary>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A55 RID: 10837 RVA: 0x00097915 File Offset: 0x00095B15
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
		/// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
		/// <param name="path">The directory to search. </param>
		/// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  </param>
		/// <param name="searchOption">One of the enumeration values  that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path " />is a zero-length string, contains only white space, or contains invalid characters as defined by <see cref="M:System.IO.Path.GetInvalidPathChars" />.- or -<paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.-or-<paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid, such as referring to an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> is a file name.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x06002A56 RID: 10838 RVA: 0x0009793C File Offset: 0x00095B3C
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("Enum value was out of legal range."));
			}
			return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00097989 File Offset: 0x00095B89
		private static IEnumerable<string> InternalEnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, true);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00097995 File Offset: 0x00095B95
		private static IEnumerable<string> EnumerateFileSystemNames(string path, string searchPattern, SearchOption searchOption, bool includeFiles, bool includeDirs)
		{
			return FileSystemEnumerableFactory.CreateFileNameIterator(path, path, searchPattern, includeFiles, includeDirs, searchOption, true);
		}

		/// <summary>Returns the volume information, root information, or both for the specified path.</summary>
		/// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
		/// <param name="path">The path of a file or directory. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A59 RID: 10841 RVA: 0x000979A4 File Offset: 0x00095BA4
		public static string GetDirectoryRoot(string path)
		{
			Path.Validate(path);
			return new string(Path.DirectorySeparatorChar, 1);
		}

		/// <summary>Creates all directories and subdirectories in the specified path.</summary>
		/// <returns>An object that represents the directory for the specified path.</returns>
		/// <param name="path">The directory path to create. </param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.-or-The network name is not known.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or-<paramref name="path" /> is prefixed with, or contains only a colon character (:).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A5A RID: 10842 RVA: 0x000979B8 File Offset: 0x00095BB8
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Path contains invalid chars");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Only blank characters in path");
			}
			if (File.Exists(path))
			{
				throw new IOException("Cannot create " + path + " because a file with the same name already exists.");
			}
			if (Environment.IsRunningOnWindows && path == ":")
			{
				throw new ArgumentException("Only ':' In path");
			}
			return Directory.CreateDirectoriesInternal(path);
		}

		/// <summary>Creates all the directories in the specified path, applying the specified Windows security.</summary>
		/// <returns>An object that represents the directory for the specified path.</returns>
		/// <param name="path">The directory to create.</param>
		/// <param name="directorySecurity">The access control to apply to the directory.</param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.-or-The network name is not known.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. -or-<paramref name="path" /> is prefixed with, or contains only a colon character (:).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A5B RID: 10843 RVA: 0x00097A5A File Offset: 0x00095C5A
		[MonoLimitation("DirectorySecurity not implemented")]
		public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
		{
			return Directory.CreateDirectory(path);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00097A64 File Offset: 0x00095C64
		private static DirectoryInfo CreateDirectoriesInternal(string path)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, path).Demand();
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(path, true);
			if (directoryInfo.Parent != null && !directoryInfo.Parent.Exists)
			{
				directoryInfo.Parent.Create();
			}
			MonoIOError monoIOError;
			if (!MonoIO.CreateDirectory(directoryInfo.FullName, out monoIOError) && monoIOError != MonoIOError.ERROR_ALREADY_EXISTS && monoIOError != MonoIOError.ERROR_FILE_EXISTS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
			return directoryInfo;
		}

		/// <summary>Deletes an empty directory from a specified path.</summary>
		/// <param name="path">The name of the empty directory to remove. This directory must be writable and empty. </param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory is the application's current working directory.-or-The directory specified by <paramref name="path" /> is not empty.-or-The directory is read-only or contains a read-only file.-or-The directory is being used by another process.-or-There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from directories. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.-or-<paramref name="path" /> refers to a file instead of a directory.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A5D RID: 10845 RVA: 0x00097AD4 File Offset: 0x00095CD4
		public static void Delete(string path)
		{
			Path.Validate(path);
			if (Environment.IsRunningOnWindows && path == ":")
			{
				throw new NotSupportedException("Only ':' In path");
			}
			MonoIOError monoIOError;
			bool flag;
			if (MonoIO.ExistsSymlink(path, out monoIOError))
			{
				flag = MonoIO.DeleteFile(path, out monoIOError);
			}
			else
			{
				flag = MonoIO.RemoveDirectory(path, out monoIOError);
			}
			if (flag)
			{
				return;
			}
			if (monoIOError != MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
			if (File.Exists(path))
			{
				throw new IOException("Directory does not exist, but a file of the same name exists.");
			}
			throw new DirectoryNotFoundException("Directory does not exist.");
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00097B54 File Offset: 0x00095D54
		private static void RecursiveDelete(string path)
		{
			foreach (string text in Directory.GetDirectories(path))
			{
				MonoIOError monoIOError;
				if (MonoIO.ExistsSymlink(text, out monoIOError))
				{
					MonoIO.DeleteFile(text, out monoIOError);
				}
				else
				{
					Directory.RecursiveDelete(text);
				}
			}
			string[] array = Directory.GetFiles(path);
			for (int i = 0; i < array.Length; i++)
			{
				File.Delete(array[i]);
			}
			Directory.Delete(path);
		}

		/// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory. </summary>
		/// <param name="path">The name of the directory to remove. </param>
		/// <param name="recursive">true to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, false. </param>
		/// <exception cref="T:System.IO.IOException">A file with the same name and location specified by <paramref name="path" /> exists.-or-The directory specified by <paramref name="path" /> is read-only, or <paramref name="recursive" /> is false and <paramref name="path" /> is not an empty directory. -or-The directory is the application's current working directory. -or-The directory contains a read-only file.-or-The directory is being used by another process.There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> does not exist or could not be found.-or-<paramref name="path" /> refers to a file instead of a directory.-or-The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A5F RID: 10847 RVA: 0x00097BB8 File Offset: 0x00095DB8
		public static void Delete(string path, bool recursive)
		{
			Path.Validate(path);
			if (recursive)
			{
				Directory.RecursiveDelete(path);
				return;
			}
			Directory.Delete(path);
		}

		/// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
		/// <returns>true if <paramref name="path" /> refers to an existing directory; otherwise, false.</returns>
		/// <param name="path">The path to test. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A60 RID: 10848 RVA: 0x00097BD0 File Offset: 0x00095DD0
		public static bool Exists(string path)
		{
			if (path == null)
			{
				return false;
			}
			if (!SecurityManager.CheckElevatedPermissions())
			{
				return false;
			}
			string fullPath;
			try
			{
				fullPath = Path.GetFullPath(path);
			}
			catch
			{
				return false;
			}
			MonoIOError monoIOError;
			return MonoIO.ExistsDirectory(fullPath, out monoIOError);
		}

		/// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
		/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <param name="path">The file or directory for which to obtain access date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A61 RID: 10849 RVA: 0x00097C14 File Offset: 0x00095E14
		public static DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
		/// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
		/// <param name="path">The file or directory for which to obtain access date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter is in an invalid format. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A62 RID: 10850 RVA: 0x00097C1C File Offset: 0x00095E1C
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return Directory.GetLastAccessTime(path).ToUniversalTime();
		}

		/// <summary>Returns the date and time the specified file or directory was last written to.</summary>
		/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <param name="path">The file or directory for which to obtain modification date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A63 RID: 10851 RVA: 0x00097C37 File Offset: 0x00095E37
		public static DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		/// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.</summary>
		/// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in UTC time.</returns>
		/// <param name="path">The file or directory for which to obtain modification date and time information. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A64 RID: 10852 RVA: 0x00097C40 File Offset: 0x00095E40
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return Directory.GetLastWriteTime(path).ToUniversalTime();
		}

		/// <summary>Gets the creation date and time of a directory.</summary>
		/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in local time.</returns>
		/// <param name="path">The path of the directory. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A65 RID: 10853 RVA: 0x00097C5B File Offset: 0x00095E5B
		public static DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		/// <summary>Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.</summary>
		/// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in UTC time.</returns>
		/// <param name="path">The path of the directory. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A66 RID: 10854 RVA: 0x00097C64 File Offset: 0x00095E64
		public static DateTime GetCreationTimeUtc(string path)
		{
			return Directory.GetCreationTime(path).ToUniversalTime();
		}

		/// <summary>Gets the current working directory of the application.</summary>
		/// <returns>A string that contains the path of the current working directory, and does not end with a backslash (\).</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system is Windows CE, which does not have current directory functionality.This method is available in the .NET Compact Framework, but is not currently supported.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A67 RID: 10855 RVA: 0x00097C80 File Offset: 0x00095E80
		public static string GetCurrentDirectory()
		{
			string text = Directory.InsecureGetCurrentDirectory();
			if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x00097CB4 File Offset: 0x00095EB4
		internal static string InsecureGetCurrentDirectory()
		{
			MonoIOError monoIOError;
			string currentDirectory = MonoIO.GetCurrentDirectory(out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			return currentDirectory;
		}

		/// <summary>Retrieves the names of the logical drives on this computer in the form "&lt;drive letter&gt;:\".</summary>
		/// <returns>The logical drives on this computer.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occured (for example, a disk error). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002A69 RID: 10857 RVA: 0x00097CD2 File Offset: 0x00095ED2
		public static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrives();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x00097CD9 File Offset: 0x00095ED9
		private static bool IsRootDirectory(string path)
		{
			return (Path.DirectorySeparatorChar == '/' && path == "/") || (Path.DirectorySeparatorChar == '\\' && path.Length == 3 && path.EndsWith(":\\"));
		}

		/// <summary>Retrieves the parent directory of the specified path, including both absolute and relative paths.</summary>
		/// <returns>The parent directory, or null if <paramref name="path" /> is the root directory, including the root of a UNC server or share name.</returns>
		/// <param name="path">The path for which to retrieve the parent directory. </param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A6B RID: 10859 RVA: 0x00097D18 File Offset: 0x00095F18
		public static DirectoryInfo GetParent(string path)
		{
			Path.Validate(path);
			if (Directory.IsRootDirectory(path))
			{
				return null;
			}
			string text = Path.GetDirectoryName(path);
			if (text.Length == 0)
			{
				text = Directory.GetCurrentDirectory();
			}
			return new DirectoryInfo(text);
		}

		/// <summary>Moves a file or a directory and its contents to a new location.</summary>
		/// <param name="sourceDirName">The path of the file or directory to move. </param>
		/// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume. -or- <paramref name="destDirName" /> already exists. -or- The <paramref name="sourceDirName" /> and <paramref name="destDirName" /> parameters refer to the same file or directory. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirName" /> or <paramref name="destDirName" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified by <paramref name="sourceDirName" /> is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A6C RID: 10860 RVA: 0x00097D50 File Offset: 0x00095F50
		public static void Move(string sourceDirName, string destDirName)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (sourceDirName.Trim().Length == 0 || sourceDirName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Invalid source directory name: " + sourceDirName, "sourceDirName");
			}
			if (destDirName.Trim().Length == 0 || destDirName.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Invalid target directory name: " + destDirName, "destDirName");
			}
			if (sourceDirName == destDirName)
			{
				throw new IOException("Source and destination path must be different.");
			}
			if (Directory.Exists(destDirName))
			{
				throw new IOException(destDirName + " already exists.");
			}
			if (!Directory.Exists(sourceDirName) && !File.Exists(sourceDirName))
			{
				throw new DirectoryNotFoundException(sourceDirName + " does not exist");
			}
			MonoIOError monoIOError;
			if (!MonoIO.MoveFile(sourceDirName, destDirName, out monoIOError))
			{
				throw MonoIO.GetException(monoIOError);
			}
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the specified directory.</summary>
		/// <param name="path">A directory to add or remove access control list (ACL) entries from.</param>
		/// <param name="directorySecurity">A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> was invalid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to the directory specified by <paramref name="path" />.-or-The current process does not have sufficient privilege to set the ACL entry.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A6D RID: 10861 RVA: 0x00097E3B File Offset: 0x0009603B
		public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			directorySecurity.PersistModifications(path);
		}

		/// <summary>Sets the creation date and time for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information. </param>
		/// <param name="creationTime">An object that contains the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A6E RID: 10862 RVA: 0x00097E52 File Offset: 0x00096052
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			File.SetCreationTime(path, creationTime);
		}

		/// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to set the creation date and time information. </param>
		/// <param name="creationTimeUtc">An object that  contains the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates or times permitted for this operation. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A6F RID: 10863 RVA: 0x00097E5B File Offset: 0x0009605B
		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			Directory.SetCreationTime(path, creationTimeUtc.ToLocalTime());
		}

		/// <summary>Sets the application's current working directory to the specified directory.</summary>
		/// <param name="path">The path to which the current working directory is set. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to access unmanaged code. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory was not found.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002A70 RID: 10864 RVA: 0x00097E6C File Offset: 0x0009606C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("path string must not be an empty string or whitespace string");
			}
			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException("Directory \"" + path + "\" not found.");
			}
			MonoIOError monoIOError;
			MonoIO.SetCurrentDirectory(path, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		/// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information. </param>
		/// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A71 RID: 10865 RVA: 0x00097ED1 File Offset: 0x000960D1
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			File.SetLastAccessTime(path, lastAccessTime);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to set the access date and time information. </param>
		/// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A72 RID: 10866 RVA: 0x00097EDA File Offset: 0x000960DA
		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			Directory.SetLastAccessTime(path, lastAccessTimeUtc.ToLocalTime());
		}

		/// <summary>Sets the date and time a directory was last written to.</summary>
		/// <param name="path">The path of the directory. </param>
		/// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.  </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A73 RID: 10867 RVA: 0x00097EE9 File Offset: 0x000960E9
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			File.SetLastWriteTime(path, lastWriteTime);
		}

		/// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
		/// <param name="path">The path of the directory. </param>
		/// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A74 RID: 10868 RVA: 0x00097EF2 File Offset: 0x000960F2
		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			Directory.SetLastWriteTime(path, lastWriteTimeUtc.ToLocalTime());
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for a specified directory.</summary>
		/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A75 RID: 10869 RVA: 0x00097F01 File Offset: 0x00096101
		public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new DirectorySecurity(path, includeSections);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for a specified directory.</summary>
		/// <returns>An object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The path to a directory containing a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows 2000 or later.</exception>
		/// <exception cref="T:System.SystemException">A system-level error occurred, such as the directory could not be found. The specific exception may be a subclass of <see cref="T:System.SystemException" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a directory that is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A76 RID: 10870 RVA: 0x00097F0A File Offset: 0x0009610A
		public static DirectorySecurity GetAccessControl(string path)
		{
			return Directory.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00097F14 File Offset: 0x00096114
		internal static string GetDemandDir(string fullPath, bool thisDirOnly)
		{
			string text;
			if (thisDirOnly)
			{
				if (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar))
				{
					text = fullPath + ".";
				}
				else
				{
					text = fullPath + Path.DirectorySeparatorCharAsString + ".";
				}
			}
			else if (!fullPath.EndsWith(Path.DirectorySeparatorChar) && !fullPath.EndsWith(Path.AltDirectorySeparatorChar))
			{
				text = fullPath + Path.DirectorySeparatorCharAsString;
			}
			else
			{
				text = fullPath;
			}
			return text;
		}

		// Token: 0x0200038E RID: 910
		internal sealed class SearchData
		{
			// Token: 0x06002A78 RID: 10872 RVA: 0x00097F8A File Offset: 0x0009618A
			public SearchData(string fullPath, string userPath, SearchOption searchOption)
			{
				this.fullPath = fullPath;
				this.userPath = userPath;
				this.searchOption = searchOption;
			}

			// Token: 0x0400167E RID: 5758
			public readonly string fullPath;

			// Token: 0x0400167F RID: 5759
			public readonly string userPath;

			// Token: 0x04001680 RID: 5760
			public readonly SearchOption searchOption;
		}
	}
}
