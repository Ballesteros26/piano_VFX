using System;
using System.IO;
using UnityEngine;

namespace SimpleFileBrowser
{
	// Token: 0x02000007 RID: 7
	public static class FileBrowserHelpers
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003AD3 File Offset: 0x00001CD3
		public static bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003ADB File Offset: 0x00001CDB
		public static bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public static bool IsDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				return true;
			}
			if (File.Exists(path))
			{
				return false;
			}
			string extension = Path.GetExtension(path);
			return extension == null || extension.Length <= 1;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003B20 File Offset: 0x00001D20
		public static FileSystemEntry[] GetEntriesInDirectory(string path)
		{
			FileSystemEntry[] array2;
			try
			{
				FileSystemInfo[] fileSystemInfos = new DirectoryInfo(path).GetFileSystemInfos();
				FileSystemEntry[] array = new FileSystemEntry[fileSystemInfos.Length];
				for (int i = 0; i < fileSystemInfos.Length; i++)
				{
					array[i] = new FileSystemEntry(fileSystemInfos[i]);
				}
				array2 = array;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				array2 = null;
			}
			return array2;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003B80 File Offset: 0x00001D80
		public static string CreateFileInDirectory(string directoryPath, string filename)
		{
			string text = Path.Combine(directoryPath, filename);
			using (File.Create(text))
			{
			}
			return text;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003BBC File Offset: 0x00001DBC
		public static string CreateFolderInDirectory(string directoryPath, string folderName)
		{
			string text = Path.Combine(directoryPath, folderName);
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003BCC File Offset: 0x00001DCC
		public static void WriteBytesToFile(string targetPath, byte[] bytes)
		{
			File.WriteAllBytes(targetPath, bytes);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public static void WriteTextToFile(string targetPath, string text)
		{
			File.WriteAllText(targetPath, text);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003BDE File Offset: 0x00001DDE
		public static void WriteCopyToFile(string targetPath, string sourceFile)
		{
			File.Copy(sourceFile, targetPath, true);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public static void AppendBytesToFile(string targetPath, byte[] bytes)
		{
			using (FileStream fileStream = new FileStream(targetPath, FileMode.Append, FileAccess.Write))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C28 File Offset: 0x00001E28
		public static void AppendTextToFile(string targetPath, string text)
		{
			File.AppendAllText(targetPath, text);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C34 File Offset: 0x00001E34
		public static void AppendCopyToFile(string targetPath, string sourceFile)
		{
			using (Stream stream = File.OpenRead(sourceFile))
			{
				using (Stream stream2 = new FileStream(targetPath, FileMode.Append, FileAccess.Write))
				{
					byte[] array = new byte[4096];
					int num;
					while ((num = stream.Read(array, 0, array.Length)) > 0)
					{
						stream2.Write(array, 0, num);
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003CAC File Offset: 0x00001EAC
		public static byte[] ReadBytesFromFile(string sourcePath)
		{
			return File.ReadAllBytes(sourcePath);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public static string ReadTextFromFile(string sourcePath)
		{
			return File.ReadAllText(sourcePath);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003CBC File Offset: 0x00001EBC
		public static void ReadCopyFromFile(string sourcePath, string destinationFile)
		{
			File.Copy(sourcePath, destinationFile, true);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static string RenameFile(string path, string newName)
		{
			string text = Path.Combine(Path.GetDirectoryName(path), newName);
			File.Move(path, text);
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static string RenameDirectory(string path, string newName)
		{
			string text = Path.Combine(new DirectoryInfo(path).Parent.FullName, newName);
			Directory.Move(path, text);
			return text;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003D18 File Offset: 0x00001F18
		public static void DeleteFile(string path)
		{
			File.Delete(path);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003D20 File Offset: 0x00001F20
		public static void DeleteDirectory(string path)
		{
			Directory.Delete(path, true);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003D29 File Offset: 0x00001F29
		public static string GetFilename(string path)
		{
			return Path.GetFileName(path);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D31 File Offset: 0x00001F31
		public static long GetFilesize(string path)
		{
			return new FileInfo(path).Length;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D3E File Offset: 0x00001F3E
		public static DateTime GetLastModifiedDate(string path)
		{
			return new FileInfo(path).LastWriteTime;
		}
	}
}
