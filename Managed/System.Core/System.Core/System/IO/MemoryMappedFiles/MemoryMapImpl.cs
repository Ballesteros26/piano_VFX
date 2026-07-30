using System;
using System.Runtime.CompilerServices;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x02000056 RID: 86
	internal static class MemoryMapImpl
	{
		// Token: 0x06000195 RID: 405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OpenFileInternal(string path, FileMode mode, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, out int error);

		// Token: 0x06000196 RID: 406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OpenHandleInternal(IntPtr handle, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, out int error);

		// Token: 0x06000197 RID: 407
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CloseMapping(IntPtr handle);

		// Token: 0x06000198 RID: 408
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Flush(IntPtr file_handle);

		// Token: 0x06000199 RID: 409
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConfigureHandleInheritability(IntPtr handle, HandleInheritability inheritability);

		// Token: 0x0600019A RID: 410
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Unmap(IntPtr mmap_handle);

		// Token: 0x0600019B RID: 411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MapInternal(IntPtr handle, long offset, ref long size, MemoryMappedFileAccess access, out IntPtr mmap_handle, out IntPtr base_address);

		// Token: 0x0600019C RID: 412 RVA: 0x0000488C File Offset: 0x00002A8C
		internal static void Map(IntPtr handle, long offset, ref long size, MemoryMappedFileAccess access, out IntPtr mmap_handle, out IntPtr base_address)
		{
			int num = MemoryMapImpl.MapInternal(handle, offset, ref size, access, out mmap_handle, out base_address);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, "<none>");
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000048B8 File Offset: 0x00002AB8
		private static Exception CreateException(int error, string path)
		{
			switch (error)
			{
			case 1:
				return new ArgumentException("A positive capacity must be specified for a Memory Mapped File backed by an empty file.");
			case 2:
				return new ArgumentOutOfRangeException("capacity", "The capacity may not be smaller than the file size.");
			case 3:
				return new FileNotFoundException(path);
			case 4:
				return new IOException("The file already exists");
			case 5:
				return new PathTooLongException();
			case 6:
				return new IOException("Could not open file");
			case 7:
				return new ArgumentException("Capacity must be bigger than zero for non-file mappings");
			case 8:
				return new ArgumentException("Invalid FileMode value.");
			case 9:
				return new IOException("Could not map file");
			case 10:
				return new UnauthorizedAccessException("Access to the path is denied.");
			case 11:
				return new ArgumentOutOfRangeException("capacity", "The capacity cannot be greater than the size of the system's logical address space.");
			default:
				return new IOException("Failed with unknown error code " + error);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000498C File Offset: 0x00002B8C
		internal static IntPtr OpenFile(string path, FileMode mode, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options)
		{
			int num = 0;
			IntPtr intPtr = MemoryMapImpl.OpenFileInternal(path, mode, mapName, out capacity, access, options, out num);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, path);
			}
			return intPtr;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000049B8 File Offset: 0x00002BB8
		internal static IntPtr OpenHandle(IntPtr handle, string mapName, out long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options)
		{
			int num = 0;
			IntPtr intPtr = MemoryMapImpl.OpenHandleInternal(handle, mapName, out capacity, access, options, out num);
			if (num != 0)
			{
				throw MemoryMapImpl.CreateException(num, "<none>");
			}
			return intPtr;
		}
	}
}
