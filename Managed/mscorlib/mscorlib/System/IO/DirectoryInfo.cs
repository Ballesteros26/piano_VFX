using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003C9 RID: 969
	[ComVisible(true)]
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryInfo" /> class on the specified path.</summary>
		/// <param name="path">A string specifying the path on which to create the DirectoryInfo. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		// Token: 0x06002D7B RID: 11643 RVA: 0x000A2999 File Offset: 0x000A0B99
		public DirectoryInfo(string path)
			: this(path, false)
		{
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000A29A3 File Offset: 0x000A0BA3
		internal DirectoryInfo(string path, bool simpleOriginalPath)
		{
			this.CheckPath(path);
			this.FullPath = Path.GetFullPath(path);
			if (simpleOriginalPath)
			{
				this.OriginalPath = Path.GetFileName(this.FullPath);
			}
			else
			{
				this.OriginalPath = path;
			}
			this.Initialize();
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000A29E1 File Offset: 0x000A0BE1
		private DirectoryInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Initialize();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000A29F4 File Offset: 0x000A0BF4
		private void Initialize()
		{
			int num = this.FullPath.Length - 1;
			if (num > 1 && this.FullPath[num] == Path.DirectorySeparatorChar)
			{
				num--;
			}
			int num2 = this.FullPath.LastIndexOf(Path.DirectorySeparatorChar, num);
			if (num2 == -1 || (num2 == 0 && num == 0))
			{
				this.current = this.FullPath;
				this.parent = null;
				return;
			}
			this.current = this.FullPath.Substring(num2 + 1, num - num2);
			if (num2 == 0 && !Environment.IsRunningOnWindows)
			{
				this.parent = Path.DirectorySeparatorStr;
			}
			else
			{
				this.parent = this.FullPath.Substring(0, num2);
			}
			if (Environment.IsRunningOnWindows && this.parent.Length == 2 && this.parent[1] == ':' && char.IsLetter(this.parent[0]))
			{
				this.parent += Path.DirectorySeparatorChar.ToString();
			}
		}

		/// <summary>Gets a value indicating whether the directory exists.</summary>
		/// <returns>true if the directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000A2AF1 File Offset: 0x000A0CF1
		public override bool Exists
		{
			get
			{
				if (this._dataInitialised == -1)
				{
					base.Refresh();
				}
				return this._data.fileAttributes != (FileAttributes)(-1) && (this._data.fileAttributes & FileAttributes.Directory) != (FileAttributes)0;
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.IO.DirectoryInfo" /> instance.</summary>
		/// <returns>The directory name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000A2B25 File Offset: 0x000A0D25
		public override string Name
		{
			get
			{
				return this.current;
			}
		}

		/// <summary>Gets the parent directory of a specified subdirectory.</summary>
		/// <returns>The parent directory, or null if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000A2B2D File Offset: 0x000A0D2D
		public DirectoryInfo Parent
		{
			get
			{
				if (this.parent == null || this.parent.Length == 0)
				{
					return null;
				}
				return new DirectoryInfo(this.parent);
			}
		}

		/// <summary>Gets the root portion of the directory.</summary>
		/// <returns>An object that represents the root of the directory.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000A2B54 File Offset: 0x000A0D54
		public DirectoryInfo Root
		{
			get
			{
				string pathRoot = Path.GetPathRoot(this.FullPath);
				if (pathRoot == null)
				{
					return null;
				}
				return new DirectoryInfo(pathRoot);
			}
		}

		/// <summary>Creates a directory.</summary>
		/// <exception cref="T:System.IO.IOException">The directory cannot be created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D83 RID: 11651 RVA: 0x000A2B78 File Offset: 0x000A0D78
		public void Create()
		{
			Directory.CreateDirectory(this.FullPath);
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid DirectoryInfo characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.-or- A file or directory already has the name specified by <paramref name="path" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.-or-The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000A2B86 File Offset: 0x000A0D86
		public DirectoryInfo CreateSubdirectory(string path)
		{
			this.CheckPath(path);
			path = Path.Combine(this.FullPath, path);
			Directory.CreateDirectory(path);
			return new DirectoryInfo(path);
		}

		/// <summary>Returns a file list from the current directory.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D85 RID: 11653 RVA: 0x000A2BAA File Offset: 0x000A0DAA
		public FileInfo[] GetFiles()
		{
			return this.GetFiles("*");
		}

		/// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <param name="searchPattern">The search string, such as "*.txt". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D86 RID: 11654 RVA: 0x000A2BB8 File Offset: 0x000A0DB8
		public FileInfo[] GetFiles(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			string[] files = Directory.GetFiles(this.FullPath, searchPattern);
			FileInfo[] array = new FileInfo[files.Length];
			int num = 0;
			foreach (string text in files)
			{
				array[num++] = new FileInfo(text);
			}
			return array;
		}

		/// <summary>Returns the subdirectories of the current directory.</summary>
		/// <returns>An array of <see cref="T:System.IO.DirectoryInfo" /> objects.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D87 RID: 11655 RVA: 0x000A2C0D File Offset: 0x000A0E0D
		public DirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories("*");
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria.</summary>
		/// <returns>An array of type DirectoryInfo matching <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string. For example, "System*" can be used to search for all directories that begin with the word "System". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D88 RID: 11656 RVA: 0x000A2C1C File Offset: 0x000A0E1C
		public DirectoryInfo[] GetDirectories(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			string[] directories = Directory.GetDirectories(this.FullPath, searchPattern);
			DirectoryInfo[] array = new DirectoryInfo[directories.Length];
			int num = 0;
			foreach (string text in directories)
			{
				array[num++] = new DirectoryInfo(text);
			}
			return array;
		}

		/// <summary>Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries representing all the files and subdirectories in a directory.</summary>
		/// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D89 RID: 11657 RVA: 0x000A2C71 File Offset: 0x000A0E71
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.GetFileSystemInfos("*");
		}

		/// <summary>Retrieves an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> objects representing the files and subdirectories that match the specified search criteria.</summary>
		/// <returns>An array of strongly typed FileSystemInfo objects matching the search criteria.</returns>
		/// <param name="searchPattern">The search string. For example, "System*" can be used to search for all directories that begin with the word "System". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D8A RID: 11658 RVA: 0x000A2C7E File Offset: 0x000A0E7E
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
		{
			return this.GetFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Retrieves an array of <see cref="T:System.IO.FileSystemInfo" /> objects that represent the files and subdirectories matching the specified search criteria.</summary>
		/// <returns>An array of file system entries that match the search criteria.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all files and directories.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D8B RID: 11659 RVA: 0x000A2C88 File Offset: 0x000A0E88
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Must be TopDirectoryOnly or AllDirectories");
			}
			if (!Directory.Exists(this.FullPath))
			{
				throw new IOException("Invalid directory");
			}
			List<FileSystemInfo> list = new List<FileSystemInfo>();
			this.InternalGetFileSystemInfos(searchPattern, searchOption, list);
			return list.ToArray();
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000A2CE8 File Offset: 0x000A0EE8
		private void InternalGetFileSystemInfos(string searchPattern, SearchOption searchOption, List<FileSystemInfo> infos)
		{
			string[] directories = Directory.GetDirectories(this.FullPath, searchPattern);
			string[] files = Directory.GetFiles(this.FullPath, searchPattern);
			Array.ForEach<string>(directories, delegate(string dir)
			{
				infos.Add(new DirectoryInfo(dir));
			});
			Array.ForEach<string>(files, delegate(string file)
			{
				infos.Add(new FileInfo(file));
			});
			if (directories.Length == 0 || searchOption == SearchOption.TopDirectoryOnly)
			{
				return;
			}
			string[] array = directories;
			for (int i = 0; i < array.Length; i++)
			{
				new DirectoryInfo(array[i]).InternalGetFileSystemInfos(searchPattern, searchOption, infos);
			}
		}

		/// <summary>Deletes this <see cref="T:System.IO.DirectoryInfo" /> if it is empty.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is not empty. -or-The directory is the application's current working directory.-or-There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D8D RID: 11661 RVA: 0x000A2D6B File Offset: 0x000A0F6B
		public override void Delete()
		{
			this.Delete(false);
		}

		/// <summary>Deletes this instance of a <see cref="T:System.IO.DirectoryInfo" />, specifying whether to delete subdirectories and files.</summary>
		/// <param name="recursive">true to delete this directory, its subdirectories, and all files; otherwise, false. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is read-only.-or- The directory contains one or more files or subdirectories and <paramref name="recursive" /> is false.-or-The directory is the application's current working directory. -or-There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000A2D74 File Offset: 0x000A0F74
		public void Delete(bool recursive)
		{
			Directory.Delete(this.FullPath, recursive);
		}

		/// <summary>Moves a <see cref="T:System.IO.DirectoryInfo" /> instance and its contents to a new path.</summary>
		/// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destDirName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destDirName" /> is an empty string (''"). </exception>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume. -or-<paramref name="destDirName" /> already exists.-or-You are not authorized to access this path.-or- The directory being moved and the destination directory have the same name.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The destination directory cannot be found.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D8F RID: 11663 RVA: 0x000A2D84 File Offset: 0x000A0F84
		public void MoveTo(string destDirName)
		{
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destDirName");
			}
			Directory.Move(this.FullPath, Path.GetFullPath(destDirName));
			this.OriginalPath = destDirName;
			this.FullPath = destDirName;
			this.Initialize();
		}

		/// <summary>Returns the original path that was passed by the user.</summary>
		/// <returns>Returns the original path that was passed by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002D90 RID: 11664 RVA: 0x000A2DDE File Offset: 0x000A0FDE
		public override string ToString()
		{
			return this.OriginalPath;
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of type DirectoryInfo matching <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string. For example, "System*" can be used to search for all directories that begin with the word "System".</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		// Token: 0x06002D91 RID: 11665 RVA: 0x000A2DE8 File Offset: 0x000A0FE8
		public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
		{
			string[] directories = Directory.GetDirectories(this.FullPath, searchPattern, searchOption);
			DirectoryInfo[] array = new DirectoryInfo[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				string text = directories[i];
				array[i] = new DirectoryInfo(text);
			}
			return array;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000A2E28 File Offset: 0x000A1028
		internal int GetFilesSubdirs(ArrayList l, string pattern)
		{
			FileInfo[] array = null;
			try
			{
				array = this.GetFiles(pattern);
			}
			catch (UnauthorizedAccessException)
			{
				return 0;
			}
			int num = array.Length;
			l.Add(array);
			foreach (DirectoryInfo directoryInfo in this.GetDirectories())
			{
				num += directoryInfo.GetFilesSubdirs(l, pattern);
			}
			return num;
		}

		/// <summary>Returns a file list from the current directory matching the given search pattern and using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <param name="searchPattern">The search string. For example, "System*" can be used to search for all directories that begin with the word "System".</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000A2E90 File Offset: 0x000A1090
		public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
		{
			if (searchOption == SearchOption.TopDirectoryOnly)
			{
				return this.GetFiles(searchPattern);
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				string text = Locale.GetText("Invalid enum value '{0}' for '{1}'.", new object[] { searchOption, "SearchOption" });
				throw new ArgumentOutOfRangeException("searchOption", text);
			}
			ArrayList arrayList = new ArrayList();
			int filesSubdirs = this.GetFilesSubdirs(arrayList, searchPattern);
			int num = 0;
			FileInfo[] array = new FileInfo[filesSubdirs];
			foreach (object obj in arrayList)
			{
				FileInfo[] array2 = (FileInfo[])obj;
				array2.CopyTo(array, num);
				num += array2.Length;
			}
			return array;
		}

		/// <summary>Creates a directory using a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object.</summary>
		/// <param name="directorySecurity">The access control to apply to the directory.</param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">Creating a directory with only the colon (:) character was attempted. </exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D94 RID: 11668 RVA: 0x000A2F4C File Offset: 0x000A114C
		[MonoLimitation("DirectorySecurity isn't implemented")]
		public void Create(DirectorySecurity directorySecurity)
		{
			if (directorySecurity != null)
			{
				throw new UnauthorizedAccessException();
			}
			this.Create();
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
		/// <param name="directorySecurity">The security to apply.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid DirectoryInfo characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.-or- A file or directory already has the name specified by <paramref name="path" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.-or-The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D95 RID: 11669 RVA: 0x000A2F5D File Offset: 0x000A115D
		[MonoLimitation("DirectorySecurity isn't implemented")]
		public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity != null)
			{
				throw new UnauthorizedAccessException();
			}
			return this.CreateSubdirectory(path);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the directory.</returns>
		/// <exception cref="T:System.SystemException">The directory could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the directory.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D96 RID: 11670 RVA: 0x000A2F6F File Offset: 0x000A116F
		public DirectorySecurity GetAccessControl()
		{
			return Directory.GetAccessControl(this.FullPath);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.ExceptionsException typeCondition<see cref="T:System.SystemException" />The directory could not be found or modified.<see cref="T:System.UnauthorizedAccessException" />The current process does not have access to open the directory.<see cref="T:System.IO.IOException" />An I/O error occurred while opening the directory.<see cref="T:System.PlatformNotSupportedException" />The current operating system is not Microsoft Windows 2000 or later.<see cref="T:System.UnauthorizedAccessException" />The directory is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</returns>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D97 RID: 11671 RVA: 0x000A2F7C File Offset: 0x000A117C
		public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return Directory.GetAccessControl(this.FullPath, includeSections);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <param name="directorySecurity">An object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000A2F8A File Offset: 0x000A118A
		public void SetAccessControl(DirectorySecurity directorySecurity)
		{
			Directory.SetAccessControl(this.FullPath, directorySecurity);
		}

		/// <summary>Returns an enumerable collection of directory information in the current directory.</summary>
		/// <returns>An enumerable collection of directories in the current directory.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000A2F98 File Offset: 0x000A1198
		public IEnumerable<DirectoryInfo> EnumerateDirectories()
		{
			return this.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern.</summary>
		/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all directories. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D9A RID: 11674 RVA: 0x000A2FA6 File Offset: 0x000A11A6
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
		{
			return this.EnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of directory information that matches a specified search pattern and search subdirectory option. </summary>
		/// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all directories.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D9B RID: 11675 RVA: 0x000A2FB0 File Offset: 0x000A11B0
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.CreateEnumerateDirectoriesIterator(searchPattern, searchOption);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000A2FC8 File Offset: 0x000A11C8
		private IEnumerable<DirectoryInfo> CreateEnumerateDirectoriesIterator(string searchPattern, SearchOption searchOption)
		{
			foreach (string text in Directory.EnumerateDirectories(this.FullPath, searchPattern, searchOption))
			{
				yield return new DirectoryInfo(text);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Returns an enumerable collection of file information in the current directory.</summary>
		/// <returns>An enumerable collection of the files in the current directory.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D9D RID: 11677 RVA: 0x000A2FE6 File Offset: 0x000A11E6
		public IEnumerable<FileInfo> EnumerateFiles()
		{
			return this.EnumerateFiles("*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file information that matches a search pattern.</summary>
		/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all files.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileInfo" /> object is invalid, (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D9E RID: 11678 RVA: 0x000A2FF4 File Offset: 0x000A11F4
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
		{
			return this.EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file information that matches a specified search pattern and search subdirectory option.</summary>
		/// <returns>An enumerable collection of files that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all files.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002D9F RID: 11679 RVA: 0x000A2FFE File Offset: 0x000A11FE
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.CreateEnumerateFilesIterator(searchPattern, searchOption);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000A3016 File Offset: 0x000A1216
		private IEnumerable<FileInfo> CreateEnumerateFilesIterator(string searchPattern, SearchOption searchOption)
		{
			foreach (string text in Directory.EnumerateFiles(this.FullPath, searchPattern, searchOption))
			{
				yield return new FileInfo(text);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Returns an enumerable collection of file system information in the current directory.</summary>
		/// <returns>An enumerable collection of file system information in the current directory. </returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileSystemInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x000A3034 File Offset: 0x000A1234
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
		{
			return this.EnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern.</summary>
		/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all files and directories.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileSystemInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000A3042 File Offset: 0x000A1242
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
		{
			return this.EnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
		}

		/// <summary>Returns an enumerable collection of file system information that matches a specified search pattern and search subdirectory option.</summary>
		/// <returns>An enumerable collection of file system information objects that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
		/// <param name="searchPattern">The search string. The default pattern is "*", which returns all files or directories.</param>
		/// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="T:System.IO.FileSystemInfo" /> object is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000A304C File Offset: 0x000A124C
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchoption");
			}
			return DirectoryInfo.EnumerateFileSystemInfos(this.FullPath, searchPattern, searchOption);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000A307B File Offset: 0x000A127B
		internal static IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string basePath, string searchPattern, SearchOption searchOption)
		{
			Path.Validate(basePath);
			SafeFindHandle findHandle = null;
			try
			{
				string text = Path.Combine(basePath, searchPattern);
				string text2;
				int num;
				int num2;
				try
				{
				}
				finally
				{
					findHandle = new SafeFindHandle(MonoIO.FindFirstFile(text, out text2, out num, out num2));
				}
				if (!findHandle.IsInvalid)
				{
					while (text2 != null)
					{
						if (!(text2 == ".") && !(text2 == ".."))
						{
							FileAttributes attrs = (FileAttributes)num;
							string fullPath = Path.Combine(basePath, text2);
							if ((attrs & FileAttributes.ReparsePoint) == (FileAttributes)0)
							{
								if ((attrs & FileAttributes.Directory) != (FileAttributes)0)
								{
									yield return new DirectoryInfo(fullPath);
								}
								else
								{
									yield return new FileInfo(fullPath);
								}
							}
							if ((attrs & FileAttributes.Directory) != (FileAttributes)0 && searchOption == SearchOption.AllDirectories)
							{
								foreach (FileSystemInfo fileSystemInfo in DirectoryInfo.EnumerateFileSystemInfos(fullPath, searchPattern, searchOption))
								{
									yield return fileSystemInfo;
								}
								IEnumerator<FileSystemInfo> enumerator = null;
							}
							fullPath = null;
						}
						int num3;
						if (!MonoIO.FindNextFile(findHandle.DangerousGetHandle(), out text2, out num, out num3))
						{
							goto JumpOutOfTryFinally-3;
						}
					}
					yield break;
				}
				MonoIOError monoIOError = (MonoIOError)num2;
				if (monoIOError != MonoIOError.ERROR_FILE_NOT_FOUND)
				{
					throw MonoIO.GetException(Path.GetDirectoryName(text), monoIOError);
				}
				yield break;
			}
			finally
			{
				if (findHandle != null)
				{
					findHandle.Dispose();
				}
			}
			JumpOutOfTryFinally-3:
			yield break;
			yield break;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000A309C File Offset: 0x000A129C
		internal void CheckPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (Environment.IsRunningOnWindows)
			{
				int num = path.IndexOf(':');
				if (num >= 0 && num != 1)
				{
					throw new ArgumentException("path");
				}
			}
		}

		// Token: 0x04001794 RID: 6036
		private string current;

		// Token: 0x04001795 RID: 6037
		private string parent;
	}
}
