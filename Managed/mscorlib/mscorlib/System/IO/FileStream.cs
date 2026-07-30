using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a file, supporting both synchronous and asynchronous read and write operations.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020003D7 RID: 983
	[ComVisible(true)]
	public class FileStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E1F RID: 11807 RVA: 0x000A49D3 File Offset: 0x000A2BD3
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access)
			: this(handle, access, true, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission and FileStream instance ownership.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E20 RID: 11808 RVA: 0x000A49E5 File Offset: 0x000A2BE5
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle)
			: this(handle, access, ownsHandle, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, FileStream instance ownership, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E21 RID: 11809 RVA: 0x000A49F7 File Offset: 0x000A2BF7
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize)
			: this(handle, access, ownsHandle, bufferSize, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, FileStream instance ownership, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="isAsync">true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="access" /> is less than FileAccess.Read or greater than FileAccess.ReadWrite or <paramref name="bufferSize" /> is less than or equal to 0. </exception>
		/// <exception cref="T:System.ArgumentException">The handle is invalid. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E22 RID: 11810 RVA: 0x000A4A06 File Offset: 0x000A2C06
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
			: this(handle, access, ownsHandle, bufferSize, isAsync, false)
		{
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000A4A18 File Offset: 0x000A2C18
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		internal FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (handle == MonoIO.InvalidHandle)
			{
				throw new ArgumentException("handle", Locale.GetText("Invalid."));
			}
			this.Init(new SafeFileHandle(handle, false), access, ownsHandle, bufferSize, isAsync, isConsoleWrapper);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path and creation mode.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06002E24 RID: 11812 RVA: 0x000A4A6D File Offset: 0x000A2C6D
		public FileStream(string path, FileMode mode)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, and read/write permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06002E25 RID: 11813 RVA: 0x000A4A87 File Offset: 0x000A2C87
		public FileStream(string path, FileMode mode, FileAccess access)
			: this(path, mode, access, (access == FileAccess.Write) ? FileShare.None : FileShare.Read, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write permission, and sharing permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06002E26 RID: 11814 RVA: 0x000A4AA1 File Offset: 0x000A2CA1
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: this(path, mode, access, share, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, and buffer size.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06002E27 RID: 11815 RVA: 0x000A4AB5 File Offset: 0x000A2CB5
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: this(path, mode, access, share, bufferSize, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="useAsync">Specifies whether to use asynchronous I/O or synchronous I/O. However, note that the underlying operating system might not support asynchronous I/O, so when specifying true, the handle might be opened synchronously depending on the platform. When opened asynchronously, the <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> and <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> methods perform better on large reads or writes, but they might be much slower for small reads or writes. If the application is designed to take advantage of asynchronous I/O, set the <paramref name="useAsync" /> parameter to true. Using asynchronous I/O correctly can speed up applications by as much as a factor of 10, but using it without redesigning the application for asynchronous I/O can decrease performance by as much as a factor of 10. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or- The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06002E28 RID: 11816 RVA: 0x000A4AC6 File Offset: 0x000A2CC6
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
			: this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="options">A value that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06002E29 RID: 11817 RVA: 0x000A4AE1 File Offset: 0x000A2CE1
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
			: this(path, mode, access, share, bufferSize, false, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission. </summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000A4AF3 File Offset: 0x000A2CF3
		public FileStream(SafeFileHandle handle, FileAccess access)
			: this(handle, access, 4096, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed.  </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000A4B03 File Offset: 0x000A2D03
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize)
			: this(handle, access, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="isAsync">true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.-or-The stream has been closed.  </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06002E2C RID: 11820 RVA: 0x000A4B0F File Offset: 0x000A2D0F
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
		{
			this.name = "[Unknown]";
			base..ctor();
			this.Init(handle, access, false, bufferSize, isAsync, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000A4B2F File Offset: 0x000A2D2F
		[MonoLimitation("This ignores the rights parameter")]
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, share, bufferSize, false, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, additional file options, access control and audit security.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <param name="fileSecurity">A constant that determines the access control and audit security for the file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying FileMode.CreateNew when the file specified by <paramref name="path" /> already exists, occurred. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		// Token: 0x06002E2E RID: 11822 RVA: 0x000A4B2F File Offset: 0x000A2D2F
		[MonoLimitation("This ignores the rights and fileSecurity parameters")]
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, share, bufferSize, false, options)
		{
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000A4AE1 File Offset: 0x000A2CE1
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath = false, bool checkHost = false)
			: this(path, mode, access, share, bufferSize, false, options)
		{
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000A4B48 File Offset: 0x000A2D48
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool isAsync, bool anonymous)
			: this(path, mode, access, share, bufferSize, anonymous, isAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000A4B68 File Offset: 0x000A2D68
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool anonymous, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			this.anonymous = anonymous;
			share &= ~FileShare.Inheritable;
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				throw new ArgumentOutOfRangeException("mode", "Enum value was out of legal range.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if ((share < FileShare.None) || share > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				throw new ArgumentOutOfRangeException("share", "Enum value was out of legal range.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Name has invalid chars");
			}
			path = Path.InsecureGetFullPath(path);
			if (Directory.Exists(path))
			{
				throw new UnauthorizedAccessException(string.Format(Locale.GetText("Access to the path '{0}' is denied."), this.GetSecureFileName(path, false)));
			}
			if (mode == FileMode.Append && (access & FileAccess.Read) == FileAccess.Read)
			{
				throw new ArgumentException("Append access can be requested only in write-only mode.");
			}
			if ((access & FileAccess.Write) == (FileAccess)0 && mode != FileMode.Open && mode != FileMode.OpenOrCreate)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Combining FileMode: {0} with FileAccess: {1} is invalid."), access, mode));
			}
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName.Length > 0 && !Directory.Exists(Path.GetFullPath(directoryName)))
			{
				string text = Locale.GetText("Could not find a part of the path \"{0}\".");
				string text2 = (anonymous ? directoryName : Path.GetFullPath(path));
				throw new DirectoryNotFoundException(string.Format(text, text2));
			}
			if (!anonymous)
			{
				this.name = path;
			}
			MonoIOError monoIOError;
			IntPtr intPtr = MonoIO.Open(path, mode, access, share, options, out monoIOError);
			if (intPtr == MonoIO.InvalidHandle)
			{
				throw MonoIO.GetException(this.GetSecureFileName(path), monoIOError);
			}
			this.safeHandle = new SafeFileHandle(intPtr, false);
			this.access = access;
			this.owner = true;
			if (MonoIO.GetFileType(this.safeHandle, out monoIOError) == MonoFileType.Disk)
			{
				this.canseek = true;
				this.async = (options & FileOptions.Asynchronous) > FileOptions.None;
			}
			else
			{
				this.canseek = false;
				this.async = false;
			}
			if (access == FileAccess.Read && this.canseek && bufferSize == 4096)
			{
				long length = this.Length;
				if ((long)bufferSize > length)
				{
					bufferSize = (int)((length < 1000L) ? 1000L : length);
				}
			}
			this.InitBuffer(bufferSize, false);
			if (mode == FileMode.Append)
			{
				this.Seek(0L, SeekOrigin.End);
				this.append_startpos = this.Position;
				return;
			}
			this.append_startpos = 0L;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000A4DD0 File Offset: 0x000A2FD0
		private void Init(SafeFileHandle safeHandle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			if (!isConsoleWrapper && safeHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid handle."), "handle");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (!isConsoleWrapper && bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			MonoIOError monoIOError;
			MonoFileType fileType = MonoIO.GetFileType(safeHandle, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.name, monoIOError);
			}
			if (fileType == MonoFileType.Unknown)
			{
				throw new IOException("Invalid handle.");
			}
			if (fileType == MonoFileType.Disk)
			{
				this.canseek = true;
			}
			else
			{
				this.canseek = false;
			}
			this.safeHandle = safeHandle;
			this.ExposeHandle();
			this.access = access;
			this.owner = ownsHandle;
			this.async = isAsync;
			this.anonymous = false;
			if (this.canseek)
			{
				this.buf_start = MonoIO.Seek(safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.name, monoIOError);
				}
			}
			this.append_startpos = 0L;
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; false if the stream is closed or was opened with write-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000A4EC5 File Offset: 0x000A30C5
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.Read || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; false if the stream is closed or was opened with read-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000A4EDB File Offset: 0x000A30DB
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.Write || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; false if the stream is closed or if the FileStream was constructed from an operating-system handle such as a pipe or output to the console.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000A4EF1 File Offset: 0x000A30F1
		public override bool CanSeek
		{
			get
			{
				return this.canseek;
			}
		}

		/// <summary>Gets a value indicating whether the FileStream was opened asynchronously or synchronously.</summary>
		/// <returns>true if the FileStream was opened asynchronously; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000A4EF9 File Offset: 0x000A30F9
		public virtual bool IsAsync
		{
			get
			{
				return this.async;
			}
		}

		/// <summary>Gets the name of the FileStream that was passed to the constructor.</summary>
		/// <returns>A string that is the name of the FileStream.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000A4F01 File Offset: 0x000A3101
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.FileStream.CanSeek" /> for this stream is false. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as the file being closed, occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000A4F0C File Offset: 0x000A310C
		public override long Length
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				this.FlushBufferIfDirty();
				MonoIOError monoIOError;
				long length = MonoIO.GetLength(this.safeHandle, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return length;
			}
		}

		/// <summary>Gets or sets the current position of this stream.</summary>
		/// <returns>The current position of this stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. - or -The position was set to a very large value beyond the end of the stream in Windows 98 or earlier.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the position to a negative value. </exception>
		/// <exception cref="T:System.IO.EndOfStreamException">Attempted seeking past the end of a stream that does not support this. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x000A4F70 File Offset: 0x000A3170
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x000A4FE5 File Offset: 0x000A31E5
		public override long Position
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				if (!this.isExposed)
				{
					return this.buf_start + (long)this.buf_offset;
				}
				MonoIOError monoIOError;
				long num = MonoIO.Seek(this.safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return num;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>Gets the operating system file handle for the file that the current FileStream object encapsulates.</summary>
		/// <returns>The operating system file handle for the file encapsulated by this FileStream object, or -1 if the FileStream has been closed.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000A500A File Offset: 0x000A320A
		[Obsolete("Use SafeFileHandle instead")]
		public virtual IntPtr Handle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
			get
			{
				IntPtr intPtr = this.safeHandle.DangerousGetHandle();
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return intPtr;
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</summary>
		/// <returns>An object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000A5025 File Offset: 0x000A3225
		public virtual SafeFileHandle SafeFileHandle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
			get
			{
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return this.safeHandle;
			}
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000A503B File Offset: 0x000A323B
		private void ExposeHandle()
		{
			this.isExposed = true;
			this.FlushBuffer();
			this.InitBuffer(0, true);
		}

		/// <summary>Reads a byte from the file and advances the read position one byte.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000A5054 File Offset: 0x000A3254
		public override int ReadByte()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.buf_size != 0)
			{
				if (this.buf_offset >= this.buf_length)
				{
					this.RefillBuffer();
					if (this.buf_length == 0)
					{
						return -1;
					}
				}
				byte[] array = this.buf;
				int num = this.buf_offset;
				this.buf_offset = num + 1;
				return array[num];
			}
			if (this.ReadData(this.safeHandle, this.buf, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.buf[0];
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte to write to the stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000A50EC File Offset: 0x000A32EC
		public override void WriteByte(byte value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.buf_offset == this.buf_size)
			{
				this.FlushBuffer();
			}
			if (this.buf_size == 0)
			{
				this.buf[0] = value;
				this.buf_dirty = true;
				this.buf_length = 1;
				this.FlushBuffer();
				return;
			}
			byte[] array = this.buf;
			int num = this.buf_offset;
			this.buf_offset = num + 1;
			array[num] = value;
			if (this.buf_offset > this.buf_length)
			{
				this.buf_length = this.buf_offset;
			}
			this.buf_dirty = true;
		}

		/// <summary>Reads a block of bytes from the stream and writes the data in a given buffer.</summary>
		/// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached.</returns>
		/// <param name="array">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1<paramref name=")" /> replaced by the bytes read from the current source. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E40 RID: 11840 RVA: 0x000A5198 File Offset: 0x000A3398
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			int num = array.Length;
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginRead(array, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000A5250 File Offset: 0x000A3450
		private int ReadInternal(byte[] dest, int offset, int count)
		{
			int num = this.ReadSegment(dest, offset, count);
			if (num == count)
			{
				return count;
			}
			int num2 = num;
			count -= num;
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				num = this.ReadData(this.safeHandle, dest, offset + num, count);
				this.buf_start += (long)num;
			}
			else
			{
				this.RefillBuffer();
				num = this.ReadSegment(dest, offset + num2, count);
			}
			return num2 + num;
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that references the asynchronous read.</returns>
		/// <param name="array">The buffer to read data into. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading. </param>
		/// <param name="numBytes">The maximum number of bytes to read. </param>
		/// <param name="userCallback">The method to be called when the asynchronous read operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">The array length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An asynchronous read was attempted past the end of the file. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E42 RID: 11842 RVA: 0x000A52BC File Offset: 0x000A34BC
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("Buffer too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
			}
			return new FileStream.ReadDelegate(this.ReadInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Waits for the pending asynchronous read operation to complete. (Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 and the number of bytes you requested. Streams only return 0 at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000A536C File Offset: 0x000A356C
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				return base.EndRead(asyncResult);
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Writes a block of bytes to the file stream.</summary>
		/// <param name="array">The buffer containing data to write to the stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> from which to begin copying bytes to the stream. </param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. - or -Another thread may have caused an unexpected change in the position of the operating system's file handle. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream instance does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E44 RID: 11844 RVA: 0x000A53D4 File Offset: 0x000A35D4
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginWrite(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			this.WriteInternal(array, offset, count);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000A547C File Offset: 0x000A367C
		private void WriteInternal(byte[] src, int offset, int count)
		{
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = count;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num = MonoIO.Write(this.safeHandle, src, offset, i, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num;
					offset += num;
				}
				this.buf_start += (long)count;
				return;
			}
			int num2 = 0;
			while (count > 0)
			{
				int num3 = this.WriteSegment(src, offset + num2, count);
				num2 += num3;
				count -= num3;
				if (count == 0)
				{
					break;
				}
				this.FlushBuffer();
			}
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <returns>An object that references the asynchronous write.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="numBytes">The maximum number of bytes to write. </param>
		/// <param name="userCallback">The method to be called when the asynchronous write operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous write request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E46 RID: 11846 RVA: 0x000A5548 File Offset: 0x000A3748
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("array too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
			}
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(userCallback, stateObject);
			fileStreamAsyncResult.BytesRead = -1;
			fileStreamAsyncResult.Count = numBytes;
			fileStreamAsyncResult.OriginalCount = numBytes;
			return new FileStream.WriteDelegate(this.WriteInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Ends an asynchronous write operation and blocks until the I/O operation is complete. (Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">The pending asynchronous I/O request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E47 RID: 11847 RVA: 0x000A5614 File Offset: 0x000A3814
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				base.EndWrite(asyncResult);
				return;
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Sets the current position of this stream to the given value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point relative to <paramref name="origin" /> from which to begin seeking. </param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="origin" />, using a value of type <see cref="T:System.IO.SeekOrigin" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the FileStream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ArgumentException">Seeking is attempted before the beginning of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E48 RID: 11848 RVA: 0x000A567C File Offset: 0x000A387C
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin", "Invalid SeekOrigin");
			}
			if (num < 0L)
			{
				throw new IOException("Attempted to Seek before the beginning of the stream");
			}
			if (num < this.append_startpos)
			{
				throw new IOException("Can't seek back over pre-existing data in append mode");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			this.buf_start = MonoIO.Seek(this.safeHandle, num, SeekOrigin.Begin, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			return this.buf_start;
		}

		/// <summary>Sets the length of this stream to the given value.</summary>
		/// <param name="value">The new length of the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the <paramref name="value" /> parameter to less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E49 RID: 11849 RVA: 0x000A5750 File Offset: 0x000A3950
		public override void SetLength(long value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value is less than 0");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			MonoIO.SetLength(this.safeHandle, value, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (this.Position > value)
			{
				this.Position = value;
			}
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000A57E6 File Offset: 0x000A39E6
		public override void Flush()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file, and also clears all intermediate file buffers.</summary>
		/// <param name="flushToDisk">true to flush all intermediate file buffers; otherwise, false. </param>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000A5808 File Offset: 0x000A3A08
		public virtual void Flush(bool flushToDisk)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
			if (flushToDisk)
			{
				MonoIOError monoIOError;
				MonoIO.Flush(this.safeHandle, out monoIOError);
			}
		}

		/// <summary>Prevents other processes from reading from or writing to the <see cref="T:System.IO.FileStream" />.</summary>
		/// <param name="position">The beginning of the range to lock. The value of this parameter must be equal to or greater than zero (0). </param>
		/// <param name="length">The range to be locked. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The process cannot access the file because another process has locked a portion of the file.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E4C RID: 11852 RVA: 0x000A5844 File Offset: 0x000A3A44
		public virtual void Lock(long position, long length)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			MonoIOError monoIOError;
			MonoIO.Lock(this.safeHandle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		/// <summary>Allows access by other processes to all or part of a file that was previously locked.</summary>
		/// <param name="position">The beginning of the range to unlock. </param>
		/// <param name="length">The range to be unlocked. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002E4D RID: 11853 RVA: 0x000A58B0 File Offset: 0x000A3AB0
		public virtual void Unlock(long position, long length)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			MonoIOError monoIOError;
			MonoIO.Unlock(this.safeHandle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000A591C File Offset: 0x000A3B1C
		~FileStream()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002E4F RID: 11855 RVA: 0x000A594C File Offset: 0x000A3B4C
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (this.safeHandle != null && !this.safeHandle.IsClosed)
			{
				try
				{
					this.FlushBuffer();
				}
				catch (Exception ex)
				{
				}
				if (this.owner)
				{
					MonoIOError monoIOError;
					MonoIO.Close(this.safeHandle.DangerousGetHandle(), out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					this.safeHandle.DangerousRelease();
				}
			}
			this.canseek = false;
			this.access = (FileAccess)0;
			if (disposing && this.buf != null)
			{
				if (this.buf.Length == 4096 && FileStream.buf_recycle == null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle == null)
						{
							FileStream.buf_recycle = this.buf;
						}
					}
				}
				this.buf = null;
				GC.SuppressFinalize(this);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <returns>An object that encapsulates the access control settings for the file described by the current <see cref="T:System.IO.FileStream" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E50 RID: 11856 RVA: 0x000A5A44 File Offset: 0x000A3C44
		public FileSecurity GetAccessControl()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			return new FileSecurity(this.SafeFileHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <param name="fileSecurity">An object that describes an ACL entry to apply to the current file.</param>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E51 RID: 11857 RVA: 0x000A5A6B File Offset: 0x000A3C6B
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			fileSecurity.PersistModifications(this.SafeFileHandle);
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests. </summary>
		/// <returns>A task that represents the asynchronous flush operation. </returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002E52 RID: 11858 RVA: 0x000A5A9F File Offset: 0x000A3C9F
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			return base.FlushAsync(cancellationToken);
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06002E53 RID: 11859 RVA: 0x000A5AC0 File Offset: 0x000A3CC0
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.ReadAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests. </summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06002E54 RID: 11860 RVA: 0x000A5ACD File Offset: 0x000A3CCD
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000A5ADA File Offset: 0x000A3CDA
		private int ReadSegment(byte[] dest, int dest_offset, int count)
		{
			count = Math.Min(count, this.buf_length - this.buf_offset);
			if (count > 0)
			{
				Buffer.InternalBlockCopy(this.buf, this.buf_offset, dest, dest_offset, count);
				this.buf_offset += count;
			}
			return count;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000A5B1C File Offset: 0x000A3D1C
		private int WriteSegment(byte[] src, int src_offset, int count)
		{
			if (count > this.buf_size - this.buf_offset)
			{
				count = this.buf_size - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(src, src_offset, this.buf, this.buf_offset, count);
				this.buf_offset += count;
				if (this.buf_offset > this.buf_length)
				{
					this.buf_length = this.buf_offset;
				}
				this.buf_dirty = true;
			}
			return count;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000A5B90 File Offset: 0x000A3D90
		private void FlushBuffer()
		{
			if (this.buf_dirty)
			{
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = this.buf_length;
				int num = 0;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num2 = MonoIO.Write(this.safeHandle, this.buf, num, this.buf_length, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num2;
					num += num2;
				}
			}
			this.buf_start += (long)this.buf_offset;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000A5C58 File Offset: 0x000A3E58
		private void FlushBufferIfDirty()
		{
			if (this.buf_dirty)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000A5C68 File Offset: 0x000A3E68
		private void RefillBuffer()
		{
			this.FlushBuffer();
			this.buf_length = this.ReadData(this.safeHandle, this.buf, 0, this.buf_size);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000A5C90 File Offset: 0x000A3E90
		private int ReadData(SafeHandle safeHandle, byte[] buf, int offset, int count)
		{
			MonoIOError monoIOError;
			int num = MonoIO.Read(safeHandle, buf, offset, count, out monoIOError);
			if (monoIOError == MonoIOError.ERROR_BROKEN_PIPE)
			{
				num = 0;
			}
			else if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (num == -1)
			{
				throw new IOException();
			}
			return num;
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000A5CD8 File Offset: 0x000A3ED8
		private void InitBuffer(int size, bool isZeroSize)
		{
			if (isZeroSize)
			{
				size = 0;
				this.buf = new byte[1];
			}
			else
			{
				if (size <= 0)
				{
					throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
				}
				size = Math.Max(size, 8);
				if (size <= 4096 && FileStream.buf_recycle != null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle != null)
						{
							this.buf = FileStream.buf_recycle;
							FileStream.buf_recycle = null;
						}
					}
				}
				if (this.buf == null)
				{
					this.buf = new byte[size];
				}
				else
				{
					Array.Clear(this.buf, 0, size);
				}
			}
			this.buf_size = size;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000A5D98 File Offset: 0x000A3F98
		private string GetSecureFileName(string filename)
		{
			if (!this.anonymous)
			{
				return Path.GetFullPath(filename);
			}
			return Path.GetFileName(filename);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000A5DAF File Offset: 0x000A3FAF
		private string GetSecureFileName(string filename, bool full)
		{
			if (this.anonymous)
			{
				return Path.GetFileName(filename);
			}
			if (!full)
			{
				return filename;
			}
			return Path.GetFullPath(filename);
		}

		// Token: 0x040017F3 RID: 6131
		internal const int DefaultBufferSize = 4096;

		// Token: 0x040017F4 RID: 6132
		private static byte[] buf_recycle;

		// Token: 0x040017F5 RID: 6133
		private static readonly object buf_recycle_lock = new object();

		// Token: 0x040017F6 RID: 6134
		private byte[] buf;

		// Token: 0x040017F7 RID: 6135
		private string name;

		// Token: 0x040017F8 RID: 6136
		private SafeFileHandle safeHandle;

		// Token: 0x040017F9 RID: 6137
		private bool isExposed;

		// Token: 0x040017FA RID: 6138
		private long append_startpos;

		// Token: 0x040017FB RID: 6139
		private FileAccess access;

		// Token: 0x040017FC RID: 6140
		private bool owner;

		// Token: 0x040017FD RID: 6141
		private bool async;

		// Token: 0x040017FE RID: 6142
		private bool canseek;

		// Token: 0x040017FF RID: 6143
		private bool anonymous;

		// Token: 0x04001800 RID: 6144
		private bool buf_dirty;

		// Token: 0x04001801 RID: 6145
		private int buf_size;

		// Token: 0x04001802 RID: 6146
		private int buf_length;

		// Token: 0x04001803 RID: 6147
		private int buf_offset;

		// Token: 0x04001804 RID: 6148
		private long buf_start;

		// Token: 0x020003D8 RID: 984
		// (Invoke) Token: 0x06002E60 RID: 11872
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x020003D9 RID: 985
		// (Invoke) Token: 0x06002E64 RID: 11876
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
