using System;
using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a location for saving a file. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002C3 RID: 707
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public sealed class SaveFileDialog : FileDialog
	{
		/// <summary>Initializes a new instance of this class.</summary>
		// Token: 0x06002ED3 RID: 11987 RVA: 0x000B4C64 File Offset: 0x000B2E64
		public SaveFileDialog()
		{
			this.form.SuspendLayout();
			this.form.Text = "Save As";
			base.FileTypeLabel = "Save as type:";
			base.OpenSaveButtonText = "Save";
			base.SearchSaveLabel = "Save in:";
			this.fileDialogType = FileDialog.FileDialogType.SaveFileDialog;
			this.form.ResumeLayout(false);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.</summary>
		/// <returns>true if the dialog box prompts the user before creating a file if the user specifies a file name that does not exist; false if the dialog box automatically creates the new file without prompting the user for permission. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000B4CD4 File Offset: 0x000B2ED4
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x000B4CC8 File Offset: 0x000B2EC8
		[DefaultValue(false)]
		public bool CreatePrompt
		{
			get
			{
				return this.createPrompt;
			}
			set
			{
				this.createPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.</summary>
		/// <returns>true if the dialog box prompts the user before overwriting an existing file if the user specifies a file name that already exists; false if the dialog box automatically overwrites the existing file without prompting the user for permission. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000B4CE8 File Offset: 0x000B2EE8
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x000B4CDC File Offset: 0x000B2EDC
		[DefaultValue(true)]
		public bool OverwritePrompt
		{
			get
			{
				return this.overwritePrompt;
			}
			set
			{
				this.overwritePrompt = value;
			}
		}

		/// <summary>Opens the file with read/write permission selected by the user.</summary>
		/// <returns>The read/write file selected by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002ED8 RID: 11992 RVA: 0x000B4CF0 File Offset: 0x000B2EF0
		public Stream OpenFile()
		{
			if (base.FileName == null)
			{
				throw new ArgumentNullException("OpenFile", "FileName is null");
			}
			Stream stream;
			try
			{
				stream = new FileStream(base.FileName, 2, 3);
			}
			catch (Exception)
			{
				stream = null;
			}
			return stream;
		}

		/// <summary>Resets all dialog box options to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002ED9 RID: 11993 RVA: 0x000B4D50 File Offset: 0x000B2F50
		public override void Reset()
		{
			base.Reset();
			this.overwritePrompt = true;
			this.createPrompt = false;
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000B4D68 File Offset: 0x000B2F68
		internal override string DialogTitle
		{
			get
			{
				string text = base.DialogTitle;
				if (text.Length == 0)
				{
					text = "Save As";
				}
				return text;
			}
		}
	}
}
