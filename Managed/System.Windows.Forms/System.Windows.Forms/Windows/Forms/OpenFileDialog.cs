using System;
using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to open a file. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027D RID: 637
	public sealed class OpenFileDialog : FileDialog
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.OpenFileDialog" /> class.</summary>
		// Token: 0x06002980 RID: 10624 RVA: 0x000A0310 File Offset: 0x0009E510
		public OpenFileDialog()
		{
			this.form.SuspendLayout();
			this.form.Text = "Open";
			this.CheckFileExists = true;
			base.OpenSaveButtonText = "Open";
			base.SearchSaveLabel = "Look in:";
			this.fileDialogType = FileDialog.FileDialogType.OpenFileDialog;
			this.form.ResumeLayout(false);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist. </summary>
		/// <returns>true if the dialog box displays a warning when the user specifies a file name that does not exist; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000A0370 File Offset: 0x0009E570
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x000A0378 File Offset: 0x0009E578
		[DefaultValue(true)]
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

		/// <summary>Gets or sets a value indicating whether the dialog box allows multiple files to be selected. </summary>
		/// <returns>true if the dialog box allows multiple files to be selected together or concurrently; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000A0384 File Offset: 0x0009E584
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x000A038C File Offset: 0x0009E58C
		[DefaultValue(false)]
		public bool Multiselect
		{
			get
			{
				return base.BMultiSelect;
			}
			set
			{
				base.BMultiSelect = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the read-only check box is selected. </summary>
		/// <returns>true if the read-only check box is selected; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000A0398 File Offset: 0x0009E598
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x000A03A0 File Offset: 0x0009E5A0
		[DefaultValue(false)]
		public new bool ReadOnlyChecked
		{
			get
			{
				return base.ReadOnlyChecked;
			}
			set
			{
				base.ReadOnlyChecked = value;
			}
		}

		/// <summary>Gets the file name and extension for the file selected in the dialog box. The file name does not include the path.</summary>
		/// <returns>The file name and extension for the file selected in the dialog box. The file name does not include the path. The default value is an empty string.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000A03AC File Offset: 0x0009E5AC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string SafeFileName
		{
			get
			{
				return Path.GetFileName(base.FileName);
			}
		}

		/// <summary>Gets an array of file names and extensions for all the selected files in the dialog box. The file names do not include the path.</summary>
		/// <returns>An array of file names and extensions for all the selected files in the dialog box. The file names do not include the path. If no files are selected, an empty array is returned.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000A03BC File Offset: 0x0009E5BC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string[] SafeFileNames
		{
			get
			{
				string[] fileNames = base.FileNames;
				for (int i = 0; i < fileNames.Length; i++)
				{
					fileNames[i] = Path.GetFileName(fileNames[i]);
				}
				return fileNames;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains a read-only check box. </summary>
		/// <returns>true if the dialog box contains a read-only check box; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000A03F0 File Offset: 0x0009E5F0
		// (set) Token: 0x0600298A RID: 10634 RVA: 0x000A03F8 File Offset: 0x0009E5F8
		[DefaultValue(false)]
		public new bool ShowReadOnly
		{
			get
			{
				return base.ShowReadOnly;
			}
			set
			{
				base.ShowReadOnly = value;
			}
		}

		/// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property. </summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
		/// <exception cref="T:System.ArgumentNullException">The file name is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600298B RID: 10635 RVA: 0x000A0404 File Offset: 0x0009E604
		public Stream OpenFile()
		{
			if (base.FileName.Length == 0)
			{
				throw new ArgumentNullException("OpenFile", "FileName is null");
			}
			return new FileStream(base.FileName, 3, 1);
		}

		/// <summary>Resets all properties to their default values. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600298C RID: 10636 RVA: 0x000A0440 File Offset: 0x0009E640
		public override void Reset()
		{
			base.Reset();
			base.BMultiSelect = false;
			base.CheckFileExists = true;
			base.ReadOnlyChecked = false;
			base.ShowReadOnly = false;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x000A0470 File Offset: 0x0009E670
		internal override string DialogTitle
		{
			get
			{
				string text = base.DialogTitle;
				if (text.Length == 0)
				{
					text = "Open";
				}
				return text;
			}
		}
	}
}
