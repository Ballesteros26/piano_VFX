using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000028 RID: 40
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(SaveFileDialog), "SaveFileDialog.bmp")]
	[Description("Prompts the user to open a file.")]
	public class VistaSaveFileDialog : VistaFileDialog
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000098D2 File Offset: 0x00007AD2
		public VistaSaveFileDialog()
			: this(false)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000098E0 File Offset: 0x00007AE0
		public VistaSaveFileDialog(bool forceDownlevel)
		{
			bool flag = forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported;
			if (flag)
			{
				base.DownlevelDialog = new SaveFileDialog();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009914 File Offset: 0x00007B14
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009954 File Offset: 0x00007B54
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.")]
		public bool CreatePrompt
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((SaveFileDialog)base.DownlevelDialog).CreatePrompt;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_CREATEPROMPT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((SaveFileDialog)base.DownlevelDialog).CreatePrompt = value;
				}
				else
				{
					base.SetOption(NativeMethods.FOS.FOS_CREATEPROMPT, value);
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009990 File Offset: 0x00007B90
		// (set) Token: 0x0600021C RID: 540 RVA: 0x000099CC File Offset: 0x00007BCC
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
		public bool OverwritePrompt
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((SaveFileDialog)base.DownlevelDialog).OverwritePrompt;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((SaveFileDialog)base.DownlevelDialog).OverwritePrompt = value;
				}
				else
				{
					base.SetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT, value);
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009A04 File Offset: 0x00007C04
		public override void Reset()
		{
			base.Reset();
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				this.OverwritePrompt = true;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009A30 File Offset: 0x00007C30
		public Stream OpenFile()
		{
			bool flag = base.DownlevelDialog != null;
			Stream stream;
			if (flag)
			{
				stream = ((SaveFileDialog)base.DownlevelDialog).OpenFile();
			}
			else
			{
				string fileName = base.FileName;
				bool flag2 = string.IsNullOrEmpty(fileName);
				if (flag2)
				{
					throw new ArgumentNullException("FileName");
				}
				stream = new FileStream(fileName, 2, 3);
			}
			return stream;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009A88 File Offset: 0x00007C88
		protected override void OnFileOk(CancelEventArgs e)
		{
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				bool flag2 = this.CheckFileExists && !File.Exists(base.FileName);
				if (flag2)
				{
					base.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.FileNotFound, new string[] { Path.GetFileName(base.FileName) }), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					e.Cancel = true;
					return;
				}
				bool flag3 = this.CreatePrompt && !File.Exists(base.FileName);
				if (flag3)
				{
					bool flag4 = !base.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.CreatePrompt, new string[] { Path.GetFileName(base.FileName) }), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (flag4)
					{
						e.Cancel = true;
						return;
					}
				}
			}
			base.OnFileOk(e);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009B58 File Offset: 0x00007D58
		internal override IFileDialog CreateFileDialog()
		{
			return (NativeFileSaveDialog)new FileSaveDialogRCW();
		}
	}
}
