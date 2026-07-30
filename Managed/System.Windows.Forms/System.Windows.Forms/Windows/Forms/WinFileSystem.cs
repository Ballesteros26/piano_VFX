using System;
using System.Collections;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000179 RID: 377
	internal class WinFileSystem : FileSystem
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x0005EBA0 File Offset: 0x0005CDA0
		public WinFileSystem()
		{
			this.desktopFSEntry = new FSEntry();
			this.desktopFSEntry.Attributes = 16;
			this.desktopFSEntry.FullName = MWFVFS.DesktopPrefix;
			this.desktopFSEntry.Name = "Desktop";
			this.desktopFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
			this.desktopFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.desktopFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("desktop/desktop");
			this.desktopFSEntry.LastAccessTime = DateTime.Now;
			this.recentlyusedFSEntry = new FSEntry();
			this.recentlyusedFSEntry.Attributes = 16;
			this.recentlyusedFSEntry.FullName = MWFVFS.RecentlyUsedPrefix;
			this.recentlyusedFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesRecentDocuments);
			this.recentlyusedFSEntry.Name = "Recently Used";
			this.recentlyusedFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.recentlyusedFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("recently/recently");
			this.recentlyusedFSEntry.LastAccessTime = DateTime.Now;
			this.personalFSEntry = new FSEntry();
			this.personalFSEntry.Attributes = 16;
			this.personalFSEntry.FullName = MWFVFS.PersonalPrefix;
			this.personalFSEntry.Name = "Personal";
			this.personalFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.personalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.personalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.personalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.personalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerpersonalFSEntry = new FSEntry();
			this.mycomputerpersonalFSEntry.Attributes = 16;
			this.mycomputerpersonalFSEntry.FullName = MWFVFS.MyComputerPersonalPrefix;
			this.mycomputerpersonalFSEntry.Name = "Personal";
			this.mycomputerpersonalFSEntry.MainTopNode = this.GetMyComputerFSEntry();
			this.mycomputerpersonalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.mycomputerpersonalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerpersonalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.mycomputerpersonalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerFSEntry = new FSEntry();
			this.mycomputerFSEntry.Attributes = 16;
			this.mycomputerFSEntry.FullName = MWFVFS.MyComputerPrefix;
			this.mycomputerFSEntry.Name = "My Computer";
			this.mycomputerFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mycomputerFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("workplace/workplace");
			this.mycomputerFSEntry.LastAccessTime = DateTime.Now;
			this.mynetworkFSEntry = new FSEntry();
			this.mynetworkFSEntry.Attributes = 16;
			this.mynetworkFSEntry.FullName = MWFVFS.MyNetworkPrefix;
			this.mynetworkFSEntry.Name = "My Network";
			this.mynetworkFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mynetworkFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mynetworkFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
			this.mynetworkFSEntry.LastAccessTime = DateTime.Now;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
		public override void WriteRecentlyUsedFiles(string fileToAdd)
		{
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005EECC File Offset: 0x0005D0CC
		public override ArrayList GetRecentlyUsedFiles()
		{
			ArrayList arrayList = new ArrayList();
			DirectoryInfo directoryInfo = new DirectoryInfo(this.recentlyusedFSEntry.RealName);
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				FSEntry fileFSEntry = this.GetFileFSEntry(fileInfo);
				if (fileFSEntry != null)
				{
					arrayList.Add(fileFSEntry);
				}
			}
			return arrayList;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005EF34 File Offset: 0x0005D134
		public override ArrayList GetMyComputerContent()
		{
			string[] logicalDrives = Directory.GetLogicalDrives();
			ArrayList arrayList = new ArrayList();
			foreach (string text in logicalDrives)
			{
				FSEntry fsentry = new FSEntry();
				fsentry.FileType = FSEntry.FSEntryType.Device;
				fsentry.FullName = text;
				fsentry.Name = text;
				fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("harddisk/harddisk");
				fsentry.Attributes = 16;
				fsentry.MainTopNode = this.GetMyComputerFSEntry();
				arrayList.Add(fsentry);
				string text2 = fsentry.FullName + "://";
				if (!MWFVFS.MyComputerDevicesPrefix.Contains(text2))
				{
					MWFVFS.MyComputerDevicesPrefix.Add(text2, fsentry);
				}
			}
			arrayList.Add(this.GetMyComputerPersonalFSEntry());
			return arrayList;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005EFFC File Offset: 0x0005D1FC
		public override ArrayList GetMyNetworkContent()
		{
			return new ArrayList();
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0005F004 File Offset: 0x0005D204
		protected override FSEntry GetDesktopFSEntry()
		{
			return this.desktopFSEntry;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0005F00C File Offset: 0x0005D20C
		protected override FSEntry GetRecentlyUsedFSEntry()
		{
			return this.recentlyusedFSEntry;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0005F014 File Offset: 0x0005D214
		protected override FSEntry GetPersonalFSEntry()
		{
			return this.personalFSEntry;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0005F01C File Offset: 0x0005D21C
		protected override FSEntry GetMyComputerPersonalFSEntry()
		{
			return this.mycomputerpersonalFSEntry;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0005F024 File Offset: 0x0005D224
		protected override FSEntry GetMyComputerFSEntry()
		{
			return this.mycomputerFSEntry;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0005F02C File Offset: 0x0005D22C
		protected override FSEntry GetMyNetworkFSEntry()
		{
			return this.mynetworkFSEntry;
		}

		// Token: 0x04000DED RID: 3565
		private FSEntry desktopFSEntry;

		// Token: 0x04000DEE RID: 3566
		private FSEntry recentlyusedFSEntry;

		// Token: 0x04000DEF RID: 3567
		private FSEntry personalFSEntry;

		// Token: 0x04000DF0 RID: 3568
		private FSEntry mycomputerpersonalFSEntry;

		// Token: 0x04000DF1 RID: 3569
		private FSEntry mycomputerFSEntry;

		// Token: 0x04000DF2 RID: 3570
		private FSEntry mynetworkFSEntry;
	}
}
