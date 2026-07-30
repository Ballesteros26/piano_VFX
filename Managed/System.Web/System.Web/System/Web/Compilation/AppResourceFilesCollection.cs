using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000612 RID: 1554
	internal class AppResourceFilesCollection
	{
		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000B1076 File Offset: 0x000AF276
		public string SourceDir
		{
			get
			{
				return this.sourceDir;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000B107E File Offset: 0x000AF27E
		public bool HasFiles
		{
			get
			{
				return !string.IsNullOrEmpty(this.sourceDir) && this.files.Count > 0;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x000B109D File Offset: 0x000AF29D
		public List<AppResourceFileInfo> Files
		{
			get
			{
				return this.files;
			}
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000B10A8 File Offset: 0x000AF2A8
		public AppResourceFilesCollection(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.isGlobal = true;
			this.files = new List<AppResourceFileInfo>();
			string text = Path.Combine(HttpRuntime.AppDomainAppPath, "App_GlobalResources");
			if (Directory.Exists(text))
			{
				this.sourceDir = text;
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000B10FC File Offset: 0x000AF2FC
		public AppResourceFilesCollection(string parserDir)
		{
			if (string.IsNullOrEmpty(parserDir))
			{
				throw new ArgumentException("parserDir cannot be empty");
			}
			this.isGlobal = true;
			this.files = new List<AppResourceFileInfo>();
			string text = Path.Combine(parserDir, "App_LocalResources");
			if (Directory.Exists(text))
			{
				this.sourceDir = text;
				HttpApplicationFactory.WatchLocationForRestart(this.sourceDir, "*");
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000B1160 File Offset: 0x000AF360
		public void Collect()
		{
			if (string.IsNullOrEmpty(this.sourceDir))
			{
				return;
			}
			FileInfo[] array = new DirectoryInfo(this.sourceDir).GetFiles();
			if (array.Length == 0)
			{
				return;
			}
			foreach (FileInfo fileInfo in array)
			{
				string extension = fileInfo.Extension;
				AppResourceFileKind appResourceFileKind;
				if (this.Acceptable(extension, out appResourceFileKind))
				{
					AppResourceFileInfo appResourceFileInfo = new AppResourceFileInfo(fileInfo, appResourceFileKind);
					this.files.Add(appResourceFileInfo);
				}
			}
			if (this.isGlobal && this.files.Count == 0)
			{
				return;
			}
			AppResourcesLengthComparer<AppResourceFileInfo> appResourcesLengthComparer = new AppResourcesLengthComparer<AppResourceFileInfo>();
			this.files.Sort(appResourcesLengthComparer);
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x000B1200 File Offset: 0x000AF400
		private bool Acceptable(string extension, out AppResourceFileKind kind)
		{
			string text = extension.ToLower(Helpers.InvariantCulture);
			if (text == ".resx")
			{
				kind = AppResourceFileKind.ResX;
				return true;
			}
			if (!(text == ".resource"))
			{
				kind = AppResourceFileKind.NotResource;
				return false;
			}
			kind = AppResourceFileKind.Resource;
			return true;
		}

		// Token: 0x040023D3 RID: 9171
		private List<AppResourceFileInfo> files;

		// Token: 0x040023D4 RID: 9172
		private bool isGlobal;

		// Token: 0x040023D5 RID: 9173
		private string sourceDir;
	}
}
