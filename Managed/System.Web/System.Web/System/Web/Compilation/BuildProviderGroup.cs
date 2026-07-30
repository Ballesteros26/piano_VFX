using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x02000646 RID: 1606
	internal class BuildProviderGroup : List<BuildProvider>
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x000BCEA4 File Offset: 0x000BB0A4
		// (set) Token: 0x06004504 RID: 17668 RVA: 0x000BCEAC File Offset: 0x000BB0AC
		public string NamePrefix { get; private set; }

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x000BCEB5 File Offset: 0x000BB0B5
		// (set) Token: 0x06004506 RID: 17670 RVA: 0x000BCEBD File Offset: 0x000BB0BD
		public bool Standalone { get; set; }

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x000BCEC6 File Offset: 0x000BB0C6
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x000BCECE File Offset: 0x000BB0CE
		public bool Application { get; private set; }

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x000BCED7 File Offset: 0x000BB0D7
		// (set) Token: 0x0600450A RID: 17674 RVA: 0x000BCEDF File Offset: 0x000BB0DF
		public bool Master { get; set; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x000BCEE8 File Offset: 0x000BB0E8
		// (set) Token: 0x0600450C RID: 17676 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		public CompilerType CompilerType { get; private set; }

		// Token: 0x0600450E RID: 17678 RVA: 0x000BCF04 File Offset: 0x000BB104
		public void AddProvider(BuildProvider bp)
		{
			if (base.Count == 0)
			{
				if (bp is ApplicationFileBuildProvider)
				{
					this.NamePrefix = "App_global.asax";
					this.Application = true;
				}
				else if (bp is ThemeDirectoryBuildProvider)
				{
					this.NamePrefix = "App_Theme";
					this.Master = true;
				}
				else
				{
					this.NamePrefix = "App_Web";
				}
				CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(bp.LanguageName, null);
				if (defaultCompilerTypeForLanguage != null)
				{
					this.CompilerType = defaultCompilerTypeForLanguage;
				}
			}
			base.Add(bp);
		}
	}
}
