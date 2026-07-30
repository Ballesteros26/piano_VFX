using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs;

namespace SFB
{
	// Token: 0x0200002D RID: 45
	public class StandaloneFileBrowserWindows : IStandaloneFileBrowser
	{
		// Token: 0x0600018C RID: 396
		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();

		// Token: 0x0600018D RID: 397 RVA: 0x00012A18 File Offset: 0x00010C18
		public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
		{
			VistaOpenFileDialog vistaOpenFileDialog = new VistaOpenFileDialog();
			vistaOpenFileDialog.Title = title;
			if (extensions != null)
			{
				vistaOpenFileDialog.Filter = StandaloneFileBrowserWindows.GetFilterFromFileExtensionList(extensions);
				vistaOpenFileDialog.FilterIndex = 1;
			}
			else
			{
				vistaOpenFileDialog.Filter = string.Empty;
			}
			vistaOpenFileDialog.Multiselect = multiselect;
			if (!string.IsNullOrEmpty(directory))
			{
				vistaOpenFileDialog.FileName = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			string[] array = ((vistaOpenFileDialog.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) == DialogResult.OK) ? vistaOpenFileDialog.FileNames : new string[0]);
			vistaOpenFileDialog.Dispose();
			return array;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00012A98 File Offset: 0x00010C98
		public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
		{
			cb(this.OpenFilePanel(title, directory, extensions, multiselect));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00012AAC File Offset: 0x00010CAC
		public string[] OpenFolderPanel(string title, string directory, bool multiselect)
		{
			VistaFolderBrowserDialog vistaFolderBrowserDialog = new VistaFolderBrowserDialog();
			vistaFolderBrowserDialog.Description = title;
			if (!string.IsNullOrEmpty(directory))
			{
				vistaFolderBrowserDialog.SelectedPath = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			string[] array;
			if (vistaFolderBrowserDialog.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) != DialogResult.OK)
			{
				array = new string[0];
			}
			else
			{
				(array = new string[1])[0] = vistaFolderBrowserDialog.SelectedPath;
			}
			vistaFolderBrowserDialog.Dispose();
			return array;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00012B0A File Offset: 0x00010D0A
		public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
		{
			cb(this.OpenFolderPanel(title, directory, multiselect));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00012B1C File Offset: 0x00010D1C
		public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
		{
			VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog();
			vistaSaveFileDialog.Title = title;
			string text = "";
			if (!string.IsNullOrEmpty(directory))
			{
				text = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			if (!string.IsNullOrEmpty(defaultName))
			{
				text += defaultName;
			}
			vistaSaveFileDialog.FileName = text;
			if (extensions != null)
			{
				vistaSaveFileDialog.Filter = StandaloneFileBrowserWindows.GetFilterFromFileExtensionList(extensions);
				vistaSaveFileDialog.FilterIndex = 1;
				vistaSaveFileDialog.DefaultExt = extensions[0].Extensions[0];
				vistaSaveFileDialog.AddExtension = true;
			}
			else
			{
				vistaSaveFileDialog.DefaultExt = string.Empty;
				vistaSaveFileDialog.Filter = string.Empty;
				vistaSaveFileDialog.AddExtension = false;
			}
			string text2 = ((vistaSaveFileDialog.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) == DialogResult.OK) ? vistaSaveFileDialog.FileName : "");
			vistaSaveFileDialog.Dispose();
			return text2;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00012BDD File Offset: 0x00010DDD
		public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
		{
			cb(this.SaveFilePanel(title, directory, defaultName, extensions));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
		{
			string text = "";
			foreach (ExtensionFilter extensionFilter in extensions)
			{
				text = text + extensionFilter.Name + "(";
				foreach (string text2 in extensionFilter.Extensions)
				{
					text = text + "*." + text2 + ",";
				}
				text = text.Remove(text.Length - 1);
				text += ") |";
				foreach (string text3 in extensionFilter.Extensions)
				{
					text = text + "*." + text3 + "; ";
				}
				text += "|";
			}
			return text.Remove(text.Length - 1);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00012CD8 File Offset: 0x00010ED8
		private static string GetDirectoryPath(string directory)
		{
			string text = Path.GetFullPath(directory);
			if (!text.EndsWith("\\"))
			{
				text += "\\";
			}
			if (Path.GetPathRoot(text) == text)
			{
				return directory;
			}
			return Path.GetDirectoryName(text) + Path.DirectorySeparatorChar.ToString();
		}
	}
}
