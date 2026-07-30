using System;

namespace System.Windows.Forms
{
	// Token: 0x0200016F RID: 367
	internal class FileViewListViewItem : ListViewItem
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
		public FileViewListViewItem(FSEntry fsEntry)
		{
			this.fsEntry = fsEntry;
			base.ImageIndex = fsEntry.IconIndex;
			base.Text = fsEntry.Name;
			switch (fsEntry.FileType)
			{
			case FSEntry.FSEntryType.File:
			{
				long num = 1L;
				try
				{
					if (fsEntry.FileSize > 1024L)
					{
						num = fsEntry.FileSize / 1024L;
					}
				}
				catch (Exception)
				{
					num = 1L;
				}
				base.SubItems.Add(num.ToString() + " KB");
				base.SubItems.Add("File");
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				break;
			}
			case FSEntry.FSEntryType.Directory:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add("Directory");
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				break;
			case FSEntry.FSEntryType.Device:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add("Device");
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				break;
			case FSEntry.FSEntryType.RemovableDevice:
				base.SubItems.Add(string.Empty);
				base.SubItems.Add("RemovableDevice");
				base.SubItems.Add(fsEntry.LastAccessTime.ToShortDateString() + " " + fsEntry.LastAccessTime.ToShortTimeString());
				break;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005CCDC File Offset: 0x0005AEDC
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x0005CCD0 File Offset: 0x0005AED0
		public FSEntry FSEntry
		{
			get
			{
				return this.fsEntry;
			}
			set
			{
				this.fsEntry = value;
			}
		}

		// Token: 0x04000DC1 RID: 3521
		private FSEntry fsEntry;
	}
}
