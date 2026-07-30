using System;

namespace SFB
{
	// Token: 0x02000029 RID: 41
	public interface IStandaloneFileBrowser
	{
		// Token: 0x06000177 RID: 375
		string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

		// Token: 0x06000178 RID: 376
		string[] OpenFolderPanel(string title, string directory, bool multiselect);

		// Token: 0x06000179 RID: 377
		string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

		// Token: 0x0600017A RID: 378
		void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb);

		// Token: 0x0600017B RID: 379
		void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb);

		// Token: 0x0600017C RID: 380
		void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb);
	}
}
