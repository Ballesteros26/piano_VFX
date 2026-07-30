using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000027 RID: 39
	[ToolboxBitmap(typeof(OpenFileDialog), "OpenFileDialog.bmp")]
	[Description("Prompts the user to open a file.")]
	public class VistaOpenFileDialog : VistaFileDialog
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00009539 File Offset: 0x00007739
		public VistaOpenFileDialog()
			: this(false)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009544 File Offset: 0x00007744
		public VistaOpenFileDialog(bool forceDownlevel)
		{
			bool flag = forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported;
			if (flag)
			{
				base.DownlevelDialog = new OpenFileDialog();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00009578 File Offset: 0x00007778
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00009590 File Offset: 0x00007790
		[DefaultValue(true)]
		[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
		public override bool CheckFileExists
		{
			get
			{
				return base.CheckFileExists;
			}
			set
			{
				base.CheckFileExists = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000959C File Offset: 0x0000779C
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000095DC File Offset: 0x000077DC
		[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Multiselect
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).Multiselect;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).Multiselect = value;
				}
				base.SetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009618 File Offset: 0x00007818
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00009650 File Offset: 0x00007850
		[Description("A value indicating whether the dialog box contains a read-only check box.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ShowReadOnly
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).ShowReadOnly;
				}
				else
				{
					flag2 = this._showReadOnly;
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).ShowReadOnly = value;
				}
				else
				{
					this._showReadOnly = value;
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009688 File Offset: 0x00007888
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000096C0 File Offset: 0x000078C0
		[DefaultValue(false)]
		[Description("A value indicating whether the read-only check box is selected.")]
		[Category("Behavior")]
		public bool ReadOnlyChecked
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked;
				}
				else
				{
					flag2 = this._readOnlyChecked;
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked = value;
				}
				else
				{
					this._readOnlyChecked = value;
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000096F8 File Offset: 0x000078F8
		public override void Reset()
		{
			base.Reset();
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				this.CheckFileExists = true;
				this._showReadOnly = false;
				this._readOnlyChecked = false;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009734 File Offset: 0x00007934
		public Stream OpenFile()
		{
			bool flag = base.DownlevelDialog != null;
			Stream stream;
			if (flag)
			{
				stream = ((OpenFileDialog)base.DownlevelDialog).OpenFile();
			}
			else
			{
				string fileName = base.FileName;
				bool flag2 = string.IsNullOrEmpty(fileName);
				if (flag2)
				{
					throw new ArgumentNullException("FileName");
				}
				stream = new FileStream(fileName, 3, 1);
			}
			return stream;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000978C File Offset: 0x0000798C
		internal override IFileDialog CreateFileDialog()
		{
			return (NativeFileOpenDialog)new FileOpenDialogRCW();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000097A8 File Offset: 0x000079A8
		internal override void SetDialogProperties(IFileDialog dialog)
		{
			base.SetDialogProperties(dialog);
			bool showReadOnly = this._showReadOnly;
			if (showReadOnly)
			{
				IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
				fileDialogCustomize.EnableOpenDropDown(16386);
				fileDialogCustomize.AddControlItem(16386, 16387, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.OpenButton));
				fileDialogCustomize.AddControlItem(16386, 16388, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ReadOnly));
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009814 File Offset: 0x00007A14
		internal override void GetResult(IFileDialog dialog)
		{
			bool multiselect = this.Multiselect;
			if (multiselect)
			{
				IShellItemArray shellItemArray;
				((IFileOpenDialog)dialog).GetResults(out shellItemArray);
				uint num;
				shellItemArray.GetCount(out num);
				string[] array = new string[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					IShellItem shellItem;
					shellItemArray.GetItemAt(num2, out shellItem);
					string text;
					shellItem.GetDisplayName((NativeMethods.SIGDN)2147844096U, out text);
					array[(int)num2] = text;
				}
				base.FileNamesInternal = array;
			}
			else
			{
				base.FileNamesInternal = null;
			}
			bool showReadOnly = this.ShowReadOnly;
			if (showReadOnly)
			{
				IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
				int num3;
				fileDialogCustomize.GetSelectedControlItem(16386, out num3);
				this._readOnlyChecked = num3 == 16388;
			}
			base.GetResult(dialog);
		}

		// Token: 0x040000BE RID: 190
		private bool _showReadOnly;

		// Token: 0x040000BF RID: 191
		private bool _readOnlyChecked;

		// Token: 0x040000C0 RID: 192
		private const int _openDropDownId = 16386;

		// Token: 0x040000C1 RID: 193
		private const int _openItemId = 16387;

		// Token: 0x040000C2 RID: 194
		private const int _readOnlyItemId = 16388;
	}
}
