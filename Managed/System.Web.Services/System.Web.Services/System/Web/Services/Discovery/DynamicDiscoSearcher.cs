using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000AF RID: 175
	internal abstract class DynamicDiscoSearcher
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0001572A File Offset: 0x0001392A
		internal DynamicDiscoSearcher(string[] excludeUrlsList)
		{
			this.excludedUrls = excludeUrlsList;
			this.filesFound = new ArrayList();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001574F File Offset: 0x0001394F
		internal virtual void SearchInit(string fileToSkipAtBegin)
		{
			this.subDirLevel = 0;
			this.fileToSkipFirst = fileToSkipAtBegin;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00015760 File Offset: 0x00013960
		protected bool IsExcluded(string url)
		{
			if (this.excludedUrlsTable == null)
			{
				this.excludedUrlsTable = new Hashtable();
				foreach (string text in this.excludedUrls)
				{
					this.excludedUrlsTable.Add(this.MakeAbsExcludedPath(text).ToLower(CultureInfo.InvariantCulture), null);
				}
			}
			return this.excludedUrlsTable.Contains(url.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000157CC File Offset: 0x000139CC
		internal DiscoveryDocument DiscoveryDocument
		{
			get
			{
				return this.discoDoc;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000157D4 File Offset: 0x000139D4
		internal DiscoverySearchPattern[] PrimarySearchPattern
		{
			get
			{
				if (this.primarySearchPatterns == null)
				{
					this.primarySearchPatterns = new DiscoverySearchPattern[]
					{
						new DiscoveryDocumentSearchPattern()
					};
				}
				return this.primarySearchPatterns;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000157F8 File Offset: 0x000139F8
		internal DiscoverySearchPattern[] SecondarySearchPattern
		{
			get
			{
				if (this.secondarySearchPatterns == null)
				{
					this.secondarySearchPatterns = new DiscoverySearchPattern[]
					{
						new ContractSearchPattern(),
						new DiscoveryDocumentLinksPattern()
					};
				}
				return this.secondarySearchPatterns;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015824 File Offset: 0x00013A24
		protected void ScanDirectory(string directory)
		{
			bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
			if (this.IsExcluded(directory))
			{
				return;
			}
			if (!this.ScanDirByPattern(directory, true, this.PrimarySearchPattern))
			{
				if (!this.IsVirtualSearch)
				{
					this.ScanDirByPattern(directory, false, this.SecondarySearchPattern);
				}
				else if (this.subDirLevel != 0)
				{
					DiscoverySearchPattern[] array = new DiscoverySearchPattern[]
					{
						new DiscoveryDocumentLinksPattern()
					};
					this.ScanDirByPattern(directory, false, array);
				}
				if (this.IsVirtualSearch && this.subDirLevel > 0)
				{
					return;
				}
				this.subDirLevel++;
				this.fileToSkipFirst = "";
				this.SearchSubDirectories(directory);
				this.subDirLevel--;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000158D0 File Offset: 0x00013AD0
		protected bool ScanDirByPattern(string dir, bool IsPrimary, DiscoverySearchPattern[] patterns)
		{
			DirectoryInfo physicalDir = this.GetPhysicalDir(dir);
			if (physicalDir == null)
			{
				return false;
			}
			bool traceVerbose = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
			bool flag = false;
			for (int i = 0; i < patterns.Length; i++)
			{
				foreach (FileInfo fileInfo in physicalDir.GetFiles(patterns[i].Pattern))
				{
					if ((fileInfo.Attributes & FileAttributes.Directory) == (FileAttributes)0)
					{
						bool traceVerbose2 = CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
						if (string.Compare(fileInfo.Name, this.fileToSkipFirst, StringComparison.OrdinalIgnoreCase) != 0)
						{
							string text = this.MakeResultPath(dir, fileInfo.Name);
							this.filesFound.Add(text);
							this.discoDoc.References.Add(patterns[i].GetDiscoveryReference(text));
							flag = true;
						}
					}
				}
			}
			return IsPrimary && flag;
		}

		// Token: 0x06000499 RID: 1177
		internal abstract void Search(string fileToSkipAtBegin);

		// Token: 0x0600049A RID: 1178
		protected abstract DirectoryInfo GetPhysicalDir(string dir);

		// Token: 0x0600049B RID: 1179
		protected abstract void SearchSubDirectories(string directory);

		// Token: 0x0600049C RID: 1180
		protected abstract string MakeResultPath(string dirName, string fileName);

		// Token: 0x0600049D RID: 1181
		protected abstract string MakeAbsExcludedPath(string pathRelativ);

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600049E RID: 1182
		protected abstract bool IsVirtualSearch { get; }

		// Token: 0x04000347 RID: 839
		protected string origUrl;

		// Token: 0x04000348 RID: 840
		protected string[] excludedUrls;

		// Token: 0x04000349 RID: 841
		protected string fileToSkipFirst;

		// Token: 0x0400034A RID: 842
		protected ArrayList filesFound;

		// Token: 0x0400034B RID: 843
		protected DiscoverySearchPattern[] primarySearchPatterns;

		// Token: 0x0400034C RID: 844
		protected DiscoverySearchPattern[] secondarySearchPatterns;

		// Token: 0x0400034D RID: 845
		protected DiscoveryDocument discoDoc = new DiscoveryDocument();

		// Token: 0x0400034E RID: 846
		protected Hashtable excludedUrlsTable;

		// Token: 0x0400034F RID: 847
		protected int subDirLevel;
	}
}
