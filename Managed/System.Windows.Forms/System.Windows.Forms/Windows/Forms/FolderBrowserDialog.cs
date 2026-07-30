using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a folder. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018E RID: 398
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("HelpRequest")]
	[DefaultProperty("SelectedPath")]
	public sealed class FolderBrowserDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FolderBrowserDialog" /> class.</summary>
		// Token: 0x0600196B RID: 6507 RVA: 0x00060DA0 File Offset: 0x0005EFA0
		public FolderBrowserDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			Size empty = Size.Empty;
			Point empty2 = Point.Empty;
			object value = MWFConfig.GetValue(this.folderbrowserdialog_string, this.width_string);
			object value2 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.height_string);
			if (value2 != null && value != null)
			{
				empty..ctor((int)value, (int)value2);
			}
			object value3 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.x_string);
			object value4 = MWFConfig.GetValue(this.folderbrowserdialog_string, this.y_string);
			if (value3 != null && value4 != null)
			{
				empty2..ctor((int)value3, (int)value4);
			}
			this.newFolderButton = new Button();
			this.folderBrowserTreeView = new FolderBrowserDialog.FolderBrowserTreeView(this);
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.descriptionLabel = new Label();
			this.folderBrowserTreeViewContextMenu = new ContextMenu();
			this.form.AcceptButton = this.okButton;
			this.form.CancelButton = this.cancelButton;
			this.form.SuspendLayout();
			this.form.ClientSize = new Size(322, 324);
			this.form.MinimumSize = new Size(310, 254);
			this.form.Text = "Browse For Folder";
			this.form.SizeGripStyle = SizeGripStyle.Show;
			this.newFolderMenuItem = new MenuItem("New Folder", new EventHandler(this.OnClickNewFolderButton));
			this.folderBrowserTreeViewContextMenu.MenuItems.Add(this.newFolderMenuItem);
			this.descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.descriptionLabel.Location = new Point(15, 14);
			this.descriptionLabel.Size = new Size(292, 40);
			this.descriptionLabel.TabIndex = 0;
			this.descriptionLabel.Text = string.Empty;
			this.folderBrowserTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.folderBrowserTreeView.ImageIndex = -1;
			this.folderBrowserTreeView.Location = new Point(15, 60);
			this.folderBrowserTreeView.SelectedImageIndex = -1;
			this.folderBrowserTreeView.Size = new Size(292, 212);
			this.folderBrowserTreeView.TabIndex = 3;
			this.folderBrowserTreeView.ShowLines = false;
			this.folderBrowserTreeView.ShowPlusMinus = true;
			this.folderBrowserTreeView.HotTracking = true;
			this.folderBrowserTreeView.BorderStyle = BorderStyle.Fixed3D;
			this.folderBrowserTreeView.ContextMenu = this.folderBrowserTreeViewContextMenu;
			this.newFolderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.newFolderButton.FlatStyle = FlatStyle.System;
			this.newFolderButton.Location = new Point(15, 285);
			this.newFolderButton.Size = new Size(105, 23);
			this.newFolderButton.TabIndex = 4;
			this.newFolderButton.Text = "Make New Folder";
			this.newFolderButton.Enabled = true;
			this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Location = new Point(135, 285);
			this.okButton.Size = new Size(80, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(227, 285);
			this.cancelButton.Size = new Size(80, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.okButton);
			this.form.Controls.Add(this.newFolderButton);
			this.form.Controls.Add(this.folderBrowserTreeView);
			this.form.Controls.Add(this.descriptionLabel);
			this.form.ResumeLayout(false);
			if (empty != Size.Empty)
			{
				this.form.Size = empty;
			}
			if (empty2 != Point.Empty)
			{
				this.form.Location = empty2;
			}
			this.okButton.Click += new EventHandler(this.OnClickOKButton);
			this.cancelButton.Click += new EventHandler(this.OnClickCancelButton);
			this.newFolderButton.Click += new EventHandler(this.OnClickNewFolderButton);
			this.form.VisibleChanged += new EventHandler(this.OnFormVisibleChanged);
			this.RootFolder = this.rootFolder;
		}

		/// <summary>Occurs when the user clicks the Help button on the dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000196 RID: 406
		// (add) Token: 0x0600196C RID: 6508 RVA: 0x000612EC File Offset: 0x0005F4EC
		// (remove) Token: 0x0600196D RID: 6509 RVA: 0x000612F8 File Offset: 0x0005F4F8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler HelpRequest
		{
			add
			{
				base.HelpRequest += value;
			}
			remove
			{
				base.HelpRequest -= value;
			}
		}

		/// <summary>Gets or sets the descriptive text displayed above the tree view control in the dialog box.</summary>
		/// <returns>The description to display. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00061314 File Offset: 0x0005F514
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x00061304 File Offset: 0x0005F504
		[Localizable(true)]
		[DefaultValue("")]
		[Browsable(true)]
		public string Description
		{
			get
			{
				return this.descriptionLabel.Text;
			}
			set
			{
				this.descriptionLabel.Text = value;
			}
		}

		/// <summary>Gets or sets the root folder where the browsing starts from.</summary>
		/// <returns>One of the <see cref="T:System.Environment.SpecialFolder" /> values. The default is Desktop.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Environment.SpecialFolder" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00061364 File Offset: 0x0005F564
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x00061324 File Offset: 0x0005F524
		[TypeConverter(typeof(SpecialFolderEnumConverter))]
		[Localizable(false)]
		[DefaultValue(0)]
		[Browsable(true)]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				return this.rootFolder;
			}
			set
			{
				Type typeFromHandle = typeof(Environment.SpecialFolder);
				if (!Enum.IsDefined(typeFromHandle, value))
				{
					throw new InvalidEnumArgumentException("value", value, typeFromHandle);
				}
				this.rootFolder = value;
			}
		}

		/// <summary>Gets or sets the path selected by the user.</summary>
		/// <returns>The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0006138C File Offset: 0x0005F58C
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x0006136C File Offset: 0x0005F56C
		[DefaultValue("")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		public string SelectedPath
		{
			get
			{
				return this.selectedPath;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.selectedPath = value;
				this.old_selectedPath = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.</summary>
		/// <returns>true if the New Folder button is shown in the dialog box; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000613B8 File Offset: 0x0005F5B8
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x00061394 File Offset: 0x0005F594
		[DefaultValue(true)]
		[Localizable(false)]
		[Browsable(true)]
		public bool ShowNewFolderButton
		{
			get
			{
				return this.showNewFolderButton;
			}
			set
			{
				if (value != this.showNewFolderButton)
				{
					this.newFolderButton.Visible = value;
					this.showNewFolderButton = value;
				}
			}
		}

		/// <summary>Resets properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001976 RID: 6518 RVA: 0x000613C0 File Offset: 0x0005F5C0
		public override void Reset()
		{
			this.Description = string.Empty;
			this.RootFolder = 0;
			this.selectedPath = string.Empty;
			this.ShowNewFolderButton = true;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000613F4 File Offset: 0x0005F5F4
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			this.folderBrowserTreeView.RootFolder = this.RootFolder;
			this.folderBrowserTreeView.SelectedPath = this.SelectedPath;
			this.form.Refresh();
			return true;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00061430 File Offset: 0x0005F630
		private void OnClickOKButton(object sender, EventArgs e)
		{
			this.WriteConfigValues();
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00061444 File Offset: 0x0005F644
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			this.WriteConfigValues();
			this.selectedPath = this.old_selectedPath;
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00061464 File Offset: 0x0005F664
		private void OnClickNewFolderButton(object sender, EventArgs e)
		{
			this.folderBrowserTreeView.CreateNewFolder();
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00061474 File Offset: 0x0005F674
		private void OnFormVisibleChanged(object sender, EventArgs e)
		{
			if (this.form.Visible && this.okButton.Enabled)
			{
				this.okButton.Select();
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000614A4 File Offset: 0x0005F6A4
		private void WriteConfigValues()
		{
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.width_string, this.form.Width);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.height_string, this.form.Height);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.x_string, this.form.Location.X);
			MWFConfig.SetValue(this.folderbrowserdialog_string, this.y_string, this.form.Location.Y);
		}

		// Token: 0x04000E4C RID: 3660
		private Environment.SpecialFolder rootFolder;

		// Token: 0x04000E4D RID: 3661
		private string selectedPath = string.Empty;

		// Token: 0x04000E4E RID: 3662
		private bool showNewFolderButton = true;

		// Token: 0x04000E4F RID: 3663
		private Label descriptionLabel;

		// Token: 0x04000E50 RID: 3664
		private Button cancelButton;

		// Token: 0x04000E51 RID: 3665
		private Button okButton;

		// Token: 0x04000E52 RID: 3666
		private FolderBrowserDialog.FolderBrowserTreeView folderBrowserTreeView;

		// Token: 0x04000E53 RID: 3667
		private Button newFolderButton;

		// Token: 0x04000E54 RID: 3668
		private ContextMenu folderBrowserTreeViewContextMenu;

		// Token: 0x04000E55 RID: 3669
		private MenuItem newFolderMenuItem;

		// Token: 0x04000E56 RID: 3670
		private string old_selectedPath = string.Empty;

		// Token: 0x04000E57 RID: 3671
		private readonly string folderbrowserdialog_string = "FolderBrowserDialog";

		// Token: 0x04000E58 RID: 3672
		private readonly string width_string = "Width";

		// Token: 0x04000E59 RID: 3673
		private readonly string height_string = "Height";

		// Token: 0x04000E5A RID: 3674
		private readonly string x_string = "X";

		// Token: 0x04000E5B RID: 3675
		private readonly string y_string = "Y";

		// Token: 0x0200018F RID: 399
		internal class FolderBrowserTreeView : TreeView
		{
			// Token: 0x0600197D RID: 6525 RVA: 0x00061548 File Offset: 0x0005F748
			public FolderBrowserTreeView(FolderBrowserDialog parent_dialog)
			{
				this.parentDialog = parent_dialog;
				base.HideSelection = false;
				base.ImageList = this.imageList;
				this.SetupImageList();
			}

			// Token: 0x17000612 RID: 1554
			// (set) Token: 0x0600197E RID: 6526 RVA: 0x00061594 File Offset: 0x0005F794
			public Environment.SpecialFolder RootFolder
			{
				set
				{
					this.rootFolder = value;
					string text = string.Empty;
					Environment.SpecialFolder specialFolder = this.rootFolder;
					switch (specialFolder)
					{
					case 5:
						this.root_node = new FolderBrowserDialog.FBTreeNode("Personal");
						text = MWFVFS.PersonalPrefix;
						this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
						break;
					default:
						if (specialFolder != null)
						{
							if (specialFolder != 17)
							{
								this.root_node = new FolderBrowserDialog.FBTreeNode(this.rootFolder.ToString());
								this.root_node.RealPath = Environment.GetFolderPath(this.rootFolder);
								text = this.root_node.RealPath;
							}
							else
							{
								this.root_node = new FolderBrowserDialog.FBTreeNode("My Computer");
								text = MWFVFS.MyComputerPrefix;
							}
						}
						else
						{
							this.root_node = new FolderBrowserDialog.FBTreeNode("Desktop");
							this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
							text = MWFVFS.DesktopPrefix;
						}
						break;
					case 8:
						this.root_node = new FolderBrowserDialog.FBTreeNode("My Recent Documents");
						this.root_node.RealPath = ThemeEngine.Current.Places(UIIcon.PlacesRecentDocuments);
						text = MWFVFS.RecentlyUsedPrefix;
						break;
					}
					this.root_node.Tag = text;
					this.root_node.ImageIndex = this.NodeImageIndex(text);
					base.BeginUpdate();
					base.Nodes.Clear();
					base.EndUpdate();
					this.FillNode(this.root_node);
					this.root_node.Expand();
					base.Nodes.Add(this.root_node);
				}
			}

			// Token: 0x17000613 RID: 1555
			// (set) Token: 0x0600197F RID: 6527 RVA: 0x00061730 File Offset: 0x0005F930
			public string SelectedPath
			{
				set
				{
					if (value.Length == 0)
					{
						return;
					}
					if (!Path.IsPathRooted(value))
					{
						return;
					}
					try
					{
						if (this.Check_if_path_is_child_of_RootFolder(value))
						{
							this.SetSelectedPath(Path.GetFullPath(value));
						}
					}
					catch (Exception)
					{
						base.EndUpdate();
						this.RootFolder = this.rootFolder;
					}
				}
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x000617A8 File Offset: 0x0005F9A8
			public void CreateNewFolder()
			{
				FolderBrowserDialog.FBTreeNode fbtreeNode = ((this.node_under_mouse != null) ? (this.node_under_mouse as FolderBrowserDialog.FBTreeNode) : (base.SelectedNode as FolderBrowserDialog.FBTreeNode));
				if (fbtreeNode == null || fbtreeNode.RealPath == null)
				{
					return;
				}
				string text = "New Folder";
				if (Directory.Exists(Path.Combine(fbtreeNode.RealPath, text)))
				{
					int num = 1;
					if (XplatUI.RunningOnUnix)
					{
						text = text + "-" + num;
					}
					else
					{
						text = string.Concat(new object[] { text, " (", num, ")" });
					}
					while (Directory.Exists(Path.Combine(fbtreeNode.RealPath, text)))
					{
						num++;
						if (XplatUI.RunningOnUnix)
						{
							text = "New Folder-" + num;
						}
						else
						{
							text = "New Folder (" + num + ")";
						}
					}
				}
				this.parent_real_path = fbtreeNode.RealPath;
				this.FillNode(fbtreeNode);
				this.dont_do_onbeforeexpand = true;
				fbtreeNode.Expand();
				this.dont_do_onbeforeexpand = false;
				string text2 = Path.Combine(fbtreeNode.RealPath, text);
				if (!this.vfs.CreateFolder(text2))
				{
					return;
				}
				FolderBrowserDialog.FBTreeNode fbtreeNode2 = new FolderBrowserDialog.FBTreeNode(text);
				fbtreeNode2.ImageIndex = this.NodeImageIndex(text);
				TreeNode treeNode = fbtreeNode2;
				string text3 = text2;
				fbtreeNode2.RealPath = text3;
				treeNode.Tag = text3;
				fbtreeNode.Nodes.Add(fbtreeNode2);
				base.LabelEdit = true;
				fbtreeNode2.BeginEdit();
			}

			// Token: 0x06001981 RID: 6529 RVA: 0x00061938 File Offset: 0x0005FB38
			protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
			{
				if (e.Label != null)
				{
					if (e.Label.Length <= 0)
					{
						e.CancelEdit = true;
						e.Node.BeginEdit();
						return;
					}
					FolderBrowserDialog.FBTreeNode fbtreeNode = e.Node as FolderBrowserDialog.FBTreeNode;
					string realPath = fbtreeNode.RealPath;
					string text = Path.Combine(this.parent_real_path, e.Label);
					if (!this.vfs.MoveFolder(realPath, text))
					{
						e.CancelEdit = true;
						e.Node.BeginEdit();
						return;
					}
					TreeNode treeNode = fbtreeNode;
					string text2 = text;
					fbtreeNode.RealPath = text2;
					treeNode.Tag = text2;
				}
				if (this.node_under_mouse == base.SelectedNode)
				{
					base.SelectedNode = e.Node;
				}
				base.LabelEdit = false;
			}

			// Token: 0x06001982 RID: 6530 RVA: 0x000619FC File Offset: 0x0005FBFC
			private void SetSelectedPath(string path)
			{
				base.BeginUpdate();
				FolderBrowserDialog.FBTreeNode fbtreeNode = this.FindPathInNodes(path, base.Nodes);
				if (fbtreeNode == null)
				{
					Stack stack = new Stack();
					string text = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
					if (!XplatUI.RunningOnUnix && text.Length == 2)
					{
						text += Path.DirectorySeparatorChar;
					}
					while (fbtreeNode == null && text.Length > 0)
					{
						fbtreeNode = this.FindPathInNodes(text, base.Nodes);
						if (fbtreeNode == null)
						{
							string text2 = text.Substring(0, text.LastIndexOf(Path.DirectorySeparatorChar));
							string text3 = text.Replace(text2, string.Empty);
							stack.Push(text3);
							text = text2;
						}
					}
					if (fbtreeNode == null)
					{
						base.EndUpdate();
						this.RootFolder = this.rootFolder;
						return;
					}
					this.FillNode(fbtreeNode);
					fbtreeNode.Expand();
					while (stack.Count > 0)
					{
						string text4 = stack.Pop() as string;
						foreach (object obj in fbtreeNode.Nodes)
						{
							TreeNode treeNode = (TreeNode)obj;
							FolderBrowserDialog.FBTreeNode fbtreeNode2 = treeNode as FolderBrowserDialog.FBTreeNode;
							if (text + text4 == fbtreeNode2.RealPath)
							{
								fbtreeNode = fbtreeNode2;
								text += text4;
								this.FillNode(fbtreeNode);
								fbtreeNode.Expand();
								break;
							}
						}
					}
					foreach (object obj2 in fbtreeNode.Nodes)
					{
						TreeNode treeNode2 = (TreeNode)obj2;
						FolderBrowserDialog.FBTreeNode fbtreeNode3 = treeNode2 as FolderBrowserDialog.FBTreeNode;
						if (path == fbtreeNode3.RealPath)
						{
							fbtreeNode = fbtreeNode3;
							break;
						}
					}
				}
				if (fbtreeNode != null)
				{
					base.SelectedNode = fbtreeNode;
					fbtreeNode.EnsureVisible();
				}
				base.EndUpdate();
			}

			// Token: 0x06001983 RID: 6531 RVA: 0x00061C3C File Offset: 0x0005FE3C
			private FolderBrowserDialog.FBTreeNode FindPathInNodes(string path, TreeNodeCollection nodes)
			{
				if (!XplatUI.RunningOnUnix && path.Length == 2)
				{
					path += Path.DirectorySeparatorChar;
				}
				foreach (object obj in nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					FolderBrowserDialog.FBTreeNode fbtreeNode = treeNode as FolderBrowserDialog.FBTreeNode;
					if (fbtreeNode != null && fbtreeNode.RealPath != null && fbtreeNode.RealPath == path)
					{
						return fbtreeNode;
					}
					FolderBrowserDialog.FBTreeNode fbtreeNode2 = this.FindPathInNodes(path, treeNode.Nodes);
					if (fbtreeNode2 != null)
					{
						return fbtreeNode2;
					}
				}
				return null;
			}

			// Token: 0x06001984 RID: 6532 RVA: 0x00061D1C File Offset: 0x0005FF1C
			private bool Check_if_path_is_child_of_RootFolder(string path)
			{
				string realPath = this.root_node.RealPath;
				if (realPath == null)
				{
					if (this.rootFolder != 17)
					{
						return false;
					}
				}
				try
				{
					if (!Directory.Exists(path))
					{
						return false;
					}
					Environment.SpecialFolder specialFolder = this.rootFolder;
					if (specialFolder != null)
					{
						if (specialFolder != 5)
						{
							if (specialFolder != 17)
							{
								return false;
							}
						}
						else
						{
							if (!path.StartsWith(realPath))
							{
								return false;
							}
							return true;
						}
					}
					return true;
				}
				catch
				{
				}
				return false;
			}

			// Token: 0x06001985 RID: 6533 RVA: 0x00061DD0 File Offset: 0x0005FFD0
			private void FillNode(TreeNode node)
			{
				base.BeginUpdate();
				node.Nodes.Clear();
				this.vfs.ChangeDirectory((string)node.Tag);
				ArrayList foldersOnly = this.vfs.GetFoldersOnly();
				foreach (object obj in foldersOnly)
				{
					FSEntry fsentry = (FSEntry)obj;
					if (!fsentry.Name.StartsWith("."))
					{
						FolderBrowserDialog.FBTreeNode fbtreeNode = new FolderBrowserDialog.FBTreeNode(fsentry.Name);
						fbtreeNode.Tag = fsentry.FullName;
						fbtreeNode.RealPath = ((fsentry.RealName != null) ? fsentry.RealName : fsentry.FullName);
						fbtreeNode.ImageIndex = this.NodeImageIndex(fsentry.FullName);
						this.vfs.ChangeDirectory(fsentry.FullName);
						ArrayList foldersOnly2 = this.vfs.GetFoldersOnly();
						foreach (object obj2 in foldersOnly2)
						{
							FSEntry fsentry2 = (FSEntry)obj2;
							if (!fsentry2.Name.StartsWith("."))
							{
								fbtreeNode.Nodes.Add(new TreeNode(string.Empty));
								break;
							}
						}
						node.Nodes.Add(fbtreeNode);
					}
				}
				base.EndUpdate();
			}

			// Token: 0x06001986 RID: 6534 RVA: 0x00061F90 File Offset: 0x00060190
			private void SetupImageList()
			{
				this.imageList.ColorDepth = ColorDepth.Depth32Bit;
				this.imageList.ImageSize = new Size(16, 16);
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 16));
				this.imageList.Images.Add(ThemeEngine.Current.Images(UIIcon.NormalFolder, 16));
				this.imageList.TransparentColor = Color.Transparent;
			}

			// Token: 0x06001987 RID: 6535 RVA: 0x00062080 File Offset: 0x00060280
			private int NodeImageIndex(string path)
			{
				int num = 5;
				if (path == MWFVFS.DesktopPrefix)
				{
					num = 1;
				}
				else if (path == MWFVFS.RecentlyUsedPrefix)
				{
					num = 0;
				}
				else if (path == MWFVFS.PersonalPrefix)
				{
					num = 2;
				}
				else if (path == MWFVFS.MyComputerPrefix)
				{
					num = 3;
				}
				else if (path == MWFVFS.MyNetworkPrefix)
				{
					num = 4;
				}
				return num;
			}

			// Token: 0x06001988 RID: 6536 RVA: 0x00062100 File Offset: 0x00060300
			protected override void OnAfterSelect(TreeViewEventArgs e)
			{
				if (e.Node == null)
				{
					return;
				}
				FolderBrowserDialog.FBTreeNode fbtreeNode = e.Node as FolderBrowserDialog.FBTreeNode;
				if (fbtreeNode.RealPath == null || fbtreeNode.RealPath.IndexOf("://") != -1)
				{
					this.parentDialog.okButton.Enabled = false;
					this.parentDialog.newFolderButton.Enabled = false;
					this.parentDialog.newFolderMenuItem.Enabled = false;
					this.dont_enable = true;
				}
				else
				{
					this.parentDialog.okButton.Enabled = true;
					this.parentDialog.newFolderButton.Enabled = true;
					this.parentDialog.newFolderMenuItem.Enabled = true;
					this.parentDialog.selectedPath = fbtreeNode.RealPath;
					this.dont_enable = false;
				}
				base.OnAfterSelect(e);
			}

			// Token: 0x06001989 RID: 6537 RVA: 0x000621D8 File Offset: 0x000603D8
			protected internal override void OnBeforeExpand(TreeViewCancelEventArgs e)
			{
				if (!this.dont_do_onbeforeexpand)
				{
					if (e.Node == this.root_node)
					{
						return;
					}
					this.FillNode(e.Node);
				}
				base.OnBeforeExpand(e);
			}

			// Token: 0x0600198A RID: 6538 RVA: 0x00062218 File Offset: 0x00060418
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.node_under_mouse = base.GetNodeAt(e.X, e.Y);
				base.OnMouseDown(e);
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x00062244 File Offset: 0x00060444
			protected override void OnMouseUp(MouseEventArgs e)
			{
				if (base.SelectedNode == null)
				{
					this.parentDialog.okButton.Enabled = false;
					this.parentDialog.newFolderButton.Enabled = false;
					this.parentDialog.newFolderMenuItem.Enabled = false;
				}
				else if (!this.dont_enable)
				{
					this.parentDialog.okButton.Enabled = true;
					this.parentDialog.newFolderButton.Enabled = true;
					this.parentDialog.newFolderMenuItem.Enabled = true;
				}
				this.node_under_mouse = null;
				base.OnMouseUp(e);
			}

			// Token: 0x04000E5C RID: 3676
			private MWFVFS vfs = new MWFVFS();

			// Token: 0x04000E5D RID: 3677
			private new FolderBrowserDialog.FBTreeNode root_node;

			// Token: 0x04000E5E RID: 3678
			private FolderBrowserDialog parentDialog;

			// Token: 0x04000E5F RID: 3679
			private ImageList imageList = new ImageList();

			// Token: 0x04000E60 RID: 3680
			private Environment.SpecialFolder rootFolder;

			// Token: 0x04000E61 RID: 3681
			private bool dont_enable;

			// Token: 0x04000E62 RID: 3682
			private TreeNode node_under_mouse;

			// Token: 0x04000E63 RID: 3683
			private string parent_real_path;

			// Token: 0x04000E64 RID: 3684
			private bool dont_do_onbeforeexpand;
		}

		// Token: 0x02000190 RID: 400
		internal class FBTreeNode : TreeNode
		{
			// Token: 0x0600198C RID: 6540 RVA: 0x000622E0 File Offset: 0x000604E0
			public FBTreeNode(string text)
			{
				base.Text = text;
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x0600198E RID: 6542 RVA: 0x000622FC File Offset: 0x000604FC
			// (set) Token: 0x0600198D RID: 6541 RVA: 0x000622F0 File Offset: 0x000604F0
			public string RealPath
			{
				get
				{
					return this.realPath;
				}
				set
				{
					this.realPath = value;
				}
			}

			// Token: 0x04000E65 RID: 3685
			private string realPath;
		}
	}
}
