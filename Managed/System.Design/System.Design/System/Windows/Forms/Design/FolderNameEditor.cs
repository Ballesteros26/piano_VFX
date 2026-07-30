using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for choosing a folder from the file system.</summary>
	// Token: 0x0200001F RID: 31
	[MonoTODO]
	public class FolderNameEditor : UITypeEditor
	{
		/// <summary>Edits the specified object using the editor style provided by <see cref="M:System.Windows.Forms.Design.FolderNameEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
		/// <returns>The new value of the object, or the old value if the object couldn't be updated.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">A service object provider. </param>
		/// <param name="value">The value to set. </param>
		// Token: 0x0600013B RID: 315 RVA: 0x00004FAF File Offset: 0x000031AF
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (this.folderBrowser == null)
			{
				this.folderBrowser = new FolderNameEditor.FolderBrowser();
				this.InitializeDialog(this.folderBrowser);
			}
			if (this.folderBrowser.ShowDialog() != 1)
			{
				return value;
			}
			return this.folderBrowser.DirectoryPath;
		}

		/// <summary>Gets the editing style used by the <see cref="M:System.Windows.Forms.Design.FolderNameEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the provided editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x0600013C RID: 316 RVA: 0x00004FAC File Offset: 0x000031AC
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>Initializes the folder browser dialog.</summary>
		/// <param name="folderBrowser">A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> to choose a folder. </param>
		// Token: 0x0600013D RID: 317 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
		}

		// Token: 0x04000042 RID: 66
		private FolderNameEditor.FolderBrowser folderBrowser;

		/// <summary>Defines identifiers used to indicate the root folder for a folder browser to initially browse to.</summary>
		// Token: 0x02000020 RID: 32
		protected enum FolderBrowserFolder
		{
			/// <summary>The user's desktop.</summary>
			// Token: 0x04000044 RID: 68
			Desktop,
			/// <summary>The user's favorites list.</summary>
			// Token: 0x04000045 RID: 69
			Favorites = 6,
			/// <summary>The contents of the My Computer icon.</summary>
			// Token: 0x04000046 RID: 70
			MyComputer = 17,
			/// <summary>The user's My Documents folder.</summary>
			// Token: 0x04000047 RID: 71
			MyDocuments = 5,
			/// <summary>User's location to store pictures.</summary>
			// Token: 0x04000048 RID: 72
			MyPictures = 39,
			/// <summary>Network and dial-up connections.</summary>
			// Token: 0x04000049 RID: 73
			NetAndDialUpConnections = 49,
			/// <summary>The network neighborhood.</summary>
			// Token: 0x0400004A RID: 74
			NetworkNeighborhood = 18,
			/// <summary>A folder containing installed printers.</summary>
			// Token: 0x0400004B RID: 75
			Printers = 4,
			/// <summary>A folder containing shortcuts to recently opened files.</summary>
			// Token: 0x0400004C RID: 76
			Recent = 8,
			/// <summary>A folder containing shortcuts to applications to send documents to.</summary>
			// Token: 0x0400004D RID: 77
			SendTo,
			/// <summary>The user's start menu.</summary>
			// Token: 0x0400004E RID: 78
			StartMenu = 11,
			/// <summary>The user's file templates.</summary>
			// Token: 0x0400004F RID: 79
			Templates = 21
		}

		/// <summary>Defines identifiers used to specify behavior of a <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" />.</summary>
		// Token: 0x02000021 RID: 33
		[Flags]
		protected enum FolderBrowserStyles
		{
			/// <summary>The folder browser can only return computers. If the user selects anything other than a computer, the OK button is grayed.</summary>
			// Token: 0x04000051 RID: 81
			BrowseForComputer = 4096,
			/// <summary>The folder browser can return any object that it can return.</summary>
			// Token: 0x04000052 RID: 82
			BrowseForEverything = 16384,
			/// <summary>The folder browser can only return printers. If the user selects anything other than a printer, the OK button is grayed.</summary>
			// Token: 0x04000053 RID: 83
			BrowseForPrinter = 8192,
			/// <summary>The folder browser will not include network folders below the domain level in the dialog box's tree view control, or allow navigation to network locations outside of the domain.</summary>
			// Token: 0x04000054 RID: 84
			RestrictToDomain = 2,
			/// <summary>The folder browser will only return local file system directories. If the user selects folders that are not part of the local file system, the OK button is grayed.</summary>
			// Token: 0x04000055 RID: 85
			RestrictToFilesystem = 1,
			/// <summary>The folder browser will only return obejcts of the local file system that are within the root folder or a subfolder of the root folder. If the user selects a subfolder of the root folder that is not part of the local file system, the OK button is grayed.</summary>
			// Token: 0x04000056 RID: 86
			RestrictToSubfolders = 8,
			/// <summary>The folder browser includes a <see cref="T:System.Windows.Forms.TextBox" /> control in the browse dialog box that allows the user to type the name of an item.</summary>
			// Token: 0x04000057 RID: 87
			ShowTextBox = 16
		}

		/// <summary>Represents a dialog box that allows the user to choose a folder. This class cannot be inherited.</summary>
		// Token: 0x02000022 RID: 34
		protected sealed class FolderBrowser : Component
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> class. </summary>
			// Token: 0x0600013E RID: 318 RVA: 0x00004FEB File Offset: 0x000031EB
			[MonoTODO]
			public FolderBrowser()
			{
				this.startLocation = FolderNameEditor.FolderBrowserFolder.Desktop;
				this.publicOptions = FolderNameEditor.FolderBrowserStyles.RestrictToFilesystem;
				this.descriptionText = string.Empty;
				this.directoryPath = string.Empty;
			}

			/// <summary>Gets or sets a description to show above the folders.</summary>
			/// <returns>The description to show above the folders.</returns>
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00005017 File Offset: 0x00003217
			// (set) Token: 0x06000140 RID: 320 RVA: 0x0000501F File Offset: 0x0000321F
			public string Description
			{
				get
				{
					return this.descriptionText;
				}
				set
				{
					this.descriptionText = ((value == null) ? string.Empty : value);
				}
			}

			/// <summary>Gets the directory path to the object the user picked.</summary>
			/// <returns>The directory path to the object the user picked.</returns>
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000141 RID: 321 RVA: 0x00005032 File Offset: 0x00003232
			public string DirectoryPath
			{
				get
				{
					return this.directoryPath;
				}
			}

			/// <summary>Gets or sets the start location of the root node.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserFolder" /> that indicates the location for the folder browser to initially browse to.</returns>
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000142 RID: 322 RVA: 0x0000503A File Offset: 0x0000323A
			// (set) Token: 0x06000143 RID: 323 RVA: 0x00005042 File Offset: 0x00003242
			public FolderNameEditor.FolderBrowserFolder StartLocation
			{
				get
				{
					return this.startLocation;
				}
				set
				{
					this.startLocation = value;
				}
			}

			/// <summary>The styles the folder browser will use when browsing folders. This should be a combination of flags from the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserStyles" /> enumeration.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserStyles" /> enumeration member that indicates behavior for the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> to use.</returns>
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000144 RID: 324 RVA: 0x0000504B File Offset: 0x0000324B
			// (set) Token: 0x06000145 RID: 325 RVA: 0x00005053 File Offset: 0x00003253
			public FolderNameEditor.FolderBrowserStyles Style
			{
				get
				{
					return this.publicOptions;
				}
				set
				{
					this.publicOptions = value;
				}
			}

			/// <summary>Shows the folder browser dialog.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DialogResult" /> from the form.</returns>
			// Token: 0x06000146 RID: 326 RVA: 0x0000505C File Offset: 0x0000325C
			[MonoTODO]
			public DialogResult ShowDialog()
			{
				return this.ShowDialog(null);
			}

			/// <summary>Shows the folder browser dialog with the specified owner.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DialogResult" /> from the form.</returns>
			/// <param name="owner">Top-level window that will own the modal dialog (e.g.: System.Windows.Forms.Form). </param>
			// Token: 0x06000147 RID: 327 RVA: 0x0000234B File Offset: 0x0000054B
			[MonoTODO]
			public DialogResult ShowDialog(IWin32Window owner)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000058 RID: 88
			private string descriptionText;

			// Token: 0x04000059 RID: 89
			private string directoryPath;

			// Token: 0x0400005A RID: 90
			private FolderNameEditor.FolderBrowserStyles publicOptions;

			// Token: 0x0400005B RID: 91
			private FolderNameEditor.FolderBrowserFolder startLocation;
		}
	}
}
