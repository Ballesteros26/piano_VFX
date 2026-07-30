using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>Provides access to information on a drive.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003CE RID: 974
	[ComVisible(true)]
	[Serializable]
	public sealed class DriveInfo : ISerializable
	{
		// Token: 0x06002DC5 RID: 11717 RVA: 0x000A37CF File Offset: 0x000A19CF
		private DriveInfo(string path, string fstype)
		{
			this.drive_format = fstype;
			this.path = path;
		}

		/// <summary>Provides access to information on the specified drive.</summary>
		/// <param name="driveName">A valid drive path or drive letter. This can be either uppercase or lowercase, 'a' to 'z'. A null value is not valid. </param>
		/// <exception cref="T:System.ArgumentNullException">The drive letter cannot be null. </exception>
		/// <exception cref="T:System.ArgumentException">The first letter of <paramref name="driveName" /> is not an uppercase or lowercase letter from 'a' to 'z'.-or-<paramref name="driveName" /> does not refer to a valid drive.</exception>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000A37E8 File Offset: 0x000A19E8
		public DriveInfo(string driveName)
		{
			if (!Environment.IsUnix)
			{
				if (driveName == null || driveName.Length == 0)
				{
					throw new ArgumentException("The drive name is null or empty", "driveName");
				}
				if (driveName.Length >= 2 && driveName[1] != ':')
				{
					throw new ArgumentException("Invalid drive name", "driveName");
				}
				driveName = char.ToUpperInvariant(driveName[0]).ToString() + ":\\";
			}
			foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
			{
				if (driveInfo.path == driveName)
				{
					this.path = driveInfo.path;
					this.drive_format = driveInfo.drive_format;
					this.path = driveInfo.path;
					return;
				}
			}
			throw new ArgumentException("The drive name does not exist", "driveName");
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000A38BC File Offset: 0x000A1ABC
		private static void GetDiskFreeSpace(string path, out ulong availableFreeSpace, out ulong totalSize, out ulong totalFreeSpace)
		{
			MonoIOError monoIOError;
			if (!DriveInfo.GetDiskFreeSpaceInternal(path, out availableFreeSpace, out totalSize, out totalFreeSpace, out monoIOError))
			{
				throw MonoIO.GetException(path, monoIOError);
			}
		}

		/// <summary>Indicates the amount of available free space on a drive.</summary>
		/// <returns>The amount of free space available on the drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000A38E0 File Offset: 0x000A1AE0
		public long AvailableFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num <= 9223372036854775807UL)
				{
					return (long)num;
				}
				return long.MaxValue;
			}
		}

		/// <summary>Gets the total amount of free space available on a drive.</summary>
		/// <returns>The total free space available on a drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">The drive is not mapped or does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x000A3918 File Offset: 0x000A1B18
		public long TotalFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num3 <= 9223372036854775807UL)
				{
					return (long)num3;
				}
				return long.MaxValue;
			}
		}

		/// <summary>Gets the total size of storage space on a drive.</summary>
		/// <returns>The total size of the drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">The drive is not mapped or does not exist. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000A3950 File Offset: 0x000A1B50
		public long TotalSize
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				if (num2 <= 9223372036854775807UL)
				{
					return (long)num2;
				}
				return long.MaxValue;
			}
		}

		/// <summary>Gets or sets the volume label of a drive.</summary>
		/// <returns>The volume label.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">The drive is not mapped or does not exist.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The volume label is being set on a network or CD-ROM drive.-or-Access to the drive information is denied.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000A3985 File Offset: 0x000A1B85
		// (set) Token: 0x06002DCC RID: 11724 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Currently get only works on Mono/Unix; set not implemented")]
		public string VolumeLabel
		{
			get
			{
				return this.path;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the file system, such as NTFS or FAT32.</summary>
		/// <returns>The name of the file system on the specified drive.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">The drive does not exist or is not mapped.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000A398D File Offset: 0x000A1B8D
		public string DriveFormat
		{
			get
			{
				return this.drive_format;
			}
		}

		/// <summary>Gets the drive type.</summary>
		/// <returns>One of the <see cref="T:System.IO.DriveType" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000A3995 File Offset: 0x000A1B95
		public DriveType DriveType
		{
			get
			{
				return (DriveType)DriveInfo.GetDriveTypeInternal(this.path);
			}
		}

		/// <summary>Gets the name of a drive.</summary>
		/// <returns>The name of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000A3985 File Offset: 0x000A1B85
		public string Name
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the root directory of a drive.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object that contains the root directory of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000A39A2 File Offset: 0x000A1BA2
		public DirectoryInfo RootDirectory
		{
			get
			{
				return new DirectoryInfo(this.path);
			}
		}

		/// <summary>Gets a value indicating whether a drive is ready.</summary>
		/// <returns>true if the drive is ready; false if the drive is not ready.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000A39AF File Offset: 0x000A1BAF
		public bool IsReady
		{
			get
			{
				return Directory.Exists(this.Name);
			}
		}

		/// <summary>Retrieves the drive names of all logical drives on a computer.</summary>
		/// <returns>An array of type <see cref="T:System.IO.DriveInfo" /> that represents the logical drives on a computer.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000A39BC File Offset: 0x000A1BBC
		[MonoTODO("In windows, alldrives are 'Fixed'")]
		public static DriveInfo[] GetDrives()
		{
			string[] logicalDrives = Environment.GetLogicalDrives();
			DriveInfo[] array = new DriveInfo[logicalDrives.Length];
			int num = 0;
			foreach (string text in logicalDrives)
			{
				array[num++] = new DriveInfo(text, DriveInfo.GetDriveFormat(text));
			}
			return array;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x0002126B File Offset: 0x0001F46B
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a drive name as a string.</summary>
		/// <returns>The name of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000A3A03 File Offset: 0x000A1C03
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002DD5 RID: 11733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDiskFreeSpaceInternal(string pathName, out ulong freeBytesAvail, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes, out MonoIOError error);

		// Token: 0x06002DD6 RID: 11734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetDriveTypeInternal(string rootPathName);

		// Token: 0x06002DD7 RID: 11735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetDriveFormat(string rootPathName);

		// Token: 0x040017B6 RID: 6070
		private string drive_format;

		// Token: 0x040017B7 RID: 6071
		private string path;
	}
}
