using System;
using System.Collections.ObjectModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of Windows Vista custom places for the <see cref="T:System.Windows.Forms.FileDialog" /> class.</summary>
	// Token: 0x02000185 RID: 389
	public class FileDialogCustomPlacesCollection : Collection<FileDialogCustomPlace>
	{
		/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
		/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder. </param>
		// Token: 0x06001944 RID: 6468 RVA: 0x00060624 File Offset: 0x0005E824
		public void Add(Guid knownFolderGuid)
		{
			this.Add(new FileDialogCustomPlace(knownFolderGuid));
		}

		/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
		/// <param name="path">A folder path to the custom place.</param>
		// Token: 0x06001945 RID: 6469 RVA: 0x00060634 File Offset: 0x0005E834
		public void Add(string path)
		{
			this.Add(new FileDialogCustomPlace(path));
		}
	}
}
