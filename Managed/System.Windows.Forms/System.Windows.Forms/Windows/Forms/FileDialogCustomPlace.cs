using System;

namespace System.Windows.Forms
{
	/// <summary>Represents an entry in a <see cref="T:System.Windows.Forms.FileDialog" /> custom place collection for Windows Vista.</summary>
	// Token: 0x02000184 RID: 388
	public class FileDialogCustomPlace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class. with a specified folder path to a custom place.</summary>
		/// <param name="path">A folder path to the custom place.</param>
		// Token: 0x0600193C RID: 6460 RVA: 0x00060578 File Offset: 0x0005E778
		public FileDialogCustomPlace(string path)
		{
			this.path = path;
			this.guid = Guid.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class with a custom place identified by a Windows Vista Known Folder GUID.</summary>
		/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder. </param>
		// Token: 0x0600193D RID: 6461 RVA: 0x00060594 File Offset: 0x0005E794
		public FileDialogCustomPlace(Guid knownFolderGuid)
		{
			this.path = string.Empty;
			this.guid = knownFolderGuid;
		}

		/// <summary>Gets or sets the folder path to the custom place.</summary>
		/// <returns>A folder path to the custom place. If the custom place was specified with a Windows Vista Known Folder GUID, then an empty string is returned.</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x000605B0 File Offset: 0x0005E7B0
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x000605B8 File Offset: 0x0005E7B8
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
				this.guid = Guid.Empty;
			}
		}

		/// <summary>Gets or sets the Windows Vista Known Folder GUID for the custom place.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that indicates the Windows Vista Known Folder for the custom place. If the custom place was specified with a folder path, then an empty GUID is returned. For a list of the available Windows Vista Known Folders, see Known Folder GUIDs for File Dialog Custom Places or the KnownFolders.h file in the Windows SDK.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x000605CC File Offset: 0x0005E7CC
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x000605D4 File Offset: 0x0005E7D4
		public Guid KnownFolderGuid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
				this.path = string.Empty;
			}
		}

		/// <summary>Returns a string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</summary>
		/// <returns>A string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</returns>
		// Token: 0x06001942 RID: 6466 RVA: 0x000605E8 File Offset: 0x0005E7E8
		public override string ToString()
		{
			return string.Format("{0} Path: {1} KnownFolderGuid: {2}", base.GetType().ToString(), this.path, this.guid);
		}

		// Token: 0x04000E30 RID: 3632
		private string path;

		// Token: 0x04000E31 RID: 3633
		private Guid guid;
	}
}
