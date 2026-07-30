using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Text;

namespace System.IO
{
	/// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000392 RID: 914
	[ComVisible(true)]
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileInfo" /> class, which acts as a wrapper for a file path.</summary>
		/// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="fileName" /> contains a colon (:) in the middle of the string. </exception>
		// Token: 0x06002A85 RID: 10885 RVA: 0x0009805A File Offset: 0x0009625A
		[SecuritySafeCritical]
		public FileInfo(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Init(fileName, true);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00098078 File Offset: 0x00096278
		[SecurityCritical]
		private void Init(string fileName, bool checkHost)
		{
			this.OriginalPath = fileName;
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			this._name = Path.GetFileName(fileName);
			this.FullPath = fullPathInternal;
			base.DisplayPath = this.GetDisplayPath(fileName);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x0000213D File Offset: 0x0000033D
		private string GetDisplayPath(string originalPath)
		{
			return originalPath;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000980B3 File Offset: 0x000962B3
		[SecurityCritical]
		private FileInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._name = Path.GetFileName(this.OriginalPath);
			base.DisplayPath = this.GetDisplayPath(this.OriginalPath);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000980E0 File Offset: 0x000962E0
		internal FileInfo(string fullPath, bool ignoreThis)
		{
			this._name = Path.GetFileName(fullPath);
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
			base.DisplayPath = this._name;
		}

		/// <summary>Gets the name of the file.</summary>
		/// <returns>The name of the file.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00098113 File Offset: 0x00096313
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the size, in bytes, of the current file.</summary>
		/// <returns>The size of the current file in bytes.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.-or- The Length property is called for a directory. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x0009811C File Offset: 0x0009631C
		public long Length
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					base.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, base.DisplayPath);
				}
				if ((this._data.fileAttributes & FileAttributes.Directory) != (FileAttributes)0)
				{
					__Error.WinIOError(2, base.DisplayPath);
				}
				return this._data.Length;
			}
		}

		/// <summary>Gets a string representing the directory's full path.</summary>
		/// <returns>A string representing the directory's full path.</returns>
		/// <exception cref="T:System.ArgumentNullException">null was passed in for the directory name. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x00098178 File Offset: 0x00096378
		public string DirectoryName
		{
			[SecuritySafeCritical]
			get
			{
				return Path.GetDirectoryName(this.FullPath);
			}
		}

		/// <summary>Gets an instance of the parent directory.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object representing the parent directory of this file.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x00098188 File Offset: 0x00096388
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		/// <summary>Gets or sets a value that determines if the current file is read only.</summary>
		/// <returns>true if the current file is read only; otherwise, false.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The user does not have write permission, but attempted to set this property to false.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000981A7 File Offset: 0x000963A7
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x000981B4 File Offset: 0x000963B4
		public bool IsReadOnly
		{
			get
			{
				return (base.Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
			}
			set
			{
				if (value)
				{
					base.Attributes |= FileAttributes.ReadOnly;
					return;
				}
				base.Attributes &= ~FileAttributes.ReadOnly;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the current file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">The current system account does not have administrative privileges.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A90 RID: 10896 RVA: 0x000981D7 File Offset: 0x000963D7
		public FileSecurity GetAccessControl()
		{
			return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the current file.     </returns>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies which group of access control entries to retrieve. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">The current system account does not have administrative privileges.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A91 RID: 10897 RVA: 0x000981E6 File Offset: 0x000963E6
		public FileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return File.GetAccessControl(this.FullPath, includeSections);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the file described by the current <see cref="T:System.IO.FileInfo" /> object.</summary>
		/// <param name="fileSecurity">A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that describes an access control list (ACL) entry to apply to the current file.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A92 RID: 10898 RVA: 0x000981F4 File Offset: 0x000963F4
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			File.SetAccessControl(this.FullPath, fileSecurity);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamReader" /> with UTF8 encoding that reads from an existing text file.</summary>
		/// <returns>A new StreamReader with UTF8 encoding.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A93 RID: 10899 RVA: 0x00098202 File Offset: 0x00096402
		[SecuritySafeCritical]
		public StreamReader OpenText()
		{
			return new StreamReader(this.FullPath, Encoding.UTF8, true, StreamReader.DefaultBufferSize, false);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that writes a new text file.</summary>
		/// <returns>A new StreamWriter.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The file name is a directory. </exception>
		/// <exception cref="T:System.IO.IOException">The disk is read-only. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A94 RID: 10900 RVA: 0x0009821B File Offset: 0x0009641B
		public StreamWriter CreateText()
		{
			return new StreamWriter(this.FullPath, false);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="T:System.IO.FileInfo" />.</summary>
		/// <returns>A new StreamWriter.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A95 RID: 10901 RVA: 0x00098229 File Offset: 0x00096429
		public StreamWriter AppendText()
		{
			return new StreamWriter(this.FullPath, true);
		}

		/// <summary>Copies an existing file to a new file, disallowing the overwriting of an existing file.</summary>
		/// <returns>A new file with a fully qualified path.</returns>
		/// <param name="destFileName">The name of the new file to copy to. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) within the string but does not specify the volume. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A96 RID: 10902 RVA: 0x00098238 File Offset: 0x00096438
		public FileInfo CopyTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("File name cannot be null."));
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty file name is not legal."), "destFileName");
			}
			destFileName = File.InternalCopy(this.FullPath, destFileName, false, true);
			return new FileInfo(destFileName, false);
		}

		/// <summary>Copies an existing file to a new file, allowing the overwriting of an existing file.</summary>
		/// <returns>A new file, or an overwrite of an existing file if <paramref name="overwrite" /> is true. If the file exists and <paramref name="overwrite" /> is false, an <see cref="T:System.IO.IOException" /> is thrown.</returns>
		/// <param name="destFileName">The name of the new file to copy to. </param>
		/// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurs, or the destination file already exists and <paramref name="overwrite" /> is false. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) in the middle of the string. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A97 RID: 10903 RVA: 0x00098294 File Offset: 0x00096494
		public FileInfo CopyTo(string destFileName, bool overwrite)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("File name cannot be null."));
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty file name is not legal."), "destFileName");
			}
			destFileName = File.InternalCopy(this.FullPath, destFileName, overwrite, true);
			return new FileInfo(destFileName, false);
		}

		/// <summary>Creates a file.</summary>
		/// <returns>A new file.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A98 RID: 10904 RVA: 0x000982ED File Offset: 0x000964ED
		public FileStream Create()
		{
			return File.Create(this.FullPath);
		}

		/// <summary>Permanently deletes a file.</summary>
		/// <exception cref="T:System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.-or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The path is a directory. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A99 RID: 10905 RVA: 0x000982FC File Offset: 0x000964FC
		[SecuritySafeCritical]
		public override void Delete()
		{
			MonoIOError monoIOError;
			if (MonoIO.ExistsDirectory(this.FullPath, out monoIOError))
			{
				__Error.WinIOError(5, base.DisplayPath);
			}
			if (!MonoIO.DeleteFile(this.FullPath, out monoIOError))
			{
				int num = (int)monoIOError;
				if (num == 2)
				{
					return;
				}
				__Error.WinIOError(num, base.DisplayPath);
			}
		}

		/// <summary>Decrypts a file that was encrypted by the current account using the <see cref="M:System.IO.FileInfo.Encrypt" /> method.</summary>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file described by the current <see cref="T:System.IO.FileInfo" /> object is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A9A RID: 10906 RVA: 0x00098346 File Offset: 0x00096546
		[ComVisible(false)]
		public void Decrypt()
		{
			File.Decrypt(this.FullPath);
		}

		/// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file described by the current <see cref="T:System.IO.FileInfo" /> object is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A9B RID: 10907 RVA: 0x00098353 File Offset: 0x00096553
		[ComVisible(false)]
		public void Encrypt()
		{
			File.Encrypt(this.FullPath);
		}

		/// <summary>Gets a value indicating whether a file exists.</summary>
		/// <returns>true if the file exists; false if the file does not exist or if the file is a directory.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x00098360 File Offset: 0x00096560
		public override bool Exists
		{
			[SecuritySafeCritical]
			get
			{
				bool flag;
				try
				{
					if (this._dataInitialised == -1)
					{
						base.Refresh();
					}
					if (this._dataInitialised != 0)
					{
						flag = false;
					}
					else
					{
						flag = (this._data.fileAttributes & FileAttributes.Directory) == (FileAttributes)0;
					}
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		/// <summary>Opens a file in the specified mode.</summary>
		/// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The file is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file is already open. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A9D RID: 10909 RVA: 0x000983B4 File Offset: 0x000965B4
		public FileStream Open(FileMode mode)
		{
			return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>Opens a file in the specified mode with read, write, or read/write access.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> object opened in the specified mode and access, and unshared.</returns>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with Read, Write, or ReadWrite file access. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty or contains only white spaces. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
		/// <exception cref="T:System.ArgumentNullException">One or more arguments is null. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file is already open. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A9E RID: 10910 RVA: 0x000983BF File Offset: 0x000965BF
		public FileStream Open(FileMode mode, FileAccess access)
		{
			return this.Open(mode, access, FileShare.None);
		}

		/// <summary>Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> object opened with the specified mode, access, and sharing options.</returns>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> constant specifying the mode (for example, Open or Append) in which to open the file. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant specifying whether to open the file with Read, Write, or ReadWrite file access. </param>
		/// <param name="share">A <see cref="T:System.IO.FileShare" /> constant specifying the type of access other FileStream objects have to this file. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty or contains only white spaces. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
		/// <exception cref="T:System.ArgumentNullException">One or more arguments is null. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file is already open. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A9F RID: 10911 RVA: 0x000983CA File Offset: 0x000965CA
		public FileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(this.FullPath, mode, access, share);
		}

		/// <summary>Creates a read-only <see cref="T:System.IO.FileStream" />.</summary>
		/// <returns>A new read-only <see cref="T:System.IO.FileStream" /> object.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file is already open. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000983DA File Offset: 0x000965DA
		public FileStream OpenRead()
		{
			return new FileStream(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		/// <summary>Creates a write-only <see cref="T:System.IO.FileStream" />.</summary>
		/// <returns>A write-only unshared <see cref="T:System.IO.FileStream" /> object for a new or existing file.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is read-only or is a directory.  </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified when creating an instance of the <see cref="T:System.IO.FileInfo" /> object is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002AA1 RID: 10913 RVA: 0x000983F0 File Offset: 0x000965F0
		public FileStream OpenWrite()
		{
			return new FileStream(this.FullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		/// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
		/// <param name="destFileName">The path to move the file to, which can specify a different file name. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the destination file already exists or the destination device is not ready. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destFileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="destFileName" /> is read-only or is a directory. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file is not found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destFileName" /> contains a colon (:) in the middle of the string. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002AA2 RID: 10914 RVA: 0x00098400 File Offset: 0x00096600
		[SecuritySafeCritical]
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty file name is not legal."), "destFileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(destFileName);
			MonoIOError monoIOError;
			if (!MonoIO.MoveFile(this.FullPath, fullPathInternal, out monoIOError))
			{
				__Error.WinIOError((int)monoIOError, string.Empty);
			}
			this.FullPath = fullPathInternal;
			this.OriginalPath = destFileName;
			this._name = Path.GetFileName(fullPathInternal);
			base.DisplayPath = this.GetDisplayPath(destFileName);
			this._dataInitialised = -1;
		}

		/// <summary>Replaces the contents of a specified file with the file described by the current <see cref="T:System.IO.FileInfo" /> object, deleting the original file, and creating a backup of the replaced file.</summary>
		/// <returns>A <see cref="T:System.IO.FileInfo" /> object that encapsulates information about the file described by the <paramref name="destFileName" /> parameter.</returns>
		/// <param name="destinationFileName">The name of a file to replace with the current file.</param>
		/// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destFileName" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destFileName" /> parameter was not of a legal form.-or-The path described by the <paramref name="destBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destFileName" /> parameter is null.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.-or-The file described by the <paramref name="destinationFileName" /> parameter could not be found. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002AA3 RID: 10915 RVA: 0x00098488 File Offset: 0x00096688
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			return this.Replace(destinationFileName, destinationBackupFileName, false);
		}

		/// <summary>Replaces the contents of a specified file with the file described by the current <see cref="T:System.IO.FileInfo" /> object, deleting the original file, and creating a backup of the replaced file.  Also specifies whether to ignore merge errors. </summary>
		/// <returns>A <see cref="T:System.IO.FileInfo" /> object that encapsulates information about the file described by the <paramref name="destFileName" /> parameter.</returns>
		/// <param name="destinationFileName">The name of a file to replace with the current file.</param>
		/// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destFileName" /> parameter.</param>
		/// <param name="ignoreMetadataErrors">true to ignore merge errors (such as attributes and ACLs) from the replaced file to the replacement file; otherwise false. </param>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destFileName" /> parameter was not of a legal form.-or-The path described by the <paramref name="destBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destFileName" /> parameter is null.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.-or-The file described by the <paramref name="destinationFileName" /> parameter could not be found. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002AA4 RID: 10916 RVA: 0x00098493 File Offset: 0x00096693
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			File.Replace(this.FullPath, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
			return new FileInfo(destinationFileName);
		}

		/// <summary>Returns the path as a string.</summary>
		/// <returns>A string representing the path.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002AA5 RID: 10917 RVA: 0x000984A9 File Offset: 0x000966A9
		public override string ToString()
		{
			return base.DisplayPath;
		}

		// Token: 0x04001681 RID: 5761
		private string _name;
	}
}
