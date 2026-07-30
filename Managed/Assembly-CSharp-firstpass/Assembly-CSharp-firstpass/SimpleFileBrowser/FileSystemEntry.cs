using System;
using System.IO;

namespace SimpleFileBrowser
{
	// Token: 0x02000006 RID: 6
	public struct FileSystemEntry
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003A5E File Offset: 0x00001C5E
		public bool IsDirectory
		{
			get
			{
				return (this.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A6D File Offset: 0x00001C6D
		public FileSystemEntry(string path, string name, bool isDirectory)
		{
			this.Path = path;
			this.Name = name;
			this.Extension = (isDirectory ? null : global::System.IO.Path.GetExtension(name));
			this.Attributes = (isDirectory ? FileAttributes.Directory : FileAttributes.Normal);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AA1 File Offset: 0x00001CA1
		public FileSystemEntry(FileSystemInfo fileInfo)
		{
			this.Path = fileInfo.FullName;
			this.Name = fileInfo.Name;
			this.Extension = fileInfo.Extension;
			this.Attributes = fileInfo.Attributes;
		}

		// Token: 0x0400004A RID: 74
		public readonly string Path;

		// Token: 0x0400004B RID: 75
		public readonly string Name;

		// Token: 0x0400004C RID: 76
		public readonly string Extension;

		// Token: 0x0400004D RID: 77
		public readonly FileAttributes Attributes;
	}
}
