using System;

namespace System.Web
{
	// Token: 0x020000C0 RID: 192
	internal interface IHttpMapPath
	{
		// Token: 0x06000AB0 RID: 2736
		string MapPath(string path);

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000AB1 RID: 2737
		string MachineConfigPath { get; }
	}
}
