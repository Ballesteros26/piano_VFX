using System;
using System.Collections;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200017A RID: 378
	internal class FSEntry
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0005F054 File Offset: 0x0005D254
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0005F048 File Offset: 0x0005D248
		public MasterMount.FsTypes FsType
		{
			get
			{
				return this.fsType;
			}
			set
			{
				this.fsType = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005F068 File Offset: 0x0005D268
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0005F05C File Offset: 0x0005D25C
		public string DeviceShort
		{
			get
			{
				return this.device_short;
			}
			set
			{
				this.device_short = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0005F07C File Offset: 0x0005D27C
		// (set) Token: 0x060018F5 RID: 6389 RVA: 0x0005F070 File Offset: 0x0005D270
		public string FullName
		{
			get
			{
				return this.fullName;
			}
			set
			{
				this.fullName = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005F090 File Offset: 0x0005D290
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x0005F084 File Offset: 0x0005D284
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005F0A4 File Offset: 0x0005D2A4
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0005F098 File Offset: 0x0005D298
		public string RealName
		{
			get
			{
				return this.realName;
			}
			set
			{
				this.realName = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0005F0B8 File Offset: 0x0005D2B8
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0005F0AC File Offset: 0x0005D2AC
		public FileAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0005F0CC File Offset: 0x0005D2CC
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x0005F0C0 File Offset: 0x0005D2C0
		public long FileSize
		{
			get
			{
				return this.fileSize;
			}
			set
			{
				this.fileSize = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005F0E0 File Offset: 0x0005D2E0
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x0005F0D4 File Offset: 0x0005D2D4
		public FSEntry.FSEntryType FileType
		{
			get
			{
				return this.fileType;
			}
			set
			{
				this.fileType = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0005F0E8 File Offset: 0x0005D2E8
		public DateTime LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
			set
			{
				this.lastAccessTime = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005F108 File Offset: 0x0005D308
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0005F0FC File Offset: 0x0005D2FC
		public int IconIndex
		{
			get
			{
				return this.iconIndex;
			}
			set
			{
				this.iconIndex = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005F11C File Offset: 0x0005D31C
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0005F110 File Offset: 0x0005D310
		public FSEntry MainTopNode
		{
			get
			{
				return this.mainTopNode;
			}
			set
			{
				this.mainTopNode = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0005F130 File Offset: 0x0005D330
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0005F124 File Offset: 0x0005D324
		public string Parent
		{
			get
			{
				this.parent = this.GetParent();
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005F144 File Offset: 0x0005D344
		private string GetParent()
		{
			if (this.fullName == MWFVFS.PersonalPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.MyComputerPersonalPrefix)
			{
				return MWFVFS.MyComputerPrefix;
			}
			if (this.fullName == MWFVFS.MyComputerPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.MyNetworkPrefix)
			{
				return MWFVFS.DesktopPrefix;
			}
			if (this.fullName == MWFVFS.DesktopPrefix)
			{
				return null;
			}
			if (this.fullName == MWFVFS.RecentlyUsedPrefix)
			{
				return null;
			}
			foreach (object obj in MWFVFS.MyComputerDevicesPrefix)
			{
				FSEntry fsentry = ((DictionaryEntry)obj).Value as FSEntry;
				if (this.fullName == fsentry.FullName)
				{
					return fsentry.MainTopNode.FullName;
				}
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(this.fullName);
			DirectoryInfo directoryInfo2 = directoryInfo.Parent;
			if (directoryInfo2 == null)
			{
				return null;
			}
			FSEntry fsentry2 = MWFVFS.MyComputerDevicesPrefix[directoryInfo2.FullName + "://"] as FSEntry;
			if (fsentry2 != null)
			{
				return fsentry2.FullName;
			}
			if (this.mainTopNode != null)
			{
				if (directoryInfo2.FullName == ThemeEngine.Current.Places(UIIcon.PlacesDesktop) && this.mainTopNode.FullName == MWFVFS.DesktopPrefix)
				{
					return this.mainTopNode.FullName;
				}
				if (directoryInfo2.FullName == ThemeEngine.Current.Places(UIIcon.PlacesPersonal) && this.mainTopNode.FullName == MWFVFS.PersonalPrefix)
				{
					return this.mainTopNode.FullName;
				}
				if (directoryInfo2.FullName == ThemeEngine.Current.Places(UIIcon.PlacesPersonal) && this.mainTopNode.FullName == MWFVFS.MyComputerPersonalPrefix)
				{
					return this.mainTopNode.FullName;
				}
			}
			return directoryInfo2.FullName;
		}

		// Token: 0x04000DF3 RID: 3571
		private MasterMount.FsTypes fsType;

		// Token: 0x04000DF4 RID: 3572
		private string device_short;

		// Token: 0x04000DF5 RID: 3573
		private string fullName;

		// Token: 0x04000DF6 RID: 3574
		private string name;

		// Token: 0x04000DF7 RID: 3575
		private string realName;

		// Token: 0x04000DF8 RID: 3576
		private FileAttributes attributes = 128;

		// Token: 0x04000DF9 RID: 3577
		private long fileSize;

		// Token: 0x04000DFA RID: 3578
		private FSEntry.FSEntryType fileType;

		// Token: 0x04000DFB RID: 3579
		private DateTime lastAccessTime;

		// Token: 0x04000DFC RID: 3580
		private FSEntry mainTopNode;

		// Token: 0x04000DFD RID: 3581
		private int iconIndex;

		// Token: 0x04000DFE RID: 3582
		private string parent;

		// Token: 0x0200017B RID: 379
		public enum FSEntryType
		{
			// Token: 0x04000E00 RID: 3584
			Desktop,
			// Token: 0x04000E01 RID: 3585
			RecentlyUsed,
			// Token: 0x04000E02 RID: 3586
			MyComputer,
			// Token: 0x04000E03 RID: 3587
			File,
			// Token: 0x04000E04 RID: 3588
			Directory,
			// Token: 0x04000E05 RID: 3589
			Device,
			// Token: 0x04000E06 RID: 3590
			RemovableDevice,
			// Token: 0x04000E07 RID: 3591
			Network
		}
	}
}
