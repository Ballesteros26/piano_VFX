using System;

namespace SFB
{
	// Token: 0x0200002B RID: 43
	public class StandaloneFileBrowser
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00012888 File Offset: 0x00010A88
		public static string[] OpenFilePanel(string title, string directory, string extension, bool multiselect)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			return StandaloneFileBrowser.OpenFilePanel(title, directory, array2, multiselect);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000128CB File Offset: 0x00010ACB
		public static string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000128DC File Offset: 0x00010ADC
		public static void OpenFilePanelAsync(string title, string directory, string extension, bool multiselect, Action<string[]> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			StandaloneFileBrowser.OpenFilePanelAsync(title, directory, array2, multiselect, cb);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00012921 File Offset: 0x00010B21
		public static void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00012933 File Offset: 0x00010B33
		public static string[] OpenFolderPanel(string title, string directory, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFolderPanel(title, directory, multiselect);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00012942 File Offset: 0x00010B42
		public static void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00012954 File Offset: 0x00010B54
		public static string SaveFilePanel(string title, string directory, string defaultName, string extension)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			return StandaloneFileBrowser.SaveFilePanel(title, directory, defaultName, array2);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00012997 File Offset: 0x00010B97
		public static string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
		{
			return StandaloneFileBrowser._platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000129A8 File Offset: 0x00010BA8
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, string extension, Action<string> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			StandaloneFileBrowser.SaveFilePanelAsync(title, directory, defaultName, array2, cb);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000129ED File Offset: 0x00010BED
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
		{
			StandaloneFileBrowser._platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
		}

		// Token: 0x040003AD RID: 941
		private static IStandaloneFileBrowser _platformWrapper = new StandaloneFileBrowserWindows();
	}
}
