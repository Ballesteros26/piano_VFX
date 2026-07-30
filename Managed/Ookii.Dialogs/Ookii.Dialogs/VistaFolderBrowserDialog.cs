using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000026 RID: 38
	[DefaultEvent("HelpRequest")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("SelectedPath")]
	[Description("Prompts the user to select a folder.")]
	public sealed class VistaFolderBrowserDialog : CommonDialog
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060001F5 RID: 501 RVA: 0x000090C6 File Offset: 0x000072C6
		// (remove) Token: 0x060001F6 RID: 502 RVA: 0x000090D1 File Offset: 0x000072D1
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

		// Token: 0x060001F7 RID: 503 RVA: 0x000090DC File Offset: 0x000072DC
		public VistaFolderBrowserDialog()
		{
			bool flag = !VistaFolderBrowserDialog.IsVistaFolderDialogSupported;
			if (flag)
			{
				this._downlevelDialog = new FolderBrowserDialog();
			}
			else
			{
				this.Reset();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009114 File Offset: 0x00007314
		[Browsable(false)]
		public static bool IsVistaFolderDialogSupported
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000912C File Offset: 0x0000732C
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009160 File Offset: 0x00007360
		[Category("Folder Browsing")]
		[DefaultValue("")]
		[Localizable(true)]
		[Browsable(true)]
		[Description("The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog.")]
		public string Description
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				string text;
				if (flag)
				{
					text = this._downlevelDialog.Description;
				}
				else
				{
					text = this._description;
				}
				return text;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.Description = value;
				}
				else
				{
					this._description = value ?? string.Empty;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000919C File Offset: 0x0000739C
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000091D0 File Offset: 0x000073D0
		[Localizable(false)]
		[Description("The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.")]
		[Category("Folder Browsing")]
		[Browsable(true)]
		[DefaultValue(typeof(Environment.SpecialFolder), "Desktop")]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				Environment.SpecialFolder specialFolder;
				if (flag)
				{
					specialFolder = this._downlevelDialog.RootFolder;
				}
				else
				{
					specialFolder = this._rootFolder;
				}
				return specialFolder;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.RootFolder = value;
				}
				else
				{
					this._rootFolder = value;
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009204 File Offset: 0x00007404
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00009238 File Offset: 0x00007438
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Description("The path selected by the user.")]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Folder Browsing")]
		public string SelectedPath
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				string text;
				if (flag)
				{
					text = this._downlevelDialog.SelectedPath;
				}
				else
				{
					text = this._selectedPath;
				}
				return text;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.SelectedPath = value;
				}
				else
				{
					this._selectedPath = value ?? string.Empty;
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00009274 File Offset: 0x00007474
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000092A8 File Offset: 0x000074A8
		[Browsable(true)]
		[Localizable(false)]
		[Description("A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.")]
		[DefaultValue(true)]
		[Category("Folder Browsing")]
		public bool ShowNewFolderButton
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this._downlevelDialog.ShowNewFolderButton;
				}
				else
				{
					flag2 = this._showNewFolderButton;
				}
				return flag2;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.ShowNewFolderButton = value;
				}
				else
				{
					this._showNewFolderButton = value;
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000092DC File Offset: 0x000074DC
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000092F4 File Offset: 0x000074F4
		[Category("Folder Browsing")]
		[DefaultValue(false)]
		[Description("A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.")]
		public bool UseDescriptionForTitle
		{
			get
			{
				return this._useDescriptionForTitle;
			}
			set
			{
				this._useDescriptionForTitle = value;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000092FE File Offset: 0x000074FE
		public override void Reset()
		{
			this._description = string.Empty;
			this._useDescriptionForTitle = false;
			this._selectedPath = string.Empty;
			this._rootFolder = 0;
			this._showNewFolderButton = true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000932C File Offset: 0x0000752C
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			bool flag = this._downlevelDialog != null;
			bool flag2;
			if (flag)
			{
				flag2 = this._downlevelDialog.ShowDialog((hwndOwner == IntPtr.Zero) ? null : new WindowHandleWrapper(hwndOwner)) == DialogResult.OK;
			}
			else
			{
				IFileDialog fileDialog = null;
				try
				{
					fileDialog = (NativeFileOpenDialog)new FileOpenDialogRCW();
					this.SetDialogProperties(fileDialog);
					int num = fileDialog.Show(hwndOwner);
					bool flag3 = num < 0;
					if (flag3)
					{
						bool flag4 = num == -2147023673;
						if (!flag4)
						{
							throw Marshal.GetExceptionForHR(num);
						}
						flag2 = false;
					}
					else
					{
						this.GetResult(fileDialog);
						flag2 = true;
					}
				}
				catch
				{
					flag2 = false;
				}
				finally
				{
					bool flag5 = fileDialog != null;
					if (flag5)
					{
						Marshal.FinalReleaseComObject(fileDialog);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000093F4 File Offset: 0x000075F4
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009440 File Offset: 0x00007640
		private void SetDialogProperties(IFileDialog dialog)
		{
			bool flag = !string.IsNullOrEmpty(this._description);
			if (flag)
			{
				bool useDescriptionForTitle = this._useDescriptionForTitle;
				if (useDescriptionForTitle)
				{
					dialog.SetTitle(this._description);
				}
				else
				{
					IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
					fileDialogCustomize.AddText(0, this._description);
				}
			}
			dialog.SetOptions(NativeMethods.FOS.FOS_PICKFOLDERS | NativeMethods.FOS.FOS_FORCEFILESYSTEM | NativeMethods.FOS.FOS_FILEMUSTEXIST);
			bool flag2 = !string.IsNullOrEmpty(this._selectedPath);
			if (flag2)
			{
				string directoryName = Path.GetDirectoryName(this._selectedPath);
				bool flag3 = directoryName == null || !Directory.Exists(directoryName);
				if (flag3)
				{
					dialog.SetFileName(this._selectedPath);
				}
				else
				{
					string fileName = Path.GetFileName(this._selectedPath);
					dialog.SetFolder(NativeMethods.CreateItemFromParsingName(directoryName));
					dialog.SetFileName(fileName);
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009510 File Offset: 0x00007710
		private void GetResult(IFileDialog dialog)
		{
			IShellItem shellItem;
			dialog.GetResult(out shellItem);
			shellItem.GetDisplayName((NativeMethods.SIGDN)2147844096U, out this._selectedPath);
		}

		// Token: 0x040000B8 RID: 184
		private FolderBrowserDialog _downlevelDialog;

		// Token: 0x040000B9 RID: 185
		private string _description;

		// Token: 0x040000BA RID: 186
		private bool _useDescriptionForTitle;

		// Token: 0x040000BB RID: 187
		private string _selectedPath;

		// Token: 0x040000BC RID: 188
		private Environment.SpecialFolder _rootFolder;

		// Token: 0x040000BD RID: 189
		private bool _showNewFolderButton;
	}
}
