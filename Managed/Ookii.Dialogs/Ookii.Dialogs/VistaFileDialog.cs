using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000024 RID: 36
	[DefaultEvent("FileOk")]
	[DefaultProperty("FileName")]
	public abstract class VistaFileDialog : CommonDialog
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060001B4 RID: 436 RVA: 0x0000824C File Offset: 0x0000644C
		// (remove) Token: 0x060001B5 RID: 437 RVA: 0x00008261 File Offset: 0x00006461
		public event CancelEventHandler FileOk
		{
			add
			{
				base.Events.AddHandler(VistaFileDialog.EventFileOk, value);
			}
			remove
			{
				base.Events.RemoveHandler(VistaFileDialog.EventFileOk, value);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008276 File Offset: 0x00006476
		protected VistaFileDialog()
		{
			this.Reset();
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008288 File Offset: 0x00006488
		[Browsable(false)]
		public static bool IsVistaFileDialogSupported
		{
			get
			{
				return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version.Major >= 6;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000082C0 File Offset: 0x000064C0
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000082F4 File Offset: 0x000064F4
		[Description("A value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AddExtension
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.AddExtension;
				}
				else
				{
					flag2 = this._addExtension;
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.AddExtension = value;
				}
				else
				{
					this._addExtension = value;
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008328 File Offset: 0x00006528
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00008360 File Offset: 0x00006560
		[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool CheckFileExists
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.CheckFileExists;
				}
				else
				{
					flag2 = this.GetOption(NativeMethods.FOS.FOS_FILEMUSTEXIST);
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.CheckFileExists = value;
				}
				else
				{
					this.SetOption(NativeMethods.FOS.FOS_FILEMUSTEXIST, value);
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008398 File Offset: 0x00006598
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000083D0 File Offset: 0x000065D0
		[Description("A value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool CheckPathExists
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.CheckPathExists;
				}
				else
				{
					flag2 = this.GetOption(NativeMethods.FOS.FOS_PATHMUSTEXIST);
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.CheckPathExists = value;
				}
				else
				{
					this.SetOption(NativeMethods.FOS.FOS_PATHMUSTEXIST, value);
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008408 File Offset: 0x00006608
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00008450 File Offset: 0x00006650
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The default file name extension.")]
		public string DefaultExt
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string text;
				if (flag)
				{
					text = this.DownlevelDialog.DefaultExt;
				}
				else
				{
					bool flag2 = this._defaultExt != null;
					if (flag2)
					{
						text = this._defaultExt;
					}
					else
					{
						text = string.Empty;
					}
				}
				return text;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.DefaultExt = value;
				}
				else
				{
					bool flag2 = value != null;
					if (flag2)
					{
						bool flag3 = value.StartsWith(".", 4);
						if (flag3)
						{
							value = value.Substring(1);
						}
						else
						{
							bool flag4 = value.Length == 0;
							if (flag4)
							{
								value = null;
							}
						}
					}
					this._defaultExt = value;
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000084B8 File Offset: 0x000066B8
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000084F4 File Offset: 0x000066F4
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).")]
		[DefaultValue(true)]
		public bool DereferenceLinks
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.DereferenceLinks;
				}
				else
				{
					flag2 = !this.GetOption(NativeMethods.FOS.FOS_NODEREFERENCELINKS);
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.DereferenceLinks = value;
				}
				else
				{
					this.SetOption(NativeMethods.FOS.FOS_NODEREFERENCELINKS, !value);
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008530 File Offset: 0x00006730
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00008594 File Offset: 0x00006794
		[DefaultValue("")]
		[Category("Data")]
		[Description("A string containing the file name selected in the file dialog box.")]
		public string FileName
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string text;
				if (flag)
				{
					text = this.DownlevelDialog.FileName;
				}
				else
				{
					bool flag2 = this._fileNames == null || this._fileNames.Length == 0 || string.IsNullOrEmpty(this._fileNames[0]);
					if (flag2)
					{
						text = string.Empty;
					}
					else
					{
						text = this._fileNames[0];
					}
				}
				return text;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.FileName = value;
				}
				this._fileNames = new string[1];
				this._fileNames[0] = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000085D4 File Offset: 0x000067D4
		[Description("The file names of all selected files in the dialog box.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string[] FileNames
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string[] array;
				if (flag)
				{
					array = this.DownlevelDialog.FileNames;
				}
				else
				{
					array = this.FileNamesInternal;
				}
				return array;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008608 File Offset: 0x00006808
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000863C File Offset: 0x0000683C
		[Description("The current file name filter string, which determines the choices that appear in the \"Save as file type\" or \"Files of type\" box in the dialog box.")]
		[Category("Behavior")]
		[Localizable(true)]
		[DefaultValue("")]
		public string Filter
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string text;
				if (flag)
				{
					text = this.DownlevelDialog.Filter;
				}
				else
				{
					text = this._filter;
				}
				return text;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.Filter = value;
				}
				else
				{
					bool flag2 = value != this._filter;
					if (flag2)
					{
						bool flag3 = !string.IsNullOrEmpty(value);
						if (flag3)
						{
							string[] array = value.Split(new char[] { '|' });
							bool flag4 = array == null || array.Length % 2 != 0;
							if (flag4)
							{
								throw new ArgumentException(Resources.InvalidFilterString);
							}
						}
						else
						{
							value = null;
						}
						this._filter = value;
					}
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000086C8 File Offset: 0x000068C8
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000086FC File Offset: 0x000068FC
		[Description("The index of the filter currently selected in the file dialog box.")]
		[Category("Behavior")]
		[DefaultValue(1)]
		public int FilterIndex
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				int num;
				if (flag)
				{
					num = this.DownlevelDialog.FilterIndex;
				}
				else
				{
					num = this._filterIndex;
				}
				return num;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.FilterIndex = value;
				}
				else
				{
					this._filterIndex = value;
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008730 File Offset: 0x00006930
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00008778 File Offset: 0x00006978
		[Description("The initial directory displayed by the file dialog box.")]
		[DefaultValue("")]
		[Category("Data")]
		public string InitialDirectory
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string text;
				if (flag)
				{
					text = this.DownlevelDialog.InitialDirectory;
				}
				else
				{
					bool flag2 = this._initialDirectory != null;
					if (flag2)
					{
						text = this._initialDirectory;
					}
					else
					{
						text = string.Empty;
					}
				}
				return text;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.InitialDirectory = value;
				}
				else
				{
					this._initialDirectory = value;
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000087AC File Offset: 0x000069AC
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000087E0 File Offset: 0x000069E0
		[DefaultValue(false)]
		[Description("A value indicating whether the dialog box restores the current directory before closing.")]
		[Category("Behavior")]
		public bool RestoreDirectory
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.RestoreDirectory;
				}
				else
				{
					flag2 = this.GetOption(NativeMethods.FOS.FOS_NOCHANGEDIR);
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.RestoreDirectory = value;
				}
				else
				{
					this.SetOption(NativeMethods.FOS.FOS_NOCHANGEDIR, value);
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008814 File Offset: 0x00006A14
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008848 File Offset: 0x00006A48
		[Description("A value indicating whether the Help button is displayed in the file dialog box.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool ShowHelp
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.ShowHelp;
				}
				else
				{
					flag2 = this._showHelp;
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.ShowHelp = value;
				}
				else
				{
					this._showHelp = value;
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000887C File Offset: 0x00006A7C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000088C4 File Offset: 0x00006AC4
		[Description("The file dialog box title.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Title
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				string text;
				if (flag)
				{
					text = this.DownlevelDialog.Title;
				}
				else
				{
					bool flag2 = this._title != null;
					if (flag2)
					{
						text = this._title;
					}
					else
					{
						text = string.Empty;
					}
				}
				return text;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.Title = value;
				}
				else
				{
					this._title = value;
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000088F8 File Offset: 0x00006AF8
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000892C File Offset: 0x00006B2C
		[Description("Indicates whether the dialog box supports displaying and saving files that have multiple file name extensions.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool SupportMultiDottedExtensions
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.SupportMultiDottedExtensions;
				}
				else
				{
					flag2 = this._supportMultiDottedExtensions;
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.SupportMultiDottedExtensions = value;
				}
				else
				{
					this._supportMultiDottedExtensions = value;
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008960 File Offset: 0x00006B60
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000899C File Offset: 0x00006B9C
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box accepts only valid Win32 file names.")]
		public bool ValidateNames
		{
			get
			{
				bool flag = this.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this.DownlevelDialog.ValidateNames;
				}
				else
				{
					flag2 = !this.GetOption(NativeMethods.FOS.FOS_NOVALIDATE);
				}
				return flag2;
			}
			set
			{
				bool flag = this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.ValidateNames = value;
				}
				else
				{
					this.SetOption(NativeMethods.FOS.FOS_NOVALIDATE, !value);
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000089D8 File Offset: 0x00006BD8
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000089F0 File Offset: 0x00006BF0
		[Browsable(false)]
		protected FileDialog DownlevelDialog
		{
			get
			{
				return this._downlevelDialog;
			}
			set
			{
				this._downlevelDialog = value;
				bool flag = value != null;
				if (flag)
				{
					value.HelpRequest += new EventHandler(this.DownlevelDialog_HelpRequest);
					value.FileOk += new CancelEventHandler(this.DownlevelDialog_FileOk);
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008A38 File Offset: 0x00006C38
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00008A71 File Offset: 0x00006C71
		internal string[] FileNamesInternal
		{
			private get
			{
				bool flag = this._fileNames == null;
				string[] array;
				if (flag)
				{
					array = new string[0];
				}
				else
				{
					array = (string[])this._fileNames.Clone();
				}
				return array;
			}
			set
			{
				this._fileNames = value;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008A7C File Offset: 0x00006C7C
		public override void Reset()
		{
			bool flag = this.DownlevelDialog != null;
			if (flag)
			{
				this.DownlevelDialog.Reset();
			}
			else
			{
				this._fileNames = null;
				this._filter = null;
				this._filterIndex = 1;
				this._addExtension = true;
				this._defaultExt = null;
				this._options = (NativeMethods.FOS)0U;
				this._showHelp = false;
				this._title = null;
				this._supportMultiDottedExtensions = false;
				this.CheckPathExists = true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008AF0 File Offset: 0x00006CF0
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			bool flag = this.DownlevelDialog != null;
			bool flag2;
			if (flag)
			{
				flag2 = this.DownlevelDialog.ShowDialog((hwndOwner == IntPtr.Zero) ? null : new WindowHandleWrapper(hwndOwner)) == DialogResult.OK;
			}
			else
			{
				flag2 = this.RunFileDialog(hwndOwner);
			}
			return flag2;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008B40 File Offset: 0x00006D40
		internal void SetOption(NativeMethods.FOS option, bool value)
		{
			if (value)
			{
				this._options |= option;
			}
			else
			{
				this._options &= ~option;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008B74 File Offset: 0x00006D74
		internal bool GetOption(NativeMethods.FOS option)
		{
			return (this._options & option) > (NativeMethods.FOS)0U;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008B94 File Offset: 0x00006D94
		internal virtual void GetResult(IFileDialog dialog)
		{
			bool flag = !this.GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
			if (flag)
			{
				this._fileNames = new string[1];
				IShellItem shellItem;
				dialog.GetResult(out shellItem);
				shellItem.GetDisplayName((NativeMethods.SIGDN)2147844096U, out this._fileNames[0]);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008BE4 File Offset: 0x00006DE4
		protected virtual void OnFileOk(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[VistaFileDialog.EventFileOk];
			bool flag = cancelEventHandler != null;
			if (flag)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008C1C File Offset: 0x00006E1C
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.DownlevelDialog != null;
				if (flag)
				{
					this.DownlevelDialog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008C68 File Offset: 0x00006E68
		internal bool PromptUser(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			string text2 = (string.IsNullOrEmpty(this._title) ? ((this is VistaOpenFileDialog) ? ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.Open) : ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ConfirmSaveAs)) : this._title);
			IWin32Window win32Window = ((this._hwndOwner == IntPtr.Zero) ? null : new WindowHandleWrapper(this._hwndOwner));
			MessageBoxOptions messageBoxOptions = (MessageBoxOptions)0;
			bool isRightToLeft = Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			if (isRightToLeft)
			{
				messageBoxOptions |= MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
			}
			return MessageBox.Show(win32Window, text, text2, buttons, icon, MessageBoxDefaultButton.Button1, messageBoxOptions) == DialogResult.Yes;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008D04 File Offset: 0x00006F04
		internal virtual void SetDialogProperties(IFileDialog dialog)
		{
			uint num;
			dialog.Advise(new VistaFileDialogEvents(this), out num);
			bool flag = this._fileNames != null && this._fileNames.Length != 0 && !string.IsNullOrEmpty(this._fileNames[0]);
			if (flag)
			{
				string directoryName = Path.GetDirectoryName(this._fileNames[0]);
				bool flag2 = directoryName == null || !Directory.Exists(directoryName);
				if (flag2)
				{
					dialog.SetFileName(this._fileNames[0]);
				}
				else
				{
					string fileName = Path.GetFileName(this._fileNames[0]);
					dialog.SetFolder(NativeMethods.CreateItemFromParsingName(directoryName));
					dialog.SetFileName(fileName);
				}
			}
			bool flag3 = !string.IsNullOrEmpty(this._filter);
			if (flag3)
			{
				string[] array = this._filter.Split(new char[] { '|' });
				NativeMethods.COMDLG_FILTERSPEC[] array2 = new NativeMethods.COMDLG_FILTERSPEC[array.Length / 2];
				for (int i = 0; i < array.Length; i += 2)
				{
					array2[i / 2].pszName = array[i];
					array2[i / 2].pszSpec = array[i + 1];
				}
				dialog.SetFileTypes((uint)array2.Length, array2);
				bool flag4 = this._filterIndex > 0 && this._filterIndex <= array2.Length;
				if (flag4)
				{
					dialog.SetFileTypeIndex((uint)this._filterIndex);
				}
			}
			bool flag5 = this._addExtension && !string.IsNullOrEmpty(this._defaultExt);
			if (flag5)
			{
				dialog.SetDefaultExtension(this._defaultExt);
			}
			bool flag6 = !string.IsNullOrEmpty(this._initialDirectory);
			if (flag6)
			{
				IShellItem shellItem = NativeMethods.CreateItemFromParsingName(this._initialDirectory);
				dialog.SetDefaultFolder(shellItem);
			}
			bool showHelp = this._showHelp;
			if (showHelp)
			{
				IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
				fileDialogCustomize.AddPushButton(16385, Resources.Help);
			}
			bool flag7 = !string.IsNullOrEmpty(this._title);
			if (flag7)
			{
				dialog.SetTitle(this._title);
			}
			dialog.SetOptions(this._options | NativeMethods.FOS.FOS_FORCEFILESYSTEM);
		}

		// Token: 0x060001E2 RID: 482
		internal abstract IFileDialog CreateFileDialog();

		// Token: 0x060001E3 RID: 483 RVA: 0x00008F1B File Offset: 0x0000711B
		internal void DoHelpRequest()
		{
			this.OnHelpRequest(new HelpEventArgs(Cursor.Position));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008F30 File Offset: 0x00007130
		internal bool DoFileOk(IFileDialog dialog)
		{
			this.GetResult(dialog);
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnFileOk(cancelEventArgs);
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008F64 File Offset: 0x00007164
		private bool RunFileDialog(IntPtr hwndOwner)
		{
			this._hwndOwner = hwndOwner;
			IFileDialog fileDialog = null;
			bool flag3;
			try
			{
				fileDialog = this.CreateFileDialog();
				this.SetDialogProperties(fileDialog);
				int num = fileDialog.Show(hwndOwner);
				bool flag = num < 0;
				if (flag)
				{
					bool flag2 = num == -2147023673;
					if (!flag2)
					{
						throw Marshal.GetExceptionForHR(num);
					}
					flag3 = false;
				}
				else
				{
					flag3 = true;
				}
			}
			catch
			{
				flag3 = false;
			}
			finally
			{
				this._hwndOwner = IntPtr.Zero;
				bool flag4 = fileDialog != null;
				if (flag4)
				{
					Marshal.FinalReleaseComObject(fileDialog);
				}
			}
			return flag3;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009000 File Offset: 0x00007200
		private void DownlevelDialog_HelpRequest(object sender, EventArgs e)
		{
			this.OnHelpRequest(e);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000900B File Offset: 0x0000720B
		private void DownlevelDialog_FileOk(object sender, CancelEventArgs e)
		{
			this.OnFileOk(e);
		}

		// Token: 0x040000A6 RID: 166
		internal const int HelpButtonId = 16385;

		// Token: 0x040000A7 RID: 167
		private FileDialog _downlevelDialog;

		// Token: 0x040000A8 RID: 168
		private NativeMethods.FOS _options;

		// Token: 0x040000A9 RID: 169
		private string _filter;

		// Token: 0x040000AA RID: 170
		private int _filterIndex;

		// Token: 0x040000AB RID: 171
		private string[] _fileNames;

		// Token: 0x040000AC RID: 172
		private string _defaultExt;

		// Token: 0x040000AD RID: 173
		private bool _addExtension;

		// Token: 0x040000AE RID: 174
		private string _initialDirectory;

		// Token: 0x040000AF RID: 175
		private bool _showHelp;

		// Token: 0x040000B0 RID: 176
		private string _title;

		// Token: 0x040000B1 RID: 177
		private bool _supportMultiDottedExtensions;

		// Token: 0x040000B2 RID: 178
		private IntPtr _hwndOwner;

		// Token: 0x040000B3 RID: 179
		private static readonly object EventFileOk = new object();
	}
}
