using System;
using System.IO;
using UnityEngine;

namespace NatSuite.Recorders.Internal
{
	// Token: 0x02000049 RID: 73
	public static class Utility
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0001449C File Offset: 0x0001269C
		public static string GetPath(string extension)
		{
			if (Utility.directory == null)
			{
				Utility.directory = ((Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) ? Directory.GetCurrentDirectory() : Application.persistentDataPath);
			}
			string text = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
			string text2 = "recording_" + text + extension;
			return Path.Combine(Utility.directory, text2);
		}

		// Token: 0x040003E3 RID: 995
		private static string directory;
	}
}
