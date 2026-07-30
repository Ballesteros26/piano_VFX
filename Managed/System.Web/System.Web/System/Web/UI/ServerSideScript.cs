using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x02000237 RID: 567
	internal class ServerSideScript
	{
		// Token: 0x06001743 RID: 5955 RVA: 0x0003E894 File Offset: 0x0003CA94
		public ServerSideScript(string script, ILocation location)
		{
			this.Script = script;
			this.Location = location;
		}

		// Token: 0x040015AB RID: 5547
		public readonly string Script;

		// Token: 0x040015AC RID: 5548
		public readonly ILocation Location;
	}
}
