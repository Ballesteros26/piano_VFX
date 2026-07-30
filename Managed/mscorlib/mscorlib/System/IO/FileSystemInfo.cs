using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	/// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039F RID: 927
	[ComVisible(true)]
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
		// Token: 0x06002AF1 RID: 10993 RVA: 0x00099436 File Offset: 0x00097636
		protected FileSystemInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> is null.</exception>
		// Token: 0x06002AF2 RID: 10994 RVA: 0x00099450 File Offset: 0x00097650
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
			this.OriginalPath = info.GetString("OriginalPath");
			this._dataInitialised = -1;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		internal void InitializeFrom(Win32Native.WIN32_FIND_DATA findData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the full path of the directory or file.</summary>
		/// <returns>A string containing the full path.</returns>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000994B1 File Offset: 0x000976B1
		public virtual string FullName
		{
			[SecuritySafeCritical]
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000994B1 File Offset: 0x000976B1
		internal virtual string UnsafeGetFullName
		{
			[SecurityCritical]
			get
			{
				return this.FullPath;
			}
		}

		/// <summary>Gets the string representing the extension part of the file.</summary>
		/// <returns>A string containing the <see cref="T:System.IO.FileSystemInfo" /> extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000994BC File Offset: 0x000976BC
		public string Extension
		{
			get
			{
				int length = this.FullPath.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = this.FullPath[num];
					if (c == '.')
					{
						return this.FullPath.Substring(num, length - num);
					}
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
					{
						break;
					}
				}
				return string.Empty;
			}
		}

		/// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the Name property gets the name of the directory.</summary>
		/// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002AF7 RID: 10999
		public abstract string Name { get; }

		/// <summary>Gets a value indicating whether the file or directory exists.</summary>
		/// <returns>true if the file or directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002AF8 RID: 11000
		public abstract bool Exists { get; }

		/// <summary>Deletes a file or directory.</summary>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">There is an open handle on the file or directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002AF9 RID: 11001
		public abstract void Delete();

		/// <summary>Gets or sets the creation time of the current file or directory.</summary>
		/// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x00099520 File Offset: 0x00097720
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0009953B File Offset: 0x0009773B
		public DateTime CreationTime
		{
			get
			{
				return this.CreationTimeUtc.ToLocalTime();
			}
			set
			{
				this.CreationTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
		/// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0009954A File Offset: 0x0009774A
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x00099584 File Offset: 0x00097784
		[ComVisible(false)]
		public DateTime CreationTimeUtc
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					this.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, this.DisplayPath);
				}
				return DateTime.FromFileTimeUtc(this._data.CreationTime);
			}
			set
			{
				if (this is DirectoryInfo)
				{
					Directory.SetCreationTimeUtc(this.FullPath, value);
				}
				else
				{
					File.SetCreationTimeUtc(this.FullPath, value);
				}
				this._dataInitialised = -1;
			}
		}

		/// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
		/// <returns>The time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000995B0 File Offset: 0x000977B0
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000995CB File Offset: 0x000977CB
		public DateTime LastAccessTime
		{
			get
			{
				return this.LastAccessTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastAccessTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
		/// <returns>The UTC time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000995DA File Offset: 0x000977DA
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x00099614 File Offset: 0x00097814
		[ComVisible(false)]
		public DateTime LastAccessTimeUtc
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					this.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, this.DisplayPath);
				}
				return DateTime.FromFileTimeUtc(this._data.LastAccessTime);
			}
			set
			{
				if (this is DirectoryInfo)
				{
					Directory.SetLastAccessTimeUtc(this.FullPath, value);
				}
				else
				{
					File.SetLastAccessTimeUtc(this.FullPath, value);
				}
				this._dataInitialised = -1;
			}
		}

		/// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
		/// <returns>The time the current file was last written.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x00099640 File Offset: 0x00097840
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0009965B File Offset: 0x0009785B
		public DateTime LastWriteTime
		{
			get
			{
				return this.LastWriteTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastWriteTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
		/// <returns>The UTC time when the current file was last written to.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0009966A File Offset: 0x0009786A
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000996A4 File Offset: 0x000978A4
		[ComVisible(false)]
		public DateTime LastWriteTimeUtc
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					this.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, this.DisplayPath);
				}
				return DateTime.FromFileTimeUtc(this._data.LastWriteTime);
			}
			set
			{
				if (this is DirectoryInfo)
				{
					Directory.SetLastWriteTimeUtc(this.FullPath, value);
				}
				else
				{
					File.SetLastWriteTimeUtc(this.FullPath, value);
				}
				this._dataInitialised = -1;
			}
		}

		/// <summary>Refreshes the state of the object.</summary>
		/// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002B06 RID: 11014 RVA: 0x000996CF File Offset: 0x000978CF
		[SecuritySafeCritical]
		public void Refresh()
		{
			this._dataInitialised = File.FillAttributeInfo(this.FullPath, ref this._data, false, false);
		}

		/// <summary>Gets or sets the attributes for the current file or directory.</summary>
		/// <returns>
		///   <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute. -or-The user attempts to set an attribute value but does not have write permission.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000996EA File Offset: 0x000978EA
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x00099720 File Offset: 0x00097920
		public FileAttributes Attributes
		{
			[SecuritySafeCritical]
			get
			{
				if (this._dataInitialised == -1)
				{
					this.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, this.DisplayPath);
				}
				return this._data.fileAttributes;
			}
			[SecuritySafeCritical]
			set
			{
				MonoIOError monoIOError;
				if (!MonoIO.SetFileAttributes(this.FullPath, value, out monoIOError))
				{
					MonoIOError monoIOError2 = monoIOError;
					if (monoIOError2 == MonoIOError.ERROR_INVALID_PARAMETER)
					{
						throw new ArgumentException(Environment.GetResourceString("Invalid File or Directory attributes value."));
					}
					if (monoIOError2 == MonoIOError.ERROR_ACCESS_DENIED)
					{
						throw new ArgumentException(Environment.GetResourceString("Access to the path is denied."));
					}
					__Error.WinIOError((int)monoIOError2, this.DisplayPath);
				}
				this._dataInitialised = -1;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002B09 RID: 11017 RVA: 0x00099779 File Offset: 0x00097979
		[SecurityCritical]
		[ComVisible(false)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000997B1 File Offset: 0x000979B1
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x000997B9 File Offset: 0x000979B9
		internal string DisplayPath
		{
			get
			{
				return this._displayPath;
			}
			set
			{
				this._displayPath = value;
			}
		}

		// Token: 0x0400169E RID: 5790
		internal MonoIOStat _data;

		// Token: 0x0400169F RID: 5791
		internal int _dataInitialised = -1;

		// Token: 0x040016A0 RID: 5792
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x040016A1 RID: 5793
		internal const int ERROR_ACCESS_DENIED = 5;

		/// <summary>Represents the fully qualified path of the directory or file.</summary>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		// Token: 0x040016A2 RID: 5794
		protected string FullPath;

		/// <summary>The path originally specified by the user, whether relative or absolute.</summary>
		// Token: 0x040016A3 RID: 5795
		protected string OriginalPath;

		// Token: 0x040016A4 RID: 5796
		private string _displayPath = "";
	}
}
