using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x0200017F RID: 383
	internal interface IScriptResourceDefinition
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000F85 RID: 3973
		string Path { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000F86 RID: 3974
		string DebugPath { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000F87 RID: 3975
		string CdnPath { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000F88 RID: 3976
		string CdnDebugPath { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000F89 RID: 3977
		string CdnPathSecureConnection { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000F8A RID: 3978
		string CdnDebugPathSecureConnection { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000F8B RID: 3979
		string ResourceName { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000F8C RID: 3980
		Assembly ResourceAssembly { get; }
	}
}
