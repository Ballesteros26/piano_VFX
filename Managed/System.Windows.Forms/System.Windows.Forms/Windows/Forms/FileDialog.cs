using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays a dialog box from which the user can select a file.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000163 RID: 355
	[DefaultProperty("FileName")]
	[DefaultEvent("FileOk")]
	public abstract class FileDialog : CommonDialog
	{
		// Token: 0x060017DC RID: 6108 RVA: 0x0005758C File Offset: 0x0005578C
		internal FileDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.vfs = new MWFVFS();
			Size empty = Size.Empty;
			Point empty2 = Point.Empty;
			object value = MWFConfig.GetValue("FileDialog", "Width");
			object value2 = MWFConfig.GetValue("FileDialog", "Height");
			if (value2 != null && value != null)
			{
				empty..ctor((int)value, (int)value2);
			}
			object value3 = MWFConfig.GetValue("FileDialog", "X");
			object value4 = MWFConfig.GetValue("FileDialog", "Y");
			if (value3 != null && value4 != null)
			{
				empty2..ctor((int)value3, (int)value4);
			}
			this.configFileNames = (string[])MWFConfig.GetValue("FileDialog", "FileNames");
			this.fileTypeComboBox = new ComboBox();
			this.backToolBarButton = new ToolBarButton();
			this.newdirToolBarButton = new ToolBarButton();
			this.searchSaveLabel = new Label();
			this.mwfFileView = new MWFFileView(this.vfs);
			this.fileNameLabel = new Label();
			this.fileNameComboBox = new ComboBox();
			this.dirComboBox = new DirComboBox(this.vfs);
			this.smallButtonToolBar = new ToolBar();
			this.menueToolBarButton = new ToolBarButton();
			this.fileTypeLabel = new Label();
			this.openSaveButton = new Button();
			this.helpButton = new Button();
			this.popupButtonPanel = new PopupButtonPanel();
			this.upToolBarButton = new ToolBarButton();
			this.cancelButton = new Button();
			this.form.CancelButton = this.cancelButton;
			this.imageListTopToolbar = new ImageList();
			this.menueToolBarButtonContextMenu = new ContextMenu();
			this.readonlyCheckBox = new CheckBox();
			this.form.SuspendLayout();
			this.imageListTopToolbar.ColorDepth = ColorDepth.Depth32Bit;
			this.imageListTopToolbar.ImageSize = new Size(16, 16);
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("go-previous.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("go-top.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("folder-new.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("preferences-system-windows.png"));
			this.imageListTopToolbar.TransparentColor = Color.Transparent;
			this.searchSaveLabel.FlatStyle = FlatStyle.System;
			this.searchSaveLabel.Location = new Point(6, 6);
			this.searchSaveLabel.Size = new Size(86, 22);
			this.searchSaveLabel.TextAlign = 64;
			this.dirComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.dirComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.dirComboBox.Location = new Point(99, 6);
			this.dirComboBox.Size = new Size(261, 22);
			this.dirComboBox.TabIndex = 7;
			this.smallButtonToolBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.smallButtonToolBar.Appearance = ToolBarAppearance.Flat;
			this.smallButtonToolBar.AutoSize = false;
			this.smallButtonToolBar.Buttons.AddRange(new ToolBarButton[] { this.backToolBarButton, this.upToolBarButton, this.newdirToolBarButton, this.menueToolBarButton });
			this.smallButtonToolBar.ButtonSize = new Size(24, 24);
			this.smallButtonToolBar.Divider = false;
			this.smallButtonToolBar.Dock = DockStyle.None;
			this.smallButtonToolBar.DropDownArrows = true;
			this.smallButtonToolBar.ImageList = this.imageListTopToolbar;
			this.smallButtonToolBar.Location = new Point(372, 6);
			this.smallButtonToolBar.ShowToolTips = true;
			this.smallButtonToolBar.Size = new Size(140, 28);
			this.smallButtonToolBar.TabIndex = 8;
			this.smallButtonToolBar.TextAlign = ToolBarTextAlign.Right;
			this.popupButtonPanel.Dock = DockStyle.None;
			this.popupButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			this.popupButtonPanel.Location = new Point(6, 35);
			this.popupButtonPanel.Size = new Size(87, 338);
			this.popupButtonPanel.TabIndex = 9;
			this.mwfFileView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.mwfFileView.Location = new Point(99, 35);
			this.mwfFileView.Size = new Size(450, 283);
			this.mwfFileView.MultiSelect = false;
			this.mwfFileView.TabIndex = 10;
			this.mwfFileView.RegisterSender(this.dirComboBox);
			this.mwfFileView.RegisterSender(this.popupButtonPanel);
			this.fileNameLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.fileNameLabel.FlatStyle = FlatStyle.System;
			this.fileNameLabel.Location = new Point(101, 326);
			this.fileNameLabel.Size = new Size(70, 21);
			this.fileNameLabel.Text = "File name:";
			this.fileNameLabel.TextAlign = 16;
			this.fileNameComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.fileNameComboBox.Location = new Point(195, 326);
			this.fileNameComboBox.Size = new Size(246, 22);
			this.fileNameComboBox.TabIndex = 1;
			this.fileNameComboBox.MaxDropDownItems = FileDialog.MaxFileNameItems;
			this.fileNameComboBox.RestoreContextMenu();
			this.UpdateRecentFiles();
			this.fileTypeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.fileTypeLabel.FlatStyle = FlatStyle.System;
			this.fileTypeLabel.Location = new Point(101, 355);
			this.fileTypeLabel.Size = new Size(90, 21);
			this.fileTypeLabel.Text = "Files of type:";
			this.fileTypeLabel.TextAlign = 16;
			this.fileTypeComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.fileTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.fileTypeComboBox.Location = new Point(195, 355);
			this.fileTypeComboBox.Size = new Size(246, 22);
			this.fileTypeComboBox.TabIndex = 2;
			this.backToolBarButton.ImageIndex = 0;
			this.backToolBarButton.Enabled = false;
			this.backToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.mwfFileView.AddControlToEnableDisableByDirStack(this.backToolBarButton);
			this.upToolBarButton.ImageIndex = 1;
			this.upToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.mwfFileView.SetFolderUpToolBarButton(this.upToolBarButton);
			this.newdirToolBarButton.ImageIndex = 2;
			this.newdirToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.menueToolBarButton.ImageIndex = 3;
			this.menueToolBarButton.DropDownMenu = this.menueToolBarButtonContextMenu;
			this.menueToolBarButton.Style = ToolBarButtonStyle.DropDownButton;
			this.menueToolBarButtonContextMenu.MenuItems.AddRange(this.mwfFileView.ViewMenuItems);
			this.openSaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.openSaveButton.FlatStyle = FlatStyle.System;
			this.openSaveButton.Location = new Point(474, 326);
			this.openSaveButton.Size = new Size(75, 23);
			this.openSaveButton.TabIndex = 4;
			this.openSaveButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(474, 353);
			this.cancelButton.Size = new Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.helpButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Location = new Point(474, 353);
			this.helpButton.Size = new Size(75, 23);
			this.helpButton.TabIndex = 6;
			this.helpButton.Text = "Help";
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Visible = false;
			this.readonlyCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.readonlyCheckBox.Text = "Open Readonly";
			this.readonlyCheckBox.Location = new Point(195, 350);
			this.readonlyCheckBox.Size = new Size(245, 21);
			this.readonlyCheckBox.TabIndex = 3;
			this.readonlyCheckBox.FlatStyle = FlatStyle.System;
			this.readonlyCheckBox.Visible = false;
			this.form.SizeGripStyle = SizeGripStyle.Show;
			this.form.AcceptButton = this.openSaveButton;
			this.form.MaximizeBox = true;
			this.form.MinimizeBox = true;
			this.form.FormBorderStyle = FormBorderStyle.Sizable;
			this.form.ClientSize = new Size(555, 385);
			this.form.MinimumSize = this.form.Size;
			this.form.Controls.Add(this.smallButtonToolBar);
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.openSaveButton);
			this.form.Controls.Add(this.mwfFileView);
			this.form.Controls.Add(this.fileTypeLabel);
			this.form.Controls.Add(this.fileNameLabel);
			this.form.Controls.Add(this.fileTypeComboBox);
			this.form.Controls.Add(this.fileNameComboBox);
			this.form.Controls.Add(this.dirComboBox);
			this.form.Controls.Add(this.searchSaveLabel);
			this.form.Controls.Add(this.popupButtonPanel);
			this.form.Controls.Add(this.helpButton);
			this.form.Controls.Add(this.readonlyCheckBox);
			this.form.ResumeLayout(true);
			if (empty != Size.Empty)
			{
				this.form.ClientSize = empty;
			}
			if (empty2 != Point.Empty)
			{
				this.form.Location = empty2;
			}
			this.openSaveButton.Click += new EventHandler(this.OnClickOpenSaveButton);
			this.cancelButton.Click += new EventHandler(this.OnClickCancelButton);
			this.helpButton.Click += new EventHandler(this.OnClickHelpButton);
			this.smallButtonToolBar.ButtonClick += this.OnClickSmallButtonToolBar;
			this.fileTypeComboBox.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChangedFileTypeComboBox);
			this.mwfFileView.SelectedFileChanged += new EventHandler(this.OnSelectedFileChangedFileView);
			this.mwfFileView.ForceDialogEnd += new EventHandler(this.OnForceDialogEndFileView);
			this.mwfFileView.SelectedFilesChanged += new EventHandler(this.OnSelectedFilesChangedFileView);
			this.mwfFileView.ColumnClick += this.OnColumnClickFileView;
			this.dirComboBox.DirectoryChanged += new EventHandler(this.OnDirectoryChangedDirComboBox);
			this.popupButtonPanel.DirectoryChanged += new EventHandler(this.OnDirectoryChangedPopupButtonPanel);
			this.readonlyCheckBox.CheckedChanged += new EventHandler(this.OnCheckCheckChanged);
			this.form.FormClosed += this.OnFileDialogFormClosed;
			this.custom_places = new FileDialogCustomPlacesCollection();
		}

		/// <summary>Occurs when the user clicks on the Open or Save button on a file dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400018E RID: 398
		// (add) Token: 0x060017DE RID: 6110 RVA: 0x000581C0 File Offset: 0x000563C0
		// (remove) Token: 0x060017DF RID: 6111 RVA: 0x000581D4 File Offset: 0x000563D4
		public event CancelEventHandler FileOk
		{
			add
			{
				base.Events.AddHandler(FileDialog.EventFileOk, value);
			}
			remove
			{
				base.Events.RemoveHandler(FileDialog.EventFileOk, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.</summary>
		/// <returns>true if the dialog box adds an extension to a file name if the user omits the extension; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000581E8 File Offset: 0x000563E8
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x000581F0 File Offset: 0x000563F0
		[DefaultValue(true)]
		public bool AddExtension
		{
			get
			{
				return this.addExtension;
			}
			set
			{
				this.addExtension = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether this <see cref="T:System.Windows.Forms.FileDialog" /> instance should automatically upgrade appearance and behavior when running on Windows Vista.</summary>
		/// <returns>true if this <see cref="T:System.Windows.Forms.FileDialog" /> instance should automatically upgrade appearance and behavior when running on Windows Vista; otherwise, false. The default is true.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000581FC File Offset: 0x000563FC
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x00058204 File Offset: 0x00056404
		[MonoTODO("Stub, value not respected")]
		[DefaultValue(true)]
		public bool AutoUpgradeEnabled
		{
			get
			{
				return this.auto_upgrade_enable;
			}
			set
			{
				this.auto_upgrade_enable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.</summary>
		/// <returns>true if the dialog box displays a warning if the user specifies a file name that does not exist; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00058210 File Offset: 0x00056410
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00058218 File Offset: 0x00056418
		[DefaultValue(false)]
		public virtual bool CheckFileExists
		{
			get
			{
				return this.checkFileExists;
			}
			set
			{
				this.checkFileExists = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.</summary>
		/// <returns>true if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00058224 File Offset: 0x00056424
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x0005822C File Offset: 0x0005642C
		[DefaultValue(true)]
		public bool CheckPathExists
		{
			get
			{
				return this.checkPathExists;
			}
			set
			{
				this.checkPathExists = value;
			}
		}

		/// <summary>Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</summary>
		/// <returns>The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00058238 File Offset: 0x00056438
		[MonoTODO("Stub, collection not used")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public FileDialogCustomPlacesCollection CustomPlaces
		{
			get
			{
				return this.custom_places;
			}
		}

		/// <summary>Gets or sets the default file name extension.</summary>
		/// <returns>The default file name extension. The returned string does not include the period. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00058240 File Offset: 0x00056440
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0005825C File Offset: 0x0005645C
		[DefaultValue("")]
		public string DefaultExt
		{
			get
			{
				if (this.defaultExt == null)
				{
					return string.Empty;
				}
				return this.defaultExt;
			}
			set
			{
				if (value != null && value.Length > 0 && value.get_Chars(0) == '.')
				{
					value = value.Substring(1);
				}
				this.defaultExt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).</summary>
		/// <returns>true if the dialog box returns the location of the file referenced by the shortcut; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0005829C File Offset: 0x0005649C
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x000582A4 File Offset: 0x000564A4
		[DefaultValue(true)]
		public bool DereferenceLinks
		{
			get
			{
				return this.dereferenceLinks;
			}
			set
			{
				this.dereferenceLinks = value;
			}
		}

		/// <summary>Gets or sets a string containing the file name selected in the file dialog box.</summary>
		/// <returns>The file name selected in the file dialog box. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x000582B0 File Offset: 0x000564B0
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x00058320 File Offset: 0x00056520
		[DefaultValue("")]
		public string FileName
		{
			get
			{
				if (this.fileNames == null || this.fileNames.Length == 0)
				{
					return string.Empty;
				}
				if (this.fileNames[0].Length == 0)
				{
					return string.Empty;
				}
				if (!this.checkForIllegalChars)
				{
					return this.fileNames[0];
				}
				Path.GetFullPath(this.fileNames[0]);
				return this.fileNames[0];
			}
			set
			{
				if (value != null)
				{
					this.fileNames = new string[] { value };
				}
				else
				{
					this.fileNames = new string[0];
				}
				this.checkForIllegalChars = false;
			}
		}

		/// <summary>Gets the file names of all selected files in the dialog box.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing the file names of all selected files in the dialog box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0005835C File Offset: 0x0005655C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string[] FileNames
		{
			get
			{
				if (this.fileNames == null || this.fileNames.Length == 0)
				{
					return new string[0];
				}
				string[] array = new string[this.fileNames.Length];
				this.fileNames.CopyTo(array, 0);
				if (!this.checkForIllegalChars)
				{
					return array;
				}
				foreach (string text in array)
				{
					Path.GetFullPath(text);
				}
				return array;
			}
		}

		/// <summary>Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.</summary>
		/// <returns>The file filtering options available in the dialog box.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Filter" /> format is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000583D4 File Offset: 0x000565D4
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000583DC File Offset: 0x000565DC
		[DefaultValue("")]
		[Localizable(true)]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null)
				{
					this.filter = string.Empty;
					if (this.fileFilter != null)
					{
						this.fileFilter.FilterArrayList.Clear();
					}
				}
				else
				{
					if (!FileFilter.CheckFilter(value))
					{
						throw new ArgumentException("The provided filter string is invalid. The filter string should contain a description of the filter, followed by the  vertical bar (|) and the filter pattern. The strings for different filtering options should also be separated by the vertical bar. Example: Text files (*.txt)|*.txt|All files (*.*)|*.*");
					}
					this.filter = value;
					this.fileFilter = new FileFilter(this.filter);
				}
				this.UpdateFilters();
			}
		}

		/// <summary>Gets or sets the index of the filter currently selected in the file dialog box.</summary>
		/// <returns>A value containing the index of the filter currently selected in the file dialog box. The default value is 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00058454 File Offset: 0x00056654
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0005845C File Offset: 0x0005665C
		[DefaultValue(1)]
		public int FilterIndex
		{
			get
			{
				return this.filterIndex;
			}
			set
			{
				this.filterIndex = value;
			}
		}

		/// <summary>Gets or sets the initial directory displayed by the file dialog box.</summary>
		/// <returns>The initial directory displayed by the file dialog box. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00058468 File Offset: 0x00056668
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x00058484 File Offset: 0x00056684
		[DefaultValue("")]
		public string InitialDirectory
		{
			get
			{
				if (this.initialDirectory == null)
				{
					return string.Empty;
				}
				return this.initialDirectory;
			}
			set
			{
				this.initialDirectory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box restores the current directory before closing.</summary>
		/// <returns>true if the dialog box restores the current directory to its original value if the user changed the directory while searching for files; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00058490 File Offset: 0x00056690
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x00058498 File Offset: 0x00056698
		[DefaultValue(false)]
		public bool RestoreDirectory
		{
			get
			{
				return this.restoreDirectory;
			}
			set
			{
				this.restoreDirectory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help button is displayed in the file dialog box.</summary>
		/// <returns>true if the dialog box includes a help button; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000584A4 File Offset: 0x000566A4
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x000584AC File Offset: 0x000566AC
		[DefaultValue(false)]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				this.showHelp = value;
				this.ResizeAndRelocateForHelpOrReadOnly();
			}
		}

		/// <summary>Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.</summary>
		/// <returns>true if the dialog box supports multiple file name extensions; otherwise, false. The default is false. </returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x000584BC File Offset: 0x000566BC
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x000584C4 File Offset: 0x000566C4
		[DefaultValue(false)]
		public bool SupportMultiDottedExtensions
		{
			get
			{
				return this.supportMultiDottedExtensions;
			}
			set
			{
				this.supportMultiDottedExtensions = value;
			}
		}

		/// <summary>Gets or sets the file dialog box title.</summary>
		/// <returns>The file dialog box title. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x000584D0 File Offset: 0x000566D0
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x000584EC File Offset: 0x000566EC
		[DefaultValue("")]
		[Localizable(true)]
		public string Title
		{
			get
			{
				if (this.title == null)
				{
					return string.Empty;
				}
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.</summary>
		/// <returns>true if the dialog box accepts only valid Win32 file names; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x000584F8 File Offset: 0x000566F8
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00058500 File Offset: 0x00056700
		[DefaultValue(true)]
		public bool ValidateNames
		{
			get
			{
				return this.validateNames;
			}
			set
			{
				this.validateNames = value;
			}
		}

		/// <summary>Resets all properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001800 RID: 6144 RVA: 0x0005850C File Offset: 0x0005670C
		public override void Reset()
		{
			this.addExtension = true;
			this.checkFileExists = false;
			this.checkPathExists = true;
			this.DefaultExt = null;
			this.dereferenceLinks = true;
			this.FileName = null;
			this.Filter = string.Empty;
			this.FilterIndex = 1;
			this.InitialDirectory = null;
			this.restoreDirectory = false;
			this.SupportMultiDottedExtensions = false;
			this.ShowHelp = false;
			this.Title = null;
			this.validateNames = true;
			this.UpdateFilters();
		}

		/// <summary>Provides a string version of this object.</summary>
		/// <returns>A string version of this object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001801 RID: 6145 RVA: 0x00058588 File Offset: 0x00056788
		public override string ToString()
		{
			return string.Format("{0}: Title: {1}, FileName: {2}", base.ToString(), this.Title, this.FileName);
		}

		/// <summary>Gets the Win32 instance handle for the application.</summary>
		/// <returns>A Win32 instance handle for the application.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x000585B4 File Offset: 0x000567B4
		protected virtual IntPtr Instance
		{
			get
			{
				if (this.form == null)
				{
					return IntPtr.Zero;
				}
				return this.form.Handle;
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.FileDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FileDialog" />.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000585D4 File Offset: 0x000567D4
		protected int Options
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x000585D8 File Offset: 0x000567D8
		internal virtual string DialogTitle
		{
			get
			{
				return this.Title;
			}
		}

		/// <summary>Defines the common dialog box hook procedure that is overridden to add specific functionality to the file dialog box.</summary>
		/// <returns>Returns zero if the default dialog box procedure processes the message; returns a nonzero value if the default dialog box procedure ignores the message.</returns>
		/// <param name="hWnd">The handle to the dialog box window. </param>
		/// <param name="msg">The message received by the dialog box. </param>
		/// <param name="wparam">Additional information about the message. </param>
		/// <param name="lparam">Additional information about the message. </param>
		// Token: 0x06001805 RID: 6149 RVA: 0x000585E0 File Offset: 0x000567E0
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06001806 RID: 6150 RVA: 0x000585E8 File Offset: 0x000567E8
		protected void OnFileOk(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[FileDialog.EventFileOk];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0005861C File Offset: 0x0005681C
		private void CleanupOnClose()
		{
			this.WriteConfigValues();
			Mime.CleanFileCache();
			this.disable_form_closed_event = true;
		}

		/// <summary>Specifies a common dialog box.</summary>
		/// <returns>true if the file could be opened; otherwise, false.</returns>
		/// <param name="hWndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
		// Token: 0x06001808 RID: 6152 RVA: 0x00058630 File Offset: 0x00056830
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			this.ReadConfigValues();
			this.form.Text = this.DialogTitle;
			string text;
			if (this.fileNames != null && this.fileNames.Length != 0)
			{
				text = this.fileNames[0];
			}
			else
			{
				text = string.Empty;
			}
			this.SelectFilter();
			this.form.Refresh();
			this.SetFileAndDirectory(text);
			this.fileNameComboBox.Select();
			return true;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000586B8 File Offset: 0x000568B8
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x000586A8 File Offset: 0x000568A8
		internal virtual bool ShowReadOnly
		{
			get
			{
				return this.showReadOnly;
			}
			set
			{
				this.showReadOnly = value;
				this.ResizeAndRelocateForHelpOrReadOnly();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000586D8 File Offset: 0x000568D8
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000586C0 File Offset: 0x000568C0
		internal virtual bool ReadOnlyChecked
		{
			get
			{
				return this.readOnlyChecked;
			}
			set
			{
				this.readOnlyChecked = value;
				this.readonlyCheckBox.Checked = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000586F8 File Offset: 0x000568F8
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x000586E0 File Offset: 0x000568E0
		internal bool BMultiSelect
		{
			get
			{
				return this.multiSelect;
			}
			set
			{
				this.multiSelect = value;
				this.mwfFileView.MultiSelect = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x00058700 File Offset: 0x00056900
		internal string OpenSaveButtonText
		{
			set
			{
				this.openSaveButton.Text = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00058710 File Offset: 0x00056910
		internal string SearchSaveLabel
		{
			set
			{
				this.searchSaveLabel.Text = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x00058720 File Offset: 0x00056920
		internal string FileTypeLabel
		{
			set
			{
				this.fileTypeLabel.Text = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00058730 File Offset: 0x00056930
		internal string CustomFilter
		{
			get
			{
				string text = this.fileNameComboBox.Text;
				if (text.IndexOfAny(this.wildcard_chars) == -1)
				{
					return null;
				}
				return text;
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00058760 File Offset: 0x00056960
		private void SelectFilter()
		{
			int num = this.filterIndex - 1;
			if (this.mwfFileView.FilterArrayList == null || this.mwfFileView.FilterArrayList.Count == 0)
			{
				num = -1;
			}
			else if (num < 0 || num >= this.mwfFileView.FilterArrayList.Count)
			{
				num = 0;
			}
			this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = true;
			this.fileTypeComboBox.BeginUpdate();
			this.fileTypeComboBox.SelectedIndex = num;
			this.fileTypeComboBox.EndUpdate();
			this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = false;
			this.mwfFileView.FilterIndex = num + 1;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00058800 File Offset: 0x00056A00
		private void SetFileAndDirectory(string fname)
		{
			if (fname.Length != 0)
			{
				if (!Path.IsPathRooted(fname))
				{
					this.mwfFileView.ChangeDirectory(null, this.lastFolder);
					this.fileNameComboBox.Text = fname;
				}
				else
				{
					string directoryName = Path.GetDirectoryName(fname);
					if (directoryName != null && directoryName.Length > 0 && Directory.Exists(directoryName))
					{
						this.fileNameComboBox.Text = Path.GetFileName(fname);
						this.mwfFileView.ChangeDirectory(null, directoryName);
					}
					else
					{
						this.fileNameComboBox.Text = fname;
						this.mwfFileView.ChangeDirectory(null, this.lastFolder);
					}
				}
			}
			else
			{
				this.mwfFileView.ChangeDirectory(null, this.lastFolder);
				this.fileNameComboBox.Text = null;
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000588D0 File Offset: 0x00056AD0
		private void OnClickOpenSaveButton(object sender, EventArgs e)
		{
			this.checkForIllegalChars = true;
			if (this.fileDialogType == FileDialog.FileDialogType.OpenFileDialog)
			{
				ListView.SelectedListViewItemCollection selectedItems = this.mwfFileView.SelectedItems;
				if (selectedItems.Count > 0 && selectedItems[0] != null)
				{
					if (selectedItems.Count == 1)
					{
						FileViewListViewItem fileViewListViewItem = selectedItems[0] as FileViewListViewItem;
						FSEntry fsentry = fileViewListViewItem.FSEntry;
						if ((fsentry.Attributes & 16) == 16)
						{
							this.mwfFileView.ChangeDirectory(null, fsentry.FullName, this.CustomFilter);
							return;
						}
					}
					else
					{
						foreach (object obj in selectedItems)
						{
							FileViewListViewItem fileViewListViewItem2 = (FileViewListViewItem)obj;
							FSEntry fsentry2 = fileViewListViewItem2.FSEntry;
							if ((fsentry2.Attributes & 16) == 16)
							{
								this.mwfFileView.ChangeDirectory(null, fsentry2.FullName, this.CustomFilter);
								return;
							}
						}
					}
				}
			}
			if (this.fileNameComboBox.Text.IndexOfAny(this.wildcard_chars) != -1)
			{
				this.mwfFileView.UpdateFileView(this.fileNameComboBox.Text);
				return;
			}
			ArrayList arrayList = new ArrayList();
			FileDialog.FileNamesTokenizer fileNamesTokenizer = new FileDialog.FileNamesTokenizer(this.fileNameComboBox.Text, this.multiSelect);
			fileNamesTokenizer.GetNextFile();
			while (fileNamesTokenizer.CurrentToken != FileDialog.TokenType.EOF)
			{
				string text = fileNamesTokenizer.TokenText;
				if (!Path.IsPathRooted(text))
				{
					if (this.mwfFileView.CurrentRealFolder != null)
					{
						text = Path.Combine(this.mwfFileView.CurrentRealFolder, text);
					}
					else if (this.mwfFileView.CurrentFSEntry != null)
					{
						text = this.mwfFileView.CurrentFSEntry.FullName;
					}
				}
				FileInfo fileInfo = new FileInfo(text);
				string text2;
				if (fileInfo.Exists || this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog)
				{
					text2 = text;
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(text);
					if (directoryInfo.Exists)
					{
						this.mwfFileView.ChangeDirectory(null, directoryInfo.FullName, this.CustomFilter);
						this.fileNameComboBox.Text = null;
						return;
					}
					text2 = text;
				}
				if (this.addExtension)
				{
					string extension = Path.GetExtension(text);
					if (extension.Length == 0)
					{
						string text3 = string.Empty;
						if (this.AddFilterExtension(text2))
						{
							text3 = this.GetExtension(text2);
						}
						if (text3.Length == 0 && this.DefaultExt.Length > 0)
						{
							text3 = "." + this.DefaultExt;
							if (this.checkFileExists && !File.Exists(text2 + text3))
							{
								text3 = string.Empty;
							}
						}
						text2 += text3;
					}
				}
				if (this.checkFileExists && !File.Exists(text2))
				{
					string text4 = "\"" + text2 + "\" does not exist. Please verify that you have entered the correct file name.";
					MessageBox.Show(text4, this.openSaveButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog)
				{
					if (this.overwritePrompt && File.Exists(text2))
					{
						string text5 = "\"" + text2 + "\" already exists. Do you want to overwrite it?";
						DialogResult dialogResult = MessageBox.Show(text5, this.openSaveButton.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
						if (dialogResult == DialogResult.Cancel)
						{
							return;
						}
					}
					if (this.createPrompt && !File.Exists(text2))
					{
						string text6 = "\"" + text2 + "\" does not exist. Do you want to create it?";
						DialogResult dialogResult2 = MessageBox.Show(text6, this.openSaveButton.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
						if (dialogResult2 == DialogResult.Cancel)
						{
							return;
						}
					}
				}
				arrayList.Add(text2);
				fileNamesTokenizer.GetNextFile();
			}
			if (arrayList.Count <= 0)
			{
				foreach (object obj2 in this.mwfFileView.SelectedItems)
				{
					FileViewListViewItem fileViewListViewItem3 = (FileViewListViewItem)obj2;
					FSEntry fsentry3 = fileViewListViewItem3.FSEntry;
					if ((fsentry3.Attributes & 16) == 16)
					{
						this.mwfFileView.ChangeDirectory(null, fsentry3.FullName, this.CustomFilter);
						return;
					}
				}
				return;
			}
			this.fileNames = new string[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				string text7 = (string)arrayList[i];
				this.fileNames[i] = text7;
				this.mwfFileView.WriteRecentlyUsed(text7);
				if (File.Exists(text7))
				{
					if (this.fileNameComboBox.Items.IndexOf(text7) == -1)
					{
						this.fileNameComboBox.Items.Insert(0, text7);
					}
				}
			}
			while (this.fileNameComboBox.Items.Count > FileDialog.MaxFileNameItems)
			{
				this.fileNameComboBox.Items.RemoveAt(FileDialog.MaxFileNameItems);
			}
			if (this.checkPathExists && this.mwfFileView.CurrentRealFolder != null && !Directory.Exists(this.mwfFileView.CurrentRealFolder))
			{
				string text8 = "\"" + this.mwfFileView.CurrentRealFolder + "\" does not exist. Please verify that you have entered the correct directory name.";
				MessageBox.Show(text8, this.openSaveButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (this.InitialDirectory.Length == 0 || !Directory.Exists(this.InitialDirectory))
				{
					this.mwfFileView.ChangeDirectory(null, this.lastFolder, this.CustomFilter);
				}
				else
				{
					this.mwfFileView.ChangeDirectory(null, this.InitialDirectory, this.CustomFilter);
				}
				return;
			}
			if (this.restoreDirectory)
			{
				this.lastFolder = this.restoreDirectoryString;
			}
			else
			{
				this.lastFolder = this.mwfFileView.CurrentFolder;
			}
			this.filterIndex = this.fileTypeComboBox.SelectedIndex + 1;
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnFileOk(cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return;
			}
			this.CleanupOnClose();
			this.form.DialogResult = DialogResult.OK;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00058F54 File Offset: 0x00057154
		private bool AddFilterExtension(string fileName)
		{
			if (this.fileDialogType != FileDialog.FileDialogType.OpenFileDialog)
			{
				return true;
			}
			if (this.DefaultExt.Length == 0)
			{
				return true;
			}
			if (this.checkFileExists)
			{
				string text = fileName + "." + this.DefaultExt;
				return !File.Exists(text);
			}
			return !File.Exists(fileName);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00058FB0 File Offset: 0x000571B0
		private string GetExtension(string fileName)
		{
			string text = string.Empty;
			if (this.fileFilter == null || this.fileTypeComboBox.SelectedIndex == -1)
			{
				return text;
			}
			FilterStruct filterStruct = (FilterStruct)this.fileFilter.FilterArrayList[this.fileTypeComboBox.SelectedIndex];
			for (int i = 0; i < filterStruct.filters.Count; i++)
			{
				string text2 = filterStruct.filters[i];
				if (text2.StartsWith("*"))
				{
					text2 = text2.Remove(0, 1);
				}
				if (text2.IndexOf('*') == -1)
				{
					if (!this.supportMultiDottedExtensions)
					{
						int num = text2.LastIndexOf('.');
						if (num > 0 && text2.LastIndexOf('.', num - 1) != -1)
						{
							text2 = text2.Remove(0, num);
						}
					}
					if (!this.checkFileExists)
					{
						text = text2;
						break;
					}
					if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog && this.DefaultExt.Length > 0)
					{
						text = text2;
						break;
					}
					string text3 = fileName + text2;
					if (File.Exists(text3))
					{
						text = text2;
						break;
					}
					if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog && this.DefaultExt.Length > 0)
					{
						text = text2;
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0005910C File Offset: 0x0005730C
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			if (this.restoreDirectory)
			{
				this.mwfFileView.CurrentFolder = this.restoreDirectoryString;
			}
			this.CleanupOnClose();
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00059148 File Offset: 0x00057348
		private void OnClickHelpButton(object sender, EventArgs e)
		{
			this.OnHelpRequest(e);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00059154 File Offset: 0x00057354
		private void OnClickSmallButtonToolBar(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == this.upToolBarButton)
			{
				this.mwfFileView.OneDirUp(this.CustomFilter);
			}
			else if (e.Button == this.backToolBarButton)
			{
				this.mwfFileView.PopDir(this.CustomFilter);
			}
			else if (e.Button == this.newdirToolBarButton)
			{
				this.mwfFileView.CreateNewFolder();
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000591CC File Offset: 0x000573CC
		private void OnSelectedIndexChangedFileTypeComboBox(object sender, EventArgs e)
		{
			if (this.do_not_call_OnSelectedIndexChangedFileTypeComboBox)
			{
				this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = false;
				return;
			}
			this.UpdateRecentFiles();
			this.mwfFileView.FilterIndex = this.fileTypeComboBox.SelectedIndex + 1;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00059200 File Offset: 0x00057400
		private void OnSelectedFileChangedFileView(object sender, EventArgs e)
		{
			this.fileNameComboBox.Text = this.mwfFileView.CurrentFSEntry.Name;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00059220 File Offset: 0x00057420
		private void OnSelectedFilesChangedFileView(object sender, EventArgs e)
		{
			string selectedFilesString = this.mwfFileView.SelectedFilesString;
			if (selectedFilesString != null && selectedFilesString.Length != 0)
			{
				this.fileNameComboBox.Text = selectedFilesString;
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00059258 File Offset: 0x00057458
		private void OnForceDialogEndFileView(object sender, EventArgs e)
		{
			this.OnClickOpenSaveButton(this, EventArgs.Empty);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00059268 File Offset: 0x00057468
		private void OnDirectoryChangedDirComboBox(object sender, EventArgs e)
		{
			this.mwfFileView.ChangeDirectory(sender, this.dirComboBox.CurrentFolder, this.CustomFilter);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00059294 File Offset: 0x00057494
		private void OnDirectoryChangedPopupButtonPanel(object sender, EventArgs e)
		{
			this.mwfFileView.ChangeDirectory(sender, this.popupButtonPanel.CurrentFolder, this.CustomFilter);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000592C0 File Offset: 0x000574C0
		private void OnCheckCheckChanged(object sender, EventArgs e)
		{
			this.ReadOnlyChecked = this.readonlyCheckBox.Checked;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000592D4 File Offset: 0x000574D4
		private void OnFileDialogFormClosed(object sender, FormClosedEventArgs e)
		{
			this.HandleFormClosedEvent(sender);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000592E0 File Offset: 0x000574E0
		private void OnColumnClickFileView(object sender, ColumnClickEventArgs e)
		{
			if (this.file_view_comparer == null)
			{
				this.file_view_comparer = new MwfFileViewItemComparer(true);
			}
			this.file_view_comparer.ColumnIndex = e.Column;
			this.file_view_comparer.Ascendent = !this.file_view_comparer.Ascendent;
			if (this.mwfFileView.ListViewItemSorter == null)
			{
				this.mwfFileView.ListViewItemSorter = this.file_view_comparer;
			}
			else
			{
				this.mwfFileView.Sort();
			}
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00059360 File Offset: 0x00057560
		private void HandleFormClosedEvent(object sender)
		{
			if (!this.disable_form_closed_event)
			{
				this.OnClickCancelButton(sender, EventArgs.Empty);
			}
			this.disable_form_closed_event = false;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00059380 File Offset: 0x00057580
		private void UpdateFilters()
		{
			if (this.fileFilter == null)
			{
				this.fileFilter = new FileFilter();
			}
			ArrayList filterArrayList = this.fileFilter.FilterArrayList;
			this.fileTypeComboBox.BeginUpdate();
			this.fileTypeComboBox.Items.Clear();
			foreach (object obj in filterArrayList)
			{
				FilterStruct filterStruct = (FilterStruct)obj;
				this.fileTypeComboBox.Items.Add(filterStruct.filterName);
			}
			this.fileTypeComboBox.EndUpdate();
			this.mwfFileView.FilterArrayList = filterArrayList;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00059450 File Offset: 0x00057650
		private void UpdateRecentFiles()
		{
			this.fileNameComboBox.Items.Clear();
			if (this.configFileNames != null)
			{
				foreach (string text in this.configFileNames)
				{
					if (text != null && text.Trim().Length != 0)
					{
						if (this.fileNameComboBox.Items.Count >= FileDialog.MaxFileNameItems)
						{
							break;
						}
						this.fileNameComboBox.Items.Add(text);
					}
				}
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000594E4 File Offset: 0x000576E4
		private void ResizeAndRelocateForHelpOrReadOnly()
		{
			this.form.SuspendLayout();
			int num = this.form.Size.Width - this.form.MinimumSize.Width;
			int num2 = this.form.Size.Height - this.form.MinimumSize.Height;
			if (!this.ShowHelp && !this.ShowReadOnly)
			{
				num2 += 29;
			}
			this.mwfFileView.Size = new Size(450 + num, 254 + num2);
			this.fileNameLabel.Location = new Point(101, 298 + num2);
			this.fileNameComboBox.Location = new Point(195, 298 + num2);
			this.fileTypeLabel.Location = new Point(101, 326 + num2);
			this.fileTypeComboBox.Location = new Point(195, 326 + num2);
			this.openSaveButton.Location = new Point(474 + num, 298 + num2);
			this.cancelButton.Location = new Point(474 + num, 324 + num2);
			this.helpButton.Location = new Point(474 + num, 353 + num2);
			this.readonlyCheckBox.Location = new Point(195, 350 + num2);
			this.helpButton.Visible = this.ShowHelp;
			this.readonlyCheckBox.Visible = this.ShowReadOnly;
			this.form.ResumeLayout();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00059694 File Offset: 0x00057894
		private void WriteConfigValues()
		{
			MWFConfig.SetValue("FileDialog", "Width", this.form.ClientSize.Width);
			MWFConfig.SetValue("FileDialog", "Height", this.form.ClientSize.Height);
			MWFConfig.SetValue("FileDialog", "X", this.form.Location.X);
			MWFConfig.SetValue("FileDialog", "Y", this.form.Location.Y);
			MWFConfig.SetValue("FileDialog", "LastFolder", this.lastFolder);
			string[] array = new string[this.fileNameComboBox.Items.Count];
			this.fileNameComboBox.Items.CopyTo(array, 0);
			MWFConfig.SetValue("FileDialog", "FileNames", array);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0005978C File Offset: 0x0005798C
		private void ReadConfigValues()
		{
			this.lastFolder = (string)MWFConfig.GetValue("FileDialog", "LastFolder");
			if (this.lastFolder != null && this.lastFolder.IndexOf("://") == -1 && !Directory.Exists(this.lastFolder))
			{
				this.lastFolder = MWFVFS.DesktopPrefix;
			}
			if (this.InitialDirectory.Length > 0 && Directory.Exists(this.InitialDirectory))
			{
				this.lastFolder = this.InitialDirectory;
			}
			else if (this.lastFolder == null || this.lastFolder.Length == 0)
			{
				this.lastFolder = Environment.CurrentDirectory;
			}
			if (this.RestoreDirectory)
			{
				this.restoreDirectoryString = this.lastFolder;
			}
		}

		// Token: 0x04000D2B RID: 3371
		private const string filedialog_string = "FileDialog";

		// Token: 0x04000D2C RID: 3372
		private const string lastfolder_string = "LastFolder";

		// Token: 0x04000D2D RID: 3373
		private const string width_string = "Width";

		// Token: 0x04000D2E RID: 3374
		private const string height_string = "Height";

		// Token: 0x04000D2F RID: 3375
		private const string filenames_string = "FileNames";

		// Token: 0x04000D30 RID: 3376
		private const string x_string = "X";

		// Token: 0x04000D31 RID: 3377
		private const string y_string = "Y";

		/// <summary>Owns the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		// Token: 0x04000D32 RID: 3378
		protected static readonly object EventFileOk = new object();

		// Token: 0x04000D33 RID: 3379
		private static int MaxFileNameItems = 10;

		// Token: 0x04000D34 RID: 3380
		private bool addExtension = true;

		// Token: 0x04000D35 RID: 3381
		private bool checkFileExists;

		// Token: 0x04000D36 RID: 3382
		private bool checkPathExists = true;

		// Token: 0x04000D37 RID: 3383
		private string defaultExt;

		// Token: 0x04000D38 RID: 3384
		private bool dereferenceLinks = true;

		// Token: 0x04000D39 RID: 3385
		private string[] fileNames;

		// Token: 0x04000D3A RID: 3386
		private string filter = string.Empty;

		// Token: 0x04000D3B RID: 3387
		private int filterIndex = 1;

		// Token: 0x04000D3C RID: 3388
		private string initialDirectory;

		// Token: 0x04000D3D RID: 3389
		private bool restoreDirectory;

		// Token: 0x04000D3E RID: 3390
		private bool showHelp;

		// Token: 0x04000D3F RID: 3391
		private string title;

		// Token: 0x04000D40 RID: 3392
		private bool validateNames = true;

		// Token: 0x04000D41 RID: 3393
		private bool auto_upgrade_enable = true;

		// Token: 0x04000D42 RID: 3394
		private FileDialogCustomPlacesCollection custom_places;

		// Token: 0x04000D43 RID: 3395
		private bool supportMultiDottedExtensions;

		// Token: 0x04000D44 RID: 3396
		private bool checkForIllegalChars = true;

		// Token: 0x04000D45 RID: 3397
		private Button cancelButton;

		// Token: 0x04000D46 RID: 3398
		private ToolBarButton upToolBarButton;

		// Token: 0x04000D47 RID: 3399
		private PopupButtonPanel popupButtonPanel;

		// Token: 0x04000D48 RID: 3400
		private Button openSaveButton;

		// Token: 0x04000D49 RID: 3401
		private Button helpButton;

		// Token: 0x04000D4A RID: 3402
		private Label fileTypeLabel;

		// Token: 0x04000D4B RID: 3403
		private ToolBarButton menueToolBarButton;

		// Token: 0x04000D4C RID: 3404
		private ContextMenu menueToolBarButtonContextMenu;

		// Token: 0x04000D4D RID: 3405
		private ToolBar smallButtonToolBar;

		// Token: 0x04000D4E RID: 3406
		private DirComboBox dirComboBox;

		// Token: 0x04000D4F RID: 3407
		private ComboBox fileNameComboBox;

		// Token: 0x04000D50 RID: 3408
		private Label fileNameLabel;

		// Token: 0x04000D51 RID: 3409
		private MWFFileView mwfFileView;

		// Token: 0x04000D52 RID: 3410
		private MwfFileViewItemComparer file_view_comparer;

		// Token: 0x04000D53 RID: 3411
		private Label searchSaveLabel;

		// Token: 0x04000D54 RID: 3412
		private ToolBarButton newdirToolBarButton;

		// Token: 0x04000D55 RID: 3413
		private ToolBarButton backToolBarButton;

		// Token: 0x04000D56 RID: 3414
		private ComboBox fileTypeComboBox;

		// Token: 0x04000D57 RID: 3415
		private ImageList imageListTopToolbar;

		// Token: 0x04000D58 RID: 3416
		private CheckBox readonlyCheckBox;

		// Token: 0x04000D59 RID: 3417
		private bool multiSelect;

		// Token: 0x04000D5A RID: 3418
		private string restoreDirectoryString = string.Empty;

		// Token: 0x04000D5B RID: 3419
		internal FileDialog.FileDialogType fileDialogType;

		// Token: 0x04000D5C RID: 3420
		private bool do_not_call_OnSelectedIndexChangedFileTypeComboBox;

		// Token: 0x04000D5D RID: 3421
		private bool showReadOnly;

		// Token: 0x04000D5E RID: 3422
		private bool readOnlyChecked;

		// Token: 0x04000D5F RID: 3423
		internal bool createPrompt;

		// Token: 0x04000D60 RID: 3424
		internal bool overwritePrompt = true;

		// Token: 0x04000D61 RID: 3425
		private FileFilter fileFilter;

		// Token: 0x04000D62 RID: 3426
		private string[] configFileNames;

		// Token: 0x04000D63 RID: 3427
		private string lastFolder = string.Empty;

		// Token: 0x04000D64 RID: 3428
		private MWFVFS vfs;

		// Token: 0x04000D65 RID: 3429
		private readonly char[] wildcard_chars = new char[] { '*', '?' };

		// Token: 0x04000D66 RID: 3430
		private bool disable_form_closed_event;

		// Token: 0x02000164 RID: 356
		internal enum FileDialogType
		{
			// Token: 0x04000D68 RID: 3432
			OpenFileDialog,
			// Token: 0x04000D69 RID: 3433
			SaveFileDialog
		}

		// Token: 0x02000165 RID: 357
		private class FileNamesTokenizer
		{
			// Token: 0x0600182A RID: 6186 RVA: 0x00059860 File Offset: 0x00057A60
			public FileNamesTokenizer(string text, bool allowMultiple)
			{
				this._text = text;
				this._position = 0;
				this._tokenType = FileDialog.TokenType.BOF;
				this._allowMultiple = allowMultiple;
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x0600182B RID: 6187 RVA: 0x00059890 File Offset: 0x00057A90
			public FileDialog.TokenType CurrentToken
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x0600182C RID: 6188 RVA: 0x00059898 File Offset: 0x00057A98
			public string TokenText
			{
				get
				{
					return this._tokenText;
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x0600182D RID: 6189 RVA: 0x000598A0 File Offset: 0x00057AA0
			public bool AllowMultiple
			{
				get
				{
					return this._allowMultiple;
				}
			}

			// Token: 0x0600182E RID: 6190 RVA: 0x000598A8 File Offset: 0x00057AA8
			private int ReadChar()
			{
				if (this._position < this._text.Length)
				{
					return (int)this._text.get_Chars(this._position++);
				}
				return -1;
			}

			// Token: 0x0600182F RID: 6191 RVA: 0x000598EC File Offset: 0x00057AEC
			private int PeekChar()
			{
				if (this._position < this._text.Length)
				{
					return (int)this._text.get_Chars(this._position);
				}
				return -1;
			}

			// Token: 0x06001830 RID: 6192 RVA: 0x00059918 File Offset: 0x00057B18
			private void SkipWhitespaceAndQuotes()
			{
				int num;
				while ((num = this.PeekChar()) != -1)
				{
					if ((ushort)num != 34 && !char.IsWhiteSpace((char)num))
					{
						break;
					}
					this.ReadChar();
				}
			}

			// Token: 0x06001831 RID: 6193 RVA: 0x0005995C File Offset: 0x00057B5C
			public void GetNextFile()
			{
				if (this._tokenType == FileDialog.TokenType.EOF)
				{
					throw new Exception(string.Empty);
				}
				this.SkipWhitespaceAndQuotes();
				if (this.PeekChar() == -1)
				{
					this._tokenType = FileDialog.TokenType.EOF;
					return;
				}
				this._tokenType = FileDialog.TokenType.FileName;
				StringBuilder stringBuilder = new StringBuilder();
				int num;
				while ((num = this.PeekChar()) != -1)
				{
					if ((ushort)num == 34)
					{
						this.ReadChar();
						if (this.AllowMultiple)
						{
							break;
						}
						int position = this._position;
						this.SkipWhitespaceAndQuotes();
						if (this.PeekChar() == -1)
						{
							break;
						}
						this._position = position + 1;
						stringBuilder.Append((char)num);
					}
					else
					{
						stringBuilder.Append((char)this.ReadChar());
					}
				}
				this._tokenText = stringBuilder.ToString();
			}

			// Token: 0x04000D6A RID: 3434
			private readonly bool _allowMultiple;

			// Token: 0x04000D6B RID: 3435
			private int _position;

			// Token: 0x04000D6C RID: 3436
			private readonly string _text;

			// Token: 0x04000D6D RID: 3437
			private FileDialog.TokenType _tokenType;

			// Token: 0x04000D6E RID: 3438
			private string _tokenText;
		}

		// Token: 0x02000166 RID: 358
		internal enum TokenType
		{
			// Token: 0x04000D70 RID: 3440
			BOF,
			// Token: 0x04000D71 RID: 3441
			EOF,
			// Token: 0x04000D72 RID: 3442
			FileName
		}
	}
}
