using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200016E RID: 366
	internal class MWFFileView : ListView
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x0005B378 File Offset: 0x00059578
		public MWFFileView(MWFVFS vfs)
		{
			this.vfs = vfs;
			this.vfs.RegisterUpdateDelegate(new MWFVFS.UpdateDelegate(this.RealFileViewUpdate), this);
			base.SuspendLayout();
			this.contextMenu = new ContextMenu();
			this.toolTip = new ToolTip();
			this.toolTip.InitialDelay = 300;
			this.toolTip.ReshowDelay = 0;
			this.menuItemView = new MenuItem("View");
			this.smallIconMenutItem = new MenuItem("Small Icon", new EventHandler(this.OnClickViewMenuSubItem));
			this.smallIconMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.smallIconMenutItem);
			this.tilesMenutItem = new MenuItem("Tiles", new EventHandler(this.OnClickViewMenuSubItem));
			this.tilesMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.tilesMenutItem);
			this.largeIconMenutItem = new MenuItem("Large Icon", new EventHandler(this.OnClickViewMenuSubItem));
			this.largeIconMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.largeIconMenutItem);
			this.listMenutItem = new MenuItem("List", new EventHandler(this.OnClickViewMenuSubItem));
			this.listMenutItem.RadioCheck = true;
			this.listMenutItem.Checked = true;
			this.menuItemView.MenuItems.Add(this.listMenutItem);
			this.previousCheckedMenuItemIndex = this.listMenutItem.Index;
			this.detailsMenutItem = new MenuItem("Details", new EventHandler(this.OnClickViewMenuSubItem));
			this.detailsMenutItem.RadioCheck = true;
			this.menuItemView.MenuItems.Add(this.detailsMenutItem);
			this.contextMenu.MenuItems.Add(this.menuItemView);
			this.contextMenu.MenuItems.Add(new MenuItem("-"));
			this.menuItemNew = new MenuItem("New");
			this.newFolderMenuItem = new MenuItem("New Folder", new EventHandler(this.OnClickNewFolderMenuItem));
			this.menuItemNew.MenuItems.Add(this.newFolderMenuItem);
			this.contextMenu.MenuItems.Add(this.menuItemNew);
			this.contextMenu.MenuItems.Add(new MenuItem("-"));
			this.showHiddenFilesMenuItem = new MenuItem("Show hidden files", new EventHandler(this.OnClickContextMenu));
			this.showHiddenFilesMenuItem.Checked = this.showHiddenFiles;
			this.contextMenu.MenuItems.Add(this.showHiddenFilesMenuItem);
			base.LabelWrap = true;
			base.SmallImageList = MimeIconEngine.SmallIcons;
			base.LargeImageList = MimeIconEngine.LargeIcons;
			base.View = (this.old_view = View.List);
			base.LabelEdit = true;
			this.ContextMenu = this.contextMenu;
			this.columns = new ColumnHeader[4];
			this.columns[0] = this.CreateColumnHeader(" Name", 170, HorizontalAlignment.Left);
			this.columns[1] = this.CreateColumnHeader("Size ", 80, HorizontalAlignment.Right);
			this.columns[2] = this.CreateColumnHeader(" Type", 100, HorizontalAlignment.Left);
			this.columns[3] = this.CreateColumnHeader(" Last Access", 150, HorizontalAlignment.Left);
			base.AllowColumnReorder = true;
			base.ResumeLayout(false);
			base.KeyDown += this.MWF_KeyDown;
		}

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x0600186D RID: 6253 RVA: 0x0005B778 File Offset: 0x00059978
		// (remove) Token: 0x0600186E RID: 6254 RVA: 0x0005B78C File Offset: 0x0005998C
		public event EventHandler SelectedFileChanged
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MSelectedFileChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MSelectedFileChangedEvent, value);
			}
		}

		// Token: 0x14000193 RID: 403
		// (add) Token: 0x0600186F RID: 6255 RVA: 0x0005B7A0 File Offset: 0x000599A0
		// (remove) Token: 0x06001870 RID: 6256 RVA: 0x0005B7B4 File Offset: 0x000599B4
		public event EventHandler SelectedFilesChanged
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MSelectedFilesChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MSelectedFilesChangedEvent, value);
			}
		}

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x06001871 RID: 6257 RVA: 0x0005B7C8 File Offset: 0x000599C8
		// (remove) Token: 0x06001872 RID: 6258 RVA: 0x0005B7DC File Offset: 0x000599DC
		public event EventHandler DirectoryChanged
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MDirectoryChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MDirectoryChangedEvent, value);
			}
		}

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x06001873 RID: 6259 RVA: 0x0005B7F0 File Offset: 0x000599F0
		// (remove) Token: 0x06001874 RID: 6260 RVA: 0x0005B804 File Offset: 0x00059A04
		public event EventHandler ForceDialogEnd
		{
			add
			{
				base.Events.AddHandler(MWFFileView.MForceDialogEndEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MWFFileView.MForceDialogEndEvent, value);
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0005B818 File Offset: 0x00059A18
		private ColumnHeader CreateColumnHeader(string text, int width, HorizontalAlignment alignment)
		{
			return new ColumnHeader
			{
				Text = text,
				Width = width,
				TextAlign = alignment
			};
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0005B844 File Offset: 0x00059A44
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0005B84C File Offset: 0x00059A4C
		public string CurrentFolder
		{
			get
			{
				return this.currentFolder;
			}
			set
			{
				this.currentFolder = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0005B858 File Offset: 0x00059A58
		public string CurrentRealFolder
		{
			get
			{
				return this.currentRealFolder;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0005B860 File Offset: 0x00059A60
		public FSEntry CurrentFSEntry
		{
			get
			{
				return this.currentFSEntry;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0005B868 File Offset: 0x00059A68
		public MenuItem[] ViewMenuItems
		{
			get
			{
				MenuItem[] array = new MenuItem[]
				{
					this.smallIconMenutItem.CloneMenu(),
					this.tilesMenutItem.CloneMenu(),
					this.largeIconMenutItem.CloneMenu(),
					this.listMenutItem.CloneMenu(),
					this.detailsMenutItem.CloneMenu()
				};
				this.viewMenuItemClones.Add(array);
				return array;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005B8DC File Offset: 0x00059ADC
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0005B8D0 File Offset: 0x00059AD0
		public ArrayList FilterArrayList
		{
			get
			{
				return this.filterArrayList;
			}
			set
			{
				this.filterArrayList = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		public bool ShowHiddenFiles
		{
			get
			{
				return this.showHiddenFiles;
			}
			set
			{
				this.showHiddenFiles = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005B914 File Offset: 0x00059B14
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0005B8F8 File Offset: 0x00059AF8
		public int FilterIndex
		{
			get
			{
				return this.filterIndex;
			}
			set
			{
				this.filterIndex = value;
				if (base.Visible)
				{
					this.UpdateFileView();
				}
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005B928 File Offset: 0x00059B28
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0005B91C File Offset: 0x00059B1C
		public string SelectedFilesString
		{
			get
			{
				return this.selectedFilesString;
			}
			set
			{
				this.selectedFilesString = value;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0005B930 File Offset: 0x00059B30
		public void PushDir()
		{
			if (this.currentFolder != null)
			{
				this.directoryStack.Push(this.currentFolder);
			}
			this.EnableOrDisableDirstackObjects();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0005B960 File Offset: 0x00059B60
		public void PopDir()
		{
			this.PopDir(null);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0005B96C File Offset: 0x00059B6C
		public void PopDir(string filter)
		{
			if (this.directoryStack.Count == 0)
			{
				return;
			}
			string text = this.directoryStack.Pop() as string;
			this.EnableOrDisableDirstackObjects();
			this.should_push = false;
			this.ChangeDirectory(null, text, filter);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0005B9B4 File Offset: 0x00059BB4
		public void RegisterSender(IUpdateFolder iud)
		{
			this.registered_senders.Add(iud);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0005B9C4 File Offset: 0x00059BC4
		public void CreateNewFolder()
		{
			if (this.currentFolder == MWFVFS.MyComputerPrefix || this.currentFolder == MWFVFS.RecentlyUsedPrefix)
			{
				return;
			}
			FSEntry fsentry = new FSEntry();
			fsentry.Attributes = 16;
			fsentry.FileType = FSEntry.FSEntryType.Directory;
			fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("inode/directory");
			fsentry.LastAccessTime = DateTime.Now;
			TextEntryDialog textEntryDialog = new TextEntryDialog();
			textEntryDialog.IconPictureBoxImage = MimeIconEngine.LargeIcons.Images.GetImage(fsentry.IconIndex);
			string text = string.Empty;
			if (this.currentFolderFSEntry.RealName != null)
			{
				text = this.currentFolderFSEntry.RealName;
			}
			else
			{
				text = this.currentFolder;
			}
			string text2 = "New Folder";
			if (Directory.Exists(Path.Combine(text, text2)))
			{
				int num = 1;
				if (XplatUI.RunningOnUnix)
				{
					text2 = text2 + "-" + num;
				}
				else
				{
					text2 = string.Concat(new object[] { text2, " (", num, ")" });
				}
				while (Directory.Exists(Path.Combine(text, text2)))
				{
					num++;
					if (XplatUI.RunningOnUnix)
					{
						text2 = "New Folder-" + num;
					}
					else
					{
						text2 = "New Folder (" + num + ")";
					}
				}
			}
			textEntryDialog.FileName = text2;
			if (textEntryDialog.ShowDialog() == DialogResult.OK)
			{
				string text3 = Path.Combine(text, textEntryDialog.FileName);
				if (this.vfs.CreateFolder(text3))
				{
					fsentry.FullName = text3;
					fsentry.Name = textEntryDialog.FileName;
					FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsentry);
					base.BeginUpdate();
					base.Items.Add(fileViewListViewItem);
					base.EndUpdate();
					fileViewListViewItem.EnsureVisible();
				}
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0005BBA8 File Offset: 0x00059DA8
		public void SetSelectedIndexTo(string fname)
		{
			foreach (object obj in base.Items)
			{
				FileViewListViewItem fileViewListViewItem = (FileViewListViewItem)obj;
				if (fileViewListViewItem.Text == fname)
				{
					base.BeginUpdate();
					base.SelectedItems.Clear();
					fileViewListViewItem.Selected = true;
					base.EndUpdate();
					break;
				}
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0005BC44 File Offset: 0x00059E44
		public void OneDirUp()
		{
			this.OneDirUp(null);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0005BC50 File Offset: 0x00059E50
		public void OneDirUp(string filter)
		{
			string parent = this.vfs.GetParent();
			if (parent != null)
			{
				this.ChangeDirectory(null, parent, filter);
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0005BC78 File Offset: 0x00059E78
		public void ChangeDirectory(object sender, string folder)
		{
			this.ChangeDirectory(sender, folder, null);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0005BC84 File Offset: 0x00059E84
		public void ChangeDirectory(object sender, string folder, string filter)
		{
			if (folder == MWFVFS.DesktopPrefix || folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.folderUpToolBarButton.Enabled = false;
			}
			else
			{
				this.folderUpToolBarButton.Enabled = true;
			}
			foreach (object obj in this.registered_senders)
			{
				IUpdateFolder updateFolder = (IUpdateFolder)obj;
				updateFolder.CurrentFolder = folder;
			}
			if (this.should_push)
			{
				this.PushDir();
			}
			else
			{
				this.should_push = true;
			}
			this.currentFolderFSEntry = this.vfs.ChangeDirectory(folder);
			this.currentFolder = folder;
			if (this.currentFolder.IndexOf("://") != -1)
			{
				this.currentRealFolder = this.currentFolderFSEntry.RealName;
			}
			else
			{
				this.currentRealFolder = this.currentFolder;
			}
			base.BeginUpdate();
			base.Items.Clear();
			base.SelectedItems.Clear();
			if (folder == MWFVFS.RecentlyUsedPrefix)
			{
				this.old_view = base.View;
				base.View = View.Details;
				this.old_menuitem_index = this.previousCheckedMenuItemIndex;
				this.UpdateMenuItems(this.detailsMenutItem);
				this.do_update_view = true;
			}
			else if (base.View != this.old_view && this.do_update_view)
			{
				this.UpdateMenuItems(this.menuItemView.MenuItems[this.old_menuitem_index]);
				base.View = this.old_view;
				this.do_update_view = false;
			}
			base.EndUpdate();
			try
			{
				this.UpdateFileView(filter);
			}
			catch (Exception ex)
			{
				if (this.should_push)
				{
					this.PopDir();
				}
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0005BEA4 File Offset: 0x0005A0A4
		public void UpdateFileView()
		{
			this.UpdateFileView(null);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0005BEB0 File Offset: 0x0005A0B0
		public void UpdateFileView(string custom_filter)
		{
			if (custom_filter != null)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.Add(custom_filter);
				this.vfs.GetFolderContent(stringCollection);
			}
			else if (this.filterArrayList != null && this.filterArrayList.Count != 0)
			{
				FilterStruct filterStruct = (FilterStruct)this.filterArrayList[this.filterIndex - 1];
				this.vfs.GetFolderContent(filterStruct.filters);
			}
			else
			{
				this.vfs.GetFolderContent();
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0005BF38 File Offset: 0x0005A138
		public void RealFileViewUpdate(ArrayList directoriesArrayList, ArrayList fileArrayList)
		{
			base.BeginUpdate();
			base.Items.Clear();
			base.SelectedItems.Clear();
			foreach (object obj in directoriesArrayList)
			{
				FSEntry fsentry = (FSEntry)obj;
				if (this.ShowHiddenFiles || (!fsentry.Name.StartsWith(".") && (fsentry.Attributes & 2) != 2))
				{
					FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsentry);
					base.Items.Add(fileViewListViewItem);
				}
			}
			StringCollection stringCollection = new StringCollection();
			foreach (object obj2 in fileArrayList)
			{
				FSEntry fsentry2 = (FSEntry)obj2;
				if (stringCollection.Contains(fsentry2.Name))
				{
					string text = fsentry2.Name;
					if (stringCollection.Contains(text))
					{
						int num = 1;
						while (stringCollection.Contains(string.Concat(new object[] { text, "(", num, ")" })))
						{
							num++;
						}
						text = string.Concat(new object[] { text, "(", num, ")" });
					}
					fsentry2.Name = text;
				}
				stringCollection.Add(fsentry2.Name);
				this.DoOneFSEntry(fsentry2);
			}
			base.EndUpdate();
			stringCollection.Clear();
			stringCollection = null;
			directoriesArrayList.Clear();
			fileArrayList.Clear();
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0005C134 File Offset: 0x0005A334
		public void AddControlToEnableDisableByDirStack(object control)
		{
			this.dirStackControlsOrComponents.Add(control);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0005C144 File Offset: 0x0005A344
		public void SetFolderUpToolBarButton(ToolBarButton tb)
		{
			this.folderUpToolBarButton = tb;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0005C150 File Offset: 0x0005A350
		public void WriteRecentlyUsed(string fullfilename)
		{
			this.vfs.WriteRecentlyUsedFiles(fullfilename);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0005C160 File Offset: 0x0005A360
		private void EnableOrDisableDirstackObjects()
		{
			foreach (object obj in this.dirStackControlsOrComponents)
			{
				if (obj is Control)
				{
					Control control = obj as Control;
					control.Enabled = this.directoryStack.Count > 1;
				}
				else if (obj is ToolBarButton)
				{
					ToolBarButton toolBarButton = obj as ToolBarButton;
					toolBarButton.Enabled = this.directoryStack.Count > 0;
				}
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0005C218 File Offset: 0x0005A418
		private void DoOneFSEntry(FSEntry fsEntry)
		{
			if (!this.ShowHiddenFiles && (fsEntry.Name.StartsWith(".") || (fsEntry.Attributes & 2) == 2))
			{
				return;
			}
			FileViewListViewItem fileViewListViewItem = new FileViewListViewItem(fsEntry);
			base.Items.Add(fileViewListViewItem);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0005C268 File Offset: 0x0005A468
		private void MWF_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				this.OneDirUp();
			}
			else if (e.Control && e.KeyCode == Keys.A && base.MultiSelect)
			{
				foreach (object obj in base.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					listViewItem.Selected = true;
				}
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0005C314 File Offset: 0x0005A514
		internal void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0005C324 File Offset: 0x0005A524
		internal void PerformDoubleClick()
		{
			this.OnDoubleClick(EventArgs.Empty);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0005C334 File Offset: 0x0005A534
		protected override void OnClick(EventArgs e)
		{
			if (!base.MultiSelect && base.SelectedItems.Count > 0)
			{
				FileViewListViewItem fileViewListViewItem = base.SelectedItems[0] as FileViewListViewItem;
				FSEntry fsentry = fileViewListViewItem.FSEntry;
				if (fsentry.FileType == FSEntry.FSEntryType.File)
				{
					this.currentFSEntry = fsentry;
					EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFileChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
				}
			}
			base.OnClick(e);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
		protected override void OnDoubleClick(EventArgs e)
		{
			if (base.SelectedItems.Count > 0)
			{
				FileViewListViewItem fileViewListViewItem = base.SelectedItems[0] as FileViewListViewItem;
				FSEntry fsentry = fileViewListViewItem.FSEntry;
				if ((fsentry.Attributes & 16) != 16)
				{
					this.currentFSEntry = fsentry;
					EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFileChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
					eventHandler = (EventHandler)base.Events[MWFFileView.MForceDialogEndEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
					return;
				}
				this.ChangeDirectory(null, fsentry.FullName);
				EventHandler eventHandler2 = (EventHandler)base.Events[MWFFileView.MDirectoryChangedEvent];
				if (eventHandler2 != null)
				{
					eventHandler2.Invoke(this, EventArgs.Empty);
				}
			}
			base.OnDoubleClick(e);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0005C498 File Offset: 0x0005A698
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.SelectedItems.Count > 0)
			{
				this.selectedFilesString = string.Empty;
				if (base.SelectedItems.Count == 1)
				{
					FileViewListViewItem fileViewListViewItem = base.SelectedItems[0] as FileViewListViewItem;
					FSEntry fsentry = fileViewListViewItem.FSEntry;
					if ((fsentry.Attributes & 16) != 16)
					{
						this.selectedFilesString = base.SelectedItems[0].Text;
					}
				}
				else
				{
					foreach (object obj in base.SelectedItems)
					{
						FileViewListViewItem fileViewListViewItem2 = (FileViewListViewItem)obj;
						FSEntry fsentry2 = fileViewListViewItem2.FSEntry;
						if ((fsentry2.Attributes & 16) != 16)
						{
							this.selectedFilesString = this.selectedFilesString + "\"" + fileViewListViewItem2.Text + "\" ";
						}
					}
				}
				EventHandler eventHandler = (EventHandler)base.Events[MWFFileView.MSelectedFilesChangedEvent];
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, EventArgs.Empty);
				}
			}
			base.OnSelectedIndexChanged(e);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0005C5E4 File Offset: 0x0005A7E4
		protected override void OnMouseMove(MouseEventArgs e)
		{
			FileViewListViewItem fileViewListViewItem = base.GetItemAt(e.X, e.Y) as FileViewListViewItem;
			if (fileViewListViewItem != null)
			{
				int index = fileViewListViewItem.Index;
				if (index != this.oldItemIndexForToolTip)
				{
					this.oldItemIndexForToolTip = index;
					if (this.toolTip != null && this.toolTip.Active)
					{
						this.toolTip.Active = false;
					}
					FSEntry fsentry = fileViewListViewItem.FSEntry;
					string text = string.Empty;
					if (fsentry.FileType == FSEntry.FSEntryType.Directory)
					{
						text = "Directory: " + fsentry.FullName;
					}
					else if (fsentry.FileType == FSEntry.FSEntryType.Device)
					{
						text = "Device: " + fsentry.FullName;
					}
					else if (fsentry.FileType == FSEntry.FSEntryType.Network)
					{
						text = "Network: " + fsentry.FullName;
					}
					else
					{
						text = "File: " + fsentry.FullName;
					}
					this.toolTip.SetToolTip(this, text);
					this.toolTip.Active = true;
				}
			}
			else
			{
				this.toolTip.Active = false;
			}
			base.OnMouseMove(e);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0005C708 File Offset: 0x0005A908
		private void OnClickContextMenu(object sender, EventArgs e)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem == this.showHiddenFilesMenuItem)
			{
				menuItem.Checked = !menuItem.Checked;
				this.showHiddenFiles = menuItem.Checked;
				this.UpdateFileView();
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0005C74C File Offset: 0x0005A94C
		private void OnClickViewMenuSubItem(object sender, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			this.UpdateMenuItems(menuItem);
			base.BeginUpdate();
			switch (menuItem.Index)
			{
			case 0:
				base.View = View.SmallIcon;
				break;
			case 1:
				base.View = View.Tile;
				break;
			case 2:
				base.View = View.LargeIcon;
				break;
			case 3:
				base.View = View.List;
				break;
			case 4:
				base.View = View.Details;
				break;
			}
			if (base.View == View.Details)
			{
				base.Columns.AddRange(this.columns);
			}
			else
			{
				base.ListViewItemSorter = null;
				base.Columns.Clear();
			}
			base.EndUpdate();
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0005C810 File Offset: 0x0005AA10
		protected override void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			FileViewListViewItem fileViewListViewItem = base.SelectedItems[0] as FileViewListViewItem;
			FSEntry fsentry = fileViewListViewItem.FSEntry;
			if (fsentry.FileType != FSEntry.FSEntryType.Directory && fsentry.FileType != FSEntry.FSEntryType.File)
			{
				e.CancelEdit = true;
			}
			base.OnBeforeLabelEdit(e);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005C85C File Offset: 0x0005AA5C
		protected override void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			base.OnAfterLabelEdit(e);
			if (e.Label == null || base.Items[e.Item].Text == e.Label)
			{
				return;
			}
			FileViewListViewItem fileViewListViewItem = base.SelectedItems[0] as FileViewListViewItem;
			FSEntry fsentry = fileViewListViewItem.FSEntry;
			string text = ((this.currentFolderFSEntry.RealName == null) ? this.currentFolder : this.currentFolderFSEntry.RealName);
			FSEntry.FSEntryType fileType = fsentry.FileType;
			if (fileType != FSEntry.FSEntryType.File)
			{
				if (fileType == FSEntry.FSEntryType.Directory)
				{
					string text2 = ((fsentry.RealName == null) ? fsentry.FullName : fsentry.RealName);
					string text3 = Path.Combine(text, e.Label);
					if (!this.vfs.MoveFolder(text2, text3))
					{
						e.CancelEdit = true;
					}
					else if (fsentry.RealName != null)
					{
						fsentry.RealName = text3;
					}
					else
					{
						fsentry.FullName = text3;
					}
				}
			}
			else
			{
				string text4 = ((fsentry.RealName == null) ? fsentry.FullName : fsentry.RealName);
				string text5 = Path.Combine(text, e.Label);
				if (!this.vfs.MoveFile(text4, text5))
				{
					e.CancelEdit = true;
				}
				else if (fsentry.RealName != null)
				{
					fsentry.RealName = text5;
				}
				else
				{
					fsentry.FullName = text5;
				}
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005C9E0 File Offset: 0x0005ABE0
		private void UpdateMenuItems(MenuItem senderMenuItem)
		{
			this.menuItemView.MenuItems[this.previousCheckedMenuItemIndex].Checked = false;
			this.menuItemView.MenuItems[senderMenuItem.Index].Checked = true;
			foreach (object obj in this.viewMenuItemClones)
			{
				MenuItem[] array = (MenuItem[])obj;
				array[this.previousCheckedMenuItemIndex].Checked = false;
				array[senderMenuItem.Index].Checked = true;
			}
			this.previousCheckedMenuItemIndex = senderMenuItem.Index;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0005CAA8 File Offset: 0x0005ACA8
		private void OnClickNewFolderMenuItem(object sender, EventArgs e)
		{
			this.CreateNewFolder();
		}

		// Token: 0x04000D9D RID: 3485
		private ArrayList filterArrayList;

		// Token: 0x04000D9E RID: 3486
		private bool showHiddenFiles;

		// Token: 0x04000D9F RID: 3487
		private string selectedFilesString;

		// Token: 0x04000DA0 RID: 3488
		private int filterIndex = 1;

		// Token: 0x04000DA1 RID: 3489
		private ToolTip toolTip;

		// Token: 0x04000DA2 RID: 3490
		private int oldItemIndexForToolTip = -1;

		// Token: 0x04000DA3 RID: 3491
		private ContextMenu contextMenu;

		// Token: 0x04000DA4 RID: 3492
		private MenuItem menuItemView;

		// Token: 0x04000DA5 RID: 3493
		private MenuItem menuItemNew;

		// Token: 0x04000DA6 RID: 3494
		private MenuItem smallIconMenutItem;

		// Token: 0x04000DA7 RID: 3495
		private MenuItem tilesMenutItem;

		// Token: 0x04000DA8 RID: 3496
		private MenuItem largeIconMenutItem;

		// Token: 0x04000DA9 RID: 3497
		private MenuItem listMenutItem;

		// Token: 0x04000DAA RID: 3498
		private MenuItem detailsMenutItem;

		// Token: 0x04000DAB RID: 3499
		private MenuItem newFolderMenuItem;

		// Token: 0x04000DAC RID: 3500
		private MenuItem showHiddenFilesMenuItem;

		// Token: 0x04000DAD RID: 3501
		private int previousCheckedMenuItemIndex;

		// Token: 0x04000DAE RID: 3502
		private ArrayList viewMenuItemClones = new ArrayList();

		// Token: 0x04000DAF RID: 3503
		private FSEntry currentFSEntry;

		// Token: 0x04000DB0 RID: 3504
		private string currentFolder;

		// Token: 0x04000DB1 RID: 3505
		private string currentRealFolder;

		// Token: 0x04000DB2 RID: 3506
		private FSEntry currentFolderFSEntry;

		// Token: 0x04000DB3 RID: 3507
		private Stack directoryStack = new Stack();

		// Token: 0x04000DB4 RID: 3508
		private ArrayList dirStackControlsOrComponents = new ArrayList();

		// Token: 0x04000DB5 RID: 3509
		private ToolBarButton folderUpToolBarButton;

		// Token: 0x04000DB6 RID: 3510
		private ArrayList registered_senders = new ArrayList();

		// Token: 0x04000DB7 RID: 3511
		private bool should_push = true;

		// Token: 0x04000DB8 RID: 3512
		private MWFVFS vfs;

		// Token: 0x04000DB9 RID: 3513
		private View old_view;

		// Token: 0x04000DBA RID: 3514
		private int old_menuitem_index;

		// Token: 0x04000DBB RID: 3515
		private bool do_update_view;

		// Token: 0x04000DBC RID: 3516
		private ColumnHeader[] columns;

		// Token: 0x04000DBD RID: 3517
		private static object MSelectedFileChangedEvent = new object();

		// Token: 0x04000DBE RID: 3518
		private static object MSelectedFilesChangedEvent = new object();

		// Token: 0x04000DBF RID: 3519
		private static object MDirectoryChangedEvent = new object();

		// Token: 0x04000DC0 RID: 3520
		private static object MForceDialogEndEvent = new object();
	}
}
