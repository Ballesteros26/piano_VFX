using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000033 RID: 51
	[DebuggerNonUserCode]
	internal class SR
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		internal SR()
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000E2E2 File Offset: 0x0000C4E2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceMan == null)
				{
					SR.resourceMan = new ResourceManager("SR", typeof(SR).Assembly);
				}
				return SR.resourceMan;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000E30E File Offset: 0x0000C50E
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000E315 File Offset: 0x0000C515
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000E31D File Offset: 0x0000C51D
		internal static string DataTypes
		{
			get
			{
				return SR.ResourceManager.GetString("DataTypes", SR.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000E333 File Offset: 0x0000C533
		internal static string Keywords
		{
			get
			{
				return SR.ResourceManager.GetString("Keywords", SR.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000E349 File Offset: 0x0000C549
		internal static string MetaDataCollections
		{
			get
			{
				return SR.ResourceManager.GetString("MetaDataCollections", SR.resourceCulture);
			}
		}

		// Token: 0x04000106 RID: 262
		private static ResourceManager resourceMan;

		// Token: 0x04000107 RID: 263
		private static CultureInfo resourceCulture;
	}
}
