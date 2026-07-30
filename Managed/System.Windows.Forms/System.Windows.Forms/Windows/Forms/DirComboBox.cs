using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200016A RID: 362
	internal class DirComboBox : ComboBox, IUpdateFolder
	{
		// Token: 0x0600184D RID: 6221 RVA: 0x0005A7C0 File Offset: 0x000589C0
		public DirComboBox(MWFVFS vfs)
		{
			this.vfs = vfs;
			base.SuspendLayout();
			base.DrawMode = DrawMode.OwnerDrawFixed;
			this.imageList.ColorDepth = ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new Size(16, 16);
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 16));
			this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.NormalFolder, 16));
			this.imageList.TransparentColor = Color.Transparent;
			this.recentlyUsedDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 0, "Recently used", MWFVFS.RecentlyUsedPrefix, 0);
			this.desktopDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 1, "Desktop", MWFVFS.DesktopPrefix, 0);
			this.personalDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 2, "Personal folder", MWFVFS.PersonalPrefix, DirComboBox.indent);
			this.myComputerDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 3, "My Computer", MWFVFS.MyComputerPrefix, DirComboBox.indent);
			this.networkDirComboboxItem = new DirComboBox.DirComboBoxItem(this.imageList, 4, "My Network", MWFVFS.MyNetworkPrefix, DirComboBox.indent);
			ArrayList arrayList = this.vfs.GetMyComputerContent();
			foreach (object obj in arrayList)
			{
				FSEntry fsentry = (FSEntry)obj;
				this.myComputerItems.Add(new DirComboBox.DirComboBoxItem(MimeIconEngine.LargeIcons, fsentry.IconIndex, fsentry.Name, fsentry.FullName, DirComboBox.indent * 2));
			}
			arrayList.Clear();
			arrayList = null;
			this.mainParentDirComboBoxItem = this.myComputerDirComboboxItem;
			base.ResumeLayout(false);
		}

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x0600184F RID: 6223 RVA: 0x0005AA48 File Offset: 0x00058C48
		// (remove) Token: 0x06001850 RID: 6224 RVA: 0x0005AA5C File Offset: 0x00058C5C
		public event EventHandler DirectoryChanged
		{
			add
			{
				base.Events.AddHandler(DirComboBox.CDirectoryChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DirComboBox.CDirectoryChangedEvent, value);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005AA80 File Offset: 0x00058C80
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x0005AA70 File Offset: 0x00058C70
		public string CurrentFolder
		{
			get
			{
				return this.currentPath;
			}
			set
			{
				this.currentPath = value;
				this.CreateComboList();
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0005AA88 File Offset: 0x00058C88
		private void CreateComboList()
		{
			this.real_parent = null;
			DirComboBox.DirComboBoxItem dirComboBoxItem = null;
			if (this.currentPath == MWFVFS.RecentlyUsedPrefix)
			{
				this.mainParentDirComboBoxItem = this.recentlyUsedDirComboboxItem;
				dirComboBoxItem = this.recentlyUsedDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.DesktopPrefix)
			{
				dirComboBoxItem = this.desktopDirComboboxItem;
				this.mainParentDirComboBoxItem = this.desktopDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.PersonalPrefix)
			{
				dirComboBoxItem = this.personalDirComboboxItem;
				this.mainParentDirComboBoxItem = this.personalDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.MyComputerPrefix)
			{
				dirComboBoxItem = this.myComputerDirComboboxItem;
				this.mainParentDirComboBoxItem = this.myComputerDirComboboxItem;
			}
			else if (this.currentPath == MWFVFS.MyNetworkPrefix)
			{
				dirComboBoxItem = this.networkDirComboboxItem;
				this.mainParentDirComboBoxItem = this.networkDirComboboxItem;
			}
			else
			{
				foreach (object obj in this.myComputerItems)
				{
					DirComboBox.DirComboBoxItem dirComboBoxItem2 = (DirComboBox.DirComboBoxItem)obj;
					if (dirComboBoxItem2.Path == this.currentPath)
					{
						dirComboBoxItem = (this.mainParentDirComboBoxItem = dirComboBoxItem2);
						break;
					}
				}
			}
			base.BeginUpdate();
			base.Items.Clear();
			base.Items.Add(this.recentlyUsedDirComboboxItem);
			base.Items.Add(this.desktopDirComboboxItem);
			base.Items.Add(this.personalDirComboboxItem);
			base.Items.Add(this.myComputerDirComboboxItem);
			base.Items.AddRange(this.myComputerItems);
			base.Items.Add(this.networkDirComboboxItem);
			if (dirComboBoxItem == null)
			{
				this.real_parent = this.CreateFolderStack();
			}
			if (this.real_parent != null)
			{
				int num;
				if (this.real_parent == this.desktopDirComboboxItem)
				{
					num = 1;
				}
				else if (this.real_parent == this.personalDirComboboxItem || this.real_parent == this.networkDirComboboxItem)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
				dirComboBoxItem = this.AppendToParent(num, this.real_parent);
			}
			base.EndUpdate();
			if (dirComboBoxItem != null)
			{
				base.SelectedItem = dirComboBoxItem;
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005AD00 File Offset: 0x00058F00
		private DirComboBox.DirComboBoxItem CreateFolderStack()
		{
			this.folderStack.Clear();
			DirectoryInfo directoryInfo = new DirectoryInfo(this.currentPath);
			this.folderStack.Push(directoryInfo);
			bool flag = !XplatUI.RunningOnUnix;
			while (directoryInfo.Parent != null)
			{
				directoryInfo = directoryInfo.Parent;
				if (this.mainParentDirComboBoxItem != this.personalDirComboboxItem && string.Compare(directoryInfo.FullName, ThemeEngine.Current.Places(UIIcon.PlacesDesktop), flag) == 0)
				{
					return this.desktopDirComboboxItem;
				}
				if (this.mainParentDirComboBoxItem == this.personalDirComboboxItem)
				{
					if (string.Compare(directoryInfo.FullName, ThemeEngine.Current.Places(UIIcon.PlacesPersonal), flag) == 0)
					{
						return this.personalDirComboboxItem;
					}
				}
				else
				{
					foreach (object obj in this.myComputerItems)
					{
						DirComboBox.DirComboBoxItem dirComboBoxItem = (DirComboBox.DirComboBoxItem)obj;
						if (string.Compare(dirComboBoxItem.Path, directoryInfo.FullName, flag) == 0)
						{
							return dirComboBoxItem;
						}
					}
				}
				this.folderStack.Push(directoryInfo);
			}
			return null;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0005AE4C File Offset: 0x0005904C
		private DirComboBox.DirComboBoxItem AppendToParent(int nr_indents, DirComboBox.DirComboBoxItem parentDirComboBoxItem)
		{
			DirComboBox.DirComboBoxItem dirComboBoxItem = null;
			int num = base.Items.IndexOf(parentDirComboBoxItem) + 1;
			int num2 = DirComboBox.indent * nr_indents;
			while (this.folderStack.Count != 0)
			{
				DirectoryInfo directoryInfo = this.folderStack.Pop() as DirectoryInfo;
				DirComboBox.DirComboBoxItem dirComboBoxItem2 = new DirComboBox.DirComboBoxItem(this.imageList, 5, directoryInfo.Name, directoryInfo.FullName, num2);
				base.Items.Insert(num, dirComboBoxItem2);
				num++;
				dirComboBoxItem = dirComboBoxItem2;
				num2 += DirComboBox.indent;
			}
			return dirComboBoxItem;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005AED4 File Offset: 0x000590D4
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index == -1)
			{
				return;
			}
			DirComboBox.DirComboBoxItem dirComboBoxItem = base.Items[e.Index] as DirComboBox.DirComboBoxItem;
			Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height, e.Graphics);
			Graphics graphics = Graphics.FromImage(bitmap);
			Color backColor = e.BackColor;
			Color color = e.ForeColor;
			int num = dirComboBoxItem.XPos;
			if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.None)
			{
				num = 0;
			}
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(backColor), new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && (!base.DroppedDown || (e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit))
			{
				color = ThemeEngine.Current.ColorHighlightText;
				int num2 = (int)graphics.MeasureString(dirComboBoxItem.Name, e.Font).Width;
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorHighlight), new Rectangle(num + 23, 1, num2 + 3, e.Bounds.Height - 2));
				if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
				{
					ControlPaint.DrawFocusRectangle(graphics, new Rectangle(num + 22, 0, num2 + 5, e.Bounds.Height), color, ThemeEngine.Current.ColorHighlight);
				}
			}
			graphics.DrawString(dirComboBoxItem.Name, e.Font, ThemeEngine.Current.ResPool.GetSolidBrush(color), new Point(24 + num, (bitmap.Height - e.Font.Height) / 2));
			graphics.DrawImage(dirComboBoxItem.ImageList.Images[dirComboBoxItem.ImageIndex], new Rectangle(new Point(num + 2, 0), new Size(16, 16)));
			e.Graphics.DrawImage(bitmap, e.Bounds.X, e.Bounds.Y);
			graphics.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0005B114 File Offset: 0x00059314
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.Items.Count > 0)
			{
				DirComboBox.DirComboBoxItem dirComboBoxItem = base.Items[this.SelectedIndex] as DirComboBox.DirComboBoxItem;
				this.currentPath = dirComboBoxItem.Path;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005B158 File Offset: 0x00059358
		protected override void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DirComboBox.CDirectoryChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000D86 RID: 3462
		private ImageList imageList = new ImageList();

		// Token: 0x04000D87 RID: 3463
		private string currentPath;

		// Token: 0x04000D88 RID: 3464
		private Stack folderStack = new Stack();

		// Token: 0x04000D89 RID: 3465
		private static readonly int indent = 6;

		// Token: 0x04000D8A RID: 3466
		private DirComboBox.DirComboBoxItem recentlyUsedDirComboboxItem;

		// Token: 0x04000D8B RID: 3467
		private DirComboBox.DirComboBoxItem desktopDirComboboxItem;

		// Token: 0x04000D8C RID: 3468
		private DirComboBox.DirComboBoxItem personalDirComboboxItem;

		// Token: 0x04000D8D RID: 3469
		private DirComboBox.DirComboBoxItem myComputerDirComboboxItem;

		// Token: 0x04000D8E RID: 3470
		private DirComboBox.DirComboBoxItem networkDirComboboxItem;

		// Token: 0x04000D8F RID: 3471
		private ArrayList myComputerItems = new ArrayList();

		// Token: 0x04000D90 RID: 3472
		private DirComboBox.DirComboBoxItem mainParentDirComboBoxItem;

		// Token: 0x04000D91 RID: 3473
		private DirComboBox.DirComboBoxItem real_parent;

		// Token: 0x04000D92 RID: 3474
		private MWFVFS vfs;

		// Token: 0x04000D93 RID: 3475
		private static object CDirectoryChangedEvent = new object();

		// Token: 0x0200016B RID: 363
		internal class DirComboBoxItem
		{
			// Token: 0x06001859 RID: 6233 RVA: 0x0005B190 File Offset: 0x00059390
			public DirComboBoxItem(ImageList imageList, int imageIndex, string name, string path, int xPos)
			{
				this.imageList = imageList;
				this.imageIndex = imageIndex;
				this.name = name;
				this.path = path;
				this.xPos = xPos;
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005B1C0 File Offset: 0x000593C0
			public int ImageIndex
			{
				get
				{
					return this.imageIndex;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x0600185B RID: 6235 RVA: 0x0005B1C8 File Offset: 0x000593C8
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005B1D0 File Offset: 0x000593D0
			public string Path
			{
				get
				{
					return this.path;
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x0600185D RID: 6237 RVA: 0x0005B1D8 File Offset: 0x000593D8
			public int XPos
			{
				get
				{
					return this.xPos;
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x0600185F RID: 6239 RVA: 0x0005B1EC File Offset: 0x000593EC
			// (set) Token: 0x0600185E RID: 6238 RVA: 0x0005B1E0 File Offset: 0x000593E0
			public ImageList ImageList
			{
				get
				{
					return this.imageList;
				}
				set
				{
					this.imageList = value;
				}
			}

			// Token: 0x06001860 RID: 6240 RVA: 0x0005B1F4 File Offset: 0x000593F4
			public override string ToString()
			{
				return this.name;
			}

			// Token: 0x04000D94 RID: 3476
			private int imageIndex;

			// Token: 0x04000D95 RID: 3477
			private string name;

			// Token: 0x04000D96 RID: 3478
			private string path;

			// Token: 0x04000D97 RID: 3479
			private int xPos;

			// Token: 0x04000D98 RID: 3480
			private ImageList imageList;
		}
	}
}
