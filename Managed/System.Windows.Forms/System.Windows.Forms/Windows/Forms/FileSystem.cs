using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000175 RID: 373
	internal abstract class FileSystem
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x0005D584 File Offset: 0x0005B784
		public FSEntry ChangeDirectory(string folder)
		{
			if (folder == MWFVFS.DesktopPrefix)
			{
				this.currentTopFolder = MWFVFS.DesktopPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetDesktopFSEntry());
			}
			else if (folder == MWFVFS.PersonalPrefix)
			{
				this.currentTopFolder = MWFVFS.PersonalPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetPersonalFSEntry());
			}
			else if (folder == MWFVFS.MyComputerPersonalPrefix)
			{
				this.currentTopFolder = MWFVFS.MyComputerPersonalPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyComputerPersonalFSEntry());
			}
			else if (folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.currentTopFolder = MWFVFS.RecentlyUsedPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetRecentlyUsedFSEntry());
			}
			else if (folder == MWFVFS.MyComputerPrefix)
			{
				this.currentTopFolder = MWFVFS.MyComputerPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyComputerFSEntry());
			}
			else if (folder == MWFVFS.MyNetworkPrefix)
			{
				this.currentTopFolder = MWFVFS.MyNetworkPrefix;
				this.currentTopFolderFSEntry = (this.currentFolderFSEntry = this.GetMyNetworkFSEntry());
			}
			else
			{
				bool flag = false;
				foreach (object obj in MWFVFS.MyComputerDevicesPrefix)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					FSEntry fsentry = dictionaryEntry.Value as FSEntry;
					if (folder == fsentry.FullName)
					{
						this.currentTopFolder = dictionaryEntry.Key as string;
						this.currentTopFolderFSEntry = (this.currentFolderFSEntry = fsentry);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.currentFolderFSEntry = this.GetDirectoryFSEntry(new DirectoryInfo(folder), this.currentTopFolderFSEntry);
				}
			}
			return this.currentFolderFSEntry;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005D7A8 File Offset: 0x0005B9A8
		public string GetParent()
		{
			return this.currentFolderFSEntry.Parent;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
		public void GetFolderContent(StringCollection filters, out ArrayList directories_out, out ArrayList files_out)
		{
			directories_out = new ArrayList();
			files_out = new ArrayList();
			if (this.currentFolderFSEntry.FullName == MWFVFS.DesktopPrefix)
			{
				FSEntry personalFSEntry = this.GetPersonalFSEntry();
				directories_out.Add(personalFSEntry);
				FSEntry myComputerFSEntry = this.GetMyComputerFSEntry();
				directories_out.Add(myComputerFSEntry);
				FSEntry myNetworkFSEntry = this.GetMyNetworkFSEntry();
				directories_out.Add(myNetworkFSEntry);
				ArrayList arrayList = null;
				ArrayList arrayList2 = null;
				this.GetNormalFolderContent(ThemeEngine.Current.Places(UIIcon.PlacesDesktop), filters, out arrayList, out arrayList2);
				directories_out.AddRange(arrayList);
				files_out.AddRange(arrayList2);
			}
			else if (this.currentFolderFSEntry.FullName == MWFVFS.RecentlyUsedPrefix)
			{
				files_out = this.GetRecentlyUsedFiles();
			}
			else if (this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPrefix)
			{
				directories_out.AddRange(this.GetMyComputerContent());
			}
			else if (this.currentFolderFSEntry.FullName == MWFVFS.PersonalPrefix || this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPersonalPrefix)
			{
				ArrayList arrayList3 = null;
				ArrayList arrayList4 = null;
				this.GetNormalFolderContent(ThemeEngine.Current.Places(UIIcon.PlacesPersonal), filters, out arrayList3, out arrayList4);
				directories_out.AddRange(arrayList3);
				files_out.AddRange(arrayList4);
			}
			else if (this.currentFolderFSEntry.FullName == MWFVFS.MyNetworkPrefix)
			{
				directories_out.AddRange(this.GetMyNetworkContent());
			}
			else
			{
				this.GetNormalFolderContent(this.currentFolderFSEntry.FullName, filters, out directories_out, out files_out);
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0005D948 File Offset: 0x0005BB48
		public ArrayList GetFoldersOnly()
		{
			ArrayList arrayList = new ArrayList();
			if (this.currentFolderFSEntry.FullName == MWFVFS.DesktopPrefix)
			{
				FSEntry personalFSEntry = this.GetPersonalFSEntry();
				arrayList.Add(personalFSEntry);
				FSEntry myComputerFSEntry = this.GetMyComputerFSEntry();
				arrayList.Add(myComputerFSEntry);
				FSEntry myNetworkFSEntry = this.GetMyNetworkFSEntry();
				arrayList.Add(myNetworkFSEntry);
				ArrayList normalFolders = this.GetNormalFolders(ThemeEngine.Current.Places(UIIcon.PlacesDesktop));
				arrayList.AddRange(normalFolders);
			}
			else if (!(this.currentFolderFSEntry.FullName == MWFVFS.RecentlyUsedPrefix))
			{
				if (this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPrefix)
				{
					arrayList.AddRange(this.GetMyComputerContent());
				}
				else if (this.currentFolderFSEntry.FullName == MWFVFS.PersonalPrefix || this.currentFolderFSEntry.FullName == MWFVFS.MyComputerPersonalPrefix)
				{
					ArrayList normalFolders2 = this.GetNormalFolders(ThemeEngine.Current.Places(UIIcon.PlacesPersonal));
					arrayList.AddRange(normalFolders2);
				}
				else if (this.currentFolderFSEntry.FullName == MWFVFS.MyNetworkPrefix)
				{
					arrayList.AddRange(this.GetMyNetworkContent());
				}
				else
				{
					arrayList = this.GetNormalFolders(this.currentFolderFSEntry.FullName);
				}
			}
			return arrayList;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005DAA0 File Offset: 0x0005BCA0
		protected void GetNormalFolderContent(string from_folder, StringCollection filters, out ArrayList directories_out, out ArrayList files_out)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(from_folder);
			directories_out = new ArrayList();
			DirectoryInfo[] array = null;
			try
			{
				array = directoryInfo.GetDirectories();
			}
			catch (Exception)
			{
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					directories_out.Add(this.GetDirectoryFSEntry(array[i], this.currentTopFolderFSEntry));
				}
			}
			directories_out.Sort(this.fsEntryComparer);
			files_out = new ArrayList();
			ArrayList arrayList = new ArrayList();
			try
			{
				if (filters == null)
				{
					arrayList.AddRange(directoryInfo.GetFiles());
				}
				else
				{
					foreach (string text in filters)
					{
						arrayList.AddRange(directoryInfo.GetFiles(text));
					}
					arrayList.Sort(this.fileInfoComparer);
				}
			}
			catch (Exception)
			{
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				FSEntry fileFSEntry = this.GetFileFSEntry(arrayList[j] as FileInfo);
				if (fileFSEntry != null)
				{
					files_out.Add(fileFSEntry);
				}
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0005DC24 File Offset: 0x0005BE24
		protected ArrayList GetNormalFolders(string from_folder)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(from_folder);
			ArrayList arrayList = new ArrayList();
			DirectoryInfo[] array = null;
			try
			{
				array = directoryInfo.GetDirectories();
			}
			catch (Exception)
			{
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(this.GetDirectoryFSEntry(array[i], this.currentTopFolderFSEntry));
				}
			}
			return arrayList;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0005DCA0 File Offset: 0x0005BEA0
		protected virtual FSEntry GetDirectoryFSEntry(DirectoryInfo dirinfo, FSEntry topFolderFSEntry)
		{
			return new FSEntry
			{
				Attributes = dirinfo.Attributes,
				FullName = dirinfo.FullName,
				Name = dirinfo.Name,
				MainTopNode = topFolderFSEntry,
				FileType = FSEntry.FSEntryType.Directory,
				IconIndex = MimeIconEngine.GetIconIndexForMimeType("inode/directory"),
				LastAccessTime = dirinfo.LastAccessTime
			};
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0005DD04 File Offset: 0x0005BF04
		protected virtual FSEntry GetFileFSEntry(FileInfo fileinfo)
		{
			if ((fileinfo.Attributes & 16) == 16)
			{
				return null;
			}
			return new FSEntry
			{
				Attributes = fileinfo.Attributes,
				FullName = fileinfo.FullName,
				Name = fileinfo.Name,
				FileType = FSEntry.FSEntryType.File,
				IconIndex = MimeIconEngine.GetIconIndexForFile(fileinfo.FullName),
				FileSize = fileinfo.Length,
				LastAccessTime = fileinfo.LastAccessTime
			};
		}

		// Token: 0x060018CC RID: 6348
		protected abstract FSEntry GetDesktopFSEntry();

		// Token: 0x060018CD RID: 6349
		protected abstract FSEntry GetRecentlyUsedFSEntry();

		// Token: 0x060018CE RID: 6350
		protected abstract FSEntry GetPersonalFSEntry();

		// Token: 0x060018CF RID: 6351
		protected abstract FSEntry GetMyComputerPersonalFSEntry();

		// Token: 0x060018D0 RID: 6352
		protected abstract FSEntry GetMyComputerFSEntry();

		// Token: 0x060018D1 RID: 6353
		protected abstract FSEntry GetMyNetworkFSEntry();

		// Token: 0x060018D2 RID: 6354
		public abstract void WriteRecentlyUsedFiles(string fileToAdd);

		// Token: 0x060018D3 RID: 6355
		public abstract ArrayList GetRecentlyUsedFiles();

		// Token: 0x060018D4 RID: 6356
		public abstract ArrayList GetMyComputerContent();

		// Token: 0x060018D5 RID: 6357
		public abstract ArrayList GetMyNetworkContent();

		// Token: 0x04000DDE RID: 3550
		protected string currentTopFolder = string.Empty;

		// Token: 0x04000DDF RID: 3551
		protected FSEntry currentFolderFSEntry;

		// Token: 0x04000DE0 RID: 3552
		protected FSEntry currentTopFolderFSEntry;

		// Token: 0x04000DE1 RID: 3553
		private FileSystem.FileInfoComparer fileInfoComparer = new FileSystem.FileInfoComparer();

		// Token: 0x04000DE2 RID: 3554
		private FileSystem.FSEntryComparer fsEntryComparer = new FileSystem.FSEntryComparer();

		// Token: 0x02000176 RID: 374
		internal class FileInfoComparer : IComparer
		{
			// Token: 0x060018D7 RID: 6359 RVA: 0x0005DD88 File Offset: 0x0005BF88
			public int Compare(object fileInfo1, object fileInfo2)
			{
				return string.Compare(((FileInfo)fileInfo1).Name, ((FileInfo)fileInfo2).Name);
			}
		}

		// Token: 0x02000177 RID: 375
		internal class FSEntryComparer : IComparer
		{
			// Token: 0x060018D9 RID: 6361 RVA: 0x0005DDB0 File Offset: 0x0005BFB0
			public int Compare(object fileInfo1, object fileInfo2)
			{
				return string.Compare(((FSEntry)fileInfo1).Name, ((FSEntry)fileInfo2).Name);
			}
		}
	}
}
