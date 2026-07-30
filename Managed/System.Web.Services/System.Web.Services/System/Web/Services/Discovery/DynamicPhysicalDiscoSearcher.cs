using System;
using System.IO;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B1 RID: 177
	internal class DynamicPhysicalDiscoSearcher : DynamicDiscoSearcher
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00015A0B File Offset: 0x00013C0B
		internal DynamicPhysicalDiscoSearcher(string searchDir, string[] excludedUrls, string startUrl)
			: base(excludedUrls)
		{
			this.startDir = searchDir;
			this.origUrl = startUrl;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015A22 File Offset: 0x00013C22
		internal override void Search(string fileToSkipAtBegin)
		{
			this.SearchInit(fileToSkipAtBegin);
			base.ScanDirectory(this.startDir);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015A38 File Offset: 0x00013C38
		protected override void SearchSubDirectories(string localDir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(localDir);
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				if (!(directoryInfo2.Name == ".") && !(directoryInfo2.Name == ".."))
				{
					base.ScanDirectory(localDir + "\\" + directoryInfo2.Name);
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015AAC File Offset: 0x00013CAC
		protected override DirectoryInfo GetPhysicalDir(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return null;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(dir);
			if (!directoryInfo.Exists)
			{
				return null;
			}
			if ((directoryInfo.Attributes & (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Temporary)) != (FileAttributes)0)
			{
				return null;
			}
			return directoryInfo;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015AE5 File Offset: 0x00013CE5
		protected override string MakeResultPath(string dirName, string fileName)
		{
			return this.origUrl + dirName.Substring(this.startDir.Length, dirName.Length - this.startDir.Length).Replace('\\', '/') + "/" + fileName;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015B24 File Offset: 0x00013D24
		protected override string MakeAbsExcludedPath(string pathRelativ)
		{
			return this.startDir + "\\" + pathRelativ.Replace('/', '\\');
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00002B51 File Offset: 0x00000D51
		protected override bool IsVirtualSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000352 RID: 850
		private string startDir;
	}
}
